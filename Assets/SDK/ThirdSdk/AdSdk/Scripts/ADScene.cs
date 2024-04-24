using System.Collections;
using System.Collections.Generic;

namespace Ad
{
    /// <summary>
    /// 广告场景基类
    /// </summary>
    public class ADSceneBase
    {
        private static Dictionary<string, ADSceneBase> _dictScene = new Dictionary<string, ADSceneBase>();

        internal static void BuildScenes(AdSceneInfo[] arrInfo)
        {
            int len = arrInfo.Length;
            for (int i = 0; i < len; i++)
            {
                ADSceneBase scene = ADSceneBase.GenScene(arrInfo[i]);
                _dictScene.Add(scene.name, scene);
            }
        }

        /// <summary>
        /// 获取场景
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static ADSceneBase FetchScene(string name)
        {
            if (_dictScene.ContainsKey(name))
            {
                return _dictScene[name];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 清空所有场景
        /// </summary>
        internal static void ClearAllScenes()
        {
            foreach (var item in _dictScene.Values)
            {
                item.Destroy();
            }
            _dictScene.Clear();
        }

        /// <summary>
        /// 生成广告场景
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private static ADSceneBase GenScene(AdSceneInfo info)
        {
            ADSceneBase scene = new ADSceneBase();
            scene.Init(info);
            return scene;
        }

        /// <summary>
        /// 忽略去广告的影响
        /// </summary>
        internal bool ignorePurchase { get { return _info == null ? true : _info.ignorePurchase == 1; } }
        /// <summary>
        /// 场景名
        /// </summary>
        internal string name { get { return _info != null ? _info.code : ""; } }

        protected AdSceneInfo _info;

        //out callee
        internal ADExternalHandleDef.calleeVoid2Void calleeClose;
        internal ADExternalHandleDef.calleeBool2Void calleeVideoClose;

        private void Init(AdSceneInfo info)
        {
            _info = info;
        }

        internal void Destroy()
        {
            _info = null;
            calleeClose = null;
            calleeVideoClose = null;
        }

        /// <summary>
        /// 加载广告
        /// </summary>
        internal void Load()
        {
            if (_info == null)
            {
                return;
            }
            string result;
            LoadGroup(out result);
        }

        /// <summary>
        /// 当前组是否有可播放内容
        /// </summary>
        internal bool isAvaiable
        {
            //按策略展示可用的广告 目前按顺序
            get
            {
                ADGroup group = FetchSuitGroup();
                return group != null && group.isAvaiable;
            }
        }

        /// <summary>
        /// 展示广告
        /// </summary>
        internal void Show()
        {
            //按策略展示可用的广告 目前按顺序
            ADGroup group = FetchSuitGroup();
            if (group != null)
            {
                group.calleeClose = OnGroupClose;
                group.Show(_info.code);
                OnShow(group.info.code);
            }
            else
            {
                calleeClose = null;
                OnGroupClose(false);
            }
        }


        /// <summary>
        /// 获取最佳的广告组
        /// </summary>
        /// <returns></returns>
        private ADGroup FetchSuitGroup()
        {
            int len = _info.groups.Length;
            for (int i = 0; i < len; i++)
            {
                ADGroup group = ADGroup.FetchGroup(_info.groups[i].group);
                if (group != null && group.isAvaiable && !AdMonitor.InGlobalGroupShowCD(group.info.cooldown))
                {
                    return group;
                }
            }
            return null;
        }

        /// <summary>
        /// 广告组可用状态检测
        /// </summary>
        private void LoadGroup(out string result)
        {
            //TODO:重构数据采集
            AdGroupBrief brief;
            ADGroup group;

            string resultGroup;
            string resultScene = "no_group";
            result = resultScene;

            for (int i = 0; i < _info.groups.Length; i++)
            {
                brief = _info.groups[i];
                group = ADGroup.FetchGroup(brief.group);
                if (group == null)
                {
                    continue;
                }
                //若是限次组 且没达上限则触发加载后不必处理
                if ( brief.policy == GROUP_POLICY.DAILY_CNT_LIMIT && 
                    ADUtil.FetchDailyShowCntInScene(_info.code, brief.group) < brief.weight )
                {
                    group.Load(out resultGroup);
                    return;
                }
                //默认策略全部加载
                group.Load(out resultGroup);
                resultScene = ADUtil.GetSceneCheckResult(resultGroup, resultScene);
            }

            result = resultScene;
        }

        /// <summary>
        /// 展示组
        /// </summary>
        protected virtual void OnShow(string groupName)
        {
            foreach (var item in _info.groups)
            {
                //若是限次组被展示 则需要采集次数
                if (item.group == groupName && item.policy == GROUP_POLICY.DAILY_CNT_LIMIT)
                {
                    ADUtil.ShowOnceInScene4DailySave(_info.code, item.group);
                    break;
                }
            }
        }

        /// <summary>
        /// 组关闭回调
        /// </summary>
        /// <param name="canVideoReward"></param>
        private void OnGroupClose(bool canVideoReward)
        {
            if (calleeClose != null)
            {
                calleeClose();
                calleeClose = null;
            }
            if (calleeVideoClose != null)
            {
                calleeVideoClose(canVideoReward);
                calleeVideoClose = null;
            }
        }
    }
}
