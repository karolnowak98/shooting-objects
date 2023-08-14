using Global.UI;
using ShootingObjects.Game.Data;
using ShootingObjects.Game.Logic;
using UnityEngine;
using Zenject;

namespace ShootingObjects.Game.UI
{
    public class MenuPanel : Panel
    {
        [SerializeField] private ObjectsButtonUI[] _buttons;

        private GameManager _gameManager;
        
        [Inject]
        private void Construct(GameConfig gameConfig, GameManager gameManager)
        {
            _gameManager = gameManager;
            
            for (var i = 0; i < gameConfig.CubesToSpawn.Length; i++)
            {
                var number = gameConfig.CubesToSpawn[i];
                _buttons[i].Label.text = number.ToString();
                _buttons[i].Button.onClick.AddListener(() => _gameManager.StartGame(number));
                _buttons[i].Button.onClick.AddListener(Hide);
            }

            _gameManager.OnRestartGame += Show;
        }
        
        private void OnDestroy()
        {
            foreach (var objectsButton in _buttons)
            {
                objectsButton.Button.onClick.RemoveAllListeners();
            }

            _gameManager.OnRestartGame -= Show;
        }
    }
}