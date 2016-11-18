using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

public class ItemUi : MonoBehaviour {

    private Image _image;
    private PlayerStatus _playerSstatus;

    public Sprite[] _itemImgs;
    private Sprite _startImg;

    // Use this for initialization
    void Start () {
        _image = transform.FindChild("ItemImage").gameObject.GetComponent<Image>() ;
        _playerSstatus = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerStatus>();
        _itemImgs = Resources.LoadAll<Sprite>("KItems");
        _startImg = _image.sprite;
    }
	
	// Update is called once per frame
	void Update () {

        if (_playerSstatus._items[int.Parse(transform.name) - 1] != null)
        {
             _image.sprite = _itemImgs[_playerSstatus._items[int.Parse(transform.name) - 1].GetComponent<ItemStatus>()._ID];
        }
        else
        {
            _image.sprite = _startImg;
        }
	    
	}
}
