using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Document Object", menuName = "Inventory System/Document/Document")]
public class DocumentObject : ScriptableObject
{
    public int Index;
    public string Title;
    [TextArea(15, 20)]
    public string Body;
}
