using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Loot
{

    public float weight;
    public float value;

    public enum ItemSlot { rightHand, leftHand, chest, head, arm, leg}
    public ItemSlot slot;

    public static Loot GetRandLoot(float value)
    {
        float r = Random.value;
        if (r < 0.1f) return Melee.GetRand(value);
        if (r < 0.3f) return Ranged.GetRand(value);
        if (r < 0.4f) return Shield.GetRand(value);
        if (r < 1.0f) return Armor.GetRand(value);
        return null;
    }

    public Loot(ItemSlot slot, float weight, float value)
    {
        this.slot = slot; this.weight = weight; this.value = value;
    }

    internal abstract string GetStats();

    public class Melee : Loot
    {
        public int damage;
        public float range;
        public bool twoHanded;

        public Melee(int damage, float range, float weight, float value, bool twoHanded = false) : base(ItemSlot.rightHand, weight, value)
        {
            this.damage = damage; this.range = range; this.twoHanded = twoHanded;
        }

        internal static Melee GetRand(float value)
        {
            int d = Random.Range(1, (int)(value / 6) + 1);
            float r = Random.Range(1, (int)((value - (d - 1) * 6) / 6 + 1));
            float w = Mathf.Max(((d - 1) * 6 + (r - 1) * 6) * 2 - value, 1);
            if (Random.value > 0.7)
                return new Melee(d, r, w, value * 2 / 3, true);
            else
                return new Melee(d, r, w, value, false);
        }

        public override string ToString()
        {
            return "Sword";
        }

        internal override string GetStats()
        {
            return "Damage: " + damage + "\nRange: " + range + (twoHanded ? "\nTwo handed" : "") + "\nWeight: " + weight + "\nValue: " + value;
        }
    }

    public class Ranged : Loot
    {
        public int damage;
        public float range;
        public bool twoHanded;

        public Ranged(int damage, float range, float weight, float value, bool twoHanded = true) : base(ItemSlot.rightHand, weight, value)
        {
            this.damage = damage; this.range = range; this.twoHanded = twoHanded;
        }

        internal static Loot GetRand(float value)
        {
            int d = Random.Range(1, (int)(value / 6) + 1);
            float r = Random.Range(2, (int)((value - (d - 1) * 6) / 3 + 2));
            float w = Mathf.Max(((d - 1) * 10 + (r - 2) * 3) * 2 - value, 1);
            return new Ranged(d, r, w, value, true);
        }

        public override string ToString()
        {
            return "Bow";
        }

        internal override string GetStats()
        {
            return "Damage: " + damage + "\nRange: " + range + (twoHanded ? "\nTwo handed" : "") + "\nWeight: " + weight + "\nValue: " + value;
        }
    }

    public class Shield : Loot
    {
        public float blockChancePassive;
        public float blockChanceActive;

        public Shield(float blockChancePassive, float blockChanceActive, float weight, float value) : base(ItemSlot.leftHand, weight, value)
        {
            this.blockChancePassive = blockChancePassive; this.blockChanceActive = blockChanceActive;
        }
        
        internal static Loot GetRand(float value)
        {
            float bcp = Random.Range(1, (value / 5) + 1) / 50;
            float bca = Random.Range(1, (value - (bcp - .02f) * 250) / 5 + 1) / 5;
            float w = Mathf.Max(((bcp - .02f) * 250 + (bca - .2f) * 25) * 2 - value, 1) * 2;
            return new Shield(bcp, bca, w, value);
        }

        public override string ToString()
        {
            return "Shield";
        }

        internal override string GetStats()
        {
            return "Passive block chance: " + blockChancePassive.ToString("#0%") + "\nActive block chance: " + blockChanceActive.ToString("#0%") + "\nWeight: " + weight + "\nValue: " + value;
        }
    }

    public class Armor : Loot
    {
        public float blockChance;

        public Armor(ItemSlot slot, float blockChance, float weight, float value) : base(slot, weight, value)
        {
            this.blockChance = blockChance;
        }

        internal static Loot GetRand(float value)
        {
            float bc = Random.Range(1, (value / 10) + 1) / 50;
            float w = Mathf.Max(value - (bc * 1000) * 2, 1);
            float v = Random.value;
            if (v < .2f) return new Armor(ItemSlot.head, bc, w, value);
            if (v < .4f) return new Armor(ItemSlot.chest, bc, w, value);
            if (v < .7f) return new Armor(ItemSlot.arm, bc, w, value);
            if (v < 1f) return new Armor(ItemSlot.leg, bc, w, value);
            throw new System.Exception("Some random did something strange. Value = " + v);
        }

        public override string ToString()
        {
            return "Armor";
        }

        internal override string GetStats()
        {
            return "Block chance: " + blockChance.ToString("#0%") + "\nSlot: " + slot + "\nWeight: " + weight + "\nValue: " + value;
        }
    }

}
