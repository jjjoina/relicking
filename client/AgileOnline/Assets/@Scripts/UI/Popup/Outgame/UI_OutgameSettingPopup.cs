using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Util;

[Serializable]
public class LogoutDataRes
{
    public int status;
    public string message;
    public bool data;
}


public class UI_OutgameSettingPopup : UI_Popup
{
    enum Texts
    {
        BGMONText,
        SFXONText,
        BGMOFFText,
        SFXOFFText,
    }

    enum Buttons
    {
        CloseButton,
        LogoutButton,
        ChangeNicknameButton,
        ChangePasswordButton,
        
    }

    enum Toggles
    {
        BGMSoundToggle,
        SFXSoundToggle,
    }
    
    // ��ü ���� �δ� ��
    private bool _isSelectedBGMSound = true;
    private bool _isSelectedSFXSound = true;
    
    
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));
        BindToggle(typeof(Toggles));
        BindText(typeof(Texts));

        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(ClosePopupUI);
        
        GetText((int)Texts.BGMOFFText).gameObject.SetActive(false);
        GetText((int)Texts.SFXOFFText).gameObject.SetActive(false);

        // ��� ��ư
        GetToggle((int)Toggles.BGMSoundToggle).gameObject.BindEvent(OnClickBGMSoundToggle);
        GetToggle((int)Toggles.SFXSoundToggle).gameObject.BindEvent(OnClickSFXSoundToggle);
        
        // �α׾ƿ� ��ư
        GetButton((int)Buttons.LogoutButton).gameObject.BindEvent(OnClickLogoutButton);
        
        // �г��� ���� ��ư
        GetButton((int)Buttons.ChangeNicknameButton).gameObject.BindEvent(OnClickChangeNicknameButton);
        
        // ��й�ȣ ���� ��ư
        GetButton((int)Buttons.ChangePasswordButton).gameObject.BindEvent(OnClickChangePasswordButton);
        
        
        return true;
    }

    void OnClickBGMSoundToggle()
    {
        Managers.Sound.PlayButtonClick();
        _isSelectedBGMSound = !_isSelectedBGMSound;

        if (_isSelectedBGMSound)
        {
            Managers.Game.BGMOn = true;
            GetText((int)Texts.BGMOFFText).gameObject.SetActive(false);
            GetText((int)Texts.BGMONText).gameObject.SetActive(true);
            
        }

        else
        {
            Managers.Game.BGMOn = false;
            GetText((int)Texts.BGMOFFText).gameObject.SetActive(true);
            GetText((int)Texts.BGMONText).gameObject.SetActive(false);
        }
    }

    void OnClickSFXSoundToggle()
    {
        Managers.Sound.PlayButtonClick();
        _isSelectedSFXSound = !_isSelectedSFXSound;

        if (_isSelectedSFXSound)
        {
            Managers.Game.EffectSoundOn = true;
            GetText((int)Texts.SFXOFFText).gameObject.SetActive(false);
            GetText((int)Texts.SFXONText).gameObject.SetActive(true);
        }

        else
        {
            Managers.Game.EffectSoundOn = false;
            GetText((int)Texts.SFXOFFText).gameObject.SetActive(true);
            GetText((int)Texts.SFXONText).gameObject.SetActive(false);
        }
    }
    
    
    void OnClickLogoutButton()
    {
        Managers.Sound.PlayButtonClick();
        StartCoroutine(JWTDeleteRequest("members/logout", res =>
        {
            // json -> ��ü�� ��ȯ
            LogoutDataRes logoutDataRes = JsonUtility.FromJson<LogoutDataRes>(res);

            // �α׾ƿ� ��û �����ÿ� �� ó������ ������ ��ū �����
            if (logoutDataRes.data == true)
            {
                SceneManager.LoadScene("NewTitleScene");
                Managers.Game.AccessToken = "";
                Managers.Game.RefreshToken = "";
            }
        }));
    }
    
    void OnClickChangeNicknameButton()
    {
        Managers.Sound.PlayButtonClick();
        UI_ChangePopup popup = Managers.UI.ShowPopupUI<UI_ChangePopup>();
        popup.SetInfo(true);
    }
    
    void OnClickChangePasswordButton()
    {
        Managers.Sound.PlayButtonClick();
        UI_ChangePopup popup = Managers.UI.ShowPopupUI<UI_ChangePopup>();
        popup.SetInfo(false);
    }
}
