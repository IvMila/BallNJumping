using TMPro;
using UnityEngine;

public class CoinsController : MonoBehaviour
{
    private TextMeshProUGUI _textCurrentCoins;
    [SerializeField] private GameManagerJSON _gameManager;

    private void Start()
    {
        _gameManager.LoadDataGame();

        _textCurrentCoins = GetComponentInChildren<TextMeshProUGUI>();
        _textCurrentCoins.text = Coins.ToString();
    }

    public void BuySkins(int coins)
    {
        Coins -= coins;
        _textCurrentCoins.text = Coins.ToString();
    }

    public void AddCoins()
    {
        var randomAddCoinsMore30Torus = Random.Range(8, 12);
        var randomAddCoinsLess30Torus = Random.Range(2, 6);
        
        if (RandomSpawnPipe.RandomPipeSpawnCount > 30)
        {
            Coins += randomAddCoinsMore30Torus;
        }
        else Coins += randomAddCoinsLess30Torus;

        _textCurrentCoins.text = Coins.ToString();
    }

    public int Coins
    {
        get { return _gameManager.PurchasedObjectsJSON.Coins; }
        set
        {
            _gameManager.PurchasedObjectsJSON.Coins = value;
            _gameManager.SaveDataGame();
        }
    }
}
