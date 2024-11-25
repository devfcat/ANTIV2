using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;


[Serializable]
public class Story
{
    [SerializeField] public List<StageCell> story;
}


[Serializable]
public class StageCell
{
    [SerializeField] public int stage;
    [SerializeField] public List<Dialogue> dialogue;
}


[Serializable]
public class Dialogue
{
    [SerializeField] public string speaker;
    [SerializeField] public string content;
}

/// <summary>
/// 대사집 파일에서 대사를 가져와 적용
/// </summary>
public class DialogueManager : MonoBehaviour
{
    [Header("스토리모드인가")] public bool isStoryMode;
    public bool isLoading;
    [Header("로드된 대사")] public List<Dialogue> loaded_dialogues;
    public int length_dialogue;
    [Header("현재 스테이지")] public int m_stage;

    public GameObject message_box;

    private static DialogueManager instance;
    public static DialogueManager Instance
    {
        get {
            if(!instance)
            {
                instance = FindObjectOfType(typeof(DialogueManager)) as DialogueManager;

                if (instance == null)
                    Debug.Log("no Singleton obj");
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Load_Dialogue()
    {
        Clear_Dialogue();

        if (isStoryMode)
        {
            StartCoroutine(Coroutine_LoadDialogue(m_stage));
        }
        else
        {
            StartCoroutine(Coroutine_LoadDialogue());
        }
    }

    IEnumerator Coroutine_LoadDialogue(int stage_index = 0)
    {
        isLoading = true;

        Clear_Dialogue();

        string filepath = "";

        if (isStoryMode)
        {
            filepath = "Assets/Story/" + "KR/" + "Story/story" + ".json";
        }
        else
        {
            filepath = "Assets/Story/" + "KR/" + "Prologue/prologue" + ".json";
        }

        string jsonContents = File.ReadAllText(filepath);
        // Debug.Log(jsonContents);

        Story content = JsonUtility.FromJson<Story>(jsonContents);

        // 어떤 스테이지의 대사집을 불러옴
        length_dialogue = content.story[stage_index].dialogue.Count;
        for (int i = 0; i < length_dialogue; i++)
        {
            loaded_dialogues.Add(null);
        }
        for (int i = 0; i < length_dialogue; i++)
        {
            loaded_dialogues[i] =  content.story[stage_index].dialogue[i] as Dialogue;
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
    public void Make_MsgBox(bool b = true)
    {
        isLoading = true;

        m_stage = (int)GameManager.Instance.m_State - 3;
        isStoryMode = b;
        Load_Dialogue();

        for (int i = 0; i < length_dialogue; i++)
        {
            GameObject instance = Instantiate(message_box, this.transform);

            // 대사 등 변경(UI 작업)
            instance.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = loaded_dialogues[i].content;
            
            string name = loaded_dialogues[i].speaker;
            int activeName = 0;
            if (name == "V")
            {
                activeName = 1;
            }
            else if (name == "P")
            {
                activeName = 2;
            }
            else
            {
                activeName = -1;
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
}