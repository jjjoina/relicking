using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public Animator transitionAnimator;
    public float transitionTime = 1f;

    // �� ��ȯ �Լ�
    public void LoadScene(Define.EScene eScene)
    {
        StartCoroutine(Transition(eScene));
    }

    IEnumerator Transition(Define.EScene eScene)
    {
        // �ִϸ��̼��� �÷����մϴ�.
        transitionAnimator.SetTrigger("StartTransition");

        // �ִϸ��̼� ��� �ð���ŭ ����մϴ�.
        yield return new WaitForSeconds(transitionTime);

        // �� �ε�
        Managers.Scene.LoadScene(eScene);
    }
}
