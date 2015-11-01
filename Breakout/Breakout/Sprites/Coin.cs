using Breakout.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breakout.Sprites
{
    public class Coin : Brick
    {

        SpriteManager spriteAnimation;

        public Coin(Game game)
            : base(game)
        {
            spriteAnimation = new SpriteManager(game, game.Content.Load<Texture2D>("coin"), 16);
            //sprite = null;
            rect = new Rectangle(0, 0, 24, 24);
            pointReward = 5;
            solid = false;
        }

        public override void Update(GameTime gameTime)
        {
            spriteAnimation.Update(gameTime);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteAnimation.Draw(gameTime, position);
            //base.Draw(gameTime);
        }
    }
}
