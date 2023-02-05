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
        var stage = GameStaticData._dataInstance.LoadCurrentStage();

        // 기록 없으면 return
        if (stage == 0) {
            GameObject.Find("Canvas").transform.Find("CheckPopup").gameObject.SetActive(true);
            return;
        }
        // 현재 3단계일 때, start 씬에서 이어하기는 3stage로 | 3stage 씬에서 깬 후, 이어하기는 엔딩으로
        // 3단계는 Ending 으로
        var sceneName = SceneManager.GetActiveScene().name;
        if ((stage <= 3 && sceneName == "StartScene") || (stage < 3 && sceneName == "Stage03Scene")) {
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

    // load story scene
    public void OnLoadStoryScene() {
        SceneManager.LoadScene("StoryScene");
    }

    // quit
    public void OnQuit() {
        Application.Quit();
    }
}
