using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneCover : UI_Base
{
    public Image coverImage;
    public float coverSpeed = 1.5f;
    private RectTransform coverRectTransform;

    void Start()
    {
        coverRectTransform = coverImage.GetComponent<RectTransform>();
        // �ʱ� ��ġ ����: ȭ�� ���� �ٱ��� ��ġ��Ű��
        coverRectTransform.anchoredPosition = new Vector2(0, coverRectTransform.sizeDelta.y);
        StartCoroutine(CoverScreen());
        
        // �� GameObject�� �� ��ȯ �߿��� �ı����� �ʵ��� ����
        DontDestroyOnLoad(gameObject);
    }

    public void CoverToScene(Define.EScene eScene)
    {
        StartCoroutine(UncoverScreen(eScene));
    }

    IEnumerator CoverScreen()
    {
        float positionY = coverRectTransform.anchoredPosition.y;

        // �̹����� ȭ�� �Ʒ��� ����������
        while (positionY > 0)
        {
            positionY -= Time.deltaTime * coverSpeed * coverRectTransform.sizeDelta.y;
            coverRectTransform.anchoredPosition = new Vector2(0, positionY);
            yield return null;
        }
    }
    
    IEnumerator UncoverScreen(Define.EScene eScene)
    {
        float positionY = coverRectTransform.anchoredPosition.y;

        // ȭ���� �ٽ� ���� �о��
        while (positionY < coverRectTransform.sizeDelta.y)
        {
            positionY += Time.deltaTime * coverSpeed * coverRectTransform.sizeDelta.y;
            coverRectTransform.anchoredPosition = new Vector2(0, positionY);
            yield return null;
        }

        Managers.Scene.LoadScene(eScene);
        
        // �� ���� �ε�� ��, �� �̹����� ��Ȱ��ȭ�ϰų� ����
        Destroy(gameObject);  // �� ����� GameObject�� ������ �ı��մϴ�.
    }
}