using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsFind : MonoBehaviour
{
    private Shoot ammo;

    private void Start()
    {
        ammo = GetComponent<Shoot>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "HealthBox")
        {
            Destroy(collision.gameObject);

            PlayerHealthSistem.Instance.FullHealth();
        }

        if (collision.gameObject.name == "AmmoBox")
        {
            Destroy(collision.gameObject);

            ammo.FullAmmo();
        }
    }
}
