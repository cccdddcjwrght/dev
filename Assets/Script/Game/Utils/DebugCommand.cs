using System.Collections;
using System.Collections.Generic;
using log4net;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace SGame
{
    public class DebugCommand
    {
        private static ILog log = LogManager.GetLogger("debug");
        
        [System.Diagnostics.Conditional("ENABLE_DEBUG")]
        public static void Init()
        {
            Game.console.AddCommand("camera", CameraDepth, "Main Camera Depth [num]");
        }

        static void CameraDepth(string[] args)
        {
            if (args.Length <= 0)
            {
                return;
            }

            switch (args[0])
            {
                case "depth":
                    if (args.Length < 2)
                    {
                        Game.console.Write("argv invalid");
                        return;
                    }

                    if (!int.TryParse(args[1], out int value))
                    {
                        Game.console.Write("Pare Int Fail =" + args[1]);
                        return;
                    }
                    var data = Camera.main.GetComponent<UniversalAdditionalCameraData>();
                    data.requiresDepthTexture = value != 0;
                    break;
            }

            //Game.console.Write("game name=" + Camera.main.name);
        }
    }
}