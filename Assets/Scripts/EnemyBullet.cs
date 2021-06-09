//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EnemyBullet : MonoBehaviour
//{
//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.CompareTag("Wall"))
//        {
//            Destroy(gameObject);
//        }
//        else if (other.CompareTag("Player"))
//        {
//            Destroy(gameObject);
//            PlayerHealthSistem.Instance.TakeDamage(5);
//        }
//    }
//}
