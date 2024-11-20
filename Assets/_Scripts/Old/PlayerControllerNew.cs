using UnityEngine;
using UnityEngine.InputSystem;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(CharacterController))]
public class PlayerControllerNew : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 3.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    
    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        jumped = context.action.triggered;
    }
    
    void Update()
    {
        Debug.Log(groundedPlayer);
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            Debug.Log("Is grounded");
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        // controller.Move(transform.rotation * move * Time.deltaTime * playerSpeed);  // I used transform.rotation to make the player moves in the direction it's facing
        controller.Move(transform.TransformDirection(move) * playerSpeed * Time.deltaTime);
        // transform.Translate(move * playerSpeed * Time.deltaTime);

            // This rotate the player to the direct they're facing 
        // if (move != Vector3.zero)
        // {
        //     gameObject.transform.forward = move;
        // }

        // Changes the height position of the player..
        if (jumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        // if (!groundedPlayer) transform.position += playerVelocity * Time.deltaTime;
    }
}
