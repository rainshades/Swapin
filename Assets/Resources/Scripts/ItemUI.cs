using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inventory; 

public class ItemUI : MonoBehaviour
{
    public Equipment AssignedEquipment;
    private Button Button;
    private Image Image;
    private TMPro.TextMeshProUGUI ItemText; 

    private void Awake()
    {
        Button = GetComponent<Button>();
        Image = GetComponent<Image>();
        Button.onClick.AddListener(() => AssignItem());
        ItemText = GetComponentInChildren<TMPro.TextMeshProUGUI>(); 
        
    }

    private void Update()
    {
        Button.interactable = AssignedEquipment.Unlocked;
        Image.sprite = AssignedEquipment.EquipmentSprite;
        ItemText.text = AssignedEquipment.Unlocked ? "---" : $"Buy : {AssignedEquipment.CoinCost}";
    }

    private void AssignItem()
    {
        PlayerObject.singleton.Player.AssignEquipment(AssignedEquipment); 
    }

    public void BuyItem()
    {
        if(!AssignedEquipment.Unlocked && PlayerObject.singleton.Player.Coins >= AssignedEquipment.CoinCost)
        {
            AssignedEquipment.Unlocked = true;
            PlayerObject.singleton.Player.Coins -= AssignedEquipment.CoinCost; 
        }
    }
}
