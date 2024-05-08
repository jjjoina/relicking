using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Util;
using Data;
public class UI_GachaRelicObject : UI_Base
{
    #region Enum

    enum EGameObjects
    {
        
    }
    
    enum EButtons
    {
        
    }
    
    enum ETexts
    {
        RelicLevelText,
        RelicStateText,
    }
    
    enum EToggles
    {
        
    }
    
    enum EImages
    {
        RelicImage,
        RelicBGImage,
    }
    
    #endregion
    
    
    private void Awake()
    {
        Init();
    }

    // 초기 세팅
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
        #endregion
        
        
        return true;
    }

    // UI_GachaResultPopup에서 호출할 것임.
    public void SetInfo(GachaRelic relic)
    {
        // 레벨 표시 -> 레벨은 ID로 따로 관리를 안하네??
        GetText((int)ETexts.RelicLevelText).text = "LV. " + relic.level.ToString();
        
        #region 레어리티 표시

        // 레어리티 표시 (BG 색깔 바꾸기) -> ID를 통해서 분석
        // 0 : C급, 1 : B급, 2 : A급, 3 : S급, 4 : SSS급 (임시, 나중에 ENUM으로 바꾸거나 할 것!)
        switch (Managers.Data.RelicDic[relic.relicNo].Rarity)
        {
            case 0:
                GetImage((int)EImages.RelicBGImage).color = RelicUIColors.GradeC;
                break;
            case 1:
                GetImage((int)EImages.RelicBGImage).color = RelicUIColors.GradeB;
                break;
            case 2:
                GetImage((int)EImages.RelicBGImage).color = RelicUIColors.GradeA;
                break;
            case 3:
                GetImage((int)EImages.RelicBGImage).color = RelicUIColors.GradeS;
                break;
            case 4:
                GetImage((int)EImages.RelicBGImage).color = RelicUIColors.GradeSSS;
                break;
            
            default:
                break;
        }

        #endregion
        
        // 유물 이미지 가져오기
        GetImage((int)EImages.RelicImage).sprite = Managers.Resource.Load<Sprite>(Managers.Data.RelicDic[relic.relicNo].ThumbnailName);

        #region 신규와 레벨업 여부

        // 신규 + 레벨업
        if (relic.newYn && relic.levelUpYn)
        {
            GetText((int)ETexts.RelicStateText).text = "NEW! EXP UP!";
            GetText((int)ETexts.RelicStateText).color = HexToColor("FFA500");
        }
        // 신규만
        else if (relic.newYn)
        {
            GetText((int)ETexts.RelicStateText).text = "NEW!";
            GetText((int)ETexts.RelicStateText).color = Color.red;
        }
        
        // 레벨업만
        else if (relic.levelUpYn)
        {
            GetText((int)ETexts.RelicStateText).text = "EXP UP!";
            GetText((int)ETexts.RelicStateText).color = Color.blue;
        }
        
        else
        {
            GetText((int)ETexts.RelicStateText).text = "";
        }
        
        #endregion
        
    }
    
    void Refresh()
    {
        
    }

    
    
}

