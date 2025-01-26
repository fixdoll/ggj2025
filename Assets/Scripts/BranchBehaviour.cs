using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BranchBehaviour : MonoBehaviour
{

    private Rigidbody2D rb;
    [SerializeField] private PlayerController playerController;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && playerController.sizeState == SizeState.Large)
        {
            StartCoroutine(Destruction());
        }
    }

    IEnumerator Destruction()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;

        yield return new WaitForSeconds(2);

        Destroy(gameObject);




    }
}
