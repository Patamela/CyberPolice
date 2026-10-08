using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private float reloadDelay = 1f; // Delay before reloading the scene
    [SerializeField] private ParticleSystem finishEffect; // Particle effect to play when the player reaches the finish line
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has reached the finish line!");
            finishEffect.Play(); // Play the finish effect

            

            Invoke(nameof(NextLevel), reloadDelay); // Load the next level after the specified delay
        }
    }

    void NextLevel()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel + 1); 
        // Load the next level
        if(unlockedLevel > 6)
        {
            SceneManager.LoadScene("Menu"); //TODO: Go to Menu
        }
        else
        {
            SceneManager.LoadScene($"Level{unlockedLevel + 1}"); //TODO: Go to Win Scene
        }
        
    }
}
