using UnityEngine;
using System.Collections;

public class RayTest : MonoBehaviour {

    private Camera _camera;
    private GameObject _chilled;
    void Start()
    {
        _chilled = transform.FindChild("DebugRay").gameObject;
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    }
    void Update()
    {
        int layerMask = ~(1 << 8);
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            _chilled.SetActive(true);
            transform.position = hit.point;
      
            // Do something with the object that was hit by the raycast.
        }
        else
        {
            _chilled.SetActive(false);
        }
    }
}
