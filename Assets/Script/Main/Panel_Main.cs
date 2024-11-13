using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Main : MonoBehaviour
{
    public Slider BgmSlider;
    public Slider SESlider;

    public GameObject BTN_LoadStart;
    
    public void Awake()
    {
        Set_UI();
        Set_UI_Slider();
    }

    public void Update()
    {
        if (BgmSlider.value != SoundManager.Instance.BgmVolume)
        {
            Set_UI_Slider();
        }
        else if (SESlider.value != SoundManager.Instance.SfxVolume)
        {
            Set_UI_Slider();
        }
    }

    public void Set_UI()
    {
        if (GameManager.Instance.m_State == eState.Main)
        {
            GameManager.Instance.Load();
            if (GameManager.Instance.saved_stage <= 1)
            {
                BTN_LoadStart.SetActive(false);
            }
            else
            {
                BTN_LoadStart.SetActive(true);
            }
        }
    }

    public void Set_UI_Slider()
    {
        BgmSlider.value = SoundManager.Instance.BgmVolume;
        SESlider.value = SoundManager.Instance.SfxVolume;
    }

    public void Game_New_Start()
    {
        SoundManager.Instance.PlaySFX(SFX.Setting);
        GameManager.Instance.SetState(eState.Prologue);
    }

    public void Game_Load_Start()
    {
        SoundManager.Instance.PlaySFX(SFX.Setting);

        GameManager.Instance.Load();
        if (GameManager.Instance.saved_stage <= 2)
        {
            GameManager.Instance.SetState(eState.Prologue);
        }
        else
        {
            GameManager.Instance.SetState((eState)GameManager.Instance.saved_stage);
        }
    }

    public void Game_ReLoad()
    {
        SoundManager.Instance.PlaySFX(SFX.Setting);
        eState now_Scene = GameManager.Instance.m_State;
        GameManager.Instance.SetState(now_Scene);
    }

    public void Save_and_Home()
    {
        GameManager.Instance.Save();
        SoundManager.Instance.PlaySFX(SFX.Setting);
        GameManager.Instance.SetState(eState.Main);
    }

    public void BTN_close()
    {
        GameManager.Instance.Control_Setting();
    }

    public void Report_Bug()
    {
        string mailto = "devfcat@gmail.com";
        string subject = EscapeURL("[Anti V : 크리스마스 버그 전쟁] 버그 제보 / 문의사항");
        string body = EscapeURL("내용 : " + "\n");
        Application.OpenURL("mailto:" + mailto + "?subject=" + subject + "&body=" + body);
    }

    private string EscapeURL(string url) 
    {
        return WWW.EscapeURL(url).Replace("+", "%20"); 
    }

    public void Quit()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}
