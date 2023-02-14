using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStaticData : MonoBehaviour {
    public static GameStaticData _dataInstance;

    // background music on/off
    public bool isBgm = true;

    // effect sound on/off
    public bool isSound = true;

    // background music volume;
    public float bgmVolume = 1f;

    // effect sound volume;
    public float soundVolume = 1f;

    // current stage level
    public int currentStage = 1;

    // Player Data | <key>
    private string KEY_NAME = "Name";                   // 이름
    private string KEY_CURRENT_STAGE = "CurrentStage";  // 현재 어느 스테이지까지 깼는지
    private string KEY_CURRENT_SCORE = "CurrentScore";  // 스테이지 1, 2, 3 현재 스코어 -> 시간 : int형
    private string KEY_MAX_STAGE = "MaxStage";  // 최고 기록의 완료한 스테이지
    private string KEY_MAX_SCORE = "MaxScore"; //최고 기록(합산된 점수) ← 각 스테이지 완료한 시점에서 갱신 확인
    private string KEY_CURRENT_BALLOON_CNT = "CurrentBalloonCnt"; // 현재 풍선 개수

    private void Awake() {
        var obj = FindObjectsOfType<GameStaticData>();

        if (obj.Length == 1) {
            _dataInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    // Player Data | save name
    public void SaveName(string value) {
        PlayerPrefs.SetString(KEY_NAME, value);
    }

    // Player Data | load name
    public string LoadName() {
        return PlayerPrefs.HasKey(KEY_NAME) ? PlayerPrefs.GetString(KEY_NAME) : "Guest";
    }

    // is saved name
    public bool isHasName() {
        return PlayerPrefs.HasKey(KEY_NAME);
    }

    // Player Data | save CurrentStage
    public void SaveCurrentStage(int stage) {
        PlayerPrefs.SetInt(KEY_CURRENT_STAGE, stage);
    }

    // Player Data | load CurrentStage
    public int LoadCurrentStage() {
        return PlayerPrefs.HasKey(KEY_CURRENT_STAGE) ? PlayerPrefs.GetInt(KEY_CURRENT_STAGE) : 0; // 0 이면 기록 없음
    }

    // Player Data | save CurrentScore
    public void SaveCurrentScore(int score) {
        PlayerPrefs.SetInt(KEY_CURRENT_SCORE, score);
    }

    // Player Data | load CurrentScore
    public int LoadCurrentScore() {
        return PlayerPrefs.HasKey(KEY_CURRENT_SCORE) ? PlayerPrefs.GetInt(KEY_CURRENT_SCORE) : 0; // 0 이면 기록 없음
    }

    // Player Data | save MaxStage
    public void SaveMaxStage(int stage) {
        PlayerPrefs.SetInt(KEY_MAX_STAGE, stage);
    }

    // Player Data | load MaxStage
    public int LoadMaxStage() {
        return PlayerPrefs.HasKey(KEY_MAX_STAGE) ? PlayerPrefs.GetInt(KEY_MAX_STAGE) : 0; // 0 이면 기록 없음
    }

    // Player Data | save MaxScore
    public void SaveMaxScore(int score) {
        PlayerPrefs.SetInt(KEY_MAX_SCORE, score);
    }

    // Player Data | load MaxScore
    public int LoadMaxScore() {
        return PlayerPrefs.HasKey(KEY_MAX_SCORE) ? PlayerPrefs.GetInt(KEY_MAX_SCORE) : 0; // 0 이면 기록 없음
    }

    public void SaveCurrentBalloonCnt(int cnt) {
        PlayerPrefs.SetInt(KEY_CURRENT_BALLOON_CNT, cnt);
    }

    // Player Data | load BalloonCnt
    public int LoadCurrentBalloonCnt() {
        return PlayerPrefs.HasKey(KEY_CURRENT_BALLOON_CNT) ? PlayerPrefs.GetInt(KEY_CURRENT_BALLOON_CNT) : 0;
    }

    public string GetTimeString(int sec) {
        int min = sec / 60;

        return string.Format("{0, 2:00} : {1, 2:00}", min, sec - min * 60);
    }

    public float GetResponsivePoint(float x) {
        return x * Screen.width / Screen.height * 2560 / 1440;
    }
}
