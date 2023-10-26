using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController UIInstance;

    [Header("--- Text Mesh Pro ---")]
    [SerializeField] private TextMeshProUGUI _textCurrentScore;
    [SerializeField] private TextMeshProUGUI _textBestScore;
    [SerializeField] private TextMeshProUGUI _textWarning;
    [SerializeField] private TextMeshProUGUI _textWin_Loss;

    [Header("--- Image ---")]
    [SerializeField] private Image _warningTextImgBackground;

    [Header("--- Setting ---")]

    [SerializeField] private GameManagerJSON _gameManagerJSON;

    [SerializeField] private CoinsController _coinsController;

    [SerializeField] private ParticleSystem _particleWin;

    [SerializeField] private RotateMainGameObject _rotatePipe;

    [SerializeField] private BallJumping _ballJumping;

    private SceneManagers _sceneManagers;

    public static int _currentScore;

    private void Awake()
    {
        UIInstance = this;
    }

    private void Start()
    {
        _sceneManagers = SceneManagers.Instance;

        _currentScore = 0;
        _coinsController.gameObject.SetActive(true);
        _gameManagerJSON.LoadDataGame();

        _textCurrentScore.text = _currentScore.ToString();
        _textBestScore.text = ConstantsScript.BEST_SCORE + _gameManagerJSON.PurchasedObjectsJSON.BestScore;

        ManagerEvents.OnGameOver += WinOrLossPanelActive;

        _ballJumping = FindObjectOfType<BallJumping>();
    }

    private void OnDestroy()
    {
        ManagerEvents.OnGameOver -= WinOrLossPanelActive;
    }

    private void Update()
    {
        _textCurrentScore.text = _currentScore.ToString();

        BestScore();
    }

    private void BestScore()
    {
        if (_currentScore > _gameManagerJSON.PurchasedObjectsJSON.BestScore)
        {
            _gameManagerJSON.PurchasedObjectsJSON.BestScore = _currentScore;

            _textBestScore.text = ConstantsScript.BEST_SCORE + _gameManagerJSON.PurchasedObjectsJSON.BestScore;

            _gameManagerJSON.SaveDataGame();
        }
    }

    private void WinOrLossPanelActive()
    {
        _textBestScore.text = ConstantsScript.BEST_SCORE + _gameManagerJSON.PurchasedObjectsJSON.BestScore;
        _textBestScore.gameObject.SetActive(true);

        if (BallJumping._gameLoss == true)
        {
            _textWin_Loss.text = ConstantsScript.TEXT_LOSS;
            _rotatePipe.DontRotate();
            _ballJumping.DontJumping();
            _sceneManagers.DontEnableButton();
        }
        else
        {
            _textWin_Loss.text = ConstantsScript.TEXT_WIN;
            _particleWin.Play();
            _rotatePipe.DontRotate();
            _coinsController.AddCoins();
            _sceneManagers.DontEnableButton();
        }   
    }

    public IEnumerator WarningTextNoCoins()
    {
        _warningTextImgBackground.gameObject.SetActive(true);
        _textWarning.text = ConstantsScript.WARNING_NO_COINS_TEXT;
        yield return new WaitForSeconds(2f);
        _warningTextImgBackground.gameObject.SetActive(false);
    }

    public IEnumerator WarningTextBougthSkin()
    {
        _warningTextImgBackground.gameObject.SetActive(true);
        _textWarning.text = ConstantsScript.WARNING_SKIN_BOUGTH_TEXT;
        yield return new WaitForSeconds(2f);
        _warningTextImgBackground.gameObject.SetActive(false);
    }
}
