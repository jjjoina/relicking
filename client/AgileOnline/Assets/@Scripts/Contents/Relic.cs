using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Relic
{
    public int key = 0; // ��Ű���� string
    public Data.RelicData RelicData;
    bool _isEquipped = false;

    public bool IsEquipped
    {
        get
        {
            // ������� ���������� Ȯ��
            return _isEquipped;
        }
        set
        {
            _isEquipped = value;
        }
    }

    
    
    // �ΰ��� ����

    // ������!!!!
    public Relic(int key)
    {
        this.key = key;

        // � Relic�̴�?
        RelicData = Managers.Data.RelicDic[key];

    }
    
    // �ΰ��� ��
    
    
}
