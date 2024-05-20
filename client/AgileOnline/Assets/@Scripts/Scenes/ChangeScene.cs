using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : UI_Popup
{
    public float transitionTime = 1f;

    #region Enum
    enum EGameObjects
    {
        DownScene,
    }

    enum EImages
    {
        
    }
    #endregion
    
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        #region Object Bind
        
        BindObject(typeof(EGameObjects));
        
        GetObject((int)EGameObjects.DownScene).SetActive(false);
        
        #endregion
        
        return true;
    }
    
    // �� ��ȯ �Լ�
    public void LoadScene(Define.EScene eScene)
    {
        StartCoroutine(Transition(eScene));
    }

    IEnumerator Transition(Define.EScene eScene)
    {
        
        GetObject((int)EGameObjects.DownScene).SetActive(true);
        // �ִϸ��̼� ��� �ð���ŭ ����մϴ�.
        yield return new WaitForSeconds(transitionTime);

        // �� �ε�
        Managers.Scene.LoadScene(eScene);
    }
}
