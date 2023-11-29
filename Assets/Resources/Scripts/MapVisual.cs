using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using CodeMonkey.Match3Game;
using System.Linq; 

[System.Serializable]
public class RouteNode
{
    public LevelSO Node, Link_1, Link_2;
    public bool Completed = false; 
    public RouteNode(LevelSO Level)
    {
        Node = Level; 

        LevelSO[] Levels = Resources.LoadAll<LevelSO>("Levels");
        Link_1 = Levels[Random.Range(0, Levels.Length)];
        Link_2 = Levels[Random.Range(0, Levels.Length)];
    }

    public RouteNode(LevelSO Node, LevelSO Link_1, LevelSO Link_2)
    {
        this.Node = Node; this.Link_1 = Link_1; this.Link_2 = Link_2; 
    }
}

public class MapVisual : MonoBehaviour
{
    [SerializeField] private List<StageNodeButtons> Nodes; 
    private StageNodeButtons CurrentNode;
    public GameObject NodeButtonPrefab;


    public void ResetStageNodes()
    {
        for (int i = 0; i < Nodes.Count; i++)
            Destroy(Nodes[i].transform.parent.gameObject);

        Nodes.Clear(); 
    }

    private void Start()
    {

        BossNode Node = FindObjectOfType<BossNode>();
        Node.AddOnClick(() => SetNode(Node));
        Node.AddOnClick(() => LaunchNode()); 

    }

    public void SetNode(StageNodeButtons Node) => CurrentNode = Node;
    public void SetNode(BossNode Node) => CurrentNode = Node; 
    public void LaunchNode() => GameManager.singleton.GameLoader.SetSceneToLoad(CurrentNode.Level.Node);
    public void RouteTaken()
    {
        PlayerObject.singleton.Player.SavedData.route[Nodes.IndexOf(CurrentNode)].Completed = true;
    }
    public List<StageNodeButtons> GetNodes => Nodes; 

    public StageNodeButtons GetNodeAt(int NodeIndex) => Nodes[NodeIndex]; 

    public void SaveRoute()
    {
        PlayerObject.singleton.Player.SavedData.route.Clear(); 

        foreach(StageNodeButtons Node in Nodes)
        {
            PlayerObject.singleton.Player.SavedData.route.Add(Node.Level);
        }
    }

    public void LoadRoute()
    {
        Nodes.Clear();

        var NodeGO = CreateNodeUI();
        Nodes.Add(NodeGO);
        NodeGO.transform.parent.position = GetComponent<Canvas>().pixelRect.center + Vector2.down * 20;
        NodeGO.Set(PlayerObject.singleton.Player.SavedData.route[0]);
        NodeGO.AddOnClick(() => SetNode(NodeGO));
        NodeGO.AddOnClick(() => RouteTaken());
        NodeGO.AddOnClick(() => LaunchNode());

        Vector2 Position = Nodes[0].transform.parent.localPosition;
        Vector2 BasePosition = Position;

        float Height = Nodes[0].GetComponent<RectTransform>().rect.height;
        float Width = Nodes[0].GetComponent<RectTransform>().rect.width;


        int row = 0;
        int segment = 0; 
        foreach (RouteNode node in PlayerObject.singleton.Player.SavedData.route)
        {
            if (PlayerObject.singleton.Player.SavedData.route.IndexOf(node) == 0)
                continue;

            bool left = PlayerObject.singleton.Player.SavedData.route.IndexOf(node) % 2 == 0;


            if (row == 5)
            {
                BasePosition += Vector2.down * (Height * 1.45f);
                Position = BasePosition;
                var NodeToAdd = CreateNode(node, Position);
                Nodes.Add(NodeToAdd);
                //Method to create new Line Renderers and Assign them to the Line Renderer Left and Right Line
                row = 0;
                Position += Vector2.up * Height;
                segment++; 
                continue;
            }


            if (left)
            {
                Vector2 Left = Position + Vector2.left * (Width * 1.45f + (Width * (row % 5)));
                Nodes.Add(CreateNode(node, Left));
                row++;
            }
            else
            {
                int rightCol = row;
                if(segment <= 0)
                    Position += Vector2.up * Height * 1.25f;

                if (segment >= 1)
                    rightCol = row - 1; 

                Vector3 Right = Position + Vector2.right * (Width * 1.45f + (Width * (rightCol % 5)));
                Nodes.Add(CreateNode(node, Right));
                
                if(segment >= 1)
                    Position += Vector2.up * Height * 1.25f;

            }
        }
    }

    private StageNodeButtons CreateNodeUI()
    {
        GameObject Go = Instantiate(NodeButtonPrefab, transform);
        StageNodeButtons MapStage = Go.GetComponentInChildren<StageNodeButtons>();
        return MapStage; 
    }

    private StageNodeButtons CreateBaseNode(LevelSO[] Levels)
    {
        var NodeGO = CreateNodeUI(); 
        NodeGO.transform.parent.position = GetComponent<Canvas>().pixelRect.center + Vector2.down * 20;  
        NodeGO.Set(Levels[Random.Range(0, Levels.Length)]);
        NodeGO.AddOnClick(() => SetNode(NodeGO));
        NodeGO.AddOnClick(() => RouteTaken());
        NodeGO.AddOnClick(() => LaunchNode());
        return NodeGO; 
    }
    private StageNodeButtons CreateNode(RouteNode Node, Vector2 Position)
    {
        var newNode = CreateNode(Node.Node, Position);
        newNode.Set(Node);
        return newNode; 
    }

    private StageNodeButtons CreateNode(LevelSO Stage, Vector2 Position)
    {
        var NodeGO = CreateNodeUI();
        NodeGO.transform.parent.localPosition = Position; 
        NodeGO.Set(Stage);
        NodeGO.AddOnClick(() => SetNode(NodeGO));
        NodeGO.AddOnClick(() => RouteTaken());
        NodeGO.AddOnClick(() => LaunchNode());
        return NodeGO; 
    }


    public void CreateRoute()
    {
        LevelSO[] Levels = Resources.LoadAll<LevelSO>("Levels");

        Nodes.Add(CreateBaseNode(Levels));

        Vector2 Position = Nodes[0].transform.parent.localPosition;
        Vector2 BasePosition = Position;

        float Height = Nodes[0].GetComponent<RectTransform>().rect.height;
        float Width = Nodes[0].GetComponent<RectTransform>().rect.width; 

        for (int i = 0; i < PlayerObject.singleton.Player.NodesUntilBoss; i++)
        {
            if(i % 5 == 0 && i > 0)
            {
                BasePosition += Vector2.down * (Height + 1f);
                Position = BasePosition;
                Nodes.Add(CreateNode(Levels[Random.Range(0, Levels.Length)], Position));
                //Method to create new Line Renderers and Assign them to the Line Renderer Left and Right Line
                continue;
            }

            Position += Vector2.up * (Height * 1.25f);
            Vector2 Left = Position + Vector2.left * (Width * 1.45f + (Width * (i % 5)));
            Vector3 Right = Position + Vector2.right * (Width * 1.45f + (Width * (i % 5)));

            Nodes.Add(CreateNode(Nodes[i].Level.Link_1, Left));
            Nodes.Add(CreateNode(Nodes[i].Level.Link_2, Right));

        }
        SaveRoute();
    }
}
