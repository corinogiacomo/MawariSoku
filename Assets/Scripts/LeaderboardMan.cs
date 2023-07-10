using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class LeaderboardMan : MonoBehaviour
{

    public GameObject panelpref; //パネルのプレハブ
    public Transform content; //コンテンツのTransform
    private GameManager gamemanager;

    private void Start()
    {
        gamemanager = GameObject.Find("Manager").GetComponent<GameManager>();
        SortScoresByTime(gamemanager.scores);

        //マネージャーからスコアを読み込む
        foreach (GameManager.Score s in gamemanager.scores) 
        {
            AddScore(s.name, s.time);
        }
    }

    //スコアを時間でソートする
    private void SortScoresByTime(List<GameManager.Score> scores)
    {
        scores.Sort((x, y) => TimeSpan.Parse(x.time).CompareTo(TimeSpan.Parse(y.time)));
    }

    public void ScrollingViewPort(Vector2 v2viewp)
    {
        //コンテンツのローカル座標のYが0より小さい場合、Yを0に設定する
        if (content.localPosition.y < 0) 
        {
            content.localPosition = new Vector2(content.localPosition.x, 0);
        }
    }

    // スコアを追加する
    public void AddScore(string name, string score)
    {
        //プレハブを生成し、コンテンツの子要素にする
        GameObject ns = Instantiate(panelpref, content);

        //名前とスコアを設定する
        ns.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
        ns.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = score;
    }

    //タイトルに戻る
    public void ToTitle()
    {
        gamemanager.ToTitle();
    }

    public void Restart()
    {
        SceneManager.LoadScene(2);
    }

    //時間文字列を float に変換
    public static float ConvertTimeStringToFloat(string timeString)
    {
        string[] timeParts = timeString.Split(':');
        int minutes = int.Parse(timeParts[0]);
        int seconds = int.Parse(timeParts[1]);

        return minutes * 60 + seconds;
    }

}
