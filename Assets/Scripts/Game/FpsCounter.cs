using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Game
{
    public class FpsCounter : MonoBehaviour
    {
        [SerializeField] private Text _fpsText;

        private int _updatesCount;
        private float _currentUnscaledTime;
        
        void Update () 
        {
            _currentUnscaledTime += Time.unscaledDeltaTime;
            _updatesCount += 1;
            
            if (_currentUnscaledTime > 0.3f)
            {
                _fpsText.text = ((int)(1f / (_currentUnscaledTime / _updatesCount))).ToString();
                _currentUnscaledTime = 0;
                _updatesCount = 0;
            }
        }
    }
}
