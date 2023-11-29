using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

using TMPro;
namespace CodeMonkey
{
    public class Match3UI : MonoBehaviour
    {

        [SerializeField] private Match3 match3;

        [SerializeField] private TextMeshProUGUI movesText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI targetScoreText;
        [SerializeField] private TextMeshProUGUI glassText;
        [SerializeField] private Transform winLoseTransform;
        [SerializeField] private Transform glassImage; 

        [SerializeField] private Slider PlayerHealthSlider;
        private static Slider s_pHealthSlider;

        private static TextMeshProUGUI timerText;


        [SerializeField] private Transform EnemyCanvas;
        [SerializeField] private Transform EnemyElement;
        [SerializeField] private Transform EnemySlider;

        public Transform _enemyCanvas => EnemyCanvas;
        public Transform _enemyElement => EnemyElement;
        public Transform _enemySlider => EnemySlider;

        private void Awake()
        {
           // winLoseTransform = transform.Find("winLose");
            winLoseTransform.gameObject.SetActive(false);

            match3.OnLevelSet += Match3_OnLevelSet;
            match3.OnMoveUsed += Match3_OnMoveUsed;
            match3.OnGlassDestroyed += Match3_OnGlassDestroyed;
            match3.OnScoreChanged += Match3_OnScoreChanged;

            match3.OnLose += Match3_OnOutOfMoves;
            match3.OnWin += Match3_OnWin;

            s_pHealthSlider = PlayerHealthSlider;
            timerText = movesText;
        }

        private void Start()
        {
            UpdatePlayerHealthSlider();
        }

        private void UpdatePlayerHealthSlider()
        {
            UpdatePlayerHealthSlider(PlayerObject.singleton.Player.MaxHealth, PlayerObject.singleton.Player.CurrentHealth); 
        }

        public static void UpdatePlayerHealthSlider(int MaxHealth, int CurrentHealth)
        {
            s_pHealthSlider.maxValue = MaxHealth;
            s_pHealthSlider.value = CurrentHealth;
        }

        private void Match3_OnWin(object sender, System.EventArgs e)
        {
            winLoseTransform.gameObject.SetActive(true);

            if (!match3.GetLevelSO().name.Contains("Boss"))
            {                
                winLoseTransform.Find("text").GetComponent<TextMeshProUGUI>().text = "<color=#1ACC23>YOU WIN!</color>";
                winLoseTransform.GetChild(3).gameObject.SetActive(true);

                winLoseTransform.GetChild(3).GetChild(0).GetComponent<Button>().onClick.AddListener(() => BackToMenuUI(GameManager.LevelManager.LoadState.ContinueRun));
                winLoseTransform.GetChild(3).GetChild(1).GetComponent<Button>().onClick.AddListener(() => BackToMenuUI(GameManager.LevelManager.LoadState.Break));
                return;
            }

            winLoseTransform.Find("text").GetComponent<TextMeshProUGUI>().text = "<color=#1ACC23>RUN COMPLETE!</color>";
            winLoseTransform.GetChild(2).gameObject.SetActive(true);
            winLoseTransform.GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(() => BackToMenuUI(GameManager.LevelManager.LoadState.NewRun_Win)); 
        }

        private void Match3_OnOutOfMoves(object sender, System.EventArgs e)
        {
            winLoseTransform.gameObject.SetActive(true);
            winLoseTransform.Find("text").GetComponent<TextMeshProUGUI>().text = "<color=#CC411A>YOU LOSE!</color>";
            winLoseTransform.GetChild(4).gameObject.SetActive(true);

            winLoseTransform.GetChild(4).GetChild(0).GetComponent<Button>().onClick.AddListener(() => BackToMenuUI(GameManager.LevelManager.LoadState.NewRun_Loss));
        }

        private void Match3_OnScoreChanged(object sender, System.EventArgs e)
        {
            UpdateText();
        }

        private void Match3_OnGlassDestroyed(object sender, System.EventArgs e)
        {
            UpdateText();
        }

        private void Match3_OnMoveUsed(object sender, System.EventArgs e)
        {
            UpdateText();
        }

        private void Match3_OnLevelSet(object sender, System.EventArgs e)
        {
            LevelSO levelSO = match3.GetLevelSO();

            switch (levelSO.goalType)
            {
                default:
                case LevelSO.GoalType.Glass:
                    glassImage.gameObject.SetActive(true);
                    glassText.gameObject.SetActive(true);
                    targetScoreText.gameObject.SetActive(false);
                    EnemyCanvas.gameObject.SetActive(false);
                    break;
                case LevelSO.GoalType.Score:
                    glassImage.gameObject.SetActive(false);
                    glassText.gameObject.SetActive(false);
                    targetScoreText.gameObject.SetActive(true);
                    targetScoreText.text = levelSO.targetScore.ToString();
                    EnemyCanvas.gameObject.SetActive(false);
                    break;

                case LevelSO.GoalType.DefeatEnemies:
                    movesText.transform.gameObject.SetActive(false);
                    glassText.gameObject.SetActive(false);
                    glassImage.gameObject.SetActive(false);
                    scoreText.gameObject.SetActive(false);
                    glassImage.parent.gameObject.SetActive(false);
                    EnemyCanvas.gameObject.SetActive(true);
                    break; //Create Enemies

                case LevelSO.GoalType.TimeTrial:
                    targetScoreText.gameObject.SetActive(true);
                    targetScoreText.text = levelSO.targetScore.ToString();
                    movesText.text = levelSO.MaxTime.ToString();
                    movesText.gameObject.SetActive(true);
                    EnemyCanvas.gameObject.SetActive(false);
                    break; //Set Timer and start it
            }

            UpdateText();
        }

        private void UpdateText()
        {
            if (match3.ReturnTimeRemaining() <= 0 && match3.GetMoveCount() <= 0)
                movesText.text = $"{0}";
            else  
                movesText.text = match3.ReturnTimeRemaining() <= 0 ? match3.GetMoveCount().ToString() : match3.ReturnTimeRemaining().ToString();
            
            scoreText.text = match3.GetScore().ToString();
            glassText.text = match3.GetGlassAmount().ToString();
        }

        public static void TimerUpdate(float newTime)
        {
            timerText.text = $"{newTime}";
        }

        public void BackToMenuUI(GameManager.LevelManager.LoadState loadState)
        {
            GameManager.singleton.GameLoader.BackToMainMenu(loadState); 
        }

    }
}