using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {
    // load 01 stage
    public void OnLoadStage01Scene() {
        SceneManager.LoadScene("Stage01Scene");
    }

    // load this stage
    public void OnLoadStageScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // load next stage
    public void LoadNextScene() {
        SceneManager.LoadScene($"Stage0{GameStaticData._dataInstance.currentStage + 1}Scene");
    }

    // load start scene
    public void OnLoadStartScene() {
        SceneManager.LoadScene("StartScene");
    }

    // quit
    public void OnQuit() {
        Application.Quit();
    }
}
