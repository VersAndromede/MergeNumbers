using Scripts.Level.BossSystem;
using Scripts.TrainingSystem.CurrentPageView;
using Scripts.TrainingSystem.Cursor;
using UnityEngine;

namespace Scripts.TrainingSystem
{
    public class TrainingInitialization : MonoBehaviour
    {
        [SerializeField] private GameObject[] _pageContent;
        [SerializeField] private TrainingSetup _trainingSetup;
        [SerializeField] private CurrentTrainingPageView _currentTrainingPageView;
        [SerializeField] private TrainingCursor _trainingCursor;

        public Training Training { get; private set; }

        public void Init(BossHealth bossHealth, int bossDataIndex, bool trainingIsViewed)
        {
            Training = new Training(trainingIsViewed, _pageContent);
            _trainingSetup.Init(Training);
            _currentTrainingPageView.Init(Training);
            _trainingCursor.Init(bossHealth, bossDataIndex);
        }
    }
}