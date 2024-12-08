using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TextMeshProUGUI ScoreText;

    public int base_score = 200;
    public int score;
    public int level = 1;

    public int ball_cost = 100;


    // The number of total levels in the game
    private readonly int NUM_LEVELS = 2;

    private Brick[] bricks;
    private Shooter shooter;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        MainMenu();
    }

    public void MainMenu()
    {
        // HACK: hide the score text on the main menu
        ScoreText.text = "";
        SceneManager.LoadScene("Menu");
    }

    public void NewGame()
    {
        score = base_score;
        ScoreText.text = "Score: " + score;

        LoadLevel(1);
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void OnBrickHit(Brick brick)
    {
        score += brick.points;
        ScoreText.text = "Score: " + score;


        if (Cleared())
        {
            CompleteLevel();
        }
    }

    private void LoadLevel(int level)
    {
        if (level > NUM_LEVELS)
        {
            level = 1;
        }

        this.level = level;

        // All levels should be named Level1, Level2, Level3, etc.
        SceneManager.LoadScene("Level" + level);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        bricks = FindObjectsByType<Brick>(FindObjectsSortMode.None);
        shooter = FindFirstObjectByType<Shooter>();
    }

    private void CompleteLevel()
    {
        if (level < NUM_LEVELS)
        {
            LoadLevel(level + 1);
        }
        else
        {
            MainMenu();
        }

    }

    private bool Cleared()
    {
        for (int i = 0; i < bricks.Length; i++)
        {
            if (bricks[i].gameObject.activeInHierarchy && !bricks[i].unbreakable)
            {
                return false;
            }
        }
        return true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainMenu();
        }
        else if (SceneManager.GetActiveScene().name.StartsWith("Level") && Input.GetMouseButtonDown(0))
        {
            SpawnBall();
        }
    }

    private void SpawnBall()
    {
        if (shooter == null || ball_cost > score)
        {
            return;
        }

        score -= ball_cost;
        ScoreText.text = "Score: " + score;

        shooter.SpawnBall();
    }

    public void OnBallDestroyed()
    {
        Ball[] balls = FindObjectsByType<Ball>(FindObjectsSortMode.None);
        if (balls.Length == 0 && score < ball_cost)
        {
            MainMenu();
        }
    }
}
