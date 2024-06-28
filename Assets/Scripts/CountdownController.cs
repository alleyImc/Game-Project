using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CountdownController : MonoBehaviour
{
    public UIDocument uiDocument;
    private VisualElement countdownPanel;
    private Label countdownLabel;
    public float countdownDuration = 5f;
    private float timer;
    private bool countdownFinished = false;

    void Start()
    {
        var root = uiDocument.rootVisualElement;

        countdownPanel = root.Q<VisualElement>("CountdownBorder");
        countdownLabel = root.Q<Label>("CountdownLabel");

        if (countdownLabel != null)
        {
            StartCountdown();
        }
        else
        {
            Debug.LogError("Countdown label is not assigned or found in the UI.");
        }
    }

    void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            UpdateCountdownUI();
        }
        else
        {
            FinishCountdown();
        }
    }

    void StartCountdown()
    {
        timer = countdownDuration;
        UpdateCountdownUI();
    }

    void UpdateCountdownUI()
    {
        if (countdownLabel != null)
        {
            countdownLabel.text = Mathf.Ceil(timer).ToString();
        }
    }

    void FinishCountdown()
    {
        Debug.Log("Countdown finished! Start the game now.");
        countdownFinished = true;

        // hide countdown when it finishes
        if (countdownPanel != null)
        {
            countdownPanel.style.display = DisplayStyle.None;
        }
    }

    public bool IsCountdownFinished()
    {
        return countdownFinished;
    }
}

