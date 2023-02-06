using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowUserData : MonoBehaviour {
    // show name
    public TextMeshProUGUI showName;
    // show stage
    public TextMeshProUGUI showStage;
    // show score
    public TextMeshProUGUI showScore;

    [SerializeField]
    private ChangeScene changeScene;


    private void Start() {
        //PlayerPrefs.DeleteAll();
        LoadStartScene();
    }

    // show name, score, stage
    public void LoadStartScene() {
        GameStaticData gameStaticData = GameStaticData._dataInstance;

        // 이름 없으면 이름 적으러 가기
        if (!gameStaticData.isHasName()) {
            changeScene.OnLoadStoryScene();
            return;
        }

        // 이름
        showName.text = gameStaticData.LoadName();
        // 최고 기록
        var score = gameStaticData.LoadMaxScore();
        showScore.text = score != 0 ? gameStaticData.GetTimeString(score) : "-- : --";
        // 최고 스테이지 | 3stage까지 깼으면, 엔딩 다시 볼 수 있도록 함
        var stage = gameStaticData.LoadMaxStage();
        showStage.text = stage != 0 ? stage.ToString() : "-";
        GameObject.Find("Canvas").transform.Find("EndingButton").gameObject.SetActive(stage == 3 ? true : false);
    }
}
