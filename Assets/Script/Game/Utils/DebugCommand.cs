using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using log4net;
using SGame.UI;
using Unity.Entities;
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
            Game.console.AddCommand("camera", CameraDepth, "[depth|udepth] [0|1]");
            Game.console.AddCommand("framerate", FrameRate, "framerate [num]");
            Game.console.AddCommand("ui", UICommand, "ui [open|close] [name]");
            Game.console.AddCommand("autosave", AutoSaveCmd, "austosave [open|close]");
            Game.console.AddCommand("quality", SetQuality, "quality [level|lod] [num]");
            Game.console.AddCommand("show2dui", Show2DUI, " [uiname] [p uiname] [uipath] [showtext]");

        }

        /// <summary>
        /// 用于测试2D子UI 的代码
        /// </summary>
        /// <param name="args"></param>
        static void Show2DUI(string[] args)
        {
            string uiPath = null;
            string showText = "test text";
            if (args.Length < 2)
            {
                Game.console.Write("error argv num" + + args.Length + "\n");
                return;
            }

            if (args.Length >= 3)
                uiPath = args[2];

            if (args.Length >= 4)
                showText = args[3];

            var parentEntity = UIUtils.GetUIEntity(args[1]);
            if (parentEntity == Entity.Null)
            {
                Game.console.Write("parent ui not found=" + args[1]);
                return;
            }

            var EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            if (!EntityManager.HasComponent<UIWindow>(parentEntity))
            {
                Game.console.Write("parent ui not found=" + args[1]);
                return;
            }
            UIWindow ui = EntityManager.GetComponentObject<UIWindow>(parentEntity);

            var parentComponent = string.IsNullOrEmpty(uiPath) ? ui.BaseValue.content : ui.BaseValue.content.GetChildByPath(uiPath) as GComponent;
            if (parentComponent == null)
            {
                Game.console.Write("uiPath Not found=" + uiPath);
                return;
            }
            
            var ret = UIUtils.Show2DUI(args[0], parentComponent);
            EntityManager.AddComponentObject(ret, new HUDTips(){title = showText});
            Game.console.Write("show2dui success=" + ret);
        }

        static void SetQuality(string[] args)
        {
            if (args.Length != 2)
            {
                Game.console.Write("error argv num\n");
                return;
            }

            if (!int.TryParse(args[1], out int value))
            {
                Game.console.Write("parse int param fail=" + args[1]);
                return;
            }

            switch (args[0])
            {
                case "level":
                    QualitySettings.SetQualityLevel(value);
                    Game.console.Write("set quality level success\n");
                    break;
                
                case "lod":
                    Shader.globalMaximumLOD = value;
                    Game.console.Write("set shader load success\n");
                    break;
                
                default:
                    Game.console.Write("error param=" + args[0]);
                    break;
            }
        }

        static void AutoSaveCmd(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                log.Error("invalid argv");
                return;
            }

            switch (args[0])
            {
                case "open":
                    DataCenterExtension.IS_AUTO_SAVE = true;
                    break;
                case "close":
                    DataCenterExtension.IS_AUTO_SAVE = false;
                    break;
            }
        }

        static void FrameRate(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                log.Error("frame rate args invalid");
                Game.console.Write("frame rate args invalid\n");
                return;
            }

            if (!int.TryParse(args[0], out int value))
            {
                log.Error("can not convert to number =" + args[0]);
                return;
            }
            
            Application.targetFrameRate = value;
        }


        static void CameraDepth(string[] args)
        {
            if (args.Length <= 0)
            {
                return;
            }

            //StageCamera.main
            switch (args[0])
            {
                case "depth":
                    {
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
                    }
                    break;

                case "udepth":
                    {
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
                        var data = StageCamera.main.GetComponent<UniversalAdditionalCameraData>();
                        data.requiresDepthTexture = value != 0;
                    }
                    break;
            }

            //Game.console.Write("game name=" + Camera.main.name);
        }

        static void UICommand(string[] args)
        {
            if (args.Length != 2)
            {
                return;
            }

            switch (args[0])
            {
                case "open":
                    UIUtils.OpenUI(args[1]);
                    break;
                
                case "close":
                    UIUtils.CloseUIByName(args[1]);
                    break;
            }
        }
    }
}