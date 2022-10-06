using UnityEngine;
using TMPro;

public class UpdaterUI : MonoBehaviour
{
    [SerializeField] private GameObject _canvasGameEnd;
    
    [SerializeField] private TextMeshProUGUI _gameEndText;
    [SerializeField] private TextMeshProUGUI _coinCounterText;

    [SerializeField] [Multiline] private string _gameVictoryText;
    [SerializeField] [Multiline] private string _gameDefeatText;
    
    private const string CoinCounterPrefix = "Монет: ";

    public void ShowGameEndCanvas(bool isVictory)
    {
        _gameEndText.text = isVictory ? _gameVictoryText : _gameDefeatText;
        _canvasGameEnd.SetActive(true);
    }

    public void UpdateCoinCounter(int count) => _coinCounterText.text = CoinCounterPrefix + count;
}
