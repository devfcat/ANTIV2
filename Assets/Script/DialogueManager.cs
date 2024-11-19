using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class Story
{
    [SerializeField] public List<StageCell> stageCell;
}

public class StageCell
{
    [SerializeField] public int stage;
    [SerializeField] public List<Dialogue> dialogue;
}

public class Dialogue
{
    [SerializeField] public string speaker;
    [SerializeField] public string content;
}

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
            Coroutine_LoadDialogue(m_stage);
        }
        else
        {
            Coroutine_LoadDialogue();
        }
    }

    IEnumerator Coroutine_LoadDialogue(int stage_index = 0)
    {
        isLoading = true;

        Clear_DialogueData();

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

        Story content = JsonUtility.FromJson<Story>(jsonContents);

        // 어떤 스테이지의 대사집을 불러옴
        length_dialogue = content.stageCell[stage_index].dialogue.Count;
        for (int i = 0; i < length_dialogue; i++)
        {
            loaded_dialogues.Add(null);
        }
        for (int i = 0; i < length_dialogue; i++)
        {
            loaded_dialogues[i] =  content.stageCell[stage_index].dialogue[i] as Dialogues;
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
    public void Make_MsgBox(bool b = false, int stage = 0)
    {
        isLoading = true;

        m_stage = stage;
        isStoryMode = b;
        Load_Dialogue(stage);

        for (int i = 0; i < length_dialogue; i++)
        {
            gameObject instance = Instiate(message_box, this.transform);
            // 대사 등 변경(UI 작업)
        }

        isLoading = false;
    }
}