using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovements : MonoBehaviour
{
    float speed;
    Rigidbody2D rb;  
    Animator anim;

    public bool isStatic;
    public bool isWalking;
    public bool walksRight;

    public Transform wallCheck, pitCheck, groundCheck;
    public bool wallDetected, pitDetected, isGround;
    public float detectionRadius;
    public LayerMask whatIsGround;
    
    private bool hasFlipped = false; // Variable para evitar múltiples giros

    void Start()
    {
        speed = GetComponent<Enemy>().speed;
        rb = GetComponent<Rigidbody2D>();  
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        pitDetected = !Physics2D.OverlapCircle(pitCheck.position, detectionRadius, whatIsGround);
        wallDetected = Physics2D.OverlapCircle(wallCheck.position, detectionRadius, whatIsGround);

        if ((pitDetected || wallDetected) && !hasFlipped)
        {
            Flip();
            hasFlipped = true; // Marca que ya giró
            Invoke("ResetFlip", 0.5f); // Espera 0.5s antes de permitir otro giro
        }
    }

    private void FixedUpdate()
    {
        if (isStatic)
        {
            anim.SetBool("Idle", true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            return;
        }

        if (isWalking)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            float moveDirection = walksRight ? 1f : -1f;
            rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
        }
    }

    public void Flip()
    {
        if (hasFlipped) return;

        Vector3 newScale = transform.localScale;
        
        Debug.Log($"Antes de Flip: {newScale}");

        newScale.x *= -1;

        if (float.IsInfinity(newScale.x) || float.IsNaN(newScale.x))
        {
            Debug.LogError("Error: Valor inválido en localScale.x antes de asignarlo");
            return;
        }

        transform.localScale = newScale;
        walksRight = !walksRight;

        hasFlipped = true;
        Invoke("ResetFlip", 0.5f);

        Debug.Log($"Después de Flip: {transform.localScale}, walksRight: {walksRight}");
    }

    void ResetFlip()
    {
        hasFlipped = false;
    }
}
