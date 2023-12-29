using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SGame
{
    public enum GameLoopType
    {
        Client, // 客户端版本
        Server, // 服务器版本
        Single, // 单机版
    }

    public struct GameLoopRequest
    {
        public GameLoopType gameType;
        public string[] param;
    }


    public class GameLoopFactory
    {
        public static IGameLoop Create(GameLoopType t)
        {
            switch (t)
            {
                case GameLoopType.Client:
                    return new GameClientLoop();

                case GameLoopType.Server:
                    break;

                case GameLoopType.Single:
                    return new GameSingleLoop();
            }

            return null;
        }
    }
}