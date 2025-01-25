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

    void Start()
    {
        PlayButton.onClick.AddListener(ShowLevels);
    }

    private void ShowLevels()
    {
        PlayButton.gameObject.SetActive(false);
        foreach(var level in Levels)
        {
            var button = Instantiate(LevelButton, LevelView.transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = level;
            button.GetComponent<Button>().onClick.AddListener(() => { LaunchScene(level); }) ;
        }
    }

    private void LaunchScene(string levelName)
    {
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }
}
