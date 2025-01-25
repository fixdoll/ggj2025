using UnityEngine;

public class SpikeBehaviour : MonoBehaviour
{
    private PlayerController playerController;
    private GameplayController gameplayController;
    private Checkpoint currentCheckpoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameplayController = FindFirstObjectByType<GameplayController>();
        playerController = FindFirstObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            currentCheckpoint = gameplayController.Checkpoints[gameplayController.Checkpoints.Count - 1];
            playerController.MoveToPosition(currentCheckpoint.transform.position);


        }


    }
}
