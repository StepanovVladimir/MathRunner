using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
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
}
