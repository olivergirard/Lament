using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Drawing;
using System.Text.Json;

namespace Lament
{
    public class StartGame : Game
    {

        private GraphicsDeviceManager graphics;
        string gameState;
        SpriteBatch spriteBatch;
        SaveAndLoad.SaveData save;
        int random;
        MouseState mouseState;

        public StartGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            gameState = "titleScreen";

            save = SaveAndLoad.LoadGame();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


        /* Master draw function. */

        protected override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();

            switch (gameState)
            {
                case "titleScreen":
                    DrawTitleScreen();
                    break;
            }

            spriteBatch.End();


        }
        protected override void OnExiting(object sender, EventArgs args)
        {
            SaveAndLoad.SaveGame(save);
            base.OnExiting(sender, args);
        }

        public void DrawTitleScreen()
        {

            if (random == 0)
            {
                random = (new Random()).Next(1, save.unlockedCharacters.Length + 1);
            }

            string titleScreenName = "";

            switch (random)
            {
                case 1:
                    titleScreenName = "pierreTitle";
                    break;
                case 2:
                    titleScreenName = "morrisTitle";
                    break;
                case 3:
                    titleScreenName = "rubyTitle";
                    break;
                case 4:
                    titleScreenName = "yokoTitle";
                    break;
            }


            spriteBatch.Draw(Content.Load<Texture2D>(titleScreenName), new Vector2(0, 0), Microsoft.Xna.Framework.Color.White);


            //draw buttons next
        }
    }
}