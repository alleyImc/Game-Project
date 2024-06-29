using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimerController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] float remainingTime;
    [SerializeField] bool isCountDownFinished;
    [SerializeField] Image countdownPopupImage;

    void Start()
    {
        remainingTime = 5f;
        isCountDownFinished = false;
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            int seconds = Mathf.CeilToInt(remainingTime % 60);
            countdownText.text = string.Format("{0}", seconds);
        }
        else
        {
            remainingTime = 0;
            isCountDownFinished = true;
            countdownText.text = "0";
            gameObject.SetActive(false);
        }
    }
}
