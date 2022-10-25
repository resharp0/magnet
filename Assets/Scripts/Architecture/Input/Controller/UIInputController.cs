using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class UIInputController : MonoBehaviour, IController
{
    private Button leftRotateBtn;
    private Button divisionBtn;
    private Button rightRotateBtn;
    private Button restartBtn;
    private GameObject winPanel;

    public IArchitecture GetArchitecture()
    {
        return InputAr.Interface;
    }

    void Start()
    {
        Transform canvas = transform.Find("Canvas");
        leftRotateBtn = canvas.Find("LeftRotateBtn").GetComponent<Button>();
        divisionBtn = canvas.Find("DivisionBtn").GetComponent<Button>();
        rightRotateBtn = canvas.Find("RightRotateBtn").GetComponent<Button>();
        restartBtn = canvas.Find("RestartBtn").GetComponent<Button>();
        winPanel = canvas.Find("WinPanel").gameObject;

        #region RegEvent
        leftRotateBtn.onClick.AddListener(() =>
        {
            InteractionAr.Interface.SendEvent<LeftRotateEvent>();
        });

        divisionBtn.onClick.AddListener(() =>
        {
            InteractionAr.Interface.SendEvent<DivisionEvent>();
        });

        rightRotateBtn.onClick.AddListener(() =>
        {
            InteractionAr.Interface.SendEvent<RightRotateEvent>();
        });

        restartBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });

        this.RegisterEvent<DisplayWinPanel>(e =>
        {
            winPanel.SetActive(true);
        }).UnRegisterWhenGameObjectDestroyed(gameObject);
        #endregion
    }
}
