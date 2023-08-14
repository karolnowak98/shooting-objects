using Global.UI;
using ShootingObjects.Game.Logic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ShootingObjects.Game.UI
{
    public class EndPanel : Panel
    {
        [SerializeField] private Button _restartBtn;

        private GameManager _gameManager;

        [Inject]
        private void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
            
            _restartBtn.onClick.AddListener(gameManager.RestartGame);
            _restartBtn.onClick.AddListener(Hide);
            _gameManager.OnEndGame += Show;
        }

        private void OnDestroy()
        {
            _restartBtn.onClick.RemoveAllListeners();
            _gameManager.OnEndGame -= Show;
        }
    }
}