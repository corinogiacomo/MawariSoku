using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed; //回転速度
    [SerializeField]
    private float speed; //移動速度
    public ParticleSystem dusteffect; //ダストエフェクトのパーティクルシステム
    ParticleSystem.MainModule psSettings;
    private bool isSpeedUp; //スピードアップフラグ
    private float speedTime = 0; //スピードアップ時間
    Rigidbody2D rb;
    public CinemachineVirtualCamera VirtualCamera;
    Vector2 force;
    private float timerwin = 3f; //勝利パネルのタイマー
    GameManager gameManager;
    SoundManager soundManager;
    public GameObject wonPanel; //勝利パネル
    private float startingboost = 3f; //スタート時のブーストタイマー

    private void Start()
    {
        wonPanel.SetActive(false);
        gameManager = GameObject.Find("Manager").GetComponent<GameManager>();
        soundManager = GameObject.Find("AudioManager").GetComponent<SoundManager>();
        rb = this.GetComponent<Rigidbody2D>();
        psSettings = dusteffect.main;
        psSettings.startColor = Color.gray;
        //スタート時のブースト
        force = new Vector2(4f, 0);
    }

    private void Awake()
    {
        Application.targetFrameRate = 120;
    }

    void Update()
    {
        if (!gameManager.won)
        {
            //力を加えてプレイヤーを移動させる
            rb.AddForce(force, ForceMode2D.Force);

            //カメラの拡大率をプレイヤーの速度に応じて変化させる (MAX 13)
            if (rb.velocity.x > 10)
            {
                VirtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(VirtualCamera.m_Lens.OrthographicSize, rb.velocity.x / 1.25f, Time.deltaTime);
                if (VirtualCamera.m_Lens.OrthographicSize > 13f)
                    VirtualCamera.m_Lens.OrthographicSize = 13f;
            }
            else
            {
                VirtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(VirtualCamera.m_Lens.OrthographicSize, 4.5f, Time.deltaTime);
            }

            //スペースキーが押された時、下向きの力を加える
            if (Input.GetKeyDown(KeyCode.Space))
            {
                force = new Vector2(speed, -10);
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {   
                force = new Vector2(speed, 0);
            }

            //スピードアップの場合、タイマーをスタートさせ、1.5秒後にスピードをデフォルトに戻す
            if (isSpeedUp && speedTime < 1.5f)
            {
                speedTime += Time.deltaTime;
            }
            if (speedTime > 1.5f)
            {
                isSpeedUp = false;
                speedTime = 0;
                force -= new Vector2(2f, 0);
                psSettings.startColor = Color.gray;

            }

            //スタート時のブーストが終了したら、スピードをデフォルトに戻す
            if (startingboost > 0)
            {
                startingboost -= Time.deltaTime;
                if (startingboost <= 0)
                {
                    force = new Vector2(speed, 0);
                }
            }

            if (transform.position.y < -20)
            {
                //マップの下に落ちたので再起動する
                SceneManager.LoadScene(2);
            }
        }
        else
        {
            timerwin -= Time.deltaTime;
            if (timerwin < 0)
            {
                wonPanel.SetActive(true);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //衝突したオブジェクトのタグが"victory"の場合、勝利フラグを立てる
        if (collision.gameObject.tag == "victory")
        {
            gameManager.won = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //衝突したオブジェクトのタグが"gasoline"の場合、スピードアップ状態にし、力を加える
        switch (collision.gameObject.tag)
        {
            case ("gasoline"):
                isSpeedUp = true;
                force += new Vector2(2f, 0);
                psSettings.startColor = Color.red;
                soundManager.PlaySE(0);
                Destroy(collision.gameObject);
                break;
        }
    }
}
