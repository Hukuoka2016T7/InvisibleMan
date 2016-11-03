using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GOAL : MonoBehaviour {

    public GameObject ClearLabel;

    bool Clear = false;

    void Update()
    {
        if(Clear==true)
        {
            ClearLabel.SetActive(true);
        }
    }

    public void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Player")
        {
            Clear = true;
        }
    }
}
