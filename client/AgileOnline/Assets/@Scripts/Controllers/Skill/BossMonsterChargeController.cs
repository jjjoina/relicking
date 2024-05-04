using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

public class BossMonsterChargeController : SkillController
{
    private Vector3 _moveDir;
    
    public int SkillId { get; private set; }
    public int NextId { get; private set; }
    public string PrefabName { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string IconName { get; private set; }
    public float CoolTime { get; private set; }
    public int Damage { get; private set; }
    public float LifeTime { get; private set; } = 10;
    public float Speed { get; private set; }
    public int ProjectileNum { get; private set; }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        SkillType = Define.ESkillType.BossMonsterCharge;
        return true;
    }

    public void InitSkill(int templateId)
    {
        SkillData data = Managers.Data.SkillDic[templateId];
        
        SkillId = data.SkillId;
        NextId = data.NextId;
        PrefabName = data.PrefabName;
        Name = data.Name;
        Description = data.Description;
        IconName = data.IconName;
        CoolTime = data.CoolTime;
        Damage = data.Damage;
        LifeTime = data.LifeTime;
        Speed = data.Speed;
        ProjectileNum = data.ProjectileNum;
    }

    private void Update()
    {
        PlayerController player = Managers.Object.Player;
        if (player != null)
        {
            _moveDir = (player.transform.position - transform.position).normalized;
            transform.position += _moveDir * Speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = Managers.Object.Player;
        player.OnDamaged(this, Damage);
        Managers.Object.Despawn(this);
    }
}
