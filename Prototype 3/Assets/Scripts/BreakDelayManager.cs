using UnityEngine;
using TMPro;
using System.Collections;

public class BreakDelayManager : MonoBehaviour
{
    public static BreakDelayManager instance;
    public TMP_Text timerText;
    private Coroutine countdownCoroutine;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartCountdown(float countdownTime)
    {
        // Stop any currently running countdown and start a new one
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }
        countdownCoroutine = StartCoroutine(CountdownRoutine(countdownTime));
    }

    private IEnumerator CountdownRoutine(float countdownTime)
    {
        while (countdownTime > 0)
        {
            if (timerText != null)
            {
                timerText.text = $"{countdownTime:F1} s";
            }
            countdownTime -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        if (timerText != null)
        {
            timerText.text = "0.0 s";
        }
    }
}
