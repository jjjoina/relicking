using UnityEngine;
using UnityEngine.UI;

public class KeyboardAdjuster : MonoBehaviour
{
    public RectTransform inputFieldRectTransform; // �Է� �ʵ��� RectTransform
    private Vector2 originalPosition; // ���� ��ġ ����

    void Start()
    {
        originalPosition = inputFieldRectTransform.anchoredPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            AdjustForKeyboard();
        }
    }

    private void AdjustForKeyboard()
    {
        // ��ġ ��ũ�� Ű������ ���� Ȯ��
        if (TouchScreenKeyboard.visible)
        {
            // Ű���尡 Ȱ��ȭ�Ǹ� �Է� �ʵ带 ���� �̵�
            if (TouchScreenKeyboard.area.height > 0)
            {
                float keyboardHeight = TouchScreenKeyboard.area.height;
                inputFieldRectTransform.anchoredPosition = new Vector2(originalPosition.x, originalPosition.y + keyboardHeight);
            }
        }
        else
        {
            // Ű���尡 ��Ȱ��ȭ�Ǹ� ���� ��ġ�� ���ư�
            inputFieldRectTransform.anchoredPosition = originalPosition;
        }
    }
}