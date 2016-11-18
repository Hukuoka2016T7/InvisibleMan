using UnityEngine;
using System.Collections;
//追加
using UnityEngine.SceneManagement;

public class NextStage1 : MonoBehaviour {

    public void SceneLoad()
    {
        SceneManager.LoadScene("Camera");  //この"Camera"は正規ステージが開発された際に要変更
    }
}
