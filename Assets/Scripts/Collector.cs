using UnityEngine;

/// <summary>
/// This class is responsible for detecting when the player collides with a collectible item and triggering the collection process.
/// </summary>
public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectible collectible = collision.GetComponent<ICollectible>();
        if (collectible != null)
        {
            collectible.Collect();
        }
    }
}
