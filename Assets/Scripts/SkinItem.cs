using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinItem : MonoBehaviour
{
    public int index;
    public Button activate;
    public SkinsManager SM;

    GameManager GM;

    void Start()
    {
        GM = FindObjectOfType<GameManager>();
    }

    public void ActivateItem()
    {
        PlayerPrefs.SetInt("Skin", index);
        SM.SetActiveSkin(index);
        SM.CheckButtons();
        GM.ActivateSkin(index);
    }
}
