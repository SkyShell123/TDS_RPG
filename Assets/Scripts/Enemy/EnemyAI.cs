using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private EnemyShoot enemyShoot;
    public float speed = 10f;
    private Rigidbody2D rb;
    private GameObject player;

    private bool chill = true;
    private bool search = false;
    private bool agro=false;

    private bool searchill;


    private void Start()
    {
        enemyShoot = GetComponentInChildren<EnemyShoot>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if (chill)
        {
            Chill();
        }
        else if (search)
        {
            Search();
        }
        else if (agro)
        {
            Agro();
        }
    }

    public void PlayerTrigger(bool state)
    {
        chill = false;
        search = !state;
        agro = state;
    }

    private bool ReyDetector()
    {
        RaycastHit2D HitInfo = Physics2D.Raycast(transform.position, Vector2.up);

        if (!HitInfo.transform.CompareTag("Wall"))
        {
            Debug.Log(HitInfo.transform.tag);   
            return true;
        }
        else
        {
            return false;
        }
    }
    private void Move()
    {
        Vector2 MovePosition = new Vector2(player.transform.position.x, player.transform.position.y) - rb.position;
        float RotateZ = Mathf.Atan2(MovePosition.y, MovePosition.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = RotateZ;

        rb.velocity = transform.up * speed;
    }
    private void SearchToChill()
    {
        search = false;
        agro = false;
        chill = true;
    }
    private void Chill()
    {
        //Move();
    }
    private void Search()
    {
        //if (!searchill)
        //{
        //    searchill = true;
        //    Invoke("SearchToChill", 10f);
        //}

        Move();





        if (ReyDetector())
        {
            PlayerTrigger(true);
        }
    }

    private void Agro()
    {
        Move();


        if (ReyDetector())
        {

            enemyShoot.Shoot();
        }
    }
}
