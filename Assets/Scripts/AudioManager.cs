using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public GameManager GM;

    public AudioSource effects;
    public AudioClip jump;
    public AudioClip collision;
    public AudioClip wrongEquation;
    public AudioClip rightEquation;
    public AudioClip lose;
    public AudioClip win;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayJumpEffect()
    {
        if (GM.isSound)
        {
            effects.PlayOneShot(jump);
        }
    }

    public void PlayCollisionEffect()
    {
        if (GM.isSound)
        {
            effects.PlayOneShot(collision);
        }
    }

    public void PlayWrongEquationEffect()
    {
        if (GM.isSound)
        {
            effects.PlayOneShot(wrongEquation);
        }
    }

    public void PlayRightEquationEffect()
    {
        if (GM.isSound)
        {
            effects.PlayOneShot(rightEquation);
        }
    }

    public void PlayLoseEffect()
    {
        if (GM.isSound)
        {
            effects.PlayOneShot(lose);
        }
    }

    public void PlayWinEffect()
    {
        if (GM.isSound)
        {
            effects.PlayOneShot(win);
        }
    }
}
