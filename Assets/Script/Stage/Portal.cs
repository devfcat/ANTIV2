using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Animator animator;
    public float touchTime = 0;

    public GameObject P;
    public GameObject V;

    public bool isP;
    public bool isV;

    public void Start()
    {
        Init();
    }

    public void Init()
    {
        touchTime = 0;
        animator.SetBool("IsBooting", false);
    }

    public void Update()
    {
        if (isP && isV)
        {
            touchTime += Time.deltaTime;
            animator.SetBool("IsBooting", true);
            if (touchTime > 2.5f)
            {
                int next = ((int)GameManager.Instance.m_State)+1;
                GameManager.Instance.SetState((eState)next);
                Debug.Log("다음 스테이지는 " + next);
                SoundManager.Instance.PlaySFX(SFX.GameClear);
                P.gameObject.SetActive(false);
                V.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
            }
        }
        else
        {
            touchTime = 0;
            animator.SetBool("IsBooting", false);
        }
    }

}
