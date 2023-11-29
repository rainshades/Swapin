using UnityEngine;
using CodeMonkey;
using System;


namespace Inventory
{
    [Serializable]
    public class Equipment
    {
        protected Match3 Game;
        public string Name, Description;
        public int CoinCost;
        public Sprite EquipmentSprite;
        public float DamageBonus;
        public float ScoreBonus;
        public bool Unlocked;
        public bool Equipped;
        public int HealthBonus;

        public Equipment(string name, int cost)
        {
            Name = name; CoinCost = cost;  
        }

        public void AssignSprite(int itemCode)
        {
            EquipmentSprite = Resources.Load<Sprite>($"Pics/200plusfantasyrpgicons/PNG/{itemCode}");
        }

        public void AssignSprite(string itemCode)
        {
            EquipmentSprite = Resources.Load<Sprite>($"Pics/200plusfantasyrpgicons/PNG/{itemCode}");
        }

        public virtual PowerUp GetPower() { return null; }
    }

    [Serializable]
    public class PowerUp
    {
        [SerializeField]
        protected string Name, Description;

        public PowerUp(string Name, string Description)
        {
            this.Name = Name; this.Description = Description;
        }

        public virtual void OnMatch(Element Gem_1)
        {

        }

        public virtual void OnMatchStart(Match3 Game)
        {

        }
    }
}