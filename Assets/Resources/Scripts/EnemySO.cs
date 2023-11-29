using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Enemy", menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public int Health, Damage;
    public Sprite EnemySprite; 
    public enum DamageSpeed { slow, medium, fast}
    public int speed;
    public DamageSpeed GetSpeedRanking()
    {
        if(speed >= 15)
            return DamageSpeed.slow;
        else if (speed < 15 && speed > 5)
            return DamageSpeed.medium;
        else
            return DamageSpeed.fast;
    } 

    
}
