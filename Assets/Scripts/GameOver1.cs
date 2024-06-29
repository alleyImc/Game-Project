using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOver1 : MonoBehaviour
{
    private UIDocument _document;
    private Button _retryButton;
    private Button _menuButton;
    private List<Button> _gameOverButtons = new List<Button>();

    private string mainMenuSceneName = "ChooseGame";
    private string currentSceneName = "Game_1";

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

        // Ba�lang��ta Game Over ekran�n� gizle
        _document.rootVisualElement.style.display = DisplayStyle.None;
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneName);
    }

    private void MenuClick(ClickEvent evt)
    {
        Debug.Log("Menu button pressed");
        UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenuSceneName);
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        //add audio
    }

    public void ShowGameOverScreen()
    {
        _document.rootVisualElement.style.display = DisplayStyle.Flex;
    }
}

