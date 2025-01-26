using System.Collections;
using UnityEngine;

public class BubbleObject : MonoBehaviour
{
    public int RespawnTime = 5;

    [Header("References")]
    public ParticleSystem ThisParticleSystem;
    public Collider2D ThisCollider2D;

    
    public void Pop()
    {
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        ThisParticleSystem.Stop(ThisParticleSystem);
        ThisCollider2D.enabled = false;
        yield return new WaitForSeconds(RespawnTime);
        ThisParticleSystem.Play(ThisParticleSystem);
        ThisCollider2D.enabled = true;
        yield break;
    }
}
