using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

    [SerializeField, Header("Playerの移動スピード")]
    public float _moveSpeed = 60;
    [SerializeField, Header("透明状態　（％）")]
    public float _invisibleGage = 0;
    [SerializeField, Header("透明か（true なら透明）")]
    public bool _invisibleTr = true;
    [SerializeField, Header("隠れるコマンド（0なら隠れる　それ以外は隠れる）")]
    public int _hide = 0;
    [SerializeField, Header("Animation（0-基本たち　1－隠れる　2－歩く）")]
    public int anime = 0;
    [SerializeField, Header("羞恥ゲージ (%)")]
    public float _embarrassedGage = 0;
    [SerializeField, Header("笑いゲージ (%)")]
    public float _laughterGage = 0;

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.O))
        {
            embarrassed(0.5f);
        }
        if (Input.GetKey(KeyCode.I))
        {
            embarrassed(-0.5f);
        }
        if (Input.GetKey(KeyCode.U))
        {
            laughterGage(0.5f);
        }
        if (Input.GetKey(KeyCode.Y))
        {
            laughterGage(-0.5f);
        }

    }
    public void embarrassed(float i)
    {
        _embarrassedGage += i;
        if (_embarrassedGage < 0)
        {
            _embarrassedGage = 0;
        }
        else if (_embarrassedGage > 100)
        {
            _embarrassedGage = 100;
        }
    }
    public void laughterGage(float i)
    {
        _laughterGage += i;
        if (_laughterGage < 0)
        {
            _laughterGage = 0;
        }
        else if (_laughterGage > 100)
        {
            _laughterGage = 100;
        }
    }
}
