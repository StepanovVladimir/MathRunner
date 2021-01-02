using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsManager : MonoBehaviour
{
    public List<SkinItem> items;
    public GameObject startMenu;

    int activeSkin;

    void Start()
    {
        activeSkin = PlayerPrefs.GetInt("Skin");
        CheckButtons();
    }

    public void SetActiveSkin(int index)
    {
        activeSkin = index;
    }

    public void OpenSkins()
    {
        startMenu.SetActive(false);
        gameObject.SetActive(true);
    }

    public void CloseSkins()
    {
        gameObject.SetActive(false);
        startMenu.SetActive(true);
    }

    public void CheckButtons()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (i != activeSkin)
            {
                items[i].activate.interactable = true;
            }
            else
            {
                items[i].activate.interactable = false;
            }
        }
    }
}
