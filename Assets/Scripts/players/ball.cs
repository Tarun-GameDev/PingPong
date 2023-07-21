using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ball : MonoBehaviour
{
    public static ball instance;

    [SerializeField] Rigidbody rb;

    public float speed = 100f;

    [SerializeField] MeshRenderer ballRenderer;
    [SerializeField] TrailRenderer trail;

    [SerializeField] Collider col;


    public int levels = 100;
    [SerializeField] float player1Score;
    [SerializeField] float player2Score;

    [SerializeField] TextMeshProUGUI pla1ScoreText;

    [SerializeField] TextMeshProUGUI pla2ScoreText;
    [SerializeField] TextMeshProUGUI noofLevelsText;


    public bool gameStarted = false;
    UIManager _uiManager;
    AudioManager _audioManager;

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
        gameStarted = false;
        _uiManager = UIManager.instance;
        _audioManager = AudioManager.instance;

        _audioManager.Play("theme");
    }

    public void StartTheGame()
    {
        StartCoroutine(startDelay());
    }

    IEnumerator startDelay()
    {
        gameStarted = true;

        resetPOS();

        col.enabled = true;
        ballRenderer.enabled = true;

        noofLevelsText.text = levels.ToString("00");

        yield return new WaitForSeconds(2f);

        StartingForce();
    }

    void resetPOS()
    {
        rb.position = Vector3.zero;
        rb.velocity = Vector3.zero;
    }

    void StartingForce()
    {
        float x = Random.value < 0.5f ? -1.0f : 1.0f;
        float y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) : Random.Range(0.5f, 1.0f);
        rb.AddForce(new Vector3(x, y, 0) * speed);
    }

    void AddForce(float _force)
    {
        rb.velocity *= _force;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _audioManager.Play("ballhit");

        if(collision.collider.tag == "Lwall")
        {
            player1Score++;
            pla1ScoreText.text = player1Score.ToString("00");
            if(player1Score >= levels)
            {
                //player win
                _uiManager.EndGame(1);
                EndGame();
                return;
            }
            StartCoroutine(resetBall());
        }
        else if(collision.collider.tag == "RWall")
        {
            player2Score++;
            pla2ScoreText.text = player2Score.ToString("00");
            if (player2Score >= levels)
            {
                //player win
                _uiManager.EndGame(2);
                EndGame();
                return;
            }
            StartCoroutine(resetBall());
        }

    }

    IEnumerator resetBall()
    {

        ballRenderer.enabled = false;
        trail.enabled = false;
        col.enabled = false;
        resetPOS();
        yield return new WaitForSeconds(3f);
        ballRenderer.enabled = true;
        trail.enabled = true;
        yield return new WaitForSeconds(1f);
        col.enabled = true;
        StartingForce();
    }

    void EndGame()
    {
        gameObject.SetActive(false);
    }

}
