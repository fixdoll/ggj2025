using UnityEngine;

public class ButtonObject : MonoBehaviour
{
    public DoorObject DoorToOpen;

    public GameObject _propeller;

    public Animator animator;

    private ParticleSystem _propellerParticle;

    public bool isPropellerButton;


    public void PressButton()
    {
        
        animator.Play("button_anim");

        if(isPropellerButton)
        {
        _propeller.GetComponent<Collider2D>().enabled = false;
        _propellerParticle = _propeller.GetComponentInChildren<ParticleSystem>();
        _propellerParticle.Play(_propellerParticle);

        } else
        {
        DoorToOpen.OpenDoor();

        }



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
