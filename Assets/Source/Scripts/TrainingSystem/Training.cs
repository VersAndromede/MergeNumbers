using System;
using UnityEngine;

namespace Scripts.TrainingSystem
{
    public class Training
    {
        [field: SerializeField] public bool IsViewed { get; private set; }

        private readonly GameObject[] _pageContent;

        public int CurrentPageIndex { get; private set; }
        public int LastPageIndex => _pageContent.Length - 1;

        public event Action PageSwitched;
        public event Action Viewed;

        public Training(bool isViewed, GameObject[] pageContent)
        {
            IsViewed = isViewed;
            _pageContent = pageContent;
            _pageContent[CurrentPageIndex].SetActive(true);
        }

        public void Complete()
        {
            IsViewed = true;
            Viewed?.Invoke();
        }

        public void SwitchNextPage()
        {
            if (CurrentPageIndex == LastPageIndex)
                return;

            _pageContent[CurrentPageIndex].SetActive(false);
            CurrentPageIndex++;
            EnableCurrentPage();
        }

        public void SwitchBackPage()
        {
            if (CurrentPageIndex == 0)
                return;

            _pageContent[CurrentPageIndex].SetActive(false);
            CurrentPageIndex--;
            EnableCurrentPage();
        }

        private void EnableCurrentPage()
        {
            _pageContent[CurrentPageIndex].SetActive(true);
            PageSwitched?.Invoke();
        }
    }
}
