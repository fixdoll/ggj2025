using System;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public KeyCode SizeChangeKey;
    public KeyCode JumpKey;
    public Rigidbody2D ThisRigidBody2D;
    public Collider2D SmallCollider;
    public MeshRenderer Small3D;
    public Collider2D LargeCollider;
    public MeshRenderer Large3D;

    [Header("Movement Params")]
    public float Speed;
    public float JumpForce;
    public float JumpPushOffset = 0.02f;
    public float RollPushOffset = 0.02f;
    public float SwimPushOffset = 0.2f;
    public float TerminalVelocity = 1f;
    public float JumpMultiplier = 2f;

    private bool jumpCounter = false;
    private bool underwater = false;
    private SizeState sizeState;
    private float dynGrav;
    private bool canLarge = false;

    private void Start()
    {
        float idealDrag = Speed / TerminalVelocity;
        ThisRigidBody2D.linearDamping = idealDrag / (idealDrag * Time.fixedDeltaTime + 1);

        sizeState = SizeState.Small;

        dynGrav = ThisRigidBody2D.gravityScale;
    }

    private void Update()
    {
        if (Input.GetKeyDown(SizeChangeKey))
        {
            if(sizeState == SizeState.Large)
                ChangeSize();
            else if (sizeState == SizeState.Small && canLarge)
            {
                canLarge = false;
                ChangeSize();
            }
        }
        if (Input.GetKeyDown(JumpKey) && !jumpCounter && !underwater)
        {
            jumpCounter = true;
            SuperJump();
        }
    }

    private void FixedUpdate()
    {
        float directionX = Input.GetAxis("Horizontal");
        float directionY = Input.GetAxis("Vertical");

        if (!underwater)
        {
            ThisRigidBody2D.AddForceX(
                Speed * directionX * (sizeState == SizeState.Small ? JumpPushOffset : RollPushOffset),
                ForceMode2D.Impulse);
        }
        else
        {
            ThisRigidBody2D.AddForce(new Vector2(Speed * directionX * SwimPushOffset, Speed * directionY * SwimPushOffset));
        }        
    }

    private void Jump()
    {
        ThisRigidBody2D.linearVelocityY = 0f;
        jumpCounter = false;
        ThisRigidBody2D.AddForceY(JumpForce, ForceMode2D.Impulse);
    }

    private void SuperJump()
    {
        ThisRigidBody2D.AddForceY(JumpForce * JumpMultiplier, ForceMode2D.Impulse);
    }

    public void MoveToPosition(Vector3 position)
    {
        ThisRigidBody2D.linearVelocity = Vector2.zero;
        transform.position = position;
    }

    private void ChangeSize()
    {
        bool isSmall = sizeState == SizeState.Small;
        if (isSmall)
        {
            sizeState = SizeState.Large;
        }
        else
        {
            sizeState = SizeState.Small;
        }
        SmallCollider.enabled = !isSmall;
        Small3D.enabled = !isSmall;
        LargeCollider.enabled = isSmall;
        Large3D.enabled = isSmall;
        ThisRigidBody2D.freezeRotation = !isSmall;
        transform.rotation = Quaternion.identity;
    }

    void ChangeWater(bool enterWater)
    {
        underwater = enterWater;
        if(enterWater)
        {
            ThisRigidBody2D.gravityScale = sizeState == SizeState.Large ? dynGrav * -4f : 0f;
            ThisRigidBody2D.linearDamping *= 2;
            jumpCounter = true;
        }
        else
        {
            ThisRigidBody2D.gravityScale = dynGrav;
            ThisRigidBody2D.linearDamping /= 2;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bubble"))
        {
            collision.GetComponent<BubbleObject>().Pop();
            canLarge = true;

        }
        if (collision.CompareTag("Water"))
        {
            ChangeWater(true);
        }
        if (collision.CompareTag("Target"))
        {
            Debug.LogWarning("[DEBUG] Target hit");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            ChangeWater(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") && sizeState == SizeState.Small && !underwater)
        {
            Jump();
        }
    }
}

public enum SizeState
{
    Small,
    Large,
}
