using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed = 4f;	// 歩くスピード（メートル/秒）
    public float gravity = 10.0f;   // 重力加速度
    public GameObject mainCamera;

    private CharacterController controller;
    private float velocityY = 0;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(x * speed, 0, z * speed);


        if (transform.position.x > mainCamera.transform.position.x - 4) {
            //カメラの位置を取得
            Vector3 cameraPos = mainCamera.transform.position;
            //プレイヤーの位置から右に4移動した位置を画面中央にする
            cameraPos.x = transform.position.x + 2;
            mainCamera.transform.position = cameraPos;
        }
        //カメラ表示領域の左下をワールド座標に変換
        Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0));
        //カメラ表示領域の右上をワールド座標に変換
        Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1));
        //プレイヤーのポジションを取得
        Vector3 pos = transform.position;
    }

}
