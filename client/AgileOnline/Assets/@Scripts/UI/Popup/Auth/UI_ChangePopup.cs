using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Util;

[Serializable]
public class ChangePasswordDataReq
{
    public string oldPassword;
    public string newPassword;
    public string newPasswordRe;
}

[Serializable]
public class ChangePasswordDataRes
{
    public int status;
    public string message;
    public bool data;
}


public class UI_ChangePopup : UI_Popup
{
    #region UI ��� ����Ʈ

    // ���� ����

    #endregion
    
    #region Enum

    enum EGameObjects
    {
        ChangeNicknameInput,
        OldPasswordInput,
        NewPasswordInput,
        CheckNewPasswordInput,
    }
    
    enum EButtons
    {
        ConfirmButton,
        BackButton,
    }
    
    enum ETexts
    {
        ChangeNicknameGuideText,
        ChangeNicknameResultText,
        ChangeNicknameText,
        
        NewPasswordGuideText,
        OldPasswordResultText,
        NewPasswordResultText,
        CheckNewPasswordResultText,
        OldPasswordText,
        NewPasswordText,
        CheckNewPasswordText,
        
    }
    
    enum EToggles
    {
        
    }
    
    enum EImages
    {
        
    }
    
    enum EInputFields
    {
        ChangeNicknameInputField,
        OldPasswordInputField,
        NewPasswordInputField,
        CheckNewPasswordInputField,
    }
    
    #endregion
    
    
    
    // ��ü ���� �δ� ��
    private bool isDuplicateNickname = false;
    private GameObject _logoImage;
    private bool _isChangeNickname = false;
    private bool _isValidatePassword = false;
    
    
    private void Awake()
    {
        Init();
    }

    // �ʱ� ����
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Debug.Log("UI_NicknamePopup");
        
        #region Object Bind
        BindObject(typeof(EGameObjects));
        BindButton(typeof(EButtons));
        BindText(typeof(ETexts));
        BindToggle(typeof(EToggles));
        BindImage(typeof(EImages)); 
        BindInputField(typeof(EInputFields));


        // ������ ������ false
        
        // �̸��� �ߺ� üũ ��� �ؽ�Ʈ
        GetText((int)ETexts.ChangeNicknameResultText).gameObject.SetActive(false);
        
        // ��й�ȣ ��ȿ�� ��� �ؽ�Ʈ
        GetText((int)ETexts.OldPasswordResultText).gameObject.SetActive(false);
        GetText((int)ETexts.NewPasswordResultText).gameObject.SetActive(false);
        GetText((int)ETexts.CheckNewPasswordResultText).gameObject.SetActive(false);
        
        // Ȯ�� ��ư
        GetButton((int)EButtons.ConfirmButton).gameObject.BindEvent(OnClickConfirmButton);
        
        // �ڷ� ���� ��ư
        GetButton((int)EButtons.BackButton).gameObject.BindEvent(OnClickBackButton);
        
        
        
        // �ű� ��й�ȣ �� ��й�ȣ Ȯ�� �ʵ��� �� ��ȭ�� ���� �̺�Ʈ �ֱ�
        GetInputField((int)EInputFields.OldPasswordInputField).onValueChanged.AddListener(OnChangeOldPassword);
        GetInputField((int)EInputFields.NewPasswordInputField).onValueChanged.AddListener(OnChangeNewPassword);
        GetInputField((int)EInputFields.CheckNewPasswordInputField).onValueChanged
            .AddListener(OnChangeCheckNewPassword);
        
        
        #endregion

        Refresh();
        
