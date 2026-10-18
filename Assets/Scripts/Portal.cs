using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private HashSet<GameObject> portalObjects = new HashSet<GameObject>();

    [SerializeField] private Transform destination; // The destination where the player will be teleported


    private Transform player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.transform.parent;
        
        if (player == null) return;


        if (portalObjects.Contains(player.gameObject)) return;

        if(destination.TryGetComponent(out Portal destinationPortal))
        {
            destinationPortal.portalObjects.Add(player.gameObject);
        }

        player.transform.position = destination.position; // Teleport the player to the destination
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (player == null) return;

        portalObjects.Remove(player.gameObject);
    }
    
}
