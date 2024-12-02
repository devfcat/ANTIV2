using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypeEffect : MonoBehaviour
{
    public TextMeshProUGUI m_TypingText;
    public string m_Message;
    public float m_Speed = 0.1f;

    public bool isLoadingEnd = false;

    void Update()
    {
        string input = m_Message;
        if (input != "" && isLoadingEnd == false)
        {
            isLoadingEnd = true;
            Init();
        }
        else if (input == "")
        {
            isLoadingEnd = false;
        }
    }

    public void Init()
    {
        //m_Message = this.transform.GetComponent<TextMeshProUGUI>().text;
        this.transform.GetComponent<TextMeshProUGUI>().text = "";
        StartCoroutine(Typing(m_TypingText, m_Message, m_Speed));
    }

    IEnumerator Typing(TextMeshProUGUI typingText, string message, float speed)
    {
        for (int i = 0; i < message.Length; i++)
        {
            typingText.text = message.Substring(0, i + 1);
            yield return new WaitForSeconds(speed);
        }
    }
}
