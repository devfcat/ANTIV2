using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;


public class Palette : MonoBehaviour
{
    public int id;

    public void Selected()
    {
        MapEditor.Instance.m_block = id;
    }

    public void Update()
    {
        if (MapEditor.Instance.m_block == id)
        {
            this.gameObject.GetComponent<Image>().color = Color.green;
        }
        else
        {
            this.gameObject.GetComponent<Image>().color = Color.white;
        }
    }
}
