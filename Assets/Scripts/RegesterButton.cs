using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RegesterButton : MonoBehaviour
{
    public void OnRegisterButtonClicked()
    {
        SceneManager.LoadScene("Register");
    }
}
