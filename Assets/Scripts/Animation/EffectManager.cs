using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EffectManager : MonoBehaviour
{
    private float typingInterval = 0.1f;

    public void DeleteBlurr() {
        Image image = GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
    }

    public void Blurr() {
        Image image = GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0.5f);
    }

    public void FadeIn(float time) {
        gameObject.SetActive(true);
        StartCoroutine(FadeEffect(1f, -time));
    }

    // fade out
    public void FadeOut(float time) {
        gameObject.SetActive(true);
        StartCoroutine(FadeEffect(0f, time));
    }

    IEnumerator FadeEffect(float start, float time) {
        Image image = GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, start);
        while (image.color.a != (1f - start)) {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + Time.deltaTime / time);
            yield return null;
        }
        gameObject.SetActive(false);
    }

    public void SkipTyping(string content) {
        StopAllCoroutines();
        GetComponent<TextMeshProUGUI>().text = content;
    }

    public void TextTyping(string content) {
        StartCoroutine(TypingAnimation(GetComponent<TextMeshProUGUI>(), content, typingInterval));
    }

    IEnumerator TypingAnimation(TextMeshProUGUI typingText, string message, float typingInterval) {
        for (int i = 0; i < message.Length; i++) {
            typingText.text = message.Substring(0, i + 1);
            yield return new WaitForSeconds(typingInterval);
        }
    }
}
