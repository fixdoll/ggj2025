using UnityEngine;

public class CollectibleSystem : MonoBehaviour
{
    public GameObject _collectible;
    private int _score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.CompareTag("Collectible"))
       {

        Debug.Log("Collected" + collider2D.name);
        Destroy(collider2D.gameObject);
        _score ++;
        Debug.Log("New score is" + _score);



       } 

        


    }
   
}
