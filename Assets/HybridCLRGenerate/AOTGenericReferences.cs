using System.Collections.Generic;
public class AOTGenericReferences : UnityEngine.MonoBehaviour
{

	// {{ AOT assemblies
	public static readonly IReadOnlyList<string> PatchedAOTAssemblyList = new List<string>
	{
		"Core.dll",
		"FlatBuffers.dll",
		"System.Core.dll",
		"Unity.Burst.dll",
		"Unity.Collections.dll",
		"Unity.Entities.dll",
		"Unity.Properties.dll",
		"Unity.Serialization.dll",
		"Unity.VisualScripting.Core.dll",
		"Unity.VisualScripting.Flow.dll",
		"UnityEngine.CoreModule.dll",
		"UnityEngine.JSONSerializeModule.dll",
		"mscorlib.dll",
		"plyLib.dll",
	};
	// }}

	// {{ constraint implement type
	// }} 

	// {{ AOT generic types
	// Callback<GamePackage>
	// Callback<int,float,object>
	// Callback<int,long,int>
	// Callback<int,object,float>
	// Callback<int,object>
	// Callback<object,object>
	// Callback<object>
	// FlatBuffers.Offset<GameConfigs.Bet>
	// FlatBuffers.Offset<GameConfigs.BetRowData>
	// FlatBuffers.Offset<GameConfigs.Build_Bank>
	// FlatBuffers.Offset<GameConfigs.Build_BankRowData>
	// FlatBuffers.Offset<GameConfigs.Build_ChanceCard>
	// FlatBuffers.Offset<GameConfigs.Build_ChanceCardRowData>
	// FlatBuffers.Offset<GameConfigs.Build_Coin>
	// FlatBuffers.Offset<GameConfigs.Build_CoinRowData>
	// FlatBuffers.Offset<GameConfigs.Build_Game>
	// FlatBuffers.Offset<GameConfigs.Build_GameRowData>
	// FlatBuffers.Offset<GameConfigs.Build_Trap>
	// FlatBuffers.Offset<GameConfigs.Build_TrapRowData>
	// FlatBuffers.Offset<GameConfigs.Build_Visit>
	// FlatBuffers.Offset<GameConfigs.Build_VisitRowData>
	// FlatBuffers.Offset<GameConfigs.Build_house>
	// FlatBuffers.Offset<GameConfigs.Build_houseRowData>
	// FlatBuffers.Offset<GameConfigs.Design_Global>
	// FlatBuffers.Offset<GameConfigs.Design_GlobalRowData>
	// FlatBuffers.Offset<GameConfigs.Equip>
	// FlatBuffers.Offset<GameConfigs.EquipRowData>
	// FlatBuffers.Offset<GameConfigs.Event_Build>
	// FlatBuffers.Offset<GameConfigs.Event_BuildRowData>
	// FlatBuffers.Offset<GameConfigs.Event_Group>
	// FlatBuffers.Offset<GameConfigs.Event_GroupRowData>
	// FlatBuffers.Offset<GameConfigs.Event_Part>
	// FlatBuffers.Offset<GameConfigs.Event_PartRowData>
	// FlatBuffers.Offset<GameConfigs.Game_Event>
	// FlatBuffers.Offset<GameConfigs.Game_EventRowData>
	// FlatBuffers.Offset<GameConfigs.Game_Function>
	// FlatBuffers.Offset<GameConfigs.Game_FunctionRowData>
	// FlatBuffers.Offset<GameConfigs.Grid>
	// FlatBuffers.Offset<GameConfigs.GridRowData>
	// FlatBuffers.Offset<GameConfigs.Grid_Path>
	// FlatBuffers.Offset<GameConfigs.Grid_PathRowData>
	// FlatBuffers.Offset<GameConfigs.Item>
	// FlatBuffers.Offset<GameConfigs.ItemRowData>
	// FlatBuffers.Offset<GameConfigs.Item_ChanceCard>
	// FlatBuffers.Offset<GameConfigs.Item_ChanceCardRowData>
	// FlatBuffers.Offset<GameConfigs.Language_CHN>
	// FlatBuffers.Offset<GameConfigs.Language_CHNRowData>
	// FlatBuffers.Offset<GameConfigs.Language_EN>
	// FlatBuffers.Offset<GameConfigs.Language_ENRowData>
	// FlatBuffers.Offset<GameConfigs.Language_HI>
	// FlatBuffers.Offset<GameConfigs.Language_HIRowData>
	// FlatBuffers.Offset<GameConfigs.Travel_Event>
	// FlatBuffers.Offset<GameConfigs.Travel_EventRowData>
	// FlatBuffers.Offset<GameConfigs.game_global>
	// FlatBuffers.Offset<GameConfigs.game_globalRowData>
	// FlatBuffers.Offset<GameConfigs.ui_groups>
	// FlatBuffers.Offset<GameConfigs.ui_groupsRowData>
	// FlatBuffers.Offset<GameConfigs.ui_res>
	// FlatBuffers.Offset<GameConfigs.ui_resRowData>
	// SGame.EventHandleV1<GamePackage>
	// SGame.EventHandleV2<int,object>
	// SGame.EventHandleV2<object,object>
	// SGame.EventHandleV3<int,float,object>
	// SGame.EventHandleV3<int,long,int>
	// SGame.MonoSingleton<object>
	// SGame.ObjectPool.<GetEnumerator>d__10<object>
	// SGame.ObjectPool.AllocDelegate<object>
	// SGame.ObjectPool.DeSpawnDelegate<object>
	// SGame.ObjectPool.SpawnDelegate<object>
	// SGame.ObjectPool<object>
	// SGame.Singleton<object>
	// SGame.WaitEvent<object>
	// StateMachine.State<int>
	// StateMachine.StateFunc<int>
	// StateMachine<int>
	// System.Action<SDK.TDSDK.ItemInfo>
	// System.Action<SGame.GameLoopRequest>
	// System.Action<SGame.ItemData.Value>
	// System.Action<SGame.ObjectPool.ExtendData<object>>
	// System.Action<System.UIntPtr>
	// System.Action<Unity.Serialization.Json.DeserializationEvent>
	// System.Action<Unity.VisualScripting.CustomEventArgs>
	// System.Action<UnityEngine.CombineInstance>
	// System.Action<byte,object,object>
	// System.Action<byte,object>
	// System.Action<byte>
	// System.Action<float>
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
	// System.Collections.Generic.ArraySortHelper<SDK.TDSDK.ItemInfo>
	// System.Collections.Generic.ArraySortHelper<SGame.GameLoopRequest>
	// System.Collections.Generic.ArraySortHelper<SGame.ItemData.Value>
	// System.Collections.Generic.ArraySortHelper<SGame.ObjectPool.ExtendData<object>>
	// System.Collections.Generic.ArraySortHelper<System.UIntPtr>
	// System.Collections.Generic.ArraySortHelper<Unity.Serialization.Json.DeserializationEvent>
	// System.Collections.Generic.ArraySortHelper<UnityEngine.CombineInstance>
	// System.Collections.Generic.ArraySortHelper<int>
	// System.Collections.Generic.ArraySortHelper<object>
	// System.Collections.Generic.Comparer<SDK.TDSDK.ItemInfo>
	// System.Collections.Generic.Comparer<SGame.GameLoopRequest>
	// System.Collections.Generic.Comparer<SGame.ItemData.Value>
	// System.Collections.Generic.Comparer<SGame.ObjectPool.ExtendData<object>>
	// System.Collections.Generic.Comparer<System.UIntPtr>
	// System.Collections.Generic.Comparer<Unity.Serialization.Json.DeserializationEvent>
	// System.Collections.Generic.Comparer<UnityEngine.CombineInstance>
	// System.Collections.Generic.Comparer<int>
	// System.Collections.Generic.Comparer<object>
	// System.Collections.Generic.Dictionary.Enumerator<int,int>
	// System.Collections.Generic.Dictionary.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.Enumerator<object,Unity.Entities.Entity>
	// System.Collections.Generic.Dictionary.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.Enumerator<object,uint>
	// System.Collections.Generic.Dictionary.Enumerator<uint,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,int>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,Unity.Entities.Entity>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,uint>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<uint,object>
	// System.Collections.Generic.Dictionary.KeyCollection<int,int>
	// System.Collections.Generic.Dictionary.KeyCollection<int,object>
	// System.Collections.Generic.Dictionary.KeyCollection<object,Unity.Entities.Entity>
	// System.Collections.Generic.Dictionary.KeyCollection<object,int>
	// System.Collections.Generic.Dictionary.KeyCollection<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection<object,uint>
	// System.Collections.Generic.Dictionary.KeyCollection<uint,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,int>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,Unity.Entities.Entity>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,uint>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<uint,object>
	// System.Collections.Generic.Dictionary.ValueCollection<int,int>
	// System.Collections.Generic.Dictionary.ValueCollection<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection<object,Unity.Entities.Entity>
	// System.Collections.Generic.Dictionary.ValueCollection<object,int>
	// System.Collections.Generic.Dictionary.ValueCollection<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection<object,uint>
	// System.Collections.Generic.Dictionary.ValueCollection<uint,object>
	// System.Collections.Generic.Dictionary<int,int>
	// System.Collections.Generic.Dictionary<int,object>
	// System.Collections.Generic.Dictionary<object,Unity.Entities.Entity>
	// System.Collections.Generic.Dictionary<object,int>
	// System.Collections.Generic.Dictionary<object,object>
	// System.Collections.Generic.Dictionary<object,uint>
	// System.Collections.Generic.Dictionary<uint,object>
	// System.Collections.Generic.EqualityComparer<Unity.Entities.Entity>
	// System.Collections.Generic.EqualityComparer<Unity.Properties.Internal.PropertyWrapper<Http.HttpPackage>>
	// System.Collections.Generic.EqualityComparer<Unity.Properties.Internal.PropertyWrapper<object>>
	// System.Collections.Generic.EqualityComparer<int>
	// System.Collections.Generic.EqualityComparer<object>
	// System.Collections.Generic.EqualityComparer<uint>
	// System.Collections.Generic.HashSet.Enumerator<object>
	// System.Collections.Generic.HashSet<object>
	// System.Collections.Generic.ICollection<SDK.TDSDK.ItemInfo>
	// System.Collections.Generic.ICollection<SGame.GameLoopRequest>
	// System.Collections.Generic.ICollection<SGame.ItemData.Value>
	// System.Collections.Generic.ICollection<SGame.ObjectPool.ExtendData<object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,Unity.Entities.Entity>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,uint>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<uint,object>>
	// System.Collections.Generic.ICollection<System.UIntPtr>
	// System.Collections.Generic.ICollection<Unity.Serialization.Json.DeserializationEvent>
	// System.Collections.Generic.ICollection<UnityEngine.CombineInstance>
	// System.Collections.Generic.ICollection<int>
	// System.Collections.Generic.ICollection<object>
	// System.Collections.Generic.IComparer<SDK.TDSDK.ItemInfo>
	// System.Collections.Generic.IComparer<SGame.GameLoopRequest>
	// System.Collections.Generic.IComparer<SGame.ItemData.Value>
	// System.Collections.Generic.IComparer<SGame.ObjectPool.ExtendData<object>>
	// System.Collections.Generic.IComparer<System.UIntPtr>
	// System.Collections.Generic.IComparer<Unity.Serialization.Json.DeserializationEvent>
	// System.Collections.Generic.IComparer<UnityEngine.CombineInstance>
	// System.Collections.Generic.IComparer<int>
	// System.Collections.Generic.IComparer<object>
	// System.Collections.Generic.IDictionary<object,object>
	// System.Collections.Generic.IEnumerable<SDK.TDSDK.ItemInfo>
	// System.Collections.Generic.IEnumerable<SGame.GameLoopRequest>
	// System.Collections.Generic.IEnumerable<SGame.ItemData.Value>
	// System.Collections.Generic.IEnumerable<SGame.ObjectPool.ExtendData<object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,Unity.Entities.Entity>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,uint>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<uint,object>>
	// System.Collections.Generic.IEnumerable<System.UIntPtr>
	// System.Collections.Generic.IEnumerable<Unity.Serialization.Json.DeserializationEvent>
	// System.Collections.Generic.IEnumerable<UnityEngine.CombineInstance>
	// System.Collections.Generic.IEnumerable<int>
	// System.Collections.Generic.IEnumerable<object>
	// System.Collections.Generic.IEnumerator<NetPackage>
	// System.Collections.Generic.IEnumerator<SDK.TDSDK.ItemInfo>
	// System.Collections.Generic.IEnumerator<SGame.GameLoopRequest>
	// System.Collections.Generic.IEnumerator<SGame.ItemData.Value>
	// System.Collections.Generic.IEnumerator<SGame.ObjectPool.ExtendData<object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,Unity.Entities.Entity>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,uint>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<uint,object>>
	// System.Collections.Generic.IEnumerator<System.UIntPtr>
	// System.Collections.Generic.IEnumerator<Unity.Serialization.Json.DeserializationEvent>
	// System.Collections.Generic.IEnumerator<UnityEngine.CombineInstance>
	// System.Collections.Generic.IEnumerator<int>
	// System.Collections.Generic.IEnumerator<object>
	// System.Collections.Generic.IEqualityComparer<int>
	// System.Collections.Generic.IEqualityComparer<object>
	// System.Collections.Generic.IEqualityComparer<uint>
	// System.Collections.Generic.IList<SDK.TDSDK.ItemInfo>
	// System.Collections.Generic.IList<SGame.GameLoopRequest>
	// System.Collections.Generic.IList<SGame.ItemData.Value>
	// System.Collections.Generic.IList<SGame.ObjectPool.ExtendData<object>>
	// System.Collections.Generic.IList<System.UIntPtr>
	// System.Collections.Generic.IList<Unity.Serialization.Json.DeserializationEvent>
	// System.Collections.Generic.IList<UnityEngine.CombineInstance>
	// System.Collections.Generic.IList<int>
	// System.Collections.Generic.IList<object>
	// System.Collections.Generic.KeyValuePair<int,int>
	// System.Collections.Generic.KeyValuePair<int,object>
	// System.Collections.Generic.KeyValuePair<object,Unity.Entities.Entity>
	// System.Collections.Generic.KeyValuePair<object,int>
	// System.Collections.Generic.KeyValuePair<object,object>
	// System.Collections.Generic.KeyValuePair<object,uint>
	// System.Collections.Generic.KeyValuePair<uint,object>
	// System.Collections.Generic.List.Enumerator<SDK.TDSDK.ItemInfo>
	// System.Collections.Generic.List.Enumerator<SGame.GameLoopRequest>
	// System.Collections.Generic.List.Enumerator<SGame.ItemData.Value>
	// System.Collections.Generic.List.Enumerator<SGame.ObjectPool.ExtendData<object>>
	// System.Collections.Generic.List.Enumerator<System.UIntPtr>
	// System.Collections.Generic.List.Enumerator<Unity.Serialization.Json.DeserializationEvent>
	// System.Collections.Generic.List.Enumerator<UnityEngine.CombineInstance>
	// System.Collections.Generic.List.Enumerator<int>
	// System.Collections.Generic.List.Enumerator<object>
	// System.Collections.Generic.List<SDK.TDSDK.ItemInfo>
	// System.Collections.Generic.List<SGame.GameLoopRequest>
	// System.Collections.Generic.List<SGame.ItemData.Value>
	// System.Collections.Generic.List<SGame.ObjectPool.ExtendData<object>>
	// System.Collections.Generic.List<System.UIntPtr>
	// System.Collections.Generic.List<Unity.Serialization.Json.DeserializationEvent>
	// System.Collections.Generic.List<UnityEngine.CombineInstance>
	// System.Collections.Generic.List<int>
	// System.Collections.Generic.List<object>
	// System.Collections.Generic.ObjectComparer<SDK.TDSDK.ItemInfo>
	// System.Collections.Generic.ObjectComparer<SGame.GameLoopRequest>
	// System.Collections.Generic.ObjectComparer<SGame.ItemData.Value>
	// System.Collections.Generic.ObjectComparer<SGame.ObjectPool.ExtendData<object>>
	// System.Collections.Generic.ObjectComparer<System.UIntPtr>
	// System.Collections.Generic.ObjectComparer<Unity.Serialization.Json.DeserializationEvent>
	// System.Collections.Generic.ObjectComparer<UnityEngine.CombineInstance>
	// System.Collections.Generic.ObjectComparer<int>
	// System.Collections.Generic.ObjectComparer<object>
	// System.Collections.Generic.ObjectEqualityComparer<Unity.Entities.Entity>
	// System.Collections.Generic.ObjectEqualityComparer<Unity.Properties.Internal.PropertyWrapper<Http.HttpPackage>>
	// System.Collections.Generic.ObjectEqualityComparer<Unity.Properties.Internal.PropertyWrapper<object>>
	// System.Collections.Generic.ObjectEqualityComparer<int>
	// System.Collections.Generic.ObjectEqualityComparer<object>
	// System.Collections.Generic.ObjectEqualityComparer<uint>
	// System.Collections.Generic.Stack.Enumerator<int>
	// System.Collections.Generic.Stack<int>
	// System.Collections.ObjectModel.ReadOnlyCollection<SDK.TDSDK.ItemInfo>
	// System.Collections.ObjectModel.ReadOnlyCollection<SGame.GameLoopRequest>
	// System.Collections.ObjectModel.ReadOnlyCollection<SGame.ItemData.Value>
	// System.Collections.ObjectModel.ReadOnlyCollection<SGame.ObjectPool.ExtendData<object>>
	// System.Collections.ObjectModel.ReadOnlyCollection<System.UIntPtr>
	// System.Collections.ObjectModel.ReadOnlyCollection<Unity.Serialization.Json.DeserializationEvent>
	// System.Collections.ObjectModel.ReadOnlyCollection<UnityEngine.CombineInstance>
	// System.Collections.ObjectModel.ReadOnlyCollection<int>
	// System.Collections.ObjectModel.ReadOnlyCollection<object>
	// System.Comparison<SDK.TDSDK.ItemInfo>
	// System.Comparison<SGame.GameLoopRequest>
	// System.Comparison<SGame.ItemData.Value>
	// System.Comparison<SGame.ObjectPool.ExtendData<object>>
	// System.Comparison<System.UIntPtr>
	// System.Comparison<Unity.Serialization.Json.DeserializationEvent>
	// System.Comparison<UnityEngine.CombineInstance>
	// System.Comparison<int>
	// System.Comparison<object>
	// System.Func<GameConfigs.ui_resRowData,byte>
	// System.Func<Unity.Serialization.Json.DeserializationEvent,byte>
	// System.Func<Unity.Serialization.Json.DeserializationEvent,object>
	// System.Func<byte>
	// System.Func<int,byte>
	// System.Func<int,object>
	// System.Func<object,Unity.Entities.EntityManager>
	// System.Func<object,UnityEngine.Vector2Int>
	// System.Func<object,byte>
	// System.Func<object,int,int,int>
	// System.Func<object,int,int,object>
	// System.Func<object,object>
	// System.IEquatable<object>
	// System.Linq.Buffer<object>
	// System.Linq.Enumerable.Iterator<int>
	// System.Linq.Enumerable.Iterator<object>
	// System.Linq.Enumerable.WhereArrayIterator<object>
	// System.Linq.Enumerable.WhereEnumerableIterator<object>
	// System.Linq.Enumerable.WhereListIterator<object>
	// System.Linq.Enumerable.WhereSelectArrayIterator<int,object>
	// System.Linq.Enumerable.WhereSelectArrayIterator<object,object>
	// System.Linq.Enumerable.WhereSelectEnumerableIterator<int,object>
	// System.Linq.Enumerable.WhereSelectEnumerableIterator<object,object>
	// System.Linq.Enumerable.WhereSelectListIterator<int,object>
	// System.Linq.Enumerable.WhereSelectListIterator<object,object>
	// System.Nullable<GameConfigs.BetRowData>
	// System.Nullable<GameConfigs.Build_BankRowData>
	// System.Nullable<GameConfigs.Build_ChanceCardRowData>
	// System.Nullable<GameConfigs.Build_CoinRowData>
	// System.Nullable<GameConfigs.Build_GameRowData>
	// System.Nullable<GameConfigs.Build_TrapRowData>
	// System.Nullable<GameConfigs.Build_VisitRowData>
	// System.Nullable<GameConfigs.Build_houseRowData>
	// System.Nullable<GameConfigs.Design_GlobalRowData>
	// System.Nullable<GameConfigs.EquipRowData>
	// System.Nullable<GameConfigs.Event_BuildRowData>
	// System.Nullable<GameConfigs.Event_GroupRowData>
	// System.Nullable<GameConfigs.Event_PartRowData>
	// System.Nullable<GameConfigs.Game_EventRowData>
	// System.Nullable<GameConfigs.Game_FunctionRowData>
	// System.Nullable<GameConfigs.GridRowData>
	// System.Nullable<GameConfigs.Grid_PathRowData>
	// System.Nullable<GameConfigs.ItemRowData>
	// System.Nullable<GameConfigs.Item_ChanceCardRowData>
	// System.Nullable<GameConfigs.Language_CHNRowData>
	// System.Nullable<GameConfigs.Language_ENRowData>
	// System.Nullable<GameConfigs.Language_HIRowData>
	// System.Nullable<GameConfigs.Travel_EventRowData>
	// System.Nullable<GameConfigs.game_globalRowData>
	// System.Nullable<GameConfigs.ui_groupsRowData>
	// System.Nullable<GameConfigs.ui_resRowData>
	// System.Nullable<byte>
	// System.Nullable<int>
	// System.Predicate<SDK.TDSDK.ItemInfo>
	// System.Predicate<SGame.GameLoopRequest>
	// System.Predicate<SGame.ItemData.Value>
	// System.Predicate<SGame.ObjectPool.ExtendData<object>>
	// System.Predicate<System.UIntPtr>
	// System.Predicate<Unity.Serialization.Json.DeserializationEvent>
	// System.Predicate<UnityEngine.CombineInstance>
	// System.Predicate<int>
	// System.Predicate<object>
	// System.ReadOnlySpan<ushort>
	// System.Runtime.CompilerServices.ConditionalWeakTable.CreateValueCallback<object,object>
	// System.Runtime.CompilerServices.ConditionalWeakTable.Enumerator<object,object>
	// System.Runtime.CompilerServices.ConditionalWeakTable<object,object>
	// System.Span<ushort>
	// System.WeakReference<object>
	// Unity.Burst.SharedStatic<System.IntPtr>
	// Unity.Burst.SharedStatic<int>
	// Unity.Collections.LowLevel.Unsafe.UnsafeList.ParallelReader<System.IntPtr>
	// Unity.Collections.LowLevel.Unsafe.UnsafeList.ParallelReader<Unity.Entities.ArchetypeChunk>
	// Unity.Collections.LowLevel.Unsafe.UnsafeList.ParallelReader<int>
	// Unity.Collections.LowLevel.Unsafe.UnsafeList.ParallelWriter<System.IntPtr>
	// Unity.Collections.LowLevel.Unsafe.UnsafeList.ParallelWriter<Unity.Entities.ArchetypeChunk>
	// Unity.Collections.LowLevel.Unsafe.UnsafeList.ParallelWriter<int>
	// Unity.Collections.LowLevel.Unsafe.UnsafeList<System.IntPtr>
	// Unity.Collections.LowLevel.Unsafe.UnsafeList<Unity.Entities.ArchetypeChunk>
	// Unity.Collections.LowLevel.Unsafe.UnsafeList<int>
	// Unity.Collections.LowLevel.Unsafe.UnsafePtrList.ParallelReader<Unity.Entities.Chunk>
	// Unity.Collections.LowLevel.Unsafe.UnsafePtrList.ParallelWriter<Unity.Entities.Chunk>
	// Unity.Collections.LowLevel.Unsafe.UnsafePtrList<Unity.Entities.Chunk>
	// Unity.Collections.NativeArray.Enumerator<int>
	// Unity.Collections.NativeArray.ReadOnly.Enumerator<int>
	// Unity.Collections.NativeArray.ReadOnly<int>
	// Unity.Collections.NativeArray<int>
	// Unity.Entities.ComponentTypeHandle<SGame.EntitySyncGameObject>
	// Unity.Entities.ComponentTypeHandle<SGame.FloatTextData>
	// Unity.Entities.ComponentTypeHandle<SGame.UserInput>
	// Unity.Entities.ComponentTypeHandle<Unity.Transforms.LocalToWorld>
	// Unity.Entities.ComponentTypeHandle<Unity.Transforms.Translation>
	// Unity.Entities.ComponentTypeHandle<object>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.DespawnFloatTextSystem.DespawnFloatTextSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.EntitySyncGameObjectSystem.EntitySyncGameObjectSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.FloatTextSystem.FloatTextSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.Http.HttpSystem.HttpSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.Http.HttpSystemSpawn.HttpSystemSpawn_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.SpawnFloatTextSystem.SpawnFloatTextSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer.ExecuteJobFunction<SGame.UserInputsystem.UserInputsystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.DespawnFloatTextSystem.DespawnFloatTextSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.EntitySyncGameObjectSystem.EntitySyncGameObjectSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.FloatTextSystem.FloatTextSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.Http.HttpSystem.HttpSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.Http.HttpSystemSpawn.HttpSystemSpawn_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.SpawnFloatTextSystem.SpawnFloatTextSystem_LambdaJob_0_Job>
	// Unity.Entities.JobEntityBatchExtensions.JobEntityBatchProducer<SGame.UserInputsystem.UserInputsystem_LambdaJob_0_Job>
	// Unity.Entities.ManagedComponentAccessor<object>
	// Unity.Properties.AOT.PropertyBagGenerator<Unity.Mathematics.float3>
	// Unity.Properties.AOT.PropertyBagGenerator<Unity.Properties.Internal.PropertyWrapper<Http.HttpPackage>>
	// Unity.Properties.AOT.PropertyBagGenerator<Unity.Properties.Internal.PropertyWrapper<object>>
	// Unity.Properties.AOT.PropertyBagGenerator<UnityEngine.Color>
	// Unity.Properties.AOT.PropertyBagGenerator<object>
	// Unity.Properties.AOT.PropertyGenerator<Unity.Mathematics.float3,float>
	// Unity.Properties.AOT.PropertyGenerator<Unity.Properties.Internal.PropertyWrapper<Http.HttpPackage>,Http.HttpPackage>
	// Unity.Properties.AOT.PropertyGenerator<Unity.Properties.Internal.PropertyWrapper<object>,object>
	// Unity.Properties.AOT.PropertyGenerator<UnityEngine.Color,float>
	// Unity.Properties.AOT.PropertyGenerator<object,Unity.Mathematics.float3>
	// Unity.Properties.AOT.PropertyGenerator<object,UnityEngine.Color>
	// Unity.Properties.AOT.PropertyGenerator<object,byte>
	// Unity.Properties.AOT.PropertyGenerator<object,float>
	// Unity.Properties.AOT.PropertyGenerator<object,int>
	// Unity.Properties.AOT.PropertyGenerator<object,object>
	// Unity.Properties.ContainerPropertyBag.PropertyCollection<Unity.Mathematics.float3>
	// Unity.Properties.ContainerPropertyBag.PropertyCollection<Unity.Properties.Internal.PropertyWrapper<Http.HttpPackage>>
	// Unity.Properties.ContainerPropertyBag.PropertyCollection<Unity.Properties.Internal.PropertyWrapper<object>>
	// Unity.Properties.ContainerPropertyBag.PropertyCollection<UnityEngine.Color>
	// Unity.Properties.ContainerPropertyBag.PropertyCollection<object>
	// Unity.Properties.ContainerPropertyBag<Unity.Mathematics.float3>
	// Unity.Properties.ContainerPropertyBag<Unity.Properties.Internal.PropertyWrapper<Http.HttpPackage>>
	// Unity.Properties.ContainerPropertyBag<Unity.Properties.Internal.PropertyWrapper<object>>
	// Unity.Properties.ContainerPropertyBag<UnityEngine.Color>
	// Unity.Properties.ContainerPropertyBag<object>
	// Unity.Properties.Internal.ICollectionPropertyBagAccept<Unity.Properties.Internal.PropertyWrapper<Http.HttpPackage>>
	// Unity.Properties.Internal.ICollectionPropertyBagAccept<Unity.Properties.Internal.PropertyWrapper<object>>
	// Unity.Properties.Internal.IDictionaryPropertyBagAccept<Unity.Properties.Internal.PropertyWrapper<Http.HttpPackage>>
	// Unity.Properties.Internal.IDictionaryPropertyBagAccept<Unity.Properties.Internal.PropertyWrapper<object>>
	// Unity.Properties.Internal.IListPropertyBagAccept<Unity.Properties.Internal.PropertyWrapper<Http.HttpPackage>>
	// Unity.Properties.Internal.IListPropertyBagAccept<Unity.Properties.Internal.PropertyWrapper<object>>
	// Unity.Properties.Internal.IPropertyBagAccept<Unity.Properties.Internal.PropertyWrapper<Http.HttpPackage>>
	// Unity.Properties.Internal.IPropertyBagAccept<Unity.Properties.Internal.PropertyWrapper<object>>
	// Unity.Properties.Internal.ISetPropertyBagAccept<Unity.Properties.Internal.PropertyWrapper<Http.HttpPackage>>
	// Unity.Properties.Internal.ISetPropertyBagAccept<Unity.Properties.Internal.PropertyWrapper<object>>
	// Unity.Properties.Internal.PropertyWrapper.Property<Http.HttpPackage>
	// Unity.Properties.Internal.PropertyWrapper.Property<object>
	// Unity.Properties.Internal.PropertyWrapper.PropertyBag<Http.HttpPackage>
	// Unity.Properties.Internal.PropertyWrapper.PropertyBag<object>
	// Unity.Properties.Internal.PropertyWrapper<Http.HttpPackage>
	// Unity.Properties.Internal.PropertyWrapper<object>
	// Unity.Properties.Property.<GetAttributes>d__22<Unity.Mathematics.float3,float>
	// Unity.Properties.Property.<GetAttributes>d__22<Unity.Properties.Internal.PropertyWrapper<Http.HttpPackage>,Http.HttpPackage>
	// Unity.Properties.Property.<GetAttributes>d__22<Unity.Properties.Internal.PropertyWrapper<object>,object>
	// Unity.Properties.Property.<GetAttributes>d__22<UnityEngine.Color,float>
	// Unity.Properties.Property.<GetAttributes>d__22<object,Unity.Mathematics.float3>
	// Unity.Properties.Property.<GetAttributes>d__22<object,UnityEngine.Color>
	// Unity.Properties.Property.<GetAttributes>d__22<object,byte>
	// Unity.Properties.Property.<GetAttributes>d__22<object,float>
	// Unity.Properties.Property.<GetAttributes>d__22<object,int>
	// Unity.Properties.Property.<GetAttributes>d__22<object,object>
	// Unity.Properties.Property<Unity.Mathematics.float3,float>
	// Unity.Properties.Property<Unity.Properties.Internal.PropertyWrapper<Http.HttpPackage>,Http.HttpPackage>
	// Unity.Properties.Property<Unity.Properties.Internal.PropertyWrapper<object>,object>
	// Unity.Properties.Property<UnityEngine.Color,float>
	// Unity.Properties.Property<object,Unity.Mathematics.float3>
	// Unity.Properties.Property<object,UnityEngine.Color>
	// Unity.Properties.Property<object,byte>
	// Unity.Properties.Property<object,float>
	// Unity.Properties.Property<object,int>
	// Unity.Properties.Property<object,object>
	// Unity.Properties.PropertyBag<Unity.Mathematics.float3>
	// Unity.Properties.PropertyBag<Unity.Properties.Internal.PropertyWrapper<Http.HttpPackage>>
	// Unity.Properties.PropertyBag<Unity.Properties.Internal.PropertyWrapper<object>>
	// Unity.Properties.PropertyBag<UnityEngine.Color>
	// Unity.Properties.PropertyBag<object>
	// Unity.Serialization.UnsafeBuffer<ushort>
	// Unity.VisualScripting.ConnectionCollectionBase<object,object,object,object>
	// Unity.VisualScripting.EventUnit.<>c__DisplayClass14_0<Unity.VisualScripting.CustomEventArgs>
	// Unity.VisualScripting.EventUnit.Data<Unity.VisualScripting.CustomEventArgs>
	// Unity.VisualScripting.EventUnit<Unity.VisualScripting.CustomEventArgs>
	// Unity.VisualScripting.GraphElement<object>
	// Unity.VisualScripting.IConnection<object,object>
	// Unity.VisualScripting.IConnectionCollection<object,object,object>
	// Unity.VisualScripting.IKeyedCollection<object,object>
	// Unity.VisualScripting.Unit.<>c__DisplayClass86_0<Unity.Entities.EntityManager>
	// Unity.VisualScripting.Unit.<>c__DisplayClass86_0<UnityEngine.Vector2Int>
	// Unity.VisualScripting.Unit.<>c__DisplayClass86_0<byte>
	// Unity.VisualScripting.Unit.<>c__DisplayClass86_0<object>
	// Unity.VisualScripting.UnitConnection<object,object>
	// Unity.VisualScripting.UnitPort.<>c__DisplayClass45_0<object,object,object>
	// Unity.VisualScripting.UnitPort.<>c__DisplayClass46_0<object,object,object>
	// Unity.VisualScripting.UnitPort<object,object,object>
	// plyLib.SingletonMonoBehaviour<object>
	// }}

