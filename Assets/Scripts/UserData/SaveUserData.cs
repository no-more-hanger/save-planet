using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveUserData : MonoBehaviour {
    // name field
    public TMP_InputField inputNameField;

    // name field save
    public void SaveStoryScene() {
        GameStaticData._dataInstance.SaveName(inputNameField.text);
    }
}