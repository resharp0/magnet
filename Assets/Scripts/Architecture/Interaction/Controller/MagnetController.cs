using QFramework;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum EnumDirection
{
    up,
    left,
    down,
    right,
}

public class MagnetController : MonoBehaviour,IController
{
    [Header("磁力大小")]
    public float magnetism;
    [Header("磁力方向")]
    [Tooltip("北极朝向")]
    public EnumDirection poleN;
    [Tooltip("南极朝向")]
    public EnumDirection poleS;
    public bool select;

    public bool moveable;
    public EnumDirection moveDirection;
    public Vector2 MoveEnd;
    public float MoveSpeed = 1f;

    private Animator anim;

    public IArchitecture GetArchitecture()
    {
        return InteractionAr.Interface;
    }

    public void SetMove(EnumDirection moveDirection, Vector2 MoveEnd)
    {
        this.moveDirection = moveDirection;
        this.MoveEnd = MoveEnd;
        switch (moveDirection)
        {
            case EnumDirection.left:
                MoveSpeed = -Mathf.Abs(MoveSpeed);
                break;
            case EnumDirection.down:
                MoveSpeed = -Mathf.Abs(MoveSpeed);
                break;
                default:
                MoveSpeed = Mathf.Abs(MoveSpeed);
                break;
        }
        moveable = true;
    }

    public void NextPole()
    {
        void Next(ref EnumDirection value)
        {
            if (value == EnumDirection.right)
            {
                value = EnumDirection.up;
            }
            else
            {
                value++;
            }
        }

        Next(ref poleN);
        anim.SetFloat("direction",(float)poleN );
        Next(ref poleS);
    }

    public void PreviousPole()
    {
        void Previous(ref EnumDirection value)
        {
            if (value == EnumDirection.up)
            {
                value = EnumDirection.right;
            }
            else
            {
                value--;
            }
        }

        Previous(ref poleN);
        anim.SetFloat("direction", (float)poleN);
        Previous(ref poleS);
    }

    public static Vector3 DirectionToVector3(EnumDirection direction)
    {
        Vector3 Pos = Vector3.zero;
        switch (direction)
        {
            case EnumDirection.up:
                Pos = Vector3.up;
                break;
            case EnumDirection.down:
                Pos = Vector3.down;
                break;
            case EnumDirection.left:
                Pos = Vector3.left;
                break;
            case EnumDirection.right:
                Pos = Vector3.right;
                break;
        }
        return Pos;
    }

