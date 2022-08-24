using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            public string head;
            public string body;
            public string weapon;
            public string accessory;
        }

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

        public Character InitializePierre()
        {
            Character pierre = new Character();
            pierre.name = "Pierre";
            pierre.health = 150;
            pierre.level = 1;
            pierre.special = 0;
            pierre.head = "none";
            pierre.body = "none";
            pierre.weapon = "none";
            pierre.accessory = "none";

            return pierre;
        }

        public Character InitializeMorris()
        {
            Character morris = new Character();
            morris.name = "Morris";
            morris.health = 100;
            morris.level = 1;
            morris.special = 0;
            morris.head = "none";
            morris.body = "none";
            morris.weapon = "none";
            morris.accessory = "none";

            return morris;
        }

        public Character InitializeRuby()
        {
            Character ruby = new Character();
            ruby.name = "Ruby";
            ruby.health = 80;
            ruby.level = 1;
            ruby.special = 0;
            ruby.head = "none";
            ruby.body = "none";
            ruby.weapon = "none";
            ruby.accessory = "Locket";

            return ruby;
        }

        public Character InitializeYoko()
        {
            Character yoko = new Character();
            yoko.name = "Yoko";
            yoko.health = 120;
            yoko.level = 1;
            yoko.special = 0;
            yoko.head = "none";
            yoko.body = "none";
            yoko.weapon = "none";
            yoko.accessory = "none";

            return yoko;
        }

    }
}
