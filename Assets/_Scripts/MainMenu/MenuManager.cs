using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject Panel;
    
    public void AnimatePanel()
    {
        Panel.GetComponent<Animator>().SetTrigger("Pop");
    }

    public void PopPanel()
    {
        AnimatePanel();
        // if (Panel.activeSelf)
        // {
        //     AnimatePanel();
        // }
        // else
        // {
        //     Panel.SetActive(true);
        //     AnimatePanel();
        // }
    }
}
