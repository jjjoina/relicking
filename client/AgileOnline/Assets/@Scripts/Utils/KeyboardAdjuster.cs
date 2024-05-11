using System.Collections;
using UnityEngine;

public class KeyboardAdjuster : MonoBehaviour
{
    public RectTransform targetRectTransform; // ������ RectTransform
    public Vector2 originalPosition; // ���� ��ġ ����

    void Start()
    {
        // ���� ��ġ ����
        originalPosition = targetRectTransform.anchoredPosition;
        StartCoroutine(AdjustForKeyboard()); // �ڷ�ƾ ����
    }

    private IEnumerator AdjustForKeyboard()
    {
        while (true)
        {
            // Ű���尡 ���� ������ ���
            yield return new WaitUntil(() => TouchScreenKeyboard.visible);
            float keyboardHeight = TouchScreenKeyboard.area.height;
            Debug.Log($"Keyboard visible height: {keyboardHeight}");

            if (keyboardHeight > 0)
            {
                // Ű���� ���̿� ���� ��ġ ����
                targetRectTransform.anchoredPosition = new Vector2(originalPosition.x, originalPosition.y + keyboardHeight);
            }

            // Ű���尡 ����� ������ ���
            yield return new WaitUntil(() => !TouchScreenKeyboard.visible);
            Debug.Log($"Keyboard visible height: {keyboardHeight}");
            // ���� ��ġ�� ����
            targetRectTransform.anchoredPosition = originalPosition;
        }
    }
}