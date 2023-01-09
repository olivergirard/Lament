using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lament
{
    public class SpriteMovement
    {
        KeyState keyState;

        public static void CheckSpriteMovement(Pierre.Sprite pierre)
        {
            if (pierre.onScreen == true)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    
                } else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    
                } else if (Keyboard.GetState().IsKeyDown(Keys.Down)) 
                { 
                    
                } else if (Keyboard.GetState().IsKeyDown(Keys.Up)) 
                {
                    
                }
            }
        }














    }
}
