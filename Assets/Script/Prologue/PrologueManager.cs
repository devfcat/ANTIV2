using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class PrologueManager : MonoBehaviour
{
    public GameObject BTN_next;
    public GameObject message_box;

    [Header("로드된 대사")] public List<Dialogue> loaded_dialogues;
    public int length_dialogue;
    public bool isLoading;


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
    }

     public void Load_Dialogue()
    {
        StartCoroutine(Coroutine_LoadDialogue());
    }

    IEnumerator Coroutine_LoadDialogue()
    {
        isLoading = true;
        Clear_Dialogue();

        string filepath = "";
        filepath = "Assets/Story/" + "KR/" + "Prologue/prologue.json";

        string jsonContents = File.ReadAllText(filepath);

        Story content = JsonUtility.FromJson<Story>(jsonContents);

        // 어떤 스테이지의 대사집을 불러옴
        length_dialogue = content.story[0].dialogue.Count;
        for (int i = 0; i < length_dialogue; i++)
        {
            loaded_dialogues.Add(null);
        }
        for (int i = 0; i < length_dialogue; i++)
        {
            loaded_dialogues[i] =  content.story[0].dialogue[i] as Dialogue;
        }

        isLoading = false;

        yield return null;
    }

    public void Clear_Dialogue()
    {
        try
        {
            loaded_dialogues.Clear();
            length_dialogue = 0;
        }
        catch {}
    }

    // 아래 메서드를 호출하여 스테이지 초기의 대사를 로드하고 메세지 박스를 띄움
    public void Make_MsgBox()
    {
        isLoading = true;
        Load_Dialogue();

        for (int i = 0; i < length_dialogue; i++)
        {
            GameObject instance = Instantiate(message_box, this.transform);

            // 대사 등 변경(UI 작업)
            instance.transform.GetChild(3).GetComponent<TypeEffect>().m_Message = loaded_dialogues[i].content;
            
            string name = loaded_dialogues[i].speaker;
            int activeName = -1;
            if (name == "V")
            {
                activeName = 1;
            }
            else if (name == "P")
            {
                activeName = 2;
            }
            else if (name == "S")
            {
                activeName = 0;
            }
            
            for (int j = 0; j < 3; j++)
            {
                if (j != activeName)
                {
                    instance.transform.GetChild(j).gameObject.SetActive(false);
                }
                else instance.transform.GetChild(j).gameObject.SetActive(true);
            }
        }

        isLoading = false;
    }

    public void Click_Setting()
    {
        GameManager.Instance.Control_Setting();
    }

}
