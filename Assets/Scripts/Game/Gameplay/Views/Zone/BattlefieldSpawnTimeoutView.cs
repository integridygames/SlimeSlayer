using TMPro;
using UnityEngine;

namespace Game.Gameplay.Views.Zone
{
    public class BattlefieldSpawnTimeoutView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timeout;

        public float CurrentTimeout
        {
            set => _timeout.text = value.ToString("n2");
        }
    }
}