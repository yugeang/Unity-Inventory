using System;

public enum StatType { Attack, Defense, Critical, MaxHP }

public class Character
{
    public int MaxHP => baseMaxHP + bonusMaxHP;
    public int CurrentHP { get; private set; }
    public int Attack => baseAttack + bonusAttack;
    public int Defense => baseDefense + bonusDefense;
    public int Critical => baseCritical + bonusCritical;

    public event Action OnStatsChanged; //���� �� �˷��ִ� �̺�Ʈ

    int baseMaxHP, baseAttack, baseDefense, baseCritical; //�⺻ ���� ����
    int bonusMaxHP, bonusAttack, bonusDefense, bonusCritical; //���������� �߰��Ǵ� ���ʽ� �հ�

    public Character(int maxHP, int currentHP, int attack, int defense, int critical)
    {
        baseMaxHP = maxHP;  
        CurrentHP = currentHP;  
        baseAttack = attack;
        baseDefense = defense;  
        baseCritical = critical;
    }

    public void AddBonus(StatType type, int amount) //���ʽ� ���ϱ�
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

    public void RemoveBonus(StatType type, int amount) //���ʽ� ����
    {
        AddBonus(type, -amount);
    }
}
