using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingSave : MonoBehaviour
{

    [SerializeField]
    private Toggle toggle;
    [SerializeField]
    private AudioSource myAudio;

    public void Awake()
    {
        // 1
        if (!PlayerPrefs.HasKey("music"))
        {
            
            PlayerPrefs.SetFloat("music", 0.5f);

            myAudio.volume = 0.5f;
            PlayerPrefs.Save();
        }
        // 2
        else
        {
            if (PlayerPrefs.GetInt("music") == 0)
            {
                myAudio.enabled = false;
                toggle.isOn = false;
            }
            else
            {
                myAudio.enabled = true;
                toggle.isOn = true;
            }
        }
    }

    public void ToggleMusic()
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt("music", 1);
            myAudio.enabled = true;
        }
        else
        {
            PlayerPrefs.SetInt("music", 0);
            myAudio.enabled = false;
        }
        PlayerPrefs.Save();
    }
}
