using UnityEngine;
using System.Collections;

public class Shadow : MonoBehaviour {

    private PlayerStatus _pStatus;
    private MeshRenderer _meshRenderer;

    // Use this for initialization
    void Start () {
        _pStatus = transform.root.GetComponent<PlayerStatus>();
        _meshRenderer = transform.GetComponent<MeshRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        print(_pStatus._invisibleTr);
        if (_pStatus._invisibleTr == true)
        {
            _meshRenderer.enabled = false;
        }
        else
        {
            _meshRenderer.enabled = true;
        }
	
	}
}
