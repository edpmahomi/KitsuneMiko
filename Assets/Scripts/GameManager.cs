﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    //定数定義
    private const int MAX_SCORE = 999999;

    public int nextStageNum;//クリア後に移るステージナンバー

    public LayerMask playerLayer;

    public GameObject textGameOver;
    public GameObject textClear;
    public GameObject textScoreNumber;
    public enum GAME_MODE
    {
        PLAY,
        CLEAR,
        GAMEOVER
    };
    public GAME_MODE gameMode = GAME_MODE.PLAY;
    

    private int score = 0;
    private int displayScore = 0;

    public AudioClip clearSE;
    public AudioClip gameoverSE;
    private AudioSource audioSource;
	// Use this for initialization
		
	void Start () {
        DontDestroyOnLoad(gameObject);
        RefreshScore();
        audioSource = this.gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (score > displayScore)
        {
            displayScore += 10;

            if (displayScore > score)
            {
                displayScore = score;
            }

            RefreshScore();
        }
	}

    public void GameOver()
    {
        audioSource.PlayOneShot(gameoverSE);
        textGameOver.SetActive(true);

        Invoke("GoBackGameTitle",2.0f);
       
    }

    // シーン遷移
    public void Next()
    {
        audioSource.PlayOneShot(clearSE);
        gameMode = GAME_MODE.CLEAR;
        textClear.SetActive(true);
        GameObject oldPlayer = FetchPlayer();
        Debug.Log(oldPlayer.GetComponent<Breakable>().hitPoint);
        SceneManager.LoadScene("GameScene" + nextStageNum); 
        GameObject newPlayer = FetchPlayer();
        Debug.Log(oldPlayer.GetComponent<Breakable>().hitPoint);
    }

    GameObject FetchPlayer()
    {
        foreach (GameObject temp_player in GameObject.FindGameObjectsWithTag("Player"))
        {
            Debug.Log(temp_player.name + " " + temp_player.layer);
            if (temp_player.layer == playerLayer)
            {
                return temp_player as GameObject;
            }
            else
            {
                Debug.Log("NULL1");
                return null;
            }
        }
        Debug.Log("NULL2");
        return null;
    }

    //スコア加算
    public void AddScore(int val)
    {
        score += val;
        if (score > MAX_SCORE)
        {
            score = MAX_SCORE;

        }
    }
    //スコア表示を更新
    void RefreshScore()
    {
        textScoreNumber.GetComponent<Text>().text = displayScore.ToString();
        
    }

    //タイトル画面に戻る
    void GoBackGameTitle()
    {
        SceneManager.LoadScene("GameTitle");
    }
}
