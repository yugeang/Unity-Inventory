using System.Collections.Generic;
using UnityEngine;

public class PlayerPreview : MonoBehaviour
{
    [Header("Player Preview Roots (각 슬롯에 해당 패널의 루트를 연결)")]
    [SerializeField] RectTransform hatRoot; // 모자 프리뷰
    [SerializeField] RectTransform weaponRoot; // 무기 프리뷰
    [SerializeField] RectTransform shieldRoot; // 방패 프리뷰

    readonly Dictionary<EquipmentSlot, GameObject> instances = new();

    void OnEnable()
    {
        // 장착/해제 이벤트
        ItemSlot.EquipmentChanged += RenderAll;
        RenderAll();
    }

    // 비활성화 시 이벤트
    void OnDisable()
    {
        ItemSlot.EquipmentChanged -= RenderAll;
        ClearAll();
    }

    // 세 슬롯을 전부 다시 그림
    void RenderAll()
    {
        RenderSlot(EquipmentSlot.Hat, hatRoot);
        RenderSlot(EquipmentSlot.Weapon, weaponRoot);
        RenderSlot(EquipmentSlot.Shield, shieldRoot);
    }

    // 특정 슬롯 하나만 다시 그림
    void RenderSlot(EquipmentSlot slot, RectTransform root)
    {
        if (instances.TryGetValue(slot, out var old) && old)
        {
            Destroy(old);
            instances.Remove(slot);
        }

        if (!root) return;

        // 현재 해당 슬롯에 장착된 프리팹 가져오기
        var prefab = ItemSlot.GetEquippedPrefab(slot);
        if (prefab == null) return; // 장착 안 됐으면 아무 것도 안 그림

        // 새 프리팹을 이 패널의 루트 밑에 인스턴스화
        var inst = Instantiate(prefab, root);
        FitToParent(inst.transform as RectTransform);
        inst.transform.SetAsLastSibling(); // 가장 위로 올려서 가려지지 않게

        instances[slot] = inst;
    }

    void ClearAll()
    {
        foreach (var kv in instances)
            if (kv.Value) Destroy(kv.Value);
        instances.Clear();
    }

    // 부모(RectTransform)의 가운데에 딱 맞게 배치
    static void FitToParent(RectTransform rt)
    {
        if (!rt) return;
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f); // 중앙 정렬
        rt.anchoredPosition = Vector2.zero; // 정확히 중앙 위치
        rt.localScale = Vector3.one;
        rt.SetAsLastSibling(); // 위로 올림
    }
}
