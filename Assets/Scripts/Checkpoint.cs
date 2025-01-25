using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int CheckpointId;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameplayController.Instance.UpdateCheckpoint(CheckpointId);
            Debug.LogWarning("[DEBUG] Checkpoint #" + CheckpointId + " get");
        }
    }
}
