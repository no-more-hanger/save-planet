using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackController : MonoBehaviour
{
    public Button backBtn;
    private bool isAble = true;

    public void OnAbleKey() {
        isAble = true;
    }

    public void OnNotAbleKey() {
        isAble = false;
    }

    void Update()
    {
        if (isAble && Input.GetKeyDown(KeyCode.B)) {
            SoundManager._soundInstance.OnButtonAudio();
            backBtn.onClick.Invoke();
        }       
    }
}
