using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Media;

namespace Lament
{
    public class Dialogue
    {
        public struct Line
        {
            public int lineNumber { get; set; }
            public string text { get; set; }
        }

        public List<Line> dialogue { get; set; }

        public static void DisplayLine(int lineNumber)
        {
            string dialogueLocation = (Path.Combine(Path.GetFullPath(StartGame.content.RootDirectory), "dialogue.json"));
            Console.WriteLine(dialogueLocation);

            if (File.Exists(dialogueLocation))
            {
                string jsonString = File.ReadAllText(dialogueLocation);

                Dialogue dialogueData = System.Text.Json.JsonSerializer.Deserialize<Dialogue>(jsonString);

                //TODO edit!
                string line = dialogueData.dialogue[lineNumber].text;

                MediaPlayer.Stop();
            }
        }
    }
}
