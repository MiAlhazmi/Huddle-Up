using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // private MeshRenderer _meshRenderer;
    
    private PlayerUI _playerUI;
    private PlayerMovement _playerMovement;
    private PlayerHit _playerHit;
    [SerializeField] private GameObject _visual; // visual gameobject
    [SerializeField] private Color _playerColor; // This attribute is for the color that the player chooses
    private SkinnedMeshRenderer _skinnedMesh;
    [SerializeField] private bool isTagger = false;
    
    private const float TaggerSpeed = 9.5f;
    private const float NonTaggerSpeed = 8f;
    
    private const float TaggerHitDelay = 1f;
    private const float NonTaggerHitDelay = 0.5f;



    private void Awake()
    {
        _playerUI = GetComponent<PlayerUI>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerHit = GetComponent<PlayerHit>();
        if (_visual == null) GameObject.FindWithTag("Visual");
        else _skinnedMesh = _visual.GetComponent<SkinnedMeshRenderer>();
        _playerColor = Color.blue;
        ChangeColor(_playerColor);
    }

    public void DeActivatePlayer()
    {
        _visual.SetActive(false);
        gameObject.GetComponent<PlayerHit>().enabled = false;
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<CharacterController>().enabled = false;
    }
    
    public void ActivatePlayer()
    {
        _visual.SetActive(true);
        gameObject.GetComponent<PlayerHit>().enabled = true;
        gameObject.GetComponent<PlayerMovement>().enabled = true;
        gameObject.GetComponent<CharacterController>().enabled = true;
        SetIsTagger(false);
    }
    
    public void SetIsTagger(bool paraIsTagger)
    {
        isTagger = paraIsTagger;
        if (isTagger)
        {
            ChangeColor(Color.red);
            ChangeSpeed(TaggerSpeed);
            _playerHit.SetHitDelay(TaggerHitDelay);
        }
        else
        {
            ChangeColor(_playerColor);
            ChangeSpeed(NonTaggerSpeed);
            _playerHit.SetHitDelay(NonTaggerHitDelay);
        }
        
        _playerUI.ShowTagOverlay(isTagger); // to show the effect on screen indicating if he's the tagger or not
    }

    public bool GetIsTagger()
    {
        return isTagger;
    }

    private void ChangeColor(Color color)
    {
        // _meshRenderer.material.color = color;
        _skinnedMesh.material.color = color;
    }

    // This function is for the color that the player chooses
    public void ChangePlayerColor(Color color)
    {
        _playerColor = color;
        ChangeColor(_playerColor);
    }

    public Color GetPlayerColor()
    {
        return _playerColor;
    }

    private void ChangeSpeed(float speed)
    {
        _playerMovement.SetSprintSpeed(speed);
        _playerMovement.RefreshSprint();
    }
}
