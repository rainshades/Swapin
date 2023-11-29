using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class StageNodeButtons : MonoBehaviour
{
    public RouteNode Level { get; set; }
    protected Button Button; 
    [SerializeField]protected Sprite[] StageSprites;

    protected virtual void Awake()
    {
        Button = GetComponent<Button>(); 
    }

    public void AddOnClick(UnityAction Action)
    {
        Button.onClick.AddListener(Action);
        FindObjectOfType<BannerAd>().HideBannerAd(); 
    }
    
    public void Set(LevelSO level)
    {
        Level = new RouteNode(level);
        SetStageSprite(level.name);
        Button.interactable = !Level.Completed; 
    }

    public void Set(RouteNode route)
    {
        Level = route;
        SetStageSprite(route.Node.name);
        Button.interactable = !route.Completed; 
    }

    public void SetStageSprite(string LevelName)
    {
        var Image = GetComponent<Image>(); 

        switch (LevelName)
        {
            case "Level_A":
                Image.sprite = StageSprites[0]; 
                break;
             case "Level_B":
                Image.sprite = StageSprites[1]; 
                break;
             case "Level_C":
                Image.sprite = StageSprites[2]; 
                break;
             case "Level_D":
                Image.sprite = StageSprites[3]; 
                break;
        }
    }

}
