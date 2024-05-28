using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

namespace Lament
{
    public class Dialogue
    {
        static bool wasDialogueDataRead = false;

        public struct Line
        {
            public int lineNumber { get; set; }
            public string text { get; set; }
        }

        public List<Line> dialogue { get; set; }
        public static Dialogue dialogueData;

        public static void DisplayLine(int lineNumber)
        {
            string dialogueLocation = (Path.Combine(Path.GetFullPath(StartGame.content.RootDirectory), "dialogue.json"));
            Console.WriteLine(dialogueLocation);

            /* Reading the dialogue in at the first prompt for a line. */
            if ((wasDialogueDataRead == false) && (File.Exists(dialogueLocation)))
            {
                string jsonString = File.ReadAllText(dialogueLocation);
                dialogueData = System.Text.Json.JsonSerializer.Deserialize<Dialogue>(jsonString);
                wasDialogueDataRead = true;
            }

            string line = dialogueData.dialogue[lineNumber].text;
            System.Diagnostics.Debug.WriteLine(line);

            StartGame.spriteBatch.DrawString(StartGame.spriteFont, line, new Vector2(100, 100), Color.Black);
        }
    }
}