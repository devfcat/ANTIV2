using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;

public class PrologueManager : MonoBehaviour
{
    [Header("패널")]
    public RectTransform credits;

    public GameObject anime_credit;

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
        
        //panel_Index = 0;
    }

    public void Paging()
    {
        SoundManager.Instance.PlaySFX(SFX.click);

        float pos_y = credits.anchoredPosition.y;
        if (pos_y != 0)
        {
            anime_credit.GetComponent<Animator>().enabled = false;
            credits.anchoredPosition = Vector2.zero;
        }
        else
        {
            GameManager.Instance.SetState(eState.Stage01);
        }

        /*
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
        */
    }

    public void Click_Setting()
    {
        GameManager.Instance.Control_Setting();
    }

}
