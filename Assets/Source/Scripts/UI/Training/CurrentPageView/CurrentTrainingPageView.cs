using TMPro;
using UnityEngine;

namespace TrainingSystem
{
    public class CurrentTrainingPageView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        private Training _training;

        private void OnDestroy()
        {
            _training.PageSwitched -= OnPageSwitched;
        }

        public void Init(Training training)
        {
            _training = training;
            OnPageSwitched();
            _training.PageSwitched += OnPageSwitched;
        }

        private void OnPageSwitched()
        {
            int currentPage = _training.CurrentPageIndex + 1;
            int lastPage = _training.LastPageIndex + 1;

            _text.text = $"{currentPage}/{lastPage}";
        }
    }
}
