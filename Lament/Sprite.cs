using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Lament
{
    public class Sprite
    {
        static ContentManager content = StartGame.content;
        public struct Pierre
        {
            public bool onScreen { get; set; }
            public string spriteImage { get; set; }
            public int x { get; set; }
            public int y { get; set; }

            public Pierre(bool onScreen, string spriteImage, int x, int y)
            {
                this.onScreen = onScreen;
                this.spriteImage = spriteImage;
                this.x = x;
                this.y = y;
            }
        }

        /* Checks the movement of the main sprite on screen. */

        public static void CheckSpriteMovement(Pierre pierre)
        {
            int walkSpeed = 5;
            int runSpeed = 10;

            if (pierre.onScreen == true)
            {

                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        pierre.x -= runSpeed;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        pierre.x += runSpeed;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.S))
                    {
                        pierre.y += runSpeed;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        pierre.y -= runSpeed;
                    }

                }
                else
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        pierre.x -= walkSpeed;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        pierre.x += walkSpeed;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.S))
                    {
                        pierre.y += walkSpeed;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        pierre.y -= walkSpeed;
                    }
                }

                StartGame.spriteBatch.Draw(content.Load<Texture2D>(pierre.spriteImage), new Vector2(pierre.x, pierre.y), Color.White);
            }
        }
    }
}
