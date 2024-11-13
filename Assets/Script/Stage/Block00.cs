using UnityEngine;

public class Block00 : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.parent.gameObject.name == "P" || col.transform.parent.gameObject.name == "V")
        {
            SoundManager.Instance.PlaySFX(SFX.Block00);
        }
    }
}
