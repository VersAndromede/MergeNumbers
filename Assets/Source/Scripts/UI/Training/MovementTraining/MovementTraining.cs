using TMPro;
using UnityEngine;

namespace TrainingSystem
{
    public class MovementTraining : MonoBehaviour
    {
        [SerializeField] private GameObject _contentForMobile;
        [SerializeField] private GameObject _contentForDesktop;

        private void Start()
        {
            if (Application.isMobilePlatform)
            {
                _contentForMobile.SetActive(true);
                _contentForDesktop.SetActive(false);
                return;
            }

            _contentForMobile.SetActive(false);
            _contentForDesktop.SetActive(true);
        }
    }
}