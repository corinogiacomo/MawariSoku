using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Globalization;
using System.IO;

public class Writebeh : MonoBehaviour
{
    TextMeshProUGUI lblname; //名前のテキスト
    private int counter = 0; //文字数のカウンター
    GameManager manager;
    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<GameManager>();
        lblname = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        foreach (char c in Input.inputString)
        { 
            if (c == '\b') //バックスペースが押した場合末尾の文字を削除する
            {
                if (lblname.text.Length != 0)
                {
                    lblname.text = lblname.text.Substring(0, lblname.text.Length - 1);
                    counter--;
                }
            }
            else if ((c == '\n') || (c == '\r'))　//エンターキーが押した場合名前と時間の情報を追加して、ファイルに書き込んで、結果リーダーボード画面に移動する
            {
               manager.scores.Add(new GameManager.Score() { name = lblname.text,time = manager.sTime });
               manager.WriteToFile(lblname.text + "*" + manager.sTime + " \n"); 
               SceneManager.LoadScene(3);
            }
            else if(counter < 14)
            {
                lblname.text += c;
                counter++;
            }
        }
    }
}
