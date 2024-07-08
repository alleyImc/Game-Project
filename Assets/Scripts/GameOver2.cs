using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Mirror;

public class GameOver2 : MonoBehaviour
{
    private UIDocument _document;
    private Button _retryButton;
    private Button _menuButton;
    private List<Button> _gameOverButtons = new List<Button>();
    private Label _wonLabel; //winner label

    private string mainMenuSceneName = "ChooseGame";
    private string currentSceneName = "Game_2";

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        // Retry button
        _retryButton = _document.rootVisualElement.Q<Button>("retryButton");
        _retryButton.RegisterCallback<ClickEvent>(RetryClick);

        // Menu button
        _menuButton = _document.rootVisualElement.Q<Button>("MenuButton");
        _menuButton.RegisterCallback<ClickEvent>(MenuClick);

        _gameOverButtons = _document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < _gameOverButtons.Count; i++)
        {
            _gameOverButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }

        // find winner label
        _wonLabel = _document.rootVisualElement.Q<Label>("WonLabel");

        // hide gameover 
        _document.rootVisualElement.style.display = DisplayStyle.None;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "Game_2")
        {
            this.enabled = false; // Disable this script if not in Game_2
            return;
        }
        else
        {
            this.enabled = true;
        }
    }

    private void OnDisable()
    {
        _retryButton.UnregisterCallback<ClickEvent>(RetryClick);
        _menuButton.UnregisterCallback<ClickEvent>(MenuClick);

        for (int i = 0; i < _gameOverButtons.Count; i++)
        {
            _gameOverButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void RetryClick(ClickEvent evt)
    {
        Debug.Log("Retry button pressed");

        if (NetworkServer.active)
        {
            NetworkManager.singleton.ServerChangeScene("Game_2");
        }
    }

    private void MenuClick(ClickEvent evt)
    {
        Debug.Log("Menu button pressed");

        if (NetworkServer.active)
        {
            NetworkManager.singleton.ServerChangeScene("ChooseGame");
        }
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        //add audio
    }

    public void ShowGameOverScreen(uint winnerId)
    {
        _document.rootVisualElement.style.display = DisplayStyle.Flex;
        if (winnerId == 0)
        {
            _wonLabel.text = "1. Oyuncu WON!!!";
        }
        else if (winnerId == 1)
        {
            _wonLabel.text = "2. Oyuncu WON!!!";
        }
    }
}
