using UnityEngine;
using System.Collections;

public class ItemStatus : MonoBehaviour {

    [SerializeField, Header("アイテムの名前")]
    private string _name;

    [SerializeField, Header("どこの部位 0:下 1中: 2:上")]
    public int _parts;

    [SerializeField, Header("アイテムID")]
    public int _ID;

    Rigidbody _rigidbody;

	void Start () {
        _name = transform.name;
        _rigidbody = transform.GetComponent<Rigidbody>();
    }
 
    public void awayItemStatus()//アイテムを捨てる
    {
        transform.parent = null;
        transform.GetComponent<BoxCollider>().enabled = true;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        _rigidbody.useGravity = true;
    }
    public void setItemStatus(GameObject _obj)
    {
        transform.GetComponent<BoxCollider>().enabled = false;
        transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        transform.GetComponent<Rigidbody>().useGravity = false;
        transform.transform.parent = _obj.transform;// _itemNames[item.GetComponent<ItemStatus>()._parts].transform;
        transform.transform.localPosition = new Vector3(0, 0, 0);
    }
    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Stage")
        {
            if (_rigidbody.useGravity == true)
            {
                transform.GetComponent<Rigidbody>().useGravity = false;
                transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }
}
