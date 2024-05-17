using System;

namespace Lament
{
    public class Party
    {
        public struct Character
        {
            public string name;
            public int level;
            public int health;
            public int special;
            public Gear.Head head;
            public Gear.Body body;
            public Gear.Weapon weapon;
            public Gear.Accessory accessory;
        }

        public Character member1;
        public Character member2;
        public Character member3;
        public Character member4;

        //TODO when doing battle interface, set special to 0 at very end of usage
        //TODO print these values to test how different they are from one another
        public void SpecialDamage(Character character)
        {
            var random = new Random();

            if (character.name == "Krieden")
            {
                for (int i = 0; i < character.level + 2; i++)
                {
                    character.special += random.Next(0, i);
                }

            } else if (character.name == "Monomo")
            {
                for (int i = 0; i < character.level; i++)
                {
                    character.special += random.Next(0, i);
                }

            } else if (character.name == "Kurage")
            {
                for (int i = 0; i < character.level + 3; i++)
                {
                    character.special += random.Next(0, i);
                }

            } else if (character.name == "Lazare")
            {
                for (int i = 0; i < character.level + 1; i++)
                {
                    character.special += random.Next(0, i);
                }
            }
        }

        public static Character InitializeKrieden()
        {
            Character krieden = new Character();
            krieden.name = "Krieden";
            krieden.health = 150;
            krieden.level = 1;
            krieden.special = 0;


            Gear.Head head = new Gear.Head();
            head.who = "Krieden";
            head.name = "";
            head.defense = 0;
            head.info = "";

            Gear.Body body = new Gear.Body();
            body.who = "Krieden";
            body.name = "";
            body.defense = 0;
            body.info = "";

            Gear.Accessory accessory = new Gear.Accessory();
            accessory.who = "Krieden";
            accessory.name = "";
            accessory.defense = 0;
            accessory.info = "";

            Gear.Weapon weapon = new Gear.Weapon();
            weapon.who = "Krieden";
            weapon.name = "";
            weapon.damage = 0;
            weapon.info = "";

            krieden.head = head;
            krieden.body = body;
            krieden.weapon = weapon;
            krieden.accessory = accessory;

            return krieden;
        }

        public static Character InitializeMonomo()
        {
            Character monomo = new Character();
            monomo.name = "Monomo";
            monomo.health = 100;
            monomo.level = 1;
            monomo.special = 0;

            Gear.Head head = new Gear.Head();
            head.who = "Monomo";
            head.name = "";
            head.defense = 0;
            head.info = "";

            Gear.Body body = new Gear.Body();
            body.who = "Monomo";
            body.name = "";
            body.defense = 0;
            body.info = "";

            Gear.Accessory accessory = new Gear.Accessory();
            accessory.who = "Monomo";
            accessory.name = "";
            accessory.defense = 0;
            accessory.info = "";

            Gear.Weapon weapon = new Gear.Weapon();
            weapon.who = "Monomo";
            weapon.name = "";
            weapon.damage = 0;
            weapon.info = "";

            monomo.head = head;
            monomo.body = body;
            monomo.weapon = weapon;
            monomo.accessory = accessory;

            return monomo;
        }

        public static Character InitializeKurage()
        {
            Character kurage = new Character();
            kurage.name = "Kurage";
            kurage.health = 80;
            kurage.level = 1;
            kurage.special = 0;

            Gear.Head head = new Gear.Head();
            head.who = "Kurage";
            head.name = "";
            head.defense = 0;
            head.info = "";

            Gear.Body body = new Gear.Body();
            body.who = "Kurage";
            body.name = "";
            body.defense = 0;
            body.info = "";

            Gear.Accessory accessory = new Gear.Accessory();
            accessory.who = "Kurage";
            accessory.name = "";
            accessory.defense = 2;
            accessory.info = "";

            Gear.Weapon weapon = new Gear.Weapon();
            weapon.who = "Kurage";
            weapon.name = "";
            weapon.damage = 0;
            weapon.info = "";

            kurage.head = head;
            kurage.body = body;
            kurage.weapon = weapon;
            kurage.accessory = accessory;

            return kurage;
        }

        public static Character InitializeLazare()
        {
            Character lazare = new Character();
            lazare.name = "Lazare";
            lazare.health = 120;
            lazare.level = 1;
            lazare.special = 0;

            Gear.Head head = new Gear.Head();
            head.who = "Lazare";
            head.name = "";
            head.defense = 0;
            head.info = "";

            Gear.Body body = new Gear.Body();
            body.who = "Lazare";
            body.name = "";
            body.defense = 0;
            body.info = "";

            Gear.Accessory accessory = new Gear.Accessory();
            accessory.who = "Lazare";
            accessory.name = "";
            accessory.defense = 0;
            accessory.info = "";

            Gear.Weapon weapon = new Gear.Weapon();
            weapon.who = "Lazare";
            weapon.name = "";
            weapon.damage = 0;
            weapon.info = "";

            lazare.head = head;
            lazare.body = body;
            lazare.weapon = weapon;
            lazare.accessory = accessory;

            return lazare;
        }

        public static Party AddToParty(Character one, Character two, Character three, Character four)
        {
            Party party = new Party();
            party.member1 = one;
            party.member2 = two;
            party.member3 = three;
            party.member4 = four;

            return party;
        }
    }
}
