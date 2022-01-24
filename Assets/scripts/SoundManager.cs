using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    public AudioClip[] clip1;

    AudioSource audiosor;

    void Start()
    {
        audiosor = GetComponent<AudioSource>();
    }

    


    void Awake()
    {
        if (instance != null)
        {


            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }




    void Update()
    {

    }


    public void Play(int clipnumber)
    {
        audiosor.PlayOneShot(clip1[clipnumber], 0.5f);
    }

}
