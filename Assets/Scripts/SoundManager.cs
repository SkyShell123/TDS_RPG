using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public AudioSource shoot;

    public void Awake()
    {
        Instance = this;
    }

    public void Shoot()
    {
        shoot.Play();
    }
}
