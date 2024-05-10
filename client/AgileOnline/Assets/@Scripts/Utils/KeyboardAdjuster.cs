using UnityEngine;

public class KeyboardAdjuster : MonoBehaviour
{
    public RectTransform targetRectTransform; // ������ RectTransform
    private Vector2 originalPosition; // ���� ��ġ ����

    void Start()
    {
        // ���� ��ġ ����
        originalPosition = targetRectTransform.anchoredPosition;
    }

    void Update()
    {
        AdjustForKeyboard();
    }

    private void AdjustForKeyboard()
    {
        if (TouchScreenKeyboard.visible)
        {
            if (TouchScreenKeyboard.area.height > 0)
            {
                float keyboardHeight = TouchScreenKeyboard.area.height;
                targetRectTransform.anchoredPosition = new Vector2(originalPosition.x, originalPosition.y + keyboardHeight);
            }
        }
        else
        {
            targetRectTransform.anchoredPosition = originalPosition;
        }
    }
}