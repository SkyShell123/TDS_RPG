using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrops : MonoBehaviour
{
    public int Count;
    public GameObject DropPref;
    private GameObject inst_obj;

    void Start()
    {
        
    }

    public void Drop()
    {
        inst_obj = Instantiate(DropPref,transform.position, transform.rotation);
        Drop drop = inst_obj.GetComponent<Drop>();
        drop.SetCount(Count);

    }
}
