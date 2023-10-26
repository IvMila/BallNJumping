using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopController : MonoBehaviour
{
    [SerializeField] private SphereScriptableObject _sphereScriptableObjects;
    [SerializeField] private GameManagerJSON _gameManager;

    [SerializeField] private GameObject _prefabPageSkin;
    [SerializeField] private List<GameObject> _childPageSkin;
    [SerializeField] private Button _buttonByu;

    [SerializeField] private SpawnRandomPositionSphere _spawnRandomPositionSphere;
    [SerializeField] private CoinsController _coinsController;

    public int _selectedIndex = -1;


    public List<Outline> _outlinePageSkin;

    private UIController _uIController;

    public Renderer _rendererMaterialSkin;

    private void Start()
    {
        _rendererMaterialSkin = _spawnRandomPositionSphere._newPrefab.GetComponent<Renderer>();

        CreatePageSkin();

        LoadSkinDefault();
        _gameManager.LoadDataGame();

        _buttonByu.onClick.AddListener(BuySkin);

        _uIController = UIController.UIInstance;
    }

    private void BuySkin()
    {
        if (_selectedIndex < 0) return;

        foreach (var page in _sphereScriptableObjects.SkinItems)
        {
            if (page.Index == _selectedIndex)
            {
                if (!_gameManager.PurchasedObjectsJSON.PurchasedItems.Contains(page.NameSkin)
                    && _coinsController.Coins >= page.Cost)
                {
                    _gameManager.PurchasedObjectsJSON.PurchasedItems.Add(page.NameSkin);

                    _gameManager.PurchasedObjectsJSON.BoughtIndex = page.Index;
                    _selectedIndex = page.Index;
                    page.Bought = true;

                    _outlinePageSkin[page.Index].effectColor = Color.green;
                    _coinsController.BuySkins(page.Cost);

                    _gameManager.SaveDataGame();
                }
                else if (_gameManager.PurchasedObjectsJSON.PurchasedItems.Contains(page.NameSkin))
                {
                    StartCoroutine(_uIController.WarningTextBougthSkin());
                }
                else StartCoroutine(_uIController.WarningTextNoCoins());
            }
        }
    }

    private void CreatePageSkin()
    {
        foreach (var page in _sphereScriptableObjects.SkinItems)
        {
            GameObject newPage = Instantiate(_prefabPageSkin, transform);

            _childPageSkin.Add(newPage);

            TextMeshProUGUI textCost = newPage.GetComponentInChildren<TextMeshProUGUI>();

            Button button = newPage.GetComponent<Button>();

            Outline newOutline = newPage.GetComponent<Outline>();
            _outlinePageSkin.Add(newOutline);

            Image image = newPage.transform.GetChild(0).GetComponent<Image>();

            LoadPageSkinStart(textCost, page.Index, image);

            var buttonIndex = page.Index;
            button.onClick.AddListener(() => LoadMaterialSkinSphere(buttonIndex));
        }
    }

    private void LoadPageSkinStart(TextMeshProUGUI textCostSkin, int index, Image spriteIsonSkin)
    {
        if (textCostSkin != null && spriteIsonSkin != null)
        {
            textCostSkin.text = _sphereScriptableObjects.SkinItems[index].Cost.ToString();

            spriteIsonSkin.sprite = _sphereScriptableObjects.SkinItems[index].Sprite;

            if (_gameManager.PurchasedObjectsJSON.PurchasedItems.Contains(_sphereScriptableObjects.SkinItems[index].NameSkin))
            {
                _outlinePageSkin[index].effectColor = Color.green;
            }
        }
        return;
    }

    private void LoadMaterialSkinSphere(int index)
    {
        _rendererMaterialSkin.material = _sphereScriptableObjects.SkinItems[index].Material;
        _selectedIndex = index;
        return;
    }

    public void LoadDontBoughtSkinOnClick()
    {
        ///--------------відреагувати метод-------------
        if (!_gameManager.PurchasedObjectsJSON.PurchasedItems.Contains(_sphereScriptableObjects.SkinItems[_selectedIndex].NameSkin))
        {
            _selectedIndex = _gameManager.PurchasedObjectsJSON.BoughtIndex;

            _rendererMaterialSkin.material = _sphereScriptableObjects.SkinItems[_selectedIndex].Material;
        }
        return;
    }

    private void LoadSkinDefault()
    {
        if (!_gameManager.PurchasedObjectsJSON.PurchasedItems.Contains(_sphereScriptableObjects.SkinItems[0].NameSkin))
        {
            _gameManager.PurchasedObjectsJSON.PurchasedItems.Add(_sphereScriptableObjects.SkinItems[0].NameSkin);
            _gameManager.PurchasedObjectsJSON.BoughtIndex = _sphereScriptableObjects.SkinItems[0].Index;
            _rendererMaterialSkin.material = _sphereScriptableObjects.SkinItems[0].Material;
            _selectedIndex = _gameManager.PurchasedObjectsJSON.BoughtIndex;

            _sphereScriptableObjects.SkinItems[0].Bought = true;
            _outlinePageSkin[_gameManager.PurchasedObjectsJSON.BoughtIndex].effectColor = Color.green;

            _gameManager.SaveDataGame();
        }
        else
        {
            _selectedIndex = _gameManager.PurchasedObjectsJSON.BoughtIndex;
            _rendererMaterialSkin.material = _sphereScriptableObjects.SkinItems[_selectedIndex].Material;
        }
        return;
    }
}
