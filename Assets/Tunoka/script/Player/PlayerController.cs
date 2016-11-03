using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Material[] modelMaterial; //モデルのマテリアル

    private PlayerStatus _pStatus;
    private bool alphaZero; //アルファが0.5以下になったらtreu　0.9以上になったらfalse
    // Use this for initialization
    void Start () {
        _pStatus = transform.GetComponent<PlayerStatus>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (_pStatus._invisibleGage <= 120)
        {
            _pStatus._invisibleGage += 0.1f;
        }
        else
        {
            _pStatus._invisibleGage = 0;
        }
         Move();
        Transparency();
    }

    void Move()
    {

        // 右・左
        float x = Input.GetAxisRaw("Horizontal")   ;

        // 上・下
        float z = Input.GetAxisRaw("Vertical")  ;

        //左右反転
        Vector3 scale = transform.localScale;
        if (x >= 0)
        {
            // 右方向に移動中
            scale.x = scale.x * 1; // そのまま（右向き）
        }
        else {
            // 左方向に移動中
            scale.x = scale.x * - 1; // 反転する（左向き）
        }
        

        // 移動する向きを求める
        Vector3 direction = new Vector3(x, 0, z).normalized;

        // 代入し直す
        transform.localScale =  scale ;
        // 移動する向きとスピードを代入する
        GetComponent<Rigidbody>().velocity = direction * _pStatus._moveSpeed * Time.deltaTime;
        //Material変化
        materialChange();
    }
    void Transparency()
    {
        foreach (var material in modelMaterial)
        {
            float _Acolor = 0.5f;
            if (_pStatus._invisibleGage >= 100)
            {
                _pStatus._invisibleTr = false;
                _Acolor = 1.0f;
            }
            else if (_pStatus._invisibleGage > 50)
            {
                Flashing(_pStatus._invisibleGage);
                _pStatus._invisibleTr = true;
                break;
            }
            else
            {
                _pStatus._invisibleTr = true;
            }
            Color originColor = new Color(material.color.r, material.color.g, material.color.b, _Acolor);
            material.color = originColor;
        }

    }
    void Flashing(float _time)//透明処理　点滅
    {
        _time = _time / 50 ;
        //アルファが0.5以下になったら透明　0.8以上になったら見えてる
        if (modelMaterial[0].color.a <= 0.5f)
        {
            alphaZero = true;
        }
        else if (modelMaterial[0].color.a >= 0.8f)
        {
            alphaZero = false;
        }
        Color alpha = new Color(0, 0, 0, Time.deltaTime * _time); //アルファを1/60秒で1変化させる

        if (alphaZero == false)
        {
            foreach (Material material in modelMaterial)
            {
                material.color -= alpha;
            }
        }
        else
        {
            foreach (Material material in modelMaterial)
            {
                material.color += alpha;
            }
        }
    }
    void OnCollisionStay(Collision other)
    {
        //仮隠れるコマンド
        if (other.gameObject.name== "hidebloc")
        {
            if (Input.GetKeyDown(KeyCode.K) && _pStatus._hide == 1)
            {
                _pStatus.anime = 0;
                _pStatus._hide = 0;

            }
            else if (Input.GetKeyDown(KeyCode.K) && _pStatus._hide == 0)
            {
                _pStatus.anime = 1;
                _pStatus._hide = 1;
            }
        }
    }
    void OnCollisionExit(Collision other)
    {

        if (other.gameObject.name == "hidebloc")
        {
            _pStatus._hide = 0;
            _pStatus.anime = 0;
        }
    }
    //リソース変更
    void materialChange()
    {
        transform.GetComponent<Renderer>().material = modelMaterial[_pStatus.anime];
    }
}
