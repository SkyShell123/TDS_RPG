using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shoot : MonoBehaviour
{

    Color color1 = new Color(0.2028302f, 1f, 1f);
    Color color2 = new Color(1f, 0f, 0.5996146f);

    public Text CurrentAmmo;
    public Text MaxAmmo;
    private float CurrentAmmoCount;
    public float MaxAmmoCount;
    private float MemoryAmmoCount;
    public Transform ShotDir;
    public GameObject Bullet;
    private float timeShot;
    public float startTime = 0.5f;
    public float SpeedAttack = 10f;
    bool recharge;

    private void Start()
    {
        MemoryAmmoCount = MaxAmmoCount;
        CurrentAmmoCount = 2;
        CurrentAmmo.text = CurrentAmmoCount.ToString();
        MaxAmmo.text = MaxAmmoCount.ToString();
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
            else if (Input.GetMouseButton(1))
            {

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
        CurrentAmmo.text = CurrentAmmoCount.ToString();
        MaxAmmo.text = MaxAmmoCount.ToString();
        recharge = false;
    }

    void Spawn()
    {
        GameObject inst_obj = Instantiate(Bullet, ShotDir.position, ShotDir.rotation);
        Rigidbody2D rb = inst_obj.GetComponent<Rigidbody2D>();
        rb.AddForce(ShotDir.up * SpeedAttack, ForceMode2D.Impulse);
        SwitchColor(1);
    }

    void SwitchColor(int i)
    {
        CurrentAmmoCount -= i;
        CurrentAmmo.text = CurrentAmmoCount.ToString();

        float switchColor = CurrentAmmoCount / MaxAmmoCount;

        CurrentAmmo.color = Color.Lerp(color2, color1, switchColor);
    }
}
