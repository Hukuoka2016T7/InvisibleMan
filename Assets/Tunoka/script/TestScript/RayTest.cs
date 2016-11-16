using UnityEngine;
using System.Collections;

public class RayTest : MonoBehaviour {

    public Camera camera;

    void Update()
    {
        int layerMask = ~(1 << 8);
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.transform.tag == "Stage")
            {
                transform.position = hit.point;
            }

            // Do something with the object that was hit by the raycast.
        }
    }
}
