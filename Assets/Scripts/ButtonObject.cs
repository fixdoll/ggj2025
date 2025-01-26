using UnityEngine;

public class ButtonObject : MonoBehaviour
{
    public DoorObject DoorToOpen;
    public Animator animator;

    public void PressButton()
    {
        //animator.Play("PressAnimation");
        DoorToOpen.OpenDoor();
    }
}
