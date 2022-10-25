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
#if UNITY_EDITOR    //�ڱ༭��ģʽ��
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
    }
}
