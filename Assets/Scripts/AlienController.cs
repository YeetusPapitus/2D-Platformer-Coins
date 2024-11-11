using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienController : MonoBehaviour
{
    private Transform spawnPoint;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask groundLayer; 
    [SerializeField] private LayerMask sprinkleLayer; 
    [SerializeField] private BoxCollider2D boxCollider;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if (IsCollidingWith(sprinkleLayer))
        {
            transform.position = spawnPoint.position;
        }
        // Movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && IsCollidingWith(groundLayer))
        {
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
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Checkpoint")) 
        {   if (spawnPoint != null)
                {
                    spawnPoint.GetComponentInParent<Checkpoint>().SetCheckpointState(false);    
                }
            spawnPoint = other.GetComponent<Checkpoint>().GetSpawnPoint();
            other.GetComponent<Checkpoint>().SetCheckpointState(true);

        }
        
    }


    private void OnDrawGizmos()
    {
        // Visualize the ground detection ray in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * (boxCollider.bounds.extents.y + 0.1f));
    }
}
