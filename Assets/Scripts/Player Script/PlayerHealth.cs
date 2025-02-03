using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public int potions;
    public int maxPotions;
    public float potionsPower;

    bool isInmune;
    public float inmunityTime;

    bool isHealing;
    Blink material;
    SpriteRenderer sprite;
    public float knockbackForceX, knockbackForceY;
    Rigidbody2D rb;

    public UnityEvent<float> healthChange = new UnityEvent<float>(); 
    public UnityEvent<int> potionChange = new UnityEvent<int>(); 

    void Start()
    {
        health = maxHealth;
        potions = maxPotions;
        potionsPower = 1;
        material = GetComponent<Blink>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        healthChange.Invoke(health);
        potionChange.Invoke(potions); 
    }

    void Update()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        // Detener el uso de pociones múltiples si ya se está usando una.
        if (Input.GetKeyDown(KeyCode.H)) // Puedes ajustar la tecla de uso de poción
        {
            if (potions > 0 && health < maxHealth)
            {
                UsePotion();
            }
        }
    }

    public void HealLife(float quantityRestored)
    {
        float auxHealth = health + quantityRestored;

        if (auxHealth > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health = auxHealth;
        }

        healthChange.Invoke(health); 
    }

    public void UsePotion()
    {
        if (potions > 0 && health < maxHealth) 
        {
            HealLife(maxHealth * potionsPower); 
            potions--; 
            potionChange.Invoke(potions); 
        }
    }

    public void restorePotion(int amount)
    {
        if (potions < maxPotions)
        {
            potions = Mathf.Clamp(potions + amount, 0, maxPotions); 
            potionChange.Invoke(potions); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isInmune)
        {
            health -= collision.GetComponent<Enemy>().damageGived;
            StartCoroutine(Inmunity());

            if (collision.transform.position.x > transform.position.x)
            {
                rb.AddForce(new Vector2(knockbackForceX, knockbackForceY), ForceMode2D.Force);
            }
            else
            {
                rb.AddForce(new Vector2(-knockbackForceX, knockbackForceY), ForceMode2D.Force);
            }

            healthChange.Invoke(health); 

            if (health <= 0)
            {
                print("player dead");
            }
        }
    }

    IEnumerator Inmunity()
    {
        isInmune = true;
        sprite.material = material.blink;
        yield return new WaitForSeconds(inmunityTime);
        sprite.material = material.original;
        isInmune = false;
    }
}


