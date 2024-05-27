using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Http;
using SGame.Http;
using log4net;

namespace SGame 
{
    partial class RequestExcuteSystem
    {
        [InitCall]
        static void InitClub() 
        {
            DataCenter.ClubUtil.InitData();
        }

        
        /// <summary>
        /// 俱乐部列表请求
        /// </summary>
        /// <returns></returns>
        public IEnumerator ClubListDataReq()
        {
            HttpPackage pkg = new HttpPackage();
            pkg.data = DataCenter.Instance.accountData.playerID.ToString();
            var result = HttpSystem.Instance.Post("http://192.168.10.109:8082/club/list", pkg.ToJson());
            yield return result;
            if (!string.IsNullOrEmpty(result.error))
            {
                Debug.LogError("club data fail=" + result.error);
                yield break;
            }
            pkg = JsonUtility.FromJson<HttpPackage>(result.data);
            DataCenter.ClubUtil.clubList = JsonUtility.FromJson<ClubList>(pkg.data);
            DataCenter.ClubUtil.DetectionResetData();

            Debug.Log("clubDataList:" + pkg.data);
            if (DataCenter.ClubUtil.clubList.id > 0)
            {
                yield return GetCurrentClubReq();
            }

            EventManager.Instance.Trigger((int)GameEvent.CLUB_LIST_UPDATE);
            EventManager.Instance.Trigger((int)GameEvent.GAME_MAIN_REFRESH);
        }

        /// <summary>
        /// 获取当前俱乐部信息
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        public IEnumerator GetCurrentClubReq() 
        {
            HttpPackage pkg = new HttpPackage();
            var data = new ClubGetData()
            {
                playerId = DataCenter.Instance.accountData.playerID,
                score = (int)PropertyManager.Instance.GetItem(DataCenter.ClubUtil.GetClubCurrencyId()).num,
            };
            pkg.data = JsonUtility.ToJson(data);
            var result = HttpSystem.Instance.Post("http://192.168.10.109:8082/club/current", pkg.ToJson());
            yield return result;
            if (!string.IsNullOrEmpty(result.error))
            {
                Debug.LogError("club get current fail=" + result.error);
                yield break;
            }
            pkg = JsonUtility.FromJson<HttpPackage>(result.data);
            if (pkg.code == 0)
            {
                Debug.Log("club cur data:" + pkg.data);
                DataCenter.ClubUtil.currentData = JsonUtility.FromJson<ClubCurrentData>(pkg.data);
                DataCenter.ClubUtil.clubList.id = DataCenter.ClubUtil.currentData.id;

                DataCenter.ClubUtil.ClearData();

                EventManager.Instance.Trigger((int)GameEvent.RECORD_PROGRESS, (int)RankScoreEnum.FIRST_LOGIN, 1);
                EventManager.Instance.Trigger((int)GameEvent.CLUB_MAIN_UPDATE);
                EventManager.Instance.Trigger((int)GameEvent.GAME_MAIN_REFRESH);
            }
        }


        /// <summary>
        /// 加入俱乐部
        /// </summary>
        /// <returns></returns>
        public IEnumerator JoinClubReq(int clubId) 
        {
            HttpPackage pkg = new HttpPackage();
            var data = new ClubJoinData()
            {
                player_id = DataCenter.Instance.accountData.playerID,
                id = clubId,
            };
            pkg.data = JsonUtility.ToJson(data);
            var result = HttpSystem.Instance.Post("http://192.168.10.109:8082/club/join", pkg.ToJson());
            yield return result;
            if (!string.IsNullOrEmpty(result.error))
            {
                Debug.LogError("club join fail=" + result.error);
                yield break;
            }
            //pkg = JsonUtility.FromJson<HttpPackage>(result.data);
            //DataCenter.ClubUtil.currentData = JsonUtility.FromJson<ClubCurrentData>(pkg.data);
            Debug.Log("club join data:" + pkg.data);
            UIUtils.CloseUIByName("clubfind");
            UIUtils.OpenUI("clubmain");
        }

