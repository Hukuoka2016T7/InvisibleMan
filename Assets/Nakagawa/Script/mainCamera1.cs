using UnityEngine;
using System.Collections;

public class mainCamera1 : MonoBehaviour {
    public Transform target;
    public Vector3 focus = Vector3.zero;
    public float Distance = 40.0f;
    public float Fixid = 30.0f;
    public float Length=6;


    // Use this for initialization
    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    void LateUpdate()
    {

        transform.position = new Vector3(target.position.x + Length, transform.position.y, transform.position.z);
        

        if (target.position.x > Distance)
        {
            transform.position = new Vector3(Fixid, transform.position.y, transform.position.z);
        }
    }

}
