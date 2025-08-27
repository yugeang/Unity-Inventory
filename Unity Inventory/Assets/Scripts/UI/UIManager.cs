using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    //�������� ���� ������ �̱��� �ν��Ͻ�

    [Header("Panels")]
    [SerializeField] private UIMainMenu uiMainMenu;
    [SerializeField] private UIStatus uiStatus;
    [SerializeField] private UIInventory uiInventory;
    //������ UI �г� 3��

    [Header("BackButton")]
    [SerializeField] private Button backButton;
    //�ڷΰ��� ��ư ����

    public UIMainMenu MainMenu => uiMainMenu;
    public UIStatus StatusMenu => uiStatus;
    public UIInventory Inventory => uiInventory;
    //�ٸ� ��ũ��Ʈ���� UI�� ������ �� ���

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //UIManager�� �ϳ��� �����ϵ��� ����

        if (backButton != null)
            backButton.onClick.AddListener(OpenMainMenu);
        //��ư ������ ���θ޴���
    }

    private void Start()
    {
        OpenMainMenu(); //���� �� ���� �޴��� �ѱ�
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

    private void ShowOnly(GameObject target) //�ϳ��� �Ѱ� ������ ����
    {
        uiMainMenu.gameObject.SetActive(target == uiMainMenu.gameObject);
        uiStatus.gameObject.SetActive(target == uiStatus.gameObject);
        uiInventory.gameObject.SetActive(target == uiInventory.gameObject);
        //���޹��� ������Ʈ�� Ȱ��ȭ�ϰ� ������ ����

        bool isMain = target == uiMainMenu.gameObject;
        if (backButton != null)
            backButton.gameObject.SetActive(!isMain);
        //���θ޴����� �ڷΰ��� �����
    }
}
