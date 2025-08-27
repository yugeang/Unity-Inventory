using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : MonoBehaviour
{
    public string itemName;       // 이름
    public StatType statType;     // 스탯
    public int amount;            // 변경 수치
}
