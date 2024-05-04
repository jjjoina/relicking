using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InvenPopup : UI_Popup
{
    #region Enum

    enum EGameObjects
    {
        ContentObjet,
        ToggleGroup,
        StatToggleObject,
        RelicToggleObject,
        RelicListObject,
    }

    enum EButtons
    {
        ClassButton,
    }

    enum ETexts
    {
        MaxHealthText,
        DamageText,
        SpeedText,
        CoinBonusText,
        CriticalRateText,
        CriticalDamageText,
        CoolDownText,
    }

    enum EToggles
    {
        RelicToggle,
        StatToggle,
    }

    enum EImages
    {
        RelicToggleBGImage,
        StatToggleBGImage,
    }

    #endregion

    public TemplateData _templateData;

    bool _isSelectedEquip = false;
    bool _isSelectedStat = false;

    public void OnDestroy()
    {
        if (Managers.Game != null)
            Managers.Game.OnResourcesChanged -= Refresh;

        if (_templateData != null)
            _templateData.OnPlayerStatusChagned -= SetClassDetailStatus;
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

        GetToggle((int)EToggles.RelicToggle).gameObject.BindEvent(OnClickRelicToggle);
        GetToggle((int)EToggles.StatToggle).gameObject.BindEvent(OnClickStatToggle);
        GetButton((int)EButtons.ClassButton).gameObject.BindEvent(OnClickClassSelectButton);

        _templateData = Resources.Load<TemplateData>("GameTemplateData");
        SetClassDetailStatus(_templateData.SelectedClassId, _templateData.EquipedRelicIds);

        foreach (var RelicData in Managers.Data.RelicDic)
        {
            if (RelicData.Key == 0)
                continue;

            int RelicId = RelicData.Key;
            GameObject RelicObject = Managers.Resource.Instantiate("UI_RelicDetailObject", GetObject((int)EGameObjects.RelicListObject).transform);
            RelicObject.name = $"RelicObject{RelicId}";
            RelicObject.BindEvent(() => OnClickRelicInfoButton(RelicId));
        }

        #endregion

        ToggleInit();
        OnClickRelicToggle();

        Managers.Game.OnResourcesChanged += Refresh;
        _templateData.OnPlayerStatusChagned += SetClassDetailStatus;

        return true;
    }

    // ����
    void Refresh()
    {

    }

    void ToggleInit()
    {
        // ���ÿ��� �ʱ�ȭ
        _isSelectedEquip = false;
        _isSelectedStat = false;

        // �˾���ư �ʱ�ȭ
        GetObject((int)EGameObjects.RelicToggleObject).SetActive(false);
        GetObject((int)EGameObjects.StatToggleObject).SetActive(false);

        GetImage((int)EImages.RelicToggleBGImage).color = Util.HexToColor("B9A691");
        GetImage((int)EImages.StatToggleBGImage).color = Util.HexToColor("B9A691");
    }

    void OnClickRelicToggle()
    {
        ToggleInit();
        GetImage((int)EImages.RelicToggleBGImage).color = Util.HexToColor("B38C61");
        if (_isSelectedEquip == true)
            return;

        GetObject((int)EGameObjects.RelicToggleObject).SetActive(true);
        _isSelectedEquip = true;
    }

    void OnClickStatToggle()
    {
        ToggleInit();
        GetImage((int)EImages.StatToggleBGImage).color = Util.HexToColor("B38C61");
        if (_isSelectedStat == true)
            return;

        GetObject((int)EGameObjects.StatToggleObject).SetActive(true);
        _isSelectedStat= true;
    }

    void OnClickClassSelectButton()
    {
        Managers.UI.ShowPopupUI<UI_InvenClassSelectPopup>();
    }

    void OnClickRelicInfoButton(int num)
    {
        Debug.Log(num);
        _templateData.SelectedRelicId = num;
        Managers.UI.ShowPopupUI<UI_InvenRelicInfoPopup>();
    }

    void SetClassDetailStatus(int num, int[] nums)
    {
        int MaxHp = Managers.Data.PlayerDic[num].MaxHp;
        int Atk = Managers.Data.PlayerDic[num].Atk;
        float Speed = Managers.Data.PlayerDic[num].Speed;
        int CoinBonus = 100;
        float CritRate = Managers.Data.PlayerDic[num].CritRate;
        float CritDmgRate = Managers.Data.PlayerDic[num].CritDmgRate;
        float CoolDown = Managers.Data.PlayerDic[num].CoolDown;

        foreach (int i in nums)
        {
            MaxHp += Managers.Data.RelicDic[i].MaxHp;
            Atk += Managers.Data.RelicDic[i].Atk;
            Speed += Managers.Data.RelicDic[i].Speed;
            CoolDown -= Managers.Data.RelicDic[i].CoolTime / 100f ;
        }

        CoolDown = Mathf.Max(CoolDown, 0.4f);

        GetText((int)ETexts.MaxHealthText).text = MaxHp.ToString();
        GetText((int)ETexts.DamageText).text = Atk.ToString();
        GetText((int)ETexts.SpeedText).text = Speed.ToString();
        GetText((int)ETexts.CoinBonusText).text = CoinBonus.ToString();
        GetText((int)ETexts.CriticalRateText).text = CritRate.ToString();
        GetText((int)ETexts.CriticalDamageText).text = CritDmgRate.ToString();
        GetText((int)ETexts.CoolDownText).text = CoolDown.ToString();
    }
}
