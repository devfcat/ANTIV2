using UnityEngine;
using TMPro;

/// <summary>
/// 창모드 / 전체화면 모드 전환 기능
/// </summary>
/// 
public enum eResolution
{
    fullscreen = 0,
    big = 1,
    midium = 2,
    small = 3,
    low = 4,
}
public class Resolution_Changer : MonoBehaviour
{
    private static Resolution_Changer _instance;
    public static Resolution_Changer Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(Resolution_Changer)) as Resolution_Changer;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    public eResolution m_resolution;
    public TextMeshProUGUI tmp;

    public int temp_width_windowed;
    public int temp_height_windowed;

    public void Awake()
    {
        Init();
    }

    public void Control_data(bool isSave = false)
    {
        if (isSave)
        {
            PlayerPrefs.SetInt("eResolution", (int)m_resolution);
        }
        else
        {
            int saved = PlayerPrefs.GetInt("eResolution");
            if (saved == 0)
            {
                m_resolution = eResolution.fullscreen;
            }
            else
            {
                m_resolution = (eResolution)saved;
            }
        }
    }

    public void Init()
    {
        Control_data();

        temp_width_windowed = Screen.width;
        temp_height_windowed = Screen.height;

        Set_UI();
        Set_Resolution();
    }

    public void Update()
    {
        /*
        if (m_resolution != eResolution.fullscreen) // 창모드이면
        {
            if (temp_width_windowed != Screen.width || temp_height_windowed != Screen.height)
            {
                temp_width_windowed = Screen.width;
                temp_height_windowed = Screen.height;
            }
        }
        */
        /*
        if (!isFullScreen) // 창모드이면
        {
            float ratio = temp_width_windowed / temp_height_windowed;

            if (ratio > 1.7777f) // 가로로 길다
            {
                temp_width_windowed = Screen.width;
                temp_height_windowed = (int)(temp_width_windowed*0.5625f);
            }
            else // 세로로 길다
            {
                temp_height_windowed = Screen.height;
                temp_width_windowed = (int)(temp_height_windowed*1.7777f);
            }

            Screen.SetResolution(temp_width_windowed, temp_height_windowed, FullScreenMode.Windowed);
        }
        */
    }

    // 현재 창 모드에 따라 UI 세팅
    public void Set_UI()
    {
        if (m_resolution == eResolution.fullscreen)
        {
            tmp.text = "[ 전체 화면 모드 ]" + "\n" + "(클릭하여 변경)";
        }
        else
        {
            switch (m_resolution)
            {
                case eResolution.big:
                    tmp.text = "[ 3840 * 2190px ]";
                    break;
                case eResolution.midium:
                    tmp.text = "[ 1920 * 1080px ]";
                    break;
                case eResolution.small:
                    tmp.text = "[ 1280 * 720px ]";
                    break;
                case eResolution.low:
                    tmp.text = "[ 800 * 450px ]";
                    break;
                default:
                    tmp.text = "[ 오류 발생. 다시 실행해주세요 ]";
                    break;
            }
        }
    }

    public void Set_Resolution()
    {
        switch (m_resolution)
        {
            case eResolution.big:
                Screen.SetResolution(3840, 2190, FullScreenMode.Windowed);
                break;
            case eResolution.midium:
                Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
                break;
            case eResolution.small:
                Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
                break;
            case eResolution.low:
                Screen.SetResolution(800, 450, FullScreenMode.Windowed);
                break;
            default:
                Screen.SetResolution(3840, 2190, FullScreenMode.FullScreenWindow);
                break;
        }
        /*
        if (isFullScreen)
        {
            // 저장된 임시 해상도가 있다면 해당 해상도로 창모드를 엶
            // 없으면 기본 해상도 (3840*2190)
            if (temp_width_windowed == 3840 && temp_height_windowed == 2190)
            {
                Screen.SetResolution(3840, 2190, FullScreenMode.Windowed);
            }
            else
            {
                Screen.SetResolution(temp_width_windowed, temp_height_windowed, FullScreenMode.Windowed);
            }
        }
        else
        {
            Screen.SetResolution(3840, 2190, FullScreenMode.FullScreenWindow);
        }
        */
    }

    public void Toggle()
    {
        int index = (int)m_resolution;
        if (index < 4)
        {
            index++;
        }
        else // index == 4이면
        {
            index = 0;
        }

        m_resolution = (eResolution)index;
        Control_data(true);

        // isFullScreen = !isFullScreen;
        Set_Resolution();
        Set_UI();
        SoundManager.Instance.PlaySFX(SFX.Setting);
    }
}
