using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bubbles : MonoBehaviour
{
    public List<GameObject> bubbles;

    public void Start()
    {
        StartCoroutine(Bubble());
    }

    IEnumerator Bubble()
    {
        for (int i = 0 ; i < bubbles.Count ; i++)
        {
            bubbles[i].SetActive(true);
            yield return new WaitForSeconds(1.5f);
        }
    }
}
