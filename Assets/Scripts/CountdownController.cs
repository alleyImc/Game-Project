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
        // Geri say�m metnini g�ncelle
        if (countdownLabel != null)
        {
            countdownLabel.text = Mathf.Ceil(timer).ToString();
        }
    }

    void FinishCountdown()
    {
        Debug.Log("Countdown finished! Start the game now.");

        // Geri say�m bitti�inde geri say�m panelini sakla veya kald�r
        if (countdownPanel != null)
        {
            countdownPanel.style.display = DisplayStyle.None;
        }

        
    }
}
