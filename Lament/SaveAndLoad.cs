using Microsoft.Xna.Framework.Media;
using System;
using System.IO;
using System.Text.Json;

namespace Lament
{
    public class SaveAndLoad
    {
        public struct SaveData
        {
            public string[] unlockedCharacters { get; set; }
            public Gear.Head[] equippedHeads { get; set; }
            public Gear.Body[] equippedBodies { get; set; }
            public Gear.Accessory[] equippedAccessories { get; set; }
            public Gear.Weapon[] equippedWeapons { get; set; }
            public float volumeState { get; set; }

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
                SaveData data = JsonSerializer.Deserialize<SaveData>(File.ReadAllText(savePath));
                saveGameData.unlockedCharacters = data.unlockedCharacters;
                saveGameData.volumeState = data.volumeState;
                MediaPlayer.Volume = saveGameData.volumeState;
            }
            else
            {
                Party.Character kurage = Party.InitializeKurage();
                Party.Character monomo = Party.InitializeMonomo();
                Party.Character lazare = Party.InitializeLazare();
                Party.Character krieden = Party.InitializeKrieden();

                Party party = Party.AddToParty(lazare, kurage, monomo, krieden);

                saveGameData.equippedHeads = Gear.EquippedHeads(party);
                saveGameData.equippedBodies = Gear.EquippedBodies(party);
                saveGameData.equippedAccessories = Gear.EquippedAccessories(party);
                saveGameData.equippedWeapons = Gear.EquippedWeapons(party);

                string[] temp = { "Lazare", "Kurage", "Monomo", "Krieden"};
                saveGameData.unlockedCharacters = temp;
                saveGameData.volumeState = 1.0f;
            }

            return saveGameData;
        }

        /* Updates the save.json in the AppData/Roaming/Lament folder. */
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
