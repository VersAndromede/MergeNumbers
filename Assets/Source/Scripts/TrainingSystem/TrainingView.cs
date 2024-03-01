using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.TrainingSystem
{
    public class TrainingView : MonoBehaviour
    {
        [SerializeField] private GameObject _contentPanel;
        [SerializeField] private Button _completeButton;

        public event Action RequestedNextPageSwitching;
        public event Action RequestedBackPageSwitching;
        public event Action RequestedTrainingCompletion;

        private void OnEnable()
        {
            _completeButton.onClick.AddListener(OnCompleteButtonClicked);
        }

        private void OnDisable()
        {
            _completeButton.onClick.RemoveListener(OnCompleteButtonClicked);
        }

        public void SetActiveContentPanel(bool value)
        {
            _contentPanel.SetActive(value);
        }

        public void EnableInteractableCompleteButton()
        {
            _completeButton.interactable = true;
        }

        public void DisableInteractableCompleteButton()
        {
            _completeButton.interactable = false;
        }

        public void SwitchNextPage()
        {
            RequestedNextPageSwitching.Invoke();
        }

        public void SwitchBackPage()
        {
            RequestedBackPageSwitching.Invoke();
        }

        private void OnCompleteButtonClicked()
        {
            RequestedTrainingCompletion?.Invoke();
        }
    }
}
