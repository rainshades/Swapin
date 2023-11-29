using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey;

[System.Serializable]
public class MapData
{
    public Image CurrentNode;
    public int CurrentNodeValue {
        get
        {
            int val = 0;
            if (route != null)
                foreach (RouteNode r in route)
                    val = r.Completed ? val + 1 : val;
            return val; 
        }
    }

    public List<RouteNode> route;
    public RouteNode RouteBoss; 

    public void Reset()
    {
        route = null; 
    }
}

public class MapMenu : MonoBehaviour
{
    public TextMeshProUGUI RemainingNodesText;
    private MapVisual Map;
    [SerializeField] BossNode BossNode;


    private void Start()
    {
        RemainingNodesText.text = $"{PlayerObject.singleton.Player.SavedData.CurrentNodeValue} / {PlayerObject.singleton.Player.NodesUntilBoss}";
    }

    private void OnDisable()
    {
        Map.ResetStageNodes(); 
    }

    public void GetNodes()
    {
        if (Map == null)
            Map = GetComponent<MapVisual>();

        Map.LoadRoute();
        BossNode.LoadBoss();
    }

    public void AssignNodes()
    {
        if (Map == null)
            Map = GetComponent<MapVisual>();

        Map.CreateRoute();
        BossNode.GenerateBoss(); 
    }

    public void LaunchNode() =>
        Map.LaunchNode();

}
