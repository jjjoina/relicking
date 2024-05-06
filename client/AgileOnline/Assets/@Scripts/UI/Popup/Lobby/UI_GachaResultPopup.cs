using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UI_GachaResultPopup : UI_Popup
{
    
    // ����� ������ ��������
    [SerializeField]
    GameObject UI_GachaRelicObject;
    
    
    #region Enum
    enum EGameObjects
    {
        GachaResultListObject, // ��ġ �ʿ�
    }

    enum EButtons
    {
        CloseButton,
    }

    enum ETexts
    {

    }

    #endregion

    // ��ü ���� ���� ��
    private List<GachaRelic> _relics;
    

    // �ʱ� ����
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        #region Object Bind

        BindObject(typeof(EGameObjects));
        BindButton(typeof(EButtons));

        GetButton((int)EButtons.CloseButton).gameObject.BindEvent(OnClickCloseButton);
       
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

    void OnClickCloseButton()
    {
        Debug.Log("CloseGachaResultPopup");
        Managers.UI.ClosePopupUI(this);
    }
}
