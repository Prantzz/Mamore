using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialChanger : MonoBehaviour
{
    public void ChangeT()
    {
        GameGlobeData.GameCon.AlterTutorialCondition();
    }
}
