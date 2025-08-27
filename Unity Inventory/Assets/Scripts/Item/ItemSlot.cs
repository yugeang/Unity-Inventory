using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("UI")]
    [SerializeField] Image hitArea; // 클릭을 받을 Image 
    [SerializeField] GameObject select;

    [Header("Effect")]
    public StatType statType;
    public int amount = 10;

    bool equipped;   // 착용 여부
    bool isHover;    // 마우스 여부

    void Awake()
    {
        if (!hitArea) hitArea = GetComponent<Image>();
    }

    // 마우스 올림/내림
    public void OnPointerEnter(PointerEventData e) { isHover = true; UpdateVisual(); }
    public void OnPointerExit(PointerEventData e) { isHover = false; UpdateVisual(); }

    // 클릭으로 착용/해제
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
