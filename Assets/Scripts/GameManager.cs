using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int thisLevel;
    public bool hasNextLevel;
    public PlayerMovement PM;
    public bool canPlay = false;
    public bool damage = false;
    public bool isSound;
    public float time;
    public Text timerText;
    public GameObject lose;
    public GameObject win;

    public List<Skin> skins;

    void Start()
    {
        timerText.text = time.ToString("F2");
        ActivateSkin(PlayerPrefs.GetInt("Skin"));
        isSound = PlayerPrefs.GetInt("Sound") == 0;
    }

    void Update()
    {
        if (canPlay)
        {
            time -= Time.deltaTime;
            if (time > 0)
            {
                timerText.text = time.ToString("F2");
            }
            else
            {
                timerText.text = 0.ToString("F2");
                StartCoroutine(Lose());
            }
        }
    }

    public void Win()
    {
        if (hasNextLevel && PlayerPrefs.GetInt("Levels") < thisLevel + 1)
        {
            PlayerPrefs.SetInt("Levels", thisLevel + 1);
        }
        win.SetActive(true);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Level" + (thisLevel + 1));
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level" + thisLevel);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Levels()
    {
        SceneManager.LoadScene("Levels");
    }

    public void ActivateSkin(int index)
    {
        for (int i = 0; i < skins.Count; i++)
        {
            if (i != index)
            {
                skins[i].HideSkin();
            }
            else
            {
                skins[i].ShowSkin();
            }
        }
        PM.animator = skins[index].animator;
    }

    IEnumerator Lose()
    {
        canPlay = false;
        PM.Lose();
        AudioManager.instance.PlayLoseEffect();

        yield return new WaitForSeconds(2.85f);

        lose.SetActive(true);
    }
}
