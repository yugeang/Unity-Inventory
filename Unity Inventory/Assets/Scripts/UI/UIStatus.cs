using TMPro;
using UnityEngine;

public class UIStatus : MonoBehaviour
{
    [Header("CurrentPower Texts")]
    [SerializeField] TMP_Text attackPower;
    [SerializeField] TMP_Text defensePower;
    [SerializeField] TMP_Text hpPower;
    [SerializeField] TMP_Text criticalPower;

    Character player;

    public void SetCharacter(Character c)
    {
        if (player != null) player.OnStatsChanged -= RefreshUI;
        player = c;
        player.OnStatsChanged += RefreshUI;
        RefreshUI();
    }

    private void OnDestroy()
    {
        if (player != null) player.OnStatsChanged -= RefreshUI;
    }

    void RefreshUI()
    {
        if (player == null) return;
        if (attackPower) attackPower.text = $"{player.Attack}";
        if (defensePower) defensePower.text = $"{player.Defense}";
        if (hpPower) hpPower.text = $"{player.MaxHP}";
        if (criticalPower) criticalPower.text = $"{player.Critical}";
    }    
}
