using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
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
    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
