using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] playercontroller[] player;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject playMenu;
    [SerializeField] GameObject EndMenu;
    [SerializeField] GameObject pauseMenu;

    public static bool gamePaused;

    [SerializeField] TMP_InputField user_field;
    [SerializeField] int noofleves;

    [SerializeField] TextMeshProUGUI playerWinText;
    ball _ball;

    private void Awake()
    {
        if(instance == null)
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
        _ball = ball.instance;
        mainMenu.SetActive(true);
    }

    private void Update()
    {
        #region PauseGame
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_ball.gameStarted == true)
            {
                if (gamePaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }

        }
        #endregion

    }

    public void PlayButton()
    {
        mainMenu.SetActive(false);
        playMenu.SetActive(true);

        string _s = user_field.text.ToString();
        int.TryParse(_s,out noofleves);
        if (noofleves != 0)
            _ball.levels = noofleves;


        _ball.StartTheGame();

    }

    public void EndGame(int _playerno)
    {
        playMenu.SetActive(false);
        EndMenu.SetActive(true);
        playerWinText.text = "player" + _playerno.ToString() + "wins";
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        playMenu.SetActive(false);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        playMenu.SetActive(true);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void levelDropDownData(int _level)
    {
        switch (_level)
        {
            case 0:
                LevelHardness(800f, 20f);
                break;
            case 1:
                LevelHardness(1000f, 25f);
                break;
            case 2:
                LevelHardness(1300f, 30f);
                break;
            case 3:
                LevelHardness(1600f, 40f);
                break;
            default:
                LevelHardness(1000f, 20f);
                break;
        }
    }


    public void LevelHardness(float _ballSpeed,float _playerSpeed)
    {
        _ball.speed = _ballSpeed;
        if (player != null)
        {
            foreach (playercontroller _player in player)
            {
                _player.speed = _playerSpeed;
            }
        }
    }

}
