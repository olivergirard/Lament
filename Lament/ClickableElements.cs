using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System.Collections;

namespace Lament
{
    public class ClickableElements
    {
        public static int mouseX;
        public static int mouseY;

        /* A list of all of the buttons on the screen. */
        public static ArrayList buttonsOnScreen = new ArrayList();

        /* Source rectangles for the sliders on the basic options menu. */
        public static Rectangle masterVolumeSourceRectangle = new Rectangle(0, 0, 685, 86);

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

        /* Determines whether there is a cursor inside of a button. */
        public static bool CursorInButton(Button button)
        {
            float scaleX = (float)StartGame.windowWidth / GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            float scaleY = (float)StartGame.windowHeight / GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            mouseX = (int)(StartGame.mouseState.X / scaleX);
            mouseY = (int)(StartGame.mouseState.Y / scaleY);

            if ((mouseX >= button.xPosition) && (mouseX < button.xPosition + button.width) && (mouseY >= button.yPosition) && (mouseY < button.yPosition + button.height))
            {
                return true;
            }

            return false;
        }


        /* Updates what buttons are shown on the screen based on the current game state. */
        public static void Update(GameTime gameTime, Button button)
        {
            if ((CursorInButton(button) == true) && (StartGame.mouseState.LeftButton == ButtonState.Pressed) && (StartGame.previousMouseState.LeftButton == ButtonState.Released))
            {
                switch (StartGame.gameState)
                {
                    case "titleScreen":
                        buttonsOnScreen.Clear();
                        TitleScreenButtons(button);
                        break;
                    case "pauseMenu":
                        buttonsOnScreen.Clear();
                        PauseMenuButtons(button);
                        break;
                    case "basicOptionsMenu":
                        buttonsOnScreen.Clear();
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

        /* Triggers if a button on the pause menu was pressed. */
        public static void PauseMenuButtons(Button button)
        {
            if (button.key == "basicOptions")
            {
                StartGame.gameState = "basicOptionsMenu";

                /* Resetting the potential for an effect, such as a fade or a blur.*/
                StartGame.captureForEffect = null;
            }
            else if (button.key == "exit")
            {
                StartGame.gameState = "exit";
            }
        }

        /* Triggers if a button on the basic options menu, the one accessible at any point in the game, was pressed. */
        public static void BasicOptionsMenuButtons(Button button)
        {
            if (button.key == "windowToggle")
            {
                StartGame.graphics.IsFullScreen = false;
                StartGame.windowWidth = 1280;
                StartGame.windowHeight = 720;
                StartGame.graphics.PreferredBackBufferWidth = StartGame.windowWidth;
                StartGame.graphics.PreferredBackBufferHeight = StartGame.windowHeight;

                StartGame.graphics.ApplyChanges();

            } else if (button.key == "fullscreenToggle") {

                StartGame.graphics.IsFullScreen = true;
                StartGame.windowWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                StartGame.windowHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                StartGame.graphics.PreferredBackBufferWidth = StartGame.windowWidth;
                StartGame.graphics.PreferredBackBufferHeight = StartGame.windowHeight;

                StartGame.graphics.ApplyChanges();

            } else if (button.key == "masterVolume")
            {
                MediaPlayer.Volume = System.Math.Clamp(((float)(mouseX - button.xPosition) / button.width), 0.0f, 1.0f);
                masterVolumeSourceRectangle = new Rectangle(0, 0, (mouseX - button.xPosition), button.height);
            }
        }

        /* Removes a button from the array of buttons on the screen. */
        public static void RemoveFromMenu(string menu)
        {
            foreach (Button button in buttonsOnScreen.ToArray())
            {
                if (button.menu == menu)
                {
                    buttonsOnScreen.Remove(button);
                }
            }
        }

        /* Adds a button to the list of buttons on the screen as long as it has not already been added. */
        public static void AddButton(Button button)
        {
            bool added = false;

            foreach (Button element in buttonsOnScreen.ToArray())
            {
                if (element.key == button.key)
                {
                    added = true;
                }
            }

            if (added == false)
            {
                buttonsOnScreen.Add(button);
            }
        }
    }
}
