using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;
    public TMP_Text scoreText;

    public float breakDelay = 3f; // Initial break delay

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

    private void Start()
    {
        UpdateScoreText();
        AdjustBreakDelays(); // Ensure initial adjustment is applied
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
        AdjustBreakDelays();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    private void AdjustBreakDelays()
    {
        breakDelay = Mathf.Clamp(3f - (0.2f * score), 0.6f, 3f); // Reduce delay by 0.2s per score increment, down to 0.6s minimum
        foreach (Destructible destructible in FindObjectsOfType<Destructible>())
        {
            destructible.breakDelay = breakDelay;
        }
    }
}
