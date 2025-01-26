using UnityEngine;

public class DoorObject : MonoBehaviour
{
    private Animator animator;

    public void OpenDoor()
    {
        animator.Play("door_open");
        
    }
}
