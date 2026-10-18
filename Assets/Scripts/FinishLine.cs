using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private float reloadDelay = 1f; // Delay before reloading the scene
   
    private const string KEY_UNLOCKED_LEVEL = "UnlockedLevel";
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has reached the finish line!");

            Invoke(nameof(NextLevel), reloadDelay); // Load the next level after the specified delay
        }
    }

    void NextLevel()
    {
        int unlockedLevel = PlayerPrefs.GetInt(KEY_UNLOCKED_LEVEL, 1);
        
        if(unlockedLevel >= 2) unlockedLevel = 1;

        PlayerPrefs.SetInt(KEY_UNLOCKED_LEVEL, unlockedLevel + 1); 
    
        PlayerPrefs.Save(); // Save the updated unlocked level to PlayerPrefs


        Debug.Log("Unlocked level: "+PlayerPrefs.GetInt(KEY_UNLOCKED_LEVEL));
        
        SceneManager.LoadScene("Levels");
        
    }
}
