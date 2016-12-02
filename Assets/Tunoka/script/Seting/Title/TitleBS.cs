using UnityEngine;
using System.Collections;

public class TitleBS : MonoBehaviour {


    [SerializeField, Header("動くスピード")]
    private float _speed;
    [SerializeField, Header("Start場所")]
    private float _Rpoint = 215;
    private float _xPos ;

    void Start()
    {
        _xPos = transform.localPosition.x;


    }

    void Update () {
        _xPos -= _speed;
        transform.localPosition = new Vector3(_xPos,
                                              transform.localPosition.y,
                                              transform.localPosition.z);
        if (transform.localPosition.x <= -_Rpoint)
        {
            _xPos = 215;
        }
	}
}
