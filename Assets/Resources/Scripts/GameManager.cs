using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CodeMonkey;
using CodeMonkey.Match3Game;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    public LevelManager GameLoader = new LevelManager();
    public AudioManager audioManager;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        var GameManagers = FindObjectsOfType<GameManager>();
        singleton = GameManagers[0]; 
        for (int i = 1; i < GameManagers.Length; i++)
            Destroy(GameManagers[i].gameObject);
    }

    public void SetSong(int index)
    {
        audioManager.SetSong(index); 
    }

    private void OnDestroy()
    {
        GameLoader.UnSubScribe();
        var menu = FindObjectOfType<MainMenu>().gameObject;
        menu.SetActive(false);
        menu.SetActive(true);
    }

    [System.Serializable]
    public class LevelManager
    {
        [SerializeField]
        LevelSO LevelTypeToLoad;

        public enum LoadState {ContinueRun, NewRun_Loss, NewRun_Win, Break}
        public LoadState loadState;

        public LevelManager()
        {
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }
        public void UnSubScribe() => SceneManager.sceneLoaded -= SceneManager_sceneLoaded; 

        private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode sceneLoadMode)
        {

            if (scene.buildIndex == 1)
            {
                LoadGameScene();
                return;
            }//GameplayScene


            LevelTypeToLoad = null;
        }

        public void SetSceneToLoad(LevelSO level)
        {
            LevelTypeToLoad = level;
            SceneManager.LoadScene(1); 
        }
        public LevelSO GetSceneToLoad() => LevelTypeToLoad;
        public void LoadGameScene()
        {
            Match3 match3 = GameObject.FindObjectOfType<Match3>();
        }

        public void BackToMainMenu(LoadState loadState)
        {
            this.loadState = loadState; 
            SceneManager.LoadScene(0);
        }

    }
}