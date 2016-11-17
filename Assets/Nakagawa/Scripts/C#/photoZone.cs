using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class photoZone : MonoBehaviour
{

    private float timer = 0;
    public float photoTime = 0;

    private bool Photo = false;

    void Update()
    {
        timer += Time.deltaTime;
        Debug.Log(timer);
    }

    public void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "PhotoTR")
        {
            Photo = true;
        }

        if(other.gameObject.tag =="PhotoFA")
        {
            Photo = false;
        }
    }

    void OnGUI()
    {
       if (Photo == true && timer > photoTime)
         {
            GetComponent<CaptureAndShareImage>().Shoot();
        }
        if (timer > photoTime)
            timer = 0;

    }
}
