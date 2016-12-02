using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

    [SerializeField, Header("Playerの移動スピード")]
    public float _moveSpeed = 60;
    [SerializeField, Header("透明状態　（％）")]
    public float _invisibleGage = 0;
    [SerializeField, Header("透明か（true なら透明）")]
    public bool _invisibleTr = true;
    [SerializeField, Header("隠れるコマンド（0なら隠れてない　それ以外は隠れる）")]
    public int _hide = 0;
    [SerializeField, Header("Animation（0-歩く1－基本たち　2－隠れる 3:ﾎﾟｰｽﾞ）")]
    public int anime = 0;
    [SerializeField, Header("ポージングID")]
    public int _appea = 0;
    [SerializeField, Header("羞恥ゲージ (%)")]
    public float _embarrassedGage = 0;
    public bool _embarrasseTr;
    private float _embarrassecount = 0;
    [SerializeField, Header("笑いゲージ (%)")]
    public float _laughterGage = 0;
    public bool _laughterGageTr;
    private float _laughterGageecount = 0;
    [SerializeField, Header("持っているアイテム ")]
    public GameObject[] _items = new GameObject[3];
    private GameObject[] _itemNames = new GameObject[3];



    void Start ()
    {
        _itemNames[0] = transform.FindChild("GetItem").gameObject.transform.FindChild("underP").gameObject;
        _itemNames[1] = transform.FindChild("GetItem").gameObject.transform.FindChild("bodyP").gameObject;
        _itemNames[2] = transform.FindChild("GetItem").gameObject.transform.FindChild("HeadP").gameObject;
    }

	void Update () {
        //デバック用
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
        if (Input.GetKey(KeyCode.P))
        {
            _invisibleGage = 100;
        }
        //////////////////////////////////////////////////////////////////



        if (_embarrasseTr == true){
            _embarrassecount++;
            if (_embarrassecount >= 20){
                _embarrasseTr = false;
            }
        }
        else{ _embarrassecount = 0; }

        if (_laughterGageTr == true){
            _laughterGageecount++;
            if (_laughterGageecount >= 20){
                _laughterGageTr = false;
            }
        }
        else{
            _laughterGageecount = 0;
        }

    }
    public void embarrassed(float i)//羞恥ゲージ変更
    {
        _embarrassedGage += i;
        if (i > 0)
        {
            _embarrasseTr = true;
            _embarrassecount = 0;
        }
        if (_embarrassedGage < 0)
        {
            _embarrassedGage = 0;
        }
        else if (_embarrassedGage > 100)
        {
            _embarrassedGage = 100;
        }
    }
    public void laughterGage(float i)//笑いゲージ変更
    {
        _laughterGage += i;
        if (i > 0)
        {
            _laughterGageTr = true;
            _laughterGageecount = 0;
        }
        if (_laughterGage < 0)
        {
            _laughterGage = 0;
        }
        else if (_laughterGage > 100)
        {
            _laughterGage = 100;
        }
    }

    public void getItem(GameObject item)//アイテムをセットする
    {
        if (_items[0] == null)
        {
            _items[0] = item;
        }
        else if (_items[1] == null)
        {
            _items[1] = item;
        }
        else if(_items[2] == null)
        {
            _items[2] = item;
        }
        else //持ち物がいっぱいだったら最初のを捨てる
        {
            awayItem(0);//捨てる
            _items[2] = item;
        }
        item.GetComponent<ItemStatus>().setItemStatus(_itemNames[item.GetComponent<ItemStatus>()._parts]);
    }
    public void awayItem(int num)//アイテムを捨てる
    {
        if (_items[num] == null)
        {
            return;
        }
        _items[num].GetComponent<ItemStatus>().awayItemStatus();
        _items[num] = null;

        if (_items[0] == null)
        {
            _items[0] = _items[1];//セットし直す
            _items[1] = _items[2];
            _items[2] = null;
        }
        else if (_items[1] == null)
        {
            _items[1] = _items[2];
            _items[2] = null;
        }
        else if (_items[2] == null)
        {
            _items[2] = null;
        }
        
    }
}
