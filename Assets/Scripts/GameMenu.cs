using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Mirror;

public class GameMenu : MonoBehaviour
{
    private UIDocument _document;
    private Button _button1;
    private Button _button2;
    private Button _button3;
    private Button _button4;

    private List<Button> _GameButtons = new List<Button>();
    private AudioSource _audioSource;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        _button1 = _document.rootVisualElement.Q("Game1Button") as Button;
        _button2 = _document.rootVisualElement.Q("Game2Button") as Button;
        _button3 = _document.rootVisualElement.Q("Game3Button") as Button;
        _button4 = _document.rootVisualElement.Q("MainMenuButton") as Button;

        _button1.RegisterCallback<ClickEvent>(OnLevelOneChoose);
        _button2.RegisterCallback<ClickEvent>(OnLevelTwoChoose);
        _button3.RegisterCallback<ClickEvent>(OnLevelThreeChoose);
        _button4.RegisterCallback<ClickEvent>(OnMainMenuChoose);

        _GameButtons = _document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < _GameButtons.Count; i++)
        {
            _GameButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }

        _audioSource = GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        _button1.UnregisterCallback<ClickEvent>(OnLevelOneChoose);

        for (int i = 0; i < _GameButtons.Count; i++)
        {
            _GameButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnLevelOneChoose(ClickEvent evt)
    {
        Debug.Log("game 1 button pressed");
        if (NetworkServer.active)
        {
            NetworkManager.singleton.ServerChangeScene("Game_1");
        }
    }

    private void OnLevelTwoChoose(ClickEvent evt)
    {
        Debug.Log("game 2 button pressed");
        if (NetworkServer.active)
        {
            NetworkManager.singleton.ServerChangeScene("Game_2");
        }
    }

    private void OnLevelThreeChoose(ClickEvent evt)
    {
        Debug.Log("game 3 button pressed");
        if (NetworkServer.active)
        {
            NetworkManager.singleton.ServerChangeScene("Game_3");
        }
    }

    private void OnMainMenuChoose(ClickEvent evt)
    {
        Debug.Log("Main Menu button pressed");
        if (NetworkServer.active)
        {
            NetworkManager.singleton.ServerChangeScene("MainMenu");
        }
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        _audioSource.Play();
    }
}
