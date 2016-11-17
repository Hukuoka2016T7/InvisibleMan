﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Material[] modelMaterial; //モデルのマテリアル
    private Camera _camera;
    private PlayerStatus _pStatus;
    NavMeshAgent agent;

    private bool alphaZero; //アルファが0.5以下になったらtreu　0.9以上になったらfalse

    private Vector3 targetPosition; //　移動する位置
    [SerializeField, Header("移動スピード")]
    private float _speed;               //　移動スピード

    [SerializeField, Header("確認用[隠れるものに当たってるか]")]
    private GameObject _hideTr;
    [SerializeField, Header("確認用[アイテムに当たってるか]")]
    private GameObject _itemTr;
    [SerializeField, Header("確認用[プレイヤーをタッチしたか]")]
    private bool _PTr;
    private float _deltaTime;


    private Vector3 touchStartPos;//フリック用
    private Vector3 touchEndPos;//フリック用
    public float touchStartTime = 0;//フリック用

    // Use this for initialization
    void Start () {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _pStatus = transform.GetComponent<PlayerStatus>();
        agent = GetComponent<NavMeshAgent>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        _deltaTime = 100 * Time.deltaTime;
        Move();
        Transparency();

        //その他
        

        if (_hideTr == null)
        {
            _pStatus.anime = 0;
            _pStatus._hide = 0;
        }
    }
    void Move()//移動
    {
        var scale = transform.localScale;
        agent.speed = _speed * _deltaTime;//スピード変更
        if (Input.GetMouseButtonDown(0))
        {
            //int layerMask = ~(1 << 8);
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "HideBloc")//隠れるブロックをクリック
                {

                    if (_hideTr == hit.transform.gameObject)//隠れる
                    {
                        agent.enabled = false;//ナビメッシュを消す
                        _pStatus.anime = 1;     //アニメーション変更
                        _pStatus._hide = 1;     //隠れている判定On

                        targetPosition = hit.transform.FindChild("Center").position;
                        transform.position = new Vector3(
                            targetPosition.x,
                            transform.position.y,
                            targetPosition.z + 0.05f);
                        return;
                    }//隠れれないとき移動する
                    targetPosition = new Vector3(
                          hit.point.x,
                          transform.position.y,
                          hit.point.z
                      );
                }
                if (hit.transform.tag == "Item")//アイテムをクリック
                {
                    if (_itemTr == hit.transform.gameObject)//拾う
                    {
                        _pStatus.getItem(hit.transform.gameObject);
                    }
                    targetPosition = new Vector3(
                         hit.point.x,
                         transform.position.y,
                         hit.point.z
                     );
                }

                if (hit.transform.tag == "Stage")//ステージをクリック
                {
                    targetPosition = new Vector3(
                        hit.point.x,
                        transform.position.y,
                        hit.point.z
                    );
                    _hideTr = null;
                }
                if (hit.transform.tag == "Player")//プレイヤーを触っていたら
                {
                    touchStartTime = 0;
                    _PTr = true;
                    //フリック用タッチした時の場所
                    touchStartPos = new Vector3(Input.mousePosition.x,
                                  Input.mousePosition.y,
                                  Input.mousePosition.z);
                    return;
                }

                agent.enabled = true;//ナビメッシュをつける

                //方向転換関係
                if (transform.position.x - targetPosition.x <= 0)
                {
                    // 右方向に移動中
                    scale.x = 1.25f; // そのまま（右向き）
                }
                else { scale.x = -1.25f; }


                // 代入し直す
                transform.localScale = scale;
                agent.SetDestination(targetPosition);
            }

        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && _PTr == true)//フリック用タッチを話した時
        {
            touchEndPos = new Vector3(Input.mousePosition.x,
                               Input.mousePosition.y,
                               Input.mousePosition.z);
            GetDirection();
            _PTr = false;
        }
        //Material変化
        touchStartTime += _deltaTime/100;//フリック用時間
        materialChange();
    }
    void Transparency()//透明度
    {
        //透明度変更------------------------
        if (_pStatus._invisibleGage <= 120)
        {
            _pStatus._invisibleGage += 0.1f;
        }
        else
        {
            _pStatus._invisibleGage = 0;
        }
        //--------------------------------------------------------------------

        //透明化条件　マテリアル変更----------------------------------------------------------
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

        //----------------------------------------------------------
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

    void GetDirection()//フリック判定
    {
        float directionX = touchEndPos.x - touchStartPos.x;
        float directionY = touchEndPos.y - touchStartPos.y;

        if (Mathf.Abs(directionX) < Mathf.Abs(directionY)){
            if (30 < directionY && touchStartTime <= 0.5f)
            {
                _pStatus.awayItem(0);
                print("フリックしたよ");
            }
        }
    }

    public void Appea(int num)
    {

        _pStatus._hide = 1;//隠れていない状態にする
        //_pStatus.anime = 3;
        _pStatus._appea = num;

        print(_pStatus._appea);
        agent.enabled = true;//ナビメッシュをつける
        agent.SetDestination(transform.position);
    }

    void OnTriggerStay(Collider other)
    {
        print(other.name);
        //仮隠れるコマンド
        if (other.transform.tag == "HideBloc")
        {
            _hideTr = other.transform.gameObject;
        }
        //アイテムを拾う
        if (other.transform.tag == "Item")
        {
            _itemTr = other.transform.gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "HideBloc")
        {
            _hideTr = null;
        }
        if (other.transform.tag == "Item")
        {
            _itemTr = null;
        }
    }
    /*
    void OnCollisionStay(Collision other)
    {
        //仮隠れるコマンド
        if (other.transform.tag == "HideBloc")
        {
            _hideTr = other.transform.gameObject;
        }
        //アイテムを拾う
        if (other.transform.tag == "Item")
        {
            _itemTr = other.transform.gameObject;
        }
    }
    void OnCollisionExit(Collision other)//離れたとき
    {

        if (other.transform.tag == "HideBloc")
        {
            _hideTr = null;
        }
        if (other.transform.tag == "Item")
        {
            _itemTr = null;
        }
    }
    */
    //リソース変更

    void materialChange()
    {
        transform.GetComponent<Renderer>().material = modelMaterial[_pStatus.anime];
    }
}
