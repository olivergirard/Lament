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
                    Music("kurageTitle");
                    break;
            }

            spriteBatch.Draw(Content.Load<Texture2D>(titleScreenName), new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), 0.35f, SpriteEffects.None, 0);

            ClickableElements.Button playButton = new ClickableElements.Button("play", 1200, 850, Content.Load<Texture2D>("playButton"), true);
            spriteBatch.Draw(playButton.texture, new Vector2(playButton.xPosition, playButton.yPosition), Color.White);
            ClickableElements.buttonsOnScreen.Add(playButton);

            ClickableElements.Button optionsButton = new ClickableElements.Button("options", 1600, 850, Content.Load<Texture2D>("optionsButton"), true);
            spriteBatch.Draw(optionsButton.texture, new Vector2(optionsButton.xPosition, optionsButton.yPosition), Color.White);
            ClickableElements.buttonsOnScreen.Add(optionsButton);

            float fade = (3 / (float) gameTime.TotalGameTime.TotalSeconds) / 9;
            
            spriteBatch.Draw(Content.Load<Texture2D>("blackFade"), new Vector2(0, 0), Color.White * fade);
        }

        /* Draws the options menu available from the title screen and from the game. */

        public void DrawOptionsMenu()
        {
            Vector2 screenCenter = new Vector2(graphics.GraphicsDevice.Viewport.Bounds.Width / 2, graphics.GraphicsDevice.Viewport.Bounds.Height / 2);
            Vector2 textureCenter = new Vector2(Content.Load<Texture2D>("optionsMenu").Width / 2, Content.Load<Texture2D>("optionsMenu").Height / 2);
            spriteBatch.Draw(Content.Load<Texture2D>("optionsMenu"), screenCenter, null, Color.White, 0f, textureCenter, 0.80f, SpriteEffects.None, 0);

            /* Volume buttons. */

            if (save.volumeState != 0)
            {
                if (save.volumeState >= 10)
                {
                    ClickableElements.Button tenPercentVolume = new ClickableElements.Button("10", 0, 0, Content.Load<Texture2D>("volumeButtons"), true);
                    spriteBatch.Draw(tenPercentVolume.texture, new Vector2(768, 493), new Rectangle(720, 480, 75, 130), Color.White, 0f, new Vector2(0, 0), 0.80f, SpriteEffects.None, 0);
                    ClickableElements.buttonsOnScreen.Add(tenPercentVolume);
                }

                if (save.volumeState >= 20)
                {
                    ClickableElements.Button twentyPercentVolume = new ClickableElements.Button("20", 0, 0, Content.Load<Texture2D>("volumeButtons"), true);
                    spriteBatch.Draw(twentyPercentVolume.texture, new Vector2(827, 493), new Rectangle(795, 480, 75, 130), Color.White, 0f, new Vector2(0, 0), 0.80f, SpriteEffects.None, 0);
                    ClickableElements.buttonsOnScreen.Add(twentyPercentVolume);
                }

                if  (save.volumeState >= 30)
                {
                    ClickableElements.Button thirtyPercentVolume = new ClickableElements.Button("30", 0, 0, Content.Load<Texture2D>("volumeButtons"), true);
                    spriteBatch.Draw(thirtyPercentVolume.texture, new Vector2(883, 493), new Rectangle(864, 480, 75, 130), Color.White, 0f, new Vector2(0, 0), 0.80f, SpriteEffects.None, 0);
                    ClickableElements.buttonsOnScreen.Add(thirtyPercentVolume);
                }

                if (save.volumeState >= 40)
                {
                    ClickableElements.Button fortyPercentVolume = new ClickableElements.Button("40", 0, 0, Content.Load<Texture2D>("volumeButtons"), true);
                    spriteBatch.Draw(fortyPercentVolume.texture, new Vector2(943, 493), new Rectangle(939, 480, 75, 130), Color.White, 0f, new Vector2(0, 0), 0.80f, SpriteEffects.None, 0);
                    ClickableElements.buttonsOnScreen.Add(fortyPercentVolume);
                }

                if (save.volumeState >= 50)
                {
                    ClickableElements.Button fiftyPercentVolume = new ClickableElements.Button("50", 0, 0, Content.Load<Texture2D>("volumeButtons"), true);
                    spriteBatch.Draw(fiftyPercentVolume.texture, new Vector2(1003, 493), new Rectangle(1015, 480, 75, 130), Color.White, 0f, new Vector2(0, 0), 0.80f, SpriteEffects.None, 0);
                    ClickableElements.buttonsOnScreen.Add(fiftyPercentVolume);
                }

                if (save.volumeState >= 60)
                {
                    ClickableElements.Button sixtyPercentVolume = new ClickableElements.Button("60", 0, 0, Content.Load<Texture2D>("volumeButtons"), true);
                    spriteBatch.Draw(sixtyPercentVolume.texture, new Vector2(1061, 493), new Rectangle(1086, 480, 75, 130), Color.White, 0f, new Vector2(0, 0), 0.80f, SpriteEffects.None, 0);
                    ClickableElements.buttonsOnScreen.Add(sixtyPercentVolume);
                }

                if (save.volumeState >= 70)
                {
                    ClickableElements.Button seventyPercentVolume = new ClickableElements.Button("70", 0, 0, Content.Load<Texture2D>("volumeButtons"), true);
                    spriteBatch.Draw(seventyPercentVolume.texture, new Vector2(1120, 493), new Rectangle(1160, 480, 75, 130), Color.White, 0f, new Vector2(0, 0), 0.80f, SpriteEffects.None, 0);
                    ClickableElements.buttonsOnScreen.Add(seventyPercentVolume);
                }

                if (save.volumeState >= 80)
                {
                    ClickableElements.Button eightyPercentVolume = new ClickableElements.Button("80", 0, 0, Content.Load<Texture2D>("volumeButtons"), true);
                    spriteBatch.Draw(eightyPercentVolume.texture, new Vector2(1184, 493), new Rectangle(1240, 480, 75, 130), Color.White, 0f, new Vector2(0, 0), 0.80f, SpriteEffects.None, 0);
                    ClickableElements.buttonsOnScreen.Add(eightyPercentVolume);
                }

                if (save.volumeState >= 90)
                {
                    ClickableElements.Button ninetyPercentVolume = new ClickableElements.Button("90", 0, 0, Content.Load<Texture2D>("volumeButtons"), true);
                    spriteBatch.Draw(ninetyPercentVolume.texture, new Vector2(1244, 493), new Rectangle(1315, 480, 75, 130), Color.White, 0f, new Vector2(0, 0), 0.80f, SpriteEffects.None, 0);
                    ClickableElements.buttonsOnScreen.Add(ninetyPercentVolume);
                }

                if (save.volumeState == 100)
                {
                    ClickableElements.Button hundredPercentVolume = new ClickableElements.Button("100", 0, 0, Content.Load<Texture2D>("volumeButtons"), true);
                    spriteBatch.Draw(hundredPercentVolume.texture, new Vector2(1300, 493), new Rectangle(1385, 480, 75, 130), Color.White, 0f, new Vector2(0, 0), 0.80f, SpriteEffects.None, 0);
                    ClickableElements.buttonsOnScreen.Add(hundredPercentVolume);
                }
            }
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