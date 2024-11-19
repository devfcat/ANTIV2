using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;

public class PrologueManager : MonoBehaviour
{
    [Header("패널")]
    public List<GameObject> Animation_Panels;

    public int panel_Index = 0;

    public GameObject BTN_next;
    public GameObject BTN_prev;

    private static PrologueManager _instance;
    public static PrologueManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(PrologueManager)) as PrologueManager;
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
        SoundManager.Instance.PlayBGM(BGM.Stage_Prologue);
        panel_Index = 0;
        Set_PageUI();
    }

    public void Set_PageUI()
    {
        if (panel_Index == 0)
        {
            BTN_prev.SetActive(false);
        }
        else
        {
            BTN_prev.SetActive(true);
        }
    }

    public void Paging(bool isNext)
    {
        SoundManager.Instance.PlaySFX(SFX.click);

        if (isNext)
        {
            if (panel_Index >= Animation_Panels.Count-2)
            {
                GameManager.Instance.SetState(eState.Stage01);
            }
            else panel_Index ++;
        }
        else
        {
            panel_Index --;
        }

        Set_PageUI();

        for (int i = 0; i < Animation_Panels.Count; i++)
        {
            if (i == panel_Index)
            {
                Animation_Panels[i].SetActive(true);
            }
            else
            {
                Animation_Panels[i].SetActive(false);
            }
        }
    }

    public void Click_Setting()
    {
        GameManager.Instance.Control_Setting();
    }

}
