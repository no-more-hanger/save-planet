using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveUserData : MonoBehaviour {
    // name field
    public TMP_InputField inputNameField;

    private void Start() {
        // 만약 이름 있으면 바로 스토리 보여주기
        if (GameStaticData._dataInstance.isHasName()) {
            GameObject.Find("Canvas").transform.Find("InputNamePopup").gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.Find("Story").gameObject.SetActive(true);
        }
    }

    // name field save
    public void SaveStoryScene() {
        GameStaticData._dataInstance.SaveName(inputNameField.text);
    }

    public void CreateRandomName() {
        inputNameField.text = System.Guid.NewGuid().ToString().Substring(0, 4);
    }
}
