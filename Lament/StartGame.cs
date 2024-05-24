using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

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

        float fadeIn = 1;
        float fadeOut = 0;
        
        int random;

        public static MouseState mouseState;
        public static MouseState previousMouseState;

        public static Texture2D capture = null;

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

            blur = Content.Load<Effect>("blur");
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
                gameState = "pauseMenu";
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
                    DrawTitleScreen();
                    break;

                case "loadMenu":
                    DrawLoadSelectMenu();
                    break;

                case "advancedOptionsMenu":
                    DrawAdvancedOptionsMenu();
                    break;

                case "galleryMenu":
                    DrawGalleryMenu();
                    break;

                case "pauseMenu":
                    if (capture == null)
                    {
                        capture = CaptureScreen();
                    }
                    spriteBatch.Draw(capture, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                    DrawPauseMenu();
                    break;

                case "basicOptionsMenu":
                    if (capture == null)
                    {
                        capture = CaptureScreen();
                    }
                    spriteBatch.Draw(capture, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                    DrawBasicOptionsMenu();
                    break;

                case "exit":
                    Exit();
                    break;

                case "newGame":
                    Dialogue.DisplayLine(1);
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            SaveAndLoad.SaveGame(save); //TODO: Turn this off, the user needs to save, not the computer!
            Exit();
            base.OnExiting(sender, args);
        }

        /* Draws the title screen differently depending on a random number generated. */
        public void DrawTitleScreen()
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

            ClickableElements.Button newButton = new ClickableElements.Button("new", 1363, 240, 360, 100, Content.Load<Texture2D>("newGame"), true, "titleMenu");
            spriteBatch.Draw(newButton.texture, new Vector2(newButton.xPosition, newButton.yPosition), Color.White);
            ClickableElements.buttonsOnScreen.Add(newButton);

            ClickableElements.Button playButton = new ClickableElements.Button("load", 1363, 407, 360, 100, Content.Load<Texture2D>("load"), true, "titleMenu");
            spriteBatch.Draw(playButton.texture, new Vector2(playButton.xPosition, playButton.yPosition), Color.White);
            ClickableElements.buttonsOnScreen.Add(playButton);

            ClickableElements.Button galleryButton = new ClickableElements.Button("gallery", 1363, 574, 360, 100, Content.Load<Texture2D>("gallery"), true, "titleMenu");
            spriteBatch.Draw(galleryButton.texture, new Vector2(galleryButton.xPosition, galleryButton.yPosition), Color.White);
            ClickableElements.buttonsOnScreen.Add(galleryButton);

            ClickableElements.Button optionsButton = new ClickableElements.Button("options", 1363, 741, 360, 100, Content.Load<Texture2D>("settings"), true, "titleMenu");
            spriteBatch.Draw(optionsButton.texture, new Vector2(optionsButton.xPosition, optionsButton.yPosition), Color.White);
            ClickableElements.buttonsOnScreen.Add(optionsButton);

            if (fadeIn >= 0)
            {
                fadeIn -= 0.01f;
                spriteBatch.Draw(Content.Load<Texture2D>("blackFade"), new Vector2(0, 0), Color.White * fadeIn);
            }
        }

        /* Captures the last outputted image on the screen and applies a blur effect to it. */
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

        /* Draws the menu that appears upon hitting the ESC key. */
        public void DrawPauseMenu()
        {
            Vector2 screenCenter = new Vector2(graphics.GraphicsDevice.Viewport.Bounds.Width / 2, graphics.GraphicsDevice.Viewport.Bounds.Height / 2);
            Vector2 textureCenter = new Vector2(Content.Load<Texture2D>("pause").Width / 2, Content.Load<Texture2D>("pause").Height / 2);
            spriteBatch.Draw(Content.Load<Texture2D>("pause"), screenCenter, null, Color.White, 0f, textureCenter, 1.0f, SpriteEffects.None, 0);

            ClickableElements.Button basicOptionsButton = new ClickableElements.Button("basicOptions", 810, 400, 360, 100, Content.Load<Texture2D>("options"), true, "pauseMenu");
            spriteBatch.Draw(basicOptionsButton.texture, new Vector2(basicOptionsButton.xPosition, basicOptionsButton.yPosition), Color.White);
            ClickableElements.buttonsOnScreen.Add(basicOptionsButton);

            ClickableElements.Button exitButton = new ClickableElements.Button("exit", 810, 567, 360, 100, Content.Load<Texture2D>("exit"), true, "pauseMenu");
            spriteBatch.Draw(exitButton.texture, new Vector2(exitButton.xPosition, exitButton.yPosition), Color.White);
            ClickableElements.buttonsOnScreen.Add(exitButton);
        }

        /* Draws the basic options menu available from the game. */
        public void DrawBasicOptionsMenu()
        {
            ClickableElements.RemoveFromMenu("titleScreen");

            Vector2 screenCenter = new Vector2(graphics.GraphicsDevice.Viewport.Bounds.Width / 2, graphics.GraphicsDevice.Viewport.Bounds.Height / 2);
            Vector2 textureCenter = new Vector2(Content.Load<Texture2D>("basicOptions").Width / 2, Content.Load<Texture2D>("basicOptions").Height / 2);
            spriteBatch.Draw(Content.Load<Texture2D>("basicOptions"), screenCenter, null, Color.White, 0f, textureCenter, 1.0f, SpriteEffects.None, 0);

            ClickableElements.Button fullscreenButton = new ClickableElements.Button("fullscreenToggle", 216, 238, 151, 134, Content.Load<Texture2D>("toggle"), true, "basicOptionsMenu");
            spriteBatch.Draw(fullscreenButton.texture, new Vector2(fullscreenButton.xPosition, fullscreenButton.yPosition), Color.White);
            ClickableElements.buttonsOnScreen.Add(fullscreenButton);

            ClickableElements.Button windowButton = new ClickableElements.Button("windowToggle", 586, 238, 151, 134, Content.Load<Texture2D>("toggle"), true, "basicOptionsMenu");
            spriteBatch.Draw(windowButton.texture, new Vector2(windowButton.xPosition, windowButton.yPosition), Color.White);
            ClickableElements.buttonsOnScreen.Add(windowButton);

            if (graphics.IsFullScreen == true)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("windowMolby"), new Vector2(216, 188), null, Color.White);

            } else
            {
                spriteBatch.Draw(Content.Load<Texture2D>("windowMolby"), new Vector2(586, 188), null, Color.White);
            }

        }

        /* Draws the advanced options meny available from the title screen. */
        public void DrawAdvancedOptionsMenu()
        {
            if (fadeOut <= 1)
            {
                fadeOut += 0.01f;
                spriteBatch.Draw(Content.Load<Texture2D>("blackFade"), new Vector2(0, 0), Color.White * fadeOut);
                fadeIn = 1.0f;
            }
            else
            {
                Vector2 screenCenter = new Vector2(graphics.GraphicsDevice.Viewport.Bounds.Width / 2, graphics.GraphicsDevice.Viewport.Bounds.Height / 2);
                Vector2 textureCenter = new Vector2(Content.Load<Texture2D>("yokoTitle").Width / 2, Content.Load<Texture2D>("yokoTitle").Height / 2);
                spriteBatch.Draw(Content.Load<Texture2D>("yokoTitle"), screenCenter, null, Color.White, 0f, textureCenter, 1.0f, SpriteEffects.None, 0);

                fadeIn -= 0.01f;
                spriteBatch.Draw(Content.Load<Texture2D>("blackFade"), new Vector2(0, 0), Color.White * fadeIn);
            }
        }

        /* Draws the menu for viewing the gallery images. */
        public void DrawGalleryMenu()
        {
            if (fadeOut <= 1)
            {
                fadeOut += 0.01f;
                spriteBatch.Draw(Content.Load<Texture2D>("blackFade"), new Vector2(0, 0), Color.White * fadeOut);
                fadeIn = 1.0f;
            } else
            {
                Vector2 screenCenter = new Vector2(graphics.GraphicsDevice.Viewport.Bounds.Width / 2, graphics.GraphicsDevice.Viewport.Bounds.Height / 2);
                Vector2 textureCenter = new Vector2(Content.Load<Texture2D>("morrisTitle").Width / 2, Content.Load<Texture2D>("morrisTitle").Height / 2);
                spriteBatch.Draw(Content.Load<Texture2D>("morrisTitle"), screenCenter, null, Color.White, 0f, textureCenter, 1.0f, SpriteEffects.None, 0);

                fadeIn -= 0.01f;
                spriteBatch.Draw(Content.Load<Texture2D>("blackFade"), new Vector2(0, 0), Color.White * fadeIn);
            }
        }

        /* Draws the menu that can be used to select a previous save file. */
        public void DrawLoadSelectMenu()
        {
            if (fadeOut <= 1)
            {
                fadeOut += 0.01f;
                spriteBatch.Draw(Content.Load<Texture2D>("blackFade"), new Vector2(0, 0), Color.White * fadeOut);
                fadeIn = 1.0f;
            }
            else
            {
                Vector2 screenCenter = new Vector2(graphics.GraphicsDevice.Viewport.Bounds.Width / 2, graphics.GraphicsDevice.Viewport.Bounds.Height / 2);
                Vector2 textureCenter = new Vector2(Content.Load<Texture2D>("rubyTitle").Width / 2, Content.Load<Texture2D>("rubyTitle").Height / 2);
                spriteBatch.Draw(Content.Load<Texture2D>("rubyTitle"), screenCenter, null, Color.White, 0f, textureCenter, 1.0f, SpriteEffects.None, 0);

                fadeIn -= 0.01f;
                spriteBatch.Draw(Content.Load<Texture2D>("blackFade"), new Vector2(0, 0), Color.White * fadeIn);
            }
        }

        /* Controls the music that plays on startup. */
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