using System;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("0=jump first, 1=directional jump")]
    public int movementTestMode = 0;

    [Header("References")]
    public Rigidbody2D ThisRigidBody2D;

    [Header("Movement Params")]
    public float Speed;
    public float JumpForce;
    public float PushOffset = 0.02f;
    
    private Vector2 velocity;

    private bool onAir = false;

    /*protected void Update()
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
    }*/

    private void Update()
    {
        switch (movementTestMode)
        {
            case 0:
                CheckOnAir();
                if (onAir)
                {
                    velocity.x = Input.GetAxis("Horizontal");
                    transform.position += (Vector3)velocity * Speed * Time.deltaTime;
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        ThisRigidBody2D.AddForceY(JumpForce, ForceMode2D.Impulse);
                    }
                }
                break;
            case 1:
                CheckOnAir();
                float direction = Input.GetAxis("Horizontal");
                if (!onAir)
                {
                    ThisRigidBody2D.linearVelocityX = 0f;
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        ThisRigidBody2D.AddForceY(JumpForce, ForceMode2D.Impulse);
                        ThisRigidBody2D.AddForceX(Speed * 0.5f, ForceMode2D.Impulse);
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        ThisRigidBody2D.AddForceY(JumpForce, ForceMode2D.Impulse);
                        ThisRigidBody2D.AddForceX(Speed * -0.5f, ForceMode2D.Impulse);
                    }
                }
                else
                {
                    ThisRigidBody2D.AddForceX(Speed * direction * PushOffset, ForceMode2D.Impulse);
                }
                break;
            default:
                break;
        }
    }

    private void CheckOnAir()
    {
        onAir = Mathf.Abs(ThisRigidBody2D.linearVelocityY) > 0.02f;
    }
}
