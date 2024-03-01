using Scripts.UI.Menu;

namespace Scripts.TrainingSystem
{
    public class TrainingPresenter
    {
        private readonly Training _training;
        private readonly TrainingView _trainingView;
        private readonly GameStarter _gameStarter;

        public TrainingPresenter(Training training, TrainingView trainingView, GameStarter gameStarter)
        {
            _training = training;
            _trainingView = trainingView;
            _gameStarter = gameStarter;
        }

        public void Enable()
        {
            _trainingView.RequestedNextPageSwitching += OnRequestedNextPageSwitching;
            _trainingView.RequestedBackPageSwitching += OnRequestedBackPageSwitching;
            _trainingView.RequestedTrainingCompletion += OnRequestedTrainingCompletion;

            _training.PageSwitched += OnPageSwitched;
            _training.Viewed += OnViewed;
            _gameStarter.GameStarted += OnGameStarted;
        }

        public void Disable()
        {
            _trainingView.RequestedNextPageSwitching -= OnRequestedNextPageSwitching;
            _trainingView.RequestedBackPageSwitching -= OnRequestedBackPageSwitching;
            _trainingView.RequestedTrainingCompletion -= OnRequestedTrainingCompletion;

            _training.PageSwitched -= OnPageSwitched;
            _training.Viewed -= OnViewed;
            _gameStarter.GameStarted -= OnGameStarted;
        }

        private void OnGameStarted()
        {
            if (_training.IsViewed == false)
                _trainingView.SetActiveContentPanel(true);
        }

        private void OnRequestedNextPageSwitching()
        {
            _training.SwitchNextPage();
        }

        private void OnRequestedBackPageSwitching()
        {
            _training.SwitchBackPage();
        }

        private void OnRequestedTrainingCompletion()
        {
            _training.Complete();
        }

        private void OnPageSwitched()
        {
            if (_training.CurrentPageIndex == _training.LastPageIndex)
                _trainingView.EnableInteractableCompleteButton();
        }

        private void OnViewed()
        {
            _trainingView.SetActiveContentPanel(false);
        }
    }
}
