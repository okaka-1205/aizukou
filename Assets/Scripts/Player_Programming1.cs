using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private float movespeed = 5.0f;//speed of the player movement
    [SerializeField] private float jumpforce = 5.0f;//force applied to the player when jumping
    private Rigidbody2D rb;//reference to the player's rigidbody component
    private bool isGrounded;//check if the player is on the ground
    private InputActionAsset inputActions;
    private InputActionMap playerMap;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = Resources.Load<InputActionAsset>("InputSystem_Actions");
        playerMap = inputActions.FindActionMap("Player");
        inputActions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
        Jump();
    }
    private void Walk()
    {
        float direction = playerMap.FindAction("Move").ReadValue<Vector2>().x;//get the horizontal input axis from Input System
        rb.linearVelocity = new Vector2(direction * movespeed, rb.linearVelocity.y);//set the player's velocity based on the input and movespeed
    }
    private void Jump()
    {
        if (playerMap.FindAction("Jump").WasPerformedThisFrame() && isGrounded)//check if the jump action is pressed and player is grounded
        {
            rb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);//apply an upward force to the player to make it jump
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnDisable()
    {
        if (inputActions != null)
        {
            inputActions.Disable();
        }
    }
}
