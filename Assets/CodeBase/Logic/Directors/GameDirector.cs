using UnityEngine.SceneManagement;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    // It's supposed to be more sophisticated data in more complex prototypes.
    [SerializeField] private int _gameVictoryCoinsCondition;
    [SerializeField] private int _currentCoins;
    
    [SerializeField] private PathDrawer _pathDrawer;
    [SerializeField] private UpdaterUI _updaterUI;

    [SerializeField] private Healthable _healthablePlayer;
    [SerializeField] private Movable _movablePlayer;

    private void Start()
    {
        _movablePlayer.OnCoinCollected = PickUpCoinAndCheckVictory;
        
        _healthablePlayer.OnDead.AddListener(GameDefeat);
        _healthablePlayer.OnDamaged.AddListener(DisableActiveComponents);
    }

    private void PickUpCoinAndCheckVictory(int valueOfCoin)
    {
        _currentCoins = Mathf.Clamp(_currentCoins + valueOfCoin, 0, _gameVictoryCoinsCondition);
        _updaterUI.UpdateCoinCounter(_currentCoins);

        CheckVictoryCondition();
    }

    public void GameVictory()
    {
        DisableActiveComponents();
        
        _updaterUI.ShowGameEndCanvas(isVictory: true);
    }

    public void GameDefeat() => _updaterUI.ShowGameEndCanvas(isVictory: false);

    public void RestartScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private void DisableActiveComponents()
    {
        _pathDrawer.ClearPathAndDisable();
        _movablePlayer.DisableMovement();
    }
 
    private void CheckVictoryCondition()
    {
        if (_currentCoins == _gameVictoryCoinsCondition)
            GameVictory();
    }
}
