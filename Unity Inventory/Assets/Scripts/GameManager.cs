using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } //�̱���
    public Character Player { get; private set; } //�÷��̾� ������

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        Player = new Character(maxHP: 100, currentHP: 100, attack: 45, defense: 30, critical: 35);
        PushToUI();
    }

    void PushToUI()
    {
        UIManager.Instance.StatusMenu.SetCharacter(Player);
    }
}
