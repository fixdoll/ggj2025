using System.Collections;
using UnityEngine;

public class BubbleObject : MonoBehaviour
{
    public int RespawnTime = 5;

    [Header("References")]
    public SpriteRenderer ThisSpriteRenderer;
    public Collider2D ThisCollider2D;

    
    public void Pop()
    {
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        ThisSpriteRenderer.enabled = false;
        ThisCollider2D.enabled = false;
        yield return new WaitForSeconds(RespawnTime);
        ThisSpriteRenderer.enabled = true;
        ThisCollider2D.enabled = true;
        yield break;
    }
}
