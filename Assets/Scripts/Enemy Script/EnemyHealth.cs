using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    Enemy enemy;
    public GameObject deathEffect;
    public bool isDamaged;
    public bool isStunned;
    public SpriteRenderer sprite;
    Blink material;
    Rigidbody2D rb;
    EnemyMovements enemyMovements;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        sprite = GetComponent<SpriteRenderer>();
        material = GetComponent<Blink>();
        rb = GetComponent<Rigidbody2D>();
        enemyMovements = GetComponent<EnemyMovements>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon") && !isDamaged)
        {
            enemy.healthPoints -= 2f;

            float knockbackDirection = collision.transform.position.x < transform.position.x ? 1f : -1f;
            rb.velocity = new Vector2(knockbackDirection * enemy.knockbackForceX, enemy.knockbackForceY);

            StartCoroutine(StunEnemy(1f)); 

            if (enemy.healthPoints <= 0)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    IEnumerator StunEnemy(float stunDuration)
    {
        isStunned = true;
        isDamaged = true;
        sprite.material = material.blink;
        enemyMovements.enabled = false; 
        rb.velocity = Vector2.zero; 

        yield return new WaitForSeconds(stunDuration);

        isStunned = false;
        isDamaged = false;
        sprite.material = material.original;
        enemyMovements.enabled = true; 
    }
}
