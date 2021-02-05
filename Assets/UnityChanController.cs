using System    .Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour
{

    // アニメーションのコンポーネントを入れる
    private Animator myAnimator;
    //　移動させるコンポーネント
    private Rigidbody myRigidbody;
    //　前方向の速度（velocity=vector3型の変数で速度を表す）
    private float velocityZ　= 16f;
    //　横方向の速度
    private float velocityX = 10f;
    //　横方向の移動できる範囲
    private float movebleRange = 3.4f;
    //　上方向の速度
    private float velocityY = 10f;
    //動きの減速係数
    private float coefficient = 0.99f;
    //ゲーム終了の判定
    private bool isEnd = false;
    //ゲーム終了のテキスト
    private GameObject stateText;
    //スコア表示テキスト
    private GameObject scoreText;
    //左ボタン押下の判定
    private bool isLButtonDown = false;
    //右ボタン押下の判定
    private bool isRButtonDown = false;
    //ジャンプボタン押下の判定
    private bool isJButtonDown = false;


    private int score = 0;
    void Start()
    {
        //　Animatorコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();
        //　走るアニメーション開始
        this.myAnimator.SetFloat("Speed", 1);
        //  Rigidbodyのコンポーネント取得  
        this.myRigidbody = GetComponent<Rigidbody>();
        //　
        this.stateText = GameObject.Find("GameResultText");
        //
        this.scoreText = GameObject.Find("ScoreText");
    }

    
    void Update()
    {

        //ゲーム終了なら動きを減衰する
        if (this.isEnd)
        {
            this.velocityZ *= this.coefficient;
            this.velocityX *= this.coefficient;
            this.velocityY *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;
        }
        //横方向の入力による速度
        float inputVelocityX = 0;
        //上方向の入力による速度
        float inputVelocityY = 0;

        //左右をキー入力かボタン入力で移動
        if ((Input.GetKey(KeyCode.LeftArrow) || this.isLButtonDown) && -this.movebleRange < this.transform.position.x)
        {
            //速度の代入
            inputVelocityX = -this.velocityX;

        }
        else if ((Input.GetKey(KeyCode.RightArrow)|| this.isRButtonDown) && this.transform.position.x < this.movebleRange)
        {
            //速度の代入
            inputVelocityX = this.velocityX;

        }

        //ジャンプしていない時に矢印キーかボタン入力でジャンプする
        if ((Input.GetKeyDown(KeyCode.Space) || this.isJButtonDown) && this.transform.position.y < 0.5f)
        {
            //ジャンプアニメの再生
            this.myAnimator.SetBool("Jump", true); 
            //上方向への速度を代入
            inputVelocityY = this.velocityY;
        }
        else
        {
            //現在のY軸の速度を代入
            inputVelocityY = this.myRigidbody.velocity.y;
        }

        //Jump中はJumpにfalseをセットする
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }
        //速度を与える
        this.myRigidbody.velocity = new Vector3(inputVelocityX, inputVelocityY, this.velocityZ);
    }

    // トリガーモードで他のオブジェクトと接触した場合の処理
    void OnTriggerEnter(Collider other)
    {

        //　障害物に衝突した場合
        if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
        {
            this.isEnd = true;
            //テキストコンポーネントを取得し表示
            this.stateText.GetComponent<Text>().text = "GAME OVER";
        }

        //　ゴール到達
        if(other.gameObject.tag == "GoalTag") 
        {
            this.isEnd = true;
            //テキストコンポーネントを取得し表示
            this.stateText.GetComponent<Text>().text = "CLEAR!!";
        }

        
        //　コインとの衝突
        if (other.gameObject.tag == "CoinTag")
        {

            //
            score += 10;

            this.scoreText.GetComponent<Text>().text = "Score" + score + "pt";


            //パーティクルを再生
            GetComponent<ParticleSystem>().Play();

            //オブジェクトの破棄
            Destroy(other.gameObject);
        }
    }
    //ジャンプ押下した時はtrue
    public void GetMyJumpButtonDown()
    {
        this.isJButtonDown = true;
    }

    //離した時はfalse　以下同じ
    public void GetMyJumpButtonUp()
    {
        this.isJButtonDown = false;
    }

    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;
    }

    public void GetMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }

    public void GetMyRightButtonDown()
    {
        this.isRButtonDown = true;
    }

    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;
    }




}



