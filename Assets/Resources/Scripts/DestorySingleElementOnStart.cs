using UnityEngine;
using CodeMonkey;

namespace Inventory
{
    using CodeMonkey.Match3Game; 

    #region PowerUps
    public class DestorySingleElementOnStart : PowerUp
    {
        [SerializeField]
        protected GemSO Gem;

        public DestorySingleElementOnStart(GemSO Gem, string name = " Clear", string description = "Clears all of a single Element from the board when the game loads") : base(name, description)
        {
            this.Gem = Gem;
        }

        public override void OnMatchStart(Match3 Game)
        {
            for (int i = 0; i < Game.Grid.GetWidth(); i++)
            {
                for (int j = 0; j < Game.Grid.GetHeight(); j++)
                {
                    Match3.GemGridPosition gemGridPosition = Game.Grid.GetGridObject(i, j);

                    if (Gem == gemGridPosition.GetGemGrid().GetGem())
                    {
                        Game.TryDestroyGemGridPosition(gemGridPosition);
                        GameObject.FindAnyObjectByType<Match3Visual>().OnDestruction();
                    }
                }
            }
        }
    }
    #endregion
}

namespace Inventory
{
    public class SecondaryMatchBonus : PowerUp
    {
        public Element bonusOne, bonusTwo;
        public float BonusAmount; 
        public SecondaryMatchBonus(Element primaryBonus, Element secondBonus, string Name, string Description) : base(Name, Description)
        {
            bonusOne = primaryBonus; bonusTwo = secondBonus; 
        }

        public override void OnMatch(Element Gem_1)
        {
            if(Gem_1 == bonusOne || Gem_1 == bonusTwo)
            {
                GameObject.FindObjectOfType<Match3>().OnGemMatch += ElementalBonus; 
            }
        }

        private void ElementalBonus(Element e, int Score, int Damage)
        {
            Score += Mathf.RoundToInt(100 * BonusAmount);
            Damage += Mathf.RoundToInt(100 * BonusAmount); 
        }

    }

    public class MarieMini : DestorySingleElementOnStart
    {
        public MarieMini(GemSO Gem, string Name ="Mineral Marie", string Description = "Deletes all of random element") : base(Gem, Name, Description)
        {
            base.Gem = SetRandomGem(); 
        }

        private GemSO SetRandomGem()
        {
            var Gems = Resources.LoadAll<GemSO>("Match3/Gems");
            return Gems[Random.Range(0, Gems.Length - 1)]; 
        }
    }

    public class WindFury : PowerUp
    {
        public WindFury(string Name = "Wind Fury", string Description = "") : base(Name, Description)
        {

        }
    }

    public class WindsKindness: PowerUp
    {
        public WindsKindness(string Name = "Wind's Kindness", string Description = "") : base(Name, Description)
        {

        }
    }
}

namespace Inventory
{
    //Image 181
    public class WardingBastion : Equipment
    {
        public WardingBastion(string name = "Warding Bastion", int cost = 100) : base(name, cost)
        {
            AssignSprite(181);
        }
    }
    //Image 41
    public class PhoenixAshCHarm : Equipment
    {
        public PhoenixAshCHarm(string name = "Pheonix Ash Charm", int cost  = 100) : base(name, cost)
        {
            AssignSprite(41);
        }
    }
    //Image107
    public class FrostFire : Equipment
    {
        public FrostFire(string name = "Frostfire", int cost = 200) : base(name, cost)
        {
            AssignSprite(107);
        }
    }
    //image204
    public class ThunderClap : Equipment
    {
        public ThunderClap(string name ="Thunderclap", int cost = 250) : base(name, cost)
        {
            AssignSprite(204);
        }
    }
    //image82
    public class SirensSongAmulet : Equipment
    {
        public SirensSongAmulet(string name= "Siren's Song Amulet", int cost = 500) : base(name, cost)
        {
            AssignSprite(82);
        }
    }
    //iamge114
    public class IllusionistsPrism : Equipment
    {
        public IllusionistsPrism(string name ="Illusionist's Prism", int cost = 500) : base(name, cost)
        {
            AssignSprite(114);
        }
    }
    //iamge 97
    public class KrakensTealGemstone : Equipment
    {
        public KrakensTealGemstone(string name="Kraken's Teal Gemstone", int cost = 500) : base(name, cost)
        {
            AssignSprite(97);
        }
    }
    //image11
    public class ShikrasTeeth : Equipment
    {
        public ShikrasTeeth(string name="Shikra's Teeth", int cost=600) : base(name, cost)
        {
            AssignSprite(11);
        }
    }
    //image151
    public class CrownOfTakiTee : Equipment
    {
        public CrownOfTakiTee(string name="Crown of Taki Tee", int cost=600) : base(name, cost)
        {
            AssignSprite(151);
        }
    }
    //image42
    public class Wyrmguard : Equipment
    {
        public Wyrmguard(string name="Wyrmguard", int cost=600) : base(name, cost)
        {
            AssignSprite(42);
        }
    }
    //image175
    public class Valor : Equipment
    {
        public Valor(string name="Valor", int cost=1000) : base(name, cost)
        {
            AssignSprite(175);
        }
    }
    //image04
    public class InfernoBlaze : Equipment
    {
        public InfernoBlaze( string name = "Inferno Blaze", int cost = 1000) : base(name, cost)
        {
            AssignSprite("04");
        }

