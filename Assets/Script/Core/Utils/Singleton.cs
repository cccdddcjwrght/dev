namespace SGame
{


    public class Singleton<T> where T : class, new()
    {
        private static T s_instance = null;

        public static T Instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = new T();

                return s_instance;
            }
        }

        public static bool HasInitialize
        {
            get { return s_instance != null; }
        }
    }
}