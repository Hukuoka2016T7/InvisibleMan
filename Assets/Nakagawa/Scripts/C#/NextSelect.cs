using UnityEngine;
using System.Collections;
//追加
using UnityEngine.SceneManagement;

public class NextSelect : MonoBehaviour {

    public void SceneLoad()
    {
       SceneManager.LoadScene("Select");
    }
}
