using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_one : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public int damage = 10;

    private Transform targetPoint;

    void Start()
    {
        targetPoint = pointA;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPoint.position,
            speed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
            targetPoint = targetPoint == pointA ? pointB : pointA;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovementt>();
            if (player != null)
                player.TakeDamage(damage);
        }
    }
}
