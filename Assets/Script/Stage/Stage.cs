using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    [Header("스테이지 애니메이터")] 
    public Animator animator;
    [Header("P와 V의 좌표")]
    public RectTransform P;
    public RectTransform V;

    public float p_x, p_y;
    public float v_x, v_y;

    public void Init_Shaking()
    {
        animator.SetInteger("Mod", 0);
    }

    public void Shaking_Stage()
    {
        p_x = P.anchoredPosition.x;
        p_y = P.anchoredPosition.y;
        v_x = V.anchoredPosition.x;
        v_y = V.anchoredPosition.y;

        float delta_x = Math.Abs(p_x - v_x);
        float delta_y = Math.Abs(p_y - v_y);
        
        if (delta_x > delta_y) // 가로 간격이 더 큼
        {
            if (p_x > v_x)
            {
                /*************
                    00P
                    V00
                    000
                **************/
                if (StageManager.Instance.character == Character.P)
                {
                    animator.SetInteger("Mod", 1);
                }
                else
                {
                    animator.SetInteger("Mod", 2);
                }
            }
            else
            {
                /*************
                    00v
                    000
                    0p0
                **************/
                if (StageManager.Instance.character == Character.P)
                {
                    animator.SetInteger("Mod", 2);
                }
                else
                {
                    animator.SetInteger("Mod", 1);
                }
            }

        }
        else if (delta_x <= delta_y) // 세로 간격이 더 큼
        {
            if (p_y > v_y)
            {
                /*************
                    00p
                    000
                    0v0
                **************/
                if (StageManager.Instance.character == Character.P)
                {
                    animator.SetInteger("Mod", 4);
                }
                else
                {
                    animator.SetInteger("Mod", 3);
                }
            }
            else // 세로 간격이 더 큼
            {
                /*************
                    00V
                    000
                    0P0
                **************/
                if (StageManager.Instance.character == Character.P)
                {
                    animator.SetInteger("Mod", 3);
                }
                else
                {
                    animator.SetInteger("Mod", 4);
                }
            }
        }
    }
}
