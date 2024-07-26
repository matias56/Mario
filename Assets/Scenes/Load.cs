using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    public int level;

    public float timer = 2;

    public string worldName;
    public Text worldText;

    public GameManager gm;
    public Text life;
    //public int worldNumber;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<Mario>().DisableMovement();

        Invoke("StartLevel", timer);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        

        

        worldName = GetSceneName();

        worldText.text = worldName;

        life.text = GameManager.lives.ToString("00");
    }


    private void StartLevel()
    {
        // Enable player movement
        player.GetComponent<Mario>().EnableMovement();

        // Destroy the title card object
        Destroy(gameObject);
    }


    string GetSceneName()
    {
        // Get the current scene build index
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;

        // Get the next scene build index
        
        // Get the next scene's name using the build index
        string SceneName = SceneUtility.GetScenePathByBuildIndex(currentSceneBuildIndex);
        SceneName = System.IO.Path.GetFileNameWithoutExtension(SceneName);

        return SceneName;
    }
}
