using QFramework;
using UnityEngine;

public class KeyBoardInputController : MonoBehaviour, IController
{
    public IArchitecture GetArchitecture()
    {
        return InputAr.Interface;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            InteractionAr.Interface.SendEvent<LeftRotateEvent>();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            InteractionAr.Interface.SendEvent<DivisionEvent>();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractionAr.Interface.SendEvent<RightRotateEvent>();
        }
    }
}
