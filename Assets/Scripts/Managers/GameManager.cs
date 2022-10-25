using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public int lives = 3;
    public int points = 0;
    public int bestScore = 0;

    public Transform playerSpawn;
    public Transform enemySpawn;
    public Transform barrierSpawn1;
    public Transform barrierSpawn2;
    public Transform barrierSpawn3;
    public GameObject player;
    public GameObject enemyFormation;
    public GameObject barrier;
    public ArrayList playerLives;

    public TextMeshPro score;
    public TextMeshPro hiScore;
    public TextMeshPro winOrLose;
    public Transform livesTransform;
    public GameObject playerSprite;

    private bool win = false;
    private float invTime = 1.5f;

    private GameObject life;
    private GameObject enemyFormTr;

    void Start()
    {
        //Instantiate player and enemy formation
        life = GameObject.Instantiate(player, playerSpawn, playerSpawn);
        StartCoroutine(InvincibleDelay());
        enemyFormTr = GameObject.Instantiate(enemyFormation, enemySpawn, enemySpawn);
        GameObject.Instantiate(barrier, barrierSpawn1);
        GameObject.Instantiate(barrier, barrierSpawn2);
        GameObject.Instantiate(barrier, barrierSpawn3);

        //loading hiscore
        bestScore = PlayerPrefs.GetInt("HighScore");
        hiScore.text = "HI-SCORE: " + bestScore;

        //Showing lives on screen
        playerLives = new ArrayList();
        for (int i = 0; i<lives; i++) {
            Vector2 location = new Vector2(livesTransform.position.x+i*player.GetComponent<Renderer>().bounds.size.x + 0.15f*i,
                                           livesTransform.position.y);
            playerLives.Add(GameObject.Instantiate(playerSprite, location, livesTransform.rotation));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!win) {
            points += enemyFormTr.GetComponent<Formation>().CheckHit();
            UpdateText();
            CheckLife();
            CheckWin();
        }
    }

    void UpdateText() {
        score.text = "SCORE: " + points;
    }

    void CheckLife() {
        if (life == null) {
            if (lives!=0) {
                lives--;
                Destroy((GameObject)playerLives[lives]);
                playerLives.Remove(lives);
                life = GameObject.Instantiate(player, playerSpawn.position, playerSpawn.rotation);
                StartCoroutine(InvincibleDelay());
            }
            else {
                GameOver();
            }
        }
    }

    void CheckWin() {
        if (enemyFormTr.GetComponent<Formation>().CheckEmpty()) {
            Destroy(enemyFormTr);
            winOrLose.text = "GAME END\n SCORE: " + points;
            if (points > bestScore) {
                PlayerPrefs.SetInt("HighScore", points);
                hiScore.text = "HI-SCORE: " + points;
            }
            win = true;
        }
    }

    void GameOver() {
        winOrLose.text = "GAME OVER\n SCORE: " + points;
        if (points > bestScore) {
            PlayerPrefs.SetInt("HighScore", points);
            hiScore.text = "HI-SCORE: " + points;
        }
    }

    IEnumerator InvincibleDelay() {
        yield return new WaitForSeconds(invTime);
        life.GetComponent<Health>().inv = false;
        life.GetComponent<SpriteRenderer>().enabled = true;

    }
}
