using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var player = target as Player;
        if (GUILayout.Button("Reset Player"))
        {
            player.Reset(); 
        }
        if(GUILayout.Button("Remove Slot 1"))
        {
            player.RemoveEquipmentFromSlot_1(); 
        }if(GUILayout.Button("Remove Slot 2"))
        {
            player.RemoveEquipmentFromSlot_2();
        }
        if (GUILayout.Button("Update Inventory"))
        {
            player.Inventory.Clear();
            player.CreateEquipment(); 
        }
    }
}
#endif