using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Globalization;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public struct Score
    {
        public string name;
        public string time;
    }

    public bool won = false;　//勝利フラグ
    public List<Score> scores = new List<Score>(); //スコアのリスト
    public TextAsset score_txt;　//スコアのテキストファイル
    string path;　//ファイルパス
    public string sTime; //時間の文字列

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        path = Application.dataPath + "/score.txt";

        //タイトル画面をロードする
        SceneManager.LoadScene(1);

        // テキストを\n文字で分割する   
        var lines = score_txt.text.Split("\n"[0]);

        foreach(string line in lines) 
        {
           if(line!="" && !string.IsNullOrEmpty(line))
            {
                //行を"*"で分割し、リストに追加する
                string[] splits = line.Split("*"[0]);
                scores.Add(new Score() { name = splits[0], time = splits[1] });
            }
        }
    }

    //ファイルに文字列を追記する
    public void WriteToFile(string s)
    {
        File.AppendAllText(path, s);
    }

    // タイトル画面をロードする
    public void ToTitle()
    {
        SceneManager.LoadScene(1);
    }

    // ゲームを再開する
    public void Restart()
    {
        SceneManager.LoadScene(2);
    }
}
