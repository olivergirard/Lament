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
