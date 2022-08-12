using System;
using System.IO;
using System.Text.Json;

namespace Lament
{
    public class SaveAndLoad
    {

        public struct SaveData
        {
            public int num { get; set; }
        }

        public static SaveData LoadGame()
        {
            string gamePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Lament");

            if ((File.Exists(gamePath)) == false)
            {
                Directory.CreateDirectory(gamePath);
            }


            SaveData saveGameData = new SaveData();
            string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Lament", "save.json");

            if (File.Exists(savePath))
            {
                saveGameData = JsonSerializer.Deserialize<SaveData>(File.ReadAllText(savePath), options);
            }
            else
            {
                saveGameData.num = 0;
            }

            return saveGameData;
        }

        public static void SaveGame(SaveData saveFile)
        {
            string saveData = JsonSerializer.Serialize(saveFile, options);
            string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Lament", "save.json");
            File.WriteAllText(savePath, saveData);
        }

        /* Makes the save data .json file human-readable. */

        private static JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };
    }
}
