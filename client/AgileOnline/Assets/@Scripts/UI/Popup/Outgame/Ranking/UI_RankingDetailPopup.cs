using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Util;
using static Define;

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
    public string updatedDate;
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
        
        // ���� ����
        LevelText1,
        LevelText2,
        LevelText3,
        LevelText4,
        LevelText5,
        LevelText6,
        
        // ��ų ����
        SkillLevelText1,
        SkillLevelText2,
        SkillLevelText3,
        SkillLevelText4,
        SkillLevelText5,
        SkillLevelText6,
    }

    enum EImages
    {
        ClassImage,
        
        // ���� ����ȭ�� ����
        RelicBGImage1,
        RelicBGImage2,
        RelicBGImage3,
        RelicBGImage4,
        RelicBGImage5,
        RelicBGImage6,
        
        // ���� �̹��� ����
        RelicImage1,
        RelicImage2,
        RelicImage3,
        RelicImage4,
        RelicImage5,
        RelicImage6,
        
        // ��ų ����
        Image1,
        Image2,
        Image3,
        Image4,
        Image5,
        Image6,
    }

    #endregion
    
    // ��ü ����
    private MyRankingInfo _myRankingInfo;

    private RankingInfo _rankingInfo;

    private int _rank;
    
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
        BindImage(typeof(EImages));

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

    public void SetRankingInfo(RankingInfo rankingInfo, int rank)
    {
        _rankingInfo = rankingInfo;
        _recordId = rankingInfo.recordId;
        _rank = rank;
        Init();
        UIRefresh();
    }

    void UIRefresh()
    {
        StartCoroutine(GetDetailRankingInfo(_recordId, () =>
        {
            if (_myRankingInfo != null)
            {
                GetText((int)ETexts.TitleText).text = $"{_myRankingInfo.rank}�� ���";
                GetText((int)ETexts.PlayerName).text = _myRankingInfo.nickname;
                GetText((int)ETexts.DifficultyText).text = $"{_myRankingInfo.difficulty}";
                
                GetImage((int)EImages.ClassImage).sprite =
                    Managers.Resource.Load<Sprite>(Managers.Data.PlayerDic[_myRankingInfo.classNo].ThumbnailName);
            }
            else
            {
                GetText((int)ETexts.TitleText).text = $"{_rank}�� ���";
                GetText((int)ETexts.PlayerName).text = _rankingInfo.nickname;
                GetText((int)ETexts.DifficultyText).text = $"{_rankingInfo.difficulty}";
                GetImage((int)EImages.ClassImage).sprite =
                    Managers.Resource.Load<Sprite>(Managers.Data.PlayerDic[_rankingInfo.classNo].ThumbnailName);
            }

            GetText((int)ETexts.RecordDate).text = _detailRankingDataRes.data.updatedDate;
            GetText((int)ETexts.EliteKillText).text = $"{_detailRankingDataRes.data.eliteKill}";
            GetText((int)ETexts.KillText).text = $"{_detailRankingDataRes.data.normalKill}";
            
            
            // ���� ���� ����
            foreach (RelicDetail relicDetail in _detailRankingDataRes.data.relicList)
            {
                switch (relicDetail.slot)
                {
                    case 1:
                        GetText((int)ETexts.LevelText1).text = $"{relicDetail.level}";
                        GetImage((int)EImages.RelicImage1).sprite = Managers.Resource.Load<Sprite>(Managers.Data.RelicDic[relicDetail.relicNo * 10 + relicDetail.level].ThumbnailName);
                        GetImage((int)EImages.RelicBGImage1).color = GetRelicColorByRarity(Managers.Data.RelicDic[relicDetail.relicNo * 10 + relicDetail.level].Rarity);
                        break;
                    case 2:
                        GetText((int)ETexts.LevelText2).text = $"{relicDetail.level}";
                        GetImage((int)EImages.RelicImage2).sprite = Managers.Resource.Load<Sprite>(Managers.Data.RelicDic[relicDetail.relicNo * 10 + relicDetail.level].ThumbnailName);
                        GetImage((int)EImages.RelicBGImage2).color = GetRelicColorByRarity(Managers.Data.RelicDic[relicDetail.relicNo * 10 + relicDetail.level].Rarity);
                        break;
                    case 3:
                        GetText((int)ETexts.LevelText3).text = $"{relicDetail.level}";
                        GetImage((int)EImages.RelicImage3).sprite = Managers.Resource.Load<Sprite>(Managers.Data.RelicDic[relicDetail.relicNo * 10 + relicDetail.level].ThumbnailName);
                        GetImage((int)EImages.RelicBGImage3).color = GetRelicColorByRarity(Managers.Data.RelicDic[relicDetail.relicNo * 10 + relicDetail.level].Rarity);
                        break;
                    case 4:
                        GetText((int)ETexts.LevelText4).text = $"{relicDetail.level}";
                        GetImage((int)EImages.RelicImage4).sprite = Managers.Resource.Load<Sprite>(Managers.Data.RelicDic[relicDetail.relicNo * 10 + relicDetail.level].ThumbnailName);
                        GetImage((int)EImages.RelicBGImage4).color = GetRelicColorByRarity(Managers.Data.RelicDic[relicDetail.relicNo * 10 + relicDetail.level].Rarity);
                        break;
                    case 5:
                        GetText((int)ETexts.LevelText5).text = $"{relicDetail.level}";
                        GetImage((int)EImages.RelicImage5).sprite = Managers.Resource.Load<Sprite>(Managers.Data.RelicDic[relicDetail.relicNo * 10 + relicDetail.level].ThumbnailName);
                        GetImage((int)EImages.RelicBGImage5).color = GetRelicColorByRarity(Managers.Data.RelicDic[relicDetail.relicNo * 10 + relicDetail.level].Rarity);
                        break;
                    case 6:
                        GetText((int)ETexts.LevelText6).text = $"{relicDetail.level}";
                        GetImage((int)EImages.RelicImage6).sprite = Managers.Resource.Load<Sprite>(Managers.Data.RelicDic[relicDetail.relicNo * 10 + relicDetail.level].ThumbnailName);
                        GetImage((int)EImages.RelicBGImage6).color = GetRelicColorByRarity(Managers.Data.RelicDic[relicDetail.relicNo * 10 + relicDetail.level].Rarity);
                        break;
                    
                    default:
                        break;
                }
            }
            
            // ��ų ���� ����
            foreach (SkillDetail skillDetail in _detailRankingDataRes.data.skillList)
            {
                switch (skillDetail.slot)
                {
                    case 1:
                        GetText((int)ETexts.SkillLevelText1).text = $"Lv.{skillDetail.level}";
                        GetImage((int)EImages.Image1).sprite = Managers.Resource.Load<Sprite>(Managers.Data.SkillDic[skillDetail.skillNo].IconName);
                        break;
                    case 2:
                        GetText((int)ETexts.SkillLevelText2).text = $"Lv.{skillDetail.level}";
                        GetImage((int)EImages.Image2).sprite = Managers.Resource.Load<Sprite>(Managers.Data.SkillDic[skillDetail.skillNo].IconName);
                        break;
                    case 3:
                        GetText((int)ETexts.SkillLevelText3).text = $"Lv.{skillDetail.level}";
                        GetImage((int)EImages.Image3).sprite = Managers.Resource.Load<Sprite>(Managers.Data.SkillDic[skillDetail.skillNo].IconName);
                        break;
                    case 4:
                        GetText((int)ETexts.SkillLevelText4).text = $"Lv.{skillDetail.level}";
                        GetImage((int)EImages.Image4).sprite = Managers.Resource.Load<Sprite>(Managers.Data.SkillDic[skillDetail.skillNo].IconName);
                        break;
                    case 5:
                        GetText((int)ETexts.SkillLevelText5).text = $"Lv.{skillDetail.level}";
                        GetImage((int)EImages.Image5).sprite = Managers.Resource.Load<Sprite>(Managers.Data.SkillDic[skillDetail.skillNo].IconName);
                        break;
                    case 6:
                        GetText((int)ETexts.SkillLevelText6).text = $"Lv.{skillDetail.level}";
                        GetImage((int)EImages.Image6).sprite = Managers.Resource.Load<Sprite>(Managers.Data.SkillDic[skillDetail.skillNo].IconName);
                        break;
                    
                    default:
                        break;
                }
            }
            
        }));
    }

    Color GetRelicColorByRarity(int rarity)
    {
        switch (rarity)
        {
            case 0: return RelicUIColors.GradeC;
            case 1: return RelicUIColors.GradeB;
            case 2: return RelicUIColors.GradeA;
            case 3: return RelicUIColors.GradeS;
            case 4: return RelicUIColors.GradeSSS;
            default: return Color.white; // �⺻ ���� ��ȯ
        }
    }
    
    private bool _isRequestingDetail = false;  // ���� �� ��ŷ ������ ��û ������ Ȯ���ϴ� �÷���

    // ���� ������ �����̾���!!!!
    IEnumerator currentDetailRequest;
    IEnumerator GetDetailRankingInfo(int recordId, Action onCompleted)
    {
        if (_isRequestingDetail) {
            Debug.LogWarning("Detail ranking request is already in progress.");
            // ���� ��û ���̸� �ߴ�
            if (currentDetailRequest != null)
            {
                StopCoroutine(currentDetailRequest);
            }
        }

        _isRequestingDetail = true;  // ��û ���� �÷��� ����

        bool isDone = false;
        currentDetailRequest = JWTGetRequest($"rankings/{recordId}", res =>
        {
            _detailRankingDataRes = JsonUtility.FromJson<DetailRankingDataRes>(res);
            isDone = true;
        });
        StartCoroutine(currentDetailRequest);

        // ��û�� �Ϸ�� ������ ���
        yield return new WaitUntil(() => isDone);

        // �Ϸ� �ݹ� ȣ��
        onCompleted?.Invoke();

        _isRequestingDetail = false;  // ��û �Ϸ� �÷��� ����
    }
    
    // ����
    void Refresh()
    {
        
    }

    void OnClickCloseButton()
    {
        Debug.Log("CloseRankingDetail");
        // ���� �˾��� ��Ȱ��ȭ
        gameObject.SetActive(false);
    }
}
