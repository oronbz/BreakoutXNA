using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breakout.Utils
{
    public class SpriteManager : DrawableGameComponent
    {
        private float timeElapsed;
        public bool IsLooping = true;
        private float timeToUpdate = 0.05f;


        public int FramesPerSecond
        {
            set { timeToUpdate = (1f / value); }
        }
        SpriteBatch spriteBatch;
        protected Texture2D Texture;
        public Vector2 Position = Vector2.Zero;
        public Color Color = Color.White;
        public Vector2 Origin;
        public float Rotation = 0f;
        public float Scale = 1f;
        public SpriteEffects SpriteEffect;
        protected Rectangle[] Rectangles;
        protected int FrameIndex = 0;

        public SpriteManager(Game game, Texture2D Texture, int frames)
            : base(game)
        {
            spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            this.Texture = Texture;
            int width = Texture.Width / frames;
            Rectangles = new Rectangle[frames];
            for (int i = 0; i < frames; i++)
                Rectangles[i] = new Rectangle(
                    i * width, 0, width, Texture.Height);
            IsLooping = true;
            FramesPerSecond = 30;
        }

        public void SetFrame(int frame)
        {
            if (frame < Rectangles.Length)
                FrameIndex = frame;
        }

        public override void Update(GameTime gameTime)
        {
            ////0=================================  
            // KeyboardState prevKbState = kbState;
            // kbState = Keyboard.GetState();

            // if (kbState.IsKeyDown(Keys.Space) && prevKbState.IsKeyUp(Keys.Space))
            //     SetFrame(random.Next(0, 6));
            // //0========================================================

            timeElapsed += (float)
                 gameTime.ElapsedGameTime.TotalSeconds;

            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;

                if (FrameIndex < Rectangles.Length - 1)
                    FrameIndex++;
                else if (IsLooping)
                    FrameIndex = 0;
            }
            //1===============================




            base.Update(gameTime);
        }


        public void Draw(GameTime gameTime, Vector2 position)
        {
            spriteBatch.Draw(Texture, position, Rectangles[FrameIndex],
                Color, Rotation, Origin, Scale, SpriteEffect, 0f);
        }

        public override void Draw(GameTime gametime)
        {
            spriteBatch.Draw(Texture, Position, Rectangles[FrameIndex],
                Color, Rotation, Origin, Scale, SpriteEffect, 0f);
        }
    }
}
