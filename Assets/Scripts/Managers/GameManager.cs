using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager instance => _instance;
    public GameObject pauseMenu;

    public static bool isPaused = false;
    public int score = 0;

    private int _lives;
    public int lives
    {
        get => _lives;
        set
        {
            if (value <= 0) GameOver();
            //if (value < _lives) Respawn();
            if (value > maxLives) value = maxLives;
            _lives = value;

            Debug.Log($"Lives have been set to {_lives}");
            //broadcast can happen here
        }
    }

    [SerializeField] private int maxLives = 3;
    [SerializeField] private PlayerController playerPrefab;

    [HideInInspector] public PlayerController PlayerInstance => _playerinstance;
    PlayerController _playerinstance = null;
    Transform currentCheckpoint;

    // Start is called before the first frame update
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
        
    }

    private void Start()
    {
        pauseMenu.SetActive(false);

        if (maxLives <= 0)
        {
            maxLives = 3;
        }
        lives = maxLives;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "Game")
            {
                pauseMenu.SetActive(false);
                isPaused = false;
                SceneManager.LoadScene(0);
            }
                
            else if (SceneManager.GetActiveScene().name == "Title")
            {
                SceneManager.LoadScene(1);
                
            }
                
        }

        if (SceneManager.GetActiveScene().name == "Game")
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (isPaused == false)
                {
                    pauseMenu.SetActive(true);
                    isPaused = true;
                    Time.timeScale = 0f;
                }
                else
                {
                    pauseMenu.SetActive(false);
                    Time.timeScale = 1f;
                    isPaused = false;
                }

            }
        }
            
    }
    public void GameOver()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(2);
    }

    private void Respawn()
    {
        _playerinstance.transform.position = currentCheckpoint.position;
        Debug.Log("Respawn");
    }

    public void SpawnPlayer(Transform spawnLocation)
    {
        _playerinstance = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);
        currentCheckpoint = spawnLocation;
    }

    public void UpdateCheckpoint (Transform updatedCheckpoint)
    {
        currentCheckpoint = updatedCheckpoint;
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene(0);
        
    }

    public void quitGame()
    {
       Application.Quit();
    }

    public void startGame()
    {
        SceneManager.LoadScene(1);
        restartGame();
    }

    public void restartGame()
    {
        SceneManager.LoadScene(1);
        score = 0;

    }

    public void addScore()
    {
        score++;
    }
}
