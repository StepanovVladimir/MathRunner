using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Sprite soundsOn;
    public Sprite soundsOff;
    public Image soundsBtnImg;

    void Start()
    {
        if (PlayerPrefs.GetInt("Sound") == 0)
        {
            soundsBtnImg.sprite = soundsOn;
        }
        else
        {
            soundsBtnImg.sprite = soundsOff;
        }
    }

    public void Play()
    {
        if (PlayerPrefs.GetInt("Levels") == 0)
        {
            SceneManager.LoadScene("Level1");
        }
        else
        {
            SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("Levels"));
        }
    }

    public void Levels()
    {
        SceneManager.LoadScene("Levels");
    }

    public void SoundBtn()
    {
        if (PlayerPrefs.GetInt("Sound") == 0)
        {
            PlayerPrefs.SetInt("Sound", 1);
            soundsBtnImg.sprite = soundsOff;
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 0);
            soundsBtnImg.sprite = soundsOn;
        }
    }
}
