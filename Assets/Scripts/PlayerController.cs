using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float boostSpeed = 35f;
    [SerializeField] private ParticleSystem snowEffect; // Particle effect to play when the player boosts
    [SerializeField] private ScoreManager scoreManager; // Reference to the ScoreManager script to update the score
    [SerializeField] private float fuerzaSalto = 10f; // Force applied to the player when jumping

    SurfaceEffector2D surfaceEffector2D;
    Rigidbody2D rb;
    Collider2D magnetCollider;

    float baseSpeed;
    InputAction moveAction;
    Vector2 moveInput;
    bool inFloor;
    int coinsCount; // Store the number of coins the player has collected

    int activePowerUpsCount; // Track the number of active power-ups

    private bool canControlPlayer = true; // Flag to control player input
    public bool CanControlPlayer {get => canControlPlayer; set => canControlPlayer = value;}

    void Start()
    {
        transform.GetChild(0).GetChild(PlayerPrefs.GetInt("SelectedCharacter", 0)).gameObject.SetActive(true); // Activate the selected character based on PlayerPrefs

        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindAnyObjectByType<SurfaceEffector2D>();
        baseSpeed = surfaceEffector2D.speed; // Store the initial speed of the SurfaceEffector2D
        magnetCollider = transform.Find("MagnetCollider").GetComponent<Collider2D>(); // Get the reference to the magnet collider
    }

    void Update()
    {
        if (!canControlPlayer) return; // If player control is disabled, exit the method
        
        BoostPlayer();
        JumpPlayer();
    } 

    private void JumpPlayer()
    {
        if (
            Keyboard.current.spaceKey.wasPressedThisFrame &&
            inFloor
            )
        {
            Debug.Log("Player has jumped!");
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse); // Apply an upward force to the player for jumping
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


        if(collision.gameObject.layer == layerIndex )
        {
            inFloor = true;
            snowEffect.Play(); // Play the boost effect
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Floor");
        if(collision.gameObject.layer == layerIndex )
        {
            inFloor = false;
            snowEffect.Stop(); // Stop the boost effect
        }
    }

    public void ApplyPowerUp(PowerUpScriptableObject powerUpData)
    {
        activePowerUpsCount++; // Increment the count of active power-ups
        if(powerUpData.PowerUpType == "Speed")
        {
            baseSpeed += powerUpData.PowerUpValue; // Increase the base speed by the power-up value
            boostSpeed += powerUpData.PowerUpValue; // Increase the boost speed by the power-up value
        }

        if(powerUpData.PowerUpType == "Magnet")
        {
            magnetCollider.gameObject.SetActive(true); // Enable the magnet collider to attract coins
        }
    }

    public void DeactivatePowerUp(PowerUpScriptableObject powerUpData)
    {
        activePowerUpsCount--; // Decrement the count of active power-ups
        if(activePowerUpsCount == 0)
        {
            if(powerUpData.PowerUpType == "Speed")
            {
                baseSpeed -= powerUpData.PowerUpValue; // Decrease the base speed by the power-up value
                boostSpeed -= powerUpData.PowerUpValue; // Decrease the boost speed by the power-up value
            }

            if(powerUpData.PowerUpType == "Magnet")
            {
                magnetCollider.gameObject.SetActive(false); // Disable the magnet collider to stop attracting coins
            }
        }
    }
}
