using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Encapsulation
    private static SoundManager _instance;
    public static SoundManager instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("Sound Manager is NULL!");
            }
            return _instance;
        }
    }

    public AudioSource audioSource;

    AudioClip damage;

    private void Start()
    {
        damage = Resources.Load<AudioClip>("dealDamage");
    }

    //Abstracion
    public void PlaySound(string clip)
    {
        switch (clip)
        {
            case "DealDamage":
                audioSource.PlayOneShot(damage);
                break;
            default:
                break;
        }
    }
    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
