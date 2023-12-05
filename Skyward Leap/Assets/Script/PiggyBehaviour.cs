// Importing necessary Unity libraries
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections; // For using coroutines

public class PlayerController : MonoBehaviour
{
    // Public variables for setting player's max speed, jump force, ground check, etc.
    public float maxSpeed = 7f;
    public float jumpForce = 400f;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public ScoreManager scoreManager; // Reference to a ScoreManager script for score handling

    // Private variables for internal state management like player orientation and grounding status
    private bool facingRight = true;
    private bool grounded = false;
    private bool isDead = false; // Flag for checking if the player is dead
    private float groundRadius = 0.1f;
    private Rigidbody2D rb; // Rigidbody component for physics calculations
    private Camera mainCamera; // Reference to the main camera

    // Prefabs for displaying messages
    public GameObject deathMessagePrefab;
    public GameObject finishedGameMessagePrefab;
    public GameObject completionMessagePrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Getting and storing the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        // Getting the main camera from the scene
        mainCamera = Camera.main;
    }

    // FixedUpdate is called once per frame, used for physics calculations
    void FixedUpdate()
    {
        // Check if the player is grounded using a circle overlap check at groundCheck position
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        // Get horizontal input for movement
        float move = Input.GetAxis("Horizontal");
        // Apply the movement to the rigidbody
        rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y);

        // Flip the player based on movement direction
        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
    }

    // Update is called once per frame, used for non-physics updates
    void Update()
    {
        // Skip updates if the player is dead
        if (isDead)
        {
            return;
        }

        // Handle jumping when grounded and space key is pressed
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, jumpForce));
            // Play jump sound effect
            SoundManager.Instance.PlaySound("jump");
        }

        // Reduce the y velocity when not holding space (for jumping mechanics)
        if (!grounded && rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        // Call Die() if the player falls below the camera's view
        if (mainCamera.WorldToViewportPoint(transform.position).y < 0)
        {
            Die();
        }

        // Update the score based on player's height if not dead
        if (scoreManager != null && !isDead)
        {
            int playerHeight = Mathf.FloorToInt(transform.position.y);
            scoreManager.UpdateScore(playerHeight);
        }
    }

    private IEnumerator SetParent(GameObject parent)
    {
    yield return null; // Wait for the end of the frame
    this.transform.parent = parent.transform;
    }

    // OnCollisionEnter2D is called when this collider/rigidbody has begun touching another rigidbody/collider
    private void OnCollisionEnter2D(Collision2D col)
    {
        // Handle collision with a platform
        if (col.gameObject.tag == "Platform" || col.gameObject.tag == "MovingPlatform")
        {
            Platform platform = col.gameObject.GetComponent<Platform>();
            // Check if the platform is the last one and handle accordingly
            if (platform != null && platform.IsLastPlatform)
            {
                HandleLastPlatformCollision();
            }
        }
        if (col.gameObject.tag == "MovingPlatform")
        {
        StartCoroutine(SetParent(col.gameObject));
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
    if (col.gameObject.tag == "MovingPlatform")
    {
        this.transform.parent = null;
    }
    }   

   private void OnTriggerEnter2D(Collider2D collider)
    {
        // Check if collided with a coin
        if (collider.gameObject.CompareTag("Coin"))
        {
            SoundManager.Instance.PlaySound("coin"); // Plays Coin Sound
            scoreManager.AddToScore(20); // Update score by 20 points
            Destroy(collider.gameObject); // Destroy the coin
        }
        // Check if collided with a saw
        else if (collider.gameObject.CompareTag("Saw"))
        {
            SoundManager.Instance.PlaySound("sawInteraction");
            Die(); // Call the Die method
        }
    }

    // Handles the player's actions when colliding with the last platform
   private void HandleLastPlatformCollision()
    {
    StartCoroutine(ShowCompletionMessageWithDelay());
    }

private IEnumerator ShowCompletionMessageWithDelay()
{
    yield return new WaitForSeconds(0.3f);

    if (SceneManager.GetActiveScene().name == "Level 1")
    {
        Instantiate(completionMessagePrefab, transform.position, Quaternion.identity);
    }
    else if (SceneManager.GetActiveScene().name == "Level 2")
    {
        Instantiate(finishedGameMessagePrefab, transform.position, Quaternion.identity);
    }

    SoundManager.Instance.PlaySound("levelComplete");
}

    // Flips the player sprite to face the opposite direction
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    // Method to handle the player's death
    public void Die()
    {
        isDead = true;
        // Play death sound effect
        SoundManager.Instance.PlaySound("death");
        // Instantiate a death message at player's position
        GameObject deathMessageInstance = Instantiate(deathMessagePrefab, transform.position, Quaternion.identity);
        Text deathMessageText = deathMessageInstance.GetComponentInChildren<Text>();

        // Display the current score in the death message
        if (deathMessageText != null && scoreManager != null)
        {
            deathMessageText.text = "Score: " + scoreManager.GetCurrentScore();
        }

        // Disable player's movement and physics
        this.enabled = false;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        Time.timeScale = 0;
    }

    // Method to restart the game
    public void RestartGame()
    {
        // Reset the score if ScoreManager is available
        Time.timeScale = 1;
        if (scoreManager != null)
        {
            scoreManager.ResetScore();
        }
        

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Method to return to the main menu
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameStart");
    }
}


//This script is what makes my player character, which I call 'Piggy', come to life in my game. 
//It controls how fast Piggy can run, how high it can jump, and checks if it's on the ground. 
//It also keeps track of Piggy's score and handles what happens when Piggy runs into 
//different things in the game, like platforms or saws. Plus, it deals with what to do when
// Piggy finishes a level or, unfortunately, if it dies.