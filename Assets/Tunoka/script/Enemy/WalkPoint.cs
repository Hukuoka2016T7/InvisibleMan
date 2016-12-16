using UnityEngine;
using System.Collections;

public class WalkPoint : MonoBehaviour {

    public Vector3[] _WalkPoint;
    public Vector3 _WPoint;

    private int num = 0;
	// Use this for initialization
	void Start () {
        _WalkPoint = new Vector3[transform.childCount];
        int count = 0;
        foreach (Transform child in transform)
        {
            child.GetComponent<MeshRenderer>().enabled = false;
               _WalkPoint[count] = child.transform.position;
            count++;
        }
        _WPoint = _WalkPoint[num];
    }

    public void ChangeWalkPoint()
    {
        num++;
        if (num >= _WalkPoint.Length)
        {
            num = 0;
        }
        _WPoint = _WalkPoint[num];
    }
	
}
