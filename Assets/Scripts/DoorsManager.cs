using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsManager : MonoBehaviour
{
    public static DoorsManager Instance { get; private set; }
    public GameObject[] Doors;
    Transform player;
    float min = 1000;
    int ArrayId;

    public void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public Vector3 DistansDoor(Vector3 enterVec)
    {
        for (int i = 0; i < Doors.Length; i++)
        {
            float minDistans = Vector2.Distance(enterVec, Doors[i].transform.position);

            if (minDistans < min)
            {
                min = minDistans;
                ArrayId = i;
            }
        }

        Vector3 DoorPos = Doors[ArrayId].transform.position;

        return DoorPos;
    }
}
