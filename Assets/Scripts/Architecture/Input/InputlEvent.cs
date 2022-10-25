using UnityEngine;

public struct LeftRotateEvent { }

public struct RightRotateEvent { }

public struct DivisionEvent { };

public struct MouseClickEvent
{
    public Vector3 Position;

    public MouseClickEvent(Vector3 position)
    {
        Position = position;
    }
}

public struct DisplayWinPanel { }
