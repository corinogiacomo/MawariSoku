using System.Collections.Generic;
using UnityEngine;

public class DrawLineRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer;  //ラインレンダラー
    public int numberOfCurves = 10;  //カーブの数
    public int pointsPerCurve = 50;  //カーブごとのポイント数
    public float curveHeight = 1f;  //カーブの高さ
    public float curveWidth = 1f;  //カーブの幅
    public GameObject victoryFlagPref;  //勝利フラッグのプレハブ
    public EdgeCollider2D coll;  //エッジコライダー2D
    private List<Vector2> points = new List<Vector2>();  //ポイントのリスト
    private bool inverse = true;  //反転フラグ
    private Vector2[] parr = new Vector2[2500];  //ポイントの配列
    public GameObject gasolinepref;  //ガソリンのプレハブ

    void Start()
    {
        lineRenderer.positionCount = numberOfCurves * pointsPerCurve;
        lineRenderer.useWorldSpace = true;

        //カーブを描画
        DrawCurves();

        //アイテムを生成
        SpawnItem(gasolinepref, 20);
    }

    //アイテムを生成する関数
    private void SpawnItem(GameObject itempref, int hm)
    {
        float deltax = 2f;
        for (int i = 0; i < hm; i++)
        {
            Instantiate(itempref, new Vector3(deltax, 4, 0), itempref.transform.rotation, null);
            deltax += (Random.Range(50, 60));
        }
    }

    //カーブを描画する関数
    void DrawCurves()
    {
        for (int i = 0; i < numberOfCurves; i++)
        {
            //カーブの高さを設定
            curveHeight = inverse ? Random.Range(0f, -5f) : Random.Range(0f, 5);
            inverse = !inverse;

            int startIndex = i * pointsPerCurve;
            float startX = (i * curveWidth) + transform.position.x;

            for (int j = 0; j < pointsPerCurve; j++)
            {
                //Mathf.Sin()を使用して、X値に基づいてY値を計算します。
                //x に Mathf.PI を掛けると、Mathf.Sin() に渡すラジアン単位の角度が得られます。
                //得られた正弦値にcurveHeightを掛けて曲線の高さを調整する。

                float x = (float)j / (pointsPerCurve - 1);              
                float y = Mathf.Sin(x * Mathf.PI) * curveHeight;    

                Vector3 point = new Vector3(startX + x * curveWidth, y, 0f);
                points.Add(point);
                lineRenderer.SetPosition(startIndex + j, point);
            }
        }

        //勝利フラッグを生成
        Instantiate(victoryFlagPref, points[points.Count - 1] + new Vector2(0, 2), victoryFlagPref.transform.rotation, null);

        //コライダーを追加
        for (int i = 0; i < points.Count; i++)
        {
            parr[i] = points[i];
        }
        coll.points = parr;
    }
}
