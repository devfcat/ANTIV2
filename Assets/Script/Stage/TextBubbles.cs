using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
public class TextBubbles : MonoBehaviour
{
    [Header("패널")]
    [ReadOnly] public List<GameObject> Animation_Panels;

    private int panel_Index = 0;

    public GameObject Start_Shell; // 처음 시작 시 대사를 다 봐야 사라지는 오브젝트

    public GameObject BTN_next;
    public string sceneIndexName;

    public void Start()
    {
        Init();
    }

    public void Init()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Animation_Panels.Add(this.transform.GetChild(i).gameObject);
        }

        sceneIndexName = ((int)GameManager.Instance.m_State).ToString();

        if (Check_Read(sceneIndexName))
        {
            Debug.Log("이미 플레이한 스테이지");
            panel_Index = Animation_Panels.Count-1;
            Set_UI(false);

            this.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("새로 접근한 스테이지");
            panel_Index = 0;
            Set_UI(true);
        }
    }

    public void Set_UI(bool b)
    {
        Start_Shell.SetActive(b);
        BTN_next.SetActive(b);

        if (!b)
        {
            for (int i = 0; i < Animation_Panels.Count; i++)
            {
                Animation_Panels[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < Animation_Panels.Count; i++)
            {
                if (i == 0)
                {
                    Animation_Panels[i].SetActive(true);
                }
                else Animation_Panels[i].SetActive(false);
            }
        }
    }

    public bool Check_Read(string s)
    {
        bool isRead = PlayerPrefs.GetInt(s) == 1 ? true : false;
        return isRead;
    }

    public void Save_Read(string s)
    {
        PlayerPrefs.SetInt(s, 1);
    }

    public void Paging(bool isNext)
    {
        SoundManager.Instance.PlaySFX(SFX.click);

        if (isNext)
        {
            if (panel_Index == Animation_Panels.Count-1)
            {
                Set_UI(false);
                Save_Read(sceneIndexName);
            }
            else 
            {
                panel_Index ++;
                for (int i = 0; i < Animation_Panels.Count; i++)
                {
                    if (i == panel_Index)
                    {
                        Animation_Panels[i].SetActive(true);
                    }
                    else Animation_Panels[i].SetActive(false);
                }
            }
        
        }
    }
}
