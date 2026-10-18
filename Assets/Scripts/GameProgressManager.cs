using System.Collections.Generic;
using UnityEngine;

public class GameProgressManager : MonoBehaviour
{
    private Dictionary<int,HashSet<int>> Gems {get; set;}
    private static GameProgressManager _instance;

    private const int TOTAL_GEMS_LEVEL = 3;
    private const int FINAL_LEVEL = 3;
    private const string KEY_UNLOCKED_FINAL_LEVEL = "UnlockedFinalLevel";
    private const string KEY_GEMS = "Gem";

    public GameProgressManager()
    {

    }

    public static GameProgressManager Instance {
        get {  return _instance; }
    }

    private void Awake()
    {   

        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        Gems = new Dictionary<int, HashSet<int>>();

        LoadProgress();
    }

    public void AddGem(int idLevel, int idGem)
    {
        if (!Gems.ContainsKey(idLevel))
        {
            Gems[idLevel] = new HashSet<int>();
        }

       bool added = Gems[idLevel].Add(idGem);

        if (!added)
        {
            Debug.Log("This Gem is collected");
        }

        SaveProgress();

        if (CompletedLevel(idLevel))
        {
            Debug.Log($"Level {idLevel} completed!");
        }

        CheckFinalLevelUnlock();
    }

    public int GetLevelGems(int idLevel)
    {
        if (!Gems.ContainsKey(idLevel)) return 0;

        return Gems[idLevel].Count;
    }

    public bool GemCollected(int idLevel, int idGem)
    {
        if(!Gems.ContainsKey(idLevel)) return false;

        return Gems[idLevel].Contains(idGem);
    }

    public bool CompletedLevel(int idLevel)
    {
        return GetLevelGems(idLevel) >= TOTAL_GEMS_LEVEL;
    }

    public bool AllCompletedLevels()
    {
        for(int i = 1; i < FINAL_LEVEL; i++)
        {
            if (!CompletedLevel(i))
            {
                return false;
            }
        }

        return true;
    }

    private void CheckFinalLevelUnlock()
    {
        if (AllCompletedLevels())
        {
            PlayerPrefs.SetInt(KEY_UNLOCKED_FINAL_LEVEL, 1);
            PlayerPrefs.Save();

            Debug.Log("Final Level Unlocked");
        }
    }

    public bool FinalLevelUnlocked()
    {
        return PlayerPrefs.GetInt(KEY_UNLOCKED_FINAL_LEVEL, 0) == 1;
    }

    private void SaveProgress()
    {
        List<string> savedGems = new List<string>();

        foreach(KeyValuePair<int, HashSet<int>> level in Gems)
        {
            foreach(int idGem in level.Value)
            {
                savedGems.Add($"{level.Key}:{idGem}");
            }
        }

        string data = string.Join(",",savedGems);

        PlayerPrefs.SetString(KEY_GEMS, data);

        PlayerPrefs.Save();
    }

    private void LoadProgress()
    {
        Gems.Clear();

        string data = PlayerPrefs.GetString(KEY_GEMS, "");

        if (string.IsNullOrEmpty(data))
        {
            return;
        }

        string[] savedGems = data.Split(',');

        foreach(string gem in savedGems)
        {
            string[] id = gem.Split(':');

            if(id.Length != 2) continue;

            if(!int.TryParse(id[0], out int idLevel)) continue;

            if(!int.TryParse(id[1], out int idGem)) continue;

            if(idLevel < 1 || idLevel > FINAL_LEVEL) continue;

            if(idGem < 1 || idGem > TOTAL_GEMS_LEVEL) continue;

            if (!Gems.ContainsKey(idLevel))
            {
                Gems[idLevel] = new HashSet<int>();
            }

            Gems[idLevel].Add(idGem);
        }
    }



}
