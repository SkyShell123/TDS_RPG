using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    private int Count;

    public void SetCount(int _Count)
    {
        Count = _Count;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UI.instance.Money(Count);
            Destroy(gameObject);
        }
    }
}
