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
        public static GraphicsDeviceManager graphics;
        public static string gameState;
        public static SpriteBatch spriteBatch;
        public static SaveAndLoad.SaveData save;
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
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp);

            /* Depending on the state of the game, different functions will be called. */

            switch (gameState)
            {
                case "titleScreen":
                    DrawTitleScreen(gameTime);
                    break;
                case "optionsMenu":
                    //TODO need to draw the screen behind it. mdont make optionsMenu a game state then, make it a kind of flag variable
                    //DrawTitleScreen(gameTime);
                    DrawOptionsMenu();
                    break;
                case "exit":
                    Exit();
                    break;
            }

            Sprite.CheckSpriteMovement(pierre);

            spriteBatch.End();
            base.Draw(gameTime);
        }
        protected override void OnExiting(object sender, EventArgs args)
        {
            SaveAndLoad.SaveGame(save);
            Exit();
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
                    Music("monomoTheme");
                    break;
            }

            spriteBatch.Draw(Content.Load<Texture2D>(titleScreenName), new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), 1, SpriteEffects.None, 0);

            ClickableElements.Button playButton = new ClickableElements.Button("play", 1200, 850, 384, 151, Content.Load<Texture2D>("playButton"), true);
            spriteBatch.Draw(playButton.texture, new Vector2(playButton.xPosition, playButton.yPosition), Color.White);
            ClickableElements.buttonsOnScreen.Add(playButton);

            ClickableElements.Button optionsButton = new ClickableElements.Button("options", 1600, 850, 151, 151, Content.Load<Texture2D>("optionsButton"), true);
            spriteBatch.Draw(optionsButton.texture, new Vector2(optionsButton.xPosition, optionsButton.yPosition), Color.White);
            ClickableElements.buttonsOnScreen.Add(optionsButton);

            float fade = (3 / (float) gameTime.TotalGameTime.TotalSeconds) / 9;
            
            spriteBatch.Draw(Content.Load<Texture2D>("blackFade"), new Vector2(0, 0), Color.White * fade);
        }

        /* Draws the options menu available from the title screen and from the game. */

        public void DrawOptionsMenu()
        {
            Vector2 screenCenter = new Vector2(graphics.GraphicsDevice.Viewport.Bounds.Width / 2, graphics.GraphicsDevice.Viewport.Bounds.Height / 2);
            Vector2 textureCenter = new Vector2(Content.Load<Texture2D>("paused").Width / 2, Content.Load<Texture2D>("paused").Height / 2);
            spriteBatch.Draw(Content.Load<Texture2D>("paused"), screenCenter, null, Color.White, 0f, textureCenter, 0.8f, SpriteEffects.None, 0);

            /* Volume buttons. */

            //TODO make a mute button :( i forgor

            /*
            if (MediaPlayer.Volume != 0)
            {
                if (MediaPlayer.Volume >= 0.1f)
                {
                    ClickableElements.Button tenPercentVolume = new ClickableElements.Button("10", 768, 480, 60, 80, Content.Load<Texture2D>("volumeButtons"), true);
                    spriteBatch.Draw(tenPercentVolume.texture, new Vector2(768, 493), new Rectangle(720, 480, 75, 130), Color.White, 0f, new Vector2(0, 0), 0.80f, SpriteEffects.None, 0);
                    ClickableElements.buttonsOnScreen.Add(tenPercentVolume);
                }

                if (MediaPlayer.Volume >= 0.2f)
                {
                    ClickableElements.Button twentyPercentVolume = new ClickableElements.Button("20", 795, 480, 60, 80, Content.Load<Texture2D>("volumeButtons"), true);
                    spriteBatch.Draw(twentyPercentVolume.texture, new Vector2(827, 493), new Rectangle(795, 480, 75, 130), Color.White, 0f, new Vector2(0, 0), 0.80f, SpriteEffects.None, 0);
                    ClickableElements.buttonsOnScreen.Add(twentyPercentVolume);
                }

                if  (MediaPlayer.Volume >= 0.3f)
                {
                    ClickableElements.Button thirtyPercentVolume = new ClickableElements.Button("30", 864, 480, 60, 80, Content.Load<Texture2D>("volumeButtons"), true);
                    spriteBatch.Draw(thirtyPercentVolume.texture, new Vector2(883, 493), new Rectangle(864, 480, 75, 130), Color.White, 0f, new Vector2(0, 0), 0.80f, SpriteEffects.None, 0);
                    ClickableElements.buttonsOnScreen.Add(thirtyPercentVolume);
                }

                if (MediaPlayer.Volume >= 0.4f)
                {
                    ClickableElements.Button fortyPercentVolume = new ClickableElements.Button("40", 939, 480, 60, 80, Content.Load<Texture2D>("volumeButtons"), true);
                    spriteBatch.Draw(fortyPercentVolume.texture, new Vector2(943, 493), new Rectangle(939, 480, 75, 130), Color.White, 0f, new Vector2(0, 0), 0.80f, SpriteEffects.None, 0);
                    ClickableElements.buttonsOnScreen.Add(fortyPercentVolume);
                }

                if (MediaPlayer.Volume >= 0.5f)
                {
                    ClickableElements.Button fiftyPercentVolume = new ClickableElements.Button("50", 1015, 480, 60, 80, Content.Load<Texture2D>("volumeButtons"), true);
                    spriteBatch.Draw(fiftyPercentVolume.texture, new Vector2(1003, 493), new Rectangle(1015, 480, 75, 130), Color.White, 0f, new Vector2(0, 0), 0.80f, SpriteEffects.None, 0);
                    ClickableElements.buttonsOnScreen.Add(fiftyPercentVolume);
                }

                if (MediaPlayer.Volume >= 0.6f)
                {
                    ClickableElements.Button sixtyPercentVolume = new ClickableElements.Button("60", 1061, 480, 60, 80, Content.Load<Texture2D>("volumeButtons"), true);
                    spriteBatch.Draw(sixtyPercentVolume.texture, new Vector2(1061, 493), new Rectangle(1086, 480, 75, 130), Color.White, 0f, new Vector2(0, 0), 0.80f, SpriteEffects.None, 0);
                    ClickableElements.buttonsOnScreen.Add(sixtyPercentVolume);
                }

                if (MediaPlayer.Volume >= 0.7f)
                {
                    ClickableElements.Button seventyPercentVolume = new ClickableElements.Button("70", 1120, 480, 60, 80, Content.Load<Texture2D>("volumeButtons"), true);
                    spriteBatch.Draw(seventyPercentVolume.texture, new Vector2(1120, 493), new Rectangle(1160, 480, 75, 130), Color.White, 0f, new Vector2(0, 0), 0.80f, SpriteEffects.None, 0);
                    ClickableElements.buttonsOnScreen.Add(seventyPercentVolume);
                }

                if (MediaPlayer.Volume >= 0.8f)
                {
                    ClickableElements.Button eightyPercentVolume = new ClickableElements.Button("80", 1184, 480, 60, 80, Content.Load<Texture2D>("volumeButtons"), true);
                    spriteBatch.Draw(eightyPercentVolume.texture, new Vector2(1184, 493), new Rectangle(1240, 480, 75, 130), Color.White, 0f, new Vector2(0, 0), 0.80f, SpriteEffects.None, 0);
                    ClickableElements.buttonsOnScreen.Add(eightyPercentVolume);
                }

                if (MediaPlayer.Volume >= 0.9f)
                {
                    ClickableElements.Button ninetyPercentVolume = new ClickableElements.Button("90", 1244, 480, 60, 80, Content.Load<Texture2D>("volumeButtons"), true);
                    spriteBatch.Draw(ninetyPercentVolume.texture, new Vector2(1244, 493), new Rectangle(1315, 480, 75, 130), Color.White, 0f, new Vector2(0, 0), 0.80f, SpriteEffects.None, 0);
                    ClickableElements.buttonsOnScreen.Add(ninetyPercentVolume);
                }

                if (MediaPlayer.Volume == 1.0f)
                {
                    ClickableElements.Button hundredPercentVolume = new ClickableElements.Button("100", 1300, 480, 60, 80, Content.Load<Texture2D>("volumeButtons"), true);
                    spriteBatch.Draw(hundredPercentVolume.texture, new Vector2(1300, 493), new Rectangle(1385, 480, 75, 130), Color.White, 0f, new Vector2(0, 0), 0.80f, SpriteEffects.None, 0);
                    ClickableElements.buttonsOnScreen.Add(hundredPercentVolume);
                }
            }
            */
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