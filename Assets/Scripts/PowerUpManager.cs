using System;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private PowerUpScriptableObject powerUpData;
    [SerializeField] private ParticleSystem powerUpEffect;
   
    PlayerController playerController;
    SpriteRenderer powerUpSpriteRenderer;

    float timeLeft;

    private void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        powerUpSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        timeLeft = powerUpData.TimeLimit;
    }

    private void Update()
    {
        CountDownPowerUpTime();
    }

    /// <summary>
    /// Counts down the time left for the active power-up and deactivates it when the time runs out.
    /// </summary>
    private void CountDownPowerUpTime()
    {
        if(powerUpSpriteRenderer.enabled == false)
        {
            if(timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;

                if(timeLeft <= 0)
                {
                    playerController.DeactivatePowerUp(powerUpData);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (powerUpSpriteRenderer.enabled)
        {
            powerUpSpriteRenderer.enabled = false;
            playerController.ApplyPowerUp(powerUpData);
            powerUpEffect.Play();

        }
    }

}
