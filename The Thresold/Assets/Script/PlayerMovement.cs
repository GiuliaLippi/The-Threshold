using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;

    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Attack")]
    public float attackCooldown = 0.5f;
    public GameObject attackPrefab;
    public Transform attackPoint;

    private Rigidbody2D rb;
    private Vector2 moveDirection = Vector2.right;
    private float lastAttackTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        HandleMovementInput();
        RotateSprite();
        HandleAttackInput();
    }

    void FixedUpdate()
    {
        rb.velocity = moveDirection * speed;
    }

    void HandleMovementInput()
    {
        Vector2 input = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        if (input != Vector2.zero)
            moveDirection = input.normalized;
    }

    void RotateSprite()
    {
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    void HandleAttackInput()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        if (attackPrefab && attackPoint)
            Instantiate(attackPrefab, attackPoint.position, attackPoint.rotation);
    }

    // 🔥 USATO DA TUTTI I NEMICI
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player colpito! Vita: " + currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("Player morto");
        gameObject.SetActive(false);
    }
}