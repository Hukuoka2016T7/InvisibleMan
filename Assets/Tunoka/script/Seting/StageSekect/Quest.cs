using UnityEngine;
using System.Collections;

public class Quest : MonoBehaviour {

    [SerializeField, Header("ステージID")]
    private int _QuestID;

    private GameObject Gear;

	void Start () {
        Gear = transform.root.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        transform.eulerAngles = new Vector3(0,0, -Gear.transform.eulerAngles.z);
	}
    
}
