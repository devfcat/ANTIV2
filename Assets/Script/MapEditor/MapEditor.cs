using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapEditor : MonoBehaviour
{
    private const int value_block = 10;
    public bool isRendered = false;
    public int m_block; // 현재 선택된 블록 아이디
    public List<GameObject> buttons; // 맵 에디터의 블록
    public List<Sprite> blocks; // 아이디 순서의 블록 이미지

    public TMP_InputField inputFields;

    public List<GameObject> prefabs;
    public GameObject screen; // 렌더할 스크린
    public RectTransform screenRect;
    public float block_x; // 스크린 블록 한칸 x
    public float block_y; // 스크린 블록 한칸 y
    

    public int[,] data = new int[,]
    {
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
    };

    private static MapEditor _instance;
    public static MapEditor Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(MapEditor)) as MapEditor;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    public void Start()
    {
        m_block = 0; // 지우개, 기본 배경 아이디

        Erasing();

        Debug.Log("초기화 완료");
    }

    // 팔레트 선택후 그리기
    public void Painting(GameObject target)
    {
        int block_id = 0;
        for (int k = 0; k < buttons.Count; k++)
        {
            if (target == buttons[k])
            {
                block_id = k;
            }
        }
        int selected_id = m_block;
        buttons[block_id].GetComponent<Image>().sprite = blocks[selected_id];

        int i = block_id % (2*value_block);
        int j = (int)((block_id - i) / (2*value_block));
        data[j,i] = selected_id;

        SaveData();
    }

    public void RePainting(int id, int selected_id)
    {
        int block_id = id;
        buttons[block_id].GetComponent<Image>().sprite = blocks[selected_id];

        int i = block_id % (2*value_block);
        int j = (int)((block_id - i) / (2*value_block));

        data[j, i] = selected_id;
    }

    // 데이터 적용
    public void WriteAndRender()
    {
        string text = inputFields.text;
        List<int> input_data = new List<int>();
        for (int i=0; i < text.Length; i++)
        {
            input_data.Add(text[i]-48);
        }

        for (int i=0; i<input_data.Count; i++)
        {
            int whatBlock = input_data[i];
            Debug.Log(whatBlock);
            RePainting(i, input_data[i]);
        }
        SaveData();

    }

    public void SaveData()
    {
        string dt = "";
        for (int j = 0; j < value_block; j++)
        {
            for (int i = 0; i < (2*value_block); i++)
            {
                int num = data[j,i];
                string add = num.ToString();
                dt = dt + add;
            }
        }
        inputFields.text = dt;
        Debug.Log(dt);
    }

    public void Rendering() 
    { 
        if (isRendered)
        {
            return;
        }

        screen.transform.GetComponent<Image>().color = new Color(0,0,0,255);

        block_x = screenRect.sizeDelta.x/(2*value_block);
        block_y = screenRect.sizeDelta.y/value_block;


        // 렌더링 안되는 버그 고치기
        for (int i = 0; i < (2*value_block); i++)
        {
            for (int j = 0; j < value_block; j++)
            {
                int id = data[j,i];
                if (id == 0)
                {
                    
                }
                else
                {
                    float pos_x = (i-value_block)*block_x;
                    float pos_y = ((value_block/2)-j)*block_y;
                    Vector2 pos = new Vector2(pos_x, pos_y);
                    
                    GameObject target = prefabs[id-1];
                    GameObject instance_Object = Instantiate(target, screen.transform);
                    instance_Object.SetActive(false);
                    instance_Object.GetComponent<RectTransform>().anchoredPosition = pos;
                    instance_Object.SetActive(true);
                }
            }
        }

        Debug.Log("렌더링됨");

        isRendered = true;
    }

    public void Erasing() 
    {
        screen.transform.GetComponent<Image>().color = new Color(0,0,0,0);

        for (int i = 0; i < (2*value_block); i++)
        {
            for (int j = 0; j < value_block; j++)
            {
                data[j,i] = 0;
                buttons[j*(2*value_block)+i].GetComponent<Image>().sprite = blocks[0];
            }
        }

        foreach (Transform child in screen.transform)
        {
            Destroy(child.gameObject);
        }

        isRendered = false;
    }
}
