using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breakout.Sprites
{
    public class Background : DrawableGameComponent
    {
        Game game;
        SpriteBatch spriteBatch;
        Texture2D sprite;

        public Background(Game game)
            : base(game)        {  
            this.game = game;
            sprite = game.Content.Load<Texture2D>("breakout_bg");
            spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
         }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Draw(sprite, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
        }

        protected override void Dispose(bool disposing)
        {
            sprite.Dispose();
            base.Dispose(disposing);
        }
    }
}
