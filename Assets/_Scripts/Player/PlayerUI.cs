using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("UI Text")]
    [SerializeField] private TextMeshProUGUI _promptText;
    [SerializeField] private TextMeshProUGUI _notifyText;
    [SerializeField] private float _notifyDuration = 3f; // how long the image stays fully opaque 
    [SerializeField] private float _notifyFadeSpeed = 3f; // how quickly the image will fade
    [SerializeField] private float _notifyDurationTimer; // Timer to check against the duration
    public bool _notifyOn;

    
    [Header("Damage Overlay")]
    [SerializeField] private Image overlay; // DamageOverlay GameObject    
    [SerializeField] private float _duration; // how long the image stays fully opaque 
    [SerializeField] private float _fadeSpeed; // how quickly the image will fade
    [SerializeField] private float _durationTimer; // Timer to check against the duration

    [Header("Damage Overlay")] [SerializeField]
    private Image _tagOverlay;

    private void Start()
    {
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
        _tagOverlay.color = new Color(_tagOverlay.color.r, _tagOverlay.color.g, _tagOverlay.color.b, 0);

        
    }

    private void Update()
    {
        if (overlay.color.a > 0)
        {
            _durationTimer += Time.deltaTime;
            if (_durationTimer > _duration)
            {
                // fade the image
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * _fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }

        // if NotificationText is updated
        if (_notifyOn)
        {
            _notifyDurationTimer += Time.deltaTime;
            if (_notifyDurationTimer > _notifyDuration)
            {
                // A hacky way to animate the text
                if (_notifyDurationTimer < _notifyDuration + _notifyFadeSpeed && _notifyText.text.Length > 0)
                {
                    _notifyText.text = _notifyText.text.Remove(_notifyText.text.Length - 1);
                }
                else
                {
                    _notifyText.text = "";
                    _notifyOn = false;
                }
            }
        }
    }

    public void TakeDamageUI()
    {
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
        _durationTimer = 0;
    }

    public void ShowTagOverlay(bool isTagger)
    {
        _tagOverlay.color = isTagger? 
            new Color(_tagOverlay.color.r, _tagOverlay.color.g, _tagOverlay.color.b, 1) : 
            new Color(_tagOverlay.color.r, _tagOverlay.color.g, _tagOverlay.color.b, 0);
    }
    
    public void UpdateInteractionText(string promptMessage)
    {
        _promptText.text = promptMessage;
    }

    public void UpdateNotificationText(String message)
    {
        _notifyText.text = message;
        _notifyOn = true;
        _notifyDurationTimer = 0;
    }
}
