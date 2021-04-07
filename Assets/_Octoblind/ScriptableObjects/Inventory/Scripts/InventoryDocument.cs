using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory2")]
public class InventoryDocument : ScriptableObject
{
    public List<InventoryDocSlot> Container = new List<InventoryDocSlot>();
    public void AddItem(DocumentObject _item, int _amount)
    {
        bool hasItem = false;
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == _item)
            {
                hasItem = true;
                return;
            }
        }
        if (!hasItem)
        {
            Container.Add(new InventoryDocSlot(_item));
        }
    }
}

[System.Serializable]
public class InventoryDocSlot
{
    public DocumentObject item;

    public InventoryDocSlot(DocumentObject _item)
    {
        item = _item;
    }

}

