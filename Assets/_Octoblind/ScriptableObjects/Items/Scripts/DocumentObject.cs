using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Document Object", menuName = "Inventory System/Item/Document")]
public class DocumentObject : ItemObject
{
    public int Index;
    public string Title;
    [TextArea(15, 20)]
    public string Body;
    public void Awake()
    {
        type = ItemType.Document;
    }
}
