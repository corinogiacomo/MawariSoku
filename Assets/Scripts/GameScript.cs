using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    private TextMeshProUGUI timer; //タイマーのテキスト
    public float time; //経過時間
    GameManager manager; 

    private void Awake()
    {
        timer = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        manager = GameObject.Find("Manager").GetComponent<GameManager>();
        manager.won = false;
    }
    private void Update()
    {
        if (!manager.won)
        {
            time += Time.deltaTime;
            TimeSpan span = TimeSpan.FromSeconds((double)(new decimal(time)));
            timer.text = span.ToString("mm':'ss", CultureInfo.CurrentCulture);
            manager.sTime = timer.text;
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Restart();
        }
    }

    //ゲームを再開する
    public void Restart()
    {
        manager.won = false;
        time = 0;
        manager.Restart();
    }

    //メインメニューに戻る
    public void ToMainMenu()
    {
        manager.won = false;
        time = 0;
        manager.ToTitle();
    }
}
