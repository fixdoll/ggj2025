using System;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D ThisRigidBody2D;

    [Header("Movement Params")]
    public float Speed;
    public float JumpForce;
    
    private Vector2 velocity;

    protected void Update()
    {
        CheckVelocity();

        transform.position += (Vector3)velocity * Speed * Time.deltaTime; 
    }

    private void CheckVelocity()
    {
        velocity.x = Input.GetAxis("Horizontal");
        //velocity.y = Input.GetAxis("Vertical");

        if(Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(ThisRigidBody2D.linearVelocityY) < 0.01f)
        {
            ThisRigidBody2D.AddForceY(JumpForce, ForceMode2D.Impulse);
        }
    }
}
