using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Move_P : MonoBehaviour
{
    [Header("좌표")]
    public float v_x; // v의 좌표
    public float v_y; // v의 좌표

    [Header("애니메이션 모드")]
    public Animator animator;

    [Header("중요 컴포넌트")]
    public Transform Body;
    public GameObject Arrow;

    public void Init()
    {
        animator.SetInteger("Mod", 0);
    }

    public void Get_position()
    {
        v_x = Body.GetComponent<RectTransform>().anchoredPosition.x;
        v_y = Body.GetComponent<RectTransform>().anchoredPosition.y;
    }

    public void Enable_Arrow(bool b)
    {
        if (b)
        {
            Arrow.SetActive(true);
            Get_position();
            Arrow.GetComponent<RectTransform>().anchoredPosition = new Vector2(v_x, v_y + 70f);
        }
        else
        {
            Arrow.SetActive(false);
        }
    }


    void FixedUpdate() //일정 시간마다 계속 실행.
    {
        Character ch = StageManager.Instance.character;
        if (ch == Character.P)  
        {
            Enable_Arrow(true);

            animator.SetInteger("Mod", StageManager.Instance.animation_mod);
            float m_speed_x = Body.GetComponent<RectTransform>().anchoredPosition.x;
            float m_speed_y = Body.GetComponent<RectTransform>().anchoredPosition.y;
            Body.GetComponent<RectTransform>().anchoredPosition 
                = new Vector3(m_speed_x + StageManager.Instance.vx, m_speed_y + StageManager.Instance.vy, 0);
            Body.transform.Rotate(0,0, StageManager.Instance.rotate_num);
        }
        else
        {
            Enable_Arrow(false);
        }
    }
}