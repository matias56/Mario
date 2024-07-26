using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Camera cam;
    public Canvas canvas;

    public int score;
    public Text scoreText;

    public int coins { get; private set; }
    public Text coinsText;

    public string worldName { get; private set; }
    public Text worldText;

    public int timer { get; private set; }
    public Text timeText;
    public float fTime;

    public static float lives;

    public static GameManager Instance { get; private set; }

    public float goTime = 6f;

    public Player p;

    public bool hurryUpPlayed = false;

    public static GameManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       

        if(SceneManager.GetActiveScene().name == "Title" || SceneManager.GetActiveScene().name == "Load" || SceneManager.GetActiveScene().name == "GameOver")
        {
            fTime = 400;
        }

        p = FindObjectOfType<Player>();

       

        hurryUpPlayed = false;

        fTime = 400;
    }

    

    // Update is called once per frame
    void Update()
    {
        cam = FindObjectOfType<Camera>();

        canvas.renderMode = RenderMode.ScreenSpaceCamera;

        canvas.worldCamera = cam;

        DontDestroyOnLoad(this);

        scoreText.text = score.ToString("000000");

        coinsText.text = coins.ToString("00");

        worldName = SceneManager.GetActiveScene().name;

        worldText.text = worldName;

        timer = Mathf.RoundToInt(fTime);

        timeText.text = timer.ToString("000");

       

        if (SceneManager.GetActiveScene().name != ("Title") && SceneManager.GetActiveScene().name != ("GameOver"))
        {

            fTime -= Time.deltaTime;
            p = FindObjectOfType<Player>();
        }

        if(fTime <= 0 && !p.timeMusic)
        {
           
            p.timeMusic = true;
            
            p.DeathTime();
            
        }

        if (SceneManager.GetActiveScene().name == "Title" || SceneManager.GetActiveScene().name == "GameOver")
        {
            fTime = 400;
        }

        if(SceneManager.GetActiveScene().name == "GameOver")
        {
            goTime -= Time.deltaTime;

            if(goTime <= 0)
            {

                SceneManager.LoadScene("Title");

                goTime = 6;
            }
        }


        if(timer < 100 && !hurryUpPlayed)
        {
            p.PlayHurry();
            hurryUpPlayed = true;
        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            score = 0;
            coins = 0;
            lives = 3;

        }

    }

    public void ResetLevel(float timer)
    {
        CancelInvoke(nameof(ResetLevel));
        Invoke(nameof(ResetLevel), timer);
    }

    public void ResetLevel()
    {
        lives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        fTime = 400;

        if(p.timeMusic == true)
        {
            p.timeMusic = false;
        }
        
        

        if(lives <=0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void AddCoin()
    {
        coins++;
        score += 100;

        if (coins == 100)
        {
            coins = 0;
            AddLife();
        }
    }

    public void AddLife()
    {
        lives++;
    }

}
