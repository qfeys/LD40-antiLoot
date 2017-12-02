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
        return Melee.GetRand(value);
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
            int d = Random.Range(1, (int)(value/10) + 1);
            float r = Random.Range(1, (value - d * 10)/10 + 1);
            float w = Mathf.Max(value - (d * 10 + r * 10) * 2, 1);
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

        public Ranged(int damage, float range, float weight, bool twoHanded = true) : base(ItemSlot.rightHand, weight, 1)
        {
            this.damage = damage; this.range = range; this.twoHanded = twoHanded;
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

        internal override string GetStats()
        {
            return "Passive block chance: " + blockChancePassive + "\nActive block chance: " + blockChanceActive + "\nWeight: " + weight + "\nValue: " + value;
        }
    }

    public abstract class Armor : Loot
    {
        public float blockChance;

        public Armor(ItemSlot slot, float blockChance, float weight, float value) : base(slot, weight, value)
        {
            this.blockChance = blockChance;
        }

        internal override string GetStats()
        {
            return "Block chance: " + blockChance + "\nSlot: " + slot + "\nWeight: " + weight + "\nValue: " + value;
        }
    }

}
