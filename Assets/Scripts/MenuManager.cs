using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button PlayButton;
    public List<string> Levels;
    public HorizontalLayoutGroup LevelView;
    public GameObject LevelButton;

    public static MenuManager Instance;

    private bool firstLoad;
    private int progress;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        progress = PlayerPrefs.GetInt("Progress", 0);

        if (GameFlowController.Instance.checkGameLaunched())
        {
            ShowLevels();
        }
        else
        {
            DontDestroyOnLoad(GameFlowController.Instance);
            PlayButton.onClick.AddListener(ShowLevels);
        }

    }

    private void ShowLevels()
    {
        PlayButton.gameObject.SetActive(false);
        for (int i = 0; i < Levels.Count; i++)
        {
            string level = Levels[i];
            var button = Instantiate(LevelButton, LevelView.transform);
            bool current = i == progress;
            bool locked = i > progress;

            button.GetComponent<LevelButtonHandler>().ButtonInit(level, current, locked);
        }
    }

    private void LaunchScene(string levelName)
    {
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }
}
