using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class photoZone : MonoBehaviour
{

    private float timer = 0;
    public float photoTime = 0;
    private bool Photo = false;

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
            subCamera.enabled = true;
            mainCamera.enabled = false;
        }

        if(other.gameObject.tag =="PhotoFA")
        {
            Photo = false;
            subCamera.enabled = false;
            mainCamera.enabled = true;
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
