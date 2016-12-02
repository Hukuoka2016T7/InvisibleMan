using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitleText : MonoBehaviour {

    [SerializeField, Header("変わるスピード")]
    private float _speed = 1;

    private bool _Down = false;
    private Text _text;
    private float _colorA;
	void Start () {
        _text = transform.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        print(_text.color.a);
        if (_text.color.a <= 0)
        {
            _Down = false;
        }
        if (_text.color.a >= 1)
        {
            _Down = true;
        }
        if (_Down == true){
            _colorA -= _speed * 0.05f;
        }
        else
        {
            _colorA += _speed * 0.05f;
        }
        _text.color = new Color(0, 0, 0, _colorA);
    }
}
