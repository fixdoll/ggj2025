using UnityEngine;

public class DoorObject : MonoBehaviour
{
    private Animator animator;
    private Collider2D collider2d;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        collider2d = GetComponent<Collider2D>();
    }

    public void OpenDoor()
    {
        animator.Play("door_open");
        collider2d.enabled = false;
    }
}
