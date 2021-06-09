using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCountMenu : MonoBehaviour
{
    Color color1 = new Color(0.2028302f, 1f, 1f);
    Color color2 = new Color(1f, 0f, 0.5996146f);

    public Text CurrentAmmo;
    public Text MaxAmmo;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void SwitchWeapons()
    {
        //CurrentAmmo.text = CurrentAmmoCount.ToString();
        //MaxAmmo.text = MaxAmmoCount.ToString();
    }
}
