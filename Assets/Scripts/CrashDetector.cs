using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        int layerIndex = LayerMask.NameToLayer("Floor");

        if (other.gameObject.layer == layerIndex)
        {
            Debug.Log("Player has collided with the floor!");
            //TODO: You can add additional logic here, such as 
            // resetting the player's position or triggering a game over state.
            Invoke(nameof(ReloadScene), 1f); // Reload the scene after 1 second
        }
    }

    void ReloadScene()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
