using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace Lament
{
    public class StartGame : Game
    {
        public GraphicsDeviceManager graphics;
        public static string gameState;
        public static SpriteBatch spriteBatch;
        SaveAndLoad.SaveData save;
        public static Sprite sprite;
        public Sprite.Pierre pierre;
        
        int random;

        public static MouseState mouseState;
        public static MouseState previousMouseState;

        public StartGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.IsFullScreen = true;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            gameState = "titleScreen";
            sprite = new Sprite(this.Content);

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

            sprite.CheckSpriteMovement(pierre);

            spriteBatch.End();
        }
        protected override void OnExiting(object sender, EventArgs args)
        {
            SaveAndLoad.SaveGame(save);
            base.OnExiting(sender, args);
        }

        /* Draws the title screen differently depending on a random number generated. */

        public void DrawTitleScreen(GameTime gameTime)
        {
            //TODO 1, not 5
            if (random == 0)
            {
                random = (new Random()).Next(5, save.unlockedCharacters.Length + 1);
            }

            string titleScreenName = "";
            Vector2 logoPosition = new Vector2(0, 0);

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
                    logoPosition = new Vector2(160, 20);
                    break;
            }

            spriteBatch.Draw(Content.Load<Texture2D>(titleScreenName), new Vector2(0, 0), Color.White);
            spriteBatch.Draw(Content.Load<Texture2D>("logo"), logoPosition, Color.White);

            //ClickableElements.Button playButton = new ClickableElements.Button("play", 180, 90, Content.Load<Texture2D>("playButton"));
            //spriteBatch.Draw(playButton.texture, new Vector2(playButton.xPosition, playButton.yPosition), Color.White);

            float fade = (3 / (float) gameTime.TotalGameTime.TotalSeconds) / 9;
            
            spriteBatch.Draw(Content.Load<Texture2D>("blackFade"), new Vector2(0, 0), Color.White * fade);
        }

        /* Draws the options menu available from the title screen and from the game. */

        public void DrawOptionsMenu()
        {


        }
    }
}