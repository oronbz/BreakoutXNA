using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breakout.Sprites
{
    public class Paddle : DrawableGameComponent
    {
        public Rectangle rect;

        Game game;
        SpriteBatch spriteBacth;
        Texture2D smallSprite;
        Texture2D sprite;
        Vector2 position;
        float moveSpeed = 400f;

        public Paddle(Game game)
            : base(game)
        {
            this.game = game;
            smallSprite = game.Content.Load<Texture2D>("paddle_small");
            sprite = smallSprite;
            spriteBacth = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            ResetPaddle();
        }

        public void ResetPaddle()
        {
            position = new Vector2(game.GraphicsDevice.Viewport.Width / 2 - sprite.Width / 2,
                                   game.GraphicsDevice.Viewport.Height - sprite.Height * 2);

            rect = new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            CheckInput(dt);
            CheckBoundaries();

            // update rect
            rect = new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height / 2);

            base.Update(gameTime);
        }

        private void CheckInput(float dt) 
        {
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Right)) position.X += moveSpeed * dt;
            if (keyState.IsKeyDown(Keys.Left)) position.X -= moveSpeed * dt;
        }

        private void CheckBoundaries()
        {
            if (position.X < 0)
            {
                position.X = 0;
            }
            else if (position.X > game.GraphicsDevice.Viewport.Width - sprite.Width)
            {
                position.X = game.GraphicsDevice.Viewport.Width - sprite.Width;
            }

        }

        public override void Draw(GameTime gameTime)
        {
            spriteBacth.Draw(sprite, position, Color.White);
            base.Draw(gameTime);
        }

        protected override void Dispose(bool disposing)
        {
            smallSprite.Dispose();
            base.Dispose(disposing);
        }
    }
}
