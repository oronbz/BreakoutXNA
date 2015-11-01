using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breakout.Sprites
{
    public class RedBrick : Brick
    {
        Texture2D blueSprite;
        bool blue = false;

        public RedBrick(Game game)
            : base(game)
        {
            blueSprite = sprite;
            sprite = game.Content.Load<Texture2D>("red_brick");
            pointReward = 3;
        }

        public override void Hit()
        {
            if (!blue)
            {
                sprite = blueSprite;
                blue = true;
            }
            else
            {
                Enabled = false;
            }
            
        }
    }
}
