using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int score;
    public int level = 1;


    // The number of total levels in the game
    private int NUM_LEVELS = 1;

    private Brick[] bricks;

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
            FindSceneBricks();
        }
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        score = 0;

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

    private void FindSceneBricks()
    {
        bricks = FindObjectsByType<Brick>(FindObjectsSortMode.None);
    }

    private void CompleteLevel()
    {
        // Implement me.
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
}
