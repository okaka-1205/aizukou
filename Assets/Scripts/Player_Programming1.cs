using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private float movespeed = 5.0f;//speed of the player movement
    [SerializeField] private float jumpforce = 5.0f;//force applied to the player when jumping
    private Rigidbody2D rb;//reference to the player's rigidbody component
    private bool isGrounded;//check if the player is on the ground
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
        Jump();
    }
    private void Walk()
    {
        float direction = Input.GetAxisRaw("Horizontal");//get the horizontal input axis (A/D or Left/Right arrow keys)
        rb.linearVelocity = new Vector2(direction * movespeed, rb.linearVelocity.y);//set the player's velocity based on the input and movespeed
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)//check if the space key is pressed and player is grounded
        {
            rb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);//apply an upward force to the player to make it jump
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }
}
