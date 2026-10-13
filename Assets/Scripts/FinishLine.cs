using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private float reloadDelay = 1f; // Delay before reloading the scene
    [SerializeField] private ParticleSystem finishEffect; // Particle effect to play when the player reaches the finish line

    private const string KEY_UNLOCKED_LEVEL = "UnlockedLevel";
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
        int unlockedLevel = PlayerPrefs.GetInt(KEY_UNLOCKED_LEVEL, 1);
        
        if(unlockedLevel >= 5) unlockedLevel = 4;

        PlayerPrefs.SetInt(KEY_UNLOCKED_LEVEL, unlockedLevel + 1); 
    
        PlayerPrefs.Save(); // Save the updated unlocked level to PlayerPrefs


        Debug.Log("Nivel desblouqado: "+PlayerPrefs.GetInt(KEY_UNLOCKED_LEVEL));
        
        SceneManager.LoadScene("Levels");
        
    }
}