    void FixedUpdate()
    {
        if (!moveable)
            return;
        if (transform.position.x != MoveEnd.x || transform.position.y != MoveEnd.y)
        {

            switch (moveDirection)
            {
                case EnumDirection.left:
                    if(transform.position.x > MoveEnd.x)
                    {
                        Vector2 newPos = new Vector2(transform.position.x + MoveSpeed * Time.fixedDeltaTime, transform.position.y);
                        transform.position = newPos;

                    }
                    else
                    {
                        transform.position = MoveEnd;
                        LevelAr.Interface.SendEvent(new AfterMoveObj(transform.gameObject));
                        //this.GetModel<InteractionModel>().diving = false;
                    }
                    break;
                case EnumDirection.right:
                    if (transform.position.x < MoveEnd.x)
                    {
                        Vector2 newPos = new Vector2(transform.position.x + MoveSpeed * Time.fixedDeltaTime, transform.position.y);
                        transform.position = newPos;
                    }
                    else
                    {
                        transform.position = MoveEnd;
                        LevelAr.Interface.SendEvent(new AfterMoveObj(transform.gameObject));
                        //this.GetModel<InteractionModel>().diving = false;
                    }
                    break;
                case EnumDirection.up:
                    if (transform.position.y < MoveEnd.y)
                    {
                        Vector2 newPos = new Vector2(transform.position.x, transform.position.y + MoveSpeed * Time.fixedDeltaTime);
                        transform.position = newPos;
                    }
                    else
                    {
                        transform.position = MoveEnd;
                        LevelAr.Interface.SendEvent(new AfterMoveObj(transform.gameObject));
                        //this.GetModel<InteractionModel>().diving = false;
                    }
                    break;
                case EnumDirection.down:
                    if (transform.position.y > MoveEnd.y)
                    {
                        Vector2 newPos = new Vector2(transform.position.x, transform.position.y + MoveSpeed * Time.fixedDeltaTime);
                        transform.position = newPos;
                    }
                    else
                    {
                        transform.position = MoveEnd;
                        LevelAr.Interface.SendEvent(new AfterMoveObj(transform.gameObject));
                        //this.GetModel<InteractionModel>().diving = false;
                    }
                    break;
            }

        }
    }
    void Start()
    {
        //MoveEnd = transform.position;
        anim = GetComponent<Animator>();
        anim.SetFloat("direction", (float)poleN);

        #region RegEvent
        this.RegisterEvent<MouseClickEvent>(mouseClick =>
        {
            if (
            Mathf.Abs(mouseClick.Position.x - transform.position.x) <= 0.5&
            Mathf.Abs(mouseClick.Position.y - transform.position.y) <= 0.5)
            {
                select = true;
                anim.SetBool("select", true);
                
            }
            else
            {
                select = false;
                anim.SetBool("select", false);
            }
        }).UnRegisterWhenGameObjectDestroyed(gameObject);

        this.RegisterEvent<LeftRotateEvent>(e =>
        {
            if (select)
            {
                NextPole();
                LevelAr.Interface.SendEvent(new MagnetRotatedEvent(gameObject));
            }
        }).UnRegisterWhenGameObjectDestroyed(gameObject);

        this.RegisterEvent<DivisionEvent>(e =>
        {
            if (select && magnetism >= 2)
            {
                // 检测是否有磁铁相斥
                GameObject hasMagnet;
                hasMagnet = this.SendQuery(new GetFirstMagnet(this.transform.position, this.poleN));
                if (hasMagnet)
                {
                    if (hasMagnet.GetComponent<MagnetController>().poleN == this.poleS)
                        return;
                }
                hasMagnet = this.SendQuery(new GetFirstMagnet(this.transform.position, this.poleS));
                if (hasMagnet)
                {
                    if (hasMagnet.GetComponent<MagnetController>().poleN == this.poleS)
                        return;
                }

                GameObject nObstacle = this.SendQuery(new GetFirstObstacle(transform.position, poleN));
                GameObject sObstacle = this.SendQuery(new GetFirstObstacle(transform.position, poleS));

                // 判断分割后的磁铁是否会被挡住
                Vector3 nPos = nObstacle.transform.position - DirectionToVector3(poleN);
                Vector3 sPos = sObstacle.transform.position - DirectionToVector3(poleS);

                if(
                Mathf.Abs(nPos.x) >= 1 || Mathf.Abs(nPos.y) >= 1||
                Mathf.Abs(sPos.x) >= 1 || Mathf.Abs(sPos.y) >= 1)
                {
                    //this.GetModel<InteractionModel>().diving = true;
                    select = false;
                    anim.SetBool("select", false);
                    magnetism = magnetism / 2;
                    GameObject obj = Instantiate(gameObject, transform.position, Quaternion.identity,transform.parent);
                    MagnetController objC = obj.GetComponent<MagnetController>();
                    objC.poleN = this.poleS;
                    objC.poleS = this.poleN;
                    SetMove(poleN, nPos);
                    objC.SetMove(poleS, sPos);
                }
            }
            
        }).UnRegisterWhenGameObjectDestroyed(gameObject);

        this.RegisterEvent<RightRotateEvent>(e =>
        {
            if (select)
            {
                PreviousPole();
                LevelAr.Interface.SendEvent(new MagnetRotatedEvent(gameObject));
            }
        }).UnRegisterWhenGameObjectDestroyed(gameObject);
        #endregion
    }
}
