using UnityEngine;
using System.Collections;

public class mainCamera1 : MonoBehaviour {
    public Transform target;

    private Vector3 offset;

    // public GameObject player;

    // Use this for initialization
    void Start()
    {
        // offset = transform.position - target.transform.position;

        target = GameObject.Find("Player").transform;

    }

    void Update()
    {
        // transform.position = target.position + offset;

        transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);


        /* if (transform.position.x >= 30)
         {
             transform.position = new Vector3(18, transform.position.y, transform.position.z);
         }*/

    }

}
