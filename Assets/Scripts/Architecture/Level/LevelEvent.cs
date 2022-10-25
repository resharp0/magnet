using UnityEngine;
public struct MagnetRotatedEvent
{
    public GameObject rotatedMagnet;

    public MagnetRotatedEvent(GameObject rotatedMagnet)
    {
        this.rotatedMagnet = rotatedMagnet;
    }
}

public struct AfterMoveObj 
{
    public GameObject movedObj;

    public AfterMoveObj(GameObject movedObj)
    {
        this.movedObj = movedObj;
    }
}