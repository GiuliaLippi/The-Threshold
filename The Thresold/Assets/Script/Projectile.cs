using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public float lifeTime = 3f;
    public bool destroyOnHit = true;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
                player.TakeDamage(damage);

            if (destroyOnHit)
                Destroy(gameObject);
        }

        if (other.CompareTag("Wall"))
            Destroy(gameObject);
    }
}