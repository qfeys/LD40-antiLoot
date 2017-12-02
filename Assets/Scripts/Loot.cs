using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class Loot
{

    public float weight;
    public float value;

    public enum ItemSlot { rightHand, leftHand, chest, head, arm, leg}
    public ItemSlot slot;

    public Loot(ItemSlot slot, float weight, float value)
    {
        this.slot = slot; this.weight = weight; this.value = value;
    }

    public class Melee : Loot
    {
        public int damage;
        public float range;
        public bool twoHanded;

        public Melee(int damage, float range, float weight, bool twoHanded = false) : base(ItemSlot.rightHand, weight, 1)
        {
            this.damage = damage; this.range = range; this.twoHanded = twoHanded;
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
    }

    public class Shield : Loot
    {
        public float blockChancePassive;
        public float blockChanceActive;

        public Shield(float blockChancePassive, float blockChanceActive, float weight, float value) : base(ItemSlot.leftHand, weight, value)
        {
            this.blockChancePassive = blockChancePassive; this.blockChanceActive = blockChanceActive;
        }
    }

    public abstract class Armor : Loot
    {
        public float blockChance;

        public Armor(ItemSlot slot, float blockChance, float weight, float value) : base(slot, weight, value)
        {
            this.blockChance = blockChance;
        }
    }

}
