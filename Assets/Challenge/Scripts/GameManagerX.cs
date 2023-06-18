using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManagerX : MonoBehaviour
{
    public static GameManagerX Instance;
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public Button restartButton;
    private int time;

    public List<GameObject> targetPrefabs;

    private int score;
    private float spawnRate = 1.5f;
    public bool isGameActive;

    private float spaceBetweenSquares = 2.5f; 
    private float minValueX = -3.75f; //  x value of the center of the left-most square
    private float minValueY = -3.75f; //  y value of the center of the bottom-most square

    private void Awake()
    {
        Instance = this;
    }

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        StartCoroutine(TimeUpdate());
        score = 0;
        time = 60;
        UpdateScore(0);
        titleScreen.SetActive(false);
    }
    
    IEnumerator TimeUpdate()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(1);
            UpdateTime();
        }
    }
    
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, 4);

            if (isGameActive)
            {
                Instantiate(targetPrefabs[index], RandomSpawnPosition(), targetPrefabs[index].transform.rotation);
            }
        }
    }
    
    Vector3 RandomSpawnPosition()
    {
        bool isObj = true;

        float spawnPosX;
        float spawnPosY;

        Vector3 spawnPosition;
        
        do
        {
            spawnPosX = minValueX + (RandomSquareIndex() * spaceBetweenSquares);
            spawnPosY = minValueY + (RandomSquareIndex() * spaceBetweenSquares);

            spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);
            
            Collider[] colliders = Physics.OverlapSphere(spawnPosition, 0.1f);
            
            if (colliders.Length > 0)
            {
                isObj = true;
            }
            else
            {
                isObj = false;
            }
        } while (isObj);
        
        return spawnPosition;

    }
    
    int RandomSquareIndex()
    {
        return Random.Range(0, 4);
    }
    
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: "+ score;
    }

    public void UpdateTime()
    {
        time -= 1;
        timeText.text = "Time: " + time;
        if(time == 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

}
