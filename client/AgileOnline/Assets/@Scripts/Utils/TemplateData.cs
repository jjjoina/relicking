using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TemplateData", menuName = "Custom/TemplateData", order = 1)]
public class TemplateData : ScriptableObject
{
    public int TempNodeNum = 0;
    
    
    public int StageId;
    public int Difficulty = 1;

    
    public List<InventoryRelicDataRes> OwnedRelics;
    
    
    public event Action<int> OnSelectedClassIdChanged;
    
    
    public event Action<int, int[]> OnPlayerStatusChagned;
    private int selectedClassId = 1;
    
    
    public int SelectedClassId
    {
        get => selectedClassId;
        set
        { 
            if (selectedClassId != value)
            {
                selectedClassId = value;
                OnSelectedClassIdChanged?.Invoke(selectedClassId);
                OnPlayerStatusChagned?.Invoke(selectedClassId, equipedRelicIds);
            }
        }
    }
    
    
    public int SelectedRelicId = 0;

    
    public event Action<int[]> OnEquipedRelicIdsChanged;
    private int[] equipedRelicIds = new int[6];
    
    
    public int[] EquipedRelicIds
    {
        get => equipedRelicIds;
        // set
        // {
        //     if (!AreArraysEqual(equipedRelicIds, value))
        //     {
        //         equipedRelicIds = value;
        //         // OnEquipedRelicIdsChanged?.Invoke(equipedRelicIds);
        //     }
        // }
    }
    
    
    private bool AreArraysEqual(int[] a, int[] b)
    {
        if (a.Length != b.Length)
        {
            return false;
        }

        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] != b[i])
            {
                return false;
            }
        }
        return true;
    }

    
    public void SetRelicAt(int index, int relicId)
    {
        if (index < 0 || index >= equipedRelicIds.Length)
        {
            throw new IndexOutOfRangeException($"Index {index} is out of range for equipedRelics array.");
        }

        // �̹� ������ ������ ������ �õ����� ���� ����ó�� �κ�
        // for (int i = 0; i < equipedRelicIds.Length; i++)
        // {
        //     if (equipedRelicIds[i] == relicId)
        //     {
        //         equipedRelicIds[i] = 0; 
        //         break;
        //     }
        // }

       
        // Debug.Log("Ȥ�� ���� �� ����?");
        
        equipedRelicIds[index] = relicId;
        OnPlayerStatusChagned?.Invoke(selectedClassId, equipedRelicIds);
        OnEquipedRelicIdsChanged?.Invoke(equipedRelicIds);
        
    }

}
