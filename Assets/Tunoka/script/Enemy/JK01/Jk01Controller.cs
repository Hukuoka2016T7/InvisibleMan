using UnityEngine;
using System.Collections;


public class Jk01Controller : MonoBehaviour {
    //行動パターン
    enum JkState
    {
        // 待機中
        Idol,
        // 対話している
        Dialogue,
        // 歩き回る
        Walking,
        // 追いかける
        Chasing,
        // 探している
        Search,
        // 驚いている
        Surprised,
        // 写真を撮る
        Photo,
        // 元いた場所に変える
        Return,
    }

    [Header("行動パターン")]
    [SerializeField, Header("0　基本立　1歩き回る　2　対話")]
    private float _pattern;
    [SerializeField, Header("対話相手")]
    private GameObject _friend;

    EnemyStatus _eStatus;
    NavMeshAgent _agent;
    private Transform _Player;
    private Vector3 _goPosition;//目的地
    private Vector3 _startPosition;//最初の位置
    [SerializeField, Header("今の行動")]
    private JkState _State = JkState.Idol;
    [SerializeField, Header("探索時間動")]
    private float __searchTime = 0;

    private WalkPoint _WalkPoint;

    // Use this for initialization
    void Start () {
        _WalkPoint = transform.FindChild("WalkPoint").gameObject.GetComponent<WalkPoint>();
        _eStatus = transform.GetComponent<EnemyStatus>();
        _Player = GameObject.FindGameObjectWithTag("Player").transform;
        _startPosition = transform.localPosition;
        _agent = GetComponent<NavMeshAgent>();
        _State = JkState.Idol;
        _agent.SetDestination(_startPosition);//最初の場所に変える

    }

    // Update is called once per frame
    void Update ()
    {
        _startPosition.y = transform.localPosition.y;
        _goPosition = _Player.transform.localPosition;
        _agent.speed = _eStatus._moveSpeed *(100 *Time.deltaTime);
        _eStatus._anim = _State.ToString();
   
        switch (_State)
        {  
            case JkState.Idol       : Idol();       break;
            case JkState.Dialogue   : Dialogue();   break;
            case JkState.Walking    : Walking();    break;
            case JkState.Chasing    : Chasing();    break;
            case JkState.Search     : Search();     break;
            case JkState.Surprised  : Surprised();  break;
            case JkState.Photo      : Photo();      break;
            case JkState.Return     : Return();     break;
        }
    }
    //待機中 0
    void Idol(){
     
        if (_pattern == 0)//ボッチ
        {
            if (_eStatus._Check)
            {
                _State = JkState.Chasing;//見つけたから追いかける
            }
            return;
        }
        else if (_pattern == 1) //うろつき
        {
            _State = JkState.Walking;
            return;
        }
        else //対話
        {
            print("対話");
            //相方が元の位置にいるか？
            if (_friend.GetComponent<EnemyStatus>()._startPosition == _friend.transform.localPosition)
            {
                transform.LookAt(_friend.transform.localPosition);
                _State = JkState.Dialogue;
            }
        }
       
    }
    //対話している 1
    void Dialogue()
    {
        if (_eStatus._Check)
        {
            _State = JkState.Chasing;//見つけたから追いかける
            //相方に教える
            _friend.GetComponent<EnemyStatus>().LookAtPlayer();
        }
    }
    //歩き回る 2
    void Walking()
    {
        if (_agent.remainingDistance <= 0)
        {
            _WalkPoint.ChangeWalkPoint();
        }
        if (_eStatus._Check)
        {
            _State = JkState.Chasing;//見つけたから追いかける
            return;
        }
        _agent.SetDestination(_WalkPoint._WPoint);
    }
    //追いかける 3
    void Chasing()
    {
        _agent.stoppingDistance = 1;
        if (_eStatus._Check)
        {
            float distance = (_goPosition - transform.position).magnitude;
            if (distance < _eStatus._arrivedRange)//距離が一定ないだったら止まる
            {
                _agent.SetDestination(transform.localPosition);//その場に止まる
                _State = JkState.Photo;//写真を撮る
                return;
            }
            _agent.SetDestination(_goPosition);
        }
        else
        {
            __searchTime = 0;
            _State = JkState.Search;//見失ったから探す
        }
    }
    //探している 4
    void Search()
    {
        __searchTime += Time.deltaTime;
        if (__searchTime >= _eStatus._searchTime)//時間内探している
        {
            _agent.SetDestination(_startPosition);//最初の場所に変える
            _State = JkState.Return;//見つかんなかったから元の場所に変える
            return;
        }
        else if (_eStatus._Check)//探す
        {
            _State = JkState.Chasing;//追いかけなおす
        }

    }
    //驚いている 5
    void Surprised() {
    }
    //写真を撮る 6
    void Photo()
    {
        if (_eStatus._Check)
        {
            float distance = (_goPosition - transform.localPosition).magnitude;
            if (distance > _eStatus._arrivedRange)//距離が一定ないだったら進む
            {
                _State = JkState.Chasing;//追いかける
                return;
            }
        }
        else
        {
            __searchTime = 0;
            _State = JkState.Search;//見失ったから探す
        }
    }
    //元いた場所に変える 7
    void Return()
    {
        if (_eStatus._Check)
        {
            _State = JkState.Chasing;//見つけたから追いかける
            return;
        }
        _agent.stoppingDistance = 0;
        print(_agent.remainingDistance);
        if (_agent.remainingDistance == 0)
        {
            _State = JkState.Idol;
        }
    }

    
}
