using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public MonsterData monsterData;

    #region monsterStat
    private int monsterID;
    private string monsterName;
    private monsterElement monsterElement;
    private float monsterHealthPoint;
    private float monsterAttack;
    private float monsterDefense;
    private float monsterSpecialAttack;
    private float monsterSpecialDefense;
    private float monsterSpeed;
    #endregion

    private void Awake()
    {
        SetUp(monsterData);
    }
    void SetUp(MonsterData monsterData)
    {
        monsterID = monsterData.monsterID;
        monsterName = monsterData.monsterName;
        monsterElement = monsterData.monsterElement;
        monsterHealthPoint= monsterData.monsterHealthPoint;
        monsterAttack = monsterData.monsterAttack;
        monsterDefense = monsterData.monsterDefense;
        monsterSpecialAttack = monsterData.monsterSpecialAttack;
        monsterSpecialDefense = monsterData.monsterSpecialDefense;
        monsterSpeed = monsterData.monsterSpeed;
    }
}
