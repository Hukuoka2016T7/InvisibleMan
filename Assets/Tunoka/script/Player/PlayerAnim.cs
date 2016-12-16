using UnityEngine;
using System.Collections;

public class PlayerAnim : MonoBehaviour {

    private PlayerStatus _pStatus;
    private bool alphaZero; //アルファが0.5以下になったらtreu　0.9以上になったらfalse
    public Script_SpriteStudio_Root Root;




    public int SetAnim;
    public int Dummy = 1;

    void Start () {
        _pStatus = transform.GetComponent<PlayerStatus>();
        SetAnim = 2;
        Dummy = SetAnim;
        Root.AnimationPlay(SetAnim, 0, 0, 1);
    }

	
	void Update ()
    {
        if (Time.deltaTime <= 0) 
        {
            Root.RateSpeed = 0;
            return;
        }

        Transparency();
        Anime();

        Root.RateSpeed = 1;
   
    }

    void Anime()
    {
        if (Dummy == _pStatus.anime) return;
        Dummy = _pStatus.anime;
        //普通に立つ
        if (_pStatus.anime == 1)
        {
            SetAnim = 2;
        }
        //歩く
        if (_pStatus.anime == 0)
        {
            SetAnim = 1;
        }
        //隠れる
        //ポージング1
        if (_pStatus.anime == 3)
        {
            SetAnim = 0;
        }
        //if (SetAnim >= 4) return;
        Root.AnimationPlay(SetAnim, 0, 0, 1);


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
            return;
        }
        else
        {
            _pStatus._invisibleTr = true;
        }
        Root.RateOpacity = _Acolor;
    }
    void Flashing(float _time)//透明処理　点滅
    {
        _time = _time / 50;
        //アルファが0.5以下になったら透明　0.8以上になったら見えてる
        if (Root.RateOpacity <= 0.5f)
        {
            alphaZero = true;
        }
        else if (Root.RateOpacity >= 0.8f)
        {
            alphaZero = false;
        }
        float alpha = Time.deltaTime * _time; //アルファを1/60秒で1変化させる

        if (alphaZero == false)
        {
            Root.RateOpacity -= alpha;
        }
        else
        {
            Root.RateOpacity += alpha;
        }
    }
}
