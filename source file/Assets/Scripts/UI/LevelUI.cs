using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUI : MonoBehaviour
{
    private int score;
    private int stage;
    private int enemyCount;

    private float minute, seconds, currentTime;
    private float startTime;
    private float countDownF;
    private bool timeActive;

    private enemySpawner spawning;

    public Text scoreText, timerText, stageText, countDownText;

    void Start()
    {
        startTime = 60f;
        countDownF = 5f;
        score = 0;
        stage = 0;
        spawning = GameObject.FindGameObjectWithTag("spawner").GetComponent<enemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "SCORE: " + score;
        stageText.text = "STAGE: " + stage;
        StageUpdate();
        CountDownDisplay(timeActive);
        TimeDisplay(timeActive);
    }

    public void ScoreUpdate(int score)
    {
       this.score += score;
    }

    private void StageUpdate()
    {
        if (score % 10 == 0 && enemyCount == 0)
            timeActive = false;
        else
            timeActive = true;
    }

    private void GameOver()
    {
        countDownText.enabled = true;
        countDownText.text = "Game Over" + Environment.NewLine + 
                             "Stages: " + stage + Environment.NewLine + 
                             "Score: " + score;
        Destroy(GameObject.FindWithTag("enemyCube"));
        spawning.enabled = false;
        timerText.enabled = false;
    }

    public void EnemyUpdate(int enemyCount)
    { 
        if (enemyCount < 0)
            this.enemyCount += enemyCount;
        else
            this.enemyCount = enemyCount;
        UnityEngine.Debug.Log(enemyCount);
    }

    
    private void CountDownDisplay(bool startCount)
    {
        if(!startCount)
        {
            countDownText.enabled = true;
            if (countDownF > 0)
            {
                countDownText.text = countDownF.ToString("0");
                countDownF -= Time.deltaTime;
            }
            if (countDownF <= 0)
            {
                StartCoroutine(Go());
                stage++;
                spawning.EnemySpawn(stage);
                countDownF = 5f;
            }
        }
    }

    private void TimeDisplay(bool startCount)
    {
        if (startCount)
        {
            if (currentTime <= 0 && enemyCount != 0)
                GameOver();
            else
            {
                timerText.enabled = true;
                currentTime -= Time.deltaTime;
                //UnityEngine.Debug.Log(currentTime);
                minute = Mathf.FloorToInt(currentTime / 60);
                seconds = Mathf.FloorToInt(currentTime % 60);
                timerText.text = string.Format("{0:00}:{1:00}", minute, seconds);
            }
        }
        else
        {
            timerText.enabled = false;
            currentTime = startTime;
        }
    }

    private IEnumerator Go()
    {
        countDownText.text = "GO!";
        yield return new WaitForSeconds(1f);
        countDownText.enabled = false;
    }

    public void LeaveGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
