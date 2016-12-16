using UnityEngine;
using System.Collections;

public class EnemyStatus : MonoBehaviour {

    [SerializeField, Header("住民の視野の距離")]
    public float _viewRange;
    [SerializeField, Header("目的地の前で止まるための距離")]
    public float _arrivedRange;
    [SerializeField, Header("視野の範囲(角度)")]
    public float _viewAngle;
    [SerializeField, Header("見失った後探す時間（秒）")]
    public float _searchTime;
    [SerializeField, Header("歩くスピード（秒）")]
    public float _moveSpeed;
    [SerializeField, Header("上がる羞恥量")]
    public float _upEmbarrassed;
    [SerializeField, Header("上がる笑い量")]
    public float _uplaughter;
    [SerializeField, Header("プレイヤー発見")]
    public bool _Check;

    private Transform _Player;

    [SerializeField, Header("アニメーション(確認用)")]
    public string _anim;
    [SerializeField, Header("最初の位置(確認用)")]
    public Vector3 _startPosition;//最初の位置
    private GameObject _sprite;//スプライト


    //transform.localEulerAngles.y

    void Start () {
        _startPosition = transform.localPosition;//最初の位置設定
        _Player = GameObject.FindGameObjectWithTag("Player").transform;
        _sprite = transform.FindChild("sprite").gameObject;
    }

    // Update is called once per frame
    void Update ()
    {
        _startPosition.y = transform.localPosition.y;
        _Check = PlayerSearch();
        LookUp();
        if (_Check == true)
        {
            _Player.GetComponent<PlayerStatus>().embarrassed(_upEmbarrassed);
            transform.LookAt(_Player);
        }



    }

    /// プレイヤーを探す
    public bool PlayerSearch()
    {
        if (!InViewRange())
        {
            print("範囲外");
            return false;
        }
        if (!IsPlayerInFieldOfView())
        {
            print("死角");
            return false;
        }
        if (!HideAndSeek())
        {
            print("死角（隠れている）");
            return false;
        }
        print("見つけたぞ");
        return true;
    }
    /// プレイヤーが視野角の範囲内にいるかを返す
    public bool IsPlayerInFieldOfView()
    {
        Vector3 find = _Player.transform.position - transform.position;
        return (Vector3.Angle(find, transform.forward) < _viewAngle);
    }
    /// プレイヤーが見えているか
    private bool InViewRange()
    {
        // プレイヤーとの距離
        float distance = (_Player.transform.localPosition - transform.localPosition).magnitude;

        return (distance <= _viewRange);
    }
    //プレイヤーが物陰にいるかどうか
    private bool HideAndSeek()
    {
        if (_Player.GetComponent<PlayerStatus>()._invisibleTr == true)
        {
            return false;
        }
        if (_Player.GetComponent<PlayerStatus>()._hide != 0)
        {
            return false;
        }

        RaycastHit hit;
        Ray ray = new Ray(transform.localPosition, _Player.transform.localPosition - transform.localPosition);
        Debug.DrawRay(transform.localPosition, -transform.localPosition + _Player.transform.localPosition, Color.red);
        //壁がある
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Stage")
            {
                return false;
            }
        }
        return true;
    }
    //絵が必ず正面を向くようにする
    private void LookUp()
    {
        if (transform.localEulerAngles.y >= 0 && transform.localEulerAngles.y < 180)
        {
            _sprite.transform.localEulerAngles = new Vector3(0, -transform.localEulerAngles.y, 0);
        }
        else
        {
            _sprite.transform.localEulerAngles = new Vector3(0, 360 - transform.localEulerAngles.y + 180, 0);
        }
    }

    //どっちの方角にいるか調べる
    public void LookAtPlayer()
    {
        transform.LookAt(_Player); 
    }
}
