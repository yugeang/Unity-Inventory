using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    //전역에서 접근 가능한 싱글톤 인스턴스

    [Header("Panels")]
    [SerializeField] private UIMainMenu uiMainMenu;
    [SerializeField] private UIStatus uiStatus;
    [SerializeField] private UIInventory uiInventory;
    //연결할 UI 패널 3개

    [Header("BackButton")]
    [SerializeField] private Button backButton;
    //뒤로가기 버튼 연결

    public UIMainMenu MainMenu => uiMainMenu;
    public UIStatus StatusMenu => uiStatus;
    public UIInventory Inventory => uiInventory;
    //다른 스크립트에서 UI에 접근할 때 사용

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //UIManager이 하나만 존재하도록 설정

        if (backButton != null)
            backButton.onClick.AddListener(OpenMainMenu);
        //버튼 누르면 메인메뉴로
    }

    private void Start()
    {
        OpenMainMenu(); //시작 시 메인 메뉴만 켜기
    }

    public void OpenMainMenu()
    {
        ShowOnly(uiMainMenu.gameObject);
    }

    public void OpenStatus()
    {
        ShowOnly(uiStatus.gameObject);
    }

    public void OpenInventory()
    {
        ShowOnly(uiInventory.gameObject);
    }

    private void ShowOnly(GameObject target) //하나만 켜고 나머지 끄기
    {
        uiMainMenu.gameObject.SetActive(target == uiMainMenu.gameObject);
        uiStatus.gameObject.SetActive(target == uiStatus.gameObject);
        uiInventory.gameObject.SetActive(target == uiInventory.gameObject);
        //전달받은 오브젝트만 활성화하고 나머지 꺼짐

        bool isMain = target == uiMainMenu.gameObject;
        if (backButton != null)
            backButton.gameObject.SetActive(!isMain);
        //메인메뉴에서 뒤로가기 숨기기
    }
}
