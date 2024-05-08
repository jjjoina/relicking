using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Util;



public class UI_GachaResultPopup : UI_Popup
{
    
    // ����� ������ ��������
    [SerializeField]
    GameObject UI_GachaRelicObject;
    
    #region Enum
    enum EGameObjects
    {
        OpenContentObject, // ��� ���� â
        ContentObject, // ���â
        GachaResultListObject, // ��ġ �ʿ�
    }

    enum EButtons
    {
        SkipButton,
        CloseButton,
    }

    enum ETexts
    {

    }

    #endregion

    // ��ü ���� ���� ��
    private List<GachaRelic> _relics;

    private Canvas _canvas;
    

    // �ʱ� ����
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        #region Object Bind

        BindObject(typeof(EGameObjects));
        BindButton(typeof(EButtons));
        
        // ���â ����
        GetObject((int)EGameObjects.OpenContentObject).SetActive(true);
        GetObject((int)EGameObjects.ContentObject).SetActive(false);
        
        // skip ��ư
        GetButton((int)EButtons.SkipButton).gameObject.BindEvent(OnClickSkipButton);
        
        // Close ��ư
        GetButton((int)EButtons.CloseButton).gameObject.BindEvent(OnClickCloseButton);


        #region Camera
        
        // ĵ���� ��������
        _canvas = GetComponent<Canvas>();
        
        // Main ī�޶� ��������
        Camera mainCamera = Camera.main;
        
        if (mainCamera != null)
        {
            // Main Camera�� Render Camera�� �����մϴ�.
            _canvas.worldCamera = mainCamera;
        }
        
        #endregion
        
        #endregion
        
        Refresh();

        return true;
    }

    // ������ ���� ���� ����Ʈ��������
    public void SetRelicsData(List<GachaRelic> gachaRelics)
    {
        _relics = gachaRelics;
        
        Init(); // �̰� �־��ִ� ������ Bind�� ���ؼ� �� ������...
        UIRefresh();
    }

    
    void UIRefresh()
    {
        
        if (_relics != null && _relics.Count > 0)
        {
            foreach (GachaRelic relic in _relics)
            {
                #region �����
                // �� ���
                // GameObject item = Instantiate(UI_GachaRelicObject,
                //     GetObject((int)EGameObjects.GachaResultListObject).transform);

                #endregion
                
                #region ��Ű�� ���
            
                // ��Ű�� ���
                GameObject container = GetObject((int)EGameObjects.GachaResultListObject);
                container.DestroyChilds();
                
                UI_GachaRelicObject item = Managers.Resource.Instantiate("UI_GachaRelicObject", pooling: true)
                    .GetOrAddComponent<UI_GachaRelicObject>();
                
                item.transform.SetParent(container.transform);
                #endregion
                // Todo -> ������� �°� �� �� (�� ���ĺ��� �ٸ��� ����� �� ��!!)
                // �� �����۵��� ���� �������ֱ�
                item.GetComponent<UI_GachaRelicObject>().SetInfo(relic);
                
                // ���Ƽ ����ũ �̻� �ִ��� ���� ã�� -> ������ ���� �� Ȳ������ �ٲٱ�
                
            }    
        }
        else
        {
            Debug.Log("ó���� ������ �����ϴ�.");
        }
        
    }
    
    // ����
    void Refresh()
    {

    }

    void OnClickSkipButton()
    {
        // �̱� ������ ��ŵ�ϰ� ��� �����ֱ�
        GetObject((int)EGameObjects.OpenContentObject).gameObject.SetActive(false);
        GetObject((int)EGameObjects.ContentObject).gameObject.SetActive(true);
        _canvas.worldCamera = null;
    }
    
    void OnClickCloseButton()
    {
        Debug.Log("CloseGachaResultPopup");
        Transform UI_root = gameObject.transform.parent; // @UI_Root
        FindChild(UI_root.gameObject, "UI_LobbyScene").SetActive(true);
        FindChild(UI_root.gameObject, "UI_GachaPopup").SetActive(true);
        Managers.UI.ClosePopupUI(this);
    }


    #region �ִϸ��̼� �̺�Ʈ, ��ƼŬ �̺�Ʈ

    [SerializeField] 
    private GameObject _particle;

    public void PlayParticle()
    {
        _particle.SetActive(true);
        StartCoroutine(CoSkil());
    }

    IEnumerator CoSkil()
    {
        yield return new WaitForSeconds(3f);
        OnClickSkipButton();
    }

    #endregion
}
