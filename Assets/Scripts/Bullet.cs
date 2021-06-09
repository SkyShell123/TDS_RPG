using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    internal float destroyTime;
    internal int damage;

    private void Start()
    {
        Invoke("DestroyBullet", destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            PlayerHealthSistem.Instance.TakeDamage(damage);
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
