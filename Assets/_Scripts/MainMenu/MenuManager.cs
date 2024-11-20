using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject Panel;
   
    public void Button()
    {
        Panel.GetComponent<Animator>().SetTrigger("Pop");
    }
}