        /// <summary>
        /// 创建俱乐部
        /// </summary>
        /// <returns></returns>
        public IEnumerator CreateClubReq(string clubName, int icon_id, int frame_id) 
        {
            HttpPackage pkg = new HttpPackage();
            var data = new ClubCreateData()
            {
                title = clubName,
                icon_id = icon_id,
                frame_id = frame_id,
                player_id = DataCenter.Instance.accountData.playerID,
            };
            pkg.data = JsonUtility.ToJson(data);
            var result = HttpSystem.Instance.Post("http://192.168.10.109:8082/club/create", pkg.ToJson());
            yield return result;

            if (!string.IsNullOrEmpty(result.error))
            {
                Debug.LogError("club create fail=" + result.error);
                yield break;
            }
            //pkg = JsonUtility.FromJson<HttpPackage>(result.data);
            //DataCenter.ClubUtil.currentData = JsonUtility.FromJson<ClubCurrentData>(pkg.data);
            Debug.Log("club create data:" + pkg.data);
            UIUtils.CloseUIByName("clubselect");
            UIUtils.CloseUIByName("clubfind");
            UIUtils.CloseUIByName("clubcreate");
            UIUtils.OpenUI("clubmain");
        }

        /// <summary>
        /// 退出俱乐部请求（会长直接解散）
        /// </summary>
        public IEnumerator QuitClubReq() 
        {
            HttpPackage pkg = new HttpPackage();
            pkg.data = DataCenter.Instance.accountData.playerID.ToString();
            var result = HttpSystem.Instance.Post("http://192.168.10.109:8082/club/leave", pkg.ToJson());
            yield return result;

            if (!string.IsNullOrEmpty(result.error))
            {
                Debug.LogError("club quit fail=" + result.error);
                yield break;
            }

            DataCenter.ClubUtil.ResetData();

            //退出后请求下俱乐部列表
            yield return ClubListDataReq();
            UIUtils.CloseUIByName("clubmain");
        }


        /// <summary>
        /// 俱乐部踢人
        /// </summary>
        /// <param name="createId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerator ClubKickReq(long createId, long userId) 
        {
            HttpPackage pkg = new HttpPackage();
            var data = new ClubKickData()
            {
                player_id = createId,
                user_id = userId
            };
            pkg.data = JsonUtility.ToJson(data);
            var result = HttpSystem.Instance.Post("http://192.168.10.109:8082/club/leave", pkg.ToJson());
            yield return result;
            if (!string.IsNullOrEmpty(result.error))
            {
                Debug.LogError("club kick fail=" + result.error);
                yield break;
            }

            DataCenter.ClubUtil.RemoveMember(userId);
            UIUtils.CloseUIByName("clubdetail");
        }

        public void ClubRewardGetReq(int rewardId) 
        {
            var rewardData = DataCenter.ClubUtil.GetClubReward(rewardId);
            if (rewardData != null && !rewardData.isGet)
            {
                var currencyId = DataCenter.ClubUtil.GetClubCurrencyId();
                if (!PropertyManager.Instance.CheckCount(currencyId, rewardData.target,1))
                {
                    "@club_ui_reward_no_tip".Tips();
                    return;
                }
                rewardData.isGet = true;
                if (ConfigSystem.Instance.TryGet<GameConfigs.ClubRewardRowData>(rewardId, out var cfg)) 
                {
                    var list = DataCenter.ClubUtil.GetItemReward(cfg);
                    for (int i = 0; i < list.Count; i++)
                        PropertyManager.Instance.Update(list[i][0], list[i][1], list[i][2]);

                    if (rewardData.isBuff) 
                        EventManager.Instance.Trigger((int)GameEvent.BUFF_TRIGGER, new BuffData(cfg.Buff(0), cfg.Buff(1), 0, DataCenter.ClubUtil.GetResidueTime()) { from = (int)EnumFrom.Club });
                    
                }
                EventManager.Instance.Trigger((int)GameEvent.CLUB_REWARD_UPDATE);
            }
        }
    }
}

