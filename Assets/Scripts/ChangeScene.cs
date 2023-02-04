using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {
    // load scene name
    public void OnLoadSceneName(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    // load 01 stage
    public void OnLoadStage01Scene() {
        GameStaticData._dataInstance.SaveCurrentStage(1);
        GameStaticData._dataInstance.SaveCurrentBalloonCnt(0);
        SceneManager.LoadScene("Stage01Scene");
    }

    // load this stage
    public void OnLoadStageScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // next continue
    public void OnLoadNextScene() {
        // 3단계는 Ending 으로
        if (GameStaticData._dataInstance.LoadCurrentStage() <= 3) {
            LoadNextScene();
        }
        else {
            OnLoadEndingScene();
        }
    }

    // load next stage
    public void LoadNextScene() {
        SceneManager.LoadScene($"Stage0{GameStaticData._dataInstance.LoadCurrentStage()}Scene");
    }

    // load start scene
    public void OnLoadStartScene() {
        SceneManager.LoadScene("StartScene");
    }

    // load Ending scene
    public void OnLoadEndingScene() {
        SceneManager.LoadScene("EndingScene");
    }

    // quit
    public void OnQuit() {
        Application.Quit();
    }
}
