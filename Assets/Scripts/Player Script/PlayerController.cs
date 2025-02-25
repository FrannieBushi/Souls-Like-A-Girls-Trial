using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class PlayerController : MonoBehaviour
{
    public float speed, jumpHeight;
    float velX, velY;
    Rigidbody2D rb;
    public Transform groundcheck;
    public bool isGrounded;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    Animator anim;

    private PlayerHealth playerHealth;

    private bool isHealing;
    public bool isAttacking;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
        anim = GetComponent<Animator>();

        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToMainMenu();
        }

        isGrounded = Physics2D.OverlapCircle(groundcheck.position, groundCheckRadius, whatIsGround);

        if (isGrounded)
        {
            anim.SetBool("Jump", false);
        }
        else
        {
            anim.SetBool("Jump", true);    
        }
        
        if (!isHealing) 
        {
            Attack();
            Heal();
        }
    }

    private void FixedUpdate()
    {
        if (!isHealing)
        {
            Movement();
            Jump();
        }
        else
        {
            rb.velocity = Vector2.zero; 
        }
    }

    public void Movement()
    {
        velX = Input.GetAxisRaw("Horizontal");
        velY = rb.velocity.y;
        rb.velocity = new Vector2(velX * speed, velY);

        if (rb.velocity.x != 0)
        {
            anim.SetBool("Walk", true);
            FlipCharacter();
        }
        else
        {
            anim.SetBool("Walk", false);
            FlipCharacter();
        }
    }

    public void FlipCharacter()
    {
        Vector3 currentScale = transform.localScale;
        if (rb.velocity.x > 0)
        {
            currentScale.x = Mathf.Abs(currentScale.x); 
        }
        else if (rb.velocity.x < 0)
        {
            currentScale.x = -Mathf.Abs(currentScale.x); 
        }
        transform.localScale = currentScale;
    }

    public void Jump()
    {
        if (Input.GetButton("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
    }

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            isAttacking = true;
            anim.SetBool("Attack", true);
            Debug.Log("¡Ataque ejecutado!");

            StartCoroutine(ResetAttackState());
        }
        else
        {
            anim.SetBool("Attack", false);    
        }
    }

    public void Heal()
    {
        if (Input.GetKeyDown(KeyCode.H) && playerHealth.potions > 0 && !isHealing)
        {
            isHealing = true;
            anim.SetBool("Heal", true);       
        }
        else
        {
            anim.SetBool("Heal", false);
        }
    }

    public void FinishHeal() 
    {
        playerHealth.potions--;
        playerHealth.HealLife(playerHealth.potionsPower);
        Debug.Log("Sanación completada. Pociones restantes: " + playerHealth.potions);

        isHealing = false; 
        anim.SetBool("Heal", false); 
    }

    IEnumerator ResetAttackState()
    {
        yield return new WaitForSeconds(0.5f); 
        isAttacking = false;
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");  
    }
}