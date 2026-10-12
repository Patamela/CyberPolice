using UnityEngine;

public class Magnet : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Coin>(out Coin coin))
        {
            coin.SetTargetPosition(transform.parent.position); // Set the target position of the coin to the magnet's position
        }
    }
}
