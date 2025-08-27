using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : MonoBehaviour
{
    public string itemName;       // �̸�
    public StatType statType;     // ����
    public int amount;            // ���� ��ġ
}