        public override PowerUp GetPower()
        {
            return new SecondaryMatchBonus(Element.Fire, Element.Physical, "All Fire", "Fire does a bonus thing"); 
        }
    }
    //image04_1
    public class FrostBiteShroud : Equipment
    {
        public FrostBiteShroud( string name="Frostbite Shroud", int cost=1000) : base(name, cost)
        {
            AssignSprite("04_1");
        }

        public override PowerUp GetPower()
        {
            return new SecondaryMatchBonus(Element.Water, Element.Poison, "All Ice", "Ice does a bonus Thing");
        }
    }
    //image04_2
    public class ThunderstormSurge : Equipment
    {
        public ThunderstormSurge( string name = "THunderstorm Surge", int cost = 1000) : base(name, cost)
        {
            AssignSprite("04_2");
        }

        public override PowerUp GetPower()
        {
            return new SecondaryMatchBonus(Element.Thunder, Element.Thunder, "All Thunder", "Electric does a bonus thing");
        }
    }
    //iamge05
    public class VerdantEmbrace : Equipment
    {
        public VerdantEmbrace( string name = "Verdant Embrace", int cost = 1000) : base(name, cost)
        {
            AssignSprite("05");
        }

        public override PowerUp GetPower()
        {
            return new SecondaryMatchBonus(Element.Nature, Element.Nature, "All Nature", "Nature does a bonus thing");
        }
    }
    //image178
    public class DivineRadiance : Equipment
    {
        public DivineRadiance(string name="Divine Radiance", int cost=1500) : base(name, cost)
        {
            AssignSprite(178);
        }

        public override PowerUp GetPower()
        {
            return new DestorySingleElementOnStart(null);
        }
    }
    //image01_3
    public class ShadowRequiem : Equipment
    {
        public ShadowRequiem(string name="Shadow Requiem", int cost=1500) : base(name, cost)
        {
            AssignSprite("01_3");
        }

        public override PowerUp GetPower()
        {
            return new DestorySingleElementOnStart(null);
        }
    }
    //iamge50
    public class TempestFury : Equipment
    {
        public TempestFury(string name="Tempest Fury", int cost=1500) : base(name, cost)
        {
            AssignSprite(50);
        }

        public override PowerUp GetPower()
        {
            return new WindFury();
        }
    }
    //iamge96_1
    public class ZephyrGust : Equipment
    {
        public ZephyrGust(string name="Zephyr Gust", int cost=1500) : base(name, cost)
        {
            AssignSprite("96_1");
        }

        public override PowerUp GetPower()
        {
            return new WindsKindness();
        }
    }
    //image98
    public class AbyssalShardfall : Equipment
    {
        public AbyssalShardfall(string name="Abyssal Shardfall", int cost=1500) : base(name, cost)
        {
            AssignSprite(98);
        }

        public override PowerUp GetPower()
        {
            return new MarieMini(null);
        }
    }

}