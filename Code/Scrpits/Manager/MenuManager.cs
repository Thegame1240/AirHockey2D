using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject difficultyToggles;
    [SerializeField] private Button startButton;
    
    
    private void Start()
    {
        print(Display.main.systemWidth + "X" + Display.main.systemHeight);
        difficultyToggles.transform.GetChild((int)GameValue.Difficulty).GetComponent<Toggle>().isOn = true;
        startButton.onClick.AddListener(LoadGame);
    }

    private void LoadGame()
    {
        SceneManager.LoadScene("Main");
    }
    
    #region Difficulty

    public void SetEasyDifficulty(bool isOn)
    {
        if (isOn)
        {
            GameValue.Difficulty = GameValue.Difficulties.Easy;
        }
    }
    
    public void SetMediumDifficulty(bool isOn)
    {
        if (isOn)
        {
            GameValue.Difficulty = GameValue.Difficulties.Medium;
        }
    }
    
    public void SetHardDifficulty(bool isOn)
    {
        if (isOn)
        {
            GameValue.Difficulty = GameValue.Difficulties.Hard;
        }
    }

    #endregion
}
