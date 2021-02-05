using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{
    //Unityちゃんのオブジェクト
    private GameObject unitychan;
    //Unityちゃんとカメラの距離
    private float difference;

    void Start()
    {
        //オブジェクト取得
        this.unitychan = GameObject.Find("unitychan");
        //unityちゃんとの位置（ｚ座標）の差を求める
        this.difference = unitychan.transform.position.z - this.transform.position.z;

    }

    
    void Update()
    {
        //unityちゃんをストーキング
        this.transform.position = new Vector3(0, this.transform.position.y, this.unitychan.transform.position.z - difference);
    }

    private void OnTriggerEnter(Collider other)
    {
        //障害物に衝突した場合
        if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag"　|| other.gameObject.tag == "CoinTag")
        {
            Destroy(other.gameObject);
        }

  
       
    
    
    }
}

