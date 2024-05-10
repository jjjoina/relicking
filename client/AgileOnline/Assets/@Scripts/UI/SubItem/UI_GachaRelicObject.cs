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
        #endregion
        
        
        return true;
    }

    // UI_GachaResultPopup���� ȣ���� ����.
    public void SetInfo(GachaRelic relic)
    {
        
        // z ��ġ 0���� �ٲٱ�
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector3 localPosition = rectTransform.localPosition;
        localPosition.z = 0;
        rectTransform.localPosition = localPosition;
        
        // ������ ����
        transform.localScale = Vector3.one;
        
        // ���� ǥ�� -> ������ ID�� ���� ������ ���ϳ�??
        GetText((int)ETexts.RelicLevelText).text = "LV. " + relic.level.ToString();
        
        #region ���Ƽ ǥ��

        // ���Ƽ ǥ�� (BG ���� �ٲٱ�) -> ID�� ���ؼ� �м�
        // 0 : C��, 1 : B��, 2 : A��, 3 : S��, 4 : SSS�� (�ӽ�, ���߿� ENUM���� �ٲٰų� �� ��!)
        switch (Managers.Data.RelicDic[relic.relicNo * 10 + relic.level].Rarity)
        {
            case 0:
                GetImage((int)EImages.RelicBGImage).sprite = Managers.Resource.Load<Sprite>("RelicFrame_C.sprite");
                break;
            case 1:
                GetImage((int)EImages.RelicBGImage).sprite = Managers.Resource.Load<Sprite>("RelicFrame_B.sprite");
                break;
            case 2:
                GetImage((int)EImages.RelicBGImage).sprite = Managers.Resource.Load<Sprite>("RelicFrame_A.sprite");
                break;
            case 3:
                GetImage((int)EImages.RelicBGImage).sprite = Managers.Resource.Load<Sprite>("RelicFrame_S.sprite");
                break;
            case 4:
                GetImage((int)EImages.RelicBGImage).sprite = Managers.Resource.Load<Sprite>("RelicFrame_SSS.sprite");
                break;
            
            default:
                break;
        }

        #endregion
        
        // ���� �̹��� ��������
        GetImage((int)EImages.RelicImage).sprite = Managers.Resource.Load<Sprite>(Managers.Data.RelicDic[relic.relicNo * 10 + relic.level].ThumbnailName);

        #region �űԿ� ������ ����

        // �ű� + ������
        if (relic.newYn && relic.levelUpYn)
        {
            GetText((int)ETexts.RelicStateText).text = "NEW! EXP UP!";
            GetText((int)ETexts.RelicStateText).color = HexToColor("FFA500");
        }
        // �űԸ�
        else if (relic.newYn)
        {
            GetText((int)ETexts.RelicStateText).text = "NEW!";
            GetText((int)ETexts.RelicStateText).color = Color.red;
        }
        
        // ��������
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

