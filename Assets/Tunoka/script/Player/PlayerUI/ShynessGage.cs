using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class ShynessGage : MonoBehaviour {

    Slider _slider;
    private PlayerStatus _playerStatus;

    void Start () {
        _slider = transform.GetComponent<Slider>();
        _playerStatus = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerStatus>();
    }
	
	void Update ()
    {
        _slider.value = _playerStatus._embarrassedGage;
        if (_playerStatus._embarrasseTr == true)
        {
        }
    }
}
