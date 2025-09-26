using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int startingLives = 2;
    public float score = 0f;
    public float scoreMultiplier = 1f;

    public UIManager uiManager;
    public PlayerController player;

    private int currentLives;
    private bool isGameOver = false;

    public bool IsGameOver { get { return isGameOver; } }

    void Awake()
    {
        if (Instance == null) Instance = this; else Destroy(gameObject);
    }

    void Start()
    {
        currentLives = startingLives;
        if (uiManager != null) uiManager.UpdateLives(currentLives);
    }

    void Update()
    {
        if (isGameOver) return;

        score += Time.deltaTime * scoreMultiplier;
        if (uiManager != null) uiManager.UpdateScore((int)score);
    }

    public void LoseLife()
    {
        if (isGameOver) return;

        currentLives--;
        if (uiManager != null) uiManager.UpdateLives(currentLives);

        if (currentLives <= 0)
            GameOver();
    }

    void GameOver()
    {
        isGameOver = true;
        if (uiManager != null) uiManager.ShowGameOver();
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}