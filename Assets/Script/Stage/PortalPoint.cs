using UnityEngine;

public class PortalPoint : MonoBehaviour
{
    public Portal portal;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.parent.gameObject.name == "P" || col.transform.parent.gameObject.name == "V")
        {
            SoundManager.Instance.PlaySFX(SFX.PP);
        }
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.transform.parent.gameObject.name == "P")
        {
            portal.isP = true;
        }
        else if (col.transform.parent.gameObject.name == "V")
        {
            portal.isV = true;
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.parent.gameObject.name == "P")
        {
            portal.isP = false;
        }
        else if (col.transform.parent.gameObject.name == "V")
        {
            portal.isV = false;
        }
    }
}
