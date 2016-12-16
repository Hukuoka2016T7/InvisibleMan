using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class photoZone : MonoBehaviour
{

    public float timer = 0;
    public float photoTime = 0;
    private bool Photo = false;
    public Text Timer;


    void Start()
    {
        
    }

    void Update()
    {
        timer -= Time.deltaTime;
        timer = Mathf.Max(timer, 0.0f);

        Timer.text = Mathf.CeilToInt(timer).ToString();
    }

  /*  public void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "PhotoTR")
        {
            Photo = true;
        }

        if(other.gameObject.tag =="PhotoFA")
        {
            Photo = false;
        }
    }*/

    void OnGUI()
    {
       if ( timer <= photoTime)
         {
            GetComponent<CaptureAndShareImage>().Shoot();
        }
    }
}
