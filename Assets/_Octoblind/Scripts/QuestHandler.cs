using UnityEngine;
using UnityEngine.UI;

public class QuestHandler : MonoBehaviour
{
    public Sprite QuestMark;
    public string description;
    public Image thisImg;

    public Vector2 position{
         get { return new Vector2(transform.position.x, transform.position.z); }
    }
    
}
