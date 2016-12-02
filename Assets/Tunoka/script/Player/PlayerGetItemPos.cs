using UnityEngine;
using System.Collections;

public class PlayerGetItemPos : MonoBehaviour {

    public GameObject[] getItemPos;

	void Start () {
        if (getItemPos[0] == null) return;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (getItemPos[0] == null) return;
    }
}
