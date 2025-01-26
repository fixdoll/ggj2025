using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowController : MonoBehaviour
{
    public static GameFlowController Instance;

    public List<string> Levels;
    public int CurrentLevel;

    private bool gameLaunched = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void Init(List<string> levelsToLoad, int progress)
    {
        Levels = new List<string>();
        foreach (var level in levelsToLoad)
            Levels.Add(level);

        CurrentLevel = progress;
    }

        public bool checkGameLaunched()
    {
        if (!gameLaunched)
        {
            gameLaunched = true;
            return false;
        }
        return gameLaunched;
    }

    public void FinishLevel()
    {
        CurrentLevel++;
        PlayerPrefs.SetInt("Progress", CurrentLevel);

        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        LaunchScene(Levels[CurrentLevel]);
    }

    private void LaunchScene(string levelName)
    {
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }
}
