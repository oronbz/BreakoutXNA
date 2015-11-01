using Breakout.Sprites;
using Breakout.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breakout.Scenes
{
    public abstract class Level : Scene
    {
        protected Game game;
        private SpriteBatch spriteBatch;
        private SoundCenter soundCenter;
        private SpriteFont font;
        private Background bg;
        private Paddle paddle;
        private Ball ball;
        private Score score;
        private Scene gameOverScene;
        private Level nextLevel;

        private bool won;

        protected List<Brick> bricks;
        bool started = false;
        protected const int brickSize = 32;

        public Level(Game game, Score score, Scene gameOverScene, Level nextLevel)
            : base(game)
        {
            this.game = game;
            spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            soundCenter = (SoundCenter)game.Services.GetService(typeof(SoundCenter));
            font = (SpriteFont)game.Services.GetService(typeof(SpriteFont));

            bg = new Background(game);
            paddle = new Paddle(game);
            ball = new Ball(game);
            this.score = score;
            this.gameOverScene = gameOverScene;
            this.nextLevel = nextLevel;

            SceneComponents.Add(bg);
            SceneComponents.Add(paddle);
            SceneComponents.Add(ball);
            SceneComponents.Add(this.score);

            // pause paddle and ball until start
            paddle.Enabled = false;
            ball.Enabled = false;

            bricks = new List<Brick>();

            CreateBricks();
        }

        public void ResetLevel()
        {
            foreach (var brick in bricks)
            {
                SceneComponents.Remove(brick);
            }
            bricks = new List<Brick>();

            ball.ResetBall();
            paddle.ResetPaddle();

            CreateBricks();

            started = false;
            won = false;
            ball.Enabled = false;
            paddle.Enabled = false;

            score.ResetPoints();
        }

        public abstract void CreateBricks();

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            if (won && keyState.IsKeyDown(Keys.Space) && nextLevel != null)
            {
                nextLevel.Show();
                this.Hide();
            }

            if (!started)
            {
                
                if (keyState.IsKeyDown(Keys.Left) || keyState.IsKeyDown(Keys.Right))
                {
                    started = true;
                    ball.Enabled = true;
                    paddle.Enabled = true;
                }

            }

            CheckWin();
            
            CheckBallPaddleIntersection();

            CheckBallBricksIntersection();

            CheckBallFell();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (!started)
            {
                DrawInstructions();
            }
            if (won)
            {
                DrawWin();
            }
            
        }

        private void DrawWin()
        {
            String instructionsText = "";
            if (nextLevel == null)
            {
                instructionsText = "You Won the game! Press 'R' to restart the game.";
            }
            else
            {
                instructionsText = "You Won! Press 'Space' for next level.";
            }
            
            Vector2 size = font.MeasureString(instructionsText);
            Vector2 origin = size * 0.5f;

            spriteBatch.DrawString(
                font,
                instructionsText,
                new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2),
                Color.White,
                0,
                origin,
                1,
                SpriteEffects.None,
                0);
        }

        private void DrawInstructions()
        {
            var instructionsText = "Use left and right keys to move the paddle";
            Vector2 size = font.MeasureString(instructionsText);
            Vector2 origin = size * 0.5f;

            spriteBatch.DrawString(
                font,
                instructionsText,
                new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2),
                Color.White,
                0,
                origin,
                1,
                SpriteEffects.None,
                0);
        }

        private void CheckWin()
        {
            if (bricks.Count == 0)
            {
                won = true;
                ball.Enabled = false;
                paddle.Enabled = false;
            }
        }

        private void CheckBallPaddleIntersection()
        {
            if (ball.rect.Intersects(paddle.rect))
            {
                ball.PaddleCollisionDetected(paddle.rect);
            }
        }

        private void CheckBallFell()
        {
            if (ball.rect.Y + ball.rect.Height > game.GraphicsDevice.Viewport.Height)
            {
                gameOverScene.Show();
                this.Hide();
            }
        }

        private void CheckBallBricksIntersection()
        {
            Brick collidedBrick = null;
            foreach (var brick in bricks)
            {
                if (ball.rect.Intersects(brick.rect))
                {
                    ball.BrickCollisionDetected(brick);
                    brick.Hit();
                    collidedBrick = brick;
                    break;
                }
            }
            if (collidedBrick != null && !collidedBrick.Enabled)
            {
                score.AddPoints(collidedBrick.pointReward);
                bricks.Remove(collidedBrick);
                SceneComponents.Remove(collidedBrick);
            }
        }

        private void _initialize() { }
        
    }
}
