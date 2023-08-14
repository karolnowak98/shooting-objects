using System;

namespace ShootingObjects.Game.Logic
{
    public class GameManager
    {
        public event Action OnRestartGame;
        public event Action OnEndGame;
        public event Action<int> OnStartGame; //<cubesNumberToSpawn>

        public void StartGame(int numberToSpawn) => OnStartGame?.Invoke(numberToSpawn);
        public void EndGame() => OnEndGame?.Invoke();
        public void RestartGame() => OnRestartGame?.Invoke();
    }
}