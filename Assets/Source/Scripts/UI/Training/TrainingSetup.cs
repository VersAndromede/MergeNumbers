using UnityEngine;

namespace TrainingSystem
{
    public class TrainingSetup : MonoBehaviour
    {
        [SerializeField] private TrainingView _trainingView;
        [SerializeField] private  GameStarter _gameStarter;

        private TrainingPresenter _trainingPresenter;

        private void OnDestroy()
        {
            _trainingPresenter.Disable();
        }

        public void Init(Training training)
        {
            _trainingPresenter = new TrainingPresenter(training, _trainingView, _gameStarter);
            _trainingPresenter.Enable();
        }
    }
}
