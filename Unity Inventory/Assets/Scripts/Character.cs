using System;

public enum StatType { Attack, Defense, Critical, MaxHP }

public class Character
{
    public int MaxHP => baseMaxHP + bonusMaxHP;
    public int CurrentHP { get; private set; }
    public int Attack => baseAttack + bonusAttack;
    public int Defense => baseDefense + bonusDefense;
    public int Critical => baseCritical + bonusCritical;

    public event Action OnStatsChanged; //변경 시 알려주는 이벤트

    int baseMaxHP, baseAttack, baseDefense, baseCritical; //기본 스탯 저장
    int bonusMaxHP, bonusAttack, bonusDefense, bonusCritical; //아이템으로 추가되는 보너스 합계

    public Character(int maxHP, int currentHP, int attack, int defense, int critical)
    {
        baseMaxHP = maxHP;  
        CurrentHP = currentHP;  
        baseAttack = attack;
        baseDefense = defense;  
        baseCritical = critical;
    }

    public void AddBonus(StatType type, int amount) //보너스 더하기
    {
        switch (type)
        {
            case StatType.MaxHP: bonusMaxHP += amount;
                break;
            case StatType.Attack: bonusAttack += amount; 
                break;
            case StatType.Defense: bonusDefense += amount;
                break;
            case StatType.Critical: bonusCritical += amount;
                break;
        }
        OnStatsChanged?.Invoke();
    }

    public void RemoveBonus(StatType type, int amount) //보너스 빼기
    {
        AddBonus(type, -amount);
    }
}
