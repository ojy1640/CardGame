using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip bgmusic;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = bgmusic;
        Invoke("bgm" , 0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void bgm()
    {
        audioSource.Play();
    }
}
