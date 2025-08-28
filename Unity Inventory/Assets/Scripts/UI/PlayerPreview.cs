using System.Collections.Generic;
using UnityEngine;

public class PlayerPreview : MonoBehaviour
{
    [Header("Player Preview Roots (�� ���Կ� �ش� �г��� ��Ʈ�� ����)")]
    [SerializeField] RectTransform hatRoot; // ���� ������
    [SerializeField] RectTransform weaponRoot; // ���� ������
    [SerializeField] RectTransform shieldRoot; // ���� ������

    readonly Dictionary<EquipmentSlot, GameObject> instances = new();

    void OnEnable()
    {
        // ����/���� �̺�Ʈ
        ItemSlot.EquipmentChanged += RenderAll;
        RenderAll();
    }

    // ��Ȱ��ȭ �� �̺�Ʈ
    void OnDisable()
    {
        ItemSlot.EquipmentChanged -= RenderAll;
        ClearAll();
    }

    // �� ������ ���� �ٽ� �׸�
    void RenderAll()
    {
        RenderSlot(EquipmentSlot.Hat, hatRoot);
        RenderSlot(EquipmentSlot.Weapon, weaponRoot);
        RenderSlot(EquipmentSlot.Shield, shieldRoot);
    }

    // Ư�� ���� �ϳ��� �ٽ� �׸�
    void RenderSlot(EquipmentSlot slot, RectTransform root)
    {
        if (instances.TryGetValue(slot, out var old) && old)
        {
            Destroy(old);
            instances.Remove(slot);
        }

        if (!root) return;

        // ���� �ش� ���Կ� ������ ������ ��������
        var prefab = ItemSlot.GetEquippedPrefab(slot);
        if (prefab == null) return; // ���� �� ������ �ƹ� �͵� �� �׸�

        // �� �������� �� �г��� ��Ʈ �ؿ� �ν��Ͻ�ȭ
        var inst = Instantiate(prefab, root);
        FitToParent(inst.transform as RectTransform);
        inst.transform.SetAsLastSibling(); // ���� ���� �÷��� �������� �ʰ�

        instances[slot] = inst;
    }

    void ClearAll()
    {
        foreach (var kv in instances)
            if (kv.Value) Destroy(kv.Value);
        instances.Clear();
    }

    // �θ�(RectTransform)�� ����� �� �°� ��ġ
    static void FitToParent(RectTransform rt)
    {
        if (!rt) return;
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f); // �߾� ����
        rt.anchoredPosition = Vector2.zero; // ��Ȯ�� �߾� ��ġ
        rt.localScale = Vector3.one;
        rt.SetAsLastSibling(); // ���� �ø�
    }
}
