using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {
    public void OnLoadStage01Scene() {
        SceneManager.LoadScene("Stage01Scene");
    }

    // load next stage
    public void LoadNextScene() {
        SceneManager.LoadScene($"Stage0{GameStaticData._dataInstance.currentStage + 1}Scene");
    }
}
