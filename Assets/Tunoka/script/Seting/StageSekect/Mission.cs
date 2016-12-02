using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Mission : MonoBehaviour {


    [SerializeField, Header("Questメモ")]
    private string[] _Message;
    private Text _text;

    Animator _anim;

    // Use this for initialization
    void Start ()
    {
        _anim = GetComponent<Animator>();
        _text = transform.FindChild("MissionText").gameObject.GetComponent<Text>();
        _text.text = _Message[0];
    }
	
	// Update is called once per frame
	void Update () {
	}
    public void ChangeText(int _num)
    {
        _anim.Play("Mission", 0, 0.0f);
        _text.text = _Message[_num - 1];
    }
}
