using UnityEngine;
using UnityEngine.UI;

namespace Pause_Menu
{
    public class MusicSlider : MonoBehaviour
    {
        private const string MusicVolume = "MusicVolume";
        [SerializeField] private Slider _slider;

        private float _volume;

        private void Start()
        {
            if (!PlayerPrefs.HasKey(MusicVolume))
            {
                _slider.value = 1;
            }
            else
                _slider.value = PlayerPrefs.GetFloat(MusicVolume);

        }

        private void Update()
        {
            if (_volume != _slider.value)
            {
                PlayerPrefs.SetFloat(MusicVolume, _slider.value);
                PlayerPrefs.Save();
                _volume = _slider.value;
            }
        }
    }
}