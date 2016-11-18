using UnityEngine;
using System.Collections;

public class TouchPointer : MonoBehaviour {

    private Camera _camera;
    private GameObject _chilled;
    void Start()
    {
        _chilled = transform.FindChild("TouchParticle").gameObject;
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    }
    void Update()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Stage")//隠れるブロックをクリック
                {
                    _chilled.SetActive(true);
                    transform.position = hit.point;
                }

                // Do something with the object that was hit by the raycast.
            }
        }
        else
        {
            _chilled.SetActive(false);
        }
    }
}