	public void RefMethods()
	{
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.BetRowData>>(FlatBuffers.Offset<GameConfigs.BetRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.Build_BankRowData>>(FlatBuffers.Offset<GameConfigs.Build_BankRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.Build_ChanceCardRowData>>(FlatBuffers.Offset<GameConfigs.Build_ChanceCardRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.Build_CoinRowData>>(FlatBuffers.Offset<GameConfigs.Build_CoinRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.Build_GameRowData>>(FlatBuffers.Offset<GameConfigs.Build_GameRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.Build_TrapRowData>>(FlatBuffers.Offset<GameConfigs.Build_TrapRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.Build_VisitRowData>>(FlatBuffers.Offset<GameConfigs.Build_VisitRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.Build_houseRowData>>(FlatBuffers.Offset<GameConfigs.Build_houseRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.Design_GlobalRowData>>(FlatBuffers.Offset<GameConfigs.Design_GlobalRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.EquipRowData>>(FlatBuffers.Offset<GameConfigs.EquipRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.Event_BuildRowData>>(FlatBuffers.Offset<GameConfigs.Event_BuildRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.Event_GroupRowData>>(FlatBuffers.Offset<GameConfigs.Event_GroupRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.Event_PartRowData>>(FlatBuffers.Offset<GameConfigs.Event_PartRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.Game_EventRowData>>(FlatBuffers.Offset<GameConfigs.Game_EventRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.Game_FunctionRowData>>(FlatBuffers.Offset<GameConfigs.Game_FunctionRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.GridRowData>>(FlatBuffers.Offset<GameConfigs.GridRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.Grid_PathRowData>>(FlatBuffers.Offset<GameConfigs.Grid_PathRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.ItemRowData>>(FlatBuffers.Offset<GameConfigs.ItemRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.Item_ChanceCardRowData>>(FlatBuffers.Offset<GameConfigs.Item_ChanceCardRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.Language_CHNRowData>>(FlatBuffers.Offset<GameConfigs.Language_CHNRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.Language_ENRowData>>(FlatBuffers.Offset<GameConfigs.Language_ENRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.Language_HIRowData>>(FlatBuffers.Offset<GameConfigs.Language_HIRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.Travel_EventRowData>>(FlatBuffers.Offset<GameConfigs.Travel_EventRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.game_globalRowData>>(FlatBuffers.Offset<GameConfigs.game_globalRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.ui_groupsRowData>>(FlatBuffers.Offset<GameConfigs.ui_groupsRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<FlatBuffers.Offset<GameConfigs.ui_resRowData>>(FlatBuffers.Offset<GameConfigs.ui_resRowData>[])
		// int FlatBuffers.ByteBuffer.ArraySize<byte>(byte[])
		// int FlatBuffers.ByteBuffer.ArraySize<float>(float[])
		// int FlatBuffers.ByteBuffer.ArraySize<int>(int[])
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.BetRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.Build_BankRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.Build_ChanceCardRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.Build_CoinRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.Build_GameRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.Build_TrapRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.Build_VisitRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.Build_houseRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.Design_GlobalRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.EquipRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.Event_BuildRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.Event_GroupRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.Event_PartRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.Game_EventRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.Game_FunctionRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.GridRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.Grid_PathRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.ItemRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.Item_ChanceCardRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.Language_CHNRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.Language_ENRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.Language_HIRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.Travel_EventRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.game_globalRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.ui_groupsRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<FlatBuffers.Offset<GameConfigs.ui_resRowData>>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<float>()
		// bool FlatBuffers.ByteBuffer.IsSupportedType<int>()
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.BetRowData>>(int,FlatBuffers.Offset<GameConfigs.BetRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.Build_BankRowData>>(int,FlatBuffers.Offset<GameConfigs.Build_BankRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.Build_ChanceCardRowData>>(int,FlatBuffers.Offset<GameConfigs.Build_ChanceCardRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.Build_CoinRowData>>(int,FlatBuffers.Offset<GameConfigs.Build_CoinRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.Build_GameRowData>>(int,FlatBuffers.Offset<GameConfigs.Build_GameRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.Build_TrapRowData>>(int,FlatBuffers.Offset<GameConfigs.Build_TrapRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.Build_VisitRowData>>(int,FlatBuffers.Offset<GameConfigs.Build_VisitRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.Build_houseRowData>>(int,FlatBuffers.Offset<GameConfigs.Build_houseRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.Design_GlobalRowData>>(int,FlatBuffers.Offset<GameConfigs.Design_GlobalRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.EquipRowData>>(int,FlatBuffers.Offset<GameConfigs.EquipRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.Event_BuildRowData>>(int,FlatBuffers.Offset<GameConfigs.Event_BuildRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.Event_GroupRowData>>(int,FlatBuffers.Offset<GameConfigs.Event_GroupRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.Event_PartRowData>>(int,FlatBuffers.Offset<GameConfigs.Event_PartRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.Game_EventRowData>>(int,FlatBuffers.Offset<GameConfigs.Game_EventRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.Game_FunctionRowData>>(int,FlatBuffers.Offset<GameConfigs.Game_FunctionRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.GridRowData>>(int,FlatBuffers.Offset<GameConfigs.GridRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.Grid_PathRowData>>(int,FlatBuffers.Offset<GameConfigs.Grid_PathRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.ItemRowData>>(int,FlatBuffers.Offset<GameConfigs.ItemRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.Item_ChanceCardRowData>>(int,FlatBuffers.Offset<GameConfigs.Item_ChanceCardRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.Language_CHNRowData>>(int,FlatBuffers.Offset<GameConfigs.Language_CHNRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.Language_ENRowData>>(int,FlatBuffers.Offset<GameConfigs.Language_ENRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.Language_HIRowData>>(int,FlatBuffers.Offset<GameConfigs.Language_HIRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.Travel_EventRowData>>(int,FlatBuffers.Offset<GameConfigs.Travel_EventRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.game_globalRowData>>(int,FlatBuffers.Offset<GameConfigs.game_globalRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.ui_groupsRowData>>(int,FlatBuffers.Offset<GameConfigs.ui_groupsRowData>[])
		// int FlatBuffers.ByteBuffer.Put<FlatBuffers.Offset<GameConfigs.ui_resRowData>>(int,FlatBuffers.Offset<GameConfigs.ui_resRowData>[])
		// int FlatBuffers.ByteBuffer.Put<float>(int,float[])
		// int FlatBuffers.ByteBuffer.Put<int>(int,int[])
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.BetRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.Build_BankRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.Build_ChanceCardRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.Build_CoinRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.Build_GameRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.Build_TrapRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.Build_VisitRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.Build_houseRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.Design_GlobalRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.EquipRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.Event_BuildRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.Event_GroupRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.Event_PartRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.Game_EventRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.Game_FunctionRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.GridRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.Grid_PathRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.ItemRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.Item_ChanceCardRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.Language_CHNRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.Language_ENRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.Language_HIRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.Travel_EventRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.game_globalRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.ui_groupsRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<FlatBuffers.Offset<GameConfigs.ui_resRowData>>()
		// int FlatBuffers.ByteBuffer.SizeOf<byte>()
		// int FlatBuffers.ByteBuffer.SizeOf<float>()
		// int FlatBuffers.ByteBuffer.SizeOf<int>()
		// byte[] FlatBuffers.ByteBuffer.ToArray<byte>(int,int)
		// float[] FlatBuffers.ByteBuffer.ToArray<float>(int,int)
		// int[] FlatBuffers.ByteBuffer.ToArray<int>(int,int)
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.BetRowData>>(FlatBuffers.Offset<GameConfigs.BetRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.Build_BankRowData>>(FlatBuffers.Offset<GameConfigs.Build_BankRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.Build_ChanceCardRowData>>(FlatBuffers.Offset<GameConfigs.Build_ChanceCardRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.Build_CoinRowData>>(FlatBuffers.Offset<GameConfigs.Build_CoinRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.Build_GameRowData>>(FlatBuffers.Offset<GameConfigs.Build_GameRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.Build_TrapRowData>>(FlatBuffers.Offset<GameConfigs.Build_TrapRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.Build_VisitRowData>>(FlatBuffers.Offset<GameConfigs.Build_VisitRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.Build_houseRowData>>(FlatBuffers.Offset<GameConfigs.Build_houseRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.Design_GlobalRowData>>(FlatBuffers.Offset<GameConfigs.Design_GlobalRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.EquipRowData>>(FlatBuffers.Offset<GameConfigs.EquipRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.Event_BuildRowData>>(FlatBuffers.Offset<GameConfigs.Event_BuildRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.Event_GroupRowData>>(FlatBuffers.Offset<GameConfigs.Event_GroupRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.Event_PartRowData>>(FlatBuffers.Offset<GameConfigs.Event_PartRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.Game_EventRowData>>(FlatBuffers.Offset<GameConfigs.Game_EventRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.Game_FunctionRowData>>(FlatBuffers.Offset<GameConfigs.Game_FunctionRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.GridRowData>>(FlatBuffers.Offset<GameConfigs.GridRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.Grid_PathRowData>>(FlatBuffers.Offset<GameConfigs.Grid_PathRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.ItemRowData>>(FlatBuffers.Offset<GameConfigs.ItemRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.Item_ChanceCardRowData>>(FlatBuffers.Offset<GameConfigs.Item_ChanceCardRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.Language_CHNRowData>>(FlatBuffers.Offset<GameConfigs.Language_CHNRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.Language_ENRowData>>(FlatBuffers.Offset<GameConfigs.Language_ENRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.Language_HIRowData>>(FlatBuffers.Offset<GameConfigs.Language_HIRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.Travel_EventRowData>>(FlatBuffers.Offset<GameConfigs.Travel_EventRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.game_globalRowData>>(FlatBuffers.Offset<GameConfigs.game_globalRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.ui_groupsRowData>>(FlatBuffers.Offset<GameConfigs.ui_groupsRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<FlatBuffers.Offset<GameConfigs.ui_resRowData>>(FlatBuffers.Offset<GameConfigs.ui_resRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<float>(float[])
		// System.Void FlatBuffers.FlatBufferBuilder.Add<int>(int[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.BetRowData>>(FlatBuffers.Offset<GameConfigs.BetRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.Build_BankRowData>>(FlatBuffers.Offset<GameConfigs.Build_BankRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.Build_ChanceCardRowData>>(FlatBuffers.Offset<GameConfigs.Build_ChanceCardRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.Build_CoinRowData>>(FlatBuffers.Offset<GameConfigs.Build_CoinRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.Build_GameRowData>>(FlatBuffers.Offset<GameConfigs.Build_GameRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.Build_TrapRowData>>(FlatBuffers.Offset<GameConfigs.Build_TrapRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.Build_VisitRowData>>(FlatBuffers.Offset<GameConfigs.Build_VisitRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.Build_houseRowData>>(FlatBuffers.Offset<GameConfigs.Build_houseRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.Design_GlobalRowData>>(FlatBuffers.Offset<GameConfigs.Design_GlobalRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.EquipRowData>>(FlatBuffers.Offset<GameConfigs.EquipRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.Event_BuildRowData>>(FlatBuffers.Offset<GameConfigs.Event_BuildRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.Event_GroupRowData>>(FlatBuffers.Offset<GameConfigs.Event_GroupRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.Event_PartRowData>>(FlatBuffers.Offset<GameConfigs.Event_PartRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.Game_EventRowData>>(FlatBuffers.Offset<GameConfigs.Game_EventRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.Game_FunctionRowData>>(FlatBuffers.Offset<GameConfigs.Game_FunctionRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.GridRowData>>(FlatBuffers.Offset<GameConfigs.GridRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.Grid_PathRowData>>(FlatBuffers.Offset<GameConfigs.Grid_PathRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.ItemRowData>>(FlatBuffers.Offset<GameConfigs.ItemRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.Item_ChanceCardRowData>>(FlatBuffers.Offset<GameConfigs.Item_ChanceCardRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.Language_CHNRowData>>(FlatBuffers.Offset<GameConfigs.Language_CHNRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.Language_ENRowData>>(FlatBuffers.Offset<GameConfigs.Language_ENRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.Language_HIRowData>>(FlatBuffers.Offset<GameConfigs.Language_HIRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.Travel_EventRowData>>(FlatBuffers.Offset<GameConfigs.Travel_EventRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.game_globalRowData>>(FlatBuffers.Offset<GameConfigs.game_globalRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.ui_groupsRowData>>(FlatBuffers.Offset<GameConfigs.ui_groupsRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<FlatBuffers.Offset<GameConfigs.ui_resRowData>>(FlatBuffers.Offset<GameConfigs.ui_resRowData>[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<float>(float[])
		// System.Void FlatBuffers.FlatBufferBuilder.Put<int>(int[])
		// byte[] FlatBuffers.Table.__vector_as_array<byte>(int)
		// float[] FlatBuffers.Table.__vector_as_array<float>(int)
		// int[] FlatBuffers.Table.__vector_as_array<int>(int)
		// bool SGame.EventDispatcher.addEventListener<GamePackage>(int,Callback<GamePackage>)
		// bool SGame.EventDispatcher.addEventListener<int,float,object>(int,Callback<int,float,object>)
		// bool SGame.EventDispatcher.addEventListener<int,long,int>(int,Callback<int,long,int>)
		// bool SGame.EventDispatcher.addEventListener<int,object>(int,Callback<int,object>)
		// bool SGame.EventDispatcher.addEventListener<object,object>(int,Callback<object,object>)
		// System.Void SGame.EventDispatcher.dispatchEvent<GamePackage>(int,GamePackage)
		// System.Void SGame.EventDispatcher.dispatchEvent<int,object,float>(int,int,object,float)
		// System.Void SGame.EventDispatcher.dispatchEvent<int,object>(int,int,object)
		// System.Void SGame.EventDispatcher.dispatchEvent<object>(int,object)
		// SGame.EventHanle SGame.EventDispatcherEx.Reg<GamePackage>(int,Callback<GamePackage>)
		// SGame.EventHanle SGame.EventDispatcherEx.Reg<int,float,object>(int,Callback<int,float,object>)
		// SGame.EventHanle SGame.EventDispatcherEx.Reg<int,long,int>(int,Callback<int,long,int>)
		// SGame.EventHanle SGame.EventDispatcherEx.Reg<int,object>(int,Callback<int,object>)
		// SGame.EventHanle SGame.EventDispatcherEx.Reg<object,object>(int,Callback<object,object>)
		// System.Void SGame.EventDispatcherEx.Trigger<GamePackage>(int,GamePackage)
		// System.Void SGame.EventDispatcherEx.Trigger<int,object,float>(int,int,object,float)
		// System.Void SGame.EventDispatcherEx.Trigger<int,object>(int,int,object)
		// System.Void SGame.EventDispatcherEx.Trigger<object>(int,object)
		// SGame.EventHanle SGame.EventManager.Reg<int,float,object>(int,Callback<int,float,object>)
		// SGame.EventHanle SGame.EventManager.Reg<int,long,int>(int,Callback<int,long,int>)
		// SGame.EventHanle SGame.EventManager.Reg<object,object>(int,Callback<object,object>)
		// System.Void SGame.EventManager.Trigger<int,object,float>(int,int,object,float)
		// System.Void SGame.EventManager.Trigger<object>(int,object)
		// object System.Activator.CreateInstance<object>()
		// byte[] System.Array.Empty<byte>()
		// object[] System.Array.Empty<object>()
		// System.Void System.Array.Fill<int>(int[],int)
		// System.Void System.Array.ForEach<object>(object[],System.Action<object>)
		// bool System.Linq.Enumerable.All<object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,bool>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Select<int,object>(System.Collections.Generic.IEnumerable<int>,System.Func<int,object>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Select<object,object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,object>)
		// object[] System.Linq.Enumerable.ToArray<object>(System.Collections.Generic.IEnumerable<object>)
		// System.Collections.Generic.Dictionary<object,object> System.Linq.Enumerable.ToDictionary<object,object,object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,object>,System.Func<object,object>)
		// System.Collections.Generic.Dictionary<object,object> System.Linq.Enumerable.ToDictionary<object,object,object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,object>,System.Func<object,object>,System.Collections.Generic.IEqualityComparer<object>)
		// System.Collections.Generic.List<object> System.Linq.Enumerable.ToList<object>(System.Collections.Generic.IEnumerable<object>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Where<object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,bool>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Iterator<int>.Select<object>(System.Func<int,object>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Iterator<object>.Select<object>(System.Func<object,object>)
		// object& System.Runtime.CompilerServices.Unsafe.As<object,object>(object&)
		// System.Void* System.Runtime.CompilerServices.Unsafe.AsPointer<object>(object&)
		// object System.Threading.Interlocked.CompareExchange<object>(object&,object,object)
		// Unity.Collections.NativeArray<int> Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(System.Void*,int,Unity.Collections.Allocator)
		// SGame.EntitySyncGameObject& Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<SGame.EntitySyncGameObject>(System.Void*)
		// SGame.FloatTextData& Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<SGame.FloatTextData>(System.Void*)
		// SGame.UserInput& Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<SGame.UserInput>(System.Void*)
		// Unity.Transforms.LocalToWorld& Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<Unity.Transforms.LocalToWorld>(System.Void*)
		// Unity.Transforms.Translation& Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<Unity.Transforms.Translation>(System.Void*)
		// object& Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<object>(System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyPtrToStructure<SGame.UserData>(System.Void*,SGame.UserData&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyPtrToStructure<SGame.UserInput>(System.Void*,SGame.UserInput&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyPtrToStructure<SGame.UserSetting>(System.Void*,SGame.UserSetting&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyPtrToStructure<object>(System.Void*,object&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.Account>(SGame.Account&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.FloatTextData>(SGame.FloatTextData&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.LiveTime>(SGame.LiveTime&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.MoveDirection>(SGame.MoveDirection&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.UserData>(SGame.UserData&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<SGame.UserSetting>(SGame.UserSetting&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<Unity.Transforms.Rotation>(Unity.Transforms.Rotation&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<Unity.Transforms.Translation>(Unity.Transforms.Translation&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr<object>(object&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyPtrToStructure<SGame.UserData>(System.Void*,SGame.UserData&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyPtrToStructure<SGame.UserInput>(System.Void*,SGame.UserInput&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyPtrToStructure<SGame.UserSetting>(System.Void*,SGame.UserSetting&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyPtrToStructure<object>(System.Void*,object&)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.Account>(SGame.Account&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.FloatTextData>(SGame.FloatTextData&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.LiveTime>(SGame.LiveTime&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.MoveDirection>(SGame.MoveDirection&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.UserData>(SGame.UserData&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<SGame.UserSetting>(SGame.UserSetting&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<Unity.Transforms.Rotation>(Unity.Transforms.Rotation&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<Unity.Transforms.Translation>(Unity.Transforms.Translation&,System.Void*)
		// System.Void Unity.Collections.LowLevel.Unsafe.UnsafeUtility.InternalCopyStructureToPtr<object>(object&,System.Void*)
		// int Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<SGame.FloatTextData>()
		// int Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<SGame.LiveTime>()
		// int Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<SGame.MoveDirection>()
		// int Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<Unity.Transforms.Rotation>()
		// int Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<Unity.Transforms.Translation>()
		// System.Void* Unity.Entities.ArchetypeChunk.GetComponentDataPtrRO<SGame.EntitySyncGameObject>(Unity.Entities.ComponentTypeHandle<SGame.EntitySyncGameObject>&)
		// System.Void* Unity.Entities.ArchetypeChunk.GetComponentDataPtrRO<SGame.FloatTextData>(Unity.Entities.ComponentTypeHandle<SGame.FloatTextData>&)
		// System.Void* Unity.Entities.ArchetypeChunk.GetComponentDataPtrRO<Unity.Transforms.LocalToWorld>(Unity.Entities.ComponentTypeHandle<Unity.Transforms.LocalToWorld>&)
		// System.Void* Unity.Entities.ArchetypeChunk.GetComponentDataPtrRO<Unity.Transforms.Translation>(Unity.Entities.ComponentTypeHandle<Unity.Transforms.Translation>&)
		// System.Void* Unity.Entities.ArchetypeChunk.GetComponentDataPtrRW<SGame.UserInput>(Unity.Entities.ComponentTypeHandle<SGame.UserInput>&)
		// Unity.Entities.ManagedComponentAccessor<object> Unity.Entities.ArchetypeChunk.GetManagedComponentAccessor<object>(Unity.Entities.ComponentTypeHandle<object>,Unity.Entities.EntityManager)
		// Unity.Entities.ComponentTypeHandle<SGame.EntitySyncGameObject> Unity.Entities.ComponentSystemBase.GetComponentTypeHandle<SGame.EntitySyncGameObject>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.FloatTextData> Unity.Entities.ComponentSystemBase.GetComponentTypeHandle<SGame.FloatTextData>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.UserInput> Unity.Entities.ComponentSystemBase.GetComponentTypeHandle<SGame.UserInput>(bool)
		// Unity.Entities.ComponentTypeHandle<Unity.Transforms.LocalToWorld> Unity.Entities.ComponentSystemBase.GetComponentTypeHandle<Unity.Transforms.LocalToWorld>(bool)
		// Unity.Entities.ComponentTypeHandle<Unity.Transforms.Translation> Unity.Entities.ComponentSystemBase.GetComponentTypeHandle<Unity.Transforms.Translation>(bool)
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<DespawningEntity>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.EntitySyncGameObject>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.FloatTextData>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<SGame.UserInput>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<Unity.Transforms.LocalToWorld>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadOnly<Unity.Transforms.Translation>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<DespawningEntity>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.Account>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.EntitySyncGameObject>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.FloatTextData>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.LiveTime>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.MoveDirection>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.UI.UIInitalized>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<SGame.UserInput>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<Unity.Entities.Disabled>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<Unity.Transforms.LocalToWorld>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<Unity.Transforms.Rotation>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<Unity.Transforms.Translation>()
		// Unity.Entities.ComponentType Unity.Entities.ComponentType.ReadWrite<object>()
		// System.Void Unity.Entities.EntityCommandBuffer.RemoveComponent<object>(Unity.Entities.Entity)
		// System.Void Unity.Entities.EntityCommandBuffer.SetComponent<SGame.FloatTextData>(Unity.Entities.Entity,SGame.FloatTextData)
		// System.Void Unity.Entities.EntityCommandBuffer.SetComponent<SGame.LiveTime>(Unity.Entities.Entity,SGame.LiveTime)
		// System.Void Unity.Entities.EntityCommandBuffer.SetComponent<SGame.MoveDirection>(Unity.Entities.Entity,SGame.MoveDirection)
		// System.Void Unity.Entities.EntityCommandBuffer.SetComponent<Unity.Transforms.Rotation>(Unity.Entities.Entity,Unity.Transforms.Rotation)
		// System.Void Unity.Entities.EntityCommandBuffer.SetComponent<Unity.Transforms.Translation>(Unity.Entities.Entity,Unity.Transforms.Translation)
		// System.Void Unity.Entities.EntityCommandBufferData.AddEntityComponentCommand<SGame.FloatTextData>(Unity.Entities.EntityCommandBufferChain*,int,Unity.Entities.ECBCommand,Unity.Entities.Entity,SGame.FloatTextData)
		// System.Void Unity.Entities.EntityCommandBufferData.AddEntityComponentCommand<SGame.LiveTime>(Unity.Entities.EntityCommandBufferChain*,int,Unity.Entities.ECBCommand,Unity.Entities.Entity,SGame.LiveTime)
		// System.Void Unity.Entities.EntityCommandBufferData.AddEntityComponentCommand<SGame.MoveDirection>(Unity.Entities.EntityCommandBufferChain*,int,Unity.Entities.ECBCommand,Unity.Entities.Entity,SGame.MoveDirection)
		// System.Void Unity.Entities.EntityCommandBufferData.AddEntityComponentCommand<Unity.Transforms.Rotation>(Unity.Entities.EntityCommandBufferChain*,int,Unity.Entities.ECBCommand,Unity.Entities.Entity,Unity.Transforms.Rotation)
		// System.Void Unity.Entities.EntityCommandBufferData.AddEntityComponentCommand<Unity.Transforms.Translation>(Unity.Entities.EntityCommandBufferChain*,int,Unity.Entities.ECBCommand,Unity.Entities.Entity,Unity.Transforms.Translation)
		// System.Void Unity.Entities.EntityCommandBufferManagedComponentExtensions.AddComponent<object>(Unity.Entities.EntityCommandBuffer,Unity.Entities.Entity,object)
		// System.Void Unity.Entities.EntityCommandBufferManagedComponentExtensions.AddEntityComponentCommandFromMainThread<object>(Unity.Entities.EntityCommandBufferData*,int,Unity.Entities.ECBCommand,Unity.Entities.Entity,object)
		// SGame.UserData Unity.Entities.EntityDataAccess.GetComponentData<SGame.UserData>(Unity.Entities.Entity)
		// SGame.UserInput Unity.Entities.EntityDataAccess.GetComponentData<SGame.UserInput>(Unity.Entities.Entity)
		// SGame.UserSetting Unity.Entities.EntityDataAccess.GetComponentData<SGame.UserSetting>(Unity.Entities.Entity)
		// object Unity.Entities.EntityDataAccess.GetComponentData<object>(Unity.Entities.Entity)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<SGame.Account>(Unity.Entities.Entity,SGame.Account,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<SGame.UserData>(Unity.Entities.Entity,SGame.UserData,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<SGame.UserSetting>(Unity.Entities.Entity,SGame.UserSetting,Unity.Entities.SystemHandleUntyped&)
		// System.Void Unity.Entities.EntityDataAccess.SetComponentData<object>(Unity.Entities.Entity,object,Unity.Entities.SystemHandleUntyped&)
		// object Unity.Entities.EntityDataAccessManagedComponentExtensions.GetComponentObject<object>(Unity.Entities.EntityDataAccess&,Unity.Entities.Entity,Unity.Entities.ComponentType)
		// bool Unity.Entities.EntityManager.AddComponent<Unity.Entities.Disabled>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.AddComponentData<SGame.Account>(Unity.Entities.Entity,SGame.Account)
		// bool Unity.Entities.EntityManager.AddComponentData<object>(Unity.Entities.Entity,object)
		// SGame.UserData Unity.Entities.EntityManager.GetComponentData<SGame.UserData>(Unity.Entities.Entity)
		// SGame.UserInput Unity.Entities.EntityManager.GetComponentData<SGame.UserInput>(Unity.Entities.Entity)
		// SGame.UserSetting Unity.Entities.EntityManager.GetComponentData<SGame.UserSetting>(Unity.Entities.Entity)
		// object Unity.Entities.EntityManager.GetComponentData<object>(Unity.Entities.Entity)
		// object Unity.Entities.EntityManager.GetComponentObject<object>(Unity.Entities.Entity)
		// Unity.Entities.ComponentTypeHandle<SGame.EntitySyncGameObject> Unity.Entities.EntityManager.GetComponentTypeHandle<SGame.EntitySyncGameObject>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.FloatTextData> Unity.Entities.EntityManager.GetComponentTypeHandle<SGame.FloatTextData>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.UserInput> Unity.Entities.EntityManager.GetComponentTypeHandle<SGame.UserInput>(bool)
		// Unity.Entities.ComponentTypeHandle<Unity.Transforms.LocalToWorld> Unity.Entities.EntityManager.GetComponentTypeHandle<Unity.Transforms.LocalToWorld>(bool)
		// Unity.Entities.ComponentTypeHandle<Unity.Transforms.Translation> Unity.Entities.EntityManager.GetComponentTypeHandle<Unity.Transforms.Translation>(bool)
		// Unity.Entities.ComponentTypeHandle<object> Unity.Entities.EntityManager.GetComponentTypeHandle<object>(bool)
		// bool Unity.Entities.EntityManager.HasComponent<DespawningEntity>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<SGame.Account>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<SGame.UI.UIInitalized>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<Unity.Entities.Disabled>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.HasComponent<object>(Unity.Entities.Entity)
		// bool Unity.Entities.EntityManager.RemoveComponent<Unity.Entities.Disabled>(Unity.Entities.Entity)
		// System.Void Unity.Entities.EntityManager.SetComponentData<SGame.Account>(Unity.Entities.Entity,SGame.Account)
		// System.Void Unity.Entities.EntityManager.SetComponentData<SGame.UserData>(Unity.Entities.Entity,SGame.UserData)
		// System.Void Unity.Entities.EntityManager.SetComponentData<SGame.UserSetting>(Unity.Entities.Entity,SGame.UserSetting)
		// System.Void Unity.Entities.EntityManager.SetComponentData<object>(Unity.Entities.Entity,object)
		// System.Void Unity.Entities.EntityManagerManagedComponentExtensions.AddComponentData<object>(Unity.Entities.EntityManager,Unity.Entities.Entity,object)
		// System.Void Unity.Entities.EntityManagerManagedComponentExtensions.SetComponentData<object>(Unity.Entities.EntityManager,Unity.Entities.Entity,object)
		// object Unity.Entities.EntityQuery.GetSingleton<object>()
		// object Unity.Entities.EntityQueryImpl.GetSingleton<object>()
		// object Unity.Entities.EntityQueryManagedComponentExtensions.GetSingleton<object>(Unity.Entities.EntityQuery)
		// System.IntPtr Unity.Entities.InternalCompilerInterface.UnsafeGetChunkNativeArrayIntPtr<SGame.UserInput>(Unity.Entities.ArchetypeChunk,Unity.Entities.ComponentTypeHandle<SGame.UserInput>)
		// System.IntPtr Unity.Entities.InternalCompilerInterface.UnsafeGetChunkNativeArrayReadOnlyIntPtr<SGame.EntitySyncGameObject>(Unity.Entities.ArchetypeChunk,Unity.Entities.ComponentTypeHandle<SGame.EntitySyncGameObject>)
		// System.IntPtr Unity.Entities.InternalCompilerInterface.UnsafeGetChunkNativeArrayReadOnlyIntPtr<SGame.FloatTextData>(Unity.Entities.ArchetypeChunk,Unity.Entities.ComponentTypeHandle<SGame.FloatTextData>)
		// System.IntPtr Unity.Entities.InternalCompilerInterface.UnsafeGetChunkNativeArrayReadOnlyIntPtr<Unity.Transforms.LocalToWorld>(Unity.Entities.ArchetypeChunk,Unity.Entities.ComponentTypeHandle<Unity.Transforms.LocalToWorld>)
		// System.IntPtr Unity.Entities.InternalCompilerInterface.UnsafeGetChunkNativeArrayReadOnlyIntPtr<Unity.Transforms.Translation>(Unity.Entities.ArchetypeChunk,Unity.Entities.ComponentTypeHandle<Unity.Transforms.Translation>)
		// Unity.Entities.Entity Unity.Entities.InternalCompilerInterface.UnsafeGetCopyOfNativeArrayPtrElement<Unity.Entities.Entity>(System.IntPtr,int)
		// SGame.EntitySyncGameObject& Unity.Entities.InternalCompilerInterface.UnsafeGetRefToNativeArrayPtrElement<SGame.EntitySyncGameObject>(System.IntPtr,int)
		// SGame.FloatTextData& Unity.Entities.InternalCompilerInterface.UnsafeGetRefToNativeArrayPtrElement<SGame.FloatTextData>(System.IntPtr,int)
		// SGame.UserInput& Unity.Entities.InternalCompilerInterface.UnsafeGetRefToNativeArrayPtrElement<SGame.UserInput>(System.IntPtr,int)
		// Unity.Transforms.LocalToWorld& Unity.Entities.InternalCompilerInterface.UnsafeGetRefToNativeArrayPtrElement<Unity.Transforms.LocalToWorld>(System.IntPtr,int)
		// Unity.Transforms.Translation& Unity.Entities.InternalCompilerInterface.UnsafeGetRefToNativeArrayPtrElement<Unity.Transforms.Translation>(System.IntPtr,int)
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.DespawnFloatTextSystem.DespawnFloatTextSystem_LambdaJob_0_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.EntitySyncGameObjectSystem.EntitySyncGameObjectSystem_LambdaJob_0_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.FloatTextSystem.FloatTextSystem_LambdaJob_0_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.Http.HttpSystem.HttpSystem_LambdaJob_0_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.Http.HttpSystemSpawn.HttpSystemSpawn_LambdaJob_0_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.SpawnFloatTextSystem.SpawnFloatTextSystem_LambdaJob_0_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.EarlyJobInit<SGame.UserInputsystem.UserInputsystem_LambdaJob_0_Job>()
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobs<SGame.DespawnFloatTextSystem.DespawnFloatTextSystem_LambdaJob_0_Job>(SGame.DespawnFloatTextSystem.DespawnFloatTextSystem_LambdaJob_0_Job&,Unity.Entities.EntityQuery)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobs<SGame.EntitySyncGameObjectSystem.EntitySyncGameObjectSystem_LambdaJob_0_Job>(SGame.EntitySyncGameObjectSystem.EntitySyncGameObjectSystem_LambdaJob_0_Job&,Unity.Entities.EntityQuery)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobs<SGame.FloatTextSystem.FloatTextSystem_LambdaJob_0_Job>(SGame.FloatTextSystem.FloatTextSystem_LambdaJob_0_Job&,Unity.Entities.EntityQuery)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobs<SGame.Http.HttpSystem.HttpSystem_LambdaJob_0_Job>(SGame.Http.HttpSystem.HttpSystem_LambdaJob_0_Job&,Unity.Entities.EntityQuery)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobs<SGame.Http.HttpSystemSpawn.HttpSystemSpawn_LambdaJob_0_Job>(SGame.Http.HttpSystemSpawn.HttpSystemSpawn_LambdaJob_0_Job&,Unity.Entities.EntityQuery)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobs<SGame.SpawnFloatTextSystem.SpawnFloatTextSystem_LambdaJob_0_Job>(SGame.SpawnFloatTextSystem.SpawnFloatTextSystem_LambdaJob_0_Job&,Unity.Entities.EntityQuery)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobs<SGame.UserInputsystem.UserInputsystem_LambdaJob_0_Job>(SGame.UserInputsystem.UserInputsystem_LambdaJob_0_Job&,Unity.Entities.EntityQuery)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.DespawnFloatTextSystem.DespawnFloatTextSystem_LambdaJob_0_Job>(SGame.DespawnFloatTextSystem.DespawnFloatTextSystem_LambdaJob_0_Job&,Unity.Entities.ArchetypeChunkIterator&)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.DespawnFloatTextSystem.DespawnFloatTextSystem_LambdaJob_0_Job>(SGame.DespawnFloatTextSystem.DespawnFloatTextSystem_LambdaJob_0_Job&,Unity.Entities.Chunk**,int,Unity.Entities.EntityComponentStore*)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.EntitySyncGameObjectSystem.EntitySyncGameObjectSystem_LambdaJob_0_Job>(SGame.EntitySyncGameObjectSystem.EntitySyncGameObjectSystem_LambdaJob_0_Job&,Unity.Entities.ArchetypeChunkIterator&)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.EntitySyncGameObjectSystem.EntitySyncGameObjectSystem_LambdaJob_0_Job>(SGame.EntitySyncGameObjectSystem.EntitySyncGameObjectSystem_LambdaJob_0_Job&,Unity.Entities.Chunk**,int,Unity.Entities.EntityComponentStore*)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.FloatTextSystem.FloatTextSystem_LambdaJob_0_Job>(SGame.FloatTextSystem.FloatTextSystem_LambdaJob_0_Job&,Unity.Entities.ArchetypeChunkIterator&)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.FloatTextSystem.FloatTextSystem_LambdaJob_0_Job>(SGame.FloatTextSystem.FloatTextSystem_LambdaJob_0_Job&,Unity.Entities.Chunk**,int,Unity.Entities.EntityComponentStore*)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.Http.HttpSystem.HttpSystem_LambdaJob_0_Job>(SGame.Http.HttpSystem.HttpSystem_LambdaJob_0_Job&,Unity.Entities.ArchetypeChunkIterator&)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.Http.HttpSystem.HttpSystem_LambdaJob_0_Job>(SGame.Http.HttpSystem.HttpSystem_LambdaJob_0_Job&,Unity.Entities.Chunk**,int,Unity.Entities.EntityComponentStore*)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.Http.HttpSystemSpawn.HttpSystemSpawn_LambdaJob_0_Job>(SGame.Http.HttpSystemSpawn.HttpSystemSpawn_LambdaJob_0_Job&,Unity.Entities.ArchetypeChunkIterator&)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.Http.HttpSystemSpawn.HttpSystemSpawn_LambdaJob_0_Job>(SGame.Http.HttpSystemSpawn.HttpSystemSpawn_LambdaJob_0_Job&,Unity.Entities.Chunk**,int,Unity.Entities.EntityComponentStore*)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.SpawnFloatTextSystem.SpawnFloatTextSystem_LambdaJob_0_Job>(SGame.SpawnFloatTextSystem.SpawnFloatTextSystem_LambdaJob_0_Job&,Unity.Entities.ArchetypeChunkIterator&)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.SpawnFloatTextSystem.SpawnFloatTextSystem_LambdaJob_0_Job>(SGame.SpawnFloatTextSystem.SpawnFloatTextSystem_LambdaJob_0_Job&,Unity.Entities.Chunk**,int,Unity.Entities.EntityComponentStore*)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.UserInputsystem.UserInputsystem_LambdaJob_0_Job>(SGame.UserInputsystem.UserInputsystem_LambdaJob_0_Job&,Unity.Entities.ArchetypeChunkIterator&)
		// System.Void Unity.Entities.JobEntityBatchExtensions.RunWithoutJobsInternal<SGame.UserInputsystem.UserInputsystem_LambdaJob_0_Job>(SGame.UserInputsystem.UserInputsystem_LambdaJob_0_Job&,Unity.Entities.Chunk**,int,Unity.Entities.EntityComponentStore*)
		// Unity.Entities.ComponentTypeHandle<SGame.EntitySyncGameObject> Unity.Entities.SystemState.GetComponentTypeHandle<SGame.EntitySyncGameObject>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.FloatTextData> Unity.Entities.SystemState.GetComponentTypeHandle<SGame.FloatTextData>(bool)
		// Unity.Entities.ComponentTypeHandle<SGame.UserInput> Unity.Entities.SystemState.GetComponentTypeHandle<SGame.UserInput>(bool)
		// Unity.Entities.ComponentTypeHandle<Unity.Transforms.LocalToWorld> Unity.Entities.SystemState.GetComponentTypeHandle<Unity.Transforms.LocalToWorld>(bool)
		// Unity.Entities.ComponentTypeHandle<Unity.Transforms.Translation> Unity.Entities.SystemState.GetComponentTypeHandle<Unity.Transforms.Translation>(bool)
		// int Unity.Entities.TypeManager.GetTypeIndex<DespawningEntity>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.Account>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.EntitySyncGameObject>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.FloatTextData>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.LiveTime>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.MoveDirection>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.UI.UIInitalized>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.UserData>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.UserInput>()
		// int Unity.Entities.TypeManager.GetTypeIndex<SGame.UserSetting>()
		// int Unity.Entities.TypeManager.GetTypeIndex<Unity.Entities.Disabled>()
		// int Unity.Entities.TypeManager.GetTypeIndex<Unity.Transforms.LocalToWorld>()
		// int Unity.Entities.TypeManager.GetTypeIndex<Unity.Transforms.Rotation>()
		// int Unity.Entities.TypeManager.GetTypeIndex<Unity.Transforms.Translation>()
		// int Unity.Entities.TypeManager.GetTypeIndex<object>()
		// System.Void Unity.Entities.TypeManager.ManagedException<DespawningEntity>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.Account>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.EntitySyncGameObject>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.FloatTextData>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.LiveTime>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.MoveDirection>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.UI.UIInitalized>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.UserData>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.UserInput>()
		// System.Void Unity.Entities.TypeManager.ManagedException<SGame.UserSetting>()
		// System.Void Unity.Entities.TypeManager.ManagedException<Unity.Entities.Disabled>()
		// System.Void Unity.Entities.TypeManager.ManagedException<Unity.Transforms.LocalToWorld>()
		// System.Void Unity.Entities.TypeManager.ManagedException<Unity.Transforms.Rotation>()
		// System.Void Unity.Entities.TypeManager.ManagedException<Unity.Transforms.Translation>()
		// System.Void Unity.Entities.TypeManager.ManagedException<object>()
		// object Unity.Entities.World.GetOrCreateSystem<object>()
		// System.Void Unity.Properties.ContainerPropertyBag<Unity.Mathematics.float3>.AddProperty<float>(Unity.Properties.Property<Unity.Mathematics.float3,float>)
		// System.Void Unity.Properties.ContainerPropertyBag<UnityEngine.Color>.AddProperty<float>(Unity.Properties.Property<UnityEngine.Color,float>)
		// System.Void Unity.Properties.ContainerPropertyBag<object>.AddProperty<Unity.Mathematics.float3>(Unity.Properties.Property<object,Unity.Mathematics.float3>)
		// System.Void Unity.Properties.ContainerPropertyBag<object>.AddProperty<UnityEngine.Color>(Unity.Properties.Property<object,UnityEngine.Color>)
		// System.Void Unity.Properties.ContainerPropertyBag<object>.AddProperty<byte>(Unity.Properties.Property<object,byte>)
		// System.Void Unity.Properties.ContainerPropertyBag<object>.AddProperty<float>(Unity.Properties.Property<object,float>)
		// System.Void Unity.Properties.ContainerPropertyBag<object>.AddProperty<int>(Unity.Properties.Property<object,int>)
		// System.Void Unity.Properties.ContainerPropertyBag<object>.AddProperty<object>(Unity.Properties.Property<object,object>)
		// System.Void Unity.Properties.Internal.PropertyBagStore.AddPropertyBag<Unity.Mathematics.float3>(Unity.Properties.Internal.IPropertyBag<Unity.Mathematics.float3>)
		// System.Void Unity.Properties.Internal.PropertyBagStore.AddPropertyBag<UnityEngine.Color>(Unity.Properties.Internal.IPropertyBag<UnityEngine.Color>)
		// System.Void Unity.Properties.Internal.PropertyBagStore.AddPropertyBag<object>(Unity.Properties.Internal.IPropertyBag<object>)
		// Unity.Properties.Internal.IPropertyBag<Unity.Properties.Internal.PropertyWrapper<Http.HttpPackage>> Unity.Properties.Internal.PropertyBagStore.GetPropertyBag<Unity.Properties.Internal.PropertyWrapper<Http.HttpPackage>>()
		// Unity.Properties.Internal.IPropertyBag<Unity.Properties.Internal.PropertyWrapper<object>> Unity.Properties.Internal.PropertyBagStore.GetPropertyBag<Unity.Properties.Internal.PropertyWrapper<object>>()
		// System.Void Unity.Properties.PropertyBag.AcceptWithSpecializedVisitor<Unity.Properties.Internal.PropertyWrapper<Http.HttpPackage>>(Unity.Properties.Internal.IPropertyBag<Unity.Properties.Internal.PropertyWrapper<Http.HttpPackage>>,Unity.Properties.Internal.IVisitor,Unity.Properties.Internal.PropertyWrapper<Http.HttpPackage>&)
		// System.Void Unity.Properties.PropertyBag.AcceptWithSpecializedVisitor<Unity.Properties.Internal.PropertyWrapper<object>>(Unity.Properties.Internal.IPropertyBag<Unity.Properties.Internal.PropertyWrapper<object>>,Unity.Properties.Internal.IVisitor,Unity.Properties.Internal.PropertyWrapper<object>&)
		// System.Void Unity.Properties.PropertyBag.Register<Unity.Mathematics.float3>(Unity.Properties.ContainerPropertyBag<Unity.Mathematics.float3>)
		// System.Void Unity.Properties.PropertyBag.Register<UnityEngine.Color>(Unity.Properties.ContainerPropertyBag<UnityEngine.Color>)
		// System.Void Unity.Properties.PropertyBag.Register<object>(Unity.Properties.ContainerPropertyBag<object>)
		// System.Void Unity.Properties.PropertyContainer.Visit<Unity.Properties.Internal.PropertyWrapper<Http.HttpPackage>>(Unity.Properties.Internal.PropertyWrapper<Http.HttpPackage>&,Unity.Properties.Internal.IVisitor,Unity.Properties.VisitParameters)
		// System.Void Unity.Properties.PropertyContainer.Visit<Unity.Properties.Internal.PropertyWrapper<object>>(Unity.Properties.Internal.PropertyWrapper<object>&,Unity.Properties.Internal.IVisitor,Unity.Properties.VisitParameters)
		// bool Unity.Properties.PropertyContainer.Visit<Unity.Properties.Internal.PropertyWrapper<Http.HttpPackage>>(Unity.Properties.Internal.PropertyWrapper<Http.HttpPackage>&,Unity.Properties.Internal.IVisitor,Unity.Properties.Internal.VisitErrorCode&)
		// bool Unity.Properties.PropertyContainer.Visit<Unity.Properties.Internal.PropertyWrapper<object>>(Unity.Properties.Internal.PropertyWrapper<object>&,Unity.Properties.Internal.IVisitor,Unity.Properties.Internal.VisitErrorCode&)
		// Http.HttpPackage Unity.Serialization.Json.JsonSerialization.FromJson<Http.HttpPackage>(string,Unity.Serialization.Json.JsonSerializationParameters)
		// object Unity.Serialization.Json.JsonSerialization.FromJson<object>(string,Unity.Serialization.Json.JsonSerializationParameters)
		// System.Void Unity.Serialization.Json.JsonSerialization.FromJsonOverride<object>(string,object&,Unity.Serialization.Json.JsonSerializationParameters)
		// System.Void Unity.Serialization.Json.JsonSerialization.ToJson<Http.HttpPackage>(Unity.Serialization.Json.JsonWriter,Http.HttpPackage,Unity.Serialization.Json.JsonSerializationParameters)
		// System.Void Unity.Serialization.Json.JsonSerialization.ToJson<object>(Unity.Serialization.Json.JsonWriter,object,Unity.Serialization.Json.JsonSerializationParameters)
		// string Unity.Serialization.Json.JsonSerialization.ToJson<Http.HttpPackage>(Http.HttpPackage,Unity.Serialization.Json.JsonSerializationParameters)
		// string Unity.Serialization.Json.JsonSerialization.ToJson<object>(object,Unity.Serialization.Json.JsonSerializationParameters)
		// bool Unity.Serialization.Json.JsonSerialization.TryFromJson<Http.HttpPackage>(Unity.Serialization.Json.SerializedObjectReader,Http.HttpPackage&,Unity.Serialization.Json.DeserializationResult&,Unity.Serialization.Json.JsonSerializationParameters)
		// bool Unity.Serialization.Json.JsonSerialization.TryFromJson<Http.HttpPackage>(string,Http.HttpPackage&,Unity.Serialization.Json.DeserializationResult&,Unity.Serialization.Json.JsonSerializationParameters)
		// bool Unity.Serialization.Json.JsonSerialization.TryFromJson<object>(Unity.Serialization.Json.SerializedObjectReader,object&,Unity.Serialization.Json.DeserializationResult&,Unity.Serialization.Json.JsonSerializationParameters)
		// bool Unity.Serialization.Json.JsonSerialization.TryFromJson<object>(string,object&,Unity.Serialization.Json.DeserializationResult&,Unity.Serialization.Json.JsonSerializationParameters)
		// bool Unity.Serialization.Json.JsonSerialization.TryFromJsonOverride<Http.HttpPackage>(string,Http.HttpPackage&,Unity.Serialization.Json.DeserializationResult&,Unity.Serialization.Json.JsonSerializationParameters)
		// bool Unity.Serialization.Json.JsonSerialization.TryFromJsonOverride<object>(string,object&,Unity.Serialization.Json.DeserializationResult&,Unity.Serialization.Json.JsonSerializationParameters)
		// int Unity.VisualScripting.Flow.FetchValue<int>(Unity.VisualScripting.ValueInput,Unity.VisualScripting.GraphReference)
		// UnityEngine.Vector2Int Unity.VisualScripting.Flow.GetValue<UnityEngine.Vector2Int>(Unity.VisualScripting.ValueInput)
		// int Unity.VisualScripting.Flow.GetValue<int>(Unity.VisualScripting.ValueInput)
		// object Unity.VisualScripting.Flow.GetValue<object>(Unity.VisualScripting.ValueInput)
		// object Unity.VisualScripting.GraphPointer.GetElementData<object>(Unity.VisualScripting.IGraphElementWithData)
		// Unity.VisualScripting.ValueInput Unity.VisualScripting.Unit.ValueInput<UnityEngine.Vector2Int>(string)
		// Unity.VisualScripting.ValueInput Unity.VisualScripting.Unit.ValueInput<UnityEngine.Vector2Int>(string,UnityEngine.Vector2Int)
		// Unity.VisualScripting.ValueInput Unity.VisualScripting.Unit.ValueInput<int>(string)
		// Unity.VisualScripting.ValueInput Unity.VisualScripting.Unit.ValueInput<int>(string,int)
		// Unity.VisualScripting.ValueInput Unity.VisualScripting.Unit.ValueInput<object>(string)
		// Unity.VisualScripting.ValueInput Unity.VisualScripting.Unit.ValueInput<object>(string,object)
		// Unity.VisualScripting.ValueOutput Unity.VisualScripting.Unit.ValueOutput<Unity.Entities.EntityManager>(string,System.Func<Unity.VisualScripting.Flow,Unity.Entities.EntityManager>)
		// Unity.VisualScripting.ValueOutput Unity.VisualScripting.Unit.ValueOutput<UnityEngine.Vector2Int>(string,System.Func<Unity.VisualScripting.Flow,UnityEngine.Vector2Int>)
		// Unity.VisualScripting.ValueOutput Unity.VisualScripting.Unit.ValueOutput<byte>(string,System.Func<Unity.VisualScripting.Flow,byte>)
		// Unity.VisualScripting.ValueOutput Unity.VisualScripting.Unit.ValueOutput<object>(string)
		// Unity.VisualScripting.ValueOutput Unity.VisualScripting.Unit.ValueOutput<object>(string,System.Func<Unity.VisualScripting.Flow,object>)
		// object UnityEngine.Component.GetComponent<object>()
		// object UnityEngine.GameObject.AddComponent<object>()
		// object UnityEngine.GameObject.GetComponent<object>()
		// object[] UnityEngine.GameObject.GetComponentsInChildren<object>()
		// object[] UnityEngine.GameObject.GetComponentsInChildren<object>(bool)
		// object UnityEngine.JsonUtility.FromJson<object>(string)
		// object UnityEngine.Object.FindObjectOfType<object>()
		// object UnityEngine.Object.FindObjectOfType<object>(bool)
		// object UnityEngine.Object.Instantiate<object>(object)
		// object UnityEngine.Object.Instantiate<object>(object,UnityEngine.Vector3,UnityEngine.Quaternion,UnityEngine.Transform)
		// object UnityEngine.Resources.Load<object>(string)
	}
}