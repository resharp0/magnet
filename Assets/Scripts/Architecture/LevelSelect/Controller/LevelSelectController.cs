using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectController : MonoBehaviour, IController
{
    public string levelName;
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
            SceneManager.LoadScene(levelName);
        });
    }
}