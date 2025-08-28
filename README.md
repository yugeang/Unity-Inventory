# Unity Inventory & Equipment UI

이 프로젝트는 **플레이어 장착 아이템을 여러 UI 패널에서 실시간으로 반영**하는 Unity 예제입니다.  
Inventory에서 아이템을 장착/해제하면 **MainMenu**와 **Status** 패널에도 동일한 프리뷰와 스탯 변화가 즉시 적용됩니다.

---

## 프로젝트 구조

### **데이터**
| 파일 | 설명 |
|-------|------|
| `ItemData.cs` | 아이템 이름, 스탯 타입, 수치를 저장하는 `ScriptableObject`. 에디터에서 생성 가능. |
| `StatType.cs` | `Attack`, `Defense`, `Critical`, `MaxHP` 스탯 타입 정의. |

### **코어 로직**
| 파일 | 설명 |
|-------|------|
| `Character.cs` | 플레이어 기본/보너스 스탯 관리. `OnStatsChanged` 이벤트로 UI 업데이트 알림. |
| `GameManager.cs` | 싱글톤. `Player` 생성 후 UI에 데이터 전달. |

### **인벤토리 / 장착**
| 파일 | 설명 |
|-------|------|
| `ItemSlot.cs` | 슬롯 클릭/호버 처리, 장착/해제 로직, 인스턴스 관리. `EquipmentChanged` 이벤트로 UI 갱신 트리거 제공. |
| `PlayerPreview.cs` | 현재 장착 상태를 MainMenu, Status, Inventory 등 **여러 패널에서 공통으로 렌더링**. |

### **UI**
| 파일 | 설명 |
|-------|------|
| `UIManager.cs` | UI 패널 전환과 Back 버튼 제어. |
| `UIMainMenu.cs` | 메인 메뉴 버튼 동작 처리. |
| `UIStatus.cs` | 플레이어 스탯 표시. 스탯 변경 시 자동 갱신. |
| `UIInventory.cs` | 인벤토리 패널용 확장 포인트(현재는 빈 스크립트). |

---

## 주요 기능

### **1. 장착/해제**
- 인벤토리 슬롯 클릭 → 장착(`Equip`) 또는 해제(`Unequip`) 실행
- `Character`의 스탯이 즉시 갱신되고, `OnStatsChanged` 이벤트로 Status UI가 업데이트
- `EquipmentChanged` 이벤트로 모든 `PlayerPreview` 컴포넌트가 실시간 갱신

### **2. 장착 프리뷰**
- `PlayerPreview`는 각 패널에 위치
- 이벤트 수신 후 해당 슬롯의 `equippedPrefab`을 RectTransform 중앙에 인스턴스화
- `FitToParent`로 크기·위치 정렬

### **3. UI 패널 전환**
- `UIManager`로 MainMenu, Status, Inventory 간 전환
- Back 버튼은 MainMenu에서는 숨기고, 다른 메뉴에서는 표시

---

## 사용법

### **1. 아이템 데이터 생성**
1. 프로젝트 뷰에서 **`Create > Inventory > Item`** 선택
2. `ItemData` 에셋에 이름, 스탯, 수치를 입력

### **2. 인벤토리 슬롯 설정**
1. `ItemSlot` 컴포넌트를 가진 오브젝트에 다음 필드를 연결:
   - `equipSlot` (Hat/Weapon/Shield)
   - `equippedPrefab` (프리뷰용 프리팹)
2. 프리뷰 위치로 사용할 `hatRoot`, `weaponRoot`, `shieldRoot` RectTransform 지정

### **3. 패널에 PlayerPreview 배치**
1. MainMenu 및 Status 패널에 `PlayerPreview` 컴포넌트 추가
2. `hatRoot`, `weaponRoot`, `shieldRoot`를 드래그해 연결

---

## 확장 아이디어
- **저장/로드 기능**: PlayerPrefs나 JSON으로 장착 상태 저장
- **아이콘 표시**: 인벤토리 슬롯에 아이콘 이미지 추가
- **툴팁 UI**: 슬롯 마우스 오버 시 상세 정보 표시
- **이펙트 연계**: 장착 시 파티클이나 애니메이션 재생

---

## 동작 흐름

```mermaid
flowchart TD
    A[슬롯 클릭] --> B{장착됨?}
    B -- No --> C[Equip 실행]
    B -- Yes --> D[Unequip 실행]
    C & D --> E[Character 스탯 갱신]
    E --> F[OnStatsChanged → UIStatus 갱신]
    C & D --> G[EquipmentChanged 이벤트 발생]
    G --> H[PlayerPreview 갱신]
