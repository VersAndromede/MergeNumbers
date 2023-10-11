namespace TrainingSystem
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
            _gameStarter.GameStarted += OnGameStarted;
            _trainingView.RequestedPageSwitching += OnRequestedPageSwitching;
            _trainingView.RequestedTrainingCompletion += OnRequestedTrainingCompletion;
            _training.PageSwitched += OnPageSwitched;
            _training.Viewed += OnViewed;
        }

        public void Disable()
        {
            _gameStarter.GameStarted -= OnGameStarted;
            _trainingView.RequestedPageSwitching -= OnRequestedPageSwitching;
            _trainingView.RequestedTrainingCompletion -= OnRequestedTrainingCompletion;
            _training.PageSwitched -= OnPageSwitched;
            _training.Viewed -= OnViewed;
        }

        private void OnGameStarted()
        {
            if (_training.IsViewed == false)
                _trainingView.SetActiveContentPanel(true);
        }

        private void OnRequestedPageSwitching(bool next)
        {
            if (next)
                _training.SwitchNextPage();
            else
                _training.SwitchBackPage();
        }

        private void OnRequestedTrainingCompletion()
        {
            _training.Complete();
        }

        private void OnPageSwitched()
        {
            if (_training.CurrentPageIndex == _training.LastPageIndex)
                _trainingView.SetInteractableCompleteButton(true);
        }

        private void OnViewed()
        {
            _trainingView.SetActiveContentPanel(false);
        }
    }
}
