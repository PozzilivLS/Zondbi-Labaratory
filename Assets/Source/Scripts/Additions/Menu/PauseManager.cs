using UnityEngine;
using Additions;

namespace Menu
{
    public class PauseManager : MonoBehaviour
    {
        [SerializeField] private Canvas _pauseCanvas;
        [SerializeField] private PlayerInput _playerInput;

        private void OnEnable() 
        {
            _playerInput.PauseStateChanged += OnPauseGame;
        }

        private void OnDisable()
        {
            _playerInput.PauseStateChanged -= OnPauseGame;
        }

        private void OnPauseGame(bool pauseState)
        {
            if (pauseState)
            {
                if (_pauseCanvas.gameObject.activeInHierarchy is true) return;
                _pauseCanvas.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                if (_pauseCanvas.gameObject.activeInHierarchy is false) return;
                _pauseCanvas.gameObject.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }
}