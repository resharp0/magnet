//using QFramework;
//using System.Collections;
//using Unity.VisualScripting;
//using UnityEngine;

//public class UnidirectionMoveObj : AbstractCommand
//{
//    public GameObject moveObj;
//    public EnumDirection direction;
//    public Vector2 end;
//    public float speed; //每帧移动的向量
//    public MonoBehaviour mono; //协程需要MonoBehaviour

//    public UnidirectionMoveObj(GameObject moveObj, EnumDirection direction, Vector2 end, float speed, MonoBehaviour mono)
//    {
//        this.moveObj = moveObj;
//        this.direction = direction;
//        this.end = end;
//        this.speed = speed;
//        this.mono = mono;
//    }

//    protected override void OnExecute()
//    {
//        mono.StartCoroutine(Move());
//    }

//    //IEnumerator Move(GameObject moveObj, EnumDirection direction, Vector2 end, float speed)
//    //{
//    //    switch (direction)
//    //    {
//    //        case EnumDirection.left:
//    //            speed = -speed;
//    //            break;
//    //        case EnumDirection.down:
//    //            speed = -speed;
//    //            break;
//    //    }
//    //    if (direction == EnumDirection.left || direction == EnumDirection.right)
//    //    {
//    //        while (Mathf.Abs(moveObj.transform.position.x) <= Mathf.Abs(end.x))
//    //        {
//    //            Vector2 newPos = new Vector2(moveObj.transform.position.x + speed, moveObj.transform.position.y);
//    //            moveObj.transform.position = newPos;
//    //            yield return new WaitForSeconds(0.01f);
//    //        }
//    //    }
//    //    else if (direction == EnumDirection.up || direction == EnumDirection.down)
//    //    {
//    //        while (Mathf.Abs(moveObj.transform.position.y) <= Mathf.Abs(end.y))
//    //        {
//    //            Vector2 newPos = new Vector2(moveObj.transform.position.x, moveObj.transform.position.y + speed);
//    //            moveObj.transform.position = newPos;
//    //            yield return new WaitForSeconds(0.01f);
//    //        }
//    //    }
//    //    moveObj.transform.position = end;
//    //    LevelAr.Interface.SendEvent(new AfterMoveObj(moveObj.transform.gameObject));
//    //}
    
//}