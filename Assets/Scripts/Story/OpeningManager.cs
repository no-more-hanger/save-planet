using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningManager : MonoBehaviour
{
    [System.Serializable]
    public class CharacterChat {
        public string name;
        public string chat;
    }
    [System.Serializable]
    public class MyChatList {
        public CharacterChat[] characterChat;
    }

    public TextAsset textJson;
    public GameObject stroyPrefab;
    private EffectManager leftPerson, rightPerson, talkPanelName, talkPanelChat, fade;
    private CharacterChat[] chatList;
    private int curChat = 0;
    private bool isTyping = false;
    private float time = 1f;
    private bool isAble = false;

    private void Awake() {
        chatList = JsonUtility.FromJson<MyChatList>(textJson.text).characterChat;
        leftPerson = stroyPrefab.transform.GetChild(0).gameObject.GetComponent<EffectManager>();
        rightPerson = stroyPrefab.transform.GetChild(1).gameObject.GetComponent<EffectManager>();
        talkPanelName = stroyPrefab.transform.GetChild(2).GetChild(0).gameObject.GetComponent<EffectManager>();
        talkPanelChat = stroyPrefab.transform.GetChild(2).GetChild(1).gameObject.GetComponent<EffectManager>();
        fade = stroyPrefab.transform.GetChild(3).gameObject.GetComponent<EffectManager>();
        StoryStart();
    }

    private void StoryStart() {
        fade.FadeIn(time);
        Invoke(nameof(FirstChat), time);
    }

    private void StoryEnd() {
        isAble = false;
        fade.FadeOut(time / 2);
        Invoke(nameof(LastChat), time);
    }

    private void FirstChat() {
        isAble = true;
        ChatSetting(chatList[curChat].name, chatList[curChat].chat);
    }

    private void LastChat() {
        // load start scene
        GameObject.Find("ChangeScene").GetComponent<ChangeScene>().OnLoadStartScene();
    }

    private void LeftChatting() {
        leftPerson.DeleteBlurr();
        rightPerson.Blurr();
    }

    private void RightChatting() {
        leftPerson.Blurr();
        rightPerson.DeleteBlurr();
    }

    private string SetChat(string chat) {
        string newChat = chat;
        if (chat.Contains('*')) {
            string[] temp = chat.Split('*');
            newChat = temp[0] + GameStaticData._dataInstance.LoadName() + temp[1];
        }
        return newChat;
    }

    private void ChatSetting(string name, string chat) {
        isTyping = true;
        if (name == "나") {
            LeftChatting();
        }
        else {
            RightChatting();
        }
        talkPanelName.SkipTyping(name);
        talkPanelChat.TextTyping(chat);
    }

    private void Update() {
        if (isAble && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))) {
            if (isTyping) { //skip
                isTyping = false;
                talkPanelChat.SkipTyping(chatList[curChat].chat);
            }
            else { //next
                curChat++;
                if (curChat == chatList.Length) {
                    // start fade out
                    StoryEnd();
                }
                else {
                    chatList[curChat].chat = SetChat(chatList[curChat].chat);
                    ChatSetting(chatList[curChat].name, chatList[curChat].chat);
                }
            }
        }
    }
}
