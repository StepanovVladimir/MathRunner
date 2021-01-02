using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour
{
    public Animator animator;

    public void ShowSkin()
    {
        gameObject.SetActive(true);
    }

    public void HideSkin()
    {
        gameObject.SetActive(false);
    }
}
