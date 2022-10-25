using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputController : MonoBehaviour, IController
{
    
    public IArchitecture GetArchitecture()
    {
        return InputAr.Interface;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -Camera.main.transform.position.z));
            MouseClickEvent mouseClick = new MouseClickEvent(pos);
            InteractionAr.Interface.SendEvent(mouseClick);
        }
    }
}
