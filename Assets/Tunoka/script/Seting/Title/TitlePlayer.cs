using UnityEngine;
using System.Collections;

public class TitlePlayer : MonoBehaviour {

    public Script_SpriteStudio_Root Root;
    public GameObject _Player;

    [SerializeField, Header("動くスピード")]
    private float _speed;

    [SerializeField, Header("Moveのパターン")]
    private int _Movenum = 1;
    private float _xPos;
    public float _cTime;
    // Use this for initialization
    void Start ()
    {
        _xPos = transform.localPosition.x;
        _cTime = 0;
        Root.AnimationPlay(0, 0, 0, 1);

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (_Movenum == 3)
        {
            Move3();
        }
        if (_Movenum == 2)
        {
            Move2();
        }
        if (_Movenum == 1)
        {
            Move1();
        }
        if (_Movenum == 0)
        {
            Move0();
        }
        _Player.transform.localPosition = Vector3.zero;



    }
    void Move0()//待機
    {
        Root.RateOpacity = 0.0f;
        _cTime += Time.deltaTime;
        if (_cTime >= 8)
        {
            _Movenum = 1;
        }
    }
    void Move1()//歩く
    {
        if (Root.RateOpacity <= 0.5f)
        {
            Root.RateOpacity += 0.01f;
        }
        _cTime = 0;
        _xPos += _speed;
        transform.localPosition = new Vector3(_xPos,
                                              transform.localPosition.y,
                                              transform.localPosition.z);
        if (transform.localPosition.x >= 34)
        {
            Root.RateOpacity += 0.0f;
            _Movenum = 3;
            Root.AnimationPlay(1, 1, 0, 1);
        }
    }
    void Move2()//バレル
    {
    }
    void Move3()//ポージング
    {
        Root.RateOpacity = 1;
    }
    public bool AnimEnd(GameObject objecto)
    {
        return true;
    }
}
