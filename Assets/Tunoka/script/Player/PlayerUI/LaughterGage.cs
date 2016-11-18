using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class LaughterGage : MonoBehaviour {

    Slider _slider;
    public PlayerStatus _playerStatus;

    void Start()
    {
        _slider = transform.GetComponent<Slider>();
    }

    void Update()
    {
        _slider.value = _playerStatus._laughterGage;
    }
}