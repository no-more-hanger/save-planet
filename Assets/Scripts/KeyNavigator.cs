using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyNavigator : MonoBehaviour
{
    EventSystem system;

    void Start() {
        system = EventSystem.current;
    }

    void Update() {
        Selectable next = null;

        if (system.currentSelectedGameObject == null) {
            system.SetSelectedGameObject(system.firstSelectedGameObject, new BaseEventData(system));
        }

        if (system.currentSelectedGameObject != null & Input.GetKeyDown(KeyCode.A)) {
            Debug.Log("A버튼");
            system.currentSelectedGameObject.GetComponent<Button>()?.onClick.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            Debug.Log("왼쪽");
            next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnLeft();  
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            Debug.Log("오른쪽");
            next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnRight();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            Debug.Log("위쪽");
            next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            Debug.Log("아래쪽");
            next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
        }
        
        if (next != null) {
            system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
        }
    }
}
