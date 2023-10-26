using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagers : MonoBehaviour
{
    public static SceneManagers Instance;
    [SerializeField] private Button _buttonShop;
    [SerializeField] private Animator _panelShopAnimator;
    [SerializeField] private RotateMainGameObject _rotateTorus;
    [SerializeField] private ShopController _shopController;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _buttonShop.onClick.AddListener(ShopButton);

        _rotateTorus = FindObjectOfType<RotateMainGameObject>();
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void ShopButton()
    {
        _panelShopAnimator.gameObject.SetActive(true);
        _buttonShop.gameObject.SetActive(false);
        _panelShopAnimator.SetTrigger(ConstantsScript.VISIBLE_SHOP_TRIGGER);
        _rotateTorus.enabled = false;
    }

    private void Update()
    {
        if (IsInputEnabled())
        {
            _shopController.LoadDontBoughtSkinOnClick();
            _panelShopAnimator.gameObject.SetActive(false);
            _buttonShop.gameObject.SetActive(true);

            _rotateTorus.enabled = true;
        }
        if (BallJumping._gameLoss == true || BallJumping._gameWin == true)
        {
            if (IsInputEnabled())
                LoadNextLevel();
        }
    }

    private bool IsInputEnabled()
    {
#if UNITY_EDITOR
        return !EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0);
#else
        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
#endif
    }

    public void DontEnableButton()
    {
        _buttonShop.enabled = false;
    }
}
