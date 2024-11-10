using UnityEngine;

public class MainManager : MonoBehaviour
{
    [Header("패널")]
    public GameObject Panel_Start;

    private static MainManager _instance;
    public static MainManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(MainManager)) as MainManager;
                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    public void Start()
    {
        Init();
    }

    public void Init()
    {
        SoundManager.Instance.PlayBGM(BGM.Main);
    }

    public void Click_Setting()
    {
        GameManager.Instance.Control_Setting();
    }

    public void Control_UI()
    {
        if (GameManager.Instance.g_State == gameState.Default)
        {
            Panel_Start.SetActive(true);
        }
        else
        {
            Panel_Start.SetActive(false);
        }
    }

}
