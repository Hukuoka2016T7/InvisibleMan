using UnityEngine;

public class ShootUI : MonoBehaviour
{

    private float timer = 0;
    public float photoTime = 0;

    void Update()
    {
        timer += Time.deltaTime;
        Debug.Log(timer);
    }

    void OnGUI()
    {
        if (timer>photoTime)
        {
            GetComponent<CaptureAndShareImage>().Shoot();
            timer = 0;
        }
    }
}