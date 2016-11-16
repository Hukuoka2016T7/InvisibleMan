using UnityEngine;
using System.Collections;

public class AppeaUI : MonoBehaviour {

    private Vector2 _closeP;//閉じたときのポジション
    private Vector2 _openP; //開いたときのポジション
    [SerializeField, Header("Uiの状態（0：閉じる　1:開いてる途中　2：開いた　3：閉じてる途中)")]
    private int _status;

    public GameObject _gearOJ;//演出オブジェクト（ギア)
    public GameObject _cursor;//演出オブジェクト（矢印)

    [SerializeField, Header("UIの移動スピード")]
    public float _moveSpeed = 60;

    
    void Start () {
        _closeP = transform.localPosition;
        _openP = new Vector2(-18.2f, _closeP.y);

    }
	
	// Update is called once per frame
	void Update ()
    {
         if (Time.deltaTime <= 0){return;}
        direction();

    }
    void direction()//演出
    {
        if (_status == 1)//開くときの演出
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, _openP, _status*2);
            _gearOJ.transform.eulerAngles += new Vector3(0f, 0f, 10f);
            if (transform.localPosition.x == _openP.x)
            {
                _cursor.transform.eulerAngles =new Vector3(0f, 0f, 0f);
                print(_cursor.transform.localRotation);
                _status = 2;
            }
        }
        if (_status == 3)//閉まるときの演出
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, _closeP, _status);
            _gearOJ.transform.eulerAngles += new Vector3(0f, 0f, 10f);
            if (transform.localPosition.x == _closeP.x)
            {
                _cursor.transform.eulerAngles = new Vector3(0f, 0f, 180f);
                print(_cursor.transform.localRotation);
                _status = 0;
            }
        }
    }
    public void _switch()
    {
        if (_status == 0)
        {
            _status = 1;
        }
        if (_status == 2)
        {
            _status = 3;
        }
    }

}
