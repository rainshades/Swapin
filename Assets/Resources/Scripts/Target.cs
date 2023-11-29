using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
using CodeMonkey; 

public class Target : MonoBehaviour
{
    public Element DefendingElement;

    public EnemySO Enemy;
    public Slider HealthSlider;
    public Image ElementRender;
    public Sprite[] ElementSprites; 

    [SerializeField]
    private int currentHealth;
    private int MaxHealth; 
    public Match3 Game;
    public Match3UI UI; 


    public bool Alive => currentHealth > 0;
    public int CurrentHealth 
    {
        get => currentHealth;  
        set
        {
            currentHealth = value;
            if (currentHealth <= 0)
            {
                Death(); 
            }
        }
    }
    
    private int CurrentDamage;
    private float AttackTimer, AttackSpeed;

    private void Awake()
    {
        UI = FindObjectOfType<Match3UI>();
        HealthSlider = UI._enemySlider.GetComponent<Slider>();
        ElementRender = UI._enemyElement.GetComponent<Image>(); 

        ElementRender.sprite = ElementSprites[(int)DefendingElement];
        Game = FindObjectOfType<Match3>(); 

        if (Game.GetEnemy(this) == null)
            Game.AddNewEnemy(this);

        if (Game.CurrentSelectedEnemy == null)
        {
            Game.CurrentSelectedEnemy = this;
            Create(); 
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        if (!Alive)
            yield return null;

        AttackTimer--;

        if (AttackTimer <= 0)
        {
            AttackTimer = AttackSpeed;
            Attack();
        }

        StartCoroutine(Start()); 
    }

    private void Attack()
    {
        PlayerObject.singleton.Player.TakeDamage(CurrentDamage);
        GameObject.Find("Player Damage Text").GetComponent<DamageText>().SpawnNewText(CurrentDamage); 
    }

    public void SetSliderHealth()
    { 
        HealthSlider.maxValue = MaxHealth;
        HealthSlider.value = currentHealth;
    }

    public void Create()
    {
        GetComponent<SpriteRenderer>().sprite = Enemy.EnemySprite;

        AttackTimer = AttackSpeed;
        MaxHealth = currentHealth = Enemy.Health;

        currentHealth = Enemy.Health;
        CurrentDamage = Enemy.Damage; 

        var RandomElementNumber = Random.Range(0, 4);

        switch (RandomElementNumber)
        {
            case 0:
                DefendingElement = Element.Water;
                break;
            case 1:
                DefendingElement = Element.Fire;
                break;
            case 2:
                DefendingElement = Element.Nature;
                break;
            case 3:
                DefendingElement = Element.Poison; 
                break;
            case 4:
                DefendingElement = Element.Physical; 
                break; 
        }

        AttackSpeed = Enemy.speed; 
    }

    private void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        HealthSlider.value = CurrentHealth; 
    }

    public int DamageCalc(int damage, Element AttackingElement, float DamageModifier)
    {
        switch (AttackingElement)
        {
            case Element.Water:
                if (DefendingElement == Element.Fire)
                    damage *= 2;
                if (DefendingElement == Element.Water || DefendingElement == Element.Nature)
                    damage /= 2; 
                break;
            case Element.Fire:
                if (DefendingElement == Element.Nature)
                    damage *= 2;
                if (DefendingElement == Element.Water || DefendingElement == Element.Fire)
                    damage /= 2;
                break;
            case Element.Nature:
                if (DefendingElement == Element.Water)
                    damage *= 2;
                if (DefendingElement == Element.Fire || DefendingElement == Element.Nature)
                    damage /= 2;
                break;
            case Element.Poison:
                if (DefendingElement == Element.Physical)
                    damage *= 2;
                if (DefendingElement == Element.Poison)
                    damage /= 2; 
                break;
            case Element.Physical:
                if (DefendingElement == Element.Poison)
                    damage *= 2;
                if (DefendingElement == Element.Physical)
                    damage /= 2;
                break;
        }

        if (PlayerObject.singleton.Player.HasBonusElement(AttackingElement))
        {
            Mathf.RoundToInt(damage * DamageModifier);
        }


        TakeDamage(damage);

        return damage; 
    }

    private void Death()
    {
        Debug.Log("Dead");

        int CurrentEnemyIndex = Game.Enemies.IndexOf(this);
        if(CurrentEnemyIndex > 0)
        {
            CurrentEnemyIndex--; 
        }
        
        Game.Enemies.Remove(this);
        if (Game.Enemies.Count > 0)
            Game.SetNewTarget(Game.Enemies[CurrentEnemyIndex]);

        try
        {         
            Destroy(gameObject);
        }
        catch { }
    }
}
