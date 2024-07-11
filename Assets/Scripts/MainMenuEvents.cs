using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;
    private Button _buttonStart;
    private Button _buttonSettings;
    private Button _buttonQuit;
    private List<Button> _menuButtons = new List<Button>();
    private AudioSource _audioSource;

    public string sceneName;

    public GameObject settingsButton;

    private void Awake()
    {

        _document = GetComponent<UIDocument>();

        //start button
        _buttonStart = _document.rootVisualElement.Q("StartButton") as Button;
        _buttonStart.RegisterCallback<ClickEvent>(StartClick);

        //setting button
        _buttonSettings = _document.rootVisualElement.Q("SettingsButton") as Button;
        _buttonSettings.RegisterCallback<ClickEvent>(SettingsClick);

        _buttonQuit = _document.rootVisualElement.Q("QuitButton") as Button;
        _buttonQuit.RegisterCallback<ClickEvent>(QuitClick);

        _menuButtons = _document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }

        _audioSource = GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        _buttonStart.UnregisterCallback<ClickEvent>(StartClick);

        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void StartClick(ClickEvent evt)
    {

        Debug.Log("start button pressed");
        if (NetworkServer.active)
        {
            NetworkManager.singleton.ServerChangeScene("ChooseGame");
        }

    }

    private void SettingsClick(ClickEvent evt)
    {
        Debug.Log("settings button pressed");
        settingsButton.SetActive(true);
    }
    private void QuitClick(ClickEvent evt)
    {
        Debug.Log("quit button pressed");
        Application.Quit();
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        _audioSource.Play();
    }
}
