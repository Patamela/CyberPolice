using System;
using UnityEngine;

public class Gems : MonoBehaviour, ICollectible
{
    public static event Action<int> OnGemCollected;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private int idGem;
    [SerializeField] private int idLevel;

    
    private bool recogida;

    public void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        recogida = false;
    }

    public void Collect()
    {
        if(recogida){
            spriteRenderer.color = Color.gray; 
            return;
        }

        recogida = true;

        GameProgressManager.Instance.AddGem(idLevel, idGem);


        Destroy(gameObject);
        OnGemCollected?.Invoke(GameProgressManager.Instance.GetLevelGems(idLevel));
    }
}
