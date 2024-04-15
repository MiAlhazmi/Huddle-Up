using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour
{
    public void RegisterButton()
    {
        SceneManager.LoadScene("Login");
        Time.timeScale = 1;
    }
}
