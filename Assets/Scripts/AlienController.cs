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
    [SerializeField] private GameObject sfxBgMusic;

    private int activeLives = 3;
    private CharacterState characterState;

    public Action<int> OnCoinCollected;
    public Action<int> OnActiveLivesChange;

    private int coinsCollected = 0;

    private Rigidbody2D rb;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Instantiate(sfxBgMusic, transform.position, Quaternion.identity);
    }

    private void Update()
    {

        if (IsCollidingWith(sprinkleLayer))
        {
            Respawn();
        }
        // Movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        bool isGrounded = IsCollidingWith(groundLayer);

        if(rb.velocity.x !=0)
        {
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

        if(isGrounded && rb.velocity.x != 0)
        {
            characterState = CharacterState.Walk;
        }

        else if(!isGrounded)
        {
            characterState = CharacterState.Jump;
        }

        else
        {
            characterState = CharacterState.Idle;
        }

        animator.SetInteger("state", (int)characterState);
        // var newState = rb.velocity.x !=0 ? CharacterState.Walk : CharacterState.Idle;

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            characterState = CharacterState.Jump;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

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
        else if (other.CompareTag("Checkpoint")) 
        {   if (checkpoint != null)
                {
                    checkpoint.SetCheckpointState(false);    
                }
            checkpoint = other.GetComponent<Checkpoint>();
            checkpoint.SetCheckpointState(true);

        }
        else if (other.CompareTag("Endpoint"))
        {
            var endPoint = other.gameObject.GetComponent<Endpoint>();
            endPoint.EndLevel();
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.gameObject.CompareTag("Enemy"))
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        activeLives--;
        OnActiveLivesChange?.Invoke(activeLives);
        transform.position = checkpoint.GetSpawnPoint().position;
    }


    private void OnDrawGizmos()
    {
        // Visualize the ground detection ray in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * (boxCollider.bounds.extents.y + 0.1f));
    }
}
