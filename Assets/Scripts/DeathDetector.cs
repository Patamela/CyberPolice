using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathDetector : MonoBehaviour
{
    [SerializeField] private float reloadDelay = 1f; // Delay before reloading the scene

    void OnTriggerEnter2D(Collider2D other)
    {

        int layerIndex = LayerMask.NameToLayer("Player");
        Debug.Log("layerIndex:"+layerIndex);
        if (other.gameObject.layer == layerIndex)
        {
            Invoke(nameof(ReloadScene), reloadDelay); // Reload the scene after the specified delay
        }
    }

    void ReloadScene()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
