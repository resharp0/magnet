using QFramework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitGameController : MonoBehaviour, IController
{
    private Button btn;
    public IArchitecture GetArchitecture()
    {
        return LevelSelectAr.Interface;
    }

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
#if UNITY_EDITOR    //在编辑器模式下
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
    }
}
