using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended;
using MonoGame.Extended.Collections;
using MonoGame.Extended.Screens;
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
        public static Sprite.Pierre pierre;
        public static Song music;
        public static ContentManager content;
        
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

            content = Content;

            //TODO remove this! used for debugging only
            pierre = new Sprite.Pierre(true, "pierreCasual", 0, 0);

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
            previousMouseState = mouseState;
            mouseState = Mouse.GetState();

            foreach (ClickableElements.Button button in ClickableElements.buttonsOnScreen)
            {
                ClickableElements.Update(gameTime, button);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                //TODO make this the escape menu. gameState = "exit" is only one of the possible cases for the buttons
                gameState = "exit";
            }

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
                case "exit":
                    Exit();
                    break;
            }

            Sprite.CheckSpriteMovement(pierre);

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
            
            if (random == 0)
            {
                //TODO 1, not 5. this was used for debugging title
                random = (new Random()).Next(5, save.unlockedCharacters.Length + 1);
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
                    Music("title");
                    break;
            }

            spriteBatch.Draw(Content.Load<Texture2D>(titleScreenName), new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), 0.35f, SpriteEffects.None, 0);

            ClickableElements.Button playButton = new ClickableElements.Button("play", 1200, 850, Content.Load<Texture2D>("playButton"), true);
            ClickableElements.buttonsOnScreen.Add(playButton);
            spriteBatch.Draw(playButton.texture, new Vector2(playButton.xPosition, playButton.yPosition), Color.White);

            ClickableElements.Button settingsButton = new ClickableElements.Button("options", 1600, 850, Content.Load<Texture2D>("optionsButton"), true);
            spriteBatch.Draw(settingsButton.texture, new Vector2(settingsButton.xPosition, settingsButton.yPosition), Color.White);

            float fade = (3 / (float) gameTime.TotalGameTime.TotalSeconds) / 9;
            
            spriteBatch.Draw(Content.Load<Texture2D>("blackFade"), new Vector2(0, 0), Color.White * fade);
        }

        /* Draws the options menu available from the title screen and from the game. */

        public void DrawOptionsMenu()
        {


        }

        public void Music(string name)
        {
            if (music == null)
            {
                music = Content.Load<Song>(name);
                MediaPlayer.Play(music);
                MediaPlayer.IsRepeating = true;
            }
        }
    }
}