using System;
using UnityEngine;
using TMPro;

public class DisplayFPS : MonoBehaviour
{
    public TextMeshProUGUI fpsText;

    private float _pollingTime = 1f;
    private float _time;
    private int _frameCount;

    private static DisplayFPS Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        _frameCount++;
        if (_time >= _pollingTime)
        {
            int frameRate = (int)Mathf.RoundToInt(_frameCount / _time);
            fpsText.text = frameRate.ToString() + " FPS";

            _time -= _pollingTime;
            _frameCount = 0;
        }
    }
}
