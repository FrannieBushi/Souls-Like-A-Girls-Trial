using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
        anim = GetComponent<Animator>();

        playerHealth = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics2D.OverlapCircle(groundcheck.position, groundCheckRadius, whatIsGround);

        if(isGrounded)
        {
            anim.SetBool("Jump", false);
        }
        else
        {
            anim.SetBool("Jump", true);    
        }
        
        if (!isHealing) // El jugador no puede realizar estas acciones mientras se cura
        {
            FlipCharacter();
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
            rb.linearVelocity = Vector2.zero; // Detén al jugador por completo
        }
    }

    public void Movement()
    {
        velX = Input.GetAxisRaw("Horizontal");
        velY = rb.linearVelocity.y;
        rb.linearVelocity = new Vector2(velX * speed, velY);

        if(rb.linearVelocity.x != 0)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }

    public void FlipCharacter()
    {
        if(rb.linearVelocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(rb.linearVelocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void Jump()
    {
        if(Input.GetButton("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2 (rb.linearVelocity.x, jumpHeight);
        }
    }

    public void Attack()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            anim.SetBool("Attack", true);
            Debug.Log("¡Ataque ejecutado!");
        }
        else
        {
            anim.SetBool("Attack", false);    
        }
    }

    public void Heal()
    {
        if(Input.GetKeyDown(KeyCode.H) && playerHealth.potions > 0 && !isHealing)
        {
            isHealing = true;
            anim.SetBool("Heal", true);       
        }
        else
        {
            anim.SetBool("Heal", false);
        }
    }

    public void FinishHeal() // Método llamado desde un Animation Event
    {
    
        playerHealth.potions--;
        playerHealth.HealLife(playerHealth.potionsPower);
        Debug.Log("Sanación completada. Pociones restantes: " + playerHealth.potions);
        
        isHealing = false; // Permite movimiento y otras acciones nuevamente
        anim.SetBool("Heal", false); 
    }

}
