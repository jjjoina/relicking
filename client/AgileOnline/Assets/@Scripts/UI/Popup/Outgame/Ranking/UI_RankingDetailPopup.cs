using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_RankingDetailPopup : UI_Popup
{
    #region Enum

    enum EGameObjects
    {
        ContentObject,
        RelicListObject,
        SkillListObject,
    }

    enum EButtons
    {
        ButtonBG,
        CloseButton,
    }

    enum ETexts
    {
        TitleText,
        PlayerName,
        RecordDate,
        DifficultyText,
        EliteKillText,
        KillText,
    }

    enum EImages
    {
        ClassImage,
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
        BindText(typeof(ETexts));

        GetButton((int)EButtons.ButtonBG).gameObject.BindEvent(OnClickCloseButton);
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
        Debug.Log("CloseRankingDetail");
        Managers.UI.ClosePopupUI(this);
    }
}
