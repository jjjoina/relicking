using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Util;

#region ��ŷ �� ��ȸ (DetailRankingDataRes)

[Serializable]
public class DetailRankingDataRes
{
    public int status;
    public string message;
    public RankingDetail data;
}

[Serializable]
public class RankingDetail
{
    public int eliteKill;
    public int normalKill;
    public List<RelicDetail> relicList;
    public List<SkillDetail> skillList;
}

[Serializable]
public class RelicDetail
{
    public int relicNo;
    public int level; 
    public int slot; 
}

[Serializable]
public class SkillDetail
{
    public int skillNo;
    public int level; 
    public int slot; 
}

#endregion

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
    
    // ��ü ����
    private MyRankingInfo _myRankingInfo;

    private int _recordId; // �̰� �� ������ �� ��ũ ��ȸ�� �ٸ� ��� ��ũ ��ȸ�� �Լ��� �ѷ� �������� �����ϱ� ����.
    
    private DetailRankingDataRes _detailRankingDataRes;
    
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

        Refresh();

        return true;
    }


    public void SetMyRankingInfo(MyRankingInfo myRankingInfo)
    {
        _myRankingInfo = myRankingInfo;
        _recordId = myRankingInfo.recordId;
        Init();
        UIRefresh();

    }

    void UIRefresh()
    {
        GetDetailRankingInfo(_recordId);
        
        // ������� �ؾߵ�!!!!
        
    }
    
    void GetDetailRankingInfo(int recordId)
    {
        StartCoroutine(JWTGetRequest($"rankings/{recordId}", res =>
        {
            // json -> ��ü�� ��ȯ
            _detailRankingDataRes = JsonUtility.FromJson<DetailRankingDataRes>(res);


        }));
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
