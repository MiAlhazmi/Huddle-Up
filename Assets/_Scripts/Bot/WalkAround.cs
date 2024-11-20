using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class WalkAround : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField] private Camera _cam;

    [SerializeField] private float DestinationDistance = 30f;
    [SerializeField] private float ObstacleDistance = 3f;
    [SerializeField] private Vector3 _destination;
    [SerializeField] private bool destinationFound = false;
    // [SerializeField] private GameObject blockedArea;

    private Vector3 _playerVelocity = Vector3.zero;
    private const float Gravity = -9.8f;
    private bool _isGrounded;

    [SerializeField] private float _speed = 5f;

    
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _destination = transform.position; // there is no destination at the beginning
        ProcessDestination();
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = _controller.isGrounded;
        // this for x only
        // if ( (int) (transform.position.x / _destination.x) == 0)
        // {
        //     Debug.Log("1.0: transform.position.x / _destination.x = " + (int) (transform.position.x / _destination.x));
        //     if (transform.position.x % _destination.x > _destination.x - 3)
        //     {
        //         Debug.Log("1.5: transform.position.x % _destination.x = " + (int) (transform.position.x / _destination.x));
        //         ProcessDestination();
        //         Debug.Log("1.99: destination was processed");
        //     }
        // }
        // else if ((int)(transform.position.x / _destination.x) == 1)
        // {
        //     Debug.Log("2.0: transform.position.x / _destination.x = " + (int) (transform.position.x / _destination.x));
        //     if (transform.position.x % _destination.x > 3)
        //     {
        //         Debug.Log("2.5: transform.position.x % _destination.x = " + (int) (transform.position.x / _destination.x));
        //         ProcessDestination();
        //         Debug.Log("2.99: destination was processed");
        //     }
        // }
        ApplyGravity();
        ProcessDestination();
        SprintForward();
    }

    private void ApplyGravity()
    {
        _playerVelocity.y += Gravity * Time.deltaTime;
        if (_isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2f;
        }
        _controller.Move(_playerVelocity * Time.deltaTime);
    }
    private void ProcessDestination()
    {
        Ray ray = new Ray(_cam.transform.position, _cam.transform.forward);
        // if obstacle found in front turn
        if (Physics.Raycast(ray, out RaycastHit obstacleHitInfo, ObstacleDistance))
        {
            destinationFound = false;
            Turn();
        }
        Debug.DrawRay(ray.origin, ray.direction * DestinationDistance, Color.blue);
        if (Physics.Raycast(ray, out RaycastHit destinationHitInfo, DestinationDistance, LayerMask.GetMask("Default")))
        {
            _destination = destinationHitInfo.transform.position;
            destinationFound = true;
        }
    }
    
    // to turn 90 degrees if an obstacle is found
    private void Turn()
    {
        Ray rightRay = new Ray(_cam.transform.position, _cam.transform.right);
        Ray leftRay = new Ray(_cam.transform.position, -_cam.transform.right);
        Debug.DrawRay(rightRay.origin, rightRay.direction * ObstacleDistance, Color.yellow);
        if (!Physics.Raycast(rightRay, ObstacleDistance) && !Physics.Raycast(leftRay, ObstacleDistance))
        {
            transform.Rotate(Vector3.up, Random.Range(10, 350));
            // ProcessDestination();
        }
        if (!Physics.Raycast(rightRay, ObstacleDistance))
        {
            transform.Rotate(Vector3.up, Random.Range(20, 170));
            // ProcessDestination();
        }
        else
        {
            transform.Rotate(Vector3.up, Random.Range(-20, -170));
            // ProcessDestination();
        }
    }

    private void TrunBack()
    {
        transform.Rotate(Vector3.up, 180f);
    }

    // To sprint forward
    private void SprintForward()
    {
        if (!destinationFound) return;
 
        // if (Vector3.Distance(transform.position, blockedArea.transform.position) < 4)
        // {
        //     // to check if the bot is facing towards the block area:
        //     float dot = Vector3.Dot(transform.forward, (blockedArea.transform.position - transform.position).normalized);
        //     if (dot > 0.7f)
        //     {
        //         Debug.Log("Faced blockedArea!");
        //         TrunBack();
        //         destinationFound = false;
        //     }
        // }
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(_destination.x, transform.position.y, _destination.z), _speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, _destination) < 2) destinationFound = false;
    }
}
