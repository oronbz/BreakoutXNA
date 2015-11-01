using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breakout.Sprites
{
    public class Score : DrawableGameComponent
    {
        public Rectangle rect;

        Game game;
        SpriteBatch spriteBacth;
        Vector2 position;
        SpriteFont font;
        int points;

        public Score(Game game)
            : base(game)
        {
            this.game = game;
            spriteBacth = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            position = new Vector2(10, 10);
            font = (SpriteFont)game.Services.GetService(typeof(SpriteFont));
            points = 0;
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public void AddPoints(int points)
        {
            this.points += points;
        }

        public int GetPoints()
        {
            return points;
        }

        public void ResetPoints()
        {
            points = 0;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBacth.DrawString(font, "Score: " + points, position, Color.White);
            base.Draw(gameTime);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
