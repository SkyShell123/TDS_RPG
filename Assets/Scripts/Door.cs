using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;
    bool IsOpen = false;
    bool IsRange = false;
    private float timerCur;
    private float timer = 0.65f;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (timerCur > 0)
        {
            timerCur -= Time.deltaTime;
        }
    }

    private void Update()
    {
        if (IsRange && Input.GetKeyUp(KeyCode.E) && timerCur<=0)
        {
            if (!IsOpen)
            {
                anim.SetTrigger("Open");
                IsOpen = true;
            }
            else
            {
                anim.SetTrigger("Close");
                IsOpen = false;
            }

            PlayerTrigg();
        }
    }
    public void PlayerTrigg()
    {
        
        if (timerCur <= 0)
        {
            timerCur = timer;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IsRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IsRange = false;
        }
    }
}
