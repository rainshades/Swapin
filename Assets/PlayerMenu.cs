using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class PlayerMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Health, Coins, TotalNodesCompleted, CurrentRunStreak, LongestRunStreak; 
    // Start is called before the first frame update
    void Start()
    {
        Player p = PlayerObject.singleton.Player; 

        Health.text = $"HP : \n {p.CurrentHealth}/{p.MaxHealth}";
        Coins.text = $"Coins : \n {p.Coins}";
        TotalNodesCompleted.text = $"Levels Completed : \n {p.TotalNodesCompleted}";
        CurrentRunStreak.text = $"Current Run : \n {p.RunStreak}";
        LongestRunStreak.text = $"Longest Run : \n {p.LongestRunStreak}";
    }
}
