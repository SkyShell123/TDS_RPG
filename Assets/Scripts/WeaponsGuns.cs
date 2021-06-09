using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsGuns : MonoBehaviour
{
    public Transform ShotDir;
    public GameObject Bullet;

    private float CurrentAmmoCount;
    private float MaxAmmoCount;
    private float MemoryAmmoCount;


    private float timeShot;
    private float startTime = 0.5f;
    private float SpeedShoot = 10f;
    private float destroyTime = 1f;
    private bool recharge;

    void Start()
    {
        MemoryAmmoCount = MaxAmmoCount;
        CurrentAmmoCount = 2;
    }

    void Update()
    {
        if (timeShot <= 0 && CurrentAmmoCount > 0 && !recharge)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Spawn();
                timeShot = startTime;
            }
            if (Input.GetMouseButton(1))
            {
                startTime = 0.1f;
                Spawn();
                timeShot = startTime;
            }
            else
            {
                startTime = 0.5f;
            }
        }
        else if (CurrentAmmoCount < 1 && !recharge && MaxAmmoCount > 0)
        {
            recharge = true;
            Invoke("Recharge", timeShot * 2);
        }
        else
        {
            timeShot -= Time.deltaTime;
        }

        
    }
    public void FullAmmo()
    {
        if (MemoryAmmoCount - MaxAmmoCount >= 10)
        {
            MaxAmmoCount += 10;
        }
        else
        {
            MaxAmmoCount = MemoryAmmoCount;
        }

        //SwitchColor(0);
        //if (CurrentAmmoCount < 1)
        //{
        //    Invoke("Recharge", timeShot * 2);
        //}
    }

    void Recharge()
    {
        CurrentAmmoCount = 2;
        MaxAmmoCount -= 2;
        //CurrentAmmo.text = CurrentAmmoCount.ToString();
        //MaxAmmo.text = MaxAmmoCount.ToString();
        recharge = false;
    }

    void Spawn()
    {
        GameObject inst_obj = Instantiate(Bullet, ShotDir.position, ShotDir.rotation);
        Bullet bullet = inst_obj.GetComponent<Bullet>();
        bullet.destroyTime = destroyTime;
        Rigidbody2D rb = inst_obj.GetComponent<Rigidbody2D>();
        rb.AddForce(ShotDir.up * SpeedShoot, ForceMode2D.Impulse);
        //Invoke("DestroyBullet", destroyTime);
        //DestroyBullet(inst_obj);
        SwitchColor(1);
    }

    //public void DestroyBullet(GameObject bullet)
    //{
    //    Destroy(bullet.gameObject);
    //}

    void SwitchColor(int i)
    {
        //CurrentAmmoCount -= i;
        //CurrentAmmo.text = CurrentAmmoCount.ToString();

        //float switchColor = CurrentAmmoCount / MaxAmmoCount;

        //CurrentAmmo.color = Color.Lerp(color2, color1, switchColor);
    }
}
