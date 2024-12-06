using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AlienController : MonoBehaviour
{
    public enum CharacterState { Idle, Walk, Jump }

    private Checkpoint checkpoint;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask groundLayer; 
    [SerializeField] private LayerMask sprinkleLayer; 
    [SerializeField] private BoxCollider2D boxCollider;
    private CharacterState characterState;

    public Action<int> OnCoinCollected;

    private int coinsCollected = 0;

    private Rigidbody2D rb;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

        if (IsCollidingWith(sprinkleLayer))
        {
            transform.position = checkpoint.GetSpawnPoint().position;
        }
        // Movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        bool isGrounded = IsCollidingWith(groundLayer);

        if(rb.velocity.x !=0 && isGrounded)
        {
            characterState = CharacterState.Walk;
            spriteRenderer.flipX = rb.velocity.x < 0;

            // if(rb.velocity.x < 0)
            // {
            //     spriteRenderer.flipX = true;
            // }
            // else
            // {
            //     spriteRenderer.flipX = false;
            // }
        }
        else
        {
            characterState = CharacterState.Idle;
        }

        // var newState = rb.velocity.x !=0 ? CharacterState.Walk : CharacterState.Idle;

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            characterState = CharacterState.Jump;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        animator.SetInteger("state", (int)characterState);
    }

    private bool IsCollidingWith(LayerMask mask)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, boxCollider.bounds.extents.y + 0.1f, mask);
        return hit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin")) 
        {
            coinsCollected++;
            OnCoinCollected?.Invoke(coinsCollected);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Checkpoint")) 
        {   if (checkpoint != null)
                {
                    checkpoint.SetCheckpointState(false);    
                }
            checkpoint = other.GetComponent<Checkpoint>();
            checkpoint.SetCheckpointState(true);

        }
        
    }


    private void OnDrawGizmos()
    {
        // Visualize the ground detection ray in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * (boxCollider.bounds.extents.y + 0.1f));
    }
}
