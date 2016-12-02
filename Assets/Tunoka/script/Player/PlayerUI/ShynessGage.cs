using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class ShynessGage : MonoBehaviour {

    Slider _slider;
    private Image _image;
    private PlayerStatus _playerStatus;
    private Sprite[] _iconImgs;

    void Start ()
    {
        _image = transform.FindChild("icon").gameObject.GetComponent<Image>();
        _slider = transform.GetComponent<Slider>();
        _playerStatus = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerStatus>();
        _iconImgs = Resources.LoadAll<Sprite>("Image");
    }
	
	void Update ()
    {
        _slider.value = _playerStatus._embarrassedGage;
        if (_playerStatus._embarrasseTr == true)
        {
            _image.sprite = _iconImgs[2];
        }
        else
        {
            _image.sprite = _iconImgs[0];
        }
    }
}
