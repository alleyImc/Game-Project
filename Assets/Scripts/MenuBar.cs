using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Mirror;

public class MenuBar : MonoBehaviour
{
    private UIDocument _document;
    private Button _button;
    private List<Button> _menuButtons = new List<Button>();
    private AudioSource _audioSource;

    public string sceneName;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        _button = _document.rootVisualElement.Q("MenuButton") as Button;
        _button.RegisterCallback<ClickEvent>(OnButtonClick);

        _menuButtons = _document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnDisable()
    {
        _button.UnregisterCallback<ClickEvent>(OnButtonClick);

        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnButtonClick(ClickEvent evt)
    {
        Debug.Log("menu button pressed");
        sceneName = "MainMenu";
        if (NetworkServer.active)
        {
            NetworkManager.singleton.ServerChangeScene(sceneName);
        }
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        _audioSource.Play();
    }
}