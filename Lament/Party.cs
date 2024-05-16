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

            if (character.name == "Pierre")
            {
                for (int i = 0; i < character.level + 2; i++)
                {
                    character.special += random.Next(0, i);
                }

            } else if (character.name == "Morris")
            {
                for (int i = 0; i < character.level; i++)
                {
                    character.special += random.Next(0, i);
                }

            } else if (character.name == "Ruby")
            {
                for (int i = 0; i < character.level + 3; i++)
                {
                    character.special += random.Next(0, i);
                }

            } else if (character.name == "Yoko")
            {
                for (int i = 0; i < character.level + 1; i++)
                {
                    character.special += random.Next(0, i);
                }
            }
        }

        public static Character InitializePierre()
        {
            Character pierre = new Character();
            pierre.name = "Pierre";
            pierre.health = 150;
            pierre.level = 1;
            pierre.special = 0;


            Gear.Head head = new Gear.Head();
            head.who = "Pierre";
            head.name = "";
            head.defense = 0;
            head.info = "";

            Gear.Body body = new Gear.Body();
            body.who = "Pierre";
            body.name = "";
            body.defense = 0;
            body.info = "";

            Gear.Accessory accessory = new Gear.Accessory();
            accessory.who = "Pierre";
            accessory.name = "";
            accessory.defense = 0;
            accessory.info = "";

            Gear.Weapon weapon = new Gear.Weapon();
            weapon.who = "Pierre";
            weapon.name = "";
            weapon.damage = 0;
            weapon.info = "";

            pierre.head = head;
            pierre.body = body;
            pierre.weapon = weapon;
            pierre.accessory = accessory;

            return pierre;
        }

        public static Character InitializeMorris()
        {
            Character morris = new Character();
            morris.name = "Morris";
            morris.health = 100;
            morris.level = 1;
            morris.special = 0;

            Gear.Head head = new Gear.Head();
            head.who = "Morris";
            head.name = "";
            head.defense = 0;
            head.info = "";

            Gear.Body body = new Gear.Body();
            body.who = "Morris";
            body.name = "";
            body.defense = 0;
            body.info = "";

            Gear.Accessory accessory = new Gear.Accessory();
            accessory.who = "Morris";
            accessory.name = "";
            accessory.defense = 0;
            accessory.info = "";

            Gear.Weapon weapon = new Gear.Weapon();
            weapon.who = "Morris";
            weapon.name = "";
            weapon.damage = 0;
            weapon.info = "";

            morris.head = head;
            morris.body = body;
            morris.weapon = weapon;
            morris.accessory = accessory;

            return morris;
        }

        public static Character InitializeRuby()
        {
            Character ruby = new Character();
            ruby.name = "Ruby";
            ruby.health = 80;
            ruby.level = 1;
            ruby.special = 0;

            Gear.Head head = new Gear.Head();
            head.who = "Ruby";
            head.name = "";
            head.defense = 0;
            head.info = "";

            Gear.Body body = new Gear.Body();
            body.who = "Ruby";
            body.name = "";
            body.defense = 0;
            body.info = "";

            Gear.Accessory accessory = new Gear.Accessory();
            accessory.who = "Ruby";
            accessory.name = "Grandma's Locket";
            accessory.defense = 2;
            accessory.info = "This locket may have no photo inside of it, but it holds lots of memories.";

            Gear.Weapon weapon = new Gear.Weapon();
            weapon.who = "Ruby";
            weapon.name = "";
            weapon.damage = 0;
            weapon.info = "";

            ruby.head = head;
            ruby.body = body;
            ruby.weapon = weapon;
            ruby.accessory = accessory;

            return ruby;
        }

        public static Character InitializeYoko()
        {
            Character yoko = new Character();
            yoko.name = "Yoko";
            yoko.health = 120;
            yoko.level = 1;
            yoko.special = 0;

            Gear.Head head = new Gear.Head();
            head.who = "Yoko";
            head.name = "";
            head.defense = 0;
            head.info = "";

            Gear.Body body = new Gear.Body();
            body.who = "Yoko";
            body.name = "";
            body.defense = 0;
            body.info = "";

            Gear.Accessory accessory = new Gear.Accessory();
            accessory.who = "Yoko";
            accessory.name = "";
            accessory.defense = 0;
            accessory.info = "";

            Gear.Weapon weapon = new Gear.Weapon();
            weapon.who = "Yoko";
            weapon.name = "";
            weapon.damage = 0;
            weapon.info = "";

            yoko.head = head;
            yoko.body = body;
            yoko.weapon = weapon;
            yoko.accessory = accessory;

            return yoko;
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
