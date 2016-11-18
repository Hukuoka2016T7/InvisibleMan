using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class photoZone : MonoBehaviour
{

    private float timer = 0;
    public float photoTime = 0;
    private bool Photo = false;

    private bool C_ON_OFF = false;

    public Camera mainCamera;
    public Camera subCamera;

    void Start()
    {
        subCamera.enabled = false;
    }

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
            mainCamera.enabled = false;
            subCamera.enabled = true;
            GetComponent<CaptureAndShareImage>().Shoot();
        }
        if (timer > photoTime)
            timer = 0;
        if (C_ON_OFF == true)
        {
            mainCamera.enabled = true;
            subCamera.enabled = false;
        }
    }
}
