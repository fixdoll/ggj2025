using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Vector3 _startPoint;
    public Vector3 _destination;

    private Vector3 _targetPos;

    public float _moveSpeed;

    private int _direction = -1;


    void Awake()
    {
        _startPoint = transform.position;



    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 target = GetCurrentDirection();

        transform.position = Vector3.MoveTowards(transform.position, target, _moveSpeed * Time.deltaTime);

        float _distance = (target - transform.position).magnitude;

        if(_distance <= 0.1f)
     {
        _direction *= -1;
     }   

    }

    Vector3 GetCurrentDirection()
    {

        if(_direction == -1)
       {
          return _startPoint;  

       } 
        else
       {

        return _destination;
       }


    }
}
