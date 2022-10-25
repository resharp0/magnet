using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionModel : AbstractModel
{
    public bool diving;

    protected override void OnInit()
    {
        diving = false;
    }
}
