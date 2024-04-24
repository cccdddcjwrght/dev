using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ad
{
    public class ADGroup
    {
        /// <summary>
        /// 广告组字典
        /// </summary>
        private static Dictionary<string, ADGroup> _dictGroup = new Dictionary<string, ADGroup>();

        internal AdGroupRawData info { get { return _info; } }
        private AdGroupRawData _info;
        protected List<ADUnitBase> _lstUnits;
        
        //out callee
        internal ADExternalHandleDef.calleeBool2Void calleeClose;
        internal ADExternalHandleDef.calleeStr2Void calleeFailed;

        /// <summary>
        /// 创建广告组
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal static ADGroup FetchGroup(string id)
        {
            //包含此id的组
            if (_dictGroup.ContainsKey(id))
            {
                return _dictGroup[id];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 构建广告组
        /// </summary>
        internal static void BuildGroups(AdGroupRawData[] arrInfos)
        {
            ClearGroups();

            int len = arrInfos.Length;
            ADGroup group;
            for (int i = 0; i < len; i++)
            {
                if ( ADUtil.ContainBanner(arrInfos[i]) )
                {
                    group = new BannerGroup();
                }
                else
                {
                    group = new ADGroup();
                }
                group.InitGroup(arrInfos[i]);
                _dictGroup.Add(arrInfos[i].code, group);
                AdMonitor.OnGroupInit(arrInfos[i].cooldown);
            }
        }

        /// <summary>
        /// 清理组信息
        /// </summary>
        internal static void ClearGroups()
        {
            foreach (ADGroup group in _dictGroup.Values)
            {
                group.Destroy();
            }
            _dictGroup.Clear();
        }

        private void InitGroup(AdGroupRawData info)
        {
            _info = info;
            InitUnits();
        }

        internal void Destroy()
        {
            calleeClose = null;
            ReleaseUnits();
        }

        /// <summary>
        /// 加载广告
        /// </summary>
        internal void Load(out string result)
        {
            LoadSingleUnit(out result);
        }

        /// <summary>
        /// 仅对单个单元进行加载
        /// </summary>
        /// <param name="result"></param>
        private void LoadSingleUnit(out string result)
        {
            result = "exist";
            //若组处于加载限制中
            if ( !CheckGroup4Load(this) )
            {
                return;
            }
            //目前按顺序
            //则按策略进行加载
            ADUnitBase unit;
            int len = _lstUnits.Count;
            for (int i = 0; i < len; i++)
            {
                unit = _lstUnits[i];
                //若是被占用 跳过
                if (unit.occupied)
                {
                    continue;
                }
                //若处于请求限制中 或处于加载超时 则触发下一个单元   或者展示中
                if (unit.onReqCD || unit.isLoadOvertime )
                {
                    result = "cd";
                    continue;
                }
                //发起加载请求
                result = "add";
                InvokeUnitLoad(_lstUnits[i]);
                return;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="unit"></param>
        private void InvokeUnitLoad(ADUnitBase unit)
        {
            //对某个单元调用请求，需要订阅后续行为
            ADCenter.Subscribe(this._info.code, unit._code);
            unit.Request();
        }

        /// <summary>
        /// 当前组是否有可播放内容
        /// </summary>
        internal bool isAvaiable
        {
            get
            {
                int len = _lstUnits.Count;
                for (int i = 0; i<len; i++)
                {
                    //如果目前还有可用的广告 则不再加载
                    if (_lstUnits[i].isAvaiable)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 展示广告
        /// </summary>
        internal void Show(string sceneName)
        {
            //按策略展示可用的广告 目前按顺序
            int len = _lstUnits.Count;
            for (int i = 0; i < len; i++)
            {
                //若有可用广告 播之
                if (_lstUnits[i].isAvaiable)
                {
                    ADCenter.Subscribe(_info.code, _lstUnits[i]._code);
                    _lstUnits[i].GroupShow(info.code, sceneName);
                    //标记组展示
                    AdMonitor.OnGroupShow(_info.cooldown);
                    return;
                }
            }
        }


        /// <summary>
        /// 初始化单位
        /// </summary>
        private void InitUnits()
        {
            int len = _info.units.Length;
            _lstUnits = new List<ADUnitBase>();
            ADUnitBase unit;
            for (int i = 0; i < len; i++)
            {
                unit = ADUnitBase.FetchUnit(_info.units[i].unit);
                if (unit == null)
                {
                    continue;
                }
                _lstUnits.Add(unit);
                unit.calleeFail += OnUnitFail;
                unit.calleeClose += OnUnitClose;
            }
        }

        /// <summary>
        /// 释放版位信息
        /// </summary>
        private void ReleaseUnits()
        {
            int len = _info.units.Length;
            for (int i = 0; i < len; i++)
            {
                _lstUnits[i].calleeFail -= OnUnitFail;
                _lstUnits[i].calleeClose -= OnUnitClose;
            }
            _lstUnits = null;
        }

        //单元失败
        virtual protected void OnUnitFail(ADUnitBase unit)
        {
            //若未订阅此单元
            if ( !ADCenter.HasSubscribe(_info.code, unit._code) )
            {
                return;
            }
            //失败取消后续订阅
            ADCenter.UnSubscribe(_info.code, unit._code);
            OnUnitLoadFail(unit);
        }

        private void OnUnitLoadFail(ADUnitBase unit)
        {
            //仅处理下一级广告
            int idx = _lstUnits.IndexOf(unit);
            //容错
            if (idx == -1)
            {
                return;
            }
            //已经穷尽
            if ( (idx + 1) >= _lstUnits.Count )
            {
                //穷尽时需要检查组是否失败
                CheckGroupFailed();
                return;
            }

            //next
            idx += 1;
            ADUnitBase next = _lstUnits[idx];
            
            //若可用 则跳过
            if (next.isAvaiable || next.isLoading || next.showing)
            {
                return;
            }

            //若处于加载cd中
            if ( next.onReqCD )
            {
                //嵌套确保后续的加载继续
                OnUnitLoadFail(next);
                return;
            }
            InvokeUnitLoad(next);
        }

        //单位关闭
        private void OnUnitClose(ADUnitBase unit)
        {
            //若未订阅此单元
            if (!ADCenter.HasSubscribe(_info.code, unit._code))
            {
                return;
            }
            //失败取消后续订阅
            ADCenter.UnSubscribe(_info.code, unit._code);

            if (unit._lastGroupShown != _info.code)
            {
                OnUnitCloseOnOtherGroup(unit);
            }
            else
            {
                AdMonitor.OnGroupClose(_info.cooldown);
                if (calleeClose != null)
                {
                    calleeClose(unit.canVideoReward);
                }
                calleeClose = null;
            }

        }

        //某单元在其他组被关闭了
        private void OnUnitCloseOnOtherGroup(ADUnitBase unit)
        {
            //若当前组没有可用的组 则发起一次请求
            if(!isAvaiable)
            {
                string result;
                Load(out result);
            }
        }

        /// <summary>
        /// 检测广告组是否需要加载
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        private static bool CheckGroup4Load(ADGroup group)
        {
            int len = group._lstUnits.Count;
            ADUnitBase unit;
            for (int i = 0; i < len; i++)
            {
                unit = group._lstUnits[i];

                //如果是可用的状态
                if (unit.isAvaiable)
                {
                    ADCenter.Subscribe(group._info.code, unit._code);
                    return false;
                }

                //如果单元被占用
                if (unit.occupied)
                {
                    continue;
                }
                
                //如果未超时
                if ( (unit.isLoading && !unit.isLoadOvertime) )
                {
                    ADCenter.Subscribe(group._info.code, unit._code);
                    return false;
                }
                //如果有版位处于展示状态
                if (unit.showing)
                {
                    ADCenter.Subscribe(group._info.code, unit._code);
                    return false;
                }
            }
            return true;
        }

        private void CheckGroupFailed()
        {
            if (calleeFailed == null || isAvaiable )
            {
                return;
            }

            calleeFailed(_info.code);
        }
    }

    internal class BannerGroup : ADGroup
    {
        protected override void OnUnitFail(ADUnitBase unit)
        {
            //重建原始内容
            //如果只有一个单元 则不重复创建
            if (_lstUnits.Count > 1)
            {
                unit.RebuildRawUnit();
            }
            base.OnUnitFail(unit);
        }
    }
}
