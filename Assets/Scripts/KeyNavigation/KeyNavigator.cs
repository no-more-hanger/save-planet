using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyNavigator : MonoBehaviour
{
    EventSystem system;
    public GameObject firstSelectedObj;
    private GameObject lastSelectedObj = null;
    public bool useNavigate = false;

    void Awake() {
        system = EventSystem.current;
        system.SetSelectedGameObject(firstSelectedObj, new BaseEventData(system));
    }

    public void SetFirstSelectObj() {
        system.SetSelectedGameObject(firstSelectedObj, new BaseEventData(system));
    }

    public void SetLastSelectObj() {
        system.SetSelectedGameObject(lastSelectedObj, new BaseEventData(system));
    }

    void Update() {
        if (system.currentSelectedGameObject == null) {
            system.SetSelectedGameObject(firstSelectedObj, new BaseEventData(system));
        }

        // save current selected object
        lastSelectedObj = system.currentSelectedGameObject;

        if (useNavigate) {
            Selectable next = null;

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
}
