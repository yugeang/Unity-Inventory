using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public enum EquipmentSlot { Hat, Weapon, Shield }

public class ItemSlot : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Hit & Highlights")]
    [SerializeField] Image hitArea; // Ŭ�� ���� Image
    [SerializeField] GameObject[] highlights;

    [Header("Item Info")]
    public EquipmentSlot equipSlot; // Hat / Weapon / Shield
    public StatType statType;
    public int amount = 10;
    public GameObject equippedPrefab; // ���� �̹���

    [Header("Player Preview Roots (�� ���Կ� ���� ����)")]
    [SerializeField] RectTransform hatRoot;
    [SerializeField] RectTransform weaponRoot;
    [SerializeField] RectTransform shieldRoot;

    public bool Equipped { get; private set; }
    bool isHover;

  
    static readonly List<ItemSlot> allSlots = new();
    static readonly Dictionary<EquipmentSlot, ItemSlot> equippedSlot = new();
    static readonly Dictionary<EquipmentSlot, GameObject> equippedInstance = new();

    //�ӽ÷� ��� ������
    GameObject hoverInstance;

    void Awake()
    {
        if (!hitArea) hitArea = GetComponent<Image>();
    }
    void OnEnable() { if (!allSlots.Contains(this)) allSlots.Add(this); UpdateVisual(); }
    void OnDisable() { allSlots.Remove(this); DestroyHover(); }

    //���콺 ����
    public void OnPointerEnter(PointerEventData e) { isHover = true; UpdateVisual(); ShowTempPreview(true); }
    public void OnPointerExit(PointerEventData e) { isHover = false; UpdateVisual(); ShowTempPreview(false); }

    //����
    public void OnPointerClick(PointerEventData e)
    {
        if (e.button != PointerEventData.InputButton.Left) return;

        var player = GameManager.Instance.Player;
        if (player == null || equippedPrefab == null) return;

        if (!Equipped)
        {
            // ���� ������ �ٸ� �� ���� ���̸� ����
            if (equippedSlot.TryGetValue(equipSlot, out var other) && other && other != this)
                other.Unequip(player);

            Equip(player);
        }
        else
        {
            Unequip(player);
        }
    }

 
    void Equip(Character player)
    {
        player.AddBonus(statType, amount);
        Equipped = true;
        equippedSlot[equipSlot] = this;

        // �ߺ� ���� �ȵ�
        if (equippedInstance.TryGetValue(equipSlot, out var old) && old) Destroy(old);

     
        var root = GetRoot(equipSlot);
        var inst = Instantiate(equippedPrefab, root);
        FitToParent(inst.transform as RectTransform);
        equippedInstance[equipSlot] = inst;

        DestroyHover();
        UpdateVisual();
        EquipmentChanged?.Invoke();
    }

    void Unequip(Character player)
    {
        player.RemoveBonus(statType, amount);
        Equipped = false;

        if (equippedSlot.TryGetValue(equipSlot, out var cur) && cur == this)
            equippedSlot.Remove(equipSlot);

        if (equippedInstance.TryGetValue(equipSlot, out var inst) && inst)
        {
            Destroy(inst);
            equippedInstance.Remove(equipSlot);
        }

        UpdateVisual();
        EquipmentChanged?.Invoke();
    }

    void UpdateVisual()
    {
        bool show = Equipped || isHover;
        if (highlights != null)
            foreach (var go in highlights) if (go) go.SetActive(show);
    }

    void ShowTempPreview(bool show)
    {
        var root = GetRoot(equipSlot);
        if (!root || equippedPrefab == null) return;

        if (show && !Equipped)
        {
            if (!hoverInstance)
            {
                hoverInstance = Instantiate(equippedPrefab, root);
                FitToParent(hoverInstance.transform as RectTransform);
                hoverInstance.transform.SetAsLastSibling();
            }
        }
        else
        {
            DestroyHover();
        }
    }

    void DestroyHover()
    {
        if (hoverInstance)
        {
            Destroy(hoverInstance);
            hoverInstance = null;
        }
    }

    RectTransform GetRoot(EquipmentSlot slot)
    {
        switch (slot)
        {
            case EquipmentSlot.Hat: return hatRoot;
            case EquipmentSlot.Weapon: return weaponRoot;
            case EquipmentSlot.Shield: return shieldRoot;
            default: return null;
        }
    }

    static void FitToParent(RectTransform rt)
    {
        if (!rt) return;
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = Vector2.zero;
        rt.localScale = Vector3.one;
        rt.SetAsLastSibling();
    }

    public static event Action EquipmentChanged; // ����/���� �� �˸�

    public static GameObject GetEquippedPrefab(EquipmentSlot slot)
    {
        if (equippedSlot.TryGetValue(slot, out var s) && s != null)
            return s.equippedPrefab;
        return null;
    }

    public static bool IsEquipped(EquipmentSlot slot)
    {
        return equippedSlot.ContainsKey(slot);
    }

}
