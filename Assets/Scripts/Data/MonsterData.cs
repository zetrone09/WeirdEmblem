using UnityEngine;

[CreateAssetMenu(fileName = "New Monster",menuName = "Monster")]
public class MonsterData : ScriptableObject
{
    public int monsterID;
    public string monsterName;
    public monsterElement monsterElement;
    public float monsterHealthPoint;
    public float monsterAttack;
    public float monsterDefense;
    public float monsterSpecialAttack;
    public float monsterSpecialDefense;
    public float monsterSpeed;
}
public enum monsterElement
{
    Mechanical,
    Alchemical,
    Biological,
    StreamBeast,
    Cosmic,
    Mythic,
}

