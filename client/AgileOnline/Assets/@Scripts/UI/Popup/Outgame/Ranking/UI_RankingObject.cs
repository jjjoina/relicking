using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_RankingObject : UI_Base
{
    #region Enum

    enum EGameObjects
    {
        
    }
    
    enum EButtons
    {
        RankingDetailButton
    }
    
    enum ETexts
    {
        Rank,
        NickName,
        Difficulty
    }
    
    enum EToggles
    {
        
    }
    
    enum EImages
    {
        
    }
    
    #endregion
    
    // ��ü ����
    
    // Show�� �ϸ� ���� ������ �ϴ� ���̾ ��� �����ϴ� ������ �߻��ؼ� �̷��� Ű�� ���� �������� ����!!!
    private UI_RankingDetailPopup _uiRankingDetailPopup; 
    
    private void Awake()
    {
        Init();
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
        BindToggle(typeof(EToggles));
        BindImage(typeof(EImages)); 
        
        
        // ��� �����Ǵ� ���� �����ϱ� ����
        _uiRankingDetailPopup = Managers.UI.ShowPopupUI<UI_RankingDetailPopup>();
        _uiRankingDetailPopup.gameObject.SetActive(false);
        
        #endregion
        
        
        return true;
    }

    // UI_RankingPopup���� ȣ���� ����.
    public void SetInfo(RankingInfo rankingInfo, int rank)
    {
        GetText((int)ETexts.Rank).text = $"{rank}";
        GetText((int)ETexts.NickName).text = rankingInfo.nickname;
        GetText((int)ETexts.Difficulty).text = $"{rankingInfo.difficulty}";
        GetButton((int)EButtons.RankingDetailButton).onClick.AddListener(()=> OnClickRankingDetailButton(rankingInfo, rank));
    }

    void OnClickRankingDetailButton(RankingInfo rankingInfo, int rank)
    {
        Managers.Sound.PlayButtonClick();
        _uiRankingDetailPopup.gameObject.SetActive(true);
        _uiRankingDetailPopup.SetRankingInfo(rankingInfo, rank);
    }
    
}
