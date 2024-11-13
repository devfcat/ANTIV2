using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 씬 이름과 상태 이름이 동일하도록 적을 것
public enum eState
{
    Splash = 0,
    Main = 1,
    Prologue = 2,
    Stage01 = 3,
    Stage02 = 4,
    Stage03,
    Stage04,
    Stage05,
    Stage06,
    Stage07,
    Stage08,
    Stage09,
    Stage10,
    Stage11,
    Stage12,
    Stage13,
    Stage14,
    Stage15, // 보스 레벨전
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
    public int saved_stage;

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

        try
        {
            Resolution_Changer.Instance.Init();
        }
        catch
        {
            
        }
    }

    public void Load()
    {
        // 임시로 구현
        int loaded = PlayerPrefs.GetInt("loaded_stage");
        Debug.Log("로드된 스테이지: " + loaded);
        GameManager.Instance.saved_stage = loaded;
    }

    public void Save()
    {
        // 임시로 구현
        int loaded = (int)GameManager.Instance.m_State;
        PlayerPrefs.SetInt("loaded_stage", loaded);
        Debug.Log("저장된 스테이지: " + loaded);
        Load();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !(SceneManager.GetActiveScene().name == "Splash"))
        {
            Control_Setting();
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("저장된 정보 삭제");
        }
#endif
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
                Panel_StartSetting.GetComponent<Panel_Main>().Set_UI();
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
                Save();
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
