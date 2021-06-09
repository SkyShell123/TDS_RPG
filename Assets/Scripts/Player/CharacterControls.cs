using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterControls : MonoBehaviour
{
    public static CharacterControls Instance { get; private set; }
    public float speed=10f;
    private Rigidbody2D rb;
    private Animator anim;
    public bool dead = false;
    public float speedAttack = 3;

    Vector2 CamPosition;
    Vector2 MovePosition;

    public GameObject[] weapons;

    public void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        CamPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MovePosition = CamPosition - rb.position;
        float RotateZ = Mathf.Atan2(MovePosition.y, MovePosition.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = RotateZ;

        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
    }

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        weapons[weapons.Length - 1].SetActive(true);
    //        anim.SetTrigger("Attack");
    //        anim.speed = 1 / speedAttack;

    //        Invoke("WideObj", speedAttack);
    //    }
    //}

    public void WideObj()
    {
        weapons[weapons.Length-1].SetActive(false);
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        dead = true;
    }
}
