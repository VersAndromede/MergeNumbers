using System;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingSystem
{
    public class TrainingView : MonoBehaviour
    {
        [SerializeField] private GameObject _contentPanel;
        [SerializeField] private Button _completeButton;

        public event Action<bool> RequestedPageSwitching;
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

        public void SetInteractableCompleteButton(bool value)
        {
            _completeButton.interactable = value;
        }

        public void SwitchPage(bool next)
        {
            RequestedPageSwitching.Invoke(next);
        }

        private void OnCompleteButtonClicked()
        {
            RequestedTrainingCompletion?.Invoke();
        }
    }
}
