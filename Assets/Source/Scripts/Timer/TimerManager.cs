using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Timer
{
    public class TimerManager : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _difficultMultiplier = 1.05f;
        [SerializeField] private float _startTime = 5f;

        private void Start()
        {
            StartCoroutine(Process());
        }

        private IEnumerator Process() // TODO кривая сложности
        {
            int index = 0;
            float difficult = 1;

            while (true)
            {
                _slider.value = 1;

                while (_slider.value > 0)
                {
                    _slider.value -= Time.deltaTime * difficult / _startTime;

                    yield return null;
                }

                difficult *= _difficultMultiplier;
            }
        }
    }
}
