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
    public List<string> LevelsToLoad;
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

        GameFlowController.Instance.Init(LevelsToLoad, progress);
        DontDestroyOnLoad(GameFlowController.Instance);

        LevelSelectButton.onClick.AddListener(ShowLevels);


        PlayButton.onClick.AddListener(() => { GameFlowController.Instance.LoadNextLevel(); });

    }

    private void ShowLevels()
    {
        LevelSelectButton.gameObject.SetActive(false);
        PlayButton.gameObject.SetActive(false);
        for (int i = 0; i < LevelsToLoad.Count; i++)
        {
            string level = LevelsToLoad[i];
            var button = Instantiate(LevelButton, LevelView.transform);
            bool current = i == progress;
            bool locked = i > progress;

            button.GetComponent<LevelButtonHandler>().ButtonInit(level, current, locked);
        }
    }
}
