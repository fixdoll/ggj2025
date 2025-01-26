using UnityEngine;

public class ButtonObject : MonoBehaviour
{
    public DoorObject DoorToOpen;
    public Animator animator;


    public void PressButton()
    {
        
        animator.Play("button_anim");
        DoorToOpen.OpenDoor();
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.CompareTag("Player"))
        {
            Debug.Log("Player detected");
            PressButton();
        }
        
    }
}
