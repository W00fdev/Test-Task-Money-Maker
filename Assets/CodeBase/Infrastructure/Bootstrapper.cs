using UnityEngine;

// Entry point in game logic.
// For prototype reasons the script has been set in "script execution order"

public class Bootstrapper : MonoBehaviour
{
    private IInputService _inputService = null;

    private void Awake() => RegisterInputService();

    public IInputService GetInputService() => _inputService;

    private void RegisterInputService()
    {
        if (Application.isEditor)
            _inputService = new StandaloneInputService();
        else
            _inputService = new MobileInputService();
    }
}
