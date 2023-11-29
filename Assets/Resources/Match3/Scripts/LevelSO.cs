using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class LevelSO : ScriptableObject {

    public enum GoalType {
        Glass,
        Score,
        DefeatEnemies, //Needs to Be added 
        TimeTrial 
    }

    public List<GemSO> gemList;
    public int width;
    public int height;
    public List<LevelGridPosition> levelGridPositionList;
    public GoalType goalType;
    public int moveAmount;
    public int targetScore;
    public List<EnemySO> enemyList;
    public int NumberOfEnemeis;
    public GameObject TargetsPrefab; 
    public float MaxTime; 

    [System.Serializable]
    public class LevelGridPosition {

        public GemSO gemSO;
        public int x;
        public int y;
        public bool hasGlass;

    }

    public void ResetScore()
    {
        targetScore = 1000;
    }

    public void Scale(int Runs, int RunPosition)
    {

        switch (goalType)
        {
            case GoalType.Score:
                targetScore += targetScore *  Mathf.RoundToInt((RunPosition + Runs * 4));
                moveAmount += Mathf.FloorToInt((MaxTime + Runs) * 0.5f);
                break; //Minimum Score Increase and more moves

            case GoalType.TimeTrial:
                targetScore += targetScore *  Mathf.RoundToInt(Runs + RunPosition * 4);
                break; //Minimum Score Increase 

            case GoalType.DefeatEnemies:
                EnemySO[] enemies = Resources.LoadAll<EnemySO>("ScriptableObjects/Enemies");
                enemyList = new List<EnemySO>(); 
                for(int i = 0; i <= RunPosition + Runs; i++)
                {
                    enemyList.Add(enemies[Random.Range(0, enemies.Length - 1)]); 
                }
                break; //Increase in the number of enemies, their health, and their type

            case GoalType.Glass:
                for(int i = 0; i <= (RunPosition - Runs) * 2; i++)
                {
                    levelGridPositionList[Random.Range(0, levelGridPositionList.Count - 1)].hasGlass = true; 
                }
                break; //Increase in the number of glass and the size of the map, but also a gradual increase in the amount of moves you get
        }

    }

}
