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
        public static ArrayList buttonsOnScreen = new ArrayList();

        public struct Button
        {
            public string key { get; set; }
            public int xPosition { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public int yPosition { get; set; }
            public Texture2D texture { get; set; }
            public bool onScreen { get; set; }

            public Button(string key, int xPosition, int yPosition, int width, int height, Texture2D texture, bool onScreen)
            {
                this.key = key;
                this.xPosition = xPosition;
                this.yPosition = yPosition;
                this.width = width;
                this.height = height;
                this.texture = texture;
                this.onScreen = onScreen;
            }
        }

        public static bool CursorInButton(Button button)
        {
            if ((StartGame.mouseState.X < button.xPosition + button.width) && (StartGame.mouseState.X > button.xPosition))
            {
                if ((StartGame.mouseState.Y < button.yPosition + button.height) && (StartGame.mouseState.Y > button.yPosition))
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
                switch (StartGame.gameState)
                {
                    case "titleScreen":
                        TitleScreenButtons(button);
                        break;
                    case "optionsMenu":
                        OptionsMenuButtons(button);
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
            } else if (button.key == "options")
            {
                StartGame.gameState = "optionsMenu";
            }
        }

        /* Triggers if a button on the options menu was pressed. */
        public static void OptionsMenuButtons(Button button)
        {
            if (button.key == "10")
            {
                MediaPlayer.Volume = 0.1f;
            } else if (button.key == "20")
            {
                MediaPlayer.Volume = 0.2f;
            } else if (button.key == "30")
            {
                MediaPlayer.Volume = 0.3f;
            } else if (button.key == "40")
            {
                MediaPlayer.Volume = 0.4f;
            } else if (button.key == "50")
            {
                MediaPlayer.Volume = 0.5f;
            } else if (button.key == "60")
            {
                MediaPlayer.Volume = 0.6f;
            } else if (button.key == "70")
            {
                MediaPlayer.Volume = 0.7f;
            } else if (button.key == "80")
            {
                MediaPlayer.Volume = 0.8f;
            } else if (button.key == "90")
            {
                MediaPlayer.Volume = 0.9f;
            } else if (button.key == "100")
            {
                MediaPlayer.Volume = 1.0f;
            }
        }
    }
}
