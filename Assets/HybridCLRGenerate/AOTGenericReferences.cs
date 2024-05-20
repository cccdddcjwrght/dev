using System.Collections.Generic;
public class AOTGenericReferences : UnityEngine.MonoBehaviour
{

	// {{ AOT assemblies
	public static readonly IReadOnlyList<string> PatchedAOTAssemblyList = new List<string>
	{
		"Cinemachine.dll",
		"Core.dll",
		"FlatBuffers.dll",
		"LitJSON.dll",
		"System.Core.dll",
		"System.dll",
		"Unity.Burst.dll",
		"Unity.Collections.dll",
		"Unity.Entities.dll",
		"Unity.Properties.dll",
		"Unity.VisualScripting.Core.dll",
		"Unity.VisualScripting.Flow.dll",
		"UnityEngine.CoreModule.dll",
		"UnityEngine.JSONSerializeModule.dll",
		"mscorlib.dll",
	};
	// }}

	// {{ constraint implement type
	// }} 

	// {{ AOT generic types
	// Callback<GamePackage>
	// Callback<SGame.BuffData>
	// Callback<Unity.Entities.Entity>
	// Callback<Unity.Mathematics.int2>
	// Callback<UnityEngine.Vector2Int,object,object>
	// Callback<byte>
	// Callback<double,double>
	// Callback<int,UnityEngine.Vector2,UnityEngine.Vector2,float>
	// Callback<int,float,object>
	// Callback<int,int,int,int>
	// Callback<int,int,int>
	// Callback<int,int,object>
	// Callback<int,int>
	// Callback<int,object,float>
	// Callback<int,object>
	// Callback<int>
	// Callback<object,byte>
	// Callback<object,int,int,int,int>
	// Callback<object,int>
	// Callback<object,object,UnityEngine.Vector2,float>
	// Callback<object,object>
	// Callback<object>
	// Coroutine.<CoWait>d__15<object>
	// FlatBuffers.Offset<GameConfigs.ADConfig>
	// FlatBuffers.Offset<GameConfigs.ADConfigRowData>
	// FlatBuffers.Offset<GameConfigs.AbilityLevel>
	// FlatBuffers.Offset<GameConfigs.AbilityLevelRowData>
	// FlatBuffers.Offset<GameConfigs.AbilityList>
	// FlatBuffers.Offset<GameConfigs.AbilityListRowData>
	// FlatBuffers.Offset<GameConfigs.ActivityTime>
	// FlatBuffers.Offset<GameConfigs.ActivityTimeRowData>
	// FlatBuffers.Offset<GameConfigs.Avatar>
	// FlatBuffers.Offset<GameConfigs.AvatarRowData>
	// FlatBuffers.Offset<GameConfigs.Buff>
	// FlatBuffers.Offset<GameConfigs.BuffRowData>
	// FlatBuffers.Offset<GameConfigs.Bullet>
	// FlatBuffers.Offset<GameConfigs.BulletRowData>
	// FlatBuffers.Offset<GameConfigs.Chest>
	// FlatBuffers.Offset<GameConfigs.ChestRowData>
	// FlatBuffers.Offset<GameConfigs.CmdConfig>
	// FlatBuffers.Offset<GameConfigs.CmdConfigRowData>
	// FlatBuffers.Offset<GameConfigs.Design_Global>
	// FlatBuffers.Offset<GameConfigs.Design_GlobalRowData>
	// FlatBuffers.Offset<GameConfigs.Egg>
	// FlatBuffers.Offset<GameConfigs.EggRowData>
	// FlatBuffers.Offset<GameConfigs.Entry>
	// FlatBuffers.Offset<GameConfigs.EntryRowData>
	// FlatBuffers.Offset<GameConfigs.Equip>
	// FlatBuffers.Offset<GameConfigs.EquipBuff>
	// FlatBuffers.Offset<GameConfigs.EquipBuffRowData>
	// FlatBuffers.Offset<GameConfigs.EquipQuality>
	// FlatBuffers.Offset<GameConfigs.EquipQualityRowData>
	// FlatBuffers.Offset<GameConfigs.EquipRowData>
	// FlatBuffers.Offset<GameConfigs.EquipUpLevelCost>
	// FlatBuffers.Offset<GameConfigs.EquipUpLevelCostRowData>
	// FlatBuffers.Offset<GameConfigs.Equipment>
	// FlatBuffers.Offset<GameConfigs.EquipmentRowData>
	// FlatBuffers.Offset<GameConfigs.Evolution>
	// FlatBuffers.Offset<GameConfigs.EvolutionRowData>
	// FlatBuffers.Offset<GameConfigs.Exchange>
	// FlatBuffers.Offset<GameConfigs.ExchangeRowData>
	// FlatBuffers.Offset<GameConfigs.FunctionConfig>
	// FlatBuffers.Offset<GameConfigs.FunctionConfigRowData>
	// FlatBuffers.Offset<GameConfigs.HunterWheel>
	// FlatBuffers.Offset<GameConfigs.HunterWheelRowData>
	// FlatBuffers.Offset<GameConfigs.Item>
	// FlatBuffers.Offset<GameConfigs.ItemRowData>
	// FlatBuffers.Offset<GameConfigs.Language_CHN>
	// FlatBuffers.Offset<GameConfigs.Language_CHNRowData>
	// FlatBuffers.Offset<GameConfigs.Language_EN>
	// FlatBuffers.Offset<GameConfigs.Language_ENRowData>
	// FlatBuffers.Offset<GameConfigs.Language_HI>
	// FlatBuffers.Offset<GameConfigs.Language_HIRowData>
	// FlatBuffers.Offset<GameConfigs.Language_setting>
	// FlatBuffers.Offset<GameConfigs.Language_settingRowData>
	// FlatBuffers.Offset<GameConfigs.Level>
	// FlatBuffers.Offset<GameConfigs.LevelRowData>
	// FlatBuffers.Offset<GameConfigs.Machine>
	// FlatBuffers.Offset<GameConfigs.MachineRowData>
	// FlatBuffers.Offset<GameConfigs.MachineStar>
	// FlatBuffers.Offset<GameConfigs.MachineStarRowData>
	// FlatBuffers.Offset<GameConfigs.MachineUpgrade>
	// FlatBuffers.Offset<GameConfigs.MachineUpgradeRowData>
	// FlatBuffers.Offset<GameConfigs.MerchantMission>
	// FlatBuffers.Offset<GameConfigs.MerchantMissionRowData>
	// FlatBuffers.Offset<GameConfigs.MerchantReward>
	// FlatBuffers.Offset<GameConfigs.MerchantRewardRowData>
	// FlatBuffers.Offset<GameConfigs.MonsterHunter>
	// FlatBuffers.Offset<GameConfigs.MonsterHunterRowData>
	// FlatBuffers.Offset<GameConfigs.Pets>
	// FlatBuffers.Offset<GameConfigs.PetsRowData>
	// FlatBuffers.Offset<GameConfigs.ProgressPack>
	// FlatBuffers.Offset<GameConfigs.ProgressPackRowData>
	// FlatBuffers.Offset<GameConfigs.RankConfig>
	// FlatBuffers.Offset<GameConfigs.RankConfigRowData>
	// FlatBuffers.Offset<GameConfigs.RedConfig>
	// FlatBuffers.Offset<GameConfigs.RedConfigRowData>
	// FlatBuffers.Offset<GameConfigs.Region>
	// FlatBuffers.Offset<GameConfigs.RegionRowData>
	// FlatBuffers.Offset<GameConfigs.RoleData>
	// FlatBuffers.Offset<GameConfigs.RoleDataRowData>
	// FlatBuffers.Offset<GameConfigs.Room>
	// FlatBuffers.Offset<GameConfigs.RoomExclusive>
	// FlatBuffers.Offset<GameConfigs.RoomExclusiveRowData>
	// FlatBuffers.Offset<GameConfigs.RoomLike>
	// FlatBuffers.Offset<GameConfigs.RoomLikeRowData>
	// FlatBuffers.Offset<GameConfigs.RoomMachine>
	// FlatBuffers.Offset<GameConfigs.RoomMachineRowData>
	// FlatBuffers.Offset<GameConfigs.RoomRowData>
	// FlatBuffers.Offset<GameConfigs.RoomTech>
	// FlatBuffers.Offset<GameConfigs.RoomTechRowData>
	// FlatBuffers.Offset<GameConfigs.Scene>
	// FlatBuffers.Offset<GameConfigs.SceneRowData>
	// FlatBuffers.Offset<GameConfigs.SettingConfig>
	// FlatBuffers.Offset<GameConfigs.SettingConfigRowData>
	// FlatBuffers.Offset<GameConfigs.Shop>
	// FlatBuffers.Offset<GameConfigs.ShopRowData>
	// FlatBuffers.Offset<GameConfigs.Sound_Effect>
	// FlatBuffers.Offset<GameConfigs.Sound_EffectRowData>
	// FlatBuffers.Offset<GameConfigs.effects>
	// FlatBuffers.Offset<GameConfigs.effectsRowData>
	// FlatBuffers.Offset<GameConfigs.fonts>
	// FlatBuffers.Offset<GameConfigs.fontsRowData>
	// FlatBuffers.Offset<GameConfigs.friend>
	// FlatBuffers.Offset<GameConfigs.friendRowData>
	// FlatBuffers.Offset<GameConfigs.game_global>
	// FlatBuffers.Offset<GameConfigs.game_globalRowData>
	// FlatBuffers.Offset<GameConfigs.language>
	// FlatBuffers.Offset<GameConfigs.languageRowData>
	// FlatBuffers.Offset<GameConfigs.role>
	// FlatBuffers.Offset<GameConfigs.roleRowData>
	// FlatBuffers.Offset<GameConfigs.table>
	// FlatBuffers.Offset<GameConfigs.tableRowData>
	// FlatBuffers.Offset<GameConfigs.ui_groups>
	// FlatBuffers.Offset<GameConfigs.ui_groupsRowData>
	// FlatBuffers.Offset<GameConfigs.ui_res>
	// FlatBuffers.Offset<GameConfigs.ui_resRowData>
	// SGame.EventHandleV1<GamePackage>
	// SGame.EventHandleV1<SGame.BuffData>
	// SGame.EventHandleV1<Unity.Entities.Entity>
	// SGame.EventHandleV1<Unity.Mathematics.int2>
	// SGame.EventHandleV1<byte>
	// SGame.EventHandleV1<int>
	// SGame.EventHandleV1<object>
	// SGame.EventHandleV2<double,double>
	// SGame.EventHandleV2<int,int>
	// SGame.EventHandleV2<int,object>
	// SGame.EventHandleV2<object,byte>
	// SGame.EventHandleV2<object,int>
	// SGame.EventHandleV2<object,object>
	// SGame.EventHandleV3<UnityEngine.Vector2Int,object,object>
	// SGame.EventHandleV3<int,float,object>
	// SGame.EventHandleV3<int,int,int>
	// SGame.EventHandleV3<int,int,object>
	// SGame.EventHandleV4<int,UnityEngine.Vector2,UnityEngine.Vector2,float>
	// SGame.EventHandleV4<int,int,int,int>
	// SGame.EventHandleV4<object,object,UnityEngine.Vector2,float>
	// SGame.EventHandleV5<object,int,int,int,int>
	// SGame.MonoSingleton<object>
	// SGame.WaitEvent<int>
	// SGame.WaitEvent<object>
	// StateMachine.State<int>
	// StateMachine.StateFunc<int>
	// StateMachine<int>
	// System.Action<GameConfigs.ADConfigRowData>
	// System.Action<GameConfigs.AbilityLevelRowData>
	// System.Action<GameConfigs.ActivityTimeRowData>
	// System.Action<GameConfigs.AvatarRowData>
	// System.Action<GameConfigs.CmdConfigRowData>
	// System.Action<GameConfigs.EquipmentRowData>
	// System.Action<GameConfigs.FunctionConfigRowData>
	// System.Action<GameConfigs.HunterWheelRowData>
	// System.Action<GameConfigs.ItemRowData>
	// System.Action<GameConfigs.MerchantMissionRowData>
	// System.Action<GameConfigs.MerchantRewardRowData>
	// System.Action<GameConfigs.MonsterHunterRowData>
	// System.Action<GameConfigs.PetsRowData>
	// System.Action<GameConfigs.RedConfigRowData>
	// System.Action<GameConfigs.RoleDataRowData>
	// System.Action<GameConfigs.RoomExclusiveRowData>
	// System.Action<GameConfigs.RoomLikeRowData>
	// System.Action<GameConfigs.RoomMachineRowData>
	// System.Action<GameConfigs.RoomRowData>
	// System.Action<GameConfigs.RoomTechRowData>
	// System.Action<GameConfigs.ShopRowData>
	// System.Action<SDK.TDSDK.ItemInfo>
	// System.Action<SGame.AbilityData.AbilitLevelRenderer>
	// System.Action<SGame.ActiveTimeSystem.ActiveData>
	// System.Action<SGame.AdItem>
	// System.Action<SGame.BuffData>
	// System.Action<SGame.BulletHitSystem.Data>
	// System.Action<SGame.ChairData>
	// System.Action<SGame.CharacterDespawnSystem.EventData>
	// System.Action<SGame.CharacterSpawnSystem.CharacterEvent>
	// System.Action<SGame.DelayExcuter.Item>
	// System.Action<SGame.GameLoopRequest>
	// System.Action<SGame.GiftReward>
	// System.Action<SGame.ItemData.Value>
	// System.Action<SGame.ObjectPool.ExtendData<object>>
	// System.Action<SGame.RecordRoleData>
	// System.Action<SGame.Worker>
	// System.Action<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Action<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Action<System.UIntPtr>
	// System.Action<Unity.Entities.Entity>
	// System.Action<Unity.Mathematics.int2>
	// System.Action<Unity.VisualScripting.CustomEventArgs>
	// System.Action<UnityEngine.CombineInstance>
	// System.Action<UnityEngine.Vector2>
	// System.Action<UnityEngine.Vector2Int>
	// System.Action<byte,Unity.Entities.Entity>
	// System.Action<byte,object,object>
	// System.Action<byte,object>
	// System.Action<byte>
	// System.Action<int,double,double>
	// System.Action<int,int>
	// System.Action<int,object,object>
	// System.Action<int,object>
	// System.Action<int>
	// System.Action<object,GamePackage>
	// System.Action<object,object>
	// System.Action<object>
	// System.ByReference<ushort>
	// System.Collections.Concurrent.ConcurrentDictionary.<GetEnumerator>d__35<object,object>
	// System.Collections.Concurrent.ConcurrentDictionary.DictionaryEnumerator<object,object>
	// System.Collections.Concurrent.ConcurrentDictionary.Node<object,object>
	// System.Collections.Concurrent.ConcurrentDictionary.Tables<object,object>
	// System.Collections.Concurrent.ConcurrentDictionary<object,object>
	// System.Collections.Generic.ArraySortHelper<GameConfigs.ADConfigRowData>
	// System.Collections.Generic.ArraySortHelper<GameConfigs.AbilityLevelRowData>
	// System.Collections.Generic.ArraySortHelper<GameConfigs.ActivityTimeRowData>
	// System.Collections.Generic.ArraySortHelper<GameConfigs.AvatarRowData>
	// System.Collections.Generic.ArraySortHelper<GameConfigs.CmdConfigRowData>
	// System.Collections.Generic.ArraySortHelper<GameConfigs.EquipmentRowData>
	// System.Collections.Generic.ArraySortHelper<GameConfigs.FunctionConfigRowData>
	// System.Collections.Generic.ArraySortHelper<GameConfigs.HunterWheelRowData>
	// System.Collections.Generic.ArraySortHelper<GameConfigs.ItemRowData>
	// System.Collections.Generic.ArraySortHelper<GameConfigs.MerchantMissionRowData>
	// System.Collections.Generic.ArraySortHelper<GameConfigs.MerchantRewardRowData>
	// System.Collections.Generic.ArraySortHelper<GameConfigs.MonsterHunterRowData>
	// System.Collections.Generic.ArraySortHelper<GameConfigs.PetsRowData>
	// System.Collections.Generic.ArraySortHelper<GameConfigs.RedConfigRowData>
	// System.Collections.Generic.ArraySortHelper<GameConfigs.RoleDataRowData>
	// System.Collections.Generic.ArraySortHelper<GameConfigs.RoomExclusiveRowData>
	// System.Collections.Generic.ArraySortHelper<GameConfigs.RoomLikeRowData>
	// System.Collections.Generic.ArraySortHelper<GameConfigs.RoomMachineRowData>
	// System.Collections.Generic.ArraySortHelper<GameConfigs.RoomRowData>
	// System.Collections.Generic.ArraySortHelper<GameConfigs.RoomTechRowData>
	// System.Collections.Generic.ArraySortHelper<GameConfigs.ShopRowData>
	// System.Collections.Generic.ArraySortHelper<SDK.TDSDK.ItemInfo>
	// System.Collections.Generic.ArraySortHelper<SGame.AbilityData.AbilitLevelRenderer>
	// System.Collections.Generic.ArraySortHelper<SGame.ActiveTimeSystem.ActiveData>
	// System.Collections.Generic.ArraySortHelper<SGame.AdItem>
	// System.Collections.Generic.ArraySortHelper<SGame.BuffData>
	// System.Collections.Generic.ArraySortHelper<SGame.BulletHitSystem.Data>
	// System.Collections.Generic.ArraySortHelper<SGame.ChairData>
	// System.Collections.Generic.ArraySortHelper<SGame.CharacterDespawnSystem.EventData>
	// System.Collections.Generic.ArraySortHelper<SGame.CharacterSpawnSystem.CharacterEvent>
	// System.Collections.Generic.ArraySortHelper<SGame.DelayExcuter.Item>
	// System.Collections.Generic.ArraySortHelper<SGame.GameLoopRequest>
	// System.Collections.Generic.ArraySortHelper<SGame.GiftReward>
	// System.Collections.Generic.ArraySortHelper<SGame.ItemData.Value>
	// System.Collections.Generic.ArraySortHelper<SGame.ObjectPool.ExtendData<object>>
	// System.Collections.Generic.ArraySortHelper<SGame.RecordRoleData>
	// System.Collections.Generic.ArraySortHelper<SGame.Worker>
	// System.Collections.Generic.ArraySortHelper<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.ArraySortHelper<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.ArraySortHelper<System.UIntPtr>
	// System.Collections.Generic.ArraySortHelper<Unity.Entities.Entity>
	// System.Collections.Generic.ArraySortHelper<Unity.Mathematics.int2>
	// System.Collections.Generic.ArraySortHelper<UnityEngine.CombineInstance>
	// System.Collections.Generic.ArraySortHelper<UnityEngine.Vector2>
	// System.Collections.Generic.ArraySortHelper<UnityEngine.Vector2Int>
	// System.Collections.Generic.ArraySortHelper<int>
	// System.Collections.Generic.ArraySortHelper<object>
	// System.Collections.Generic.Comparer<GameConfigs.ADConfigRowData>
	// System.Collections.Generic.Comparer<GameConfigs.AbilityLevelRowData>
	// System.Collections.Generic.Comparer<GameConfigs.ActivityTimeRowData>
	// System.Collections.Generic.Comparer<GameConfigs.AvatarRowData>
	// System.Collections.Generic.Comparer<GameConfigs.CmdConfigRowData>
	// System.Collections.Generic.Comparer<GameConfigs.EquipmentRowData>
	// System.Collections.Generic.Comparer<GameConfigs.FunctionConfigRowData>
	// System.Collections.Generic.Comparer<GameConfigs.HunterWheelRowData>
	// System.Collections.Generic.Comparer<GameConfigs.ItemRowData>
	// System.Collections.Generic.Comparer<GameConfigs.MerchantMissionRowData>
	// System.Collections.Generic.Comparer<GameConfigs.MerchantRewardRowData>
	// System.Collections.Generic.Comparer<GameConfigs.MonsterHunterRowData>
	// System.Collections.Generic.Comparer<GameConfigs.PetsRowData>
	// System.Collections.Generic.Comparer<GameConfigs.RedConfigRowData>
	// System.Collections.Generic.Comparer<GameConfigs.RoleDataRowData>
	// System.Collections.Generic.Comparer<GameConfigs.RoomExclusiveRowData>
	// System.Collections.Generic.Comparer<GameConfigs.RoomLikeRowData>
	// System.Collections.Generic.Comparer<GameConfigs.RoomMachineRowData>
	// System.Collections.Generic.Comparer<GameConfigs.RoomRowData>
	// System.Collections.Generic.Comparer<GameConfigs.RoomTechRowData>
	// System.Collections.Generic.Comparer<GameConfigs.ShopRowData>
	// System.Collections.Generic.Comparer<SDK.TDSDK.ItemInfo>
	// System.Collections.Generic.Comparer<SGame.AbilityData.AbilitLevelRenderer>
	// System.Collections.Generic.Comparer<SGame.ActiveTimeSystem.ActiveData>
	// System.Collections.Generic.Comparer<SGame.AdItem>
	// System.Collections.Generic.Comparer<SGame.BuffData>
	// System.Collections.Generic.Comparer<SGame.BulletHitSystem.Data>
	// System.Collections.Generic.Comparer<SGame.ChairData>
	// System.Collections.Generic.Comparer<SGame.CharacterDespawnSystem.EventData>
	// System.Collections.Generic.Comparer<SGame.CharacterSpawnSystem.CharacterEvent>
	// System.Collections.Generic.Comparer<SGame.DelayExcuter.Item>
	// System.Collections.Generic.Comparer<SGame.GameLoopRequest>
	// System.Collections.Generic.Comparer<SGame.GiftReward>
	// System.Collections.Generic.Comparer<SGame.ItemData.Value>
	// System.Collections.Generic.Comparer<SGame.ObjectPool.ExtendData<object>>
	// System.Collections.Generic.Comparer<SGame.RecordRoleData>
	// System.Collections.Generic.Comparer<SGame.Worker>
	// System.Collections.Generic.Comparer<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.Comparer<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.Comparer<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.Comparer<System.UIntPtr>
	// System.Collections.Generic.Comparer<Unity.Entities.Entity>
	// System.Collections.Generic.Comparer<Unity.Mathematics.int2>
	// System.Collections.Generic.Comparer<UnityEngine.CombineInstance>
	// System.Collections.Generic.Comparer<UnityEngine.Vector2>
	// System.Collections.Generic.Comparer<UnityEngine.Vector2Int>
	// System.Collections.Generic.Comparer<int>
	// System.Collections.Generic.Comparer<object>
	// System.Collections.Generic.Dictionary.Enumerator<GameConfigs.ItemRowData,object>
	// System.Collections.Generic.Dictionary.Enumerator<Unity.Entities.Entity,int>
	// System.Collections.Generic.Dictionary.Enumerator<Unity.Entities.Entity,object>
	// System.Collections.Generic.Dictionary.Enumerator<Unity.Mathematics.int2,int>
	// System.Collections.Generic.Dictionary.Enumerator<Unity.VisualScripting.EventHook,object>
	// System.Collections.Generic.Dictionary.Enumerator<int,GameConfigs.RoomTechRowData>
	// System.Collections.Generic.Dictionary.Enumerator<int,SGame.ActiveTimeSystem.TimeRange>
	// System.Collections.Generic.Dictionary.Enumerator<int,SGame.CharacterFactory.SPAWN_DATA>
	// System.Collections.Generic.Dictionary.Enumerator<int,SGame.GiftReward>
	// System.Collections.Generic.Dictionary.Enumerator<int,Unity.Entities.Entity>
	// System.Collections.Generic.Dictionary.Enumerator<int,UnityEngine.Vector2>
	// System.Collections.Generic.Dictionary.Enumerator<int,float>
	// System.Collections.Generic.Dictionary.Enumerator<int,int>
	// System.Collections.Generic.Dictionary.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.Enumerator<int,uint>
	// System.Collections.Generic.Dictionary.Enumerator<object,Unity.Entities.Entity>
	// System.Collections.Generic.Dictionary.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.Enumerator<object,uint>
	// System.Collections.Generic.Dictionary.Enumerator<uint,int>
	// System.Collections.Generic.Dictionary.Enumerator<uint,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<GameConfigs.ItemRowData,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<Unity.Entities.Entity,int>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<Unity.Entities.Entity,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<Unity.Mathematics.int2,int>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<Unity.VisualScripting.EventHook,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,GameConfigs.RoomTechRowData>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,SGame.ActiveTimeSystem.TimeRange>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,SGame.CharacterFactory.SPAWN_DATA>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,SGame.GiftReward>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,Unity.Entities.Entity>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,UnityEngine.Vector2>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,float>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,int>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,uint>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,Unity.Entities.Entity>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,uint>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<uint,int>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<uint,object>
	// System.Collections.Generic.Dictionary.KeyCollection<GameConfigs.ItemRowData,object>
	// System.Collections.Generic.Dictionary.KeyCollection<Unity.Entities.Entity,int>
	// System.Collections.Generic.Dictionary.KeyCollection<Unity.Entities.Entity,object>
	// System.Collections.Generic.Dictionary.KeyCollection<Unity.Mathematics.int2,int>
	// System.Collections.Generic.Dictionary.KeyCollection<Unity.VisualScripting.EventHook,object>
	// System.Collections.Generic.Dictionary.KeyCollection<int,GameConfigs.RoomTechRowData>
	// System.Collections.Generic.Dictionary.KeyCollection<int,SGame.ActiveTimeSystem.TimeRange>
	// System.Collections.Generic.Dictionary.KeyCollection<int,SGame.CharacterFactory.SPAWN_DATA>
	// System.Collections.Generic.Dictionary.KeyCollection<int,SGame.GiftReward>
	// System.Collections.Generic.Dictionary.KeyCollection<int,Unity.Entities.Entity>
	// System.Collections.Generic.Dictionary.KeyCollection<int,UnityEngine.Vector2>
	// System.Collections.Generic.Dictionary.KeyCollection<int,float>
	// System.Collections.Generic.Dictionary.KeyCollection<int,int>
	// System.Collections.Generic.Dictionary.KeyCollection<int,object>
	// System.Collections.Generic.Dictionary.KeyCollection<int,uint>
	// System.Collections.Generic.Dictionary.KeyCollection<object,Unity.Entities.Entity>
	// System.Collections.Generic.Dictionary.KeyCollection<object,int>
	// System.Collections.Generic.Dictionary.KeyCollection<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection<object,uint>
	// System.Collections.Generic.Dictionary.KeyCollection<uint,int>
	// System.Collections.Generic.Dictionary.KeyCollection<uint,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<GameConfigs.ItemRowData,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<Unity.Entities.Entity,int>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<Unity.Entities.Entity,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<Unity.Mathematics.int2,int>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<Unity.VisualScripting.EventHook,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,GameConfigs.RoomTechRowData>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,SGame.ActiveTimeSystem.TimeRange>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,SGame.CharacterFactory.SPAWN_DATA>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,SGame.GiftReward>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,Unity.Entities.Entity>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,UnityEngine.Vector2>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,float>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,int>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,uint>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,Unity.Entities.Entity>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,uint>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<uint,int>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<uint,object>
	// System.Collections.Generic.Dictionary.ValueCollection<GameConfigs.ItemRowData,object>
	// System.Collections.Generic.Dictionary.ValueCollection<Unity.Entities.Entity,int>
	// System.Collections.Generic.Dictionary.ValueCollection<Unity.Entities.Entity,object>
	// System.Collections.Generic.Dictionary.ValueCollection<Unity.Mathematics.int2,int>
	// System.Collections.Generic.Dictionary.ValueCollection<Unity.VisualScripting.EventHook,object>
	// System.Collections.Generic.Dictionary.ValueCollection<int,GameConfigs.RoomTechRowData>
	// System.Collections.Generic.Dictionary.ValueCollection<int,SGame.ActiveTimeSystem.TimeRange>
	// System.Collections.Generic.Dictionary.ValueCollection<int,SGame.CharacterFactory.SPAWN_DATA>
	// System.Collections.Generic.Dictionary.ValueCollection<int,SGame.GiftReward>
	// System.Collections.Generic.Dictionary.ValueCollection<int,Unity.Entities.Entity>
	// System.Collections.Generic.Dictionary.ValueCollection<int,UnityEngine.Vector2>
	// System.Collections.Generic.Dictionary.ValueCollection<int,float>
	// System.Collections.Generic.Dictionary.ValueCollection<int,int>
	// System.Collections.Generic.Dictionary.ValueCollection<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection<int,uint>
	// System.Collections.Generic.Dictionary.ValueCollection<object,Unity.Entities.Entity>
	// System.Collections.Generic.Dictionary.ValueCollection<object,int>
	// System.Collections.Generic.Dictionary.ValueCollection<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection<object,uint>
	// System.Collections.Generic.Dictionary.ValueCollection<uint,int>
	// System.Collections.Generic.Dictionary.ValueCollection<uint,object>
	// System.Collections.Generic.Dictionary<GameConfigs.ItemRowData,object>
	// System.Collections.Generic.Dictionary<Unity.Entities.Entity,int>
	// System.Collections.Generic.Dictionary<Unity.Entities.Entity,object>
	// System.Collections.Generic.Dictionary<Unity.Mathematics.int2,int>
	// System.Collections.Generic.Dictionary<Unity.VisualScripting.EventHook,object>
	// System.Collections.Generic.Dictionary<int,GameConfigs.RoomTechRowData>
	// System.Collections.Generic.Dictionary<int,SGame.ActiveTimeSystem.TimeRange>
	// System.Collections.Generic.Dictionary<int,SGame.CharacterFactory.SPAWN_DATA>
	// System.Collections.Generic.Dictionary<int,SGame.GiftReward>
	// System.Collections.Generic.Dictionary<int,Unity.Entities.Entity>
	// System.Collections.Generic.Dictionary<int,UnityEngine.Vector2>
	// System.Collections.Generic.Dictionary<int,float>
	// System.Collections.Generic.Dictionary<int,int>
	// System.Collections.Generic.Dictionary<int,object>
	// System.Collections.Generic.Dictionary<int,uint>
	// System.Collections.Generic.Dictionary<object,Unity.Entities.Entity>
	// System.Collections.Generic.Dictionary<object,int>
	// System.Collections.Generic.Dictionary<object,object>
	// System.Collections.Generic.Dictionary<object,uint>
	// System.Collections.Generic.Dictionary<uint,int>
	// System.Collections.Generic.Dictionary<uint,object>
	// System.Collections.Generic.EqualityComparer<GameConfigs.ItemRowData>
	// System.Collections.Generic.EqualityComparer<GameConfigs.RoomTechRowData>
	// System.Collections.Generic.EqualityComparer<SGame.ActiveTimeSystem.TimeRange>
	// System.Collections.Generic.EqualityComparer<SGame.CharacterFactory.SPAWN_DATA>
	// System.Collections.Generic.EqualityComparer<SGame.GiftReward>
	// System.Collections.Generic.EqualityComparer<Unity.Entities.Entity>
	// System.Collections.Generic.EqualityComparer<Unity.Mathematics.int2>
	// System.Collections.Generic.EqualityComparer<Unity.VisualScripting.EventHook>
	// System.Collections.Generic.EqualityComparer<UnityEngine.Vector2>
	// System.Collections.Generic.EqualityComparer<float>
	// System.Collections.Generic.EqualityComparer<int>
	// System.Collections.Generic.EqualityComparer<object>
	// System.Collections.Generic.EqualityComparer<uint>
	// System.Collections.Generic.HashSet.Enumerator<int>
	// System.Collections.Generic.HashSet.Enumerator<object>
	// System.Collections.Generic.HashSet<int>
	// System.Collections.Generic.HashSet<object>
	// System.Collections.Generic.HashSetEqualityComparer<int>
	// System.Collections.Generic.HashSetEqualityComparer<object>
	// System.Collections.Generic.ICollection<GameConfigs.ADConfigRowData>
	// System.Collections.Generic.ICollection<GameConfigs.AbilityLevelRowData>
	// System.Collections.Generic.ICollection<GameConfigs.ActivityTimeRowData>
	// System.Collections.Generic.ICollection<GameConfigs.AvatarRowData>
	// System.Collections.Generic.ICollection<GameConfigs.CmdConfigRowData>
	// System.Collections.Generic.ICollection<GameConfigs.EquipmentRowData>
	// System.Collections.Generic.ICollection<GameConfigs.FunctionConfigRowData>
	// System.Collections.Generic.ICollection<GameConfigs.HunterWheelRowData>
	// System.Collections.Generic.ICollection<GameConfigs.ItemRowData>
	// System.Collections.Generic.ICollection<GameConfigs.MerchantMissionRowData>
	// System.Collections.Generic.ICollection<GameConfigs.MerchantRewardRowData>
	// System.Collections.Generic.ICollection<GameConfigs.MonsterHunterRowData>
	// System.Collections.Generic.ICollection<GameConfigs.PetsRowData>
	// System.Collections.Generic.ICollection<GameConfigs.RedConfigRowData>
	// System.Collections.Generic.ICollection<GameConfigs.RoleDataRowData>
	// System.Collections.Generic.ICollection<GameConfigs.RoomExclusiveRowData>
	// System.Collections.Generic.ICollection<GameConfigs.RoomLikeRowData>
	// System.Collections.Generic.ICollection<GameConfigs.RoomMachineRowData>
	// System.Collections.Generic.ICollection<GameConfigs.RoomRowData>
	// System.Collections.Generic.ICollection<GameConfigs.RoomTechRowData>
	// System.Collections.Generic.ICollection<GameConfigs.ShopRowData>
	// System.Collections.Generic.ICollection<SDK.TDSDK.ItemInfo>
	// System.Collections.Generic.ICollection<SGame.AbilityData.AbilitLevelRenderer>
	// System.Collections.Generic.ICollection<SGame.ActiveTimeSystem.ActiveData>
	// System.Collections.Generic.ICollection<SGame.AdItem>
	// System.Collections.Generic.ICollection<SGame.BuffData>
	// System.Collections.Generic.ICollection<SGame.BulletHitSystem.Data>
	// System.Collections.Generic.ICollection<SGame.ChairData>
	// System.Collections.Generic.ICollection<SGame.CharacterDespawnSystem.EventData>
	// System.Collections.Generic.ICollection<SGame.CharacterSpawnSystem.CharacterEvent>
	// System.Collections.Generic.ICollection<SGame.DelayExcuter.Item>
	// System.Collections.Generic.ICollection<SGame.GameLoopRequest>
	// System.Collections.Generic.ICollection<SGame.GiftReward>
	// System.Collections.Generic.ICollection<SGame.ItemData.Value>
	// System.Collections.Generic.ICollection<SGame.ObjectPool.ExtendData<object>>
	// System.Collections.Generic.ICollection<SGame.RecordRoleData>
	// System.Collections.Generic.ICollection<SGame.Worker>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<GameConfigs.ItemRowData,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<Unity.Entities.Entity,int>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<Unity.Entities.Entity,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<Unity.Mathematics.int2,int>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<Unity.VisualScripting.EventHook,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,GameConfigs.RoomTechRowData>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,SGame.ActiveTimeSystem.TimeRange>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,SGame.CharacterFactory.SPAWN_DATA>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,SGame.GiftReward>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,Unity.Entities.Entity>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,UnityEngine.Vector2>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,float>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,uint>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,Unity.Entities.Entity>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,uint>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<uint,int>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<uint,object>>
	// System.Collections.Generic.ICollection<System.UIntPtr>
	// System.Collections.Generic.ICollection<Unity.Entities.Entity>
	// System.Collections.Generic.ICollection<Unity.Mathematics.int2>
	// System.Collections.Generic.ICollection<UnityEngine.CombineInstance>
	// System.Collections.Generic.ICollection<UnityEngine.Vector2>
	// System.Collections.Generic.ICollection<UnityEngine.Vector2Int>
	// System.Collections.Generic.ICollection<byte>
	// System.Collections.Generic.ICollection<float>
	// System.Collections.Generic.ICollection<int>
	// System.Collections.Generic.ICollection<object>
	// System.Collections.Generic.IComparer<GameConfigs.ADConfigRowData>
	// System.Collections.Generic.IComparer<GameConfigs.AbilityLevelRowData>
	// System.Collections.Generic.IComparer<GameConfigs.ActivityTimeRowData>
	// System.Collections.Generic.IComparer<GameConfigs.AvatarRowData>
	// System.Collections.Generic.IComparer<GameConfigs.CmdConfigRowData>
	// System.Collections.Generic.IComparer<GameConfigs.EquipmentRowData>
	// System.Collections.Generic.IComparer<GameConfigs.FunctionConfigRowData>
	// System.Collections.Generic.IComparer<GameConfigs.HunterWheelRowData>
	// System.Collections.Generic.IComparer<GameConfigs.ItemRowData>
	// System.Collections.Generic.IComparer<GameConfigs.MerchantMissionRowData>
	// System.Collections.Generic.IComparer<GameConfigs.MerchantRewardRowData>
	// System.Collections.Generic.IComparer<GameConfigs.MonsterHunterRowData>
	// System.Collections.Generic.IComparer<GameConfigs.PetsRowData>
	// System.Collections.Generic.IComparer<GameConfigs.RedConfigRowData>
	// System.Collections.Generic.IComparer<GameConfigs.RoleDataRowData>
	// System.Collections.Generic.IComparer<GameConfigs.RoomExclusiveRowData>
	// System.Collections.Generic.IComparer<GameConfigs.RoomLikeRowData>
	// System.Collections.Generic.IComparer<GameConfigs.RoomMachineRowData>
	// System.Collections.Generic.IComparer<GameConfigs.RoomRowData>
	// System.Collections.Generic.IComparer<GameConfigs.RoomTechRowData>
	// System.Collections.Generic.IComparer<GameConfigs.ShopRowData>
	// System.Collections.Generic.IComparer<SDK.TDSDK.ItemInfo>
	// System.Collections.Generic.IComparer<SGame.AbilityData.AbilitLevelRenderer>
	// System.Collections.Generic.IComparer<SGame.ActiveTimeSystem.ActiveData>
	// System.Collections.Generic.IComparer<SGame.AdItem>
	// System.Collections.Generic.IComparer<SGame.BuffData>
	// System.Collections.Generic.IComparer<SGame.BulletHitSystem.Data>
	// System.Collections.Generic.IComparer<SGame.ChairData>
	// System.Collections.Generic.IComparer<SGame.CharacterDespawnSystem.EventData>
	// System.Collections.Generic.IComparer<SGame.CharacterSpawnSystem.CharacterEvent>
	// System.Collections.Generic.IComparer<SGame.DelayExcuter.Item>
	// System.Collections.Generic.IComparer<SGame.GameLoopRequest>
	// System.Collections.Generic.IComparer<SGame.GiftReward>
	// System.Collections.Generic.IComparer<SGame.ItemData.Value>
	// System.Collections.Generic.IComparer<SGame.ObjectPool.ExtendData<object>>
	// System.Collections.Generic.IComparer<SGame.RecordRoleData>
	// System.Collections.Generic.IComparer<SGame.Worker>
	// System.Collections.Generic.IComparer<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.IComparer<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IComparer<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IComparer<System.UIntPtr>
	// System.Collections.Generic.IComparer<Unity.Entities.Entity>
	// System.Collections.Generic.IComparer<Unity.Mathematics.int2>
	// System.Collections.Generic.IComparer<UnityEngine.CombineInstance>
	// System.Collections.Generic.IComparer<UnityEngine.Vector2>
	// System.Collections.Generic.IComparer<UnityEngine.Vector2Int>
	// System.Collections.Generic.IComparer<int>
	// System.Collections.Generic.IComparer<object>
	// System.Collections.Generic.IDictionary<int,object>
	// System.Collections.Generic.IDictionary<object,LitJson.ArrayMetadata>
	// System.Collections.Generic.IDictionary<object,LitJson.ObjectMetadata>
	// System.Collections.Generic.IDictionary<object,LitJson.PropertyMetadata>
	// System.Collections.Generic.IDictionary<object,object>
	// System.Collections.Generic.IEnumerable<GameConfigs.ADConfigRowData>
	// System.Collections.Generic.IEnumerable<GameConfigs.AbilityLevelRowData>
	// System.Collections.Generic.IEnumerable<GameConfigs.ActivityTimeRowData>
	// System.Collections.Generic.IEnumerable<GameConfigs.AvatarRowData>
	// System.Collections.Generic.IEnumerable<GameConfigs.CmdConfigRowData>
	// System.Collections.Generic.IEnumerable<GameConfigs.EquipmentRowData>
	// System.Collections.Generic.IEnumerable<GameConfigs.FunctionConfigRowData>
	// System.Collections.Generic.IEnumerable<GameConfigs.HunterWheelRowData>
	// System.Collections.Generic.IEnumerable<GameConfigs.ItemRowData>
	// System.Collections.Generic.IEnumerable<GameConfigs.MerchantMissionRowData>
	// System.Collections.Generic.IEnumerable<GameConfigs.MerchantRewardRowData>
	// System.Collections.Generic.IEnumerable<GameConfigs.MonsterHunterRowData>
	// System.Collections.Generic.IEnumerable<GameConfigs.PetsRowData>
	// System.Collections.Generic.IEnumerable<GameConfigs.RedConfigRowData>
	// System.Collections.Generic.IEnumerable<GameConfigs.RoleDataRowData>
	// System.Collections.Generic.IEnumerable<GameConfigs.RoomExclusiveRowData>
	// System.Collections.Generic.IEnumerable<GameConfigs.RoomLikeRowData>
	// System.Collections.Generic.IEnumerable<GameConfigs.RoomMachineRowData>
	// System.Collections.Generic.IEnumerable<GameConfigs.RoomRowData>
	// System.Collections.Generic.IEnumerable<GameConfigs.RoomTechRowData>
	// System.Collections.Generic.IEnumerable<GameConfigs.ShopRowData>
	// System.Collections.Generic.IEnumerable<SDK.TDSDK.ItemInfo>
	// System.Collections.Generic.IEnumerable<SGame.AbilityData.AbilitLevelRenderer>
	// System.Collections.Generic.IEnumerable<SGame.ActiveTimeSystem.ActiveData>
	// System.Collections.Generic.IEnumerable<SGame.AdItem>
	// System.Collections.Generic.IEnumerable<SGame.BuffData>
	// System.Collections.Generic.IEnumerable<SGame.BulletHitSystem.Data>
	// System.Collections.Generic.IEnumerable<SGame.ChairData>
	// System.Collections.Generic.IEnumerable<SGame.CharacterDespawnSystem.EventData>
	// System.Collections.Generic.IEnumerable<SGame.CharacterSpawnSystem.CharacterEvent>
	// System.Collections.Generic.IEnumerable<SGame.DelayExcuter.Item>
	// System.Collections.Generic.IEnumerable<SGame.GameLoopRequest>
	// System.Collections.Generic.IEnumerable<SGame.GiftReward>
	// System.Collections.Generic.IEnumerable<SGame.ItemData.Value>
	// System.Collections.Generic.IEnumerable<SGame.ObjectPool.ExtendData<object>>
	// System.Collections.Generic.IEnumerable<SGame.RecordRoleData>
	// System.Collections.Generic.IEnumerable<SGame.Worker>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<GameConfigs.ItemRowData,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<Unity.Entities.Entity,int>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<Unity.Entities.Entity,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<Unity.Mathematics.int2,int>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<Unity.VisualScripting.EventHook,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,GameConfigs.RoomTechRowData>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,SGame.ActiveTimeSystem.TimeRange>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,SGame.CharacterFactory.SPAWN_DATA>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,SGame.GiftReward>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,Unity.Entities.Entity>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,UnityEngine.Vector2>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,float>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,uint>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,Unity.Entities.Entity>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,uint>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<uint,int>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<uint,object>>
	// System.Collections.Generic.IEnumerable<System.UIntPtr>
	// System.Collections.Generic.IEnumerable<Unity.Entities.Entity>
	// System.Collections.Generic.IEnumerable<Unity.Mathematics.int2>
	// System.Collections.Generic.IEnumerable<UnityEngine.CombineInstance>
	// System.Collections.Generic.IEnumerable<UnityEngine.Vector2>
	// System.Collections.Generic.IEnumerable<UnityEngine.Vector2Int>
	// System.Collections.Generic.IEnumerable<double>
	// System.Collections.Generic.IEnumerable<int>
	// System.Collections.Generic.IEnumerable<object>
	// System.Collections.Generic.IEnumerator<GameConfigs.ADConfigRowData>
	// System.Collections.Generic.IEnumerator<GameConfigs.AbilityLevelRowData>
	// System.Collections.Generic.IEnumerator<GameConfigs.ActivityTimeRowData>
	// System.Collections.Generic.IEnumerator<GameConfigs.AvatarRowData>
	// System.Collections.Generic.IEnumerator<GameConfigs.CmdConfigRowData>
	// System.Collections.Generic.IEnumerator<GameConfigs.EquipmentRowData>
	// System.Collections.Generic.IEnumerator<GameConfigs.FunctionConfigRowData>
	// System.Collections.Generic.IEnumerator<GameConfigs.HunterWheelRowData>
	// System.Collections.Generic.IEnumerator<GameConfigs.ItemRowData>
	// System.Collections.Generic.IEnumerator<GameConfigs.MerchantMissionRowData>
	// System.Collections.Generic.IEnumerator<GameConfigs.MerchantRewardRowData>
	// System.Collections.Generic.IEnumerator<GameConfigs.MonsterHunterRowData>
	// System.Collections.Generic.IEnumerator<GameConfigs.PetsRowData>
	// System.Collections.Generic.IEnumerator<GameConfigs.RedConfigRowData>
	// System.Collections.Generic.IEnumerator<GameConfigs.RoleDataRowData>
	// System.Collections.Generic.IEnumerator<GameConfigs.RoomExclusiveRowData>
	// System.Collections.Generic.IEnumerator<GameConfigs.RoomLikeRowData>
	// System.Collections.Generic.IEnumerator<GameConfigs.RoomMachineRowData>
	// System.Collections.Generic.IEnumerator<GameConfigs.RoomRowData>
	// System.Collections.Generic.IEnumerator<GameConfigs.RoomTechRowData>
	// System.Collections.Generic.IEnumerator<GameConfigs.ShopRowData>
	// System.Collections.Generic.IEnumerator<NetPackage>
	// System.Collections.Generic.IEnumerator<SDK.TDSDK.ItemInfo>
	// System.Collections.Generic.IEnumerator<SGame.AbilityData.AbilitLevelRenderer>
	// System.Collections.Generic.IEnumerator<SGame.ActiveTimeSystem.ActiveData>
	// System.Collections.Generic.IEnumerator<SGame.AdItem>
	// System.Collections.Generic.IEnumerator<SGame.BuffData>
	// System.Collections.Generic.IEnumerator<SGame.BulletHitSystem.Data>
	// System.Collections.Generic.IEnumerator<SGame.ChairData>
	// System.Collections.Generic.IEnumerator<SGame.CharacterDespawnSystem.EventData>
	// System.Collections.Generic.IEnumerator<SGame.CharacterSpawnSystem.CharacterEvent>
	// System.Collections.Generic.IEnumerator<SGame.DelayExcuter.Item>
	// System.Collections.Generic.IEnumerator<SGame.GameLoopRequest>
	// System.Collections.Generic.IEnumerator<SGame.GiftReward>
	// System.Collections.Generic.IEnumerator<SGame.ItemData.Value>
	// System.Collections.Generic.IEnumerator<SGame.ObjectPool.ExtendData<object>>
	// System.Collections.Generic.IEnumerator<SGame.RecordRoleData>
	// System.Collections.Generic.IEnumerator<SGame.Worker>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<GameConfigs.ItemRowData,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<Unity.Entities.Entity,int>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<Unity.Entities.Entity,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<Unity.Mathematics.int2,int>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<Unity.VisualScripting.EventHook,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,GameConfigs.RoomTechRowData>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,SGame.ActiveTimeSystem.TimeRange>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,SGame.CharacterFactory.SPAWN_DATA>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,SGame.GiftReward>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,Unity.Entities.Entity>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,UnityEngine.Vector2>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,float>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,uint>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,Unity.Entities.Entity>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,uint>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<uint,int>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<uint,object>>
	// System.Collections.Generic.IEnumerator<System.UIntPtr>
	// System.Collections.Generic.IEnumerator<Unity.Entities.Entity>
	// System.Collections.Generic.IEnumerator<Unity.Mathematics.int2>
	// System.Collections.Generic.IEnumerator<UnityEngine.CombineInstance>
	// System.Collections.Generic.IEnumerator<UnityEngine.Vector2>
	// System.Collections.Generic.IEnumerator<UnityEngine.Vector2Int>
	// System.Collections.Generic.IEnumerator<double>
	// System.Collections.Generic.IEnumerator<int>
	// System.Collections.Generic.IEnumerator<object>
	// System.Collections.Generic.IEqualityComparer<GameConfigs.ItemRowData>
	// System.Collections.Generic.IEqualityComparer<Unity.Entities.Entity>
	// System.Collections.Generic.IEqualityComparer<Unity.Mathematics.int2>
	// System.Collections.Generic.IEqualityComparer<Unity.VisualScripting.EventHook>
	// System.Collections.Generic.IEqualityComparer<int>
	// System.Collections.Generic.IEqualityComparer<object>
	// System.Collections.Generic.IEqualityComparer<uint>
	// System.Collections.Generic.IList<GameConfigs.ADConfigRowData>
	// System.Collections.Generic.IList<GameConfigs.AbilityLevelRowData>
	// System.Collections.Generic.IList<GameConfigs.ActivityTimeRowData>
	// System.Collections.Generic.IList<GameConfigs.AvatarRowData>
	// System.Collections.Generic.IList<GameConfigs.CmdConfigRowData>
	// System.Collections.Generic.IList<GameConfigs.EquipmentRowData>
	// System.Collections.Generic.IList<GameConfigs.FunctionConfigRowData>
	// System.Collections.Generic.IList<GameConfigs.HunterWheelRowData>
	// System.Collections.Generic.IList<GameConfigs.ItemRowData>
	// System.Collections.Generic.IList<GameConfigs.MerchantMissionRowData>
	// System.Collections.Generic.IList<GameConfigs.MerchantRewardRowData>
	// System.Collections.Generic.IList<GameConfigs.MonsterHunterRowData>
	// System.Collections.Generic.IList<GameConfigs.PetsRowData>
	// System.Collections.Generic.IList<GameConfigs.RedConfigRowData>
	// System.Collections.Generic.IList<GameConfigs.RoleDataRowData>
	// System.Collections.Generic.IList<GameConfigs.RoomExclusiveRowData>
	// System.Collections.Generic.IList<GameConfigs.RoomLikeRowData>
	// System.Collections.Generic.IList<GameConfigs.RoomMachineRowData>
	// System.Collections.Generic.IList<GameConfigs.RoomRowData>
	// System.Collections.Generic.IList<GameConfigs.RoomTechRowData>
	// System.Collections.Generic.IList<GameConfigs.ShopRowData>
	// System.Collections.Generic.IList<SDK.TDSDK.ItemInfo>
	// System.Collections.Generic.IList<SGame.AbilityData.AbilitLevelRenderer>
	// System.Collections.Generic.IList<SGame.ActiveTimeSystem.ActiveData>
	// System.Collections.Generic.IList<SGame.AdItem>
	// System.Collections.Generic.IList<SGame.BuffData>
	// System.Collections.Generic.IList<SGame.BulletHitSystem.Data>
	// System.Collections.Generic.IList<SGame.ChairData>
	// System.Collections.Generic.IList<SGame.CharacterDespawnSystem.EventData>
	// System.Collections.Generic.IList<SGame.CharacterSpawnSystem.CharacterEvent>
	// System.Collections.Generic.IList<SGame.DelayExcuter.Item>
	// System.Collections.Generic.IList<SGame.GameLoopRequest>
	// System.Collections.Generic.IList<SGame.GiftReward>
	// System.Collections.Generic.IList<SGame.ItemData.Value>
	// System.Collections.Generic.IList<SGame.ObjectPool.ExtendData<object>>
	// System.Collections.Generic.IList<SGame.RecordRoleData>
	// System.Collections.Generic.IList<SGame.Worker>
	// System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IList<System.UIntPtr>
	// System.Collections.Generic.IList<Unity.Entities.Entity>
	// System.Collections.Generic.IList<Unity.Mathematics.int2>
	// System.Collections.Generic.IList<UnityEngine.CombineInstance>
	// System.Collections.Generic.IList<UnityEngine.Vector2>
	// System.Collections.Generic.IList<UnityEngine.Vector2Int>
	// System.Collections.Generic.IList<byte>
	// System.Collections.Generic.IList<float>
	// System.Collections.Generic.IList<int>
	// System.Collections.Generic.IList<object>
	// System.Collections.Generic.IReadOnlyCollection<object>
	// System.Collections.Generic.KeyValuePair<GameConfigs.ItemRowData,object>
	// System.Collections.Generic.KeyValuePair<Unity.Entities.Entity,int>
	// System.Collections.Generic.KeyValuePair<Unity.Entities.Entity,object>
	// System.Collections.Generic.KeyValuePair<Unity.Mathematics.int2,int>
	// System.Collections.Generic.KeyValuePair<Unity.VisualScripting.EventHook,object>
	// System.Collections.Generic.KeyValuePair<int,GameConfigs.RoomTechRowData>
	// System.Collections.Generic.KeyValuePair<int,SGame.ActiveTimeSystem.TimeRange>
	// System.Collections.Generic.KeyValuePair<int,SGame.CharacterFactory.SPAWN_DATA>
	// System.Collections.Generic.KeyValuePair<int,SGame.GiftReward>
	// System.Collections.Generic.KeyValuePair<int,Unity.Entities.Entity>
	// System.Collections.Generic.KeyValuePair<int,UnityEngine.Vector2>
	// System.Collections.Generic.KeyValuePair<int,float>
	// System.Collections.Generic.KeyValuePair<int,int>
	// System.Collections.Generic.KeyValuePair<int,object>
	// System.Collections.Generic.KeyValuePair<int,uint>
	// System.Collections.Generic.KeyValuePair<object,Unity.Entities.Entity>
	// System.Collections.Generic.KeyValuePair<object,int>
	// System.Collections.Generic.KeyValuePair<object,object>
	// System.Collections.Generic.KeyValuePair<object,uint>
	// System.Collections.Generic.KeyValuePair<uint,int>
	// System.Collections.Generic.KeyValuePair<uint,object>
	// System.Collections.Generic.List.Enumerator<GameConfigs.ADConfigRowData>
	// System.Collections.Generic.List.Enumerator<GameConfigs.AbilityLevelRowData>
	// System.Collections.Generic.List.Enumerator<GameConfigs.ActivityTimeRowData>
	// System.Collections.Generic.List.Enumerator<GameConfigs.AvatarRowData>
	// System.Collections.Generic.List.Enumerator<GameConfigs.CmdConfigRowData>
	// System.Collections.Generic.List.Enumerator<GameConfigs.EquipmentRowData>
	// System.Collections.Generic.List.Enumerator<GameConfigs.FunctionConfigRowData>
	// System.Collections.Generic.List.Enumerator<GameConfigs.HunterWheelRowData>
	// System.Collections.Generic.List.Enumerator<GameConfigs.ItemRowData>
	// System.Collections.Generic.List.Enumerator<GameConfigs.MerchantMissionRowData>
	// System.Collections.Generic.List.Enumerator<GameConfigs.MerchantRewardRowData>
	// System.Collections.Generic.List.Enumerator<GameConfigs.MonsterHunterRowData>
	// System.Collections.Generic.List.Enumerator<GameConfigs.PetsRowData>
	// System.Collections.Generic.List.Enumerator<GameConfigs.RedConfigRowData>
	// System.Collections.Generic.List.Enumerator<GameConfigs.RoleDataRowData>
	// System.Collections.Generic.List.Enumerator<GameConfigs.RoomExclusiveRowData>
	// System.Collections.Generic.List.Enumerator<GameConfigs.RoomLikeRowData>
	// System.Collections.Generic.List.Enumerator<GameConfigs.RoomMachineRowData>
	// System.Collections.Generic.List.Enumerator<GameConfigs.RoomRowData>
	// System.Collections.Generic.List.Enumerator<GameConfigs.RoomTechRowData>
	// System.Collections.Generic.List.Enumerator<GameConfigs.ShopRowData>
	// System.Collections.Generic.List.Enumerator<SDK.TDSDK.ItemInfo>
	// System.Collections.Generic.List.Enumerator<SGame.AbilityData.AbilitLevelRenderer>
	// System.Collections.Generic.List.Enumerator<SGame.ActiveTimeSystem.ActiveData>
	// System.Collections.Generic.List.Enumerator<SGame.AdItem>
	// System.Collections.Generic.List.Enumerator<SGame.BuffData>
	// System.Collections.Generic.List.Enumerator<SGame.BulletHitSystem.Data>
	// System.Collections.Generic.List.Enumerator<SGame.ChairData>
	// System.Collections.Generic.List.Enumerator<SGame.CharacterDespawnSystem.EventData>
	// System.Collections.Generic.List.Enumerator<SGame.CharacterSpawnSystem.CharacterEvent>
	// System.Collections.Generic.List.Enumerator<SGame.DelayExcuter.Item>
	// System.Collections.Generic.List.Enumerator<SGame.GameLoopRequest>
	// System.Collections.Generic.List.Enumerator<SGame.GiftReward>
	// System.Collections.Generic.List.Enumerator<SGame.ItemData.Value>
	// System.Collections.Generic.List.Enumerator<SGame.ObjectPool.ExtendData<object>>
	// System.Collections.Generic.List.Enumerator<SGame.RecordRoleData>
	// System.Collections.Generic.List.Enumerator<SGame.Worker>
	// System.Collections.Generic.List.Enumerator<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.List.Enumerator<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.List.Enumerator<System.UIntPtr>
	// System.Collections.Generic.List.Enumerator<Unity.Entities.Entity>
	// System.Collections.Generic.List.Enumerator<Unity.Mathematics.int2>
	// System.Collections.Generic.List.Enumerator<UnityEngine.CombineInstance>
	// System.Collections.Generic.List.Enumerator<UnityEngine.Vector2>
	// System.Collections.Generic.List.Enumerator<UnityEngine.Vector2Int>
	// System.Collections.Generic.List.Enumerator<int>
	// System.Collections.Generic.List.Enumerator<object>
	// System.Collections.Generic.List<GameConfigs.ADConfigRowData>
	// System.Collections.Generic.List<GameConfigs.AbilityLevelRowData>
	// System.Collections.Generic.List<GameConfigs.ActivityTimeRowData>
	// System.Collections.Generic.List<GameConfigs.AvatarRowData>
	// System.Collections.Generic.List<GameConfigs.CmdConfigRowData>
	// System.Collections.Generic.List<GameConfigs.EquipmentRowData>
	// System.Collections.Generic.List<GameConfigs.FunctionConfigRowData>
	// System.Collections.Generic.List<GameConfigs.HunterWheelRowData>
	// System.Collections.Generic.List<GameConfigs.ItemRowData>
	// System.Collections.Generic.List<GameConfigs.MerchantMissionRowData>
	// System.Collections.Generic.List<GameConfigs.MerchantRewardRowData>
	// System.Collections.Generic.List<GameConfigs.MonsterHunterRowData>
	// System.Collections.Generic.List<GameConfigs.PetsRowData>
	// System.Collections.Generic.List<GameConfigs.RedConfigRowData>
	// System.Collections.Generic.List<GameConfigs.RoleDataRowData>
	// System.Collections.Generic.List<GameConfigs.RoomExclusiveRowData>
	// System.Collections.Generic.List<GameConfigs.RoomLikeRowData>
	// System.Collections.Generic.List<GameConfigs.RoomMachineRowData>
	// System.Collections.Generic.List<GameConfigs.RoomRowData>
	// System.Collections.Generic.List<GameConfigs.RoomTechRowData>
	// System.Collections.Generic.List<GameConfigs.ShopRowData>
	// System.Collections.Generic.List<SDK.TDSDK.ItemInfo>
	// System.Collections.Generic.List<SGame.AbilityData.AbilitLevelRenderer>
	// System.Collections.Generic.List<SGame.ActiveTimeSystem.ActiveData>
	// System.Collections.Generic.List<SGame.AdItem>
	// System.Collections.Generic.List<SGame.BuffData>
	// System.Collections.Generic.List<SGame.BulletHitSystem.Data>
	// System.Collections.Generic.List<SGame.ChairData>
	// System.Collections.Generic.List<SGame.CharacterDespawnSystem.EventData>
	// System.Collections.Generic.List<SGame.CharacterSpawnSystem.CharacterEvent>
	// System.Collections.Generic.List<SGame.DelayExcuter.Item>
	// System.Collections.Generic.List<SGame.GameLoopRequest>
	// System.Collections.Generic.List<SGame.GiftReward>
	// System.Collections.Generic.List<SGame.ItemData.Value>
	// System.Collections.Generic.List<SGame.ObjectPool.ExtendData<object>>
	// System.Collections.Generic.List<SGame.RecordRoleData>
	// System.Collections.Generic.List<SGame.Worker>
	// System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.List<System.UIntPtr>
	// System.Collections.Generic.List<Unity.Entities.Entity>
	// System.Collections.Generic.List<Unity.Mathematics.int2>
	// System.Collections.Generic.List<UnityEngine.CombineInstance>
	// System.Collections.Generic.List<UnityEngine.Vector2>
	// System.Collections.Generic.List<UnityEngine.Vector2Int>
	// System.Collections.Generic.List<int>
	// System.Collections.Generic.List<object>
	// System.Collections.Generic.ObjectComparer<GameConfigs.ADConfigRowData>
	// System.Collections.Generic.ObjectComparer<GameConfigs.AbilityLevelRowData>
	// System.Collections.Generic.ObjectComparer<GameConfigs.ActivityTimeRowData>
	// System.Collections.Generic.ObjectComparer<GameConfigs.AvatarRowData>
	// System.Collections.Generic.ObjectComparer<GameConfigs.CmdConfigRowData>
	// System.Collections.Generic.ObjectComparer<GameConfigs.EquipmentRowData>
	// System.Collections.Generic.ObjectComparer<GameConfigs.FunctionConfigRowData>
	// System.Collections.Generic.ObjectComparer<GameConfigs.HunterWheelRowData>
	// System.Collections.Generic.ObjectComparer<GameConfigs.ItemRowData>
	// System.Collections.Generic.ObjectComparer<GameConfigs.MerchantMissionRowData>
	// System.Collections.Generic.ObjectComparer<GameConfigs.MerchantRewardRowData>
	// System.Collections.Generic.ObjectComparer<GameConfigs.MonsterHunterRowData>
	// System.Collections.Generic.ObjectComparer<GameConfigs.PetsRowData>
	// System.Collections.Generic.ObjectComparer<GameConfigs.RedConfigRowData>
	// System.Collections.Generic.ObjectComparer<GameConfigs.RoleDataRowData>
	// System.Collections.Generic.ObjectComparer<GameConfigs.RoomExclusiveRowData>
	// System.Collections.Generic.ObjectComparer<GameConfigs.RoomLikeRowData>
	// System.Collections.Generic.ObjectComparer<GameConfigs.RoomMachineRowData>
	// System.Collections.Generic.ObjectComparer<GameConfigs.RoomRowData>
	// System.Collections.Generic.ObjectComparer<GameConfigs.RoomTechRowData>
	// System.Collections.Generic.ObjectComparer<GameConfigs.ShopRowData>
	// System.Collections.Generic.ObjectComparer<SDK.TDSDK.ItemInfo>
	// System.Collections.Generic.ObjectComparer<SGame.AbilityData.AbilitLevelRenderer>
	// System.Collections.Generic.ObjectComparer<SGame.ActiveTimeSystem.ActiveData>
	// System.Collections.Generic.ObjectComparer<SGame.AdItem>
	// System.Collections.Generic.ObjectComparer<SGame.BuffData>
	// System.Collections.Generic.ObjectComparer<SGame.BulletHitSystem.Data>
	// System.Collections.Generic.ObjectComparer<SGame.ChairData>
	// System.Collections.Generic.ObjectComparer<SGame.CharacterDespawnSystem.EventData>
	// System.Collections.Generic.ObjectComparer<SGame.CharacterSpawnSystem.CharacterEvent>
	// System.Collections.Generic.ObjectComparer<SGame.DelayExcuter.Item>
	// System.Collections.Generic.ObjectComparer<SGame.GameLoopRequest>
	// System.Collections.Generic.ObjectComparer<SGame.GiftReward>
	// System.Collections.Generic.ObjectComparer<SGame.ItemData.Value>
	// System.Collections.Generic.ObjectComparer<SGame.ObjectPool.ExtendData<object>>
	// System.Collections.Generic.ObjectComparer<SGame.RecordRoleData>
	// System.Collections.Generic.ObjectComparer<SGame.Worker>
	// System.Collections.Generic.ObjectComparer<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.ObjectComparer<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.ObjectComparer<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.ObjectComparer<System.UIntPtr>
	// System.Collections.Generic.ObjectComparer<Unity.Entities.Entity>
	// System.Collections.Generic.ObjectComparer<Unity.Mathematics.int2>
	// System.Collections.Generic.ObjectComparer<UnityEngine.CombineInstance>
	// System.Collections.Generic.ObjectComparer<UnityEngine.Vector2>
	// System.Collections.Generic.ObjectComparer<UnityEngine.Vector2Int>
	// System.Collections.Generic.ObjectComparer<int>
	// System.Collections.Generic.ObjectComparer<object>
	// System.Collections.Generic.ObjectEqualityComparer<GameConfigs.ItemRowData>
	// System.Collections.Generic.ObjectEqualityComparer<GameConfigs.RoomTechRowData>
	// System.Collections.Generic.ObjectEqualityComparer<SGame.ActiveTimeSystem.TimeRange>
	// System.Collections.Generic.ObjectEqualityComparer<SGame.CharacterFactory.SPAWN_DATA>
	// System.Collections.Generic.ObjectEqualityComparer<SGame.GiftReward>
	// System.Collections.Generic.ObjectEqualityComparer<Unity.Entities.Entity>
	// System.Collections.Generic.ObjectEqualityComparer<Unity.Mathematics.int2>
	// System.Collections.Generic.ObjectEqualityComparer<Unity.VisualScripting.EventHook>
	// System.Collections.Generic.ObjectEqualityComparer<UnityEngine.Vector2>
	// System.Collections.Generic.ObjectEqualityComparer<float>
	// System.Collections.Generic.ObjectEqualityComparer<int>
	// System.Collections.Generic.ObjectEqualityComparer<object>
	// System.Collections.Generic.ObjectEqualityComparer<uint>
	// System.Collections.Generic.Queue.Enumerator<SGame.ChairData>
	// System.Collections.Generic.Queue.Enumerator<SGame.ItemData.Value>
	// System.Collections.Generic.Queue<SGame.ChairData>
	// System.Collections.Generic.Queue<SGame.ItemData.Value>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_0<int,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_1<int,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<int,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass5_0<int,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass6_0<int,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.Enumerator<int,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection<int,object>
	// System.Collections.Generic.SortedDictionary.KeyValuePairComparer<int,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass5_0<int,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass6_0<int,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.Enumerator<int,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection<int,object>
	// System.Collections.Generic.SortedDictionary<int,object>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass52_0<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass53_0<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.SortedSet.Enumerator<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.SortedSet.Node<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.SortedSet<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.Stack.Enumerator<int>
	// System.Collections.Generic.Stack.Enumerator<object>
	// System.Collections.Generic.Stack<int>
	// System.Collections.Generic.Stack<object>
	// System.Collections.Generic.TreeSet<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.TreeWalkPredicate<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.ObjectModel.ReadOnlyCollection<GameConfigs.ADConfigRowData>
	// System.Collections.ObjectModel.ReadOnlyCollection<GameConfigs.AbilityLevelRowData>
	// System.Collections.ObjectModel.ReadOnlyCollection<GameConfigs.ActivityTimeRowData>
	// System.Collections.ObjectModel.ReadOnlyCollection<GameConfigs.AvatarRowData>
	// System.Collections.ObjectModel.ReadOnlyCollection<GameConfigs.CmdConfigRowData>
	// System.Collections.ObjectModel.ReadOnlyCollection<GameConfigs.EquipmentRowData>
	// System.Collections.ObjectModel.ReadOnlyCollection<GameConfigs.FunctionConfigRowData>
	// System.Collections.ObjectModel.ReadOnlyCollection<GameConfigs.HunterWheelRowData>
	// System.Collections.ObjectModel.ReadOnlyCollection<GameConfigs.ItemRowData>
	// System.Collections.ObjectModel.ReadOnlyCollection<GameConfigs.MerchantMissionRowData>
	// System.Collections.ObjectModel.ReadOnlyCollection<GameConfigs.MerchantRewardRowData>
	// System.Collections.ObjectModel.ReadOnlyCollection<GameConfigs.MonsterHunterRowData>
	// System.Collections.ObjectModel.ReadOnlyCollection<GameConfigs.PetsRowData>
	// System.Collections.ObjectModel.ReadOnlyCollection<GameConfigs.RedConfigRowData>
	// System.Collections.ObjectModel.ReadOnlyCollection<GameConfigs.RoleDataRowData>
	// System.Collections.ObjectModel.ReadOnlyCollection<GameConfigs.RoomExclusiveRowData>
	// System.Collections.ObjectModel.ReadOnlyCollection<GameConfigs.RoomLikeRowData>
	// System.Collections.ObjectModel.ReadOnlyCollection<GameConfigs.RoomMachineRowData>
	// System.Collections.ObjectModel.ReadOnlyCollection<GameConfigs.RoomRowData>
	// System.Collections.ObjectModel.ReadOnlyCollection<GameConfigs.RoomTechRowData>
	// System.Collections.ObjectModel.ReadOnlyCollection<GameConfigs.ShopRowData>
	// System.Collections.ObjectModel.ReadOnlyCollection<SDK.TDSDK.ItemInfo>
	// System.Collections.ObjectModel.ReadOnlyCollection<SGame.AbilityData.AbilitLevelRenderer>
	// System.Collections.ObjectModel.ReadOnlyCollection<SGame.ActiveTimeSystem.ActiveData>
	// System.Collections.ObjectModel.ReadOnlyCollection<SGame.AdItem>
	// System.Collections.ObjectModel.ReadOnlyCollection<SGame.BuffData>
	// System.Collections.ObjectModel.ReadOnlyCollection<SGame.BulletHitSystem.Data>
	// System.Collections.ObjectModel.ReadOnlyCollection<SGame.ChairData>
	// System.Collections.ObjectModel.ReadOnlyCollection<SGame.CharacterDespawnSystem.EventData>
	// System.Collections.ObjectModel.ReadOnlyCollection<SGame.CharacterSpawnSystem.CharacterEvent>
	// System.Collections.ObjectModel.ReadOnlyCollection<SGame.DelayExcuter.Item>
	// System.Collections.ObjectModel.ReadOnlyCollection<SGame.GameLoopRequest>
	// System.Collections.ObjectModel.ReadOnlyCollection<SGame.GiftReward>
	// System.Collections.ObjectModel.ReadOnlyCollection<SGame.ItemData.Value>
	// System.Collections.ObjectModel.ReadOnlyCollection<SGame.ObjectPool.ExtendData<object>>
	// System.Collections.ObjectModel.ReadOnlyCollection<SGame.RecordRoleData>
	// System.Collections.ObjectModel.ReadOnlyCollection<SGame.Worker>
	// System.Collections.ObjectModel.ReadOnlyCollection<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.ObjectModel.ReadOnlyCollection<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.ObjectModel.ReadOnlyCollection<System.UIntPtr>
	// System.Collections.ObjectModel.ReadOnlyCollection<Unity.Entities.Entity>
	// System.Collections.ObjectModel.ReadOnlyCollection<Unity.Mathematics.int2>
	// System.Collections.ObjectModel.ReadOnlyCollection<UnityEngine.CombineInstance>
	// System.Collections.ObjectModel.ReadOnlyCollection<UnityEngine.Vector2>
	// System.Collections.ObjectModel.ReadOnlyCollection<UnityEngine.Vector2Int>
	// System.Collections.ObjectModel.ReadOnlyCollection<int>
	// System.Collections.ObjectModel.ReadOnlyCollection<object>
	// System.Comparison<GameConfigs.ADConfigRowData>
	// System.Comparison<GameConfigs.AbilityLevelRowData>
	// System.Comparison<GameConfigs.ActivityTimeRowData>
	// System.Comparison<GameConfigs.AvatarRowData>
	// System.Comparison<GameConfigs.CmdConfigRowData>
	// System.Comparison<GameConfigs.EquipmentRowData>
	// System.Comparison<GameConfigs.FunctionConfigRowData>
	// System.Comparison<GameConfigs.HunterWheelRowData>
	// System.Comparison<GameConfigs.ItemRowData>
	// System.Comparison<GameConfigs.MerchantMissionRowData>
	// System.Comparison<GameConfigs.MerchantRewardRowData>
	// System.Comparison<GameConfigs.MonsterHunterRowData>
	// System.Comparison<GameConfigs.PetsRowData>
	// System.Comparison<GameConfigs.RedConfigRowData>
	// System.Comparison<GameConfigs.RoleDataRowData>
	// System.Comparison<GameConfigs.RoomExclusiveRowData>
	// System.Comparison<GameConfigs.RoomLikeRowData>
	// System.Comparison<GameConfigs.RoomMachineRowData>
	// System.Comparison<GameConfigs.RoomRowData>
	// System.Comparison<GameConfigs.RoomTechRowData>
	// System.Comparison<GameConfigs.ShopRowData>
	// System.Comparison<SDK.TDSDK.ItemInfo>
	// System.Comparison<SGame.AbilityData.AbilitLevelRenderer>
	// System.Comparison<SGame.ActiveTimeSystem.ActiveData>
	// System.Comparison<SGame.AdItem>
	// System.Comparison<SGame.BuffData>
	// System.Comparison<SGame.BulletHitSystem.Data>
	// System.Comparison<SGame.ChairData>
	// System.Comparison<SGame.CharacterDespawnSystem.EventData>
	// System.Comparison<SGame.CharacterSpawnSystem.CharacterEvent>
	// System.Comparison<SGame.DelayExcuter.Item>
	// System.Comparison<SGame.GameLoopRequest>
	// System.Comparison<SGame.GiftReward>
	// System.Comparison<SGame.ItemData.Value>
	// System.Comparison<SGame.ObjectPool.ExtendData<object>>
	// System.Comparison<SGame.RecordRoleData>
	// System.Comparison<SGame.Worker>
	// System.Comparison<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Comparison<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Comparison<System.UIntPtr>
	// System.Comparison<Unity.Entities.Entity>
	// System.Comparison<Unity.Mathematics.int2>
	// System.Comparison<UnityEngine.CombineInstance>
	// System.Comparison<UnityEngine.Vector2>
	// System.Comparison<UnityEngine.Vector2Int>
	// System.Comparison<int>
	// System.Comparison<object>
	// System.Func<GameConfigs.ADConfigRowData,byte>
	// System.Func<GameConfigs.AbilityLevelRowData,byte>
	// System.Func<GameConfigs.ActivityTimeRowData,byte>
	// System.Func<GameConfigs.AvatarRowData,byte>
	// System.Func<GameConfigs.BuffRowData,byte>
	// System.Func<GameConfigs.CmdConfigRowData,byte>
	// System.Func<GameConfigs.EquipmentRowData,byte>
	// System.Func<GameConfigs.FunctionConfigRowData,byte>
	// System.Func<GameConfigs.HunterWheelRowData,byte>
	// System.Func<GameConfigs.HunterWheelRowData,int>
	// System.Func<GameConfigs.ItemRowData,SGame.ItemData.Value>
	// System.Func<GameConfigs.ItemRowData,byte>
	// System.Func<GameConfigs.ItemRowData,int>
	// System.Func<GameConfigs.ItemRowData,object>
	// System.Func<GameConfigs.MerchantMissionRowData,byte>
	// System.Func<GameConfigs.MerchantRewardRowData,byte>
	// System.Func<GameConfigs.MonsterHunterRowData,byte>
	// System.Func<GameConfigs.MonsterHunterRowData,object>
	// System.Func<GameConfigs.PetsRowData,byte>
	// System.Func<GameConfigs.RankConfigRowData,byte>
	// System.Func<GameConfigs.RedConfigRowData,byte>
	// System.Func<GameConfigs.RoleDataRowData,byte>
	// System.Func<GameConfigs.RoomExclusiveRowData,byte>
	// System.Func<GameConfigs.RoomExclusiveRowData,int>
	// System.Func<GameConfigs.RoomLikeRowData,byte>
	// System.Func<GameConfigs.RoomLikeRowData,int>
	// System.Func<GameConfigs.RoomMachineRowData,GameConfigs.RoomMachineRowData>
	// System.Func<GameConfigs.RoomMachineRowData,byte>
	// System.Func<GameConfigs.RoomMachineRowData,int>
	// System.Func<GameConfigs.RoomMachineRowData,object>
	// System.Func<GameConfigs.RoomRowData,byte>
	// System.Func<GameConfigs.RoomTechRowData,GameConfigs.RoomTechRowData>
	// System.Func<GameConfigs.RoomTechRowData,byte>
	// System.Func<GameConfigs.RoomTechRowData,int>
	// System.Func<GameConfigs.ShopRowData,byte>
	// System.Func<GameConfigs.ui_resRowData,byte>
	// System.Func<SGame.BuffData,byte>
	// System.Func<SGame.ItemData.Value,SGame.ItemData.Value>
	// System.Func<SGame.ItemData.Value,byte>
	// System.Func<SGame.ItemData.Value,double>
	// System.Func<SGame.ItemData.Value,int>
	// System.Func<SGame.ItemData.Value,uint>
	// System.Func<SGame.Worker,byte>
	// System.Func<System.Collections.Generic.KeyValuePair<int,float>,System.Collections.Generic.KeyValuePair<int,float>,System.Collections.Generic.KeyValuePair<int,float>>
	// System.Func<System.Collections.Generic.KeyValuePair<int,int>,byte>
	// System.Func<System.Collections.Generic.KeyValuePair<int,int>,object>
	// System.Func<Unity.Entities.Entity,byte>
	// System.Func<UnityEngine.Vector2Int,byte>
	// System.Func<byte>
	// System.Func<double,byte>
	// System.Func<int,SGame.ItemData.Value>
	// System.Func<int,UnityEngine.Vector2Int>
	// System.Func<int,byte>
	// System.Func<int,int>
	// System.Func<int,object>
	// System.Func<int>
	// System.Func<object,SGame.BuffData>
	// System.Func<object,SGame.ChairData>
	// System.Func<object,SGame.ItemData.Value>
	// System.Func<object,Unity.Entities.Entity>
	// System.Func<object,Unity.Entities.EntityManager>
	// System.Func<object,Unity.Mathematics.int2>
	// System.Func<object,UnityEngine.Vector2>
	// System.Func<object,UnityEngine.Vector2Int>
	// System.Func<object,byte>
	// System.Func<object,double>
	// System.Func<object,float>
	// System.Func<object,int,int,int>
	// System.Func<object,int,int,object>
	// System.Func<object,int>
	// System.Func<object,object,object,byte>
	// System.Func<object,object>
	// System.Func<object,uint>
	// System.Func<object>
	// System.IEquatable<SGame.DelayExcuter.Item>
	// System.IEquatable<SGame.Worker>
	// System.IEquatable<object>
	// System.Linq.Buffer<int>
	// System.Linq.Buffer<object>
	// System.Linq.Enumerable.<TakeIterator>d__25<object>
	// System.Linq.Enumerable.Iterator<GameConfigs.HunterWheelRowData>
	// System.Linq.Enumerable.Iterator<GameConfigs.ItemRowData>
	// System.Linq.Enumerable.Iterator<GameConfigs.MonsterHunterRowData>
	// System.Linq.Enumerable.Iterator<GameConfigs.RoomExclusiveRowData>
	// System.Linq.Enumerable.Iterator<GameConfigs.RoomLikeRowData>
	// System.Linq.Enumerable.Iterator<GameConfigs.RoomMachineRowData>
	// System.Linq.Enumerable.Iterator<SGame.BuffData>
	// System.Linq.Enumerable.Iterator<SGame.ItemData.Value>
	// System.Linq.Enumerable.Iterator<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Linq.Enumerable.Iterator<UnityEngine.Vector2Int>
	// System.Linq.Enumerable.Iterator<double>
	// System.Linq.Enumerable.Iterator<int>
	// System.Linq.Enumerable.Iterator<object>
	// System.Linq.Enumerable.WhereArrayIterator<GameConfigs.RoomMachineRowData>
	// System.Linq.Enumerable.WhereArrayIterator<SGame.ItemData.Value>
	// System.Linq.Enumerable.WhereArrayIterator<object>
	// System.Linq.Enumerable.WhereEnumerableIterator<GameConfigs.RoomMachineRowData>
	// System.Linq.Enumerable.WhereEnumerableIterator<SGame.BuffData>
	// System.Linq.Enumerable.WhereEnumerableIterator<SGame.ItemData.Value>
	// System.Linq.Enumerable.WhereEnumerableIterator<UnityEngine.Vector2Int>
	// System.Linq.Enumerable.WhereEnumerableIterator<double>
	// System.Linq.Enumerable.WhereEnumerableIterator<int>
	// System.Linq.Enumerable.WhereEnumerableIterator<object>
	// System.Linq.Enumerable.WhereListIterator<GameConfigs.RoomMachineRowData>
	// System.Linq.Enumerable.WhereListIterator<SGame.ItemData.Value>
	// System.Linq.Enumerable.WhereListIterator<object>
	// System.Linq.Enumerable.WhereSelectArrayIterator<GameConfigs.HunterWheelRowData,int>
	// System.Linq.Enumerable.WhereSelectArrayIterator<GameConfigs.ItemRowData,SGame.ItemData.Value>
	// System.Linq.Enumerable.WhereSelectArrayIterator<GameConfigs.ItemRowData,int>
	// System.Linq.Enumerable.WhereSelectArrayIterator<GameConfigs.ItemRowData,object>
	// System.Linq.Enumerable.WhereSelectArrayIterator<GameConfigs.MonsterHunterRowData,object>
	// System.Linq.Enumerable.WhereSelectArrayIterator<GameConfigs.RoomExclusiveRowData,int>
	// System.Linq.Enumerable.WhereSelectArrayIterator<GameConfigs.RoomLikeRowData,int>
	// System.Linq.Enumerable.WhereSelectArrayIterator<GameConfigs.RoomMachineRowData,object>
	// System.Linq.Enumerable.WhereSelectArrayIterator<SGame.ItemData.Value,double>
	// System.Linq.Enumerable.WhereSelectArrayIterator<System.Collections.Generic.KeyValuePair<int,int>,object>
	// System.Linq.Enumerable.WhereSelectArrayIterator<int,SGame.ItemData.Value>
	// System.Linq.Enumerable.WhereSelectArrayIterator<int,UnityEngine.Vector2Int>
	// System.Linq.Enumerable.WhereSelectArrayIterator<int,object>
	// System.Linq.Enumerable.WhereSelectArrayIterator<object,SGame.BuffData>
	// System.Linq.Enumerable.WhereSelectArrayIterator<object,SGame.ItemData.Value>
	// System.Linq.Enumerable.WhereSelectArrayIterator<object,int>
	// System.Linq.Enumerable.WhereSelectArrayIterator<object,object>
	// System.Linq.Enumerable.WhereSelectEnumerableIterator<GameConfigs.HunterWheelRowData,int>
	// System.Linq.Enumerable.WhereSelectEnumerableIterator<GameConfigs.ItemRowData,SGame.ItemData.Value>
	// System.Linq.Enumerable.WhereSelectEnumerableIterator<GameConfigs.ItemRowData,int>
	// System.Linq.Enumerable.WhereSelectEnumerableIterator<GameConfigs.ItemRowData,object>
	// System.Linq.Enumerable.WhereSelectEnumerableIterator<GameConfigs.MonsterHunterRowData,object>
	// System.Linq.Enumerable.WhereSelectEnumerableIterator<GameConfigs.RoomExclusiveRowData,int>
	// System.Linq.Enumerable.WhereSelectEnumerableIterator<GameConfigs.RoomLikeRowData,int>
	// System.Linq.Enumerable.WhereSelectEnumerableIterator<GameConfigs.RoomMachineRowData,object>
	// System.Linq.Enumerable.WhereSelectEnumerableIterator<SGame.ItemData.Value,double>
	// System.Linq.Enumerable.WhereSelectEnumerableIterator<System.Collections.Generic.KeyValuePair<int,int>,object>
	// System.Linq.Enumerable.WhereSelectEnumerableIterator<int,SGame.ItemData.Value>
	// System.Linq.Enumerable.WhereSelectEnumerableIterator<int,UnityEngine.Vector2Int>
	// System.Linq.Enumerable.WhereSelectEnumerableIterator<int,object>
	// System.Linq.Enumerable.WhereSelectEnumerableIterator<object,SGame.BuffData>
	// System.Linq.Enumerable.WhereSelectEnumerableIterator<object,SGame.ItemData.Value>
	// System.Linq.Enumerable.WhereSelectEnumerableIterator<object,int>
	// System.Linq.Enumerable.WhereSelectEnumerableIterator<object,object>
	// System.Linq.Enumerable.WhereSelectListIterator<GameConfigs.HunterWheelRowData,int>
	// System.Linq.Enumerable.WhereSelectListIterator<GameConfigs.ItemRowData,SGame.ItemData.Value>
	// System.Linq.Enumerable.WhereSelectListIterator<GameConfigs.ItemRowData,int>
	// System.Linq.Enumerable.WhereSelectListIterator<GameConfigs.ItemRowData,object>
	// System.Linq.Enumerable.WhereSelectListIterator<GameConfigs.MonsterHunterRowData,object>
	// System.Linq.Enumerable.WhereSelectListIterator<GameConfigs.RoomExclusiveRowData,int>
	// System.Linq.Enumerable.WhereSelectListIterator<GameConfigs.RoomLikeRowData,int>
	// System.Linq.Enumerable.WhereSelectListIterator<GameConfigs.RoomMachineRowData,object>
	// System.Linq.Enumerable.WhereSelectListIterator<SGame.ItemData.Value,double>
	// System.Linq.Enumerable.WhereSelectListIterator<System.Collections.Generic.KeyValuePair<int,int>,object>
	// System.Linq.Enumerable.WhereSelectListIterator<int,SGame.ItemData.Value>
	// System.Linq.Enumerable.WhereSelectListIterator<int,UnityEngine.Vector2Int>
	// System.Linq.Enumerable.WhereSelectListIterator<int,object>
	// System.Linq.Enumerable.WhereSelectListIterator<object,SGame.BuffData>
	// System.Linq.Enumerable.WhereSelectListIterator<object,SGame.ItemData.Value>
	// System.Linq.Enumerable.WhereSelectListIterator<object,int>
	// System.Linq.Enumerable.WhereSelectListIterator<object,object>
	// System.Linq.GroupedEnumerable<GameConfigs.RoomMachineRowData,int,GameConfigs.RoomMachineRowData>
	// System.Linq.GroupedEnumerable<SGame.ItemData.Value,int,SGame.ItemData.Value>
	// System.Linq.GroupedEnumerable<SGame.ItemData.Value,uint,SGame.ItemData.Value>
	// System.Linq.GroupedEnumerable<int,int,int>
	// System.Linq.GroupedEnumerable<object,int,object>
	// System.Linq.IGrouping<int,GameConfigs.RoomMachineRowData>
	// System.Linq.IGrouping<int,SGame.ItemData.Value>
	// System.Linq.IGrouping<int,int>
	// System.Linq.IGrouping<int,object>
	// System.Linq.IGrouping<uint,SGame.ItemData.Value>
	// System.Linq.IdentityFunction.<>c<GameConfigs.RoomMachineRowData>
	// System.Linq.IdentityFunction.<>c<GameConfigs.RoomTechRowData>
	// System.Linq.IdentityFunction.<>c<SGame.ItemData.Value>
	// System.Linq.IdentityFunction.<>c<int>
	// System.Linq.IdentityFunction.<>c<object>
	// System.Linq.IdentityFunction<GameConfigs.RoomMachineRowData>
	// System.Linq.IdentityFunction<GameConfigs.RoomTechRowData>
	// System.Linq.IdentityFunction<SGame.ItemData.Value>
	// System.Linq.IdentityFunction<int>
	// System.Linq.IdentityFunction<object>
	// System.Linq.Lookup.<GetEnumerator>d__12<int,GameConfigs.RoomMachineRowData>
	// System.Linq.Lookup.<GetEnumerator>d__12<int,SGame.ItemData.Value>
	// System.Linq.Lookup.<GetEnumerator>d__12<int,int>
	// System.Linq.Lookup.<GetEnumerator>d__12<int,object>
	// System.Linq.Lookup.<GetEnumerator>d__12<uint,SGame.ItemData.Value>
	// System.Linq.Lookup.Grouping.<GetEnumerator>d__7<int,GameConfigs.RoomMachineRowData>
	// System.Linq.Lookup.Grouping.<GetEnumerator>d__7<int,SGame.ItemData.Value>
	// System.Linq.Lookup.Grouping.<GetEnumerator>d__7<int,int>
	// System.Linq.Lookup.Grouping.<GetEnumerator>d__7<int,object>
	// System.Linq.Lookup.Grouping.<GetEnumerator>d__7<uint,SGame.ItemData.Value>
	// System.Linq.Lookup.Grouping<int,GameConfigs.RoomMachineRowData>
	// System.Linq.Lookup.Grouping<int,SGame.ItemData.Value>
	// System.Linq.Lookup.Grouping<int,int>
	// System.Linq.Lookup.Grouping<int,object>
	// System.Linq.Lookup.Grouping<uint,SGame.ItemData.Value>
	// System.Linq.Lookup<int,GameConfigs.RoomMachineRowData>
	// System.Linq.Lookup<int,SGame.ItemData.Value>
	// System.Linq.Lookup<int,int>
	// System.Linq.Lookup<int,object>
	// System.Linq.Lookup<uint,SGame.ItemData.Value>
	// System.Nullable<GameConfigs.ADConfigRowData>
	// System.Nullable<GameConfigs.AbilityLevelRowData>
	// System.Nullable<GameConfigs.AbilityListRowData>
	// System.Nullable<GameConfigs.ActivityTimeRowData>
	// System.Nullable<GameConfigs.AvatarRowData>
	// System.Nullable<GameConfigs.BuffRowData>
	// System.Nullable<GameConfigs.BulletRowData>
	// System.Nullable<GameConfigs.ChestRowData>
	// System.Nullable<GameConfigs.CmdConfigRowData>
	// System.Nullable<GameConfigs.Design_GlobalRowData>
	// System.Nullable<GameConfigs.EggRowData>
	// System.Nullable<GameConfigs.EntryRowData>
	// System.Nullable<GameConfigs.EquipBuffRowData>
	// System.Nullable<GameConfigs.EquipQualityRowData>
	// System.Nullable<GameConfigs.EquipRowData>
	// System.Nullable<GameConfigs.EquipUpLevelCostRowData>
	// System.Nullable<GameConfigs.EquipmentRowData>
	// System.Nullable<GameConfigs.EvolutionRowData>
	// System.Nullable<GameConfigs.ExchangeRowData>
	// System.Nullable<GameConfigs.FunctionConfigRowData>
	// System.Nullable<GameConfigs.HunterWheelRowData>
	// System.Nullable<GameConfigs.ItemRowData>
	// System.Nullable<GameConfigs.Language_CHNRowData>
	// System.Nullable<GameConfigs.Language_ENRowData>
	// System.Nullable<GameConfigs.Language_HIRowData>
	// System.Nullable<GameConfigs.Language_settingRowData>
	// System.Nullable<GameConfigs.LevelRowData>
	// System.Nullable<GameConfigs.MachineRowData>
	// System.Nullable<GameConfigs.MachineStarRowData>
	// System.Nullable<GameConfigs.MachineUpgradeRowData>
	// System.Nullable<GameConfigs.MerchantMissionRowData>
	// System.Nullable<GameConfigs.MerchantRewardRowData>
	// System.Nullable<GameConfigs.MonsterHunterRowData>
	// System.Nullable<GameConfigs.PetsRowData>
	// System.Nullable<GameConfigs.ProgressPackRowData>
	// System.Nullable<GameConfigs.RankConfigRowData>
	// System.Nullable<GameConfigs.RedConfigRowData>
	// System.Nullable<GameConfigs.RegionRowData>
	// System.Nullable<GameConfigs.RoleDataRowData>
	// System.Nullable<GameConfigs.RoomExclusiveRowData>
	// System.Nullable<GameConfigs.RoomLikeRowData>
	// System.Nullable<GameConfigs.RoomMachineRowData>
	// System.Nullable<GameConfigs.RoomRowData>
	// System.Nullable<GameConfigs.RoomTechRowData>
	// System.Nullable<GameConfigs.SceneRowData>
	// System.Nullable<GameConfigs.SettingConfigRowData>
	// System.Nullable<GameConfigs.ShopRowData>
	// System.Nullable<GameConfigs.Sound_EffectRowData>
	// System.Nullable<GameConfigs.effectsRowData>
	// System.Nullable<GameConfigs.fontsRowData>
	// System.Nullable<GameConfigs.friendRowData>
	// System.Nullable<GameConfigs.game_globalRowData>
	// System.Nullable<GameConfigs.languageRowData>
	// System.Nullable<GameConfigs.roleRowData>
	// System.Nullable<GameConfigs.tableRowData>
	// System.Nullable<GameConfigs.ui_groupsRowData>
	// System.Nullable<GameConfigs.ui_resRowData>
	// System.Nullable<double>
	// System.Nullable<int>
	// System.Predicate<GameConfigs.ADConfigRowData>
	// System.Predicate<GameConfigs.AbilityLevelRowData>
	// System.Predicate<GameConfigs.ActivityTimeRowData>
	// System.Predicate<GameConfigs.AvatarRowData>
	// System.Predicate<GameConfigs.CmdConfigRowData>
	// System.Predicate<GameConfigs.EquipmentRowData>
	// System.Predicate<GameConfigs.FunctionConfigRowData>
	// System.Predicate<GameConfigs.HunterWheelRowData>
	// System.Predicate<GameConfigs.ItemRowData>
	// System.Predicate<GameConfigs.MerchantMissionRowData>
	// System.Predicate<GameConfigs.MerchantRewardRowData>
	// System.Predicate<GameConfigs.MonsterHunterRowData>
	// System.Predicate<GameConfigs.PetsRowData>
	// System.Predicate<GameConfigs.RedConfigRowData>
	// System.Predicate<GameConfigs.RoleDataRowData>
	// System.Predicate<GameConfigs.RoomExclusiveRowData>
	// System.Predicate<GameConfigs.RoomLikeRowData>
	// System.Predicate<GameConfigs.RoomMachineRowData>
	// System.Predicate<GameConfigs.RoomRowData>
	// System.Predicate<GameConfigs.RoomTechRowData>
	// System.Predicate<GameConfigs.ShopRowData>
	// System.Predicate<SDK.TDSDK.ItemInfo>
	// System.Predicate<SGame.AbilityData.AbilitLevelRenderer>
	// System.Predicate<SGame.ActiveTimeSystem.ActiveData>
	// System.Predicate<SGame.AdItem>
	// System.Predicate<SGame.BuffData>
	// System.Predicate<SGame.BulletHitSystem.Data>
	// System.Predicate<SGame.ChairData>
	// System.Predicate<SGame.CharacterDespawnSystem.EventData>
	// System.Predicate<SGame.CharacterSpawnSystem.CharacterEvent>
	// System.Predicate<SGame.DelayExcuter.Item>
	// System.Predicate<SGame.GameLoopRequest>
	// System.Predicate<SGame.GiftReward>
	// System.Predicate<SGame.ItemData.Value>
	// System.Predicate<SGame.ObjectPool.ExtendData<object>>
	// System.Predicate<SGame.RecordRoleData>
	// System.Predicate<SGame.Worker>
	// System.Predicate<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Predicate<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Predicate<System.UIntPtr>
	// System.Predicate<Unity.Entities.Entity>
	// System.Predicate<Unity.Mathematics.int2>
	// System.Predicate<UnityEngine.CombineInstance>
	// System.Predicate<UnityEngine.Vector2>
	// System.Predicate<UnityEngine.Vector2Int>
	// System.Predicate<int>
	// System.Predicate<object>
	// System.ReadOnlySpan<ushort>
	// System.Span<ushort>
	// System.ValueTuple<int,int>
	// Unity.Burst.SharedStatic<System.IntPtr>
	// Unity.Burst.SharedStatic<int>
	// Unity.Collections.LowLevel.Unsafe.UnsafeList.ParallelReader<System.IntPtr>
	// Unity.Collections.LowLevel.Unsafe.UnsafeList.ParallelReader<Unity.Entities.ArchetypeChunk>
	// Unity.Collections.LowLevel.Unsafe.UnsafeList.ParallelReader<Unity.Entities.EntityQuery>
	// Unity.Collections.LowLevel.Unsafe.UnsafeList.ParallelReader<int>
	// Unity.Collections.LowLevel.Unsafe.UnsafeList.ParallelWriter<System.IntPtr>
	// Unity.Collections.LowLevel.Unsafe.UnsafeList.ParallelWriter<Unity.Entities.ArchetypeChunk>
	// Unity.Collections.LowLevel.Unsafe.UnsafeList.ParallelWriter<Unity.Entities.EntityQuery>
	// Unity.Collections.LowLevel.Unsafe.UnsafeList.ParallelWriter<int>
	// Unity.Collections.LowLevel.Unsafe.UnsafeList<System.IntPtr>
	// Unity.Collections.LowLevel.Unsafe.UnsafeList<Unity.Entities.ArchetypeChunk>
	// Unity.Collections.LowLevel.Unsafe.UnsafeList<Unity.Entities.EntityQuery>
	// Unity.Collections.LowLevel.Unsafe.UnsafeList<int>
	// Unity.Collections.LowLevel.Unsafe.UnsafePtrList.ParallelReader<Unity.Entities.Chunk>
	// Unity.Collections.LowLevel.Unsafe.UnsafePtrList.ParallelWriter<Unity.Entities.Chunk>
	// Unity.Collections.LowLevel.Unsafe.UnsafePtrList<Unity.Entities.Chunk>
	// Unity.Collections.NativeArray.Enumerator<PathPositions>
	// Unity.Collections.NativeArray.Enumerator<Unity.Entities.Entity>
	// Unity.Collections.NativeArray.Enumerator<Unity.Transforms.Child>
	// Unity.Collections.NativeArray.Enumerator<int>
	// Unity.Collections.NativeArray.ReadOnly.Enumerator<PathPositions>
	// Unity.Collections.NativeArray.ReadOnly.Enumerator<Unity.Entities.Entity>
	// Unity.Collections.NativeArray.ReadOnly.Enumerator<Unity.Transforms.Child>
	// Unity.Collections.NativeArray.ReadOnly.Enumerator<int>
	// Unity.Collections.NativeArray.ReadOnly<PathPositions>
	// Unity.Collections.NativeArray.ReadOnly<Unity.Entities.Entity>
	// Unity.Collections.NativeArray.ReadOnly<Unity.Transforms.Child>
	// Unity.Collections.NativeArray.ReadOnly<int>
	// Unity.Collections.NativeArray<PathPositions>
	// Unity.Collections.NativeArray<Unity.Entities.Entity>
	// Unity.Collections.NativeArray<Unity.Transforms.Child>
	// Unity.Collections.NativeArray<int>
	// Unity.Collections.NativeSlice.Enumerator<PathPositions>
	// Unity.Collections.NativeSlice.Enumerator<Unity.Transforms.Child>
	// Unity.Collections.NativeSlice<PathPositions>
	// Unity.Collections.NativeSlice<Unity.Transforms.Child>
	// Unity.Entities.BufferAccessor<PathPositions>
	// Unity.Entities.BufferTypeHandle<PathPositions>
	// Unity.Entities.ComponentDataFromEntity<Unity.Transforms.Translation>
	// Unity.Entities.ComponentTypeHandle<Follow>
	// Unity.Entities.ComponentTypeHandle<SGame.BulletData>
	// Unity.Entities.ComponentTypeHandle<SGame.CharacterAttribue>
	// Unity.Entities.ComponentTypeHandle<SGame.CharacterSpawnSystem.CharacterSpawn>
	// Unity.Entities.ComponentTypeHandle<SGame.FoodTips>
	// Unity.Entities.ComponentTypeHandle<SGame.LastRotation>
	// Unity.Entities.ComponentTypeHandle<SGame.MoveTarget>
	// Unity.Entities.ComponentTypeHandle<SGame.RedEvent>
	// Unity.Entities.ComponentTypeHandle<SGame.Redpoint>
	// Unity.Entities.ComponentTypeHandle<SGame.RewardData>
	// Unity.Entities.ComponentTypeHandle<SGame.RewardWait>
	// Unity.Entities.ComponentTypeHandle<SGame.RotationSpeed>
	// Unity.Entities.ComponentTypeHandle<SGame.SoundData>
	// Unity.Entities.ComponentTypeHandle<SGame.SoundTime>
	// Unity.Entities.ComponentTypeHandle<SGame.SpawnReq>
	// Unity.Entities.ComponentTypeHandle<SGame.UI.HUDFlowE>
	// Unity.Entities.ComponentTypeHandle<SGame.UserInput>
	// Unity.Entities.ComponentTypeHandle<Speed>
	// Unity.Entities.ComponentTypeHandle<Unity.Transforms.Rotation>
	// Unity.Entities.ComponentTypeHandle<Unity.Transforms.Translation>
	// Unity.Entities.ComponentTypeHandle<object>
	// Unity.Entities.DynamicBuffer<PathPositions>
	// Unity.Entities.DynamicBuffer<Unity.Transforms.Child>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.BulletHitSystem.BulletHitSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.CharacterAnimSystem.CharacterAnimSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.CharacterAttributeSystem.CharacterAttributeSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.CharacterDespawnSystem.CharacterDespawnSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.DespawnFoodTipSystem.DespawnFoodTipSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.EntitySyncGameObjectSystem.EntitySyncGameObjectSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.FollowRotationSystem.FollowRotationSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.FollowRotationSystem.FollowRotationSystem_LambdaJob_1_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.GameObjectSyncSystem.GameObjectSyncSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.Http.HttpSystem.HttpSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.RedpointSystem.RedpointSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.RedpointSystem.RedpointSystem_LambdaJob_1_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.RedpointSystem.RedpointSystem_LambdaJob_2_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.RedpointSystem.RedpointSystem_LambdaJob_3_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.RewardSystem.RewardSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.RewardSystem.RewardSystem_LambdaJob_1_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.SpawnSystem.SpawnSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.SpawnSystem.SpawnSystem_LambdaJob_2_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.UI.HUDFlowSystem.HUDFlowSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.UI.HUDFlowSystem.HUDFlowSystem_LambdaJob_1_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.UI.HUDSyncSystem.HUDSyncSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.UserInputsystem.UserInputsystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.BulletHitSystem.BulletHitSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.CharacterAnimSystem.CharacterAnimSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.CharacterAttributeSystem.CharacterAttributeSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.CharacterDespawnSystem.CharacterDespawnSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.DespawnFoodTipSystem.DespawnFoodTipSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.EntitySyncGameObjectSystem.EntitySyncGameObjectSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.FollowRotationSystem.FollowRotationSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.FollowRotationSystem.FollowRotationSystem_LambdaJob_1_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.GameObjectSyncSystem.GameObjectSyncSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.Http.HttpSystem.HttpSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.RedpointSystem.RedpointSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.RedpointSystem.RedpointSystem_LambdaJob_1_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.RedpointSystem.RedpointSystem_LambdaJob_2_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.RedpointSystem.RedpointSystem_LambdaJob_3_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.RewardSystem.RewardSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.RewardSystem.RewardSystem_LambdaJob_1_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.SpawnSystem.SpawnSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.SpawnSystem.SpawnSystem_LambdaJob_2_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.UI.HUDFlowSystem.HUDFlowSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.UI.HUDFlowSystem.HUDFlowSystem_LambdaJob_1_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.UI.HUDSyncSystem.HUDSyncSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.UserInputsystem.UserInputsystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchIndexExtensions.JobEntityBatchIndexProducer.ExecuteJobFunction<SGame.MoveTargetSystem.MoveTargetSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchIndexExtensions.JobEntityBatchIndexProducer<SGame.MoveTargetSystem.MoveTargetSystem_LambdaJob_0_Job>
	// Unity.Properties.AOT.ListPropertyGenerator<object,object,byte>
	// Unity.Properties.AOT.ListPropertyGenerator<object,object,object>
	// Unity.Properties.AOT.PropertyBagGenerator<SGame.PoolID>
	// Unity.Properties.AOT.PropertyBagGenerator<System.Nullable<UnityEngine.Rect>>
	// Unity.Properties.AOT.PropertyBagGenerator<Unity.Entities.Entity>
	// Unity.Properties.AOT.PropertyBagGenerator<Unity.Mathematics.float3>
	// Unity.Properties.AOT.PropertyBagGenerator<UnityEngine.Color>
	// Unity.Properties.AOT.PropertyBagGenerator<UnityEngine.Quaternion>
	// Unity.Properties.AOT.PropertyBagGenerator<UnityEngine.Rect>
	// Unity.Properties.AOT.PropertyBagGenerator<UnityEngine.Vector2>
	// Unity.Properties.AOT.PropertyBagGenerator<UnityEngine.Vector2Int>
	// Unity.Properties.AOT.PropertyBagGenerator<UnityEngine.Vector3>
	// Unity.Properties.AOT.PropertyBagGenerator<object>
	// Unity.Properties.AOT.PropertyGenerator<SGame.PoolID,int>
	// Unity.Properties.AOT.PropertyGenerator<Unity.Entities.Entity,int>
	// Unity.Properties.AOT.PropertyGenerator<Unity.Mathematics.float3,float>
	// Unity.Properties.AOT.PropertyGenerator<UnityEngine.Color,float>
	// Unity.Properties.AOT.PropertyGenerator<UnityEngine.Quaternion,float>
	// Unity.Properties.AOT.PropertyGenerator<UnityEngine.Vector2,float>
	// Unity.Properties.AOT.PropertyGenerator<UnityEngine.Vector3,float>
	// Unity.Properties.AOT.PropertyGenerator<object,SGame.PoolID>
	// Unity.Properties.AOT.PropertyGenerator<object,System.Nullable<UnityEngine.Rect>>
	// Unity.Properties.AOT.PropertyGenerator<object,Unity.Entities.Entity>
	// Unity.Properties.AOT.PropertyGenerator<object,Unity.Mathematics.float3>
	// Unity.Properties.AOT.PropertyGenerator<object,UnityEngine.Color>
	// Unity.Properties.AOT.PropertyGenerator<object,UnityEngine.Quaternion>
	// Unity.Properties.AOT.PropertyGenerator<object,UnityEngine.Rect>
	// Unity.Properties.AOT.PropertyGenerator<object,UnityEngine.Vector2>
	// Unity.Properties.AOT.PropertyGenerator<object,UnityEngine.Vector2Int>
	// Unity.Properties.AOT.PropertyGenerator<object,UnityEngine.Vector3>
	// Unity.Properties.AOT.PropertyGenerator<object,byte>
	// Unity.Properties.AOT.PropertyGenerator<object,float>
	// Unity.Properties.AOT.PropertyGenerator<object,int>
	// Unity.Properties.AOT.PropertyGenerator<object,object>
	// Unity.Properties.ContainerPropertyBag.PropertyCollection<SGame.PoolID>
	// Unity.Properties.ContainerPropertyBag.PropertyCollection<System.Nullable<UnityEngine.Rect>>
	// Unity.Properties.ContainerPropertyBag.PropertyCollection<Unity.Entities.Entity>
	// Unity.Properties.ContainerPropertyBag.PropertyCollection<Unity.Mathematics.float3>
	// Unity.Properties.ContainerPropertyBag.PropertyCollection<UnityEngine.Color>
	// Unity.Properties.ContainerPropertyBag.PropertyCollection<UnityEngine.Quaternion>
	// Unity.Properties.ContainerPropertyBag.PropertyCollection<UnityEngine.Rect>
	// Unity.Properties.ContainerPropertyBag.PropertyCollection<UnityEngine.Vector2>
	// Unity.Properties.ContainerPropertyBag.PropertyCollection<UnityEngine.Vector2Int>
	// Unity.Properties.ContainerPropertyBag.PropertyCollection<UnityEngine.Vector3>
	// Unity.Properties.ContainerPropertyBag.PropertyCollection<object>
	// Unity.Properties.ContainerPropertyBag<SGame.PoolID>
	// Unity.Properties.ContainerPropertyBag<System.Nullable<UnityEngine.Rect>>
	// Unity.Properties.ContainerPropertyBag<Unity.Entities.Entity>
	// Unity.Properties.ContainerPropertyBag<Unity.Mathematics.float3>
	// Unity.Properties.ContainerPropertyBag<UnityEngine.Color>
	// Unity.Properties.ContainerPropertyBag<UnityEngine.Quaternion>
	// Unity.Properties.ContainerPropertyBag<UnityEngine.Rect>
	// Unity.Properties.ContainerPropertyBag<UnityEngine.Vector2>
	// Unity.Properties.ContainerPropertyBag<UnityEngine.Vector2Int>
	// Unity.Properties.ContainerPropertyBag<UnityEngine.Vector3>
	// Unity.Properties.ContainerPropertyBag<object>
	// Unity.Properties.Internal.IListPropertyAccept<object>
	// Unity.Properties.Internal.ListElementProperty<object,byte>
	// Unity.Properties.Internal.ListElementProperty<object,object>
	// Unity.Properties.Internal.ListPropertyBag.PropertyCollection.Enumerator<object,byte>
	// Unity.Properties.Internal.ListPropertyBag.PropertyCollection.Enumerator<object,object>
	// Unity.Properties.Internal.ListPropertyBag.PropertyCollection<object,byte>
	// Unity.Properties.Internal.ListPropertyBag.PropertyCollection<object,object>
	// Unity.Properties.Internal.ListPropertyBag<object,byte>
	// Unity.Properties.Internal.ListPropertyBag<object,object>
	// Unity.Properties.Property.<GetAttributes>d__22<SGame.PoolID,int>
	// Unity.Properties.Property.<GetAttributes>d__22<Unity.Entities.Entity,int>
	// Unity.Properties.Property.<GetAttributes>d__22<Unity.Mathematics.float3,float>
	// Unity.Properties.Property.<GetAttributes>d__22<UnityEngine.Color,float>
	// Unity.Properties.Property.<GetAttributes>d__22<UnityEngine.Quaternion,float>
	// Unity.Properties.Property.<GetAttributes>d__22<UnityEngine.Vector2,float>
	// Unity.Properties.Property.<GetAttributes>d__22<UnityEngine.Vector3,float>
	// Unity.Properties.Property.<GetAttributes>d__22<object,SGame.PoolID>
	// Unity.Properties.Property.<GetAttributes>d__22<object,System.Nullable<UnityEngine.Rect>>
	// Unity.Properties.Property.<GetAttributes>d__22<object,Unity.Entities.Entity>
	// Unity.Properties.Property.<GetAttributes>d__22<object,Unity.Mathematics.float3>
	// Unity.Properties.Property.<GetAttributes>d__22<object,UnityEngine.Color>
	// Unity.Properties.Property.<GetAttributes>d__22<object,UnityEngine.Quaternion>
	// Unity.Properties.Property.<GetAttributes>d__22<object,UnityEngine.Rect>
	// Unity.Properties.Property.<GetAttributes>d__22<object,UnityEngine.Vector2>
	// Unity.Properties.Property.<GetAttributes>d__22<object,UnityEngine.Vector2Int>
	// Unity.Properties.Property.<GetAttributes>d__22<object,UnityEngine.Vector3>
	// Unity.Properties.Property.<GetAttributes>d__22<object,byte>
	// Unity.Properties.Property.<GetAttributes>d__22<object,float>
	// Unity.Properties.Property.<GetAttributes>d__22<object,int>
	// Unity.Properties.Property.<GetAttributes>d__22<object,object>
	// Unity.Properties.Property<SGame.PoolID,int>
	// Unity.Properties.Property<Unity.Entities.Entity,int>
	// Unity.Properties.Property<Unity.Mathematics.float3,float>
	// Unity.Properties.Property<UnityEngine.Color,float>
	// Unity.Properties.Property<UnityEngine.Quaternion,float>
	// Unity.Properties.Property<UnityEngine.Vector2,float>
	// Unity.Properties.Property<UnityEngine.Vector3,float>
	// Unity.Properties.Property<object,SGame.PoolID>
	// Unity.Properties.Property<object,System.Nullable<UnityEngine.Rect>>
	// Unity.Properties.Property<object,Unity.Entities.Entity>
	// Unity.Properties.Property<object,Unity.Mathematics.float3>
	// Unity.Properties.Property<object,UnityEngine.Color>
	// Unity.Properties.Property<object,UnityEngine.Quaternion>
	// Unity.Properties.Property<object,UnityEngine.Rect>
	// Unity.Properties.Property<object,UnityEngine.Vector2>
	// Unity.Properties.Property<object,UnityEngine.Vector2Int>
	// Unity.Properties.Property<object,UnityEngine.Vector3>
	// Unity.Properties.Property<object,byte>
	// Unity.Properties.Property<object,float>
	// Unity.Properties.Property<object,int>
	// Unity.Properties.Property<object,object>
	// Unity.Properties.PropertyBag<SGame.PoolID>
	// Unity.Properties.PropertyBag<System.Nullable<UnityEngine.Rect>>
	// Unity.Properties.PropertyBag<Unity.Entities.Entity>
	// Unity.Properties.PropertyBag<Unity.Mathematics.float3>
	// Unity.Properties.PropertyBag<UnityEngine.Color>
	// Unity.Properties.PropertyBag<UnityEngine.Quaternion>
	// Unity.Properties.PropertyBag<UnityEngine.Rect>
	// Unity.Properties.PropertyBag<UnityEngine.Vector2>
	// Unity.Properties.PropertyBag<UnityEngine.Vector2Int>
	// Unity.Properties.PropertyBag<UnityEngine.Vector3>
	// Unity.Properties.PropertyBag<object>
	// Unity.VisualScripting.ConnectionCollectionBase<object,object,object,object>
	// Unity.VisualScripting.EventUnit.<>c__DisplayClass14_0<Unity.VisualScripting.CustomEventArgs>
	// Unity.VisualScripting.EventUnit.<>c__DisplayClass14_0<object>
	// Unity.VisualScripting.EventUnit.Data<Unity.VisualScripting.CustomEventArgs>
	// Unity.VisualScripting.EventUnit.Data<object>
	// Unity.VisualScripting.EventUnit<Unity.VisualScripting.CustomEventArgs>
	// Unity.VisualScripting.EventUnit<object>
	// Unity.VisualScripting.GameObjectEventUnit.Data<object>
	// Unity.VisualScripting.GameObjectEventUnit<object>
	// Unity.VisualScripting.HashSetPool<object>
	// Unity.VisualScripting.IConnection<object,object>
	// Unity.VisualScripting.IConnectionCollection<object,object,object>
	// Unity.VisualScripting.IKeyedCollection<object,object>
	// Unity.VisualScripting.Unit.<>c__DisplayClass86_0<SGame.ChairData>
	// Unity.VisualScripting.Unit.<>c__DisplayClass86_0<Unity.Entities.Entity>
	// Unity.VisualScripting.Unit.<>c__DisplayClass86_0<Unity.Entities.EntityManager>
	// Unity.VisualScripting.Unit.<>c__DisplayClass86_0<Unity.Mathematics.int2>
	// Unity.VisualScripting.Unit.<>c__DisplayClass86_0<UnityEngine.Vector2>
	// Unity.VisualScripting.Unit.<>c__DisplayClass86_0<UnityEngine.Vector2Int>
	// Unity.VisualScripting.Unit.<>c__DisplayClass86_0<byte>
	// Unity.VisualScripting.Unit.<>c__DisplayClass86_0<double>
	// Unity.VisualScripting.Unit.<>c__DisplayClass86_0<float>
	// Unity.VisualScripting.Unit.<>c__DisplayClass86_0<int>
	// Unity.VisualScripting.Unit.<>c__DisplayClass86_0<object>
	// Unity.VisualScripting.UnitPort.<>c__DisplayClass45_0<object,object,object>
	// Unity.VisualScripting.UnitPort.<>c__DisplayClass46_0<object,object,object>
	// Unity.VisualScripting.UnitPort<object,object,object>
	// }}

