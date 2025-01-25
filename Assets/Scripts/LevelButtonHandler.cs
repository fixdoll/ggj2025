using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButtonHandler : MonoBehaviour
{
    public GameObject Checkmark;
    public Image ThisImage;
    public Button ThisButton;
    public TextMeshProUGUI ThisButtonText;

    internal void ButtonInit(string levelName, bool current, bool locked)
    {
        ThisButtonText.text = levelName;
        if (locked) 
        {
            ThisImage.color = Color.black;
            return;
        }
        ThisButton.onClick.AddListener(() => SceneManager.LoadScene(levelName, LoadSceneMode.Single));
        if (!current) Checkmark.SetActive(true);
    }
}
