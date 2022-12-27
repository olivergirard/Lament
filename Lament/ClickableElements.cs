using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Lament
{
    public class ClickableElements
    {
        MouseState mouseState;
        MouseState previousMouseState;
        string gameState;
        public ClickableElements(StartGame game)
        {
            mouseState = StartGame.mouseState;
            previousMouseState = StartGame.previousMouseState;
            gameState = StartGame.gameState;
        }

        public struct Button
        {
            public string key { get; set; }
            public int xPosition { get; set; }
            public int yPosition { get; set; }
            public Texture2D texture { get; set; }

            public Button(string key, int xPosition, int yPosition, Texture2D texture)
            {
                this.key = key;
                this.xPosition = xPosition;
                this.yPosition = yPosition;
                this.texture = texture;
            }
        }

        public bool CursorInButton(Button button)
        {
            if ((mouseState.X < button.xPosition + button.texture.Width) && (mouseState.X > button.xPosition))
            {
                if ((mouseState.Y < button.yPosition + button.texture.Height) && (mouseState.Y > button.yPosition))
                {
                    return true;
                }
            }

            return false;
        }

        public void Update(GameTime gameTime, Button button)
        {

            if ((CursorInButton(button) == true) && (mouseState.LeftButton == ButtonState.Pressed) && (previousMouseState.LeftButton == ButtonState.Released))
            {
                switch (gameState)
                {
                    case "titleScreen":
                        TitleScreenButtons(button);
                        break;
                }
            }
        }

        public void TitleScreenButtons(Button button)
        {
            switch (button.key)
            {
                case "play":
                    break;
                case "settings":
                    break;
            }
        }
    }
}
