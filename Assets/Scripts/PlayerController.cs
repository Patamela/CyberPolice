using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float torqueAmount = 1f;
    [SerializeField] private float boostSpeed = 35f;
    [SerializeField] private ParticleSystem snowEffect; // Particle effect to play when the player boosts
    // [SerializeField] private ParticleSystem boostEffect; // Particle effect to play when the player collides with the floor
    [SerializeField] private ScoreManager scoreManager; // Reference to the ScoreManager script to update the score

    SurfaceEffector2D surfaceEffector2D;
    Rigidbody2D rb;

    float baseSpeed;
    InputAction moveAction;
    Vector2 moveInput;
    float previousRotation; // Store the previous rotation of the player to calculate flips
    float totalRotation; // Store the total rotation of the player to calculate flips
    int flipCount; // Store the number of flips the player has performed

    private bool canControlPlayer = true; // Flag to control player input
    public bool CanControlPlayer {get => canControlPlayer; set => canControlPlayer = value;}

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindAnyObjectByType<SurfaceEffector2D>();
        baseSpeed = surfaceEffector2D.speed; // Store the initial speed of the SurfaceEffector2D
    }

    void Update()
    {
        if (!canControlPlayer) return; // If player control is disabled, exit the method
        
        PlayerTorque();
        BoostPlayer();
        CalculateFlips();
    }

    /// <summary>
    /// Calculates the number of flips the player has performed based on their rotation.
    /// </summary>
    private void CalculateFlips()
    {
        // Get the current rotation of the player in degrees
        float currentRotation = transform.rotation.eulerAngles.z; 

        // Calculate the change in rotation since the last frame
        totalRotation += Mathf.DeltaAngle(previousRotation, currentRotation); 
        if(Math.Abs(totalRotation) > 340) // If the player has rotated more than 340 degrees in either direction
        {
            flipCount++; // Increment the flip count
            scoreManager.AddScore(flipCount * 100); // Update the score
            totalRotation = 0; // Reset the total rotation
        }

        previousRotation = currentRotation; // Update the previous rotation for the next frame
    }   

    /// <summary>
    /// Applies torque to the player based on input from the Move action.
    /// </summary>
    void PlayerTorque()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        if (moveInput.x < 0)
        {
            rb.AddTorque(torqueAmount);
        }
        else if (moveInput.x > 0)
        {
            rb.AddTorque(-torqueAmount);
        }
    }

    /// <summary>
    /// Boosts the player's speed when the up arrow key is pressed. 
    /// </summary>
    private void BoostPlayer()
    {
        //Increase the player's speed when the up arrow key is pressed
        //Surface Effector speed is increased to boostSpeed
        if(moveInput.y > 0)
        {
            surfaceEffector2D.speed = boostSpeed;
            
        }
        else
        {
            surfaceEffector2D.speed = baseSpeed;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Floor");
        if(collision.gameObject.layer == layerIndex)
        {

            snowEffect.Play(); // Play the boost effect
            /*if (moveInput.y > 0)
            {
                if (!boostEffect.isPlaying)
                {
                    boostEffect.Play(); // Play the boost effect
                }
            }
            else
            {
                boostEffect.Stop(); // Stop the boost effect
            }
            */
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Floor");
        if(collision.gameObject.layer == layerIndex )
        {
            snowEffect.Stop(); // Stop the boost effect
        }
    }

}
