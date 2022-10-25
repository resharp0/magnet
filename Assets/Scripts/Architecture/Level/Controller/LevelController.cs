using QFramework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class LevelController : MonoBehaviour, IController
{
    private LevelObjModel levelObjModel;

    public IArchitecture GetArchitecture()
    {
        return LevelAr.Interface;
    }

    void Start()
    {
        levelObjModel = this.GetModel<LevelObjModel>();
        levelObjModel.ReSet();

        levelObjModel.obstacleObj = transform.Find("Obstacle").gameObject;
        levelObjModel.interactionObj = transform.Find("Interaction").gameObject;
        levelObjModel.moveableObj = transform.Find("Moveable").gameObject;
        levelObjModel.eventObj = transform.Find("Event").gameObject;
        levelObjModel.needstays = GameObject.FindGameObjectsWithTag("needstay");
        levelObjModel.winObj = GameObject.FindGameObjectWithTag("win");

        Transform parrent = GameObject.Find("Obstacle").transform;
        GameObject obstacleObj = new GameObject("obstacleObj");
        obstacleObj.transform.SetParent(parrent);
        this.GetModel<CompareObj>().obstacleObj = obstacleObj;

        #region RegEvent
        this.RegisterEvent<MagnetRotatedEvent>(e =>
        {
            MagnetController rotatedMagnet = e.rotatedMagnet.GetComponent<MagnetController>();
            GameObject nearNMagnetObj = this.SendQuery(new GetFirstMagnet(rotatedMagnet.transform.position, rotatedMagnet.poleN));
            if (nearNMagnetObj)
            {
                MagnetController nearNMagnetC = nearNMagnetObj.GetComponent<MagnetController>();
                // 异极相吸
                if (rotatedMagnet.poleN == nearNMagnetC.poleN)
                {
                    GameObject obstacleN = this.SendQuery(new GetFirstObstacle(rotatedMagnet.transform.position, rotatedMagnet.poleN));
                    GameObject obstacleS = this.SendQuery(new GetFirstObstacle(nearNMagnetC.transform.position, rotatedMagnet.poleS));

                    float endx = (rotatedMagnet.transform.position.x + nearNMagnetC.transform.position.x);
                    endx = endx == 0 ? 0 : endx / 2;
                    float endy = (rotatedMagnet.transform.position.y + nearNMagnetC.transform.position.y);
                    endy = endy == 0 ? 0 : endy / 2;
                    Vector2 end = new Vector2(endx, endy);

                    if (obstacleN)
                    {
                        if (obstacleN.CompareTag("destroyable")){
                            if(obstacleN == obstacleS)
                            {
                                rotatedMagnet.SetMove(rotatedMagnet.poleN, obstacleN.transform.position);
                                nearNMagnetC.SetMove(rotatedMagnet.poleS, obstacleN.transform.position);
                            }
                        }
                        else if(
                        obstacleN == this.SendQuery(new GetFirstObstacle(nearNMagnetC.transform.position, rotatedMagnet.poleN))&&
                        obstacleS == this.SendQuery(new GetFirstObstacle(rotatedMagnet.transform.position, rotatedMagnet.poleS)))
                        {
                            rotatedMagnet.SetMove(rotatedMagnet.poleN, end);
                            nearNMagnetC.SetMove(rotatedMagnet.poleS, end);
                        }

                    }
                    else
                    {
                        rotatedMagnet.SetMove(rotatedMagnet.poleN, end);
                        nearNMagnetC.SetMove(rotatedMagnet.poleS, end);
                    }
                }

                // 同极相斥
                if (rotatedMagnet.poleN == nearNMagnetC.poleS)
                {
                    GameObject obtacleS = this.SendQuery(new GetFirstObstacle(rotatedMagnet.transform.position, rotatedMagnet.poleS));
                    Vector3 directionS = MagnetController.DirectionToVector3(rotatedMagnet.poleS);
                    Vector2 endS = new Vector2(obtacleS.transform.position.x - directionS.x, obtacleS.transform.position.y - directionS.y);

                    rotatedMagnet.SetMove(rotatedMagnet.poleS, endS);

                    obtacleS = this.SendQuery(new GetFirstObstacle(nearNMagnetC.transform.position, nearNMagnetC.poleS));
                    directionS = MagnetController.DirectionToVector3(nearNMagnetC.poleS);
                    Vector2 endN = new Vector2(obtacleS.transform.position.x - directionS.x, obtacleS.transform.position.y - directionS.y);
                    nearNMagnetC.SetMove(nearNMagnetC.poleS, endN);
                }
            }

            GameObject nearSMagnetObj = this.SendQuery(new GetFirstMagnet(rotatedMagnet.transform.position, rotatedMagnet.poleS));
            if (nearSMagnetObj)
            {
                MagnetController nearSMagnetC = nearSMagnetObj.GetComponent<MagnetController>();
                // 异极相吸
                if (rotatedMagnet.poleS == nearSMagnetC.poleS)
                {
                    GameObject obstacleS = this.SendQuery(new GetFirstObstacle(rotatedMagnet.transform.position, rotatedMagnet.poleS));
                    GameObject obstacleN = this.SendQuery(new GetFirstObstacle(nearSMagnetC.transform.position, rotatedMagnet.poleN));

                    float endx = (rotatedMagnet.transform.position.x + nearSMagnetC.transform.position.x);
                    endx = endx == 0 ? 0 : endx / 2;
                    float endy = (rotatedMagnet.transform.position.y + nearSMagnetC.transform.position.y);
                    endy = endy == 0 ? 0 : endy / 2;
                    Vector2 end = new Vector2(endx, endy);

                    if (obstacleS)
                    {
                        if (obstacleS.CompareTag("destroyable"))
                        {
                            if (obstacleS == obstacleN)
                            {
                                rotatedMagnet.SetMove(rotatedMagnet.poleS, obstacleS.transform.position);
                                nearSMagnetC.SetMove(nearSMagnetC.poleN, obstacleS.transform.position);
                            }
                        }
                        else if (
                            obstacleS == this.SendQuery(new GetFirstObstacle(nearSMagnetC.transform.position, rotatedMagnet.poleS)) &&
                            obstacleN == this.SendQuery(new GetFirstObstacle(rotatedMagnet.transform.position, rotatedMagnet.poleN)))
                        {
                            rotatedMagnet.SetMove(rotatedMagnet.poleS, end);
                            nearSMagnetC.SetMove(rotatedMagnet.poleN, end);
                        }
                    }
                    else
                    {
                        rotatedMagnet.SetMove(rotatedMagnet.poleS, end);
                        nearSMagnetC.SetMove(nearSMagnetC.poleN, end);
                    }
                }

                    // 同极相斥
                    if (rotatedMagnet.poleN == nearSMagnetC.poleS)
                {
                    GameObject obtacleN = this.SendQuery(new GetFirstObstacle(rotatedMagnet.transform.position, rotatedMagnet.poleN));
                    Vector3 directionN = MagnetController.DirectionToVector3(rotatedMagnet.poleN);
                    Vector2 endS = new Vector2(obtacleN.transform.position.x - directionN.x, obtacleN.transform.position.y - directionN.y);
                    rotatedMagnet.SetMove(rotatedMagnet.poleN, endS);

                    obtacleN = this.SendQuery(new GetFirstObstacle(nearSMagnetC.transform.position, nearSMagnetC.poleN));
                    directionN = MagnetController.DirectionToVector3(nearSMagnetC.poleN);
                    Vector2 endN = new Vector2(obtacleN.transform.position.x - directionN.x, obtacleN.transform.position.y - directionN.y);
                    nearSMagnetC.SetMove(nearSMagnetC.poleN, endN);
                }
            }

        }).UnRegisterWhenGameObjectDestroyed(gameObject);

        this.RegisterEvent<AfterMoveObj>(e =>
        {
            foreach(Transform transfrom in levelObjModel.interactionObj.transform)
            {
                if (e.movedObj == transfrom.gameObject)
                    continue;
                if(e.movedObj.transform.position == transfrom.position)
                {
                    // 磁铁合并
                    if (transfrom.GetComponent<MagnetController>() && e.movedObj.GetComponent<MagnetController>())
                    {
                        MagnetController magnet = e.movedObj.GetComponent<MagnetController>();
                        magnet.magnetism = magnet.magnetism + e.movedObj.GetComponent<MagnetController>().magnetism;
                        Destroy(transfrom.gameObject,1);
                    }
                    
                }
            }

            // 经过任务点
            foreach(GameObject obj in levelObjModel.needstays)
            {
                if(obj.transform.position == e.movedObj.transform.position)
                {
                    if (!obj.GetComponent<Animator>().GetBool("active")) 
                    {
                        obj.GetComponent<Animator>().SetBool("active", true);
                        levelObjModel.currentNeedstay++;
                        if(levelObjModel.currentNeedstay >= levelObjModel.needstays.Length)
                        {
                            levelObjModel.winObj.GetComponent<Animator>().SetBool("active", true);
                        }
                    }
                }
            }

            // win
            if (levelObjModel.winObj.GetComponent<Animator>().GetBool("active"))
            {
                if (levelObjModel.winObj.transform.position == e.movedObj.transform.position)
                {
                    if (!levelObjModel.win)
                    {
                        levelObjModel.win = true;
                        InputAr.Interface.SendEvent<DisplayWinPanel>();
                    }
                }
            }

            //摧毁可摧毁的障碍物
            GameObject[] destroyables = GameObject.FindGameObjectsWithTag("destroyable");
            foreach(GameObject destroyable in destroyables)
            {
                if(destroyable.transform.position == e.movedObj.transform.position)
                {
                    Destroy(destroyable);
                }
            }
        }).UnRegisterWhenGameObjectDestroyed(gameObject);
        #endregion
    }
}


