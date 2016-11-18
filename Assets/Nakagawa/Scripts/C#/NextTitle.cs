using UnityEngine;
using System.Collections;
//追加
using UnityEngine.SceneManagement;

public class NextTitle : MonoBehaviour {

    public void SceneLoad()
    {
        SceneManager.LoadScene("Title");
    }
}
