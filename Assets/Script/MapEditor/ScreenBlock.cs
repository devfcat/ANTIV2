using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScreenBlock : MonoBehaviour
{

    public void Awake()
    {
        this.GetComponent<Button>().onClick.AddListener(this.Clicked);
    }

    public void Clicked()
    {
        MapEditor.Instance.Painting(this.gameObject);
    }
}
