using UnityEngine;

namespace SGame
{
    public class GameCamera : MonoBehaviour
    {
        public static Camera camera;
        // Start is called before the first frame update
        void Start()
        {
            camera = GetComponent<Camera>();
			DontDestroyOnLoad(gameObject);
        }
    }
}
