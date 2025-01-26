using UnityEngine;

public class Target : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameFlowController.Instance.FinishLevel();
        }
    }
}
