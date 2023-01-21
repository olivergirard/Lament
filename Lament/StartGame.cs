using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Runtime.CompilerServices;

namespace Lament
{
    public class StartGame : Game
    {

        private GraphicsDeviceManager graphics;
        public static string gameState;
        public static SpriteBatch spriteBatch;
        SaveAndLoad.SaveData save;
        public Sprite.Pierre pierre;
        
        int random;

        public static MouseState mouseState;
        public static MouseState previousMouseState;

        public StartGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            gameState = "titleScreen";

            //TODO remove this! used for debugging only
            pierre.onScreen = true;
            pierre.spriteImage = "pierreCasual";

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
            mouseState = Mouse.GetState();
            previousMouseState = mouseState;

            base.Update(gameTime);
        }


        /* Master draw function. */

        protected override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();

            /* Depending on the state of the game, different functions will be called. */

            switch (gameState)
            {
                case "titleScreen":
                    DrawTitleScreen(gameTime);
                    break;
                case "optionsMenu":
                    DrawOptionsMenu();
                    break;
            }

            CheckSpriteMovement(pierre);

            spriteBatch.End();
        }
        protected override void OnExiting(object sender, EventArgs args)
        {
            SaveAndLoad.SaveGame(save);
            base.OnExiting(sender, args);
        }

        public void DrawTitleScreen(GameTime gameTime)
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
                case 5:
                    titleScreenName = "monomoTitle";
                    break;
            }

            spriteBatch.Draw(Content.Load<Texture2D>(titleScreenName), new Vector2(0, 0), Color.White);

            ClickableElements.Button playButton = new ClickableElements.Button("play", 180, 90, Content.Load<Texture2D>("playButton"));
            spriteBatch.Draw(playButton.texture, new Vector2(playButton.xPosition, playButton.yPosition), Color.White);
            
            float fade = (3 / (float) gameTime.TotalGameTime.TotalSeconds) / 9;
            spriteBatch.Draw(Content.Load<Texture2D>("blackFade"), new Vector2(0, 0), Color.White * fade);
            
        }

        public void DrawOptionsMenu()
        {


        }

        //TODO add a running feature based on if another key is pressed too?

        public void CheckSpriteMovement(Sprite.Pierre pierre)
        {
            if (pierre.onScreen == true)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    Sprite.Pierre.x -= 5;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    Sprite.Pierre.x += 5;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    Sprite.Pierre.y += 5;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    Sprite.Pierre.y -= 5;
                }

                spriteBatch.Draw(Content.Load<Texture2D>(pierre.spriteImage), new Vector2(Sprite.Pierre.x, Sprite.Pierre.y), Color.White);

            }
        }
    }
}