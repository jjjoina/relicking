using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Data
{
    #region PlayerData
    
    [Serializable]
    public class PlayerData
    {
        public int PlayerId;
        public string PrefabName;
        public string Name;
        public int MaxHp;
        public int Atk;
        public float Speed;
        public float CritRate;
        public float CritDmgRate;
        public float CoolDown;
        public List<int> SkillList;
    }

    [Serializable]
    public class PlayerDataLoader : ILoader<int, PlayerData>
    {
        public List<PlayerData> players = new List<PlayerData>();

        public Dictionary<int, PlayerData> MakeDict()
        {
            Dictionary<int, PlayerData> dict = new Dictionary<int, PlayerData>();
            foreach (PlayerData player in players)
                dict.Add(player.PlayerId, player);

            return dict;
        }
    }
    
    #endregion
    
    #region MonsterData

    [Serializable]
    public class MonsterData
    {
        public int MonsterId;
        public string PrefabName;
        public string Name;
        public int MaxHp;
        public int Atk;
        public float Speed;
        public int DropGold;
        public float CritRate;
        public float CritDmgRate;
        public float CoolDown;
        public List<int> SkillList;
    }
    
    [Serializable]
    public class MonsterDataLoader : ILoader<int, MonsterData>
    {
        public List<MonsterData> monsters = new List<MonsterData>();

        public Dictionary<int, MonsterData> MakeDict()
        {
            Dictionary<int, MonsterData> dict = new Dictionary<int, MonsterData>();
            foreach (MonsterData monster in monsters)
                dict.Add(monster.MonsterId, monster);

            return dict;
        }
    }
    
    #endregion

    #region StageData

    [Serializable]
    public class StageData
    {
        public int StageId;
        public string PrefabName;
        public string Name;
        public List<int> NormalMonsterList;
        public List<int> EliteMonsterList;
        public List<int> BossMonsterList;
    }
    
    [Serializable]
    public class StageDataLoader : ILoader<int, StageData>
    {
        public List<StageData> stages = new List<StageData>();

        public Dictionary<int, StageData> MakeDict()
        {
            Dictionary<int, StageData> dict = new Dictionary<int, StageData>();
            foreach (StageData stage in stages)
                dict.Add(stage.StageId, stage);

            return dict;
        }
    }

    #endregion
}