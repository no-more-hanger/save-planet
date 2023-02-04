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

    // show name
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
        // 최고 스테이지
        var stage = gameStaticData.LoadMaxStage();
        showStage.text = stage != 0 ? stage.ToString() : "-";
    }
}
