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
                Container[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            Container.Add(new InventoryDocSlot(_item, _amount));
        }
    }
}

[System.Serializable]
public class InventoryDocSlot
{
    public DocumentObject item;
    public int amount;

    public InventoryDocSlot(DocumentObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}

