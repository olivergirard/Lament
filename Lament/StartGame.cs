using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Drawing;
using System.Text.Json;

namespace Lament
{
    public class StartGame : Game
    {

        private GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        SaveAndLoad.SaveData save;

        public StartGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            save = SaveAndLoad.LoadGame();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>(@"C:\Users\azure\source\repos\Lament\Content\bin\DesktopGL\Content\font");

        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);

            spriteBatch.Begin();
            string output = "" + save.num;
            Vector2 fontorigin = font.MeasureString(output) / 2;
            spriteBatch.DrawString(font, output, fontorigin, Microsoft.Xna.Framework.Color.Blue);
            spriteBatch.End();
        }
        protected override void OnExiting(object sender, EventArgs args)
        {
            (save.num)++;
            SaveAndLoad.SaveGame(save);
            base.OnExiting(sender, args);
        }
    }
}