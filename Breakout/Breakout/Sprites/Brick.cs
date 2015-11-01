using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breakout.Sprites
{
    public class Brick : DrawableGameComponent
    {

        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                rect.X = (int)value.X;
                rect.Y = (int)value.Y;
                position = value;
            }
        }

        public Texture2D sprite;
        public Rectangle rect;
        public bool solid;
        public int pointReward;

        protected Game game;
        protected SpriteBatch spriteBatch;
        protected Vector2 position = Vector2.Zero;


        public Brick(Game game)
            : base(game)
        {
            this.game = game;
            sprite = game.Content.Load<Texture2D>("brick");
            spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            rect = new Rectangle(0, 0, sprite.Width, sprite.Height);
            solid = true;
            pointReward = 1;
        }

        public virtual void Hit()
        {
            Enabled = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
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
