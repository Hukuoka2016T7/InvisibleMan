using UnityEngine;
using System.Collections;

public class AppeaButton : MonoBehaviour {


    private PlayerController _playerController;
    [SerializeField, Header("ﾎﾟｰｽﾞID")]
    private int _ID = 0;
    void Start () {
        _playerController = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerController>();

    }

    public void appealButton()
    {
        _playerController.Appea(_ID);
    }
}
