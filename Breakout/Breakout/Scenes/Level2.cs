using Breakout.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breakout.Scenes
{
    public class Level2 : Level
    {
        public Level2(Game game, Score score, Scene gameOverScene, Level nextLevel)
            : base(game, score, gameOverScene, nextLevel)
        {

        }

        public override void CreateBricks()
        {
            bricks = new List<Brick>();

            for (int y = 1; y < 5; y++)
            {
                for (int x = 1; x < 19; x++)
                {
                    Brick brick = null;
                    if ((y + x) % 2 == 0)
                    {
                        brick = new RedBrick(game);
                    }
                    else
                    {
                        brick = new Brick(game);
                    }
                    brick.Position = new Vector2(x * brickSize, y * brickSize);
                    bricks.Add(brick);
                    SceneComponents.Add(brick);
                }
            }
        }


    }
}
