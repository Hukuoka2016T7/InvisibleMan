using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shynessicon : MonoBehaviour {

    public PlayerStatus _playerStatus;
    private Image _image;


    void Start ()
    {
        _playerStatus = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerStatus>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
