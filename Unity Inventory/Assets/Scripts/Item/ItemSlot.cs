using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("UI")]
    [SerializeField] Image hitArea; // Ŭ���� ���� Image 
    [SerializeField] GameObject select;

    [Header("Effect")]
    public StatType statType;
    public int amount = 10;

    bool equipped;   // ���� ����
    bool isHover;    // ���콺 ����

    void Awake()
    {
        if (!hitArea) hitArea = GetComponent<Image>();
    }

    // ���콺 �ø�/����
    public void OnPointerEnter(PointerEventData e) { isHover = true; UpdateVisual(); }
    public void OnPointerExit(PointerEventData e) { isHover = false; UpdateVisual(); }

    // Ŭ������ ����/����
    public void OnPointerClick(PointerEventData e)
    {
        if (e.button != PointerEventData.InputButton.Left) return;

        var player = GameManager.Instance.Player;
        if (player == null) return;

        if (!equipped) { player.AddBonus(statType, amount); equipped = true; }
        else { player.RemoveBonus(statType, amount); equipped = false; }

        UpdateVisual();
    }

    void OnEnable() => UpdateVisual();

    void UpdateVisual()
    {
        if (select) select.SetActive(equipped || isHover);
    }
}
