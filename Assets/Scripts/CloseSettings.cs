using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CloseSettings : MonoBehaviour
{
    private UIDocument _document;
    private Button _button;
    private List<Button> _menuButtons = new List<Button>();
    private AudioSource _audioSource;

    private void OnEnable()
    {
        _document = GetComponent<UIDocument>();
        _button = _document.rootVisualElement.Q("CloseButton") as Button;
        _button.RegisterCallback<ClickEvent>(CloseClick);

        _menuButtons = _document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnDisable()
    {
        if (_button != null)
        {
            _button.UnregisterCallback<ClickEvent>(CloseClick);
        }

        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void CloseClick(ClickEvent evt)
    {
        gameObject.SetActive(false);
        Debug.Log("Close button pressed");
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        _audioSource.Play();
    }
}
