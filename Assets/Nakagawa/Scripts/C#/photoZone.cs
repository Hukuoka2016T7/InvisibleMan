using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class photoZone : MonoBehaviour
{

    private float timer = 0;
    public float photoTime = 0;
<<<<<<< HEAD
    private bool Photo = false;

    public Camera mainCamera;
    public Camera subCamera;

    void Start()
    {
        subCamera.enabled = false;
    }
=======

    private bool Photo = false;
>>>>>>> 505532c7d5d107b346dfe7e0a76491d1f23e81ac

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
<<<<<<< HEAD
            subCamera.enabled = true;
            mainCamera.enabled = false;
=======
>>>>>>> 505532c7d5d107b346dfe7e0a76491d1f23e81ac
        }

        if(other.gameObject.tag =="PhotoFA")
        {
            Photo = false;
<<<<<<< HEAD
            subCamera.enabled = false;
            mainCamera.enabled = true;
=======
>>>>>>> 505532c7d5d107b346dfe7e0a76491d1f23e81ac
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
<<<<<<< HEAD
            
=======
>>>>>>> 505532c7d5d107b346dfe7e0a76491d1f23e81ac

    }
}
