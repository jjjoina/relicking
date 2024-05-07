using System;
using System.Collections;
using TMPro;
using UnityEngine;
using static Define;
using static Util;

#region ���� ����

[Serializable]
public class IdleDataReq
{
    public int lockTime;
}

[Serializable]
public class IdleDataRes
{
    public int status;
    public string message;
    public IdleRewardData data;
}

[Serializable]
public class IdleRewardData
{
    public int earnedGacha;
    public int bonusGacha;
    public int gachaAfterLock;
}

#endregion

public class UI_IdleRewardInfoPopup: UI_Popup
{
     #region Enum

    enum EGameObjects
    {
        TicketCounter,
    }

    enum EButtons
    {
        CloseButton,
    }

    enum ETexts
    {
        TotalIdleContent,
        RewardBonusText,
        Tickets,
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

        GetButton((int)EButtons.CloseButton).gameObject.BindEvent(OnClickCloseButton);

        #endregion

        PostIdleReward();
        
        Managers.Game.OnResourcesChanged += Refresh;
        Refresh();

        return true;
    }

    void PostIdleReward()
    {
        // get ���� ���
        // StartCoroutine(JWTPostRequest("lock", res =>
        // {
        //     // json -> ��ü�� ��ȯ
        //     GrowthStaticDataRes growthDataRes = GrowthStaticDataRes.FromJson(res);
        //
        //     // �� ������ ó��
        //     if (growthDataRes.status == 200)
        //     {
        //         // UI ������Ʈ ���ֱ� -> �ٵ�.. �̰� ȭ�� ���ö����� ������Ʈ �ϴ� �޼ҵ带 ���� �����ؾ��� �� �;��
        //         UpdateGrowthUI(growthDataRes.data);
        //     }
        // }));

        IdleDataReq idleDataReq = new IdleDataReq
        {
            lockTime = Managers.Game.idleRewardTime
        };
        
        string idleJsonData = JsonUtility.ToJson(idleDataReq);

        StartCoroutine(JWTPostRequest("lock/end", idleJsonData, res =>
        {
            // json -> ��ü�� ��ȯ
            IdleDataRes idleDataRes = JsonUtility.FromJson<IdleDataRes>(res);

                
            if (idleDataRes.status == 200)
            {
                UpdateRewardUI(idleDataRes.data);
            }
                
        }));
    }
    
    void UpdateRewardUI(IdleRewardData data)
    {
        GetText((int)ETexts.TotalIdleContent).text = FormatTime(Managers.Game.idleRewardTime);
        GetText((int)ETexts.RewardBonusText).text = $"��Ʈ�� ���� ���ʽ� {data.bonusGacha}%";
        GetText((int)ETexts.Tickets).text = data.earnedGacha.ToString();

        if (data.bonusGacha > 0)
        {
            // ���ʽ� ȿ���� 0���� Ŭ ���� ���ʽ��� �޴´ٴ� �ؽ�Ʈ Ȱ��ȭ
            GetText((int)ETexts.RewardBonusText).gameObject.SetActive(true);
        }
        else
        {
            // ���ʽ� ȿ���� 0 ������ �� �ؽ�Ʈ ��Ȱ��ȭ
            GetText((int)ETexts.RewardBonusText).gameObject.SetActive(false);
        }
    }

    // ����
    void Refresh()
    {
    }

    void OnClickCloseButton()
    {
        Debug.Log("�����ϱ� Clicked");
        Managers.UI.ClosePopupUI(this);
    }
    
    string FormatTime(int totalSeconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(totalSeconds);
        return time.ToString(@"hh\:mm\:ss");
    }
}