	public void RefMethods()
	{
		// object Cinemachine.CinemachineVirtualCamera.AddCinemachineComponent<object>()
		// object Cinemachine.CinemachineVirtualCamera.GetCinemachineComponent<object>()
		// System.Collections.IEnumerator Coroutine.CoWait<object>(object,System.Action<object>)
		// System.Void Coroutine.Wait<object>(object,System.Action<object>)
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.ADConfigRowData>>(FlatBuffers.Offset<GameConfigs.ADConfigRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.AbilityLevelRowData>>(FlatBuffers.Offset<GameConfigs.AbilityLevelRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.AbilityListRowData>>(FlatBuffers.Offset<GameConfigs.AbilityListRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.ActivityTimeRowData>>(FlatBuffers.Offset<GameConfigs.ActivityTimeRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.AvatarRowData>>(FlatBuffers.Offset<GameConfigs.AvatarRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.BuffRowData>>(FlatBuffers.Offset<GameConfigs.BuffRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.BulletRowData>>(FlatBuffers.Offset<GameConfigs.BulletRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.ChestRowData>>(FlatBuffers.Offset<GameConfigs.ChestRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.CmdConfigRowData>>(FlatBuffers.Offset<GameConfigs.CmdConfigRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.Design_GlobalRowData>>(FlatBuffers.Offset<GameConfigs.Design_GlobalRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.EggRowData>>(FlatBuffers.Offset<GameConfigs.EggRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.EntryRowData>>(FlatBuffers.Offset<GameConfigs.EntryRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.EquipBuffRowData>>(FlatBuffers.Offset<GameConfigs.EquipBuffRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.EquipQualityRowData>>(FlatBuffers.Offset<GameConfigs.EquipQualityRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.EquipRowData>>(FlatBuffers.Offset<GameConfigs.EquipRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.EquipUpLevelCostRowData>>(FlatBuffers.Offset<GameConfigs.EquipUpLevelCostRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.EquipmentRowData>>(FlatBuffers.Offset<GameConfigs.EquipmentRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.EvolutionRowData>>(FlatBuffers.Offset<GameConfigs.EvolutionRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.ExchangeRowData>>(FlatBuffers.Offset<GameConfigs.ExchangeRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.FunctionConfigRowData>>(FlatBuffers.Offset<GameConfigs.FunctionConfigRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.HunterWheelRowData>>(FlatBuffers.Offset<GameConfigs.HunterWheelRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.ItemRowData>>(FlatBuffers.Offset<GameConfigs.ItemRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.Language_CHNRowData>>(FlatBuffers.Offset<GameConfigs.Language_CHNRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.Language_ENRowData>>(FlatBuffers.Offset<GameConfigs.Language_ENRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.Language_HIRowData>>(FlatBuffers.Offset<GameConfigs.Language_HIRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.Language_settingRowData>>(FlatBuffers.Offset<GameConfigs.Language_settingRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.LevelRowData>>(FlatBuffers.Offset<GameConfigs.LevelRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.MachineRowData>>(FlatBuffers.Offset<GameConfigs.MachineRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.MachineStarRowData>>(FlatBuffers.Offset<GameConfigs.MachineStarRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.MachineUpgradeRowData>>(FlatBuffers.Offset<GameConfigs.MachineUpgradeRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.MerchantMissionRowData>>(FlatBuffers.Offset<GameConfigs.MerchantMissionRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.MerchantRewardRowData>>(FlatBuffers.Offset<GameConfigs.MerchantRewardRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.MonsterHunterRowData>>(FlatBuffers.Offset<GameConfigs.MonsterHunterRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.PetsRowData>>(FlatBuffers.Offset<GameConfigs.PetsRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.ProgressPackRowData>>(FlatBuffers.Offset<GameConfigs.ProgressPackRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.RankConfigRowData>>(FlatBuffers.Offset<GameConfigs.RankConfigRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.RedConfigRowData>>(FlatBuffers.Offset<GameConfigs.RedConfigRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.RegionRowData>>(FlatBuffers.Offset<GameConfigs.RegionRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.RoleDataRowData>>(FlatBuffers.Offset<GameConfigs.RoleDataRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.RoomExclusiveRowData>>(FlatBuffers.Offset<GameConfigs.RoomExclusiveRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.RoomLikeRowData>>(FlatBuffers.Offset<GameConfigs.RoomLikeRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.RoomMachineRowData>>(FlatBuffers.Offset<GameConfigs.RoomMachineRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.RoomRowData>>(FlatBuffers.Offset<GameConfigs.RoomRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.RoomTechRowData>>(FlatBuffers.Offset<GameConfigs.RoomTechRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.SceneRowData>>(FlatBuffers.Offset<GameConfigs.SceneRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.SettingConfigRowData>>(FlatBuffers.Offset<GameConfigs.SettingConfigRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.ShopRowData>>(FlatBuffers.Offset<GameConfigs.ShopRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.Sound_EffectRowData>>(FlatBuffers.Offset<GameConfigs.Sound_EffectRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.effectsRowData>>(FlatBuffers.Offset<GameConfigs.effectsRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.fontsRowData>>(FlatBuffers.Offset<GameConfigs.fontsRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.friendRowData>>(FlatBuffers.Offset<GameConfigs.friendRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.game_globalRowData>>(FlatBuffers.Offset<GameConfigs.game_globalRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.languageRowData>>(FlatBuffers.Offset<GameConfigs.languageRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.roleRowData>>(FlatBuffers.Offset<GameConfigs.roleRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.tableRowData>>(FlatBuffers.Offset<GameConfigs.tableRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.ui_groupsRowData>>(FlatBuffers.Offset<GameConfigs.ui_groupsRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.ui_resRowData>>(FlatBuffers.Offset<GameConfigs.ui_resRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.StringOffset>(FlatBuffers.StringOffset[])
		// int FlatBuffers.ByteBuffer.ArraySize<byte>(byte[])
		// int FlatBuffers.ByteBuffer.ArraySize<float>(float[])
		// int FlatBuffers.ByteBuffer.ArraySize<int>(int[])
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.ADConfigRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.AbilityLevelRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.AbilityListRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.ActivityTimeRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.AvatarRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.BuffRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.BulletRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.ChestRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.CmdConfigRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.Design_GlobalRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.EggRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.EntryRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.EquipBuffRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.EquipQualityRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.EquipRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.EquipUpLevelCostRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.EquipmentRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.EvolutionRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.ExchangeRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.FunctionConfigRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.HunterWheelRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.ItemRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.Language_CHNRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.Language_ENRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.Language_HIRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.Language_settingRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.LevelRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.MachineRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.MachineStarRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.MachineUpgradeRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.MerchantMissionRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.MerchantRewardRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.MonsterHunterRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.PetsRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.ProgressPackRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.RankConfigRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.RedConfigRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.RegionRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.RoleDataRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.RoomExclusiveRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.RoomLikeRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.RoomMachineRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.RoomRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.RoomTechRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.SceneRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.SettingConfigRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.ShopRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.Sound_EffectRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.effectsRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.fontsRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.friendRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.game_globalRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.languageRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.roleRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.tableRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.ui_groupsRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.ui_resRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.StringOffset>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<float>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<int>()
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.ADConfigRowData>>(int,FlatBuffers.Offset<GameConfigs.ADConfigRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.AbilityLevelRowData>>(int,FlatBuffers.Offset<GameConfigs.AbilityLevelRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.AbilityListRowData>>(int,FlatBuffers.Offset<GameConfigs.AbilityListRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.ActivityTimeRowData>>(int,FlatBuffers.Offset<GameConfigs.ActivityTimeRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.AvatarRowData>>(int,FlatBuffers.Offset<GameConfigs.AvatarRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.BuffRowData>>(int,FlatBuffers.Offset<GameConfigs.BuffRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.BulletRowData>>(int,FlatBuffers.Offset<GameConfigs.BulletRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.ChestRowData>>(int,FlatBuffers.Offset<GameConfigs.ChestRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.CmdConfigRowData>>(int,FlatBuffers.Offset<GameConfigs.CmdConfigRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.Design_GlobalRowData>>(int,FlatBuffers.Offset<GameConfigs.Design_GlobalRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.EggRowData>>(int,FlatBuffers.Offset<GameConfigs.EggRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.EntryRowData>>(int,FlatBuffers.Offset<GameConfigs.EntryRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.EquipBuffRowData>>(int,FlatBuffers.Offset<GameConfigs.EquipBuffRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.EquipQualityRowData>>(int,FlatBuffers.Offset<GameConfigs.EquipQualityRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.EquipRowData>>(int,FlatBuffers.Offset<GameConfigs.EquipRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.EquipUpLevelCostRowData>>(int,FlatBuffers.Offset<GameConfigs.EquipUpLevelCostRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.EquipmentRowData>>(int,FlatBuffers.Offset<GameConfigs.EquipmentRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.EvolutionRowData>>(int,FlatBuffers.Offset<GameConfigs.EvolutionRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.ExchangeRowData>>(int,FlatBuffers.Offset<GameConfigs.ExchangeRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.FunctionConfigRowData>>(int,FlatBuffers.Offset<GameConfigs.FunctionConfigRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.HunterWheelRowData>>(int,FlatBuffers.Offset<GameConfigs.HunterWheelRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.ItemRowData>>(int,FlatBuffers.Offset<GameConfigs.ItemRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.Language_CHNRowData>>(int,FlatBuffers.Offset<GameConfigs.Language_CHNRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.Language_ENRowData>>(int,FlatBuffers.Offset<GameConfigs.Language_ENRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.Language_HIRowData>>(int,FlatBuffers.Offset<GameConfigs.Language_HIRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.Language_settingRowData>>(int,FlatBuffers.Offset<GameConfigs.Language_settingRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.LevelRowData>>(int,FlatBuffers.Offset<GameConfigs.LevelRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.MachineRowData>>(int,FlatBuffers.Offset<GameConfigs.MachineRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.MachineStarRowData>>(int,FlatBuffers.Offset<GameConfigs.MachineStarRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.MachineUpgradeRowData>>(int,FlatBuffers.Offset<GameConfigs.MachineUpgradeRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.MerchantMissionRowData>>(int,FlatBuffers.Offset<GameConfigs.MerchantMissionRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.MerchantRewardRowData>>(int,FlatBuffers.Offset<GameConfigs.MerchantRewardRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.MonsterHunterRowData>>(int,FlatBuffers.Offset<GameConfigs.MonsterHunterRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.PetsRowData>>(int,FlatBuffers.Offset<GameConfigs.PetsRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.ProgressPackRowData>>(int,FlatBuffers.Offset<GameConfigs.ProgressPackRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.RankConfigRowData>>(int,FlatBuffers.Offset<GameConfigs.RankConfigRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.RedConfigRowData>>(int,FlatBuffers.Offset<GameConfigs.RedConfigRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.RegionRowData>>(int,FlatBuffers.Offset<GameConfigs.RegionRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.RoleDataRowData>>(int,FlatBuffers.Offset<GameConfigs.RoleDataRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.RoomExclusiveRowData>>(int,FlatBuffers.Offset<GameConfigs.RoomExclusiveRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.RoomLikeRowData>>(int,FlatBuffers.Offset<GameConfigs.RoomLikeRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.RoomMachineRowData>>(int,FlatBuffers.Offset<GameConfigs.RoomMachineRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.RoomRowData>>(int,FlatBuffers.Offset<GameConfigs.RoomRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.RoomTechRowData>>(int,FlatBuffers.Offset<GameConfigs.RoomTechRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.SceneRowData>>(int,FlatBuffers.Offset<GameConfigs.SceneRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.SettingConfigRowData>>(int,FlatBuffers.Offset<GameConfigs.SettingConfigRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.ShopRowData>>(int,FlatBuffers.Offset<GameConfigs.ShopRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.Sound_EffectRowData>>(int,FlatBuffers.Offset<GameConfigs.Sound_EffectRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.effectsRowData>>(int,FlatBuffers.Offset<GameConfigs.effectsRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.fontsRowData>>(int,FlatBuffers.Offset<GameConfigs.fontsRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.friendRowData>>(int,FlatBuffers.Offset<GameConfigs.friendRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.game_globalRowData>>(int,FlatBuffers.Offset<GameConfigs.game_globalRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.languageRowData>>(int,FlatBuffers.Offset<GameConfigs.languageRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.roleRowData>>(int,FlatBuffers.Offset<GameConfigs.roleRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.tableRowData>>(int,FlatBuffers.Offset<GameConfigs.tableRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.ui_groupsRowData>>(int,FlatBuffers.Offset<GameConfigs.ui_groupsRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.ui_resRowData>>(int,FlatBuffers.Offset<GameConfigs.ui_resRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.StringOffset>(int,FlatBuffers.StringOffset[])
		// int FlatBuffers.ByteBuffer.Put<float>(int,float[])
		// int FlatBuffers.ByteBuffer.Put<int>(int,int[])
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.ADConfigRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.AbilityLevelRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.AbilityListRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.ActivityTimeRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.AvatarRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.BuffRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.BulletRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.ChestRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.CmdConfigRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.Design_GlobalRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.EggRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.EntryRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.EquipBuffRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.EquipQualityRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.EquipRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.EquipUpLevelCostRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.EquipmentRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.EvolutionRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.ExchangeRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.FunctionConfigRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.HunterWheelRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.ItemRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.Language_CHNRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.Language_ENRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.Language_HIRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.Language_settingRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.LevelRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.MachineRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.MachineStarRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.MachineUpgradeRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.MerchantMissionRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.MerchantRewardRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.MonsterHunterRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.PetsRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.ProgressPackRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.RankConfigRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.RedConfigRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.RegionRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.RoleDataRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.RoomExclusiveRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.RoomLikeRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.RoomMachineRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.RoomRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.RoomTechRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.SceneRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.SettingConfigRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.ShopRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.Sound_EffectRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.effectsRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.fontsRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.friendRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.game_globalRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.languageRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.roleRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.tableRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.ui_groupsRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.ui_resRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.StringOffset>()
		// int FlatBuffers.ByteBuffer.SizeOf<byte>()
		// int FlatBuffers.ByteBuffer.SizeOf<float>()
		// int FlatBuffers.ByteBuffer.SizeOf<int>()
		// byte[] FlatBuffers.ByteBuffer.ToArray<byte>(int,int)
		// float[] FlatBuffers.ByteBuffer.ToArray<float>(int,int)
		// int[] FlatBuffers.ByteBuffer.ToArray<int>(int,int)
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.ADConfigRowData>>(FlatBuffers.Offset<GameConfigs.ADConfigRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.AbilityLevelRowData>>(FlatBuffers.Offset<GameConfigs.AbilityLevelRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.AbilityListRowData>>(FlatBuffers.Offset<GameConfigs.AbilityListRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.ActivityTimeRowData>>(FlatBuffers.Offset<GameConfigs.ActivityTimeRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.AvatarRowData>>(FlatBuffers.Offset<GameConfigs.AvatarRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.BuffRowData>>(FlatBuffers.Offset<GameConfigs.BuffRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.BulletRowData>>(FlatBuffers.Offset<GameConfigs.BulletRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.ChestRowData>>(FlatBuffers.Offset<GameConfigs.ChestRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.CmdConfigRowData>>(FlatBuffers.Offset<GameConfigs.CmdConfigRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.Design_GlobalRowData>>(FlatBuffers.Offset<GameConfigs.Design_GlobalRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.EggRowData>>(FlatBuffers.Offset<GameConfigs.EggRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.EntryRowData>>(FlatBuffers.Offset<GameConfigs.EntryRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.EquipBuffRowData>>(FlatBuffers.Offset<GameConfigs.EquipBuffRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.EquipQualityRowData>>(FlatBuffers.Offset<GameConfigs.EquipQualityRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.EquipRowData>>(FlatBuffers.Offset<GameConfigs.EquipRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.EquipUpLevelCostRowData>>(FlatBuffers.Offset<GameConfigs.EquipUpLevelCostRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.EquipmentRowData>>(FlatBuffers.Offset<GameConfigs.EquipmentRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.EvolutionRowData>>(FlatBuffers.Offset<GameConfigs.EvolutionRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.ExchangeRowData>>(FlatBuffers.Offset<GameConfigs.ExchangeRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.FunctionConfigRowData>>(FlatBuffers.Offset<GameConfigs.FunctionConfigRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.HunterWheelRowData>>(FlatBuffers.Offset<GameConfigs.HunterWheelRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.ItemRowData>>(FlatBuffers.Offset<GameConfigs.ItemRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.Language_CHNRowData>>(FlatBuffers.Offset<GameConfigs.Language_CHNRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.Language_ENRowData>>(FlatBuffers.Offset<GameConfigs.Language_ENRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.Language_HIRowData>>(FlatBuffers.Offset<GameConfigs.Language_HIRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.Language_settingRowData>>(FlatBuffers.Offset<GameConfigs.Language_settingRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.LevelRowData>>(FlatBuffers.Offset<GameConfigs.LevelRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.MachineRowData>>(FlatBuffers.Offset<GameConfigs.MachineRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.MachineStarRowData>>(FlatBuffers.Offset<GameConfigs.MachineStarRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.MachineUpgradeRowData>>(FlatBuffers.Offset<GameConfigs.MachineUpgradeRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.MerchantMissionRowData>>(FlatBuffers.Offset<GameConfigs.MerchantMissionRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.MerchantRewardRowData>>(FlatBuffers.Offset<GameConfigs.MerchantRewardRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.MonsterHunterRowData>>(FlatBuffers.Offset<GameConfigs.MonsterHunterRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.PetsRowData>>(FlatBuffers.Offset<GameConfigs.PetsRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.ProgressPackRowData>>(FlatBuffers.Offset<GameConfigs.ProgressPackRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.RankConfigRowData>>(FlatBuffers.Offset<GameConfigs.RankConfigRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.RedConfigRowData>>(FlatBuffers.Offset<GameConfigs.RedConfigRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.RegionRowData>>(FlatBuffers.Offset<GameConfigs.RegionRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.RoleDataRowData>>(FlatBuffers.Offset<GameConfigs.RoleDataRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.RoomExclusiveRowData>>(FlatBuffers.Offset<GameConfigs.RoomExclusiveRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.RoomLikeRowData>>(FlatBuffers.Offset<GameConfigs.RoomLikeRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.RoomMachineRowData>>(FlatBuffers.Offset<GameConfigs.RoomMachineRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.RoomRowData>>(FlatBuffers.Offset<GameConfigs.RoomRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.RoomTechRowData>>(FlatBuffers.Offset<GameConfigs.RoomTechRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.SceneRowData>>(FlatBuffers.Offset<GameConfigs.SceneRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.SettingConfigRowData>>(FlatBuffers.Offset<GameConfigs.SettingConfigRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.ShopRowData>>(FlatBuffers.Offset<GameConfigs.ShopRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.Sound_EffectRowData>>(FlatBuffers.Offset<GameConfigs.Sound_EffectRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.effectsRowData>>(FlatBuffers.Offset<GameConfigs.effectsRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.fontsRowData>>(FlatBuffers.Offset<GameConfigs.fontsRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.friendRowData>>(FlatBuffers.Offset<GameConfigs.friendRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.game_globalRowData>>(FlatBuffers.Offset<GameConfigs.game_globalRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.languageRowData>>(FlatBuffers.Offset<GameConfigs.languageRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.roleRowData>>(FlatBuffers.Offset<GameConfigs.roleRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.tableRowData>>(FlatBuffers.Offset<GameConfigs.tableRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.ui_groupsRowData>>(FlatBuffers.Offset<GameConfigs.ui_groupsRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.ui_resRowData>>(FlatBuffers.Offset<GameConfigs.ui_resRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.StringOffset>(FlatBuffers.StringOffset[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<float>(float[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<int>(int[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.ADConfigRowData>>(FlatBuffers.Offset<GameConfigs.ADConfigRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.AbilityLevelRowData>>(FlatBuffers.Offset<GameConfigs.AbilityLevelRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.AbilityListRowData>>(FlatBuffers.Offset<GameConfigs.AbilityListRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.ActivityTimeRowData>>(FlatBuffers.Offset<GameConfigs.ActivityTimeRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.AvatarRowData>>(FlatBuffers.Offset<GameConfigs.AvatarRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.BuffRowData>>(FlatBuffers.Offset<GameConfigs.BuffRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.BulletRowData>>(FlatBuffers.Offset<GameConfigs.BulletRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.ChestRowData>>(FlatBuffers.Offset<GameConfigs.ChestRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.CmdConfigRowData>>(FlatBuffers.Offset<GameConfigs.CmdConfigRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.Design_GlobalRowData>>(FlatBuffers.Offset<GameConfigs.Design_GlobalRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.EggRowData>>(FlatBuffers.Offset<GameConfigs.EggRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.EntryRowData>>(FlatBuffers.Offset<GameConfigs.EntryRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.EquipBuffRowData>>(FlatBuffers.Offset<GameConfigs.EquipBuffRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.EquipQualityRowData>>(FlatBuffers.Offset<GameConfigs.EquipQualityRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.EquipRowData>>(FlatBuffers.Offset<GameConfigs.EquipRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.EquipUpLevelCostRowData>>(FlatBuffers.Offset<GameConfigs.EquipUpLevelCostRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.EquipmentRowData>>(FlatBuffers.Offset<GameConfigs.EquipmentRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.EvolutionRowData>>(FlatBuffers.Offset<GameConfigs.EvolutionRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.ExchangeRowData>>(FlatBuffers.Offset<GameConfigs.ExchangeRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.FunctionConfigRowData>>(FlatBuffers.Offset<GameConfigs.FunctionConfigRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.HunterWheelRowData>>(FlatBuffers.Offset<GameConfigs.HunterWheelRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.ItemRowData>>(FlatBuffers.Offset<GameConfigs.ItemRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.Language_CHNRowData>>(FlatBuffers.Offset<GameConfigs.Language_CHNRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.Language_ENRowData>>(FlatBuffers.Offset<GameConfigs.Language_ENRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.Language_HIRowData>>(FlatBuffers.Offset<GameConfigs.Language_HIRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.Language_settingRowData>>(FlatBuffers.Offset<GameConfigs.Language_settingRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.LevelRowData>>(FlatBuffers.Offset<GameConfigs.LevelRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.MachineRowData>>(FlatBuffers.Offset<GameConfigs.MachineRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.MachineStarRowData>>(FlatBuffers.Offset<GameConfigs.MachineStarRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.MachineUpgradeRowData>>(FlatBuffers.Offset<GameConfigs.MachineUpgradeRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.MerchantMissionRowData>>(FlatBuffers.Offset<GameConfigs.MerchantMissionRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.MerchantRewardRowData>>(FlatBuffers.Offset<GameConfigs.MerchantRewardRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.MonsterHunterRowData>>(FlatBuffers.Offset<GameConfigs.MonsterHunterRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.PetsRowData>>(FlatBuffers.Offset<GameConfigs.PetsRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.ProgressPackRowData>>(FlatBuffers.Offset<GameConfigs.ProgressPackRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.RankConfigRowData>>(FlatBuffers.Offset<GameConfigs.RankConfigRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.RedConfigRowData>>(FlatBuffers.Offset<GameConfigs.RedConfigRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.RegionRowData>>(FlatBuffers.Offset<GameConfigs.RegionRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.RoleDataRowData>>(FlatBuffers.Offset<GameConfigs.RoleDataRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.RoomExclusiveRowData>>(FlatBuffers.Offset<GameConfigs.RoomExclusiveRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.RoomLikeRowData>>(FlatBuffers.Offset<GameConfigs.RoomLikeRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.RoomMachineRowData>>(FlatBuffers.Offset<GameConfigs.RoomMachineRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.RoomRowData>>(FlatBuffers.Offset<GameConfigs.RoomRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.RoomTechRowData>>(FlatBuffers.Offset<GameConfigs.RoomTechRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.SceneRowData>>(FlatBuffers.Offset<GameConfigs.SceneRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.SettingConfigRowData>>(FlatBuffers.Offset<GameConfigs.SettingConfigRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.ShopRowData>>(FlatBuffers.Offset<GameConfigs.ShopRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.Sound_EffectRowData>>(FlatBuffers.Offset<GameConfigs.Sound_EffectRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.effectsRowData>>(FlatBuffers.Offset<GameConfigs.effectsRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.fontsRowData>>(FlatBuffers.Offset<GameConfigs.fontsRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.friendRowData>>(FlatBuffers.Offset<GameConfigs.friendRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.game_globalRowData>>(FlatBuffers.Offset<GameConfigs.game_globalRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.languageRowData>>(FlatBuffers.Offset<GameConfigs.languageRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.roleRowData>>(FlatBuffers.Offset<GameConfigs.roleRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.tableRowData>>(FlatBuffers.Offset<GameConfigs.tableRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.ui_groupsRowData>>(FlatBuffers.Offset<GameConfigs.ui_groupsRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.ui_resRowData>>(FlatBuffers.Offset<GameConfigs.ui_resRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.StringOffset>(FlatBuffers.StringOffset[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<float>(float[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<int>(int[])
		// byte[] FlatBuffers.Table.__vector_as_array<byte>(int)
		// float[] FlatBuffers.Table.__vector_as_array<float>(int)
		// int[] FlatBuffers.Table.__vector_as_array<int>(int)
		// object LitJson.JsonMapper.ToObject<object>(string)
		// bool SGame.EventDispatcher.addEventListener<GamePackage>(int,Callback<GamePackage>)
		// bool SGame.EventDispatcher.addEventListener<SGame.BuffData>(int,Callback<SGame.BuffData>)
		// bool SGame.EventDispatcher.addEventListener<Unity.Entities.Entity>(int,Callback<Unity.Entities.Entity>)
		// bool SGame.EventDispatcher.addEventListener<Unity.Mathematics.int2>(int,Callback<Unity.Mathematics.int2>)
		// bool SGame.EventDispatcher.addEventListener<UnityEngine.Vector2Int,object,object>(int,Callback<UnityEngine.Vector2Int,object,object>)
		// bool SGame.EventDispatcher.addEventListener<byte>(int,Callback<byte>)
		// bool SGame.EventDispatcher.addEventListener<double,double>(int,Callback<double,double>)
		// bool SGame.EventDispatcher.addEventListener<int,UnityEngine.Vector2,UnityEngine.Vector2,float>(int,Callback<int,UnityEngine.Vector2,UnityEngine.Vector2,float>)
		// bool SGame.EventDispatcher.addEventListener<int,float,object>(int,Callback<int,float,object>)
		// bool SGame.EventDispatcher.addEventListener<int,int,int,int>(int,Callback<int,int,int,int>)
		// bool SGame.EventDispatcher.addEventListener<int,int,int>(int,Callback<int,int,int>)
		// bool SGame.EventDispatcher.addEventListener<int,int,object>(int,Callback<int,int,object>)
		// bool SGame.EventDispatcher.addEventListener<int,int>(int,Callback<int,int>)
		// bool SGame.EventDispatcher.addEventListener<int,object>(int,Callback<int,object>)
		// bool SGame.EventDispatcher.addEventListener<int>(int,Callback<int>)
		// bool SGame.EventDispatcher.addEventListener<object,byte>(int,Callback<object,byte>)
		// bool SGame.EventDispatcher.addEventListener<object,int,int,int,int>(int,Callback<object,int,int,int,int>)
		// bool SGame.EventDispatcher.addEventListener<object,int>(int,Callback<object,int>)
		// bool SGame.EventDispatcher.addEventListener<object,object,UnityEngine.Vector2,float>(int,Callback<object,object,UnityEngine.Vector2,float>)
		// bool SGame.EventDispatcher.addEventListener<object,object>(int,Callback<object,object>)
		// bool SGame.EventDispatcher.addEventListener<object>(int,Callback<object>)
		// System.Void SGame.EventDispatcher.dispatchEvent<GamePackage>(int,GamePackage)
		// System.Void SGame.EventDispatcher.dispatchEvent<SGame.BuffData>(int,SGame.BuffData)
		// System.Void SGame.EventDispatcher.dispatchEvent<Unity.Entities.Entity>(int,Unity.Entities.Entity)
		// System.Void SGame.EventDispatcher.dispatchEvent<UnityEngine.Vector2Int,object,object>(int,UnityEngine.Vector2Int,object,object)
		// System.Void SGame.EventDispatcher.dispatchEvent<byte>(int,byte)
		// System.Void SGame.EventDispatcher.dispatchEvent<double,double>(int,double,double)
		// System.Void SGame.EventDispatcher.dispatchEvent<int,UnityEngine.Vector2,UnityEngine.Vector2,float>(int,int,UnityEngine.Vector2,UnityEngine.Vector2,float)
		// System.Void SGame.EventDispatcher.dispatchEvent<int,int,int,int>(int,int,int,int,int)
		// System.Void SGame.EventDispatcher.dispatchEvent<int,int,int>(int,int,int,int)
		// System.Void SGame.EventDispatcher.dispatchEvent<int,int>(int,int,int)
		// System.Void SGame.EventDispatcher.dispatchEvent<int,object,float>(int,int,object,float)
		// System.Void SGame.EventDispatcher.dispatchEvent<int,object>(int,int,object)
		// System.Void SGame.EventDispatcher.dispatchEvent<int>(int,int)
		// System.Void SGame.EventDispatcher.dispatchEvent<object,byte>(int,object,byte)
		// System.Void SGame.EventDispatcher.dispatchEvent<object,int,int,int,int>(int,object,int,int,int,int)
		// System.Void SGame.EventDispatcher.dispatchEvent<object,int>(int,object,int)
		// System.Void SGame.EventDispatcher.dispatchEvent<object,object,UnityEngine.Vector2,float>(int,object,object,UnityEngine.Vector2,float)
		// System.Void SGame.EventDispatcher.dispatchEvent<object>(int,object)
		// System.Void SGame.EventDispatcher.removeEventListener<double,double>(int,Callback<double,double>)
		// System.Void SGame.EventDispatcher.removeEventListener<int,int>(int,Callback<int,int>)
		// System.Void SGame.EventDispatcher.removeEventListener<int>(int,Callback<int>)
		// System.Void SGame.EventDispatcher.removeEventListener<object,byte>(int,Callback<object,byte>)
		// System.Void SGame.EventDispatcher.removeEventListener<object,int>(int,Callback<object,int>)
		// SGame.EventHanle SGame.EventDispatcherEx.Reg<GamePackage>(int,Callback<GamePackage>)
		// SGame.EventHanle SGame.EventDispatcherEx.Reg<SGame.BuffData>(int,Callback<SGame.BuffData>)
		// SGame.EventHanle SGame.EventDispatcherEx.Reg<Unity.Entities.Entity>(int,Callback<Unity.Entities.Entity>)
		// SGame.EventHanle SGame.EventDispatcherEx.Reg<Unity.Mathematics.int2>(int,Callback<Unity.Mathematics.int2>)
		// SGame.EventHanle SGame.EventDispatcherEx.Reg<UnityEngine.Vector2Int,object,object>(int,Callback<UnityEngine.Vector2Int,object,object>)
		// SGame.EventHanle SGame.EventDispatcherEx.Reg<byte>(int,Callback<byte>)
		// SGame.EventHanle SGame.EventDispatcherEx.Reg<double,double>(int,Callback<double,double>)
		// SGame.EventHanle SGame.EventDispatcherEx.Reg<int,UnityEngine.Vector2,UnityEngine.Vector2,float>(int,Callback<int,UnityEngine.Vector2,UnityEngine.Vector2,float>)
		// SGame.EventHanle SGame.EventDispatcherEx.Reg<int,float,object>(int,Callback<int,float,object>)
		// SGame.EventHanle SGame.EventDispatcherEx.Reg<int,int,int,int>(int,Callback<int,int,int,int>)
		// SGame.EventHanle SGame.EventDispatcherEx.Reg<int,int,int>(int,Callback<int,int,int>)
		// SGame.EventHanle SGame.EventDispatcherEx.Reg<int,int,object>(int,Callback<int,int,object>)
		// SGame.EventHanle SGame.EventDispatcherEx.Reg<int,int>(int,Callback<int,int>)
		// SGame.EventHanle SGame.EventDispatcherEx.Reg<int,object>(int,Callback<int,object>)
		// SGame.EventHanle SGame.EventDispatcherEx.Reg<int>(int,Callback<int>)
		// SGame.EventHanle SGame.EventDispatcherEx.Reg<object,byte>(int,Callback<object,byte>)
		// SGame.EventHanle SGame.EventDispatcherEx.Reg<object,int,int,int,int>(int,Callback<object,int,int,int,int>)
		// SGame.EventHanle SGame.EventDispatcherEx.Reg<object,int>(int,Callback<object,int>)
		// SGame.EventHanle SGame.EventDispatcherEx.Reg<object,object,UnityEngine.Vector2,float>(int,Callback<object,object,UnityEngine.Vector2,float>)
		// SGame.EventHanle SGame.EventDispatcherEx.Reg<object,object>(int,Callback<object,object>)
		// SGame.EventHanle SGame.EventDispatcherEx.Reg<object>(int,Callback<object>)
		// System.Void SGame.EventDispatcherEx.Trigger<GamePackage>(int,GamePackage)
		// System.Void SGame.EventDispatcherEx.Trigger<SGame.BuffData>(int,SGame.BuffData)
		// System.Void SGame.EventDispatcherEx.Trigger<Unity.Entities.Entity>(int,Unity.Entities.Entity)
		// System.Void SGame.EventDispatcherEx.Trigger<UnityEngine.Vector2Int,object,object>(int,UnityEngine.Vector2Int,object,object)
		// System.Void SGame.EventDispatcherEx.Trigger<byte>(int,byte)
		// System.Void SGame.EventDispatcherEx.Trigger<double,double>(int,double,double)
		// System.Void SGame.EventDispatcherEx.Trigger<int,UnityEngine.Vector2,UnityEngine.Vector2,float>(int,int,UnityEngine.Vector2,UnityEngine.Vector2,float)
		// System.Void SGame.EventDispatcherEx.Trigger<int,int,int,int>(int,int,int,int,int)
		// System.Void SGame.EventDispatcherEx.Trigger<int,int,int>(int,int,int,int)
		// System.Void SGame.EventDispatcherEx.Trigger<int,int>(int,int,int)
		// System.Void SGame.EventDispatcherEx.Trigger<int,object,float>(int,int,object,float)
		// System.Void SGame.EventDispatcherEx.Trigger<int,object>(int,int,object)
		// System.Void SGame.EventDispatcherEx.Trigger<int>(int,int)
		// System.Void SGame.EventDispatcherEx.Trigger<object,byte>(int,object,byte)
		// System.Void SGame.EventDispatcherEx.Trigger<object,int,int,int,int>(int,object,int,int,int,int)
		// System.Void SGame.EventDispatcherEx.Trigger<object,int>(int,object,int)
		// System.Void SGame.EventDispatcherEx.Trigger<object,object,UnityEngine.Vector2,float>(int,object,object,UnityEngine.Vector2,float)
		// System.Void SGame.EventDispatcherEx.Trigger<object>(int,object)
		// System.Void SGame.EventDispatcherEx.UnReg<double,double>(int,Callback<double,double>)
		// System.Void SGame.EventDispatcherEx.UnReg<int,int>(int,Callback<int,int>)
		// System.Void SGame.EventDispatcherEx.UnReg<int>(int,Callback<int>)
		// System.Void SGame.EventDispatcherEx.UnReg<object,byte>(int,Callback<object,byte>)
		// System.Void SGame.EventDispatcherEx.UnReg<object,int>(int,Callback<object,int>)
		// SGame.EventHanle SGame.EventManager.Reg<SGame.BuffData>(int,Callback<SGame.BuffData>)
		// SGame.EventHanle SGame.EventManager.Reg<Unity.Entities.Entity>(int,Callback<Unity.Entities.Entity>)
		// SGame.EventHanle SGame.EventManager.Reg<Unity.Mathematics.int2>(int,Callback<Unity.Mathematics.int2>)
		// SGame.EventHanle SGame.EventManager.Reg<UnityEngine.Vector2Int,object,object>(int,Callback<UnityEngine.Vector2Int,object,object>)
		// SGame.EventHanle SGame.EventManager.Reg<byte>(int,Callback<byte>)
		// SGame.EventHanle SGame.EventManager.Reg<double,double>(int,Callback<double,double>)
		// SGame.EventHanle SGame.EventManager.Reg<int,UnityEngine.Vector2,UnityEngine.Vector2,float>(int,Callback<int,UnityEngine.Vector2,UnityEngine.Vector2,float>)
		// SGame.EventHanle SGame.EventManager.Reg<int,float,object>(int,Callback<int,float,object>)
		// SGame.EventHanle SGame.EventManager.Reg<int,int,int,int>(int,Callback<int,int,int,int>)
		// SGame.EventHanle SGame.EventManager.Reg<int,int,int>(int,Callback<int,int,int>)
		// SGame.EventHanle SGame.EventManager.Reg<int,int,object>(int,Callback<int,int,object>)
		// SGame.EventHanle SGame.EventManager.Reg<int,int>(int,Callback<int,int>)
		// SGame.EventHanle SGame.EventManager.Reg<int>(int,Callback<int>)
		// SGame.EventHanle SGame.EventManager.Reg<object,byte>(int,Callback<object,byte>)
		// SGame.EventHanle SGame.EventManager.Reg<object,int,int,int,int>(int,Callback<object,int,int,int,int>)
		// SGame.EventHanle SGame.EventManager.Reg<object,int>(int,Callback<object,int>)
		// SGame.EventHanle SGame.EventManager.Reg<object,object,UnityEngine.Vector2,float>(int,Callback<object,object,UnityEngine.Vector2,float>)
		// SGame.EventHanle SGame.EventManager.Reg<object,object>(int,Callback<object,object>)
		// SGame.EventHanle SGame.EventManager.Reg<object>(int,Callback<object>)
		// System.Void SGame.EventManager.Trigger<SGame.BuffData>(int,SGame.BuffData)
		// System.Void SGame.EventManager.Trigger<Unity.Entities.Entity>(int,Unity.Entities.Entity)
		// System.Void SGame.EventManager.Trigger<UnityEngine.Vector2Int,object,object>(int,UnityEngine.Vector2Int,object,object)
		// System.Void SGame.EventManager.Trigger<byte>(int,byte)
		// System.Void SGame.EventManager.Trigger<double,double>(int,double,double)
		// System.Void SGame.EventManager.Trigger<int,UnityEngine.Vector2,UnityEngine.Vector2,float>(int,int,UnityEngine.Vector2,UnityEngine.Vector2,float)
		// System.Void SGame.EventManager.Trigger<int,int,int,int>(int,int,int,int,int)
		// System.Void SGame.EventManager.Trigger<int,int,int>(int,int,int,int)
		// System.Void SGame.EventManager.Trigger<int,int>(int,int,int)
		// System.Void SGame.EventManager.Trigger<int,object,float>(int,int,object,float)
		// System.Void SGame.EventManager.Trigger<int>(int,int)
		// System.Void SGame.EventManager.Trigger<object,byte>(int,object,byte)
		// System.Void SGame.EventManager.Trigger<object,int,int,int,int>(int,object,int,int,int,int)
		// System.Void SGame.EventManager.Trigger<object,int>(int,object,int)
		// System.Void SGame.EventManager.Trigger<object,object,UnityEngine.Vector2,float>(int,object,object,UnityEngine.Vector2,float)
		// System.Void SGame.EventManager.Trigger<object>(int,object)
		// System.Void SGame.EventManager.UnReg<double,double>(int,Callback<double,double>)
		// System.Void SGame.EventManager.UnReg<int,int>(int,Callback<int,int>)
		// System.Void SGame.EventManager.UnReg<int>(int,Callback<int>)
		// System.Void SGame.EventManager.UnReg<object,byte>(int,Callback<object,byte>)
		// System.Void SGame.EventManager.UnReg<object,int>(int,Callback<object,int>)
		// GameConfigs.PetsRowData SGame.Randoms.Random.NextItem<GameConfigs.PetsRowData>(System.Collections.Generic.IList<GameConfigs.PetsRowData>)
		// GameConfigs.PetsRowData SGame.Randoms.Random.NextItem<GameConfigs.PetsRowData>(System.Collections.Generic.IList<GameConfigs.PetsRowData>,int&)
		// SGame.Worker SGame.Randoms.Random.NextItem<SGame.Worker>(System.Collections.Generic.IList<SGame.Worker>,int&)
		// System.Void SGame.Randoms.Random.NextItem<GameConfigs.EquipmentRowData>(System.Collections.Generic.IList<GameConfigs.EquipmentRowData>,int,System.Collections.Generic.List<GameConfigs.EquipmentRowData>&,bool)
		// System.Void SGame.Randoms.Random.NextItem<GameConfigs.RoomExclusiveRowData>(System.Collections.Generic.IList<GameConfigs.RoomExclusiveRowData>,int,System.Collections.Generic.List<GameConfigs.RoomExclusiveRowData>&,bool)
		// System.Void SGame.Randoms.Random.NextItem<GameConfigs.RoomLikeRowData>(System.Collections.Generic.IList<GameConfigs.RoomLikeRowData>,int,System.Collections.Generic.List<GameConfigs.RoomLikeRowData>&,bool)
		// System.Void SGame.Randoms.Random.NextItem<UnityEngine.Vector2Int>(System.Collections.Generic.IList<UnityEngine.Vector2Int>,int,System.Collections.Generic.List<UnityEngine.Vector2Int>&,bool)
		// object System.Activator.CreateInstance<object>()
		// byte[] System.Array.Empty<byte>()
		// double[] System.Array.Empty<double>()
		// int[] System.Array.Empty<int>()
		// object[] System.Array.Empty<object>()
		// System.Void System.Array.Fill<int>(int[],int)
		// System.Void System.Array.ForEach<object>(object[],System.Action<object>)
		// bool System.Collections.Generic.CollectionExtensions.TryAdd<int,object>(System.Collections.Generic.IDictionary<int,object>,int,object)
		// bool System.Enum.TryParse<int>(string,bool,int&)
		// bool System.Enum.TryParse<int>(string,int&)
		// bool System.Enum.TryParse<object>(string,bool,object&)
		// bool System.Enum.TryParse<object>(string,object&)
		// System.Collections.Generic.KeyValuePair<int,float> System.Linq.Enumerable.Aggregate<System.Collections.Generic.KeyValuePair<int,float>>(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,float>>,System.Func<System.Collections.Generic.KeyValuePair<int,float>,System.Collections.Generic.KeyValuePair<int,float>,System.Collections.Generic.KeyValuePair<int,float>>)
		// bool System.Linq.Enumerable.All<Unity.Entities.Entity>(System.Collections.Generic.IEnumerable<Unity.Entities.Entity>,System.Func<Unity.Entities.Entity,bool>)
		// bool System.Linq.Enumerable.All<object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,bool>)
		// bool System.Linq.Enumerable.Any<GameConfigs.ActivityTimeRowData>(System.Collections.Generic.IEnumerable<GameConfigs.ActivityTimeRowData>,System.Func<GameConfigs.ActivityTimeRowData,bool>)
		// bool System.Linq.Enumerable.Any<GameConfigs.RoomTechRowData>(System.Collections.Generic.IEnumerable<GameConfigs.RoomTechRowData>,System.Func<GameConfigs.RoomTechRowData,bool>)
		// bool System.Linq.Enumerable.Any<Unity.Entities.Entity>(System.Collections.Generic.IEnumerable<Unity.Entities.Entity>,System.Func<Unity.Entities.Entity,bool>)
		// bool System.Linq.Enumerable.Any<object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,bool>)
		// bool System.Linq.Enumerable.Contains<object>(System.Collections.Generic.IEnumerable<object>,object)
		// bool System.Linq.Enumerable.Contains<object>(System.Collections.Generic.IEnumerable<object>,object,System.Collections.Generic.IEqualityComparer<object>)
		// int System.Linq.Enumerable.Count<int>(System.Collections.Generic.IEnumerable<int>)
		// int System.Linq.Enumerable.Count<object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,bool>)
		// System.Collections.Generic.KeyValuePair<int,object> System.Linq.Enumerable.First<System.Collections.Generic.KeyValuePair<int,object>>(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,object>>)
		// int System.Linq.Enumerable.First<int>(System.Collections.Generic.IEnumerable<int>)
		// SGame.ItemData.Value System.Linq.Enumerable.FirstOrDefault<SGame.ItemData.Value>(System.Collections.Generic.IEnumerable<SGame.ItemData.Value>,System.Func<SGame.ItemData.Value,bool>)
		// object System.Linq.Enumerable.FirstOrDefault<object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,bool>)
		// System.Collections.Generic.IEnumerable<System.Linq.IGrouping<int,GameConfigs.RoomMachineRowData>> System.Linq.Enumerable.GroupBy<GameConfigs.RoomMachineRowData,int>(System.Collections.Generic.IEnumerable<GameConfigs.RoomMachineRowData>,System.Func<GameConfigs.RoomMachineRowData,int>)
		// System.Collections.Generic.IEnumerable<System.Linq.IGrouping<int,SGame.ItemData.Value>> System.Linq.Enumerable.GroupBy<SGame.ItemData.Value,int>(System.Collections.Generic.IEnumerable<SGame.ItemData.Value>,System.Func<SGame.ItemData.Value,int>)
		// System.Collections.Generic.IEnumerable<System.Linq.IGrouping<int,int>> System.Linq.Enumerable.GroupBy<int,int>(System.Collections.Generic.IEnumerable<int>,System.Func<int,int>)
		// System.Collections.Generic.IEnumerable<System.Linq.IGrouping<int,object>> System.Linq.Enumerable.GroupBy<object,int>(System.Collections.Generic.IEnumerable<object>,System.Func<object,int>)
		// System.Collections.Generic.IEnumerable<System.Linq.IGrouping<uint,SGame.ItemData.Value>> System.Linq.Enumerable.GroupBy<SGame.ItemData.Value,uint>(System.Collections.Generic.IEnumerable<SGame.ItemData.Value>,System.Func<SGame.ItemData.Value,uint>)
		// object System.Linq.Enumerable.Last<object>(System.Collections.Generic.IEnumerable<object>)
		// SGame.DelayExcuter.Item System.Linq.Enumerable.LastOrDefault<SGame.DelayExcuter.Item>(System.Collections.Generic.IEnumerable<SGame.DelayExcuter.Item>)
		// System.Collections.Generic.IEnumerable<SGame.BuffData> System.Linq.Enumerable.Select<object,SGame.BuffData>(System.Collections.Generic.IEnumerable<object>,System.Func<object,SGame.BuffData>)
		// System.Collections.Generic.IEnumerable<SGame.ItemData.Value> System.Linq.Enumerable.Select<GameConfigs.ItemRowData,SGame.ItemData.Value>(System.Collections.Generic.IEnumerable<GameConfigs.ItemRowData>,System.Func<GameConfigs.ItemRowData,SGame.ItemData.Value>)
		// System.Collections.Generic.IEnumerable<SGame.ItemData.Value> System.Linq.Enumerable.Select<int,SGame.ItemData.Value>(System.Collections.Generic.IEnumerable<int>,System.Func<int,SGame.ItemData.Value>)
		// System.Collections.Generic.IEnumerable<SGame.ItemData.Value> System.Linq.Enumerable.Select<object,SGame.ItemData.Value>(System.Collections.Generic.IEnumerable<object>,System.Func<object,SGame.ItemData.Value>)
		// System.Collections.Generic.IEnumerable<UnityEngine.Vector2Int> System.Linq.Enumerable.Select<int,UnityEngine.Vector2Int>(System.Collections.Generic.IEnumerable<int>,System.Func<int,UnityEngine.Vector2Int>)
		// System.Collections.Generic.IEnumerable<double> System.Linq.Enumerable.Select<SGame.ItemData.Value,double>(System.Collections.Generic.IEnumerable<SGame.ItemData.Value>,System.Func<SGame.ItemData.Value,double>)
		// System.Collections.Generic.IEnumerable<int> System.Linq.Enumerable.Select<GameConfigs.HunterWheelRowData,int>(System.Collections.Generic.IEnumerable<GameConfigs.HunterWheelRowData>,System.Func<GameConfigs.HunterWheelRowData,int>)
		// System.Collections.Generic.IEnumerable<int> System.Linq.Enumerable.Select<GameConfigs.ItemRowData,int>(System.Collections.Generic.IEnumerable<GameConfigs.ItemRowData>,System.Func<GameConfigs.ItemRowData,int>)
		// System.Collections.Generic.IEnumerable<int> System.Linq.Enumerable.Select<GameConfigs.RoomExclusiveRowData,int>(System.Collections.Generic.IEnumerable<GameConfigs.RoomExclusiveRowData>,System.Func<GameConfigs.RoomExclusiveRowData,int>)
		// System.Collections.Generic.IEnumerable<int> System.Linq.Enumerable.Select<GameConfigs.RoomLikeRowData,int>(System.Collections.Generic.IEnumerable<GameConfigs.RoomLikeRowData>,System.Func<GameConfigs.RoomLikeRowData,int>)
		// System.Collections.Generic.IEnumerable<int> System.Linq.Enumerable.Select<object,int>(System.Collections.Generic.IEnumerable<object>,System.Func<object,int>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Select<GameConfigs.ItemRowData,object>(System.Collections.Generic.IEnumerable<GameConfigs.ItemRowData>,System.Func<GameConfigs.ItemRowData,object>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Select<GameConfigs.MonsterHunterRowData,object>(System.Collections.Generic.IEnumerable<GameConfigs.MonsterHunterRowData>,System.Func<GameConfigs.MonsterHunterRowData,object>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Select<GameConfigs.RoomMachineRowData,object>(System.Collections.Generic.IEnumerable<GameConfigs.RoomMachineRowData>,System.Func<GameConfigs.RoomMachineRowData,object>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Select<System.Collections.Generic.KeyValuePair<int,int>,object>(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,int>>,System.Func<System.Collections.Generic.KeyValuePair<int,int>,object>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Select<int,object>(System.Collections.Generic.IEnumerable<int>,System.Func<int,object>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Select<object,object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,object>)
		// double System.Linq.Enumerable.Sum<SGame.ItemData.Value>(System.Collections.Generic.IEnumerable<SGame.ItemData.Value>,System.Func<SGame.ItemData.Value,double>)
		// int System.Linq.Enumerable.Sum<object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,int>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Take<object>(System.Collections.Generic.IEnumerable<object>,int)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.TakeIterator<object>(System.Collections.Generic.IEnumerable<object>,int)
		// int[] System.Linq.Enumerable.ToArray<int>(System.Collections.Generic.IEnumerable<int>)
		// object[] System.Linq.Enumerable.ToArray<object>(System.Collections.Generic.IEnumerable<object>)
		// System.Collections.Generic.Dictionary<int,GameConfigs.RoomTechRowData> System.Linq.Enumerable.ToDictionary<GameConfigs.RoomTechRowData,int,GameConfigs.RoomTechRowData>(System.Collections.Generic.IEnumerable<GameConfigs.RoomTechRowData>,System.Func<GameConfigs.RoomTechRowData,int>,System.Func<GameConfigs.RoomTechRowData,GameConfigs.RoomTechRowData>,System.Collections.Generic.IEqualityComparer<int>)
		// System.Collections.Generic.Dictionary<int,GameConfigs.RoomTechRowData> System.Linq.Enumerable.ToDictionary<GameConfigs.RoomTechRowData,int>(System.Collections.Generic.IEnumerable<GameConfigs.RoomTechRowData>,System.Func<GameConfigs.RoomTechRowData,int>)
		// System.Collections.Generic.Dictionary<int,int> System.Linq.Enumerable.ToDictionary<object,int,int>(System.Collections.Generic.IEnumerable<object>,System.Func<object,int>,System.Func<object,int>)
		// System.Collections.Generic.Dictionary<int,int> System.Linq.Enumerable.ToDictionary<object,int,int>(System.Collections.Generic.IEnumerable<object>,System.Func<object,int>,System.Func<object,int>,System.Collections.Generic.IEqualityComparer<int>)
		// System.Collections.Generic.Dictionary<int,object> System.Linq.Enumerable.ToDictionary<object,int,object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,int>,System.Func<object,object>,System.Collections.Generic.IEqualityComparer<int>)
		// System.Collections.Generic.Dictionary<int,object> System.Linq.Enumerable.ToDictionary<object,int>(System.Collections.Generic.IEnumerable<object>,System.Func<object,int>)
		// System.Collections.Generic.Dictionary<object,object> System.Linq.Enumerable.ToDictionary<object,object,object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,object>,System.Func<object,object>)
		// System.Collections.Generic.Dictionary<object,object> System.Linq.Enumerable.ToDictionary<object,object,object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,object>,System.Func<object,object>,System.Collections.Generic.IEqualityComparer<object>)
		// System.Collections.Generic.Dictionary<uint,object> System.Linq.Enumerable.ToDictionary<object,uint,object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,uint>,System.Func<object,object>)
		// System.Collections.Generic.Dictionary<uint,object> System.Linq.Enumerable.ToDictionary<object,uint,object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,uint>,System.Func<object,object>,System.Collections.Generic.IEqualityComparer<uint>)
		// System.Collections.Generic.List<SGame.BuffData> System.Linq.Enumerable.ToList<SGame.BuffData>(System.Collections.Generic.IEnumerable<SGame.BuffData>)
		// System.Collections.Generic.List<SGame.ItemData.Value> System.Linq.Enumerable.ToList<SGame.ItemData.Value>(System.Collections.Generic.IEnumerable<SGame.ItemData.Value>)
		// System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<object,object>> System.Linq.Enumerable.ToList<System.Collections.Generic.KeyValuePair<object,object>>(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,object>>)
		// System.Collections.Generic.List<UnityEngine.Vector2Int> System.Linq.Enumerable.ToList<UnityEngine.Vector2Int>(System.Collections.Generic.IEnumerable<UnityEngine.Vector2Int>)
		// System.Collections.Generic.List<int> System.Linq.Enumerable.ToList<int>(System.Collections.Generic.IEnumerable<int>)
		// System.Collections.Generic.List<object> System.Linq.Enumerable.ToList<object>(System.Collections.Generic.IEnumerable<object>)
		// System.Collections.Generic.IEnumerable<GameConfigs.RoomMachineRowData> System.Linq.Enumerable.Where<GameConfigs.RoomMachineRowData>(System.Collections.Generic.IEnumerable<GameConfigs.RoomMachineRowData>,System.Func<GameConfigs.RoomMachineRowData,bool>)
		// System.Collections.Generic.IEnumerable<SGame.ItemData.Value> System.Linq.Enumerable.Where<SGame.ItemData.Value>(System.Collections.Generic.IEnumerable<SGame.ItemData.Value>,System.Func<SGame.ItemData.Value,bool>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Where<object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,bool>)
		// System.Collections.Generic.IEnumerable<SGame.BuffData> System.Linq.Enumerable.Iterator<object>.Select<SGame.BuffData>(System.Func<object,SGame.BuffData>)
		// System.Collections.Generic.IEnumerable<SGame.ItemData.Value> System.Linq.Enumerable.Iterator<GameConfigs.ItemRowData>.Select<SGame.ItemData.Value>(System.Func<GameConfigs.ItemRowData,SGame.ItemData.Value>)
		// System.Collections.Generic.IEnumerable<SGame.ItemData.Value> System.Linq.Enumerable.Iterator<int>.Select<SGame.ItemData.Value>(System.Func<int,SGame.ItemData.Value>)
		// System.Collections.Generic.IEnumerable<SGame.ItemData.Value> System.Linq.Enumerable.Iterator<object>.Select<SGame.ItemData.Value>(System.Func<object,SGame.ItemData.Value>)
		// System.Collections.Generic.IEnumerable<UnityEngine.Vector2Int> System.Linq.Enumerable.Iterator<int>.Select<UnityEngine.Vector2Int>(System.Func<int,UnityEngine.Vector2Int>)
		// System.Collections.Generic.IEnumerable<double> System.Linq.Enumerable.Iterator<SGame.ItemData.Value>.Select<double>(System.Func<SGame.ItemData.Value,double>)
		// System.Collections.Generic.IEnumerable<int> System.Linq.Enumerable.Iterator<GameConfigs.HunterWheelRowData>.Select<int>(System.Func<GameConfigs.HunterWheelRowData,int>)
		// System.Collections.Generic.IEnumerable<int> System.Linq.Enumerable.Iterator<GameConfigs.ItemRowData>.Select<int>(System.Func<GameConfigs.ItemRowData,int>)
		// System.Collections.Generic.IEnumerable<int> System.Linq.Enumerable.Iterator<GameConfigs.RoomExclusiveRowData>.Select<int>(System.Func<GameConfigs.RoomExclusiveRowData,int>)
		// System.Collections.Generic.IEnumerable<int> System.Linq.Enumerable.Iterator<GameConfigs.RoomLikeRowData>.Select<int>(System.Func<GameConfigs.RoomLikeRowData,int>)
		// System.Collections.Generic.IEnumerable<int> System.Linq.Enumerable.Iterator<object>.Select<int>(System.Func<object,int>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Iterator<GameConfigs.ItemRowData>.Select<object>(System.Func<GameConfigs.ItemRowData,object>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Iterator<GameConfigs.MonsterHunterRowData>.Select<object>(System.Func<GameConfigs.MonsterHunterRowData,object>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Iterator<GameConfigs.RoomMachineRowData>.Select<object>(System.Func<GameConfigs.RoomMachineRowData,object>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Iterator<System.Collections.Generic.KeyValuePair<int,int>>.Select<object>(System.Func<System.Collections.Generic.KeyValuePair<int,int>,object>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Iterator<int>.Select<object>(System.Func<int,object>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Iterator<object>.Select<object>(System.Func<object,object>)
		// object& System.Runtime.CompilerServices.Unsafe.As<object,object>(object&)
		// System.Void* System.Runtime.CompilerServices.Unsafe.AsPointer<object>(object&)
		// Unity.Collections.NativeArray<int> Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(System.Void*,int,Unity.Collections.Allocator)
		// System.Void* Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf<SGame.SoundTime>(SGame.SoundTime&)
		// System.Void* Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf<Unity.Entities.JobEntityBatchExtensions.JobEntityBatchWrapper<SGame.FollowRotationSystem.FollowRotationSystem_LambdaJob_0_Job>>(Unity.Entities.JobEntityBatchExtensions.JobEntityBatchWrapper<SGame.FollowRotationSystem.FollowRotationSystem_LambdaJob_0_Job>&)
		// System.Void* Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf<Unity.Entities.JobEntityBatchExtensions.JobEntityBatchWrapper<SGame.FollowRotationSystem.FollowRotationSystem_LambdaJob_1_Job>>(Unity.Entities.JobEntityBatchExtensions.JobEntityBatchWrapper<SGame.FollowRotationSystem.FollowRotationSystem_LambdaJob_1_Job>&)
		// System.Void* Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf<Unity.Entities.JobEntityBatchExtensions.JobEntityBatchWrapper<SGame.RewardSystem.RewardSystem_LambdaJob_1_Job>>(Unity.Entities.JobEntityBatchExtensions.JobEntityBatchWrapper<SGame.RewardSystem.RewardSystem_LambdaJob_1_Job>&)
		// System.Void* Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf<Unity.Entities.JobEntityBatchIndexExtensions.JobEntityBatchIndexWrapper<SGame.MoveTargetSystem.MoveTargetSystem_LambdaJob_0_Job>>(Unity.Entities.JobEntityBatchIndexExtensions.JobEntityBatchIndexWrapper<SGame.MoveTargetSystem.MoveTargetSystem_LambdaJob_0_Job>&)
		// System.Void* Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf<Unity.Entities.PrefilterForJobEntityBatchWithIndex>(Unity.Entities.PrefilterForJobEntityBatchWithIndex&)
		// Follow& Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<Follow>(System.Void*)
		// SGame.BulletData& Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<SGame.BulletData>(System.Void*)
		// SGame.CharacterAttribue& Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<SGame.CharacterAttribue>(System.Void*)
		// SGame.FoodTips& Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<SGame.FoodTips>(System.Void*)
		// SGame.LastRotation& Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<SGame.LastRotation>(System.Void*)
		// SGame.MoveTarget& Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<SGame.MoveTarget>(System.Void*)
		// SGame.RedEvent& Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<SGame.RedEvent>(System.Void*)
		// SGame.Redpoint& Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<SGame.Redpoint>(System.Void*)
		// SGame.RewardData& Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<SGame.RewardData>(System.Void*)
		// SGame.RewardWait& Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<SGame.RewardWait>(System.Void*)
		// SGame.RotationSpeed& Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<SGame.RotationSpeed>(System.Void*)
		// SGame.UI.HUDFlowE& Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<SGame.UI.HUDFlowE>(System.Void*)
		// SGame.UserInput& Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<SGame.UserInput>(System.Void*)
		// Speed& Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<Speed>(System.Void*)
		// Unity.Transforms.Rotation& Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<Unity.Transforms.Rotation>(System.Void*)
		// Unity.Transforms.Translation& Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<Unity.Transforms.Translation>(System.Void*)
		// object& Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<object>(System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyPtrToStructure<Follow>(System.Void*,Follow&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyPtrToStructure<SGame.CharacterSpawnSystem.CharacterSpawn>(System.Void*,SGame.CharacterSpawnSystem.CharacterSpawn&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyPtrToStructure<SGame.EffectData>(System.Void*,SGame.EffectData&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyPtrToStructure<SGame.FoodItem>(System.Void*,SGame.FoodItem&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyPtrToStructure<SGame.FoodTips>(System.Void*,SGame.FoodTips&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyPtrToStructure<SGame.Redpoint>(System.Void*,SGame.Redpoint&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyPtrToStructure<SGame.SoundData>(System.Void*,SGame.SoundData&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyPtrToStructure<SGame.SoundTime>(System.Void*,SGame.SoundTime&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyPtrToStructure<SGame.SpawnLayer>(System.Void*,SGame.SpawnLayer&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyPtrToStructure<SGame.SpawnReq>(System.Void*,SGame.SpawnReq&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyPtrToStructure<SGame.UI.TweenTime>(System.Void*,SGame.UI.TweenTime&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyPtrToStructure<SGame.UserData>(System.Void*,SGame.UserData&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyPtrToStructure<SGame.UserInput>(System.Void*,SGame.UserInput&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyPtrToStructure<SGame.UserSetting>(System.Void*,SGame.UserSetting&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyPtrToStructure<Unity.Transforms.Rotation>(System.Void*,Unity.Transforms.Rotation&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyPtrToStructure<Unity.Transforms.Scale>(System.Void*,Unity.Transforms.Scale&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyPtrToStructure<Unity.Transforms.Translation>(System.Void*,Unity.Transforms.Translation&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyPtrToStructure<object>(System.Void*,object&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<FindPathParams>(FindPathParams&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.BulletData>(SGame.BulletData&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.CharacterAttribue>(SGame.CharacterAttribue&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.CharacterSpawnSystem.CharacterSpawn>(SGame.CharacterSpawnSystem.CharacterSpawn&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.EffectData>(SGame.EffectData&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.FoodItem>(SGame.FoodItem&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.FoodTips>(SGame.FoodTips&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.GameObjectSyncTag>(SGame.GameObjectSyncTag&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.LastRotation>(SGame.LastRotation&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.LiveTime>(SGame.LiveTime&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.MoveTarget>(SGame.MoveTarget&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.RedEvent>(SGame.RedEvent&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.Redpoint>(SGame.Redpoint&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.RewardData>(SGame.RewardData&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.RewardWait>(SGame.RewardWait&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.RotationSpeed>(SGame.RotationSpeed&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.SoundData>(SGame.SoundData&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.SoundRef>(SGame.SoundRef&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.SoundTime>(SGame.SoundTime&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.SpawnData>(SGame.SpawnData&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.SpawnLayer>(SGame.SpawnLayer&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.SpawnReq>(SGame.SpawnReq&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.UI.HUDFlowE>(SGame.UI.HUDFlowE&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.UI.TweenTime>(SGame.UI.TweenTime&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.UserData>(SGame.UserData&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.UserSetting>(SGame.UserSetting&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<Speed>(Speed&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<Unity.Transforms.LocalToWorld>(Unity.Transforms.LocalToWorld&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<Unity.Transforms.Parent>(Unity.Transforms.Parent&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<Unity.Transforms.Rotation>(Unity.Transforms.Rotation&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<Unity.Transforms.Translation>(Unity.Transforms.Translation&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<object>(object&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyPtrToStructure<Follow>(System.Void*,Follow&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyPtrToStructure<SGame.CharacterSpawnSystem.CharacterSpawn>(System.Void*,SGame.CharacterSpawnSystem.CharacterSpawn&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyPtrToStructure<SGame.EffectData>(System.Void*,SGame.EffectData&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyPtrToStructure<SGame.FoodItem>(System.Void*,SGame.FoodItem&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyPtrToStructure<SGame.FoodTips>(System.Void*,SGame.FoodTips&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyPtrToStructure<SGame.Redpoint>(System.Void*,SGame.Redpoint&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyPtrToStructure<SGame.SoundData>(System.Void*,SGame.SoundData&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyPtrToStructure<SGame.SoundTime>(System.Void*,SGame.SoundTime&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyPtrToStructure<SGame.SpawnLayer>(System.Void*,SGame.SpawnLayer&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyPtrToStructure<SGame.SpawnReq>(System.Void*,SGame.SpawnReq&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyPtrToStructure<SGame.UI.TweenTime>(System.Void*,SGame.UI.TweenTime&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyPtrToStructure<SGame.UserData>(System.Void*,SGame.UserData&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyPtrToStructure<SGame.UserInput>(System.Void*,SGame.UserInput&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyPtrToStructure<SGame.UserSetting>(System.Void*,SGame.UserSetting&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyPtrToStructure<Unity.Transforms.Rotation>(System.Void*,Unity.Transforms.Rotation&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyPtrToStructure<Unity.Transforms.Scale>(System.Void*,Unity.Transforms.Scale&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyPtrToStructure<Unity.Transforms.Translation>(System.Void*,Unity.Transforms.Translation&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyPtrToStructure<object>(System.Void*,object&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<FindPathParams>(FindPathParams&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.BulletData>(SGame.BulletData&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.CharacterAttribue>(SGame.CharacterAttribue&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.CharacterSpawnSystem.CharacterSpawn>(SGame.CharacterSpawnSystem.CharacterSpawn&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.EffectData>(SGame.EffectData&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.FoodItem>(SGame.FoodItem&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.FoodTips>(SGame.FoodTips&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.GameObjectSyncTag>(SGame.GameObjectSyncTag&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.LastRotation>(SGame.LastRotation&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.LiveTime>(SGame.LiveTime&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.MoveTarget>(SGame.MoveTarget&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.RedEvent>(SGame.RedEvent&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.Redpoint>(SGame.Redpoint&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.RewardData>(SGame.RewardData&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.RewardWait>(SGame.RewardWait&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.RotationSpeed>(SGame.RotationSpeed&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.SoundData>(SGame.SoundData&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.SoundRef>(SGame.SoundRef&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.SoundTime>(SGame.SoundTime&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.SpawnData>(SGame.SpawnData&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.SpawnLayer>(SGame.SpawnLayer&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.SpawnReq>(SGame.SpawnReq&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.UI.HUDFlowE>(SGame.UI.HUDFlowE&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.UI.TweenTime>(SGame.UI.TweenTime&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.UserData>(SGame.UserData&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.UserSetting>(SGame.UserSetting&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<Speed>(Speed&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<Unity.Transforms.LocalToWorld>(Unity.Transforms.LocalToWorld&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<Unity.Transforms.Parent>(Unity.Transforms.Parent&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<Unity.Transforms.Rotation>(Unity.Transforms.Rotation&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<Unity.Transforms.Translation>(Unity.Transforms.Translation&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<object>(object&,System.Void*)
		// int Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<SGame.RewardWait>()
		// int Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<SGame.SoundTime>()
		// int Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<SGame.SpawnReq>()
		// Unity.Entities.BufferAccessor<PathPositions> Unity.Entities.ArchetypeChunk.GetBufferAccessor<PathPositions>(Unity.Entities.BufferTypeHandle<PathPositions>)
		// System.Void* Unity.Entities.ArchetypeChunk.GetComponentDataPtrRO<Follow>(Unity.Entities.ComponentTypeHandle<Follow>&)
		// System.Void* Unity.Entities.ArchetypeChunk.GetComponentDataPtrRO<SGame.BulletData>(Unity.Entities.ComponentTypeHandle<SGame.BulletData>&)
		// System.Void* Unity.Entities.ArchetypeChunk.GetComponentDataPtrRO<SGame.CharacterAttribue>(Unity.Entities.ComponentTypeHandle<SGame.CharacterAttribue>&)
		// System.Void* Unity.Entities.ArchetypeChunk.GetComponentDataPtrRO<SGame.FoodTips>(Unity.Entities.ComponentTypeHandle<SGame.FoodTips>&)
		// System.Void* Unity.Entities.ArchetypeChunk.GetComponentDataPtrRO<SGame.LastRotation>(Unity.Entities.ComponentTypeHandle<SGame.LastRotation>&)
		// System.Void* Unity.Entities.ArchetypeChunk.GetComponentDataPtrRO<SGame.MoveTarget>(Unity.Entities.ComponentTypeHandle<SGame.MoveTarget>&)
		// System.Void* Unity.Entities.ArchetypeChunk.GetComponentDataPtrRO<SGame.RedEvent>(Unity.Entities.ComponentTypeHandle<SGame.RedEvent>&)
		// System.Void* Unity.Entities.ArchetypeChunk.GetComponentDataPtrRO<SGame.Redpoint>(Unity.Entities.ComponentTypeHandle<SGame.Redpoint>&)
		// System.Void* Unity.Entities.ArchetypeChunk.GetComponentDataPtrRO<SGame.RewardData>(Unity.Entities.ComponentTypeHandle<SGame.RewardData>&)
		// System.Void* Unity.Entities.ArchetypeChunk.GetComponentDataPtrRO<SGame.RewardWait>(Unity.Entities.ComponentTypeHandle<SGame.RewardWait>&)
		// System.Void* Unity.Entities.ArchetypeChunk.GetComponentDataPtrRO<SGame.RotationSpeed>(Unity.Entities.ComponentTypeHandle<SGame.RotationSpeed>&)
		// System.Void* Unity.Entities.ArchetypeChunk.GetComponentDataPtrRO<SGame.UI.HUDFlowE>(Unity.Entities.ComponentTypeHandle<SGame.UI.HUDFlowE>&)
		// System.Void* Unity.Entities.ArchetypeChunk.GetComponentDataPtrRO<Speed>(Unity.Entities.ComponentTypeHandle<Speed>&)
		// System.Void* Unity.Entities.ArchetypeChunk.GetComponentDataPtrRO<Unity.Transforms.Rotation>(Unity.Entities.ComponentTypeHandle<Unity.Transforms.Rotation>&)
		// System.Void* Unity.Entities.ArchetypeChunk.GetComponentDataPtrRO<Unity.Transforms.Translation>(Unity.Entities.ComponentTypeHandle<Unity.Transforms.Translation>&)
		// System.Void* Unity.Entities.ArchetypeChunk.GetComponentDataPtrRW<SGame.Redpoint>(Unity.Entities.ComponentTypeHandle<SGame.Redpoint>&)
		// System.Void* Unity.Entities.ArchetypeChunk.GetComponentDataPtrRW<SGame.UserInput>(Unity.Entities.ComponentTypeHandle<SGame.UserInput>&)
		// System.Void* Unity.Entities.ArchetypeChunk.GetComponentDataPtrRW<Speed>(Unity.Entities.ComponentTypeHandle<Speed>&)
		// System.Void* Unity.Entities.ArchetypeChunk.GetComponentDataPtrRW<Unity.Transforms.Rotation>(Unity.Entities.ComponentTypeHandle<Unity.Transforms.Rotation>&)
		// System.Void* Unity.Entities.ArchetypeChunk.GetComponentDataPtrRW<Unity.Transforms.Translation>(Unity.Entities.ComponentTypeHandle<Unity.Transforms.Translation>&)
		// Unity.Entities.BufferTypeHandle<PathPositions> Unity.Entities.ComponentSystemBase.GetBufferTypeHandle<PathPositions>(bool)
		// Unity.Entities.ComponentDataFromEntity<Unity.Transforms.Translation> Unity.Entities.ComponentSystemBase.GetComponentDataFromEntity<Unity.Transforms.Translation>(bool)
		// Unity.Entities.ComponentTypeHandle<Follow> Unity.Entities.ComponentSystemBase.GetComponentTypeHandle<Follow>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.BulletData> Unity.Entities.ComponentSystemBase.GetComponentTypeHandle<SGame.BulletData>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.CharacterAttribue> Unity.Entities.ComponentSystemBase.GetComponentTypeHandle<SGame.CharacterAttribue>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.CharacterSpawnSystem.CharacterSpawn> Unity.Entities.ComponentSystemBase.GetComponentTypeHandle<SGame.CharacterSpawnSystem.CharacterSpawn>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.FoodTips> Unity.Entities.ComponentSystemBase.GetComponentTypeHandle<SGame.FoodTips>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.LastRotation> Unity.Entities.ComponentSystemBase.GetComponentTypeHandle<SGame.LastRotation>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.MoveTarget> Unity.Entities.ComponentSystemBase.GetComponentTypeHandle<SGame.MoveTarget>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.RedEvent> Unity.Entities.ComponentSystemBase.GetComponentTypeHandle<SGame.RedEvent>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.Redpoint> Unity.Entities.ComponentSystemBase.GetComponentTypeHandle<SGame.Redpoint>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.RewardData> Unity.Entities.ComponentSystemBase.GetComponentTypeHandle<SGame.RewardData>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.RewardWait> Unity.Entities.ComponentSystemBase.GetComponentTypeHandle<SGame.RewardWait>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.RotationSpeed> Unity.Entities.ComponentSystemBase.GetComponentTypeHandle<SGame.RotationSpeed>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.SoundData> Unity.Entities.ComponentSystemBase.GetComponentTypeHandle<SGame.SoundData>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.SoundTime> Unity.Entities.ComponentSystemBase.GetComponentTypeHandle<SGame.SoundTime>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.SpawnReq> Unity.Entities.ComponentSystemBase.GetComponentTypeHandle<SGame.SpawnReq>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.UI.HUDFlowE> Unity.Entities.ComponentSystemBase.GetComponentTypeHandle<SGame.UI.HUDFlowE>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.UserInput> Unity.Entities.ComponentSystemBase.GetComponentTypeHandle<SGame.UserInput>(bool)
		// Unity.Entities.ComponentTypeHandle<Speed> Unity.Entities.ComponentSystemBase.GetComponentTypeHandle<Speed>(bool)
		// Unity.Entities.ComponentTypeHandle<Unity.Transforms.Rotation> Unity.Entities.ComponentSystemBase.GetComponentTypeHandle<Unity.Transforms.Rotation>(bool)
		// Unity.Entities.ComponentTypeHandle<Unity.Transforms.Translation> Unity.Entities.ComponentSystemBase.GetComponentTypeHandle<Unity.Transforms.Translation>(bool)
		// System.Void Unity.Entities.ComponentSystemBase.RequireSingletonForUpdate<GameInitFinish>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<DespawningEntity>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<Follow>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<GameInitFinish>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<PathPositions>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.AudioSystem.DespawnedTag>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.BulletData>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.CharacterAttribue>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.CharacterSpawnSystem.CharacterInitalized>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.CharacterSpawnSystem.CharacterSpawn>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.DisableAttributeTag>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.EffectData>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.EffectSysData>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.EntitySyncGameObjectTag>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.FoodTips>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.GameObjectSyncTag>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.LastRotation>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.MoveTarget>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.RedCheck>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.RedDestroy>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.RedEvent>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.RedNode>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.RedPause>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.RedStatusChange>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.Redpoint>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.RewardData>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.RewardWait>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.RotationSpeed>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.SoundData>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.SoundTime>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.SpawnData>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.SpawnReq>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.SpawnSysData>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.UI.HUDFlowE>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.UI.HUDSync>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.UI.UIInitalized>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.UserInput>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<Speed>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<Unity.Transforms.Rotation>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<Unity.Transforms.Translation>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<object>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<DespawningEntity>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<FindPathParams>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<Follow>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<GameInitFinish>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<PathPositions>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.AudioSystem.DespawnedTag>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.BulletData>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.CharacterAttribue>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.CharacterSpawnSystem.CharacterInitalized>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.CharacterSpawnSystem.CharacterSpawn>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.DisableAttributeTag>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.EffectData>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.EffectSysData>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.EntitySyncGameObjectTag>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.FoodItem>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.FoodTips>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.GameObjectSyncTag>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.LastRotation>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.LiveTime>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.MoveTarget>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.RedCheck>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.RedDestroy>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.RedEvent>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.RedNode>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.RedPause>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.RedStatusChange>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.Redpoint>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.RewardData>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.RewardWait>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.RotationSpeed>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.SoundData>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.SoundRef>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.SoundTime>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.SpawnData>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.SpawnLayer>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.SpawnReq>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.SpawnSysData>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.UI.HUDFlowE>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.UI.HUDSync>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.UI.TweenTime>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.UI.UIInitalized>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.UserInput>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<Speed>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<Unity.Entities.Disabled>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<Unity.Transforms.Child>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<Unity.Transforms.LocalToParent>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<Unity.Transforms.LocalToWorld>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<Unity.Transforms.Parent>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<Unity.Transforms.Rotation>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<Unity.Transforms.Scale>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<Unity.Transforms.Translation>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<object>()
		// System.Void Unity.Entities.EntityCommandBuffer.AddComponent<DespawningEntity>(Unity.Entities.Entity)
		// System.Void Unity.Entities.EntityCommandBuffer.AddComponent<SGame.RedDestroy>(Unity.Entities.Entity)
		// System.Void Unity.Entities.EntityCommandBuffer.AddComponent<SGame.SpawnSysData>(Unity.Entities.Entity)
		// System.Void Unity.Entities.EntityCommandBuffer.RemoveComponent<SGame.RedEvent>(Unity.Entities.Entity)
		// System.Void Unity.Entities.EntityCommandBuffer.RemoveComponent<SGame.SpawnReq>(Unity.Entities.Entity)
		// System.Void Unity.Entities.EntityCommandBuffer.RemoveComponent<SGame.SpawnSysData>(Unity.Entities.Entity)
		// System.Void Unity.Entities.EntityCommandBuffer.RemoveComponent<object>(Unity.Entities.Entity)
		// System.Void Unity.Entities.EntityCommandBuffer.ParallelWriter.AddComponent<SGame.RewardWait>(int,Unity.Entities.Entity,SGame.RewardWait)
		// System.Void Unity.Entities.EntityCommandBuffer.ParallelWriter.AddComponent<SGame.SpawnReq>(int,Unity.Entities.Entity,SGame.SpawnReq)
		// System.Void Unity.Entities.EntityCommandBuffer.ParallelWriter.RemoveComponent<SGame.MoveTarget>(int,Unity.Entities.Entity)
		// System.Void Unity.Entities.EntityCommandBufferData.AddEntityComponentCommand<SGame.RewardWait>(Unity.Entities.EntityCommandBufferChain*,int,Unity.Entities.ECBCommand,Unity.Entities.Entity,SGame.RewardWait)
		// System.Void Unity.Entities.EntityCommandBufferData.AddEntityComponentCommand<SGame.SpawnReq>(Unity.Entities.EntityCommandBufferChain*,int,Unity.Entities.ECBCommand,Unity.Entities.Entity,SGame.SpawnReq)
		// Unity.Entities.DynamicBuffer<Unity.Transforms.Child> Unity.Entities.EntityDataAccess.GetBuffer<Unity.Transforms.Child>(Unity.Entities.Entity,bool)
		// Unity.Entities.BufferTypeHandle<PathPositions> Unity.Entities.EntityDataAccess.GetBufferTypeHandle<PathPositions>(bool)
		// Follow Unity.Entities.EntityDataAccess.GetComponentData<Follow>(Unity.Entities.Entity)
		// SGame.EffectData Unity.Entities.EntityDataAccess.GetComponentData<SGame.EffectData>(Unity.Entities.Entity)
		// SGame.FoodItem Unity.Entities.EntityDataAccess.GetComponentData<SGame.FoodItem>(Unity.Entities.Entity)
		// SGame.FoodTips Unity.Entities.EntityDataAccess.GetComponentData<SGame.FoodTips>(Unity.Entities.Entity)
		// SGame.Redpoint Unity.Entities.EntityDataAccess.GetComponentData<SGame.Redpoint>(Unity.Entities.Entity)
		// SGame.SpawnLayer Unity.Entities.EntityDataAccess.GetComponentData<SGame.SpawnLayer>(Unity.Entities.Entity)
		// SGame.UI.TweenTime Unity.Entities.EntityDataAccess.GetComponentData<SGame.UI.TweenTime>(Unity.Entities.Entity)
		// SGame.UserData Unity.Entities.EntityDataAccess.GetComponentData<SGame.UserData>(Unity.Entities.Entity)
		// SGame.UserInput Unity.Entities.EntityDataAccess.GetComponentData<SGame.UserInput>(Unity.Entities.Entity)
		// SGame.UserSetting Unity.Entities.EntityDataAccess.GetComponentData<SGame.UserSetting>(Unity.Entities.Entity)
		// Unity.Transforms.Rotation Unity.Entities.EntityDataAccess.GetComponentData<Unity.Transforms.Rotation>(Unity.Entities.Entity)
		// Unity.Transforms.Scale Unity.Entities.EntityDataAccess.GetComponentData<Unity.Transforms.Scale>(Unity.Entities.Entity)
		// Unity.Transforms.Translation Unity.Entities.EntityDataAccess.GetComponentData<Unity.Transforms.Translation>(Unity.Entities.Entity)
		// object Unity.Entities.EntityDataAccess.GetComponentData<object>(Unity.Entities.Entity)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<FindPathParams>(Unity.Entities.Entity,FindPathParams,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<SGame.BulletData>(Unity.Entities.Entity,SGame.BulletData,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<SGame.CharacterAttribue>(Unity.Entities.Entity,SGame.CharacterAttribue,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<SGame.CharacterSpawnSystem.CharacterSpawn>(Unity.Entities.Entity,SGame.CharacterSpawnSystem.CharacterSpawn,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<SGame.EffectData>(Unity.Entities.Entity,SGame.EffectData,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<SGame.FoodItem>(Unity.Entities.Entity,SGame.FoodItem,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<SGame.FoodTips>(Unity.Entities.Entity,SGame.FoodTips,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<SGame.GameObjectSyncTag>(Unity.Entities.Entity,SGame.GameObjectSyncTag,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<SGame.LastRotation>(Unity.Entities.Entity,SGame.LastRotation,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<SGame.LiveTime>(Unity.Entities.Entity,SGame.LiveTime,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<SGame.MoveTarget>(Unity.Entities.Entity,SGame.MoveTarget,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<SGame.RedEvent>(Unity.Entities.Entity,SGame.RedEvent,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<SGame.Redpoint>(Unity.Entities.Entity,SGame.Redpoint,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<SGame.RewardData>(Unity.Entities.Entity,SGame.RewardData,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<SGame.RotationSpeed>(Unity.Entities.Entity,SGame.RotationSpeed,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<SGame.SoundData>(Unity.Entities.Entity,SGame.SoundData,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<SGame.SoundRef>(Unity.Entities.Entity,SGame.SoundRef,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<SGame.SoundTime>(Unity.Entities.Entity,SGame.SoundTime,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<SGame.SpawnData>(Unity.Entities.Entity,SGame.SpawnData,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<SGame.SpawnLayer>(Unity.Entities.Entity,SGame.SpawnLayer,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<SGame.UI.HUDFlowE>(Unity.Entities.Entity,SGame.UI.HUDFlowE,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<SGame.UI.TweenTime>(Unity.Entities.Entity,SGame.UI.TweenTime,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<SGame.UserData>(Unity.Entities.Entity,SGame.UserData,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<SGame.UserSetting>(Unity.Entities.Entity,SGame.UserSetting,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<Speed>(Unity.Entities.Entity,Speed,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<Unity.Transforms.LocalToWorld>(Unity.Entities.Entity,Unity.Transforms.LocalToWorld,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<Unity.Transforms.Parent>(Unity.Entities.Entity,Unity.Transforms.Parent,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<Unity.Transforms.Rotation>(Unity.Entities.Entity,Unity.Transforms.Rotation,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<Unity.Transforms.Translation>(Unity.Entities.Entity,Unity.Transforms.Translation,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<object>(Unity.Entities.Entity,object,Unity.Entities.SystemHandleUntyped&)
		// object Unity.Entities.EntityDataAccessManagedComponentExtensions.GetComponentObject<object>(Unity.Entities.EntityDataAccess&,Unity.Entities.Entity,Unity.Entities.ComponentType)
		// Unity.Entities.DynamicBuffer<Unity.Transforms.Child> Unity.Entities.EntityManager.AddBuffer<Unity.Transforms.Child>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.AddComponent<DespawningEntity>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.AddComponent<FindPathParams>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.AddComponent<SGame.AudioSystem.DespawnedTag>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.AddComponent<SGame.CharacterSpawnSystem.CharacterInitalized>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.AddComponent<SGame.DisableAttributeTag>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.AddComponent<SGame.EffectSysData>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.AddComponent<SGame.EntitySyncGameObjectTag>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.AddComponent<SGame.LastRotation>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.AddComponent<SGame.LiveTime>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.AddComponent<SGame.RedCheck>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.AddComponent<SGame.RedNode>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.AddComponent<SGame.RedPause>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.AddComponent<SGame.RedStatusChange>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.AddComponent<SGame.SoundRef>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.AddComponent<SGame.SpawnData>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.AddComponent<SGame.UI.HUDFlowE>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.AddComponent<SGame.UI.HUDSync>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.AddComponent<SGame.UI.TweenTime>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.AddComponent<Unity.Entities.Disabled>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.AddComponent<Unity.Transforms.Child>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.AddComponent<Unity.Transforms.LocalToParent>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.AddComponent<Unity.Transforms.LocalToWorld>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.AddComponent<Unity.Transforms.Parent>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.AddComponent<Unity.Transforms.Rotation>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.AddComponent<Unity.Transforms.Translation>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.AddComponent<object>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.AddComponentData<SGame.BulletData>(Unity.Entities.Entity,SGame.BulletData)
		// bool Unity.Entities.EntityManager.AddComponentData<SGame.CharacterAttribue>(Unity.Entities.Entity,SGame.CharacterAttribue)
		// bool Unity.Entities.EntityManager.AddComponentData<SGame.FoodItem>(Unity.Entities.Entity,SGame.FoodItem)
		// bool Unity.Entities.EntityManager.AddComponentData<SGame.FoodTips>(Unity.Entities.Entity,SGame.FoodTips)
		// bool Unity.Entities.EntityManager.AddComponentData<SGame.GameObjectSyncTag>(Unity.Entities.Entity,SGame.GameObjectSyncTag)
		// bool Unity.Entities.EntityManager.AddComponentData<SGame.MoveTarget>(Unity.Entities.Entity,SGame.MoveTarget)
		// bool Unity.Entities.EntityManager.AddComponentData<SGame.Redpoint>(Unity.Entities.Entity,SGame.Redpoint)
		// bool Unity.Entities.EntityManager.AddComponentData<SGame.RewardData>(Unity.Entities.Entity,SGame.RewardData)
		// bool Unity.Entities.EntityManager.AddComponentData<SGame.RotationSpeed>(Unity.Entities.Entity,SGame.RotationSpeed)
		// bool Unity.Entities.EntityManager.AddComponentData<SGame.SpawnLayer>(Unity.Entities.Entity,SGame.SpawnLayer)
		// bool Unity.Entities.EntityManager.AddComponentData<Speed>(Unity.Entities.Entity,Speed)
		// bool Unity.Entities.EntityManager.AddComponentData<Unity.Transforms.Translation>(Unity.Entities.Entity,Unity.Transforms.Translation)
		// bool Unity.Entities.EntityManager.AddComponentData<object>(Unity.Entities.Entity,object)
		// Unity.Entities.DynamicBuffer<Unity.Transforms.Child> Unity.Entities.EntityManager.GetBuffer<Unity.Transforms.Child>(Unity.Entities.Entity,bool)
		// Unity.Entities.BufferTypeHandle<PathPositions> Unity.Entities.EntityManager.GetBufferTypeHandle<PathPositions>(bool)
		// Follow Unity.Entities.EntityManager.GetComponentData<Follow>(Unity.Entities.Entity)
		// SGame.EffectData Unity.Entities.EntityManager.GetComponentData<SGame.EffectData>(Unity.Entities.Entity)
		// SGame.FoodItem Unity.Entities.EntityManager.GetComponentData<SGame.FoodItem>(Unity.Entities.Entity)
		// SGame.FoodTips Unity.Entities.EntityManager.GetComponentData<SGame.FoodTips>(Unity.Entities.Entity)
		// SGame.Redpoint Unity.Entities.EntityManager.GetComponentData<SGame.Redpoint>(Unity.Entities.Entity)
		// SGame.SpawnLayer Unity.Entities.EntityManager.GetComponentData<SGame.SpawnLayer>(Unity.Entities.Entity)
		// SGame.UI.TweenTime Unity.Entities.EntityManager.GetComponentData<SGame.UI.TweenTime>(Unity.Entities.Entity)
		// SGame.UserData Unity.Entities.EntityManager.GetComponentData<SGame.UserData>(Unity.Entities.Entity)
		// SGame.UserInput Unity.Entities.EntityManager.GetComponentData<SGame.UserInput>(Unity.Entities.Entity)
		// SGame.UserSetting Unity.Entities.EntityManager.GetComponentData<SGame.UserSetting>(Unity.Entities.Entity)
		// Unity.Transforms.Rotation Unity.Entities.EntityManager.GetComponentData<Unity.Transforms.Rotation>(Unity.Entities.Entity)
		// Unity.Transforms.Scale Unity.Entities.EntityManager.GetComponentData<Unity.Transforms.Scale>(Unity.Entities.Entity)
		// Unity.Transforms.Translation Unity.Entities.EntityManager.GetComponentData<Unity.Transforms.Translation>(Unity.Entities.Entity)
		// object Unity.Entities.EntityManager.GetComponentData<object>(Unity.Entities.Entity)
		// Unity.Entities.ComponentDataFromEntity<Unity.Transforms.Translation> Unity.Entities.EntityManager.GetComponentDataFromEntity<Unity.Transforms.Translation>(bool)
		// Unity.Entities.ComponentDataFromEntity<Unity.Transforms.Translation> Unity.Entities.EntityManager.GetComponentDataFromEntity<Unity.Transforms.Translation>(int,bool)
		// object Unity.Entities.EntityManager.GetComponentObject<object>(Unity.Entities.Entity)
		// Unity.Entities.ComponentTypeHandle<Follow> Unity.Entities.EntityManager.GetComponentTypeHandle<Follow>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.BulletData> Unity.Entities.EntityManager.GetComponentTypeHandle<SGame.BulletData>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.CharacterAttribue> Unity.Entities.EntityManager.GetComponentTypeHandle<SGame.CharacterAttribue>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.CharacterSpawnSystem.CharacterSpawn> Unity.Entities.EntityManager.GetComponentTypeHandle<SGame.CharacterSpawnSystem.CharacterSpawn>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.FoodTips> Unity.Entities.EntityManager.GetComponentTypeHandle<SGame.FoodTips>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.LastRotation> Unity.Entities.EntityManager.GetComponentTypeHandle<SGame.LastRotation>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.MoveTarget> Unity.Entities.EntityManager.GetComponentTypeHandle<SGame.MoveTarget>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.RedEvent> Unity.Entities.EntityManager.GetComponentTypeHandle<SGame.RedEvent>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.Redpoint> Unity.Entities.EntityManager.GetComponentTypeHandle<SGame.Redpoint>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.RewardData> Unity.Entities.EntityManager.GetComponentTypeHandle<SGame.RewardData>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.RewardWait> Unity.Entities.EntityManager.GetComponentTypeHandle<SGame.RewardWait>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.RotationSpeed> Unity.Entities.EntityManager.GetComponentTypeHandle<SGame.RotationSpeed>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.SoundData> Unity.Entities.EntityManager.GetComponentTypeHandle<SGame.SoundData>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.SoundTime> Unity.Entities.EntityManager.GetComponentTypeHandle<SGame.SoundTime>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.SpawnReq> Unity.Entities.EntityManager.GetComponentTypeHandle<SGame.SpawnReq>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.UI.HUDFlowE> Unity.Entities.EntityManager.GetComponentTypeHandle<SGame.UI.HUDFlowE>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.UserInput> Unity.Entities.EntityManager.GetComponentTypeHandle<SGame.UserInput>(bool)
		// Unity.Entities.ComponentTypeHandle<Speed> Unity.Entities.EntityManager.GetComponentTypeHandle<Speed>(bool)
		// Unity.Entities.ComponentTypeHandle<Unity.Transforms.Rotation> Unity.Entities.EntityManager.GetComponentTypeHandle<Unity.Transforms.Rotation>(bool)
		// Unity.Entities.ComponentTypeHandle<Unity.Transforms.Translation> Unity.Entities.EntityManager.GetComponentTypeHandle<Unity.Transforms.Translation>(bool)
		// Unity.Entities.ComponentTypeHandle<object> Unity.Entities.EntityManager.GetComponentTypeHandle<object>(bool)
		// bool Unity.Entities.EntityManager.HasComponent<DespawningEntity>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<FindPathParams>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<Follow>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<PathPositions>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<SGame.CharacterSpawnSystem.CharacterInitalized>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<SGame.DisableAttributeTag>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<SGame.EffectData>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<SGame.EffectSysData>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<SGame.EntitySyncGameObjectTag>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<SGame.FoodTips>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<SGame.LastRotation>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<SGame.LiveTime>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<SGame.SpawnData>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<SGame.SpawnLayer>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<SGame.SpawnSysData>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<SGame.UI.HUDFlowE>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<SGame.UI.UIInitalized>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<Unity.Entities.Disabled>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<Unity.Transforms.Child>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<Unity.Transforms.LocalToParent>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<Unity.Transforms.LocalToWorld>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<Unity.Transforms.Parent>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<Unity.Transforms.Rotation>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<Unity.Transforms.Scale>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<Unity.Transforms.Translation>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<object>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.RemoveComponent<SGame.DisableAttributeTag>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.RemoveComponent<SGame.EffectSysData>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.RemoveComponent<SGame.LastRotation>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.RemoveComponent<SGame.RedPause>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.RemoveComponent<Unity.Entities.Disabled>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.RemoveComponent<Unity.Transforms.LocalToParent>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.RemoveComponent<Unity.Transforms.Parent>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.RemoveComponent<object>(Unity.Entities.Entity)
		// System.Void Unity.Entities.EntityManager.SetComponentData<FindPathParams>(Unity.Entities.Entity,FindPathParams)
		// System.Void Unity.Entities.EntityManager.SetComponentData<SGame.BulletData>(Unity.Entities.Entity,SGame.BulletData)
		// System.Void Unity.Entities.EntityManager.SetComponentData<SGame.CharacterAttribue>(Unity.Entities.Entity,SGame.CharacterAttribue)
		// System.Void Unity.Entities.EntityManager.SetComponentData<SGame.CharacterSpawnSystem.CharacterSpawn>(Unity.Entities.Entity,SGame.CharacterSpawnSystem.CharacterSpawn)
		// System.Void Unity.Entities.EntityManager.SetComponentData<SGame.EffectData>(Unity.Entities.Entity,SGame.EffectData)
		// System.Void Unity.Entities.EntityManager.SetComponentData<SGame.FoodItem>(Unity.Entities.Entity,SGame.FoodItem)
		// System.Void Unity.Entities.EntityManager.SetComponentData<SGame.FoodTips>(Unity.Entities.Entity,SGame.FoodTips)
		// System.Void Unity.Entities.EntityManager.SetComponentData<SGame.GameObjectSyncTag>(Unity.Entities.Entity,SGame.GameObjectSyncTag)
		// System.Void Unity.Entities.EntityManager.SetComponentData<SGame.LastRotation>(Unity.Entities.Entity,SGame.LastRotation)
		// System.Void Unity.Entities.EntityManager.SetComponentData<SGame.LiveTime>(Unity.Entities.Entity,SGame.LiveTime)
		// System.Void Unity.Entities.EntityManager.SetComponentData<SGame.MoveTarget>(Unity.Entities.Entity,SGame.MoveTarget)
		// System.Void Unity.Entities.EntityManager.SetComponentData<SGame.RedEvent>(Unity.Entities.Entity,SGame.RedEvent)
		// System.Void Unity.Entities.EntityManager.SetComponentData<SGame.Redpoint>(Unity.Entities.Entity,SGame.Redpoint)
		// System.Void Unity.Entities.EntityManager.SetComponentData<SGame.RewardData>(Unity.Entities.Entity,SGame.RewardData)
		// System.Void Unity.Entities.EntityManager.SetComponentData<SGame.RotationSpeed>(Unity.Entities.Entity,SGame.RotationSpeed)
		// System.Void Unity.Entities.EntityManager.SetComponentData<SGame.SoundData>(Unity.Entities.Entity,SGame.SoundData)
		// System.Void Unity.Entities.EntityManager.SetComponentData<SGame.SoundRef>(Unity.Entities.Entity,SGame.SoundRef)
		// System.Void Unity.Entities.EntityManager.SetComponentData<SGame.SoundTime>(Unity.Entities.Entity,SGame.SoundTime)
		// System.Void Unity.Entities.EntityManager.SetComponentData<SGame.SpawnData>(Unity.Entities.Entity,SGame.SpawnData)
		// System.Void Unity.Entities.EntityManager.SetComponentData<SGame.SpawnLayer>(Unity.Entities.Entity,SGame.SpawnLayer)
		// System.Void Unity.Entities.EntityManager.SetComponentData<SGame.UI.HUDFlowE>(Unity.Entities.Entity,SGame.UI.HUDFlowE)
		// System.Void Unity.Entities.EntityManager.SetComponentData<SGame.UI.TweenTime>(Unity.Entities.Entity,SGame.UI.TweenTime)
		// System.Void Unity.Entities.EntityManager.SetComponentData<SGame.UserData>(Unity.Entities.Entity,SGame.UserData)
		// System.Void Unity.Entities.EntityManager.SetComponentData<SGame.UserSetting>(Unity.Entities.Entity,SGame.UserSetting)
		// System.Void Unity.Entities.EntityManager.SetComponentData<Speed>(Unity.Entities.Entity,Speed)
		// System.Void Unity.Entities.EntityManager.SetComponentData<Unity.Transforms.LocalToWorld>(Unity.Entities.Entity,Unity.Transforms.LocalToWorld)
		// System.Void Unity.Entities.EntityManager.SetComponentData<Unity.Transforms.Parent>(Unity.Entities.Entity,Unity.Transforms.Parent)
		// System.Void Unity.Entities.EntityManager.SetComponentData<Unity.Transforms.Rotation>(Unity.Entities.Entity,Unity.Transforms.Rotation)
		// System.Void Unity.Entities.EntityManager.SetComponentData<Unity.Transforms.Translation>(Unity.Entities.Entity,Unity.Transforms.Translation)
		// System.Void Unity.Entities.EntityManager.SetComponentData<object>(Unity.Entities.Entity,object)
		// object Unity.Entities.EntityQuery.GetSingleton<object>()
		// object Unity.Entities.EntityQueryImpl.GetSingleton<object>()
		// SGame.CharacterSpawnSystem.CharacterSpawn Unity.Entities.InternalCompilerInterface.GetComponentData<SGame.CharacterSpawnSystem.CharacterSpawn>(Unity.Entities.EntityManager,Unity.Entities.Entity,int,SGame.CharacterSpawnSystem.CharacterSpawn&)
		// SGame.SoundData Unity.Entities.InternalCompilerInterface.GetComponentData<SGame.SoundData>(Unity.Entities.EntityManager,Unity.Entities.Entity,int,SGame.SoundData&)
		// SGame.SoundTime Unity.Entities.InternalCompilerInterface.GetComponentData<SGame.SoundTime>(Unity.Entities.EntityManager,Unity.Entities.Entity,int,SGame.SoundTime&)
		// SGame.SpawnReq Unity.Entities.InternalCompilerInterface.GetComponentData<SGame.SpawnReq>(Unity.Entities.EntityManager,Unity.Entities.Entity,int,SGame.SpawnReq&)
		// System.IntPtr Unity.Entities.InternalCompilerInterface.UnsafeGetChunkNativeArrayIntPtr<SGame.Redpoint>(Unity.Entities.ArchetypeChunk,Unity.Entities.ComponentTypeHandle<SGame.Redpoint>)
		// System.IntPtr Unity.Entities.InternalCompilerInterface.UnsafeGetChunkNativeArrayIntPtr<SGame.UserInput>(Unity.Entities.ArchetypeChunk,Unity.Entities.ComponentTypeHandle<SGame.UserInput>)
		// System.IntPtr Unity.Entities.InternalCompilerInterface.UnsafeGetChunkNativeArrayIntPtr<Speed>(Unity.Entities.ArchetypeChunk,Unity.Entities.ComponentTypeHandle<Speed>)
		// System.IntPtr Unity.Entities.InternalCompilerInterface.UnsafeGetChunkNativeArrayIntPtr<Unity.Transforms.Rotation>(Unity.Entities.ArchetypeChunk,Unity.Entities.ComponentTypeHandle<Unity.Transforms.Rotation>)
		// System.IntPtr Unity.Entities.InternalCompilerInterface.UnsafeGetChunkNativeArrayIntPtr<Unity.Transforms.Translation>(Unity.Entities.ArchetypeChunk,Unity.Entities.ComponentTypeHandle<Unity.Transforms.Translation>)
		// System.IntPtr Unity.Entities.InternalCompilerInterface.UnsafeGetChunkNativeArrayReadOnlyIntPtr<Follow>(Unity.Entities.ArchetypeChunk,Unity.Entities.ComponentTypeHandle<Follow>)
		// System.IntPtr Unity.Entities.InternalCompilerInterface.UnsafeGetChunkNativeArrayReadOnlyIntPtr<SGame.BulletData>(Unity.Entities.ArchetypeChunk,Unity.Entities.ComponentTypeHandle<SGame.BulletData>)
		// System.IntPtr Unity.Entities.InternalCompilerInterface.UnsafeGetChunkNativeArrayReadOnlyIntPtr<SGame.CharacterAttribue>(Unity.Entities.ArchetypeChunk,Unity.Entities.ComponentTypeHandle<SGame.CharacterAttribue>)
		// System.IntPtr Unity.Entities.InternalCompilerInterface.UnsafeGetChunkNativeArrayReadOnlyIntPtr<SGame.FoodTips>(Unity.Entities.ArchetypeChunk,Unity.Entities.ComponentTypeHandle<SGame.FoodTips>)
		// System.IntPtr Unity.Entities.InternalCompilerInterface.UnsafeGetChunkNativeArrayReadOnlyIntPtr<SGame.LastRotation>(Unity.Entities.ArchetypeChunk,Unity.Entities.ComponentTypeHandle<SGame.LastRotation>)
		// System.IntPtr Unity.Entities.InternalCompilerInterface.UnsafeGetChunkNativeArrayReadOnlyIntPtr<SGame.MoveTarget>(Unity.Entities.ArchetypeChunk,Unity.Entities.ComponentTypeHandle<SGame.MoveTarget>)
		// System.IntPtr Unity.Entities.InternalCompilerInterface.UnsafeGetChunkNativeArrayReadOnlyIntPtr<SGame.RedEvent>(Unity.Entities.ArchetypeChunk,Unity.Entities.ComponentTypeHandle<SGame.RedEvent>)
		// System.IntPtr Unity.Entities.InternalCompilerInterface.UnsafeGetChunkNativeArrayReadOnlyIntPtr<SGame.Redpoint>(Unity.Entities.ArchetypeChunk,Unity.Entities.ComponentTypeHandle<SGame.Redpoint>)
		// System.IntPtr Unity.Entities.InternalCompilerInterface.UnsafeGetChunkNativeArrayReadOnlyIntPtr<SGame.RewardData>(Unity.Entities.ArchetypeChunk,Unity.Entities.ComponentTypeHandle<SGame.RewardData>)
		// System.IntPtr Unity.Entities.InternalCompilerInterface.UnsafeGetChunkNativeArrayReadOnlyIntPtr<SGame.RewardWait>(Unity.Entities.ArchetypeChunk,Unity.Entities.ComponentTypeHandle<SGame.RewardWait>)
		// System.IntPtr Unity.Entities.InternalCompilerInterface.UnsafeGetChunkNativeArrayReadOnlyIntPtr<SGame.RotationSpeed>(Unity.Entities.ArchetypeChunk,Unity.Entities.ComponentTypeHandle<SGame.RotationSpeed>)
		// System.IntPtr Unity.Entities.InternalCompilerInterface.UnsafeGetChunkNativeArrayReadOnlyIntPtr<SGame.UI.HUDFlowE>(Unity.Entities.ArchetypeChunk,Unity.Entities.ComponentTypeHandle<SGame.UI.HUDFlowE>)
		// System.IntPtr Unity.Entities.InternalCompilerInterface.UnsafeGetChunkNativeArrayReadOnlyIntPtr<Speed>(Unity.Entities.ArchetypeChunk,Unity.Entities.ComponentTypeHandle<Speed>)
		// System.IntPtr Unity.Entities.InternalCompilerInterface.UnsafeGetChunkNativeArrayReadOnlyIntPtr<Unity.Transforms.Rotation>(Unity.Entities.ArchetypeChunk,Unity.Entities.ComponentTypeHandle<Unity.Transforms.Rotation>)
		// System.IntPtr Unity.Entities.InternalCompilerInterface.UnsafeGetChunkNativeArrayReadOnlyIntPtr<Unity.Transforms.Translation>(Unity.Entities.ArchetypeChunk,Unity.Entities.ComponentTypeHandle<Unity.Transforms.Translation>)
		// Unity.Entities.Entity Unity.Entities.InternalCompilerInterface.UnsafeGetCopyOfNativeArrayPtrElement<Unity.Entities.Entity>(System.IntPtr,int)
		// Follow& Unity.Entities.InternalCompilerInterface.UnsafeGetRefToNativeArrayPtrElement<Follow>(System.IntPtr,int)
		// SGame.BulletData& Unity.Entities.InternalCompilerInterface.UnsafeGetRefToNativeArrayPtrElement<SGame.BulletData>(System.IntPtr,int)
		// SGame.CharacterAttribue& Unity.Entities.InternalCompilerInterface.UnsafeGetRefToNativeArrayPtrElement<SGame.CharacterAttribue>(System.IntPtr,int)
		// SGame.FoodTips& Unity.Entities.InternalCompilerInterface.UnsafeGetRefToNativeArrayPtrElement<SGame.FoodTips>(System.IntPtr,int)
		// SGame.LastRotation& Unity.Entities.InternalCompilerInterface.UnsafeGetRefToNativeArrayPtrElement<SGame.LastRotation>(System.IntPtr,int)
		// SGame.MoveTarget& Unity.Entities.InternalCompilerInterface.UnsafeGetRefToNativeArrayPtrElement<SGame.MoveTarget>(System.IntPtr,int)
		// SGame.RedEvent& Unity.Entities.InternalCompilerInterface.UnsafeGetRefToNativeArrayPtrElement<SGame.RedEvent>(System.IntPtr,int)
		// SGame.Redpoint& Unity.Entities.InternalCompilerInterface.UnsafeGetRefToNativeArrayPtrElement<SGame.Redpoint>(System.IntPtr,int)
		// SGame.RewardData& Unity.Entities.InternalCompilerInterface.UnsafeGetRefToNativeArrayPtrElement<SGame.RewardData>(System.IntPtr,int)
		// SGame.RewardWait& Unity.Entities.InternalCompilerInterface.UnsafeGetRefToNativeArrayPtrElement<SGame.RewardWait>(System.IntPtr,int)
		// SGame.RotationSpeed& Unity.Entities.InternalCompilerInterface.UnsafeGetRefToNativeArrayPtrElement<SGame.RotationSpeed>(System.IntPtr,int)
		// SGame.UI.HUDFlowE& Unity.Entities.InternalCompilerInterface.UnsafeGetRefToNativeArrayPtrElement<SGame.UI.HUDFlowE>(System.IntPtr,int)
		// SGame.UserInput& Unity.Entities.InternalCompilerInterface.UnsafeGetRefToNativeArrayPtrElement<SGame.UserInput>(System.IntPtr,int)
		// Speed& Unity.Entities.InternalCompilerInterface.UnsafeGetRefToNativeArrayPtrElement<Speed>(System.IntPtr,int)
		// Unity.Transforms.Rotation& Unity.Entities.InternalCompilerInterface.UnsafeGetRefToNativeArrayPtrElement<Unity.Transforms.Rotation>(System.IntPtr,int)
		// Unity.Transforms.Translation& Unity.Entities.InternalCompilerInterface.UnsafeGetRefToNativeArrayPtrElement<Unity.Transforms.Translation>(System.IntPtr,int)
		// System.Void Unity.Entities.InternalCompilerInterface.WriteComponentData<SGame.SoundTime>(Unity.Entities.EntityManager,Unity.Entities.Entity,int,SGame.SoundTime&,SGame.SoundTime&)
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.BulletHitSystem.BulletHitSystem_LambdaJob_0_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.CharacterAnimSystem.CharacterAnimSystem_LambdaJob_0_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.CharacterAttributeSystem.CharacterAttributeSystem_LambdaJob_0_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.CharacterDespawnSystem.CharacterDespawnSystem_LambdaJob_0_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.DespawnFoodTipSystem.DespawnFoodTipSystem_LambdaJob_0_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.EntitySyncGameObjectSystem.EntitySyncGameObjectSystem_LambdaJob_0_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.FollowRotationSystem.FollowRotationSystem_LambdaJob_0_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.FollowRotationSystem.FollowRotationSystem_LambdaJob_1_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.GameObjectSyncSystem.GameObjectSyncSystem_LambdaJob_0_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.Http.HttpSystem.HttpSystem_LambdaJob_0_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.RedpointSystem.RedpointSystem_LambdaJob_0_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.RedpointSystem.RedpointSystem_LambdaJob_1_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.RedpointSystem.RedpointSystem_LambdaJob_2_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.RedpointSystem.RedpointSystem_LambdaJob_3_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.RewardSystem.RewardSystem_LambdaJob_0_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.RewardSystem.RewardSystem_LambdaJob_1_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.SpawnSystem.SpawnSystem_LambdaJob_0_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.SpawnSystem.SpawnSystem_LambdaJob_2_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.UI.HUDFlowSystem.HUDFlowSystem_LambdaJob_0_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.UI.HUDFlowSystem.HUDFlowSystem_LambdaJob_1_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.UI.HUDSyncSystem.HUDSyncSystem_LambdaJob_0_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.UserInputsystem.UserInputsystem_LambdaJob_0_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobs<SGame.BulletHitSystem.BulletHitSystem_LambdaJob_0_Job>(SGame.BulletHitSystem.BulletHitSystem_LambdaJob_0_Job&,Unity.Entities.EntityQuery)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobs<SGame.CharacterAnimSystem.CharacterAnimSystem_LambdaJob_0_Job>(SGame.CharacterAnimSystem.CharacterAnimSystem_LambdaJob_0_Job&,Unity.Entities.EntityQuery)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobs<SGame.CharacterAttributeSystem.CharacterAttributeSystem_LambdaJob_0_Job>(SGame.CharacterAttributeSystem.CharacterAttributeSystem_LambdaJob_0_Job&,Unity.Entities.EntityQuery)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobs<SGame.CharacterDespawnSystem.CharacterDespawnSystem_LambdaJob_0_Job>(SGame.CharacterDespawnSystem.CharacterDespawnSystem_LambdaJob_0_Job&,Unity.Entities.EntityQuery)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobs<SGame.DespawnFoodTipSystem.DespawnFoodTipSystem_LambdaJob_0_Job>(SGame.DespawnFoodTipSystem.DespawnFoodTipSystem_LambdaJob_0_Job&,Unity.Entities.EntityQuery)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobs<SGame.EntitySyncGameObjectSystem.EntitySyncGameObjectSystem_LambdaJob_0_Job>(SGame.EntitySyncGameObjectSystem.EntitySyncGameObjectSystem_LambdaJob_0_Job&,Unity.Entities.EntityQuery)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobs<SGame.GameObjectSyncSystem.GameObjectSyncSystem_LambdaJob_0_Job>(SGame.GameObjectSyncSystem.GameObjectSyncSystem_LambdaJob_0_Job&,Unity.Entities.EntityQuery)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobs<SGame.Http.HttpSystem.HttpSystem_LambdaJob_0_Job>(SGame.Http.HttpSystem.HttpSystem_LambdaJob_0_Job&,Unity.Entities.EntityQuery)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobs<SGame.RedpointSystem.RedpointSystem_LambdaJob_0_Job>(SGame.RedpointSystem.RedpointSystem_LambdaJob_0_Job&,Unity.Entities.EntityQuery)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobs<SGame.RedpointSystem.RedpointSystem_LambdaJob_1_Job>(SGame.RedpointSystem.RedpointSystem_LambdaJob_1_Job&,Unity.Entities.EntityQuery)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobs<SGame.RedpointSystem.RedpointSystem_LambdaJob_2_Job>(SGame.RedpointSystem.RedpointSystem_LambdaJob_2_Job&,Unity.Entities.EntityQuery)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobs<SGame.RedpointSystem.RedpointSystem_LambdaJob_3_Job>(SGame.RedpointSystem.RedpointSystem_LambdaJob_3_Job&,Unity.Entities.EntityQuery)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobs<SGame.RewardSystem.RewardSystem_LambdaJob_0_Job>(SGame.RewardSystem.RewardSystem_LambdaJob_0_Job&,Unity.Entities.EntityQuery)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobs<SGame.SpawnSystem.SpawnSystem_LambdaJob_0_Job>(SGame.SpawnSystem.SpawnSystem_LambdaJob_0_Job&,Unity.Entities.EntityQuery)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobs<SGame.SpawnSystem.SpawnSystem_LambdaJob_2_Job>(SGame.SpawnSystem.SpawnSystem_LambdaJob_2_Job&,Unity.Entities.EntityQuery)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobs<SGame.UI.HUDFlowSystem.HUDFlowSystem_LambdaJob_0_Job>(SGame.UI.HUDFlowSystem.HUDFlowSystem_LambdaJob_0_Job&,Unity.Entities.EntityQuery)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobs<SGame.UI.HUDFlowSystem.HUDFlowSystem_LambdaJob_1_Job>(SGame.UI.HUDFlowSystem.HUDFlowSystem_LambdaJob_1_Job&,Unity.Entities.EntityQuery)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobs<SGame.UI.HUDSyncSystem.HUDSyncSystem_LambdaJob_0_Job>(SGame.UI.HUDSyncSystem.HUDSyncSystem_LambdaJob_0_Job&,Unity.Entities.EntityQuery)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobs<SGame.UserInputsystem.UserInputsystem_LambdaJob_0_Job>(SGame.UserInputsystem.UserInputsystem_LambdaJob_0_Job&,Unity.Entities.EntityQuery)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.BulletHitSystem.BulletHitSystem_LambdaJob_0_Job>(SGame.BulletHitSystem.BulletHitSystem_LambdaJob_0_Job&,Unity.Entities.ArchetypeChunkIterator&)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.BulletHitSystem.BulletHitSystem_LambdaJob_0_Job>(SGame.BulletHitSystem.BulletHitSystem_LambdaJob_0_Job&,Unity.Entities.Chunk**,int,Unity.Entities.EntityComponentStore*)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.CharacterAnimSystem.CharacterAnimSystem_LambdaJob_0_Job>(SGame.CharacterAnimSystem.CharacterAnimSystem_LambdaJob_0_Job&,Unity.Entities.ArchetypeChunkIterator&)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.CharacterAnimSystem.CharacterAnimSystem_LambdaJob_0_Job>(SGame.CharacterAnimSystem.CharacterAnimSystem_LambdaJob_0_Job&,Unity.Entities.Chunk**,int,Unity.Entities.EntityComponentStore*)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.CharacterAttributeSystem.CharacterAttributeSystem_LambdaJob_0_Job>(SGame.CharacterAttributeSystem.CharacterAttributeSystem_LambdaJob_0_Job&,Unity.Entities.ArchetypeChunkIterator&)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.CharacterAttributeSystem.CharacterAttributeSystem_LambdaJob_0_Job>(SGame.CharacterAttributeSystem.CharacterAttributeSystem_LambdaJob_0_Job&,Unity.Entities.Chunk**,int,Unity.Entities.EntityComponentStore*)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.CharacterDespawnSystem.CharacterDespawnSystem_LambdaJob_0_Job>(SGame.CharacterDespawnSystem.CharacterDespawnSystem_LambdaJob_0_Job&,Unity.Entities.ArchetypeChunkIterator&)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.CharacterDespawnSystem.CharacterDespawnSystem_LambdaJob_0_Job>(SGame.CharacterDespawnSystem.CharacterDespawnSystem_LambdaJob_0_Job&,Unity.Entities.Chunk**,int,Unity.Entities.EntityComponentStore*)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.DespawnFoodTipSystem.DespawnFoodTipSystem_LambdaJob_0_Job>(SGame.DespawnFoodTipSystem.DespawnFoodTipSystem_LambdaJob_0_Job&,Unity.Entities.ArchetypeChunkIterator&)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.DespawnFoodTipSystem.DespawnFoodTipSystem_LambdaJob_0_Job>(SGame.DespawnFoodTipSystem.DespawnFoodTipSystem_LambdaJob_0_Job&,Unity.Entities.Chunk**,int,Unity.Entities.EntityComponentStore*)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.EntitySyncGameObjectSystem.EntitySyncGameObjectSystem_LambdaJob_0_Job>(SGame.EntitySyncGameObjectSystem.EntitySyncGameObjectSystem_LambdaJob_0_Job&,Unity.Entities.ArchetypeChunkIterator&)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.EntitySyncGameObjectSystem.EntitySyncGameObjectSystem_LambdaJob_0_Job>(SGame.EntitySyncGameObjectSystem.EntitySyncGameObjectSystem_LambdaJob_0_Job&,Unity.Entities.Chunk**,int,Unity.Entities.EntityComponentStore*)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.GameObjectSyncSystem.GameObjectSyncSystem_LambdaJob_0_Job>(SGame.GameObjectSyncSystem.GameObjectSyncSystem_LambdaJob_0_Job&,Unity.Entities.ArchetypeChunkIterator&)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.GameObjectSyncSystem.GameObjectSyncSystem_LambdaJob_0_Job>(SGame.GameObjectSyncSystem.GameObjectSyncSystem_LambdaJob_0_Job&,Unity.Entities.Chunk**,int,Unity.Entities.EntityComponentStore*)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.Http.HttpSystem.HttpSystem_LambdaJob_0_Job>(SGame.Http.HttpSystem.HttpSystem_LambdaJob_0_Job&,Unity.Entities.ArchetypeChunkIterator&)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.Http.HttpSystem.HttpSystem_LambdaJob_0_Job>(SGame.Http.HttpSystem.HttpSystem_LambdaJob_0_Job&,Unity.Entities.Chunk**,int,Unity.Entities.EntityComponentStore*)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.RedpointSystem.RedpointSystem_LambdaJob_0_Job>(SGame.RedpointSystem.RedpointSystem_LambdaJob_0_Job&,Unity.Entities.ArchetypeChunkIterator&)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.RedpointSystem.RedpointSystem_LambdaJob_0_Job>(SGame.RedpointSystem.RedpointSystem_LambdaJob_0_Job&,Unity.Entities.Chunk**,int,Unity.Entities.EntityComponentStore*)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.RedpointSystem.RedpointSystem_LambdaJob_1_Job>(SGame.RedpointSystem.RedpointSystem_LambdaJob_1_Job&,Unity.Entities.ArchetypeChunkIterator&)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.RedpointSystem.RedpointSystem_LambdaJob_1_Job>(SGame.RedpointSystem.RedpointSystem_LambdaJob_1_Job&,Unity.Entities.Chunk**,int,Unity.Entities.EntityComponentStore*)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.RedpointSystem.RedpointSystem_LambdaJob_2_Job>(SGame.RedpointSystem.RedpointSystem_LambdaJob_2_Job&,Unity.Entities.ArchetypeChunkIterator&)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.RedpointSystem.RedpointSystem_LambdaJob_2_Job>(SGame.RedpointSystem.RedpointSystem_LambdaJob_2_Job&,Unity.Entities.Chunk**,int,Unity.Entities.EntityComponentStore*)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.RedpointSystem.RedpointSystem_LambdaJob_3_Job>(SGame.RedpointSystem.RedpointSystem_LambdaJob_3_Job&,Unity.Entities.ArchetypeChunkIterator&)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.RedpointSystem.RedpointSystem_LambdaJob_3_Job>(SGame.RedpointSystem.RedpointSystem_LambdaJob_3_Job&,Unity.Entities.Chunk**,int,Unity.Entities.EntityComponentStore*)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.RewardSystem.RewardSystem_LambdaJob_0_Job>(SGame.RewardSystem.RewardSystem_LambdaJob_0_Job&,Unity.Entities.ArchetypeChunkIterator&)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.RewardSystem.RewardSystem_LambdaJob_0_Job>(SGame.RewardSystem.RewardSystem_LambdaJob_0_Job&,Unity.Entities.Chunk**,int,Unity.Entities.EntityComponentStore*)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.SpawnSystem.SpawnSystem_LambdaJob_0_Job>(SGame.SpawnSystem.SpawnSystem_LambdaJob_0_Job&,Unity.Entities.ArchetypeChunkIterator&)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.SpawnSystem.SpawnSystem_LambdaJob_0_Job>(SGame.SpawnSystem.SpawnSystem_LambdaJob_0_Job&,Unity.Entities.Chunk**,int,Unity.Entities.EntityComponentStore*)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.SpawnSystem.SpawnSystem_LambdaJob_2_Job>(SGame.SpawnSystem.SpawnSystem_LambdaJob_2_Job&,Unity.Entities.ArchetypeChunkIterator&)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.SpawnSystem.SpawnSystem_LambdaJob_2_Job>(SGame.SpawnSystem.SpawnSystem_LambdaJob_2_Job&,Unity.Entities.Chunk**,int,Unity.Entities.EntityComponentStore*)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.UI.HUDFlowSystem.HUDFlowSystem_LambdaJob_0_Job>(SGame.UI.HUDFlowSystem.HUDFlowSystem_LambdaJob_0_Job&,Unity.Entities.ArchetypeChunkIterator&)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.UI.HUDFlowSystem.HUDFlowSystem_LambdaJob_0_Job>(SGame.UI.HUDFlowSystem.HUDFlowSystem_LambdaJob_0_Job&,Unity.Entities.Chunk**,int,Unity.Entities.EntityComponentStore*)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.UI.HUDFlowSystem.HUDFlowSystem_LambdaJob_1_Job>(SGame.UI.HUDFlowSystem.HUDFlowSystem_LambdaJob_1_Job&,Unity.Entities.ArchetypeChunkIterator&)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.UI.HUDFlowSystem.HUDFlowSystem_LambdaJob_1_Job>(SGame.UI.HUDFlowSystem.HUDFlowSystem_LambdaJob_1_Job&,Unity.Entities.Chunk**,int,Unity.Entities.EntityComponentStore*)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.UI.HUDSyncSystem.HUDSyncSystem_LambdaJob_0_Job>(SGame.UI.HUDSyncSystem.HUDSyncSystem_LambdaJob_0_Job&,Unity.Entities.ArchetypeChunkIterator&)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.UI.HUDSyncSystem.HUDSyncSystem_LambdaJob_0_Job>(SGame.UI.HUDSyncSystem.HUDSyncSystem_LambdaJob_0_Job&,Unity.Entities.Chunk**,int,Unity.Entities.EntityComponentStore*)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.UserInputsystem.UserInputsystem_LambdaJob_0_Job>(SGame.UserInputsystem.UserInputsystem_LambdaJob_0_Job&,Unity.Entities.ArchetypeChunkIterator&)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.UserInputsystem.UserInputsystem_LambdaJob_0_Job>(SGame.UserInputsystem.UserInputsystem_LambdaJob_0_Job&,Unity.Entities.Chunk**,int,Unity.Entities.EntityComponentStore*)
		// Unity.Jobs.JobHandle Unity.Entities.JobEntityBatchExtensions.ScheduleInternal<SGame.FollowRotationSystem.FollowRotationSystem_LambdaJob_0_Job>(SGame.FollowRotationSystem.FollowRotationSystem_LambdaJob_0_Job&,Unity.Entities.EntityQuery,Unity.Jobs.JobHandle,Unity.Jobs.LowLevel.Unsafe.ScheduleMode,bool)
		// Unity.Jobs.JobHandle Unity.Entities.JobEntityBatchExtensions.ScheduleInternal<SGame.FollowRotationSystem.FollowRotationSystem_LambdaJob_1_Job>(SGame.FollowRotationSystem.FollowRotationSystem_LambdaJob_1_Job&,Unity.Entities.EntityQuery,Unity.Jobs.JobHandle,Unity.Jobs.LowLevel.Unsafe.ScheduleMode,bool)
		// Unity.Jobs.JobHandle Unity.Entities.JobEntityBatchExtensions.ScheduleInternal<SGame.RewardSystem.RewardSystem_LambdaJob_1_Job>(SGame.RewardSystem.RewardSystem_LambdaJob_1_Job&,Unity.Entities.EntityQuery,Unity.Jobs.JobHandle,Unity.Jobs.LowLevel.Unsafe.ScheduleMode,bool)
		// Unity.Jobs.JobHandle Unity.Entities.JobEntityBatchExtensions.ScheduleParallel<SGame.FollowRotationSystem.FollowRotationSystem_LambdaJob_0_Job>(SGame.FollowRotationSystem.FollowRotationSystem_LambdaJob_0_Job,Unity.Entities.EntityQuery,Unity.Jobs.JobHandle)
		// Unity.Jobs.JobHandle Unity.Entities.JobEntityBatchExtensions.ScheduleParallel<SGame.FollowRotationSystem.FollowRotationSystem_LambdaJob_1_Job>(SGame.FollowRotationSystem.FollowRotationSystem_LambdaJob_1_Job,Unity.Entities.EntityQuery,Unity.Jobs.JobHandle)
		// Unity.Jobs.JobHandle Unity.Entities.JobEntityBatchExtensions.ScheduleParallel<SGame.RewardSystem.RewardSystem_LambdaJob_1_Job>(SGame.RewardSystem.RewardSystem_LambdaJob_1_Job,Unity.Entities.EntityQuery,Unity.Jobs.JobHandle)
		// System.Void Unity.Entities.JobEntityBatchIndexExtensions.EarlyJobInit<SGame.MoveTargetSystem.MoveTargetSystem_LambdaJob_0_Job>()
		// Unity.Jobs.JobHandle Unity.Entities.JobEntityBatchIndexExtensions.ScheduleInternal<SGame.MoveTargetSystem.MoveTargetSystem_LambdaJob_0_Job>(SGame.MoveTargetSystem.MoveTargetSystem_LambdaJob_0_Job&,Unity.Entities.EntityQuery,Unity.Jobs.JobHandle,Unity.Jobs.LowLevel.Unsafe.ScheduleMode,bool)
		// Unity.Jobs.JobHandle Unity.Entities.JobEntityBatchIndexExtensions.ScheduleParallel<SGame.MoveTargetSystem.MoveTargetSystem_LambdaJob_0_Job>(SGame.MoveTargetSystem.MoveTargetSystem_LambdaJob_0_Job,Unity.Entities.EntityQuery,Unity.Jobs.JobHandle)
		// Unity.Transforms.Rotation Unity.Entities.SystemBase.GetComponent<Unity.Transforms.Rotation>(Unity.Entities.Entity)
		// Unity.Transforms.Scale Unity.Entities.SystemBase.GetComponent<Unity.Transforms.Scale>(Unity.Entities.Entity)
		// Unity.Transforms.Translation Unity.Entities.SystemBase.GetComponent<Unity.Transforms.Translation>(Unity.Entities.Entity)
		// Unity.Entities.ComponentDataFromEntity<Unity.Transforms.Translation> Unity.Entities.SystemBase.GetComponentDataFromEntity<Unity.Transforms.Translation>(bool)
		// bool Unity.Entities.SystemBase.HasComponent<Unity.Transforms.Scale>(Unity.Entities.Entity)
		// Unity.Entities.BufferTypeHandle<PathPositions> Unity.Entities.SystemState.GetBufferTypeHandle<PathPositions>(bool)
		// Unity.Entities.ComponentDataFromEntity<Unity.Transforms.Translation> Unity.Entities.SystemState.GetComponentDataFromEntity<Unity.Transforms.Translation>(bool)
		// Unity.Entities.ComponentTypeHandle<Follow> Unity.Entities.SystemState.GetComponentTypeHandle<Follow>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.BulletData> Unity.Entities.SystemState.GetComponentTypeHandle<SGame.BulletData>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.CharacterAttribue> Unity.Entities.SystemState.GetComponentTypeHandle<SGame.CharacterAttribue>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.CharacterSpawnSystem.CharacterSpawn> Unity.Entities.SystemState.GetComponentTypeHandle<SGame.CharacterSpawnSystem.CharacterSpawn>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.FoodTips> Unity.Entities.SystemState.GetComponentTypeHandle<SGame.FoodTips>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.LastRotation> Unity.Entities.SystemState.GetComponentTypeHandle<SGame.LastRotation>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.MoveTarget> Unity.Entities.SystemState.GetComponentTypeHandle<SGame.MoveTarget>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.RedEvent> Unity.Entities.SystemState.GetComponentTypeHandle<SGame.RedEvent>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.Redpoint> Unity.Entities.SystemState.GetComponentTypeHandle<SGame.Redpoint>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.RewardData> Unity.Entities.SystemState.GetComponentTypeHandle<SGame.RewardData>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.RewardWait> Unity.Entities.SystemState.GetComponentTypeHandle<SGame.RewardWait>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.RotationSpeed> Unity.Entities.SystemState.GetComponentTypeHandle<SGame.RotationSpeed>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.SoundData> Unity.Entities.SystemState.GetComponentTypeHandle<SGame.SoundData>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.SoundTime> Unity.Entities.SystemState.GetComponentTypeHandle<SGame.SoundTime>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.SpawnReq> Unity.Entities.SystemState.GetComponentTypeHandle<SGame.SpawnReq>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.UI.HUDFlowE> Unity.Entities.SystemState.GetComponentTypeHandle<SGame.UI.HUDFlowE>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.UserInput> Unity.Entities.SystemState.GetComponentTypeHandle<SGame.UserInput>(bool)
		// Unity.Entities.ComponentTypeHandle<Speed> Unity.Entities.SystemState.GetComponentTypeHandle<Speed>(bool)
		// Unity.Entities.ComponentTypeHandle<Unity.Transforms.Rotation> Unity.Entities.SystemState.GetComponentTypeHandle<Unity.Transforms.Rotation>(bool)
		// Unity.Entities.ComponentTypeHandle<Unity.Transforms.Translation> Unity.Entities.SystemState.GetComponentTypeHandle<Unity.Transforms.Translation>(bool)
		// System.Void Unity.Entities.SystemState.RequireSingletonForUpdate<GameInitFinish>()
		// int Unity.Entities.TypeManager.GetTypeIndex<DespawningEntity>()
		// int Unity.Entities.TypeManager.GetTypeIndex<FindPathParams>()
		// int Unity.Entities.TypeManager.GetTypeIndex<Follow>()
		// int Unity.Entities.TypeManager.GetTypeIndex<GameInitFinish>()
		// int Unity.Entities.TypeManager.GetTypeIndex<PathPositions>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.AudioSystem.DespawnedTag>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.BulletData>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.CharacterAttribue>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.CharacterSpawnSystem.CharacterInitalized>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.CharacterSpawnSystem.CharacterSpawn>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.DisableAttributeTag>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.EffectData>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.EffectSysData>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.EntitySyncGameObjectTag>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.FoodItem>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.FoodTips>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.GameObjectSyncTag>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.LastRotation>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.LiveTime>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.MoveTarget>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.RedCheck>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.RedDestroy>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.RedEvent>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.RedNode>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.RedPause>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.RedStatusChange>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.Redpoint>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.RewardData>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.RewardWait>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.RotationSpeed>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.SoundData>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.SoundRef>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.SoundTime>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.SpawnData>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.SpawnLayer>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.SpawnReq>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.SpawnSysData>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.UI.HUDFlowE>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.UI.HUDSync>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.UI.TweenTime>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.UI.UIInitalized>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.UserData>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.UserInput>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.UserSetting>()
		// int Unity.Entities.TypeManager.GetTypeIndex<Speed>()
		// int Unity.Entities.TypeManager.GetTypeIndex<Unity.Entities.Disabled>()
		// int Unity.Entities.TypeManager.GetTypeIndex<Unity.Transforms.Child>()
		// int Unity.Entities.TypeManager.GetTypeIndex<Unity.Transforms.LocalToParent>()
		// int Unity.Entities.TypeManager.GetTypeIndex<Unity.Transforms.LocalToWorld>()
		// int Unity.Entities.TypeManager.GetTypeIndex<Unity.Transforms.Parent>()
		// int Unity.Entities.TypeManager.GetTypeIndex<Unity.Transforms.Rotation>()
		// int Unity.Entities.TypeManager.GetTypeIndex<Unity.Transforms.Scale>()
		// int Unity.Entities.TypeManager.GetTypeIndex<Unity.Transforms.Translation>()
		// int Unity.Entities.TypeManager.GetTypeIndex<object>()
		// System.Void Unity.Entities.TypeManager.ManagedException<DespawningEntity>()
		// System.Void Unity.Entities.TypeManager.ManagedException<FindPathParams>()
		// System.Void Unity.Entities.TypeManager.ManagedException<Follow>()
		// System.Void Unity.Entities.TypeManager.ManagedException<GameInitFinish>()
		// System.Void Unity.Entities.TypeManager.ManagedException<PathPositions>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.AudioSystem.DespawnedTag>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.BulletData>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.CharacterAttribue>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.CharacterSpawnSystem.CharacterInitalized>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.CharacterSpawnSystem.CharacterSpawn>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.DisableAttributeTag>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.EffectData>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.EffectSysData>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.EntitySyncGameObjectTag>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.FoodItem>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.FoodTips>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.GameObjectSyncTag>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.LastRotation>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.LiveTime>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.MoveTarget>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.RedCheck>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.RedDestroy>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.RedEvent>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.RedNode>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.RedPause>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.RedStatusChange>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.Redpoint>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.RewardData>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.RewardWait>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.RotationSpeed>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.SoundData>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.SoundRef>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.SoundTime>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.SpawnData>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.SpawnLayer>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.SpawnReq>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.SpawnSysData>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.UI.HUDFlowE>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.UI.HUDSync>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.UI.TweenTime>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.UI.UIInitalized>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.UserData>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.UserInput>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.UserSetting>()
		// System.Void Unity.Entities.TypeManager.ManagedException<Speed>()
		// System.Void Unity.Entities.TypeManager.ManagedException<Unity.Entities.Disabled>()
		// System.Void Unity.Entities.TypeManager.ManagedException<Unity.Transforms.Child>()
		// System.Void Unity.Entities.TypeManager.ManagedException<Unity.Transforms.LocalToParent>()
		// System.Void Unity.Entities.TypeManager.ManagedException<Unity.Transforms.LocalToWorld>()
		// System.Void Unity.Entities.TypeManager.ManagedException<Unity.Transforms.Parent>()
		// System.Void Unity.Entities.TypeManager.ManagedException<Unity.Transforms.Rotation>()
		// System.Void Unity.Entities.TypeManager.ManagedException<Unity.Transforms.Scale>()
		// System.Void Unity.Entities.TypeManager.ManagedException<Unity.Transforms.Translation>()
		// System.Void Unity.Entities.TypeManager.ManagedException<object>()
		// object Unity.Entities.World.GetOrCreateSystem<object>()
		// System.Void Unity.Jobs.IJobBurstSchedulableExtensions.Run<Unity.Entities.PrefilterForJobEntityBatchWithIndex>(Unity.Entities.PrefilterForJobEntityBatchWithIndex)
		// Unity.Jobs.JobHandle Unity.Jobs.IJobBurstSchedulableExtensions.Schedule<Unity.Entities.PrefilterForJobEntityBatchWithIndex>(Unity.Entities.PrefilterForJobEntityBatchWithIndex,Unity.Jobs.JobHandle)
		// System.Void Unity.Properties.ContainerPropertyBag<SGame.PoolID>.AddProperty<int>(Unity.Properties.Property<SGame.PoolID,int>)
		// System.Void Unity.Properties.ContainerPropertyBag<Unity.Entities.Entity>.AddProperty<int>(Unity.Properties.Property<Unity.Entities.Entity,int>)
		// System.Void Unity.Properties.ContainerPropertyBag<Unity.Mathematics.float3>.AddProperty<float>(Unity.Properties.Property<Unity.Mathematics.float3,float>)
		// System.Void Unity.Properties.ContainerPropertyBag<UnityEngine.Color>.AddProperty<float>(Unity.Properties.Property<UnityEngine.Color,float>)
		// System.Void Unity.Properties.ContainerPropertyBag<UnityEngine.Quaternion>.AddProperty<float>(Unity.Properties.Property<UnityEngine.Quaternion,float>)
		// System.Void Unity.Properties.ContainerPropertyBag<UnityEngine.Vector2>.AddProperty<float>(Unity.Properties.Property<UnityEngine.Vector2,float>)
		// System.Void Unity.Properties.ContainerPropertyBag<UnityEngine.Vector3>.AddProperty<float>(Unity.Properties.Property<UnityEngine.Vector3,float>)
		// System.Void Unity.Properties.ContainerPropertyBag<object>.AddProperty<SGame.PoolID>(Unity.Properties.Property<object,SGame.PoolID>)
		// System.Void Unity.Properties.ContainerPropertyBag<object>.AddProperty<System.Nullable<UnityEngine.Rect>>(Unity.Properties.Property<object,System.Nullable<UnityEngine.Rect>>)
		// System.Void Unity.Properties.ContainerPropertyBag<object>.AddProperty<Unity.Entities.Entity>(Unity.Properties.Property<object,Unity.Entities.Entity>)
		// System.Void Unity.Properties.ContainerPropertyBag<object>.AddProperty<Unity.Mathematics.float3>(Unity.Properties.Property<object,Unity.Mathematics.float3>)
		// System.Void Unity.Properties.ContainerPropertyBag<object>.AddProperty<UnityEngine.Color>(Unity.Properties.Property<object,UnityEngine.Color>)
		// System.Void Unity.Properties.ContainerPropertyBag<object>.AddProperty<UnityEngine.Quaternion>(Unity.Properties.Property<object,UnityEngine.Quaternion>)
		// System.Void Unity.Properties.ContainerPropertyBag<object>.AddProperty<UnityEngine.Rect>(Unity.Properties.Property<object,UnityEngine.Rect>)
		// System.Void Unity.Properties.ContainerPropertyBag<object>.AddProperty<UnityEngine.Vector2>(Unity.Properties.Property<object,UnityEngine.Vector2>)
		// System.Void Unity.Properties.ContainerPropertyBag<object>.AddProperty<UnityEngine.Vector2Int>(Unity.Properties.Property<object,UnityEngine.Vector2Int>)
		// System.Void Unity.Properties.ContainerPropertyBag<object>.AddProperty<UnityEngine.Vector3>(Unity.Properties.Property<object,UnityEngine.Vector3>)
		// System.Void Unity.Properties.ContainerPropertyBag<object>.AddProperty<byte>(Unity.Properties.Property<object,byte>)
		// System.Void Unity.Properties.ContainerPropertyBag<object>.AddProperty<float>(Unity.Properties.Property<object,float>)
		// System.Void Unity.Properties.ContainerPropertyBag<object>.AddProperty<int>(Unity.Properties.Property<object,int>)
		// System.Void Unity.Properties.ContainerPropertyBag<object>.AddProperty<object>(Unity.Properties.Property<object,object>)
		// System.Void Unity.Properties.Internal.PropertyBagStore.AddPropertyBag<SGame.PoolID>(Unity.Properties.Internal.IPropertyBag<SGame.PoolID>)
		// System.Void Unity.Properties.Internal.PropertyBagStore.AddPropertyBag<System.Nullable<UnityEngine.Rect>>(Unity.Properties.Internal.IPropertyBag<System.Nullable<UnityEngine.Rect>>)
		// System.Void Unity.Properties.Internal.PropertyBagStore.AddPropertyBag<Unity.Entities.Entity>(Unity.Properties.Internal.IPropertyBag<Unity.Entities.Entity>)
		// System.Void Unity.Properties.Internal.PropertyBagStore.AddPropertyBag<Unity.Mathematics.float3>(Unity.Properties.Internal.IPropertyBag<Unity.Mathematics.float3>)
		// System.Void Unity.Properties.Internal.PropertyBagStore.AddPropertyBag<UnityEngine.Color>(Unity.Properties.Internal.IPropertyBag<UnityEngine.Color>)
		// System.Void Unity.Properties.Internal.PropertyBagStore.AddPropertyBag<UnityEngine.Quaternion>(Unity.Properties.Internal.IPropertyBag<UnityEngine.Quaternion>)
		// System.Void Unity.Properties.Internal.PropertyBagStore.AddPropertyBag<UnityEngine.Rect>(Unity.Properties.Internal.IPropertyBag<UnityEngine.Rect>)
		// System.Void Unity.Properties.Internal.PropertyBagStore.AddPropertyBag<UnityEngine.Vector2>(Unity.Properties.Internal.IPropertyBag<UnityEngine.Vector2>)
		// System.Void Unity.Properties.Internal.PropertyBagStore.AddPropertyBag<UnityEngine.Vector2Int>(Unity.Properties.Internal.IPropertyBag<UnityEngine.Vector2Int>)
		// System.Void Unity.Properties.Internal.PropertyBagStore.AddPropertyBag<UnityEngine.Vector3>(Unity.Properties.Internal.IPropertyBag<UnityEngine.Vector3>)
		// System.Void Unity.Properties.Internal.PropertyBagStore.AddPropertyBag<object>(Unity.Properties.Internal.IPropertyBag<object>)
		// System.Void Unity.Properties.PropertyBag.Register<SGame.PoolID>(Unity.Properties.ContainerPropertyBag<SGame.PoolID>)
		// System.Void Unity.Properties.PropertyBag.Register<System.Nullable<UnityEngine.Rect>>(Unity.Properties.ContainerPropertyBag<System.Nullable<UnityEngine.Rect>>)
		// System.Void Unity.Properties.PropertyBag.Register<Unity.Entities.Entity>(Unity.Properties.ContainerPropertyBag<Unity.Entities.Entity>)
		// System.Void Unity.Properties.PropertyBag.Register<Unity.Mathematics.float3>(Unity.Properties.ContainerPropertyBag<Unity.Mathematics.float3>)
		// System.Void Unity.Properties.PropertyBag.Register<UnityEngine.Color>(Unity.Properties.ContainerPropertyBag<UnityEngine.Color>)
		// System.Void Unity.Properties.PropertyBag.Register<UnityEngine.Quaternion>(Unity.Properties.ContainerPropertyBag<UnityEngine.Quaternion>)
		// System.Void Unity.Properties.PropertyBag.Register<UnityEngine.Rect>(Unity.Properties.ContainerPropertyBag<UnityEngine.Rect>)
		// System.Void Unity.Properties.PropertyBag.Register<UnityEngine.Vector2>(Unity.Properties.ContainerPropertyBag<UnityEngine.Vector2>)
		// System.Void Unity.Properties.PropertyBag.Register<UnityEngine.Vector2Int>(Unity.Properties.ContainerPropertyBag<UnityEngine.Vector2Int>)
		// System.Void Unity.Properties.PropertyBag.Register<UnityEngine.Vector3>(Unity.Properties.ContainerPropertyBag<UnityEngine.Vector3>)
		// System.Void Unity.Properties.PropertyBag.Register<object>(Unity.Properties.ContainerPropertyBag<object>)
		// System.Void Unity.Properties.PropertyBag.RegisterList<object,object,byte>()
		// System.Void Unity.Properties.PropertyBag.RegisterList<object,object,object>()
		// System.Void Unity.VisualScripting.EventBus.Trigger<object>(Unity.VisualScripting.EventHook,object)
		// System.Void Unity.VisualScripting.EventBus.Trigger<object>(string,UnityEngine.GameObject,object)
		// int Unity.VisualScripting.Flow.FetchValue<int>(Unity.VisualScripting.ValueInput,Unity.VisualScripting.GraphReference)
		// SGame.ChairData Unity.VisualScripting.Flow.GetValue<SGame.ChairData>(Unity.VisualScripting.ValueInput)
		// Unity.Entities.Entity Unity.VisualScripting.Flow.GetValue<Unity.Entities.Entity>(Unity.VisualScripting.ValueInput)
		// Unity.Mathematics.int2 Unity.VisualScripting.Flow.GetValue<Unity.Mathematics.int2>(Unity.VisualScripting.ValueInput)
		// UnityEngine.Vector2 Unity.VisualScripting.Flow.GetValue<UnityEngine.Vector2>(Unity.VisualScripting.ValueInput)
		// UnityEngine.Vector2Int Unity.VisualScripting.Flow.GetValue<UnityEngine.Vector2Int>(Unity.VisualScripting.ValueInput)
		// UnityEngine.Vector3 Unity.VisualScripting.Flow.GetValue<UnityEngine.Vector3>(Unity.VisualScripting.ValueInput)
		// byte Unity.VisualScripting.Flow.GetValue<byte>(Unity.VisualScripting.ValueInput)
		// double Unity.VisualScripting.Flow.GetValue<double>(Unity.VisualScripting.ValueInput)
		// float Unity.VisualScripting.Flow.GetValue<float>(Unity.VisualScripting.ValueInput)
		// int Unity.VisualScripting.Flow.GetValue<int>(Unity.VisualScripting.ValueInput)
		// object Unity.VisualScripting.Flow.GetValue<object>(Unity.VisualScripting.ValueInput)
		// uint Unity.VisualScripting.Flow.GetValue<uint>(Unity.VisualScripting.ValueInput)
		// object Unity.VisualScripting.GraphPointer.GetElementData<object>(Unity.VisualScripting.IGraphElementWithData)
		// Unity.VisualScripting.ValueInput Unity.VisualScripting.Unit.ValueInput<SGame.ChairData>(string)
		// Unity.VisualScripting.ValueInput Unity.VisualScripting.Unit.ValueInput<SGame.ChairData>(string,SGame.ChairData)
		// Unity.VisualScripting.ValueInput Unity.VisualScripting.Unit.ValueInput<Unity.Entities.Entity>(string)
		// Unity.VisualScripting.ValueInput Unity.VisualScripting.Unit.ValueInput<Unity.Mathematics.int2>(string)
		// Unity.VisualScripting.ValueInput Unity.VisualScripting.Unit.ValueInput<Unity.Mathematics.int2>(string,Unity.Mathematics.int2)
		// Unity.VisualScripting.ValueInput Unity.VisualScripting.Unit.ValueInput<UnityEngine.Vector2>(string)
		// Unity.VisualScripting.ValueInput Unity.VisualScripting.Unit.ValueInput<UnityEngine.Vector2Int>(string)
		// Unity.VisualScripting.ValueInput Unity.VisualScripting.Unit.ValueInput<UnityEngine.Vector2Int>(string,UnityEngine.Vector2Int)
		// Unity.VisualScripting.ValueInput Unity.VisualScripting.Unit.ValueInput<UnityEngine.Vector3>(string)
		// Unity.VisualScripting.ValueInput Unity.VisualScripting.Unit.ValueInput<UnityEngine.Vector3>(string,UnityEngine.Vector3)
		// Unity.VisualScripting.ValueInput Unity.VisualScripting.Unit.ValueInput<byte>(string)
		// Unity.VisualScripting.ValueInput Unity.VisualScripting.Unit.ValueInput<byte>(string,byte)
		// Unity.VisualScripting.ValueInput Unity.VisualScripting.Unit.ValueInput<double>(string)
		// Unity.VisualScripting.ValueInput Unity.VisualScripting.Unit.ValueInput<float>(string)
		// Unity.VisualScripting.ValueInput Unity.VisualScripting.Unit.ValueInput<int>(string)
		// Unity.VisualScripting.ValueInput Unity.VisualScripting.Unit.ValueInput<int>(string,int)
		// Unity.VisualScripting.ValueInput Unity.VisualScripting.Unit.ValueInput<object>(string)
		// Unity.VisualScripting.ValueInput Unity.VisualScripting.Unit.ValueInput<object>(string,object)
		// Unity.VisualScripting.ValueInput Unity.VisualScripting.Unit.ValueInput<uint>(string)
		// Unity.VisualScripting.ValueInput Unity.VisualScripting.Unit.ValueInput<uint>(string,uint)
		// Unity.VisualScripting.ValueOutput Unity.VisualScripting.Unit.ValueOutput<SGame.ChairData>(string,System.Func<Unity.VisualScripting.Flow,SGame.ChairData>)
		// Unity.VisualScripting.ValueOutput Unity.VisualScripting.Unit.ValueOutput<Unity.Entities.Entity>(string,System.Func<Unity.VisualScripting.Flow,Unity.Entities.Entity>)
		// Unity.VisualScripting.ValueOutput Unity.VisualScripting.Unit.ValueOutput<Unity.Entities.EntityManager>(string,System.Func<Unity.VisualScripting.Flow,Unity.Entities.EntityManager>)
		// Unity.VisualScripting.ValueOutput Unity.VisualScripting.Unit.ValueOutput<Unity.Mathematics.int2>(string,System.Func<Unity.VisualScripting.Flow,Unity.Mathematics.int2>)
		// Unity.VisualScripting.ValueOutput Unity.VisualScripting.Unit.ValueOutput<UnityEngine.Vector2>(string,System.Func<Unity.VisualScripting.Flow,UnityEngine.Vector2>)
		// Unity.VisualScripting.ValueOutput Unity.VisualScripting.Unit.ValueOutput<UnityEngine.Vector2Int>(string,System.Func<Unity.VisualScripting.Flow,UnityEngine.Vector2Int>)
		// Unity.VisualScripting.ValueOutput Unity.VisualScripting.Unit.ValueOutput<byte>(string,System.Func<Unity.VisualScripting.Flow,byte>)
		// Unity.VisualScripting.ValueOutput Unity.VisualScripting.Unit.ValueOutput<double>(string,System.Func<Unity.VisualScripting.Flow,double>)
		// Unity.VisualScripting.ValueOutput Unity.VisualScripting.Unit.ValueOutput<float>(string,System.Func<Unity.VisualScripting.Flow,float>)
		// Unity.VisualScripting.ValueOutput Unity.VisualScripting.Unit.ValueOutput<int>(string,System.Func<Unity.VisualScripting.Flow,int>)
		// Unity.VisualScripting.ValueOutput Unity.VisualScripting.Unit.ValueOutput<object>(string)
		// Unity.VisualScripting.ValueOutput Unity.VisualScripting.Unit.ValueOutput<object>(string,System.Func<Unity.VisualScripting.Flow,object>)
		// System.Void Unity.VisualScripting.XHashSetPool.Free<object>(System.Collections.Generic.HashSet<object>)
		// object UnityEngine.Component.GetComponent<object>()
		// object UnityEngine.Component.GetComponentInChildren<object>()
		// object UnityEngine.Component.GetComponentInParent<object>()
		// object[] UnityEngine.Component.GetComponents<object>()
		// object UnityEngine.GameObject.AddComponent<object>()
		// object UnityEngine.GameObject.GetComponent<object>()
		// object UnityEngine.GameObject.GetComponentInChildren<object>()
		// object UnityEngine.GameObject.GetComponentInChildren<object>(bool)
		// object[] UnityEngine.GameObject.GetComponents<object>()
		// object[] UnityEngine.GameObject.GetComponentsInChildren<object>()
		// object[] UnityEngine.GameObject.GetComponentsInChildren<object>(bool)
		// Http.HttpPackage UnityEngine.JsonUtility.FromJson<Http.HttpPackage>(string)
		// object UnityEngine.JsonUtility.FromJson<object>(string)
		// object UnityEngine.Object.FindAnyObjectByType<object>(UnityEngine.FindObjectsInactive)
		// object UnityEngine.Object.FindObjectOfType<object>()
		// object UnityEngine.Object.FindObjectOfType<object>(bool)
		// object UnityEngine.Object.Instantiate<object>(object)
		// object UnityEngine.Object.Instantiate<object>(object,UnityEngine.Transform)
		// object UnityEngine.Object.Instantiate<object>(object,UnityEngine.Transform,bool)
		// object UnityEngine.Resources.Load<object>(string)
	}
}