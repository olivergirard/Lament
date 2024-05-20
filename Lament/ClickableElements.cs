using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System.Collections;
using System.Text.RegularExpressions;

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

            public string menu { get; set; }

            public Button(string key, int xPosition, int yPosition, int width, int height, Texture2D texture, bool onScreen, string menu)
            {
                this.key = key;
                this.xPosition = xPosition;
                this.yPosition = yPosition;
                this.width = width;
                this.height = height;
                this.texture = texture;
                this.onScreen = onScreen;
                this.menu = menu;
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
                    case "pauseMenu":
                        PauseMenuButtons(button);
                        break;
                    case "basicOptionsMenu":
                        BasicOptionsMenuButtons(button);
                        break;
                }
            }
        }

        /* Triggers if a button on the title screen was pressed. */
        public static void TitleScreenButtons(Button button)
        {
            if (button.key == "new")
            {
                StartGame.gameState = "newGame";
                MediaPlayer.Stop();
            } else if (button.key == "load") {

                StartGame.gameState = "loadMenu";
            }
            else if (button.key == "gallery")
            {
                StartGame.gameState = "galleryMenu";
            }
            else if (button.key == "options") 
            {
                StartGame.gameState = "advancedOptionsMenu";
            }
        }

        /* Triggers if a button on the options menu was pressed. */
        public static void PauseMenuButtons(Button button)
        {
            if (button.key == "basicOptions")
            {
                StartGame.gameState = "basicOptionsMenu";
                StartGame.capture = null;
            }
            else if (button.key == "exit")
            {
                StartGame.gameState = "exit";
            }
        }

        public static void BasicOptionsMenuButtons(Button button)
        {
            if (button.key == "windowToggle")
            {
                StartGame.graphics.IsFullScreen = true;
                StartGame.graphics.ToggleFullScreen();

            } else if (button.key == "fullscreenToggle") {
                StartGame.graphics.IsFullScreen = false;
                StartGame.graphics.ToggleFullScreen();
            }
        }

        public static void RemoveFromMenu(string menu)
        {
            foreach (Button button in buttonsOnScreen)
            {
                if (button.menu == menu)
                {
                    buttonsOnScreen.Remove(button);
                }
            }
        }
    }
}
