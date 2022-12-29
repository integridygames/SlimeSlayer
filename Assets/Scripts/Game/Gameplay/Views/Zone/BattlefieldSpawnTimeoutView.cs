using TMPro;
using UnityEngine;

namespace Game.Gameplay.Views.Zone
{
    public class BattlefieldSpawnTimeoutView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timeout;

        public float CurrentTimeout
        {
            set
            {
                if (value > 0)
                {
                    _timeout.gameObject.SetActive(true);
                    _timeout.text = value.ToString("n1");
                }
                else
                {
                    if(_timeout != null)
                        _timeout.gameObject.SetActive(false);
                }
            }
        }
    }
}