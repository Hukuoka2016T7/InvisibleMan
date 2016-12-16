using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ShootUI : MonoBehaviour
{
    [SerializeField]
    Image[] images = new Image[4];
    [SerializeField]
    Sprite[] numberSprites = new Sprite[10];

    public GameObject cam;
    private int counter = 1;

    public float timeCount { get; private set; }

   // public Text Timer;

    public float photoTime = 0;
    public float restTime = 0;
    private bool isRunning = true;

    void Update()
    {
        if (isRunning)
        {
            restTime -= Time.deltaTime;
        //    timeCount = restTime;
            StartCoroutine(TimerStart());
        }
      //  Timer.text = Mathf.CeilToInt(restTime).ToString();
        Debug.Log(restTime);
    }

    void OnGUI()
    {
            if (restTime < photoTime)
            {
                GetComponent<CaptureAndShareImage>().Shoot();
                cam.SetActive(true);
                StopTimer();

                 counter++;
                 if(counter>=3)
                  {
                     cam.SetActive(false);
                   }
            }
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void PauseTimer()
    {
        isRunning = false;
    }

    public void StopTimer()
    {
        PauseTimer();
        restTime = 0;
    }

    void SetNumbers(int sec,int val1,int val2)
    {
        string str = String.Format("{00:00}",sec);
        images[val1].sprite = numberSprites[Convert.ToInt32(str.Substring(0, 1))];
        images[val2].sprite = numberSprites[Convert.ToInt32(str.Substring(1, 1))];
    }

    IEnumerator TimerStart()
    {
        while (restTime >= 0)
        {
            int sec = Mathf.FloorToInt(restTime % 60);
            SetNumbers(sec, 2, 3);
            int minu = Mathf.FloorToInt((restTime - sec) / 60);
            SetNumbers(minu, 0, 1);
            yield return new WaitForSeconds(1.0f);
         //   restTime -= 1.0f;
        }
    }
}