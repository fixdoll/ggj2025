using System;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D ThisRigidBody2D;
    public KeyCode SizeChangeKey;
    public SpriteRenderer SmallSprite;
    public Collider2D SmallCollider;
    public SpriteRenderer LargeSprite;
    public Collider2D LargeCollider;
    

    [Header("Movement Params")]
    public float Speed;
    public float JumpForce;
    public float JumpPushOffset = 0.02f;
    public float RollPushOffset = 0.02f;
    public float SwimPushOffset = 0.2f;
    public float TerminalVelocity = 1f;
    public float JumpMultiplier = 2f;

    private bool onAir = false;
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
    }

    private void FixedUpdate()
    {
        float directionX = Input.GetAxis("Horizontal");
        float directionY = Input.GetAxis("Vertical");

        if (!underwater)
        {
            if (sizeState == SizeState.Small)
            {
                CheckOnAir();
                if (!onAir)
                {
                    ThisRigidBody2D.linearVelocity = Vector2.zero;
                    var addJump = Input.GetKey(KeyCode.UpArrow);
                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        ThisRigidBody2D.AddForceY(JumpForce * (addJump ? JumpMultiplier : 1f), ForceMode2D.Impulse);
                        ThisRigidBody2D.AddForceX(Speed * 0.5f, ForceMode2D.Impulse);
                    }
                    else if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        ThisRigidBody2D.AddForceY(JumpForce * (addJump ? JumpMultiplier : 1), ForceMode2D.Impulse);
                        ThisRigidBody2D.AddForceX(Speed * -0.5f, ForceMode2D.Impulse);
                    }
                }
                else
                {
                    ThisRigidBody2D.AddForceX(Speed * directionX * JumpPushOffset, ForceMode2D.Impulse);
                }
            }
            else if (sizeState == SizeState.Large)
            {
                ThisRigidBody2D.AddForceX(Speed * directionX * RollPushOffset, ForceMode2D.Impulse);
            }
        }
        else
        {
            ThisRigidBody2D.AddForce(new Vector2(Speed * directionX * SwimPushOffset, Speed * directionY * SwimPushOffset));
        }        
    }

    private void CheckOnAir()
    {
        onAir = Mathf.Abs(ThisRigidBody2D.linearVelocityY) > 0.01f;
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
        SmallSprite.enabled = !isSmall;
        SmallCollider.enabled = !isSmall;
        LargeSprite.enabled = isSmall;
        LargeCollider.enabled = isSmall;
        ThisRigidBody2D.freezeRotation = !isSmall;
    }

    void ChangeWater(bool enterWater)
    {
        underwater = enterWater;
        if(enterWater)
        {
            ThisRigidBody2D.gravityScale = sizeState == SizeState.Large ? dynGrav * -4f : 0f;
            ThisRigidBody2D.linearDamping *= 2;
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
        if (collision.otherCollider.CompareTag("Ground"))
        {
            onAir = false;
            ThisRigidBody2D.linearVelocity = Vector2.zero;
        }
    }
}

public enum SizeState
{
    Small,
    Large,
}
