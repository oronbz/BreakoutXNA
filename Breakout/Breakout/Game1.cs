using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Breakout.Scenes;
using Breakout.Utils;
using Breakout.Sprites;

namespace Breakout
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SoundCenter soundCenter;
        SpriteFont font;
        Level1 level1;
        Level2 level2;

        Scene curScene;
        Score score;

        Scene gameOverScene;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 640;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            soundCenter = new SoundCenter(this);
            font = Content.Load<SpriteFont>("MyFont");

            Services.AddService(typeof(SpriteBatch), spriteBatch);
            Services.AddService(typeof(SoundCenter), soundCenter);
            Services.AddService(typeof(SpriteFont), font);

            score = new Score(this);
            Components.Add(score);

            gameOverScene = new GameOverScene(this, score);
            gameOverScene.Hide();

            level2 = new Level2(this, score, gameOverScene, null);
            level1 = new Level1(this, score, gameOverScene, level2);

            Components.Add(level1);
            Components.Add(level2);
            Components.Add(gameOverScene);

            level1.Show();
            level2.Hide();

            curScene = level2;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.R) || keyState.IsKeyDown(Keys.F1))
            {
                level1.ResetLevel();
                level1.Show();

                gameOverScene.Hide();
                level2.Hide();
            }

            if (keyState.IsKeyDown(Keys.F2))
            {
                level2.Show();
                level2.ResetLevel();
                
                level1.Hide();
                gameOverScene.Hide();
                
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            base.Draw(gameTime);
            spriteBatch.DrawString(font, "Press F1 or F2 to change levels. ", new Vector2(180, 10), Color.White);
            spriteBatch.End();
        }
    }
}
