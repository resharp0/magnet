using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionAr : Architecture<InteractionAr>
{
    protected override void Init()
    {
        this.RegisterModel(new InteractionModel());
    }
}
