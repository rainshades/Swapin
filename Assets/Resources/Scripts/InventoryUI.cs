using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using System.Linq; 

public class InventoryUI : MonoBehaviour
{
    public GameObject ImagePrefab;
    [SerializeField] private Transform Panel;
    [SerializeField] private Image Slot_1, Slot_2;
    [SerializeField] TextMeshProUGUI Cash;
    public List<ItemUI> UIInventory = new List<ItemUI>(); 

    private void Awake()
    {
        var Inventory = PlayerObject.singleton.Player.Inventory;

        for(int i = 0; i < Inventory.Count; i++)
        {
            GameObject ItemGO = Instantiate(ImagePrefab, Panel);
            ItemUI Item = ItemGO.GetComponent<ItemUI>();
            Item.AssignedEquipment = Inventory[i];
            UIInventory.Add(Item);
        }
    }

    private void Update()
    {
        for(int i = 0; i < UIInventory.Count; i++)
        {
            UIInventory[i].AssignedEquipment = PlayerObject.singleton.Player.Inventory[i]; 
        }
        try
        {
            Slot_1.sprite = PlayerObject.singleton.Player.Slot_1.EquipmentSprite;
        }
        catch { Slot_1.sprite = null; }
        try
        {
            Slot_2.sprite = PlayerObject.singleton.Player.Slot_2.EquipmentSprite;
        }
        catch { Slot_2.sprite = null; }

        Cash.text = $"{PlayerObject.singleton.Player.Coins}";

    }

    public void RemoveFromInventory(bool is_Slot_1) => PlayerObject.singleton.Player.RemoveEquipment(is_Slot_1);

}
