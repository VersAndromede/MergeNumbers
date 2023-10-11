using System;
using UnityEngine;

namespace TrainingSystem
{
    public class Training
    {
        [field: SerializeField] public bool IsViewed { get; private set; }
        
        private readonly GameObject[] _pageContent;
        
        private int _currentPageIndex;

        public event Action Viewed;

        public Training(bool isViewed, GameObject[] pageContent)
        {
            IsViewed = isViewed;
            _pageContent = pageContent;
        }

        public void SwitchPage()
        {
            _pageContent[_currentPageIndex].SetActive(false);
            _currentPageIndex++;

            if (TryComplete())
                return;

            _pageContent[_currentPageIndex].SetActive(true);
        }

        private bool TryComplete()
        {
            if (_currentPageIndex >= _pageContent.Length)
            {
                IsViewed = true;
                Viewed?.Invoke();
                return true;
            }

            return false;
        }
    }
}
