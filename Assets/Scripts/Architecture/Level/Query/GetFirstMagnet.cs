using QFramework;
using UnityEngine;

public class GetFirstMagnet : AbstractQuery<GameObject>
{
    public Vector2 origin;
    public EnumDirection direction;
    public GetFirstMagnet(Vector2 origin, EnumDirection direction)
    {
        this.origin = origin;
        this.direction = direction;
    }

    protected override GameObject OnDo()
    {
        Vector2 raycastDirection = Vector2.zero;
        switch (direction)
        {
            case EnumDirection.up:
                raycastDirection = Vector2.up;
                break;
            case EnumDirection.down:
                raycastDirection = Vector2.down;
                break;
            case EnumDirection.left:
                raycastDirection = Vector2.left;
                break;
            case EnumDirection.right:
                raycastDirection = Vector2.right;
                break;
        }
        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, raycastDirection);
        if (hits.Length < 1)
            return null;
        GameObject firstMagnet = LevelAr.Interface.GetModel<CompareObj>().obstacleObj;
        firstMagnet.transform.position = raycastDirection * 999;
        for (int i = 1;i < hits.Length; i++)
        {
            RaycastHit2D hit = hits[i];
            MagnetController magnet = hit.transform.GetComponent<MagnetController>();
            if (magnet)
            {
                switch (direction)
                {
                    case EnumDirection.up:
                        if (magnet.transform.position.y < firstMagnet.transform.position.y)
                        {
                            firstMagnet = magnet.gameObject;
                        }
                        break;
                    case EnumDirection.down:
                        if (magnet.transform.position.y > firstMagnet.transform.position.y)
                        {
                            firstMagnet = magnet.gameObject;
                        }
                        break;
                    case EnumDirection.left:
                        if (magnet.transform.position.x > firstMagnet.transform.position.x)
                        {
                            firstMagnet = magnet.gameObject;
                        }
                        break;
                    case EnumDirection.right:
                        if (magnet.transform.position.x < firstMagnet.transform.position.x)
                        {
                            firstMagnet = magnet.gameObject;
                        }
                        break;
                }
            }
        }
        if (firstMagnet.GetComponent<MagnetController>() == null)
            return null;
        return firstMagnet;
    }
}
