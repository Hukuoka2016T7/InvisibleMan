using UnityEngine;
using System.Collections;

public class Jk01Anim : MonoBehaviour {
    enum AnimState
    {
        // 待機中
        Look,
        Ran,
        scare01,
        scare02,
        scare11,
        scare12,
        talk,
        idol,
    }

    public Script_SpriteStudio_Root Root;
    EnemyStatus _eStatus;

    public string Dummy = null;

    // Use this for initialization
    void Start () {
        _eStatus = transform.GetComponent<EnemyStatus>();
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.deltaTime <= 0)
        {
            Root.RateSpeed = 0;
            return;
        }
        Anime();
        Root.RateSpeed = 1;
    }
    void Anime()
    {
        if (Dummy == _eStatus._anim ) return;
        if (Dummy == "Move" && (_eStatus._anim == "Walking" || _eStatus._anim == "Chasing" || _eStatus._anim == "Return"))
        {
            return;
        }
        Dummy = _eStatus._anim;
        
        if (_eStatus._anim == "Idol")
        {
            Idol();
            return;
        }
        if (_eStatus._anim == "Dialogue")
        {
            Dialogue();
            return;
        }
        if (_eStatus._anim == "Search")
        {
            Search();
            return;
        }
        if (_eStatus._anim == "Surprised")
        {
            Surprised();
            return;
        }
        if (_eStatus._anim == "Photo")
        {
            Photo();
            return;
        }
        if (_eStatus._anim == "Walking" || _eStatus._anim == "Chasing" || _eStatus._anim == "Return")
        {
            Dummy = "Move";
            Move();
            return;
        }
    }
    void Idol()
    {
        Root.AnimationPlay((int)AnimState.idol, 0, 0, 1);
    }
    void Dialogue()
    {
        Root.AnimationPlay((int)AnimState.talk, 0, 0, 1);
    }
    void Move()
    {
        Root.AnimationPlay((int)AnimState.Ran, 0, 0, 1);
    }
    void Search()
    {
        Root.AnimationPlay((int)AnimState.Look, 0, 0, 1);
    }
    void Surprised()
    {
        Root.AnimationPlay((int)AnimState.scare11, 0, 0, 1);
    }
   
    void Photo()
    {
        Root.AnimationPlay((int)AnimState.scare02, 0, 0, 1);
    }
    
}
