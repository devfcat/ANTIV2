using UnityEngine;
using TMPro;

/// <summary>
/// 창모드 / 전체화면 모드 전환 기능
/// </summary>
public class Resolution_Changer : MonoBehaviour
{
    public bool isFullScreen;
    public TextMeshProUGUI tmp;

    public int temp_width_windowed;
    public int temp_height_windowed;

    public void Awake()
    {
        Init();
    }

    public void Init()
    {
        isFullScreen = Screen.fullScreen;

        Set_UI();
    }

    public void Update()
    {
        if (!isFullScreen) // 창모드이면
        {
            temp_width_windowed = Screen.width;
            temp_height_windowed = Screen.height;
        }
    }

    public void Set_UI()
    {
        if (isFullScreen)
        {
            tmp.text = "[ 창 모드로 전환 ]";
        }
        else
        {
            tmp.text = "[ 전체 화면 모드로 전환 ]";
        }
    }

    public void Set_Resolution()
    {
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
    }

    public void Toggle()
    {
        Set_Resolution();
        isFullScreen = !isFullScreen;
        Set_UI();
        SoundManager.Instance.PlaySFX(SFX.Setting);
    }
}
