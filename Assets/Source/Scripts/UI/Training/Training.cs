using System;
using UnityEngine;

namespace TrainingSystem
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

            SwitchPage(true);
        }

        public void SwitchBackPage()
        {
            if (CurrentPageIndex == 0)
                return;

            SwitchPage(false);
        }

        private void SwitchPage(bool next)
        {
            _pageContent[CurrentPageIndex].SetActive(false);

            if (next)
                CurrentPageIndex++;
            else
                CurrentPageIndex--;

            _pageContent[CurrentPageIndex].SetActive(true);
            PageSwitched?.Invoke();
        }
    }
}
