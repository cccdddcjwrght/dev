namespace SGame
{
    public class SceneRequestFactory
    {
        public static GSceneRequest Create(string sceneName)
        {
            GSceneRequest req = new GSceneRequest();
            req.name = sceneName;
            return req;
        }

        // 不支持网络
        public static INetworkSceneLoader CreateNetwork(string sceneName)
        {
            return null;
        }

        public static GAssetRequest CreatePreloadRequest(string sceneName)
        {
            return null;
        }
    }
}
