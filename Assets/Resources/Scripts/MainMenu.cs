using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject Map, Inventory, PlayerStats;

    private void Start()
    {
        GameObject.Find("Continue Run").GetComponent<Button>().interactable = PlayerObject.singleton.Player.SavedData.route != null && PlayerObject.singleton.Player.SavedData.CurrentNodeValue > 0;
    }

    private void OnEnable()
    {
        try
        {
            GameManager.singleton.SetSong(0);

            switch (GameManager.singleton.GameLoader.loadState)
            {
                case GameManager.LevelManager.LoadState.Break:
                    break;

                case GameManager.LevelManager.LoadState.ContinueRun:
                    ContinueOldRun();
                    break;

                case GameManager.LevelManager.LoadState.NewRun_Loss:
                    PlayerObject.singleton.Player.Reset();
                    break;

                case GameManager.LevelManager.LoadState.NewRun_Win:
                    PlayerObject.singleton.Player.CompleteRun();
                    break;
            }
        }
        catch
        {

            FindObjectOfType<GameManager>().GameLoader.loadState = GameManager.LevelManager.LoadState.Break;
            FindObjectOfType<GameManager>().SetSong(0); 

            Debug.LogWarning("Main Menu OnEnable Initialized with errors");
        }

        FindObjectOfType<BannerAd>().ShowBannerAd(); 
    }

    public void BeginNewRun()
    {
        Map?.SetActive(true);
        Map?.GetComponent<MapMenu>().AssignNodes(); 
        gameObject.SetActive(false);

    }

    public void ContinueOldRun()
    {
        gameObject.SetActive(false); 
        Map?.SetActive(true);
        Map?.GetComponent<MapMenu>().GetNodes();
        gameObject.SetActive(false);
    }

    public void OpenInventoryScreen()
    {
        gameObject.SetActive(false);
        Inventory?.SetActive(true); 
    }

    public void OpenStatsScreen()
    {
        PlayerStats.gameObject.SetActive(true);
        gameObject.SetActive(false); 
    }

    public void BackToMainMenu(Transform OpenMenu)
    {
        GameManager.singleton.GameLoader.loadState = GameManager.LevelManager.LoadState.Break;
        gameObject.SetActive(true);
        OpenMenu.gameObject.SetActive(false); 
    }

}