        return true;
    }

    // ����
    void Refresh()
    {
        // Field �Է³���� �� ������
        GetInputField((int)EInputFields.ChangeNicknameInputField).text = "";
        GetInputField((int)EInputFields.OldPasswordInputField).text = "";
        GetInputField((int)EInputFields.NewPasswordInputField).text = "";
        GetInputField((int)EInputFields.CheckNewPasswordInputField).text = "";
    }

    public void SetInfo(bool isChangeNickname)
    {
        _isChangeNickname = isChangeNickname;
        
        #region �г��� or ��й�ȣ

        // �г��Ӱ� ��й�ȣ ���� ���ο� ���� UI ����
        
        if (_isChangeNickname)
        {
            // �г��� ���� �� Ŵ
            GetText((int)ETexts.ChangeNicknameGuideText).gameObject.SetActive(true);
            
            GetText((int)ETexts.ChangeNicknameText).gameObject.SetActive(true);
            GetObject((int)EGameObjects.ChangeNicknameInput).gameObject.SetActive(true);
            
            // ��й�ȣ ���� �� ��
            GetText((int)ETexts.NewPasswordGuideText).gameObject.SetActive(false);
            
            GetText((int)ETexts.OldPasswordText).gameObject.SetActive(false);
            GetObject((int)EGameObjects.OldPasswordInput).gameObject.SetActive(false);
            
            GetText((int)ETexts.NewPasswordText).gameObject.SetActive(false);
            GetObject((int)EGameObjects.NewPasswordInput).gameObject.SetActive(false);
            
            
            GetText((int)ETexts.CheckNewPasswordText).gameObject.SetActive(false);
            GetObject((int)EGameObjects.CheckNewPasswordInput).gameObject.SetActive(false);
            
        }
        else
        {
            //��й�ȣ ���� �� Ŵ
            GetText((int)ETexts.NewPasswordGuideText).gameObject.SetActive(true);
            
            GetText((int)ETexts.OldPasswordText).gameObject.SetActive(true);
            GetObject((int)EGameObjects.OldPasswordInput).gameObject.SetActive(true);

            GetText((int)ETexts.NewPasswordText).gameObject.SetActive(true);
            GetObject((int)EGameObjects.NewPasswordInput).gameObject.SetActive(true);

            
            GetText((int)ETexts.CheckNewPasswordText).gameObject.SetActive(true);
            GetObject((int)EGameObjects.CheckNewPasswordInput).gameObject.SetActive(true);

            
            // �г��� ���� �� ��
            GetText((int)ETexts.ChangeNicknameGuideText).gameObject.SetActive(false);
            
            GetText((int)ETexts.ChangeNicknameText).gameObject.SetActive(false);
            GetObject((int)EGameObjects.ChangeNicknameInput).gameObject.SetActive(false);


        }
        

        #endregion
        
        
    }
    
    
    void OnClickConfirmButton()
    {
        // �г��� ���� ����
        if (_isChangeNickname)
        {
            CheckNickname(() => {
                // �г��� �ߺ� �˻� �� ����ϸ� �г��� ���� ��û
                if (!isDuplicateNickname)
                {
                    ChangeNickname();
                }
            });
        }

        // ��й�ȣ ���� ����
        else
        {
            ChangePassword();
        }
        
    }

    void OnClickBackButton()
    {
        Managers.UI.ClosePopupUI(this);
    }
    

    #region �г��� ���� ����

    void CheckNickname(Action onCompleted)
    {
        StartCoroutine(GetRequest(
            $"members/duplicate-nickname?nickname={GetInputField((int)EInputFields.ChangeNicknameInputField).text}",
            res =>
            {
                NicknameDataRes checkNickname = JsonUtility.FromJson<NicknameDataRes>(res);
                isDuplicateNickname = !checkNickname.data;
                if (isDuplicateNickname)
                {
                    Debug.Log("��� �Ұ����� �г���");
                    GetText((int)ETexts.ChangeNicknameResultText).gameObject.SetActive(true);
                }
                else
                {
                    Debug.Log("��� ������ �г���");
                    GetText((int)ETexts.ChangeNicknameResultText).gameObject.SetActive(false);
                }
                onCompleted?.Invoke(); // �ݹ� �Լ� ȣ��
            }));
    }

    void ChangeNickname()
    {
        // �ٲ� �г��� ��ü �����
        NicknameDataReq nicknameDataReq = new NicknameDataReq
        {
            nickname = GetInputField((int)EInputFields.ChangeNicknameInputField).text
        };
        
        // ��ü -> Json ��ȯ
        string nicknameJsonData = JsonUtility.ToJson(nicknameDataReq);
        
        // ������ ���� ��û ������
        StartCoroutine(JWTPatchRequest("members/nickname", nicknameJsonData, res =>
        {
            NicknameDataRes nicknameDataRes = JsonUtility.FromJson<NicknameDataRes>(res);

            if (nicknameDataRes != null && nicknameDataRes.status == 200)
            {
                Managers.UI.ClosePopupUI(this);
            }
        }));

    }

    #endregion


    #region ��й�ȣ ���� ����

    void ChangePassword()
    {
        // �ٲ� ��й�ȣ ��ü �����
        ChangePasswordDataReq changePasswordDataReq = new ChangePasswordDataReq
        {
            oldPassword = GetInputField((int)EInputFields.OldPasswordInputField).text,
            newPassword = GetInputField((int)EInputFields.NewPasswordInputField).text,
            newPasswordRe = GetInputField((int)EInputFields.CheckNewPasswordInputField).text,
        };
        
        // ��ü -> Json ��ȯ
        string changePasswordJsonData = JsonUtility.ToJson(changePasswordDataReq);
        
        // ������ ��û ������
        StartCoroutine(JWTPatchRequest("members/password", changePasswordJsonData, res =>
        {
            ChangePasswordDataRes changePasswordDataRes = JsonUtility.FromJson<ChangePasswordDataRes>(res);
            
            if (changePasswordDataRes.status == 200)
            {
                Managers.UI.ClosePopupUI(this);
            }
        }));
    }
    

    #endregion

    void OnChangeOldPassword(string value)
    {
        GetText((int)ETexts.OldPasswordResultText).gameObject.SetActive(false);
    }
    
    void OnChangeNewPassword(string value)
    {
        
        _isValidatePassword = ValidatePassword(value);

        // ��й�ȣ ��ȿ�ϴٸ�
        if (_isValidatePassword)
        {
            GetText((int)ETexts.NewPasswordResultText).gameObject.SetActive(false);
        }
        
        else
        {
            GetText((int)ETexts.NewPasswordResultText).gameObject.SetActive(true);
        }
        
        RefreshPasswordMatch();
    }

    void OnChangeCheckNewPassword(string value)
    {
        RefreshPasswordMatch();
    }

    void RefreshPasswordMatch()
    {
        bool passwordsMatch = ValidatePasswordsMatch(
            GetInputField((int)EInputFields.NewPasswordInputField).text,
            GetInputField((int)EInputFields.CheckNewPasswordInputField).text
        );

        // ��ġ ���ϸ� ���޽��� Ȱ��ȭ
        GetText((int)ETexts.CheckNewPasswordResultText).gameObject.SetActive(!passwordsMatch);
    }

    
    // ��й�ȣ ��ȿ�� �˻�
    bool ValidatePassword(string password)
    {
        if (password.Length < 8 || password.Length > 16)
        {
            return false; // ���� üũ
        }

        bool hasLetter = false; // ������ ���� ����
        bool hasDigit = false;  // ���� ���� ����
        bool hasSpecialChar = false; // Ư������ ���� ����

        foreach (char c in password)
        {
            if (char.IsLetter(c)) hasLetter = true;
            else if (char.IsDigit(c)) hasDigit = true;
            else if (!char.IsWhiteSpace(c) && !char.IsControl(c)) hasSpecialChar = true;
        
            // ��� ������ �����ϸ� �� �̻� �˻��� �ʿ䰡 ����
            if (hasLetter && hasDigit && hasSpecialChar)
            {
                return true;
            }
        }

        // �� ���� �� �ϳ��� �������� ������ false ��ȯ
        return hasLetter && hasDigit && hasSpecialChar;
    }
    
    
    // ��й�ȣ ��ġ �˻�
    bool ValidatePasswordsMatch(string password, string confirmPassword)
    {
        return password == confirmPassword;
    }
    
}
