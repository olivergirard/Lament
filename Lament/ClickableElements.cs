using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System;
using Microsoft.Xna.Framework.Content;
using System.Collections;

namespace Lament
{
    public class ClickableElements
    {
        static string gameState = StartGame.gameState;
        static ContentManager content = StartGame.content;
        public static ArrayList buttonsOnScreen = new ArrayList();

        public struct Button
        {
            public string key { get; set; }
            public int xPosition { get; set; }
            public int yPosition { get; set; }
            public Texture2D texture { get; set; }
            public bool onScreen { get; set; }

            public Button(string key, int xPosition, int yPosition, Texture2D texture, bool onScreen)
            {
                this.key = key;
                this.xPosition = xPosition;
                this.yPosition = yPosition;
                this.texture = texture;
                this.onScreen = onScreen;
            }
        }

        public static bool CursorInButton(Button button)
        {
            if ((StartGame.mouseState.X < button.xPosition + button.texture.Width) && (StartGame.mouseState.X > button.xPosition))
            {
                if ((StartGame.mouseState.Y < button.yPosition + button.texture.Height) && (StartGame.mouseState.Y > button.yPosition))
                {
                    return true;
                }
            }

            return false;
        }

        public static void Update(GameTime gameTime, Button button)
        {

            if ((CursorInButton(button) == true) && (StartGame.mouseState.LeftButton == ButtonState.Pressed) && (StartGame.previousMouseState.LeftButton == ButtonState.Released))
            {
                switch (gameState)
                {
                    case "titleScreen":

                        TitleScreenButtons(button);
                        break;
                }
            }
        }

        /* Triggers if a button on the title screen was pressed. */

        public static void TitleScreenButtons(Button button)
        {
            if (button.key == "play")
            {
                MediaPlayer.Stop();
            }
        }
    }
}
