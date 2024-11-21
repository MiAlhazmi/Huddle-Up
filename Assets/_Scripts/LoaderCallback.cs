using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This class purpose is to make sure that this scene is loaded for at least one frame.
public class LoaderCallback : MonoBehaviour
{
    private bool _isFirstUpdate = true; 

    void Update()
    {
        if (_isFirstUpdate)
        {
            Loader.LoaderCallback();
            _isFirstUpdate = false;
        }
    }
}
