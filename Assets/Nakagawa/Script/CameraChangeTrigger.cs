using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraChangeTrigger : MonoBehaviour {

    public Camera m_Camera;
    public float m_Duration = 1f;
    public AnimationCurve m_Curve;

    Vector3 m_StartPosition;
    Vector3 m_Destination;

    Quaternion m_StartRotation;
    Quaternion m_DestRotation;

    bool m_Interporating;
    float m_StartTime;

    void Start()
    {
        m_Destination = m_Camera.transform.position;
        m_DestRotation = m_Camera.transform.rotation;
    }

    void Update()
    {
            if (!m_Interporating)
            return;

        float elapsed = Time.time - m_StartTime;

        if (elapsed >= m_Duration)
        {
            m_Camera.transform.position = m_Destination;
            m_Camera.transform.rotation = m_DestRotation;
            m_Interporating = false;
            return;
        }

        float progressRate = elapsed / m_Duration;
        progressRate *= m_Curve.Evaluate(progressRate);

        m_Camera.transform.position =
            Vector3.Lerp(
                m_StartPosition,
                m_Destination,
                progressRate);

        m_Camera.transform.rotation =
            Quaternion.Slerp(
                m_StartRotation,
                m_DestRotation,
                progressRate);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (m_Interporating)
            return;

        if (other.tag == "Player")
        {
            m_Interporating = true;
            m_StartTime = Time.time;
            m_StartPosition = Camera.main.transform.position;
            m_StartRotation = Camera.main.transform.rotation;

            Camera.main.enabled = false;
            m_Camera.enabled = true;

            m_Camera.transform.position = m_StartPosition;
            m_Camera.transform.rotation = m_StartRotation;
        }
    }
}
