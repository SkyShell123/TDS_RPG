using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Transform ShotDir;
    public GameObject Bullet;

    private float timeShot;
    private float timeRecharge;
    private float CurBullet;
    public float SpeedShoot = 0.5f;
    public float SpeedBullet = 10f;
    public float destroyTime = 1f;
    public int Damage = 1;

    public float SizeBullet = 1;
    public int CountShoots = 1;
    public int CountBullet = 5;
    public float Recharge = 3f;

    private void Start()
    {
        CurBullet = CountBullet;
    }

    void Update()
    {
        if (timeShot > 0)
        {
            timeShot -= Time.deltaTime;
        }
        if (timeRecharge > 0)
        {
            timeRecharge -= Time.deltaTime;
        }
    }

    public void Shoot()
    {
        if (timeShot <= 0 && timeRecharge <= 0)
        {
            if (CountShoots > 1)
            {
                ShotDir.rotation = ShotDir.rotation * Quaternion.Euler(0, 0, CountShoots * CountShoots);
            }

           

            for (int i = 0; i < CountShoots; i++)
            {
                if (CountShoots > 1)
                {
                    ShotDir.rotation = ShotDir.rotation * Quaternion.Euler(0, 0, -CountShoots*2);
                }
                
                Spawn();
            }

            ShotDir.rotation = transform.rotation;

            timeShot = SpeedShoot;
            CurBullet--;
            SoundManager.Instance.Shoot();

            if (CurBullet <= 0)
            {
                timeRecharge = Recharge;
                CurBullet = CountBullet;
            }
        }
    }

    private void Spawn()
    {
        GameObject inst_obj = Instantiate(Bullet, ShotDir.position, ShotDir.rotation);
        Bullet bullet = inst_obj.GetComponent<Bullet>();
        bullet.destroyTime = destroyTime;
        bullet.damage = Damage;
        bullet.transform.localScale = new Vector2(SizeBullet, SizeBullet);
        Rigidbody2D rb = inst_obj.GetComponent<Rigidbody2D>();
        rb.AddForce(ShotDir.up * SpeedBullet, ForceMode2D.Impulse);
    }
}