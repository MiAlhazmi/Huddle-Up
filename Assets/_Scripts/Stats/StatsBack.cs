using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsBack : MonoBehaviour
{
    public void GoToStatsMenu()
    {
        SceneManager.LoadScene("StatsMenu");
    }
}
