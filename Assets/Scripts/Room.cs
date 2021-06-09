using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public EnemyAI[] EnemiesPref;
    public List<EnemyAI> Enemies;

    public GameObject DoorU;
    public GameObject DoorR;
    public GameObject DoorD;
    public GameObject DoorL;

    internal int index;
    public int CountEnemies;

    //public Mesh[] BlockMeshes;

    private void Start()
    {
        for (int i = 0; i < CountEnemies; i++)
        {
            EnemyAI inst_obj = Instantiate(EnemiesPref[Random.Range(0, EnemiesPref.Length)], new Vector2(Random.Range(transform.position.x - 4, transform.position.x + 4), Random.Range(transform.position.y - 4, transform.position.y + 4)), transform.rotation);
            Enemies.Add(inst_obj);

        }








        //foreach (var filter in GetComponentsInChildren<MeshFilter>())
        //{
        //    if (filter.sharedMesh == BlockMeshes[0])
        //    {
        //        filter.sharedMesh = BlockMeshes[Random.Range(0, BlockMeshes.Length)];
        //        filter.transform.rotation = Quaternion.Euler(-90, 0, 90 * Random.Range(0, 4));
        //    }
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (var item in Enemies)
            {
                if (item != null)
                {
                    item.PlayerTrigger(true);
                }

            }
        }

        if (collision.CompareTag("Enemy"))
        {
            EnemyAI Cur = collision.GetComponent<EnemyAI>();
            Enemies.Add(Cur);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (var item in Enemies)
            {
                if (item != null)
                {
                    item.PlayerTrigger(false);
                }

            }
        }

        if (collision.CompareTag("Enemy"))
        {
            EnemyAI Cur = collision.GetComponent<EnemyAI>();
            Enemies.Remove(Cur);

        }
    }

    public void RotateRandomly()
    {
        //int count = Random.Range(0, 4);

        //for (int i = 0; i < count; i++)
        //{
        //    transform.Rotate(0, 0, 90);

        //    GameObject tmp = DoorL;
        //    DoorL = DoorD;
        //    DoorD = DoorR;
        //    DoorR = DoorU;
        //    DoorU = tmp;
        //}
    }
}
