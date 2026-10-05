using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float torqueAmount = 1f;
    [SerializeField] private float boostSpeed = 35f;
    [SerializeField] private ParticleSystem snowEffect; // Particle effect to play when the player boosts
    // [SerializeField] private ParticleSystem boostEffect; // Particle effect to play when the player collides with the floor

    private bool canControlPlayer = true; // Flag to control player input
    public bool CanControlPlayer {
        get => canControlPlayer;
        set => canControlPlayer = value; 
    }

    SurfaceEffector2D surfaceEffector2D;

    float baseSpeed;

    InputAction moveAction;
    Vector2 moveInput;

    Rigidbody2D rb;

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
