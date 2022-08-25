using Newtonsoft.Json;
using System.IO;
using System.Text.Json;
using static Lament.SaveAndLoad;

namespace Lament
{
    public class Gear
    {
        public struct Weapon 
        {
            public string who;
            public string name;
            public int damage;
            public string info;
        }

        public struct Head
        {
            public string who;
            public string name;
            public int defense;
            public string info;
        }

        public struct Body
        {
            public string who;
            public string name;
            public int defense;
            public string info;
        }

        public struct Accessory 
        {
            public string who;
            public string name;
            public int defense;
            public string info;
        }

        public static void WriteGearToJson(string savePath, SaveData saveData)
        {
            SaveAndLoad.SaveData saveGameData = System.Text.Json.JsonSerializer.Deserialize<SaveData>(File.ReadAllText(savePath), options);

            foreach (Head item in saveData.equippedHeads)
            {
                Head head = JsonConvert.DeserializeObject<Head>(JsonConvert.SerializeObject(item));
                string headData = System.Text.Json.JsonSerializer.Serialize(head, options);
                File.WriteAllText(savePath, headData);
            }

            foreach(Weapon item in saveData.equippedWeapons)
            {
                Weapon weapon = JsonConvert.DeserializeObject<Weapon>(JsonConvert.SerializeObject(item));
                string weaponData = System.Text.Json.JsonSerializer.Serialize(weapon, options);
                File.WriteAllText(savePath, weaponData);
            }

            foreach (Accessory item in saveData.equippedAccessories)
            {
                Accessory accessory = JsonConvert.DeserializeObject<Accessory>(JsonConvert.SerializeObject(item));
                string accessoryData = System.Text.Json.JsonSerializer.Serialize(accessory, options);
                File.WriteAllText(savePath, accessoryData);
            }

            foreach (Body item in saveData.equippedBodies)
            {
                Body body = JsonConvert.DeserializeObject<Body>(JsonConvert.SerializeObject(item));
                string bodyData = System.Text.Json.JsonSerializer.Serialize(body, options);
                File.WriteAllText(savePath, bodyData);
            }
        }

        private static JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };

        public static Head[] EquippedHeads(Party party)
        {
            Head[] equipped = new Head[4];

            equipped[0] = party.member1.head;
            equipped[1] = party.member2.head;
            equipped[2] = party.member3.head;
            equipped[3] = party.member4.head;

            return equipped;
        }

        public static Body[] EquippedBodies(Party party)
        {
            Body[] equipped = new Body[4];

            equipped[0] = party.member1.body;
            equipped[1] = party.member2.body;
            equipped[2] = party.member3.body;
            equipped[3] = party.member4.body;

            return equipped;
        }

        public static Accessory[] EquippedAccessories(Party party)
        {
            Accessory[] equipped = new Accessory[4];

            equipped[0] = party.member1.accessory;
            equipped[1] = party.member2.accessory;
            equipped[2] = party.member3.accessory;
            equipped[3] = party.member4.accessory;

            return equipped;
        }

        public static Weapon[] EquippedWeapons(Party party)
        {
            Weapon[] equipped = new Weapon[4];

            equipped[0] = party.member1.weapon;
            equipped[1] = party.member2.weapon;
            equipped[2] = party.member3.weapon;
            equipped[3] = party.member4.weapon;

            return equipped;
        }
    }
}
