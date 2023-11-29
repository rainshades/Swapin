using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;


[CreateAssetMenu(fileName = "Player", menuName = "Player [only make one]")]
public class Player : ScriptableObject
{
    [SerializeField] private int currentHealth, maxHealth;
    public int Coins; 
    public int TotalNodesCompleted;
    public int RunStreak, LongestRunStreak;
    public MapData SavedData;
    public int NodesUntilBoss => Mathf.RoundToInt(1.5f * (RunStreak +
        (RunStreak % 7)) + 5);
    [SerializeReference]
    public List<Equipment> Inventory;
    public Equipment Slot_1 { 
        get 
        {
            if(slot_index_1 >= 0)
            return Inventory[slot_index_1];

            return null;
        }
    }
    public Equipment Slot_2
    {
        get
        {
            if (slot_index_2 >= 0)
                return Inventory[slot_index_2];

            return null;
        }
    }

    int slot_index_1 = -1, slot_index_2 = -1; 

    public float DamageMultiplier
    {
        get
        {
            
            float Bonus = 0;
            if (slot_index_1 > 0)
            {
                Bonus = Inventory[slot_index_1] != null ? Bonus + Inventory[slot_index_1].DamageBonus : Bonus;
                Bonus = Inventory[slot_index_2] != null ? Bonus + Inventory[slot_index_2].DamageBonus : Bonus;
            }
            return Bonus; 
        }
    }
    public float ScoreMultiplier
    {
        get
        {
            float Bonus = 0;

            if (slot_index_2 > 0)
            {
                Bonus = Inventory[slot_index_1] != null ? Bonus + Inventory[slot_index_1].DamageBonus : Bonus;
                Bonus = Inventory[slot_index_2] != null ? Bonus + Inventory[slot_index_2].DamageBonus : Bonus;
            }
            return Bonus;

        }
    }

    public void CompleteRun()
    {
        Coins += 10;
        RunStreak++;
        SetRunStreak();
        SavedData.Reset(); 
    }

    public void SetRunStreak()
    {
        if (RunStreak > LongestRunStreak)
            LongestRunStreak = RunStreak; 
    }

    public void CreateEquipment()
    {
        if (Inventory.Count == 0)
        {
            Inventory.Add(new WardingBastion());
            Inventory.Add(new PhoenixAshCHarm());
            Inventory.Add(new FrostFire());
            Inventory.Add(new ThunderClap());
            Inventory.Add(new SirensSongAmulet());
            Inventory.Add(new IllusionistsPrism());
            Inventory.Add(new KrakensTealGemstone());
            Inventory.Add(new ShikrasTeeth());
            Inventory.Add(new CrownOfTakiTee());
            Inventory.Add(new Wyrmguard());
            Inventory.Add(new Valor());
            Inventory.Add(new InfernoBlaze());
            Inventory.Add(new FrostBiteShroud());
            Inventory.Add(new ThunderstormSurge());
            Inventory.Add(new VerdantEmbrace());
            Inventory.Add(new DivineRadiance());
            Inventory.Add(new ShadowRequiem());
            Inventory.Add(new TempestFury());
            Inventory.Add(new ZephyrGust());
            Inventory.Add(new AbyssalShardfall());
        }
    }

    public void Buy(Equipment equipment)
    {
        int index = Inventory.IndexOf(equipment); 
        if(Coins >= Inventory[index].CoinCost)
        {
            equipment.Unlocked = true; 
        }
    }

    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            CodeMonkey.Match3UI.UpdatePlayerHealthSlider(MaxHealth, currentHealth);
        }
    }

    public int MaxHealth
    {
        get => maxHealth;
        set
        {
            currentHealth = maxHealth = value;
        }
    }

    public int CachedNode1, CachedNode2;
    //equipment, score, lives, run bonuses

    public void Reset()
    {
        CurrentHealth = MaxHealth;
        RunStreak = 0;
        SavedData.route.Clear(); 
    }

    public int TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        return damage; 
    }

    public void AssignEquipment(Equipment equipment)
    {
        if (slot_index_1 == -1)
            AssignEquipmentToSlot_1(equipment);
        else if (slot_index_2 == -1)
            AssignEquipmentToSlot_2(equipment);

    }

    public void AssignEquipmentToSlot_1(Equipment equipment)
    {
        int indexCheck = Inventory.IndexOf(equipment);

        if (slot_index_2 != indexCheck)
        {
            slot_index_1 = indexCheck; 
            Inventory[slot_index_1].Equipped = true;
        }
    }

    public void AssignEquipmentToSlot_2(Equipment equipment)
    {
        int indexCheck = Inventory.IndexOf(equipment);

        if (slot_index_1 != indexCheck)
        {
            slot_index_2 = indexCheck; 
            Inventory[slot_index_2].Equipped = true;
        }
    }

    public void RemoveEquipment(bool Slot_1)
    {
        if (Slot_1)
        {
            RemoveEquipmentFromSlot_1();
            return;
        }
        RemoveEquipmentFromSlot_2();
    }

    public void RemoveEquipmentFromSlot_1()
    {
        Inventory[slot_index_1].Equipped = false;
        slot_index_1 = -1; 
    }

    public void RemoveEquipmentFromSlot_2()
    {
        Inventory[slot_index_2].Equipped = false;
        slot_index_2 = -1; 
    }

    public bool HasBonusElement(Element element)
    {
        //If there isn't anything in slot one don't even bother checking
        if (slot_index_1 < 0)
            return false; 

        if (Inventory[slot_index_1] == null)
            return false;

        return Inventory[slot_index_1].GetPower() is SecondaryMatchBonus || Inventory[slot_index_2]?.GetPower() is SecondaryMatchBonus;
    }
}
