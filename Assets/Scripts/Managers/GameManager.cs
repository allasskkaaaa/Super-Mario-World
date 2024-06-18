using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager instance => _instance;

    
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
                SceneManager.LoadScene(0);
            }
                
            else if (SceneManager.GetActiveScene().name == "Title")
            {
                SceneManager.LoadScene(1);
            }
                
        }
    }
    public void GameOver()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(2);
    }

}
