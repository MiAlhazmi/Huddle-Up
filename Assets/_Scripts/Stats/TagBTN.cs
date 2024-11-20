using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TagBTN : MonoBehaviour
{
    public void GoToStats()
    {
        SceneManager.LoadScene("Stats");
    }
}

