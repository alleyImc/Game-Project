using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class TimerGame3 : MonoBehaviour
{
    private Label countdownLabel;
    public float countdownTime = 25f;

    private CountdownController countdownController;
    private bool isCountdownStarted = false;
    private bool gameEnded = false;

    void Start()
    {
        // find CountdownController
        countdownController = FindObjectOfType<CountdownController>();
        if (countdownController == null)
        {
            Debug.LogError("CountdownController not found in the scene.");
        }
    }

    void Update()
    {
        // wait till countdown finishes
        if (!isCountdownStarted && countdownController != null && countdownController.IsCountdownFinished())
        {
            var uiDocument = GetComponent<UIDocument>();
            var root = uiDocument.rootVisualElement;
            countdownLabel = root.Q<Label>("TimerLabel");

            StartCoroutine(Countdown());
            isCountdownStarted = true;
        }
    }

    IEnumerator Countdown()
    {
        while (countdownTime > 0 && !gameEnded)
        {
            countdownLabel.text = countdownTime.ToString("F1"); // Format to one decimal place
            yield return new WaitForSeconds(0.1f);
            countdownTime -= 0.1f;
        }

        if (!gameEnded)
        {
            countdownLabel.text = "0.0";
            // Perform any action when the countdown reaches zero
            GameOver3 gameOver = FindObjectOfType<GameOver3>();
            if (gameOver != null)
            {
                gameOver.ShowGameOverScreen(1); // Assuming player 1 wins if timer runs out
            }
            gameEnded = true;
        }
    }
}
