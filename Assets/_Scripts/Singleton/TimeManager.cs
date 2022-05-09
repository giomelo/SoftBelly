namespace _Scripts.Singleton
{
    public class TimeManager : MonoSingleton<TimeManager>
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}