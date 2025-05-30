namespace Common
{
    public class TimerSL
    {
        private float _currentTime;
        public bool isFinished => _currentTime <= 0;

        public TimerSL(float startTime)
        {
            Start(startTime);
        }

        public void Start(float startTime)
        {
            _currentTime = startTime;
        }

        public void RemoveTime(float deltaTime)
        {
            if (_currentTime <= 0)
            {
                return;
            }
            _currentTime -= deltaTime;
        }


    }
}

