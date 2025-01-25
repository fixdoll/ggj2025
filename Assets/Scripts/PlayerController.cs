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
    public float PushOffset = 0.02f;
    public float TerminalVelocity = 1f;

    private bool onAir = false;
    private SizeState sizeState;

    private void Start()
    {
        float idealDrag = Speed / TerminalVelocity;
        ThisRigidBody2D.linearDamping = idealDrag / (idealDrag * Time.fixedDeltaTime + 1);

        sizeState = SizeState.Small;
    }

    private void Update()
    {
        if (Input.GetKeyDown(SizeChangeKey))
        {
            ChangeSize();
        }
    }

    private void FixedUpdate()
    {
        CheckOnAir();
        float direction = Input.GetAxis("Horizontal");
        if (!onAir)
        {
            ThisRigidBody2D.linearVelocity = Vector2.zero;
            if (Input.GetKey(KeyCode.RightArrow))
            {
                ThisRigidBody2D.AddForceY(JumpForce, ForceMode2D.Impulse);
                ThisRigidBody2D.AddForceX(Speed * 0.5f, ForceMode2D.Impulse);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                ThisRigidBody2D.AddForceY(JumpForce, ForceMode2D.Impulse);
                ThisRigidBody2D.AddForceX(Speed * -0.5f, ForceMode2D.Impulse);
            }
        }
        else
        {
            ThisRigidBody2D.AddForceX(Speed * direction * PushOffset, ForceMode2D.Impulse);
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
        ThisRigidBody2D.freezeRotation = isSmall;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            Debug.LogWarning("[DEBUG] Target hit");
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
