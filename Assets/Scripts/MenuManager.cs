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
    public Button LevelSelectButton;
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

        LevelSelectButton.onClick.AddListener(ShowLevels);

        PlayButton.GetComponentInChildren<TextMeshProUGUI>().text = progress == 0 ? "Play" : "Continue";
        PlayButton.onClick.AddListener(() => { LaunchScene(Levels[progress]); });

    }

    private void ShowLevels()
    {
        LevelSelectButton.gameObject.SetActive(false);
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
