using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int playerNumber;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private bool hasTag = false;
    [SerializeField] private GameObject stick;

    private float _horizontalInput;
    private float _verticalInput;
    private float _jumpInput;
    private Rigidbody _rb ;
    private Vector3 _movementInput;
    [SerializeField] private GameInput _gameInput;
    
    private float distToGround;
    public float maxDistance = 0.8f;
    public Vector3 boxSize;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Controller();
    }
    
    private void Controller()
    {
        Vector2 inputVector = _gameInput.GameMovementVectorNormalized();
        // if (playerNumber == 2) inputVector = _gameInput.GameMovementVectorNormalizedPlayer2();
        Vector3 moveDir = new Vector3(inputVector.x,0 , inputVector.y);
        transform.Translate(moveDir * Time.deltaTime * speed);
        
        if (Input.GetMouseButtonDown(0))
        {
            const float hitDistance = 2.5f;
            if (Physics.Raycast(transform.position, moveDir , out RaycastHit raycastHit, hitDistance))
            {
                Debug.Log(raycastHit.collider.gameObject.name);   
                if (raycastHit.collider.CompareTag("Player"))
                {
                    Debug.Log("hit a player");
                    GameObject hitPlayerObject = raycastHit.collider.gameObject;
                    hitPlayerObject.transform.position += transform.forward + Vector3.up / 4;
                    PassTag(hitPlayerObject.GetComponent<PlayerController>()); // we pass the tag to the player we hit
                }
            }
            /***  هذه فلسفة مال امها داعي:  ***/
            // RaycastHit hit;
            // Ray ray = Camera.allCameras[playerNumber-1].ScreenPointToRay(Input.mousePosition);
            // if (Physics.Raycast(ray, out hit, 2.5f))
            // {
            //     Debug.Log(hit.collider.gameObject.name);
            //     if (hit.collider.CompareTag("Player")) // same as hit.collider.gameObject.tag == "Player"
            //     {
            //         GameObject hitPlayerObject = hit.collider.gameObject;
            //         hitPlayerObject.transform.position += transform.forward + Vector3.up/4;
            //         PassTag(hitPlayerObject.GetComponent<PlayerController>());  // we pass the tag to the player we hit
            //         // hitPlayerObject.transform.position += Vector3.up/4;
            //         // hit.collider.gameObject.transform.position = Vector3.Slerp(hit.collider.gameObject.transform.position, transform.position,  Time.deltaTime * 10f);
            //         Debug.Log("hit a player");
            //     }
            // }
        }
    }

    // Legacy!!
    void OldMovementController()
    {
        /* For movement and jumping: */
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        // float mouseInput = Input.GetAxis("Fire1");
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) _rb.AddForce(transform.up * jumpForce, ForceMode.Impulse); // _rb.AddForce(0, jumpForce, 0);
        _movementInput = new Vector3(_horizontalInput * speed, _jumpInput * jumpForce, _verticalInput * speed);
        // transform.position += _movementInput * Time.deltaTime;
        transform.Translate(_movementInput * Time.deltaTime);
        
        /* for hitting: */
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 2.5f))
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.CompareTag("Player")) // same as hit.collider.gameObject.tag == "Player"
                {
                    GameObject hitPlayerObject = hit.collider.gameObject;
                    hitPlayerObject.transform.position += transform.forward + Vector3.up/4;
                    PassTag(hitPlayerObject.GetComponent<PlayerController>());  // we pass the tag to the player we hit
                    // hitPlayerObject.transform.position += Vector3.up/4;
                    // hit.collider.gameObject.transform.position = Vector3.Slerp(hit.collider.gameObject.transform.position, transform.position,  Time.deltaTime * 10f);
                    Debug.Log("hit a player");
                }
            }
        }
    }
    
    private bool IsGrounded()
    {
        // distToGround = GetComponent<Collider>().bounds.extents.y;
        // Physics.Raycast(transform.position, -Vector3.up, 0.1);
        // RaycastHit hit;
        // return Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), maxDistance);
        return Physics.BoxCast(transform.position, boxSize, transform.TransformDirection(-Vector3.up), transform.rotation, maxDistance);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Gizmos.DrawCube(transform.position-transform.up * distToGround, boxSize);
        // Gizmos.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * 1.2f);
        // Debug.Log(transform.TransformDirection(-Vector3.up) + " maxDistance: " + maxDistance);
        Gizmos.DrawCube(transform.position + (transform.TransformDirection(-Vector3.up) * maxDistance), boxSize);
    }

    private void PassTag(PlayerController hitPlayerGameObject)
    {
        // we pass the tag to the player we hit
        SetHasTag(false);
        hitPlayerGameObject.SetHasTag(true);  
        DeactiveStick();
        hitPlayerGameObject.ActiveStick();
    }

    public void SetHasTag(bool paramHasTag)
    {
        this.hasTag = paramHasTag;
    }

    public void ActiveStick()
    {
        stick.SetActive(true);
    }
    
    public void DeactiveStick()
    {
        stick.SetActive(false);
    }
}
