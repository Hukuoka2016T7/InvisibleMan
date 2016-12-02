using UnityEngine;
using System.Collections;

public class TitleMain : MonoBehaviour {

    private SceneChanger _sceneChang;

	void Start () {
        _sceneChang = transform.GetComponent<SceneChanger>();


    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))//クリックしたら
        {
            _sceneChang.FadeIn();
        }
    }
}
