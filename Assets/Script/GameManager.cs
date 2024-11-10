using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum eState
{
    Splash = 0,
    Main,
    Prologue,
    Stage01,
    Stage02,
    Ending
}

public enum gameState
{
    Default = 0,
    Setting,
}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    [Header("상태 정보")]
    public eState m_State; // 현재 씬 상태
    public gameState g_State; // 현재 게임 상태
    public bool isWorking;

    [Header("설정 창")]
    public GameObject Panel_StartSetting;
    public GameObject Panel_Setting;

    [Header("UI")]
    public GameObject Panel_fade;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        Init();
    }

    public void Init()
    {
        Application.targetFrameRate = 60;

        isWorking = false;
        SetState(eState.Splash);
        SetGameState(gameState.Default);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !(m_State == eState.Splash))
        {
            Control_Setting();
        }
    }

    // 설정창을 끄거나 키는 기능
    public void Control_Setting()
    {
        SoundManager.Instance.PlaySFX(SFX.Setting);
        if (g_State == gameState.Default)
        {
            SetGameState(gameState.Setting);
        }
        else
        {
            SetGameState(gameState.Default);
        }
    }

    // 설정창 또는 일반 스테이지 등을 구분하는 상태 머신
    public void SetGameState(gameState state)
    {
        g_State = state;

        if (m_State == eState.Main)
        {
            Panel_Setting.SetActive(false);
            try
            {
                MainManager.Instance.Control_UI();
            }
            catch {}
            if (g_State == gameState.Setting)
            {
                Panel_StartSetting.SetActive(true);
            }
            else
            {
                Panel_StartSetting.SetActive(false);
            }
        }
        else
        {
            Panel_StartSetting.SetActive(false);
            if (g_State == gameState.Setting)
            {
                Panel_Setting.SetActive(true);
            }
            else
            {
                Panel_Setting.SetActive(false);
            }
        }
    }
    
    // 상태 머신 함수
    public void SetState(eState state)
    {
        if (isWorking)
        {
            return;
        }
        else
        {
            isWorking = true;
        }

        m_State = state;

        switch(m_State)
        {
            case eState.Splash:
                isWorking = false;
                SetState(eState.Main);
                break;
            case eState.Main:
                StartCoroutine(Change_Scene("Main"));
                break;
            case eState.Prologue:
                StartCoroutine(Change_Scene("Prologue"));
                break;
            case eState.Stage01:
                StartCoroutine(Change_Scene("Stage01"));
                break;
            default:
                isWorking = false;
                SetState(eState.Main);
                break;
            
        }
    }

    IEnumerator Change_Scene(string scenename)
    {
        Panel_fade.SetActive(false);
        Panel_fade.SetActive(true);
        Fade(2);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadSceneAsync(scenename);
        SetGameState(gameState.Default);
        Fade(1);
        isWorking = false;
    }
    
    public void Fade(int fadeType)
    {
        Panel_fade.GetComponent<Animator>().SetInteger("FadeType", fadeType);
    }

}
