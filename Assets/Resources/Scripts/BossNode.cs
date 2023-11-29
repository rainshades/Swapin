using UnityEngine.UI;
using UnityEngine;

public class BossNode : StageNodeButtons
{
    [SerializeField] private LevelSO[] Bosses; 
    public bool unLocked => PlayerObject.singleton.Player.NodesUntilBoss <= PlayerObject.singleton.Player.SavedData.CurrentNodeValue;

    private void Start()
    {
        Button.interactable = unLocked; 
    }

    public void GenerateBoss()
    {
        SetBossNode(Random.Range(0, Bosses.Length));
        PlayerObject.singleton.Player.SavedData.RouteBoss = Level; 
    }

    public void LoadBoss()
    {
        Level = PlayerObject.singleton.Player.SavedData.RouteBoss;
        int index = System.Array.IndexOf(Bosses, Level.Node);
        Image image = GetComponent<Image>();
        image.sprite = StageSprites[index];        
    }

    private void SetBossNode(int num)
    {
        Image image = GetComponent<Image>();
        image.sprite = StageSprites[num];
        Level = new RouteNode(Bosses[num]); 
    }
}
