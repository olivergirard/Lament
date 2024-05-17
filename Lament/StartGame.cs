using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.IO;

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
        Effect blur;
        
        int random;

        public static MouseState mouseState;
        public static MouseState previousMouseState;

        Texture2D capture = null;

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

            save = SaveAndLoad.LoadGame();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            blur = Content.Load<Effect>("Blur");
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
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp);

            /* Depending on the state of the game, different functions will be called. */

            switch (gameState)
            {
                case "titleScreen":
                    DrawTitleScreen(gameTime);
                    break;
                case "optionsMenu":

                    if (capture == null)
                    {
                        capture = CaptureScreen();
                    }
                    spriteBatch.Draw(capture, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                    DrawOptionsMenu();

                    break;
                case "exit":
                    Exit();
                    break;
            }

            //Sprite.CheckSpriteMovement(pierre);

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
                    titleScreenName = "kurageTitle";
                    break;
                case 3:
                    titleScreenName = "lazareTitle";
                    break;
                case 4:
                    titleScreenName = "kriedenTitle";
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

        public Texture2D CaptureScreen()
        {
            int w = GraphicsDevice.PresentationParameters.BackBufferWidth;
            int h = GraphicsDevice.PresentationParameters.BackBufferHeight;

            int[] backBuffer = new int[w * h];
            GraphicsDevice.GetBackBufferData(backBuffer);

            Texture2D sourceTexture = new Texture2D(GraphicsDevice, w, h, false, GraphicsDevice.PresentationParameters.BackBufferFormat);
            sourceTexture.SetData(backBuffer);

            RenderTarget2D renderTarget1 = new RenderTarget2D(GraphicsDevice, sourceTexture.Width, sourceTexture.Height);
            RenderTarget2D renderTarget2 = new RenderTarget2D(GraphicsDevice, sourceTexture.Width, sourceTexture.Height);

            GraphicsDevice.SetRenderTarget(renderTarget1);
            GraphicsDevice.Clear(Color.Transparent);

            blur.Parameters["pixelSize"].SetValue(new Vector2(1.0f / sourceTexture.Width, 0));
            blur.CurrentTechnique.Passes[0].Apply();
            spriteBatch.Draw(sourceTexture, Vector2.Zero, Color.White);

            GraphicsDevice.SetRenderTarget(renderTarget2);
            GraphicsDevice.Clear(Color.Transparent);

            blur.Parameters["pixelSize"].SetValue(new Vector2(0, 1.0f / sourceTexture.Height));
            blur.CurrentTechnique.Passes[0].Apply();
            spriteBatch.Draw(renderTarget1, Vector2.Zero, Color.White);

            GraphicsDevice.SetRenderTarget(null);

            Texture2D blurredTexture = new Texture2D(GraphicsDevice, sourceTexture.Width, sourceTexture.Height);
            Color[] pixels = new Color[sourceTexture.Width * sourceTexture.Height];
            renderTarget2.GetData(pixels);
            blurredTexture.SetData(pixels);

            return blurredTexture;
        }

        /* Draws the options menu available from the title screen and from the game. */
        public void DrawOptionsMenu()
        {
            Vector2 screenCenter = new Vector2(graphics.GraphicsDevice.Viewport.Bounds.Width / 2, graphics.GraphicsDevice.Viewport.Bounds.Height / 2);
            Vector2 textureCenter = new Vector2(Content.Load<Texture2D>("paused").Width / 2, Content.Load<Texture2D>("paused").Height / 2);
            spriteBatch.Draw(Content.Load<Texture2D>("paused"), screenCenter, null, Color.White, 0f, textureCenter, 0.8f, SpriteEffects.None, 0);
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