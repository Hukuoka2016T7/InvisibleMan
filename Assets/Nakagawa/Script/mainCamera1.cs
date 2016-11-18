﻿using UnityEngine;
using System.Collections;

public class mainCamera1 : MonoBehaviour {
    public Transform target;

    private Vector3 offset;

    public float Distance = 40.0f;
    public float Fixid = 30.0f;

    // Use this for initialization
    void Start()
    {

        target = GameObject.Find("Player").transform;

    }

    void Update()
    {

        transform.position = new Vector3(target.position.x + 6, transform.position.y, transform.position.z);


        if(target.position.x > Distance)
        {
            transform.position = new Vector3(Fixid, transform.position.y, transform.position.z);
        }
    }


}