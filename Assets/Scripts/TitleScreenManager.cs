using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    public GameObject sousaimg; //チュートリアル画像
    SoundManager smanager; //サウンドマネージャー
    private void Start()
    {
        //SoundManagerを取得する
        smanager = GameObject.Find("AudioManager").GetComponent<SoundManager>();
    }

    //チュートリアル画像を表示する
    public void ShowSousa()
    {
        sousaimg.SetActive(true);
    }

    //ゲームを開始のシーンをロードする
    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    //チュートリアル画像を非表示する
    public void CloseTutorial()
    {
        sousaimg.SetActive(false);
    }

    //音量を変更する
    public void ChangeVolume(float val)
    {
        smanager.ChangeVolume(val);
    }

    //ゲームを終了する
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
