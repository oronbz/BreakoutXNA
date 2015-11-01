using Breakout.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breakout.Sprites
{
    public class Ball : DrawableGameComponent
    {
        public Rectangle rect;

        Game game;
        SpriteBatch spriteBatch;
        SoundCenter soundCenter;
        Texture2D sprite;
        Vector2 position;
        Vector2 velocity;
        float maxSpeed = 300;

        public Ball(Game game)
            : base(game)
        {
            this.game = game;
            sprite = game.Content.Load<Texture2D>("ball");
            spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            soundCenter = (SoundCenter)game.Services.GetService(typeof(SoundCenter));
            ResetBall();
        }

        public void ResetBall()
        {
            position = new Vector2(game.GraphicsDevice.Viewport.Width / 2 - sprite.Width / 2,
                                               game.GraphicsDevice.Viewport.Height - sprite.Height * 6);
            rect = new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height);

            velocity = new Vector2(0, 300);
        }

        public override void Update(GameTime gameTime)
        {
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            position += velocity * dt;

            CheckBoundries();

            // update rect
            rect = new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height);

            base.Update(gameTime);
        }

        public void PaddleCollisionDetected(Rectangle paddleRect)
        {
            soundCenter.Hit.Play();
            position.Y = paddleRect.Top - sprite.Height;
            velocity.Y *= -1;

            float ballCenterX = rect.Center.X;
            float paddleCenterX = paddleRect.Center.X;

            float speed = Math.Abs(paddleCenterX - ballCenterX);
            float xSpeed = (speed / (paddleRect.Width / 2 - rect.Width / 2)) * maxSpeed;
            if (xSpeed < 1) xSpeed = 1;
            
            bool movingLeft = ballCenterX < paddleCenterX;

            velocity.X = movingLeft ? -xSpeed : xSpeed;
        }

        public void BrickCollisionDetected(Brick brick)
        {
            if (brick.solid)
            {
                soundCenter.Hit.Play();
                velocity.Y *= -1;
            }
            else
            {
                soundCenter.PickupCoin.Play();
            }
        }

        private void CheckBoundries()
        {
            if (position.X < 0 || position.X > game.GraphicsDevice.Viewport.Width - sprite.Width)
            {
                soundCenter.Hit.Play();
                position.X = MathHelper.Clamp(position.X, 0, game.GraphicsDevice.Viewport.Width - sprite.Width);
                velocity.X *= -1;
            }
            if (position.Y < 0)
            {
                soundCenter.Hit.Play();
                position.Y = MathHelper.Clamp(position.Y, 0, game.GraphicsDevice.Viewport.Height - sprite.Height);
                velocity.Y *= -1;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(sprite, position, Color.White);
            base.Draw(gameTime);
        }

        protected override void Dispose(bool disposing)
        {
            sprite.Dispose();
            base.Dispose(disposing);
        }
    }
}
