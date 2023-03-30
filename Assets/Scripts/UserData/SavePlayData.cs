using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SavePlayData : MonoBehaviour {
    private TimerController timerController;

    // show save score
    [SerializeField]
    private TextMeshProUGUI showPlayScore;
    // show accumulate score
    [SerializeField]
    private TextMeshProUGUI showAccumulatePlayScore;
    // show max stage
    [SerializeField]
    private TextMeshProUGUI showMaxStage;
    // show max score
    [SerializeField]
    private TextMeshProUGUI showMaxScore;

    // current stage
    private int currentPlayStage;
    // current score
    private int currentPlayScore;
    // 누적 스코어
    private int accumulatePlayScore;

    // max score
    private int maxScore;
    // max stage
    private int maxStage;

    void Start() {
        timerController = GameObject.FindWithTag("Timer").GetComponent<TimerController>();
        SavePlayEnding();
    }

    public void SavePlayEnding() {
        GameStaticData gameStaticData = GameStaticData._dataInstance;
        currentPlayStage = gameStaticData.LoadCurrentStage();
        currentPlayScore = timerController.GetTimeSeconds();

        maxScore = gameStaticData.LoadMaxScore();
        maxStage = gameStaticData.LoadMaxStage();
        accumulatePlayScore = currentPlayScore;

        // 1스테이지에선 누적합 없음
        if (currentPlayStage != 1) {
            accumulatePlayScore += gameStaticData.LoadCurrentScore();
        }

        // 끝나자마자 현재 스테이지, TImer 스코어로 current 저장
        // 최고 기록은 현재 스테이지, 스코어와 비교해서 max에 저장
        var tmpStage = currentPlayStage + 1;
        gameStaticData.SaveCurrentStage(tmpStage > 3 ? 3 : tmpStage);          // 현재 플레이 스테이지
        gameStaticData.SaveCurrentScore(accumulatePlayScore);                  // 현재 플레이 스코어

        // 스테이지 높으면 바로 갱신
        // 스테이지 같거나 높고, 스코어 높으면 or 점수 없으면 갱신
        if (maxStage < currentPlayStage || (maxStage <= currentPlayStage && (maxScore > accumulatePlayScore || maxScore == 0))) {
            gameStaticData.SaveMaxStage(currentPlayStage);
            gameStaticData.SaveMaxScore(accumulatePlayScore);
            maxScore = accumulatePlayScore;
            maxStage = currentPlayStage;
        }

        showPlayScore.text = gameStaticData.GetTimeString(currentPlayScore);// 현재 스코어 보여주기
        showMaxStage.text = maxStage.ToString();                            // 최고 스테이지 보기
        showMaxScore.text = gameStaticData.GetTimeString(maxScore);         // 최고 스코어 보여주기
        showAccumulatePlayScore.text = gameStaticData.GetTimeString(accumulatePlayScore);// 누적합
    }
}