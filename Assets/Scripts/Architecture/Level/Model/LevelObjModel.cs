using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjModel : AbstractModel
{
    public GameObject obstacleObj;
    public GameObject moveableObj;
    public GameObject interactionObj;
    public GameObject eventObj;
    public GameObject[] needstays;
    public int currentNeedstay;
    public GameObject winObj;
    public bool win;

    public void ReSet()
    {
        obstacleObj = null;
        moveableObj = null;
        interactionObj = null;
        eventObj = null;
        needstays = null;
        currentNeedstay = 0;
        winObj = null;
        win = false;
    }

    protected override void OnInit()
    {
        currentNeedstay = 0;
    }
}

public class CompareObj : AbstractModel
{
    public GameObject obstacleObj;
    protected override void OnInit()
    {
        
    }
}
