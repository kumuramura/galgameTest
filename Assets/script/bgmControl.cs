using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bgmControl : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider backgroundMusic;

    public void Awake()
    {
        // 1
        if (!PlayerPrefs.HasKey("music"))
        {

            PlayerPrefs.SetFloat("music", 0.5f);

            backgroundMusic.value = 0.5f;
            PlayerPrefs.Save();
        }
        // 2
        else
        {
            backgroundMusic.value=PlayerPrefs.GetFloat("music");
            
        }
    }

    

    public void change_volume()
    {
        PlayerPrefs.SetFloat("music", backgroundMusic.value);
        backgroundMusic.value = PlayerPrefs.GetFloat("music");
        audioSource.volume = backgroundMusic.value;
        
        PlayerPrefs.Save();
    }
}
