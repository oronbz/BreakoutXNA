using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Breakout.Sprites;
using Breakout.Utils;

namespace Breakout.Scenes
{
    public class GameOverScene : Scene
    {
        protected Game game;
        private SpriteBatch spriteBatch;
        private SoundCenter soundCenter;
        private SpriteFont font;
        private Score score;

        public GameOverScene(Game game, Score score)
            : base(game)
        {
            this.game = game;
            spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            soundCenter = (SoundCenter)game.Services.GetService(typeof(SoundCenter));
            font = (SpriteFont)game.Services.GetService(typeof(SpriteFont));

            this.score = score;

            SceneComponents.Add(this.score);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
                var gameOverText = "Game Over";
                Vector2 size = font.MeasureString(gameOverText);
                Vector2 origin = size * 0.5f;

                spriteBatch.DrawString(
                    font,
                    gameOverText,
                    new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2),
                    Color.White,
                    0,
                    origin,
                    2,
                    SpriteEffects.None,
                    0);


                var instructionsText = "Press 'R' to restart the game";
                size = font.MeasureString(instructionsText);
                origin = size * 0.5f;

                spriteBatch.DrawString(
                    font,
                    instructionsText,
                    new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2 + 30f),
                    Color.White,
                    0,
                    origin,
                    1,
                    SpriteEffects.None,
                    0);
        }

    }
}
