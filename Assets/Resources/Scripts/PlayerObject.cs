using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public static PlayerObject singleton;

    public Player Player;

    private void Awake()
    {
        singleton = this;
    }

}