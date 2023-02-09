using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyNavigator : MonoBehaviour
{
    EventSystem system;
    public GameObject firstSelectObj;
    public bool useNavigate = false;
    private bool isReset = true;

    void Start() {
        system = EventSystem.current;
        system.SetSelectedGameObject(firstSelectObj, new BaseEventData(system));
    }

    public void IsReset() {
        isReset = true;
    }

    void Update() {
        if (system.currentSelectedGameObject == null && isReset) {
            isReset = false;
            system.SetSelectedGameObject(firstSelectObj, new BaseEventData(system));
        }

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
