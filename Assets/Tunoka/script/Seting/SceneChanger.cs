﻿using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    public string m_scenechange;

    [SerializeField, Header("フェード関係")]
    private GameObject m_Fade;

    [SerializeField, Header("変わるまでの時間")]
    private int m_fadeTime = 2;
    bool m_tr = true;

    [SerializeField, Header("音楽")]
    private GameObject[] SaundObj;


    void Awake()
    {
        m_Fade.gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, 255);
    }
    void Start()
    {
        FadeOut();
    }

    public void FadeIn()
    {
        m_tr = true;
        // SetValue()を毎フレーム呼び出して、１秒間に０から１までの値の中間値を渡す
        iTween.ValueTo(gameObject, iTween.Hash("from", 0f, "to", 1f, "time", m_fadeTime, "onupdate", "SetValue"));
    }
    public void HalfFadeIn()
    {
        // SetValue()を毎フレーム呼び出して、１秒間に０から0.5までの値の中間値を渡す
        iTween.ValueTo(gameObject, iTween.Hash("from", 0f, "to", 0.9f, "time", m_fadeTime, "onupdate", "SetValue"));
    }

    public void FadeIn(string name)
    {
        m_scenechange = name;
        m_tr = true;
        // SetValue()を毎フレーム呼び出して、１秒間に０から１までの値の中間値を渡す
        iTween.ValueTo(gameObject, iTween.Hash("from", 0f, "to", 1f, "time", m_fadeTime, "onupdate", "SetValue"));
    }
    public void FadeOut()
    {
        print("FadeOut");
        // SetValue()を毎フレーム呼び出して、１秒間に１から０までの値の中間値を渡す
        iTween.ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f, "time", m_fadeTime, "onupdate", "SetValue"));
        m_tr = false;
    }
    void SetValue(float alpha)
    {

        if (SaundObj != null)
        {
            float volume = 1 - alpha;
            foreach (GameObject value in SaundObj)
            {
                value.GetComponent<AudioSource>().volume = volume;
            }
        }

        m_Fade.gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, alpha);
        if (alpha >= 1 && m_tr) SceneChange(m_scenechange);
    }
    void SceneChange(string name)
    {
        print(name + "にシーンチェンジ");
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }
}
