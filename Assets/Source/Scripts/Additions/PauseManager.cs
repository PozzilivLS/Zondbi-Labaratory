using UnityEngine;

namespace Additions
{
    public class PauseManager : MonoBehaviour
    {
        [SerializeField] private Canvas _pauseCanvas;
        [SerializeField] private PlayerInput _playerInput;

        private void Update()
        {
            PauseGame();
        }

        private void PauseGame()
        {
            if (_playerInput.IsPaused)
            {
                if (_pauseCanvas.gameObject.activeInHierarchy is true) return;
                print("paused");
                _pauseCanvas.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                if (_pauseCanvas.gameObject.activeInHierarchy is false) return;
                print("not paused");
                _pauseCanvas.gameObject.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }
}