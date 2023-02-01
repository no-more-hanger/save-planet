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

    void Awake() {
        _dataInstance = this;
    }

    private void Start() {
        var obj = FindObjectsOfType<GameStaticData>();

        if (obj.Length == 1) {
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
}
