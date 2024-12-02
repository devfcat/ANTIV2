using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum Character
{
    P = 0,
    V = 1,
}

public class StageManager : MonoBehaviour
{
    private static StageManager _instance;
    public static StageManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(StageManager)) as StageManager;
                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    [Header("제어변수")]
    [SerializeField] float speed; // 이동 속도
    [SerializeField] float r_speed; // 각 속도
    public bool isGameOver = false;
    public TextMeshProUGUI value_stagePercent;
    [Tooltip("현재 조종 캐릭터")] public Character character;
    [Tooltip("애니메이션 모드")] public int animation_mod;

    [Tooltip("캐릭터 변경 중인가")] public bool isWorking = false;

    public Stage stage;

    public float vx; // 속도
    public float vy; // 속도
    public float rotate_num;
    bool leftFlag;

    public void Awake()
    {
        Init();
    }
    
    void Update()
    {
        vx = 0;
        vy = 0;
        rotate_num = 0;

        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            vx = speed; // 오른쪽으로 이동
            rotate_num = -r_speed;
            leftFlag = false;
        }
        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            vx = -speed; // 왼쪽으로 이동
            rotate_num = r_speed;
            leftFlag = true;
        }
        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            animation_mod = 1;
            vy = speed; // 위쪽으로 이동
        }
        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            animation_mod = 2;
            vy = -speed; // 아래쪽으로 이동
        }

        if (vy == 0 && vx == 0)
        {
            animation_mod = 0;
        }

        if (Input.GetKey("tab") && !isWorking)
        {
            StartCoroutine(Change_Character());
        }
    }

    IEnumerator Change_Character()
    {
        isWorking = true;
        SoundManager.Instance.PlaySFX(SFX.Setting);

        if (character == Character.P)
        {
            character = Character.V;
        }
        else
        {
            character = Character.P;
        }

        stage.Shaking_Stage();

        yield return new WaitForSeconds(0.5f);

        isWorking = false;
    } 

    public void Init()
    {
        SoundManager.Instance.PlayBGM(BGM.Stage);
        animation_mod = 0;
        isWorking = false;
        isGameOver = false;
    }

    public void Set_Percent()
    {
        int stage_num = (int)GameManager.Instance.m_State - 3;
        Debug.Log(stage_num + "번째 스테이지");

        int percent = 0;

        if (stage_num < 3)
        {
            percent = stage_num*10;
        }
        else
        {
            if (stage_num < 10)
            {
                percent = 26 + stage_num*6;
            }
            else // 10~13
            {
                percent = 80 + stage_num*5;
            }

        }

        value_stagePercent.text = "회로 완성도: " + percent.ToString() + "%";
    }

    public void Click_Setting()
    {
        GameManager.Instance.Control_Setting();
    }
}
