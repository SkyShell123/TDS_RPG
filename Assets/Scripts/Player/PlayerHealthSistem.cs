using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSistem : MonoBehaviour
{
    public static PlayerHealthSistem Instance { get; private set; }
    public Image bar;
    public Text healthCur;
    public Text healthMax;
    float healthMemory;
    public float health = 2f;
    SpriteRenderer spriteRenderer;
    bool Dead = false;

    public void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        healthMemory = health;
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateHealth();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        spriteRenderer.color = Color.red;
        Invoke("DamageEffect", 0.2f);

        UpdateHealth();
    }

    void DamageEffect()
    {
        spriteRenderer.color = Color.white;

        if (health <= 0 && !Dead)
        {
            Dead = true;
            spriteRenderer.color = Color.red;
            Invoke("Die", 0.1f);
        }
    }

    public void FullHealth()
    {
        health = healthMemory;
        UpdateHealth();
    }

    private void UpdateHealth() 
    {
        bar.fillAmount = health / healthMemory;
        healthCur.text = health.ToString();
        healthMax.text = healthMemory.ToString();
    }

    void Die()
    {
        CharacterControls.Instance.Die();
    }
}
