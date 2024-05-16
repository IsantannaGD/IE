using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GeneralUIController : MonoBehaviour
{
    [SerializeField] private GameObject _messageBackground;
    [SerializeField] private TextMeshProUGUI _messageDisplay;
    [SerializeField] private Image _pointer;

    [SerializeField] private PanelMovementController _settingsPanelController;
    [SerializeField] private PanelMovementController _inventoryPanelController;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _exitGameButton;
    [SerializeField] private Button _closePanelButton;

    private void Start()
    {
        Initializations();
    }

    private void Initializations()
    {
        GameManager.OnShowMessage += PlayerMessageControllerCallback;
        GameManager.OnCallSettings += CallSettings;

        _mainMenuButton.onClick.AddListener(() => GameManager.LoadSceneCallback(1));
        _exitGameButton.onClick.AddListener(GameManager.QuitGameCallback);
        _closePanelButton.onClick.AddListener(CallSettings);
    }

    private void PlayerMessageControllerCallback(string message, bool status)
    {
        _pointer.color = status ? Color.green : Color.yellow;

        _messageBackground.SetActive(status);
        _messageDisplay.text = message;
    }

    private void CallSettings()
    {
        if (_inventoryPanelController.IsClosed)
        { GameManager.OnGamePause?.Invoke(); }
        
        _settingsPanelController.MovementManagerCallback(ActiveControlPanel);
    }

    private void ActiveControlPanel()
    {
        _settingsPanel.SetActive(!_settingsPanel.activeInHierarchy);
    }

    private void OnDestroy()
    {
        GameManager.OnShowMessage -= PlayerMessageControllerCallback;
        GameManager.OnCallSettings -= CallSettings;
    }
}
