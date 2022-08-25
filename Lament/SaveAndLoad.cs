using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;

namespace Lament
{
    public class SaveAndLoad
    {

        public struct SaveData
        {
            public int num { get; set; }
            public Gear.Head[] equippedHeads { get; set; }
            public Gear.Body[] equippedBodies { get; set; }
            public Gear.Accessory[] equippedAccessories { get; set; }
            public Gear.Weapon[] equippedWeapons { get; set; }

        }

        /* Loads the game from the save.json in the AppData/Roaming/Lament folder. */

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
                Party.Character pierre = Party.InitializePierre();
                Party.Character morris = Party.InitializeMorris();
                Party.Character ruby = Party.InitializeRuby();
                Party.Character yoko = Party.InitializeYoko();

                Party party = Party.AddToParty(pierre, morris, ruby, yoko);

                saveGameData.num = 0;

                saveGameData.equippedHeads = Gear.EquippedHeads(party);
                saveGameData.equippedBodies = Gear.EquippedBodies(party);
                saveGameData.equippedAccessories = Gear.EquippedAccessories(party);
                saveGameData.equippedWeapons = Gear.EquippedWeapons(party);
            }

            return saveGameData;
        }

        /* Updates the save.json in the AppData/Roaming/Lament folder. */

        public static void SaveGame(SaveData saveFile)
        {
            string saveData = JsonSerializer.Serialize(saveFile, options);
            string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Lament", "save.json");
            File.WriteAllText(savePath, saveData);

            Gear.WriteGearToJson(savePath, saveFile);
        }

        /* Makes the save data .json file human-readable. */

        private static JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };
    }
}
