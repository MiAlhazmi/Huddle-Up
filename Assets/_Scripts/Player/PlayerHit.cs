using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private Camera _cam; // we need this to know where the player is aiming at.
    private PlayerToGameControl _gameControl;
    // private Player playerClass; // This's the player's component of the player game object  
    [SerializeField] private LayerMask _playerLayerMask;
    private PlayerAnimation _animation;
    private PlayerUI _playerUI;
    private SoundEffectPlayer _sEPlayer;
    
    [SerializeField] private float _hitDistance = 4f;
    private float _hitDelay = 1f; // One second
    private bool _hitAllowed = true; 

    public bool testLineCast = true;
    
    private void Awake()
    {
        _playerLayerMask = LayerMask.GetMask("Player");
        _gameControl = FindObjectOfType<GameControl>().GetComponent<GameControl>();
        _animation = GetComponent<PlayerAnimation>();
        _playerUI = GetComponent<PlayerUI>();
        _sEPlayer = GetComponent<SoundEffectPlayer>();
        // playerClass = GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _cam = GetComponent<PlayerLook>().GetCam(); // because we already have a camera on PLayerLook script which is attached to the player gameobject
    }

    // Update is called once per frame
    void Update()
    {
        // if (testLineCast)
        // {
        //     Ray ray = new Ray(_cam.transform.position, _cam.transform.forward);
        //     Ray ray2 = new Ray(_cam.transform.position, _cam.transform.forward + _cam.transform.right/30);
        //     Ray ray3 = new Ray(_cam.transform.position, _cam.transform.forward - _cam.transform.right/30);
        //
        //     Debug.DrawRay(ray.origin, ray.direction * _hitDistance, Color.blue);
        //     Debug.DrawRay(ray2.origin, ray2.direction * _hitDistance, Color.green);
        //     Debug.DrawRay(ray3.origin, ray3.direction * _hitDistance, Color.red);
        // }
    }

    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Ray ray = new Ray(_cam.transform.position, _cam.transform.forward);
    //     // ray.origin + (ray.direction * _hitDistance)/2 -> To get the pivot of the cube to be between thw origin and the max hit distance
    //     Gizmos.DrawCube(ray.origin + (ray.direction * _hitDistance)/2, new Vector3(1, 1, 4));
    //     // Debug.Log("Forward: " + ray.direction);
    // }   

    // TODO: This function should be broken down to multiple funtions
    public void Hit()
    {
        if (!_hitAllowed) return;
        
        
        // I can make hit delay so it's matched with the animation -- maybe not :(
        _animation.IsHittingTrigger(); // for animation
        Transform camTransform = _cam.transform;        // TODO: I should take the cam Transform in the attribute instead of the whole object
        Vector3 camPos = camTransform.position;
        Vector3 camTransformForward = camTransform.forward;
        Vector3 camTransformRight = camTransform.right;
        Debug.Log("Hit attempted!");
        Ray ray = new Ray(camPos, camTransformForward);
        Ray ray2 = new Ray(_cam.transform.position, _cam.transform.forward + _cam.transform.right/10);
        Ray ray3 = new Ray(_cam.transform.position, _cam.transform.forward - _cam.transform.right/10);
        // Ray ray2 = new Ray(camPos, camTransformForward + camTransformRight/30);
        // Ray ray3 = new Ray(camPos, camTransformForward - camTransformRight/30);
        
        // Debug.DrawRay(ray.origin, ray.direction * _hitDistance, Color.red);
        Debug.DrawRay(ray.origin, ray.direction * _hitDistance, Color.red);
        Debug.DrawRay(ray.origin, ray2.direction * _hitDistance, Color.blue);
        Debug.DrawRay(ray.origin, ray3.direction * _hitDistance, Color.green);

        // to make the hit (ray) doesn't go through walls
        // _playerLayerMask this was deleted to make the ray consider other objects
        if (Physics.Raycast(ray, out RaycastHit hitTarget, _hitDistance, _playerLayerMask)) // || Physics.Raycast(ray2, out hitTarget, _hitDistance, _playerLayerMask) || Physics.Raycast(ray3, out hitTarget, _hitDistance, _playerLayerMask)
        {
            _sEPlayer.PlayHitMarker();
            // if (hitTarget.collider.gameObject.layer != _playerLayerMask)
            // {
            //     Debug.Log("Ray has hit another object!");
            // }
            // else
            Debug.Log("Ray has hit a player!");
            _gameControl.HasHit(gameObject, hitTarget.collider.gameObject);
        }
        else if (Physics.Raycast(ray2, out RaycastHit hitTarget2, _hitDistance, _playerLayerMask))
        {
            _sEPlayer.PlayHitMarker();
            Debug.Log("A player was Hit!");
            _gameControl.HasHit(gameObject, hitTarget2.collider.gameObject);
        }
        
        else if (Physics.Raycast(ray3, out RaycastHit hitTarget3, _hitDistance, _playerLayerMask))
        {
            _sEPlayer.PlayHitMarker();
            Debug.Log("A player was Hit!");
            _gameControl.HasHit(gameObject, hitTarget3.collider.gameObject);
        }
        else
        {
            _sEPlayer.PlayPunch();
        }

        _hitAllowed = false;
        Invoke(nameof(AllowHit), _hitDelay);
    }

    private void AllowHit()
    {
        _hitAllowed = true;
    }
    
    public void GetHit(Vector3 hitDirection)
    {
        // playerClass.ChangeColor(Color.red); Prefer to do this in the Player class cuz u might get hit from non tagger
        transform.position += hitDirection + Vector3.up / 4;
    }

    public void SetHitDelay(float time)
    {
        _hitDelay = time;
    }
}
