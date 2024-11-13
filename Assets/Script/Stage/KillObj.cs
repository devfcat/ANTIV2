using UnityEngine;

public class KillObj : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.parent.gameObject.name == "P" || col.transform.parent.gameObject.name == "V")
        {
            StageManager.Instance.isGameOver = true;
            SoundManager.Instance.PlaySFX(SFX.Error);
            GameManager.Instance.SetState(GameManager.Instance.m_State);
        }
    }
}
