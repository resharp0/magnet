using QFramework;
using Unity.VisualScripting;
using UnityEngine;
public class GetFirstObstacle : AbstractQuery<GameObject>
{
    public Vector2 origin;
    public EnumDirection direction;

    public GetFirstObstacle(Vector2 origin, EnumDirection direction)
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
        GameObject firstObstacle = LevelAr.Interface.GetModel<CompareObj>().obstacleObj;
        firstObstacle.transform.position = raycastDirection * 999;
        foreach (RaycastHit2D hit in hits)
        {
            SpriteRenderer sprite = hit.transform.GetComponent<SpriteRenderer>();
            if(sprite != null)
            {
                if (sprite.sortingLayerName.Equals("obstacle"))
                {
                    switch (direction)
                    {
                        case EnumDirection.up:
                            if(sprite.transform.position.y < firstObstacle.transform.position.y)
                            {
                                firstObstacle = sprite.gameObject;
                            }
                            break;
                        case EnumDirection.down:
                            if (sprite.transform.position.y > firstObstacle.transform.position.y)
                            {
                                firstObstacle = sprite.gameObject;
                            }
                            break;
                        case EnumDirection.left:
                            if (sprite.transform.position.x > firstObstacle.transform.position.x)
                            {
                                firstObstacle = sprite.gameObject;
                            }
                            break;
                        case EnumDirection.right:
                            if (sprite.transform.position.x < firstObstacle.transform.position.x)
                            {
                                firstObstacle = sprite.gameObject;
                            }
                            break;
                    }
                }
            }
        }
        if (!firstObstacle.GetComponent<SpriteRenderer>())
            return null;
        return firstObstacle;
    }
}