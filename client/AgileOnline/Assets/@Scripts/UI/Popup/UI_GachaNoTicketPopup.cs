using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GachaNoTicketPopup : UI_Popup
{
    #region Enum
    enum EGameObjects
    {
        ContentObjet,
    }

    enum EButtons
    {
        CloseButton
    }

    #endregion

    public void OnDestroy()
    {
        if (Managers.Game != null)
            Managers.Game.OnResourcesChanged -= Refresh;
    }

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

        Managers.Game.OnResourcesChanged += Refresh;
        Refresh();

        return true;
    }

    // ����
    void Refresh()
    {

    }

    void OnClickCloseButton()
    {
        Debug.Log("CloseGachaInfo");
        Managers.UI.ClosePopupUI(this);
    }
}
