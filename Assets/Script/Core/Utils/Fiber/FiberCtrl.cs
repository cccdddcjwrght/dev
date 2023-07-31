using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace Fibers
{

	public enum FiberBucket : int
	{
		Manual = 0,
		
		/*BeforeFixedUpdate1,
		BeforeFixedUpdate2,
		BeforeFixedUpdate3,
		FixedUpdate,
		AfterFixedUpdate1,
		AfterFixedUpdate2,
		AfterFixedUpdate3,
		
		BeforeUpdate1,
		BeforeUpdate2,
		BeforeUpdate3,*/
		Update,
		/*AfterUpdate1,
		AfterUpdate2,
		AfterUpdate3,
		
		BeforeLateUpdate1,
		BeforeLateUpdate2,
		BeforeLateUpdate3,
		LateUpdate,
		AfterLateUpdate1,
		AfterLateUpdate2,
		AfterLateUpdate3,
		AfterLateUpdate4,*/
	}

    public class Fiber
    {
        public delegate void OnExitHandler();
        
		public class DetectFiber
        {
            public DetectFiber(OnExitHandler handler)
            {
                this.handler = handler;
            }
            public OnExitHandler handler;
        }
		
		public class OnTerminate
        {
            public OnTerminate(OnExitHandler handler)
            {
                this.handler = handler;
            }
            public OnExitHandler handler;
        }
		
		public class OnExit
        {
            public OnExit(OnExitHandler handler)
            {
                this.handler = handler;
            }
            public OnExitHandler handler;
        }
		
		public class OnLeave
        {
            public OnLeave(OnExitHandler handler)
            {
                this.handler = handler;
            }
            public OnExitHandler handler;
        }
		
		public class OnEnter
        {
            public OnEnter(OnExitHandler handler)
            {
                this.handler = handler;
            }
            public OnExitHandler handler;
        }
		
        public class Goto
        {
            public Goto(IEnumerator code)
            {
                this.code = code;
            }
            public IEnumerator code;
        }
        public class State
        {
            public IEnumerator code;
            public OnExitHandler onExit;
			public OnExitHandler onLeave;
			public OnExitHandler onEnter;
			public OnExitHandler onTerminate;
			
            public State(IEnumerator code)
            {
                this.code = code;
                this.onExit = null;
				this.onLeave = null;
				this.onEnter = null;
                this.onTerminate = null;
            }

            public void ActiveByPool(IEnumerator code)
            {
                this.code = code;
                this.onExit = null;
                this.onLeave = null;
                this.onEnter = null;
                this.onTerminate = null;
            }

            public void Clear()
            {
                this.code = null;
                this.onExit = null;
                this.onLeave = null;
                this.onEnter = null;
                this.onTerminate = null;
            }
        }
		public class Wait {
			public float TimeToWait;
			private bool clickToFinish;
			
			public Wait (float timeToWait)
			{
				this.TimeToWait = timeToWait;
				this.clickToFinish = false;
			}
			
			public Wait (float timeToWait, bool clickToFinish)
			{
				this.TimeToWait = timeToWait;
				this.clickToFinish = clickToFinish;
			}
			
			public IEnumerator Loop() {
				while (TimeToWait>0) {
					TimeToWait-=Time.deltaTime;
					
					if (clickToFinish && (UnityEngine.Input.touchCount>0 || UnityEngine.Input.GetMouseButtonDown(0) || UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Space))) break;
					
					yield return null;
				}
				yield break;
			}
		}
#if !UNITY_2018_1_OR_NEWER		
		class WWWRunner {
			private WWW www;
			
			public WWWRunner(WWW www) {
				this.www = www;
			}
			
			public IEnumerator Loop() {
				while(!www.isDone && www.error == null) {
					yield return null;
				}
			}
		}
#endif		

        class WebRequestRunner
        {
            private UnityEngine.Networking.UnityWebRequest www;
            public WebRequestRunner(UnityEngine.Networking.UnityWebRequest www) {
                this.www = www;
            }
			
            public IEnumerator Loop() {
                while(!www.isDone && www.error == null) {
                    yield return null;
                }
            }
        }
		
		class AsyncOperationRunner {
			private AsyncOperation async;
			
			public AsyncOperationRunner(AsyncOperation async) {
				this.async = async;
			}
			
			public IEnumerator Loop() {
				while(!async.isDone) {
					yield return null;
				}
			}			
		}
		
		public FiberBucket bucket = FiberBucket.Update;
        BetterList<State> cor = new BetterList<State>();
        private int m_instId = 0; // 实例化ID
        private bool m_isInStep = false; // 是否在运行状体
        public bool  isInStep {  get { return m_isInStep;  } }
        public Fiber()
		{
            m_instId = 0;
        }

		public Fiber(FiberBucket bucket)
		{
			this.bucket = bucket;
		}
        public Fiber(IEnumerator main)
        {
			Start(main);
        }
        public Fiber(IEnumerator main, FiberBucket bucket)
        {
			this.bucket = bucket;
			Start(main);
        }
		
		public void Start(IEnumerator main, bool isImmediately = false)
		{
			if (!IsTerminated) Terminate();
            cor.Add(FiberPools.AllocState(main));//new State(main));
            m_instId++;
            if (isImmediately && m_isInStep == false)
            {
                Step();
            }
            /*
			if (bucket != FiberBucket.Manual) { // Only do the initial step if fiber is not manual
				bool hasMore = Step(); //Always do first step immediately
				if (hasMore) FiberCtrl.AddFiber(this, bucket);
			}
			*/
        }
		
		public void Push(IEnumerator sub)
		{
            m_instId++;
            if (cor.size>0 && cor.Peek().onLeave!=null) cor.Peek().onLeave();
			cor.Add(FiberPools.AllocState(sub));
            //if (IsTerminated && bucket != FiberBucket.Manual) FiberCtrl.AddFiber(this, bucket);
        }
		
		public void GotoState(IEnumerator sub)
		{
            m_instId++;
            Pop();
			cor.Add(FiberPools.AllocState(sub));
            //if (IsTerminated && bucket != FiberBucket.Manual) FiberCtrl.AddFiber(this, bucket);
        }
		
		public int Count { get { return cor.size; } }

        public bool Step()
        {
            if (m_isInStep == true)
            {
                // 堆栈内已有代码调用该函数, 直接返回
                Debug.LogError("there have another code run step in same stack!");
                return true;
            }

            m_isInStep = true;      // 标记正在运行Step, 一个协程同时只能跑一个Step!
            int runInst = m_instId; // 标记运行的是同一个实例, 若实例不同表示该step生命周期已经结束!!!!
            try
            {
                while (true)
                {
                    while (runInst == m_instId && cor.size > 0 && !cor.Peek().code.MoveNext())
                    {
                        if (runInst != m_instId)
                        {
                            return true;
                        }

                        if (cor.size > 0) Pop();
                    }
                    if (runInst == m_instId && cor.size > 0)
                    {
                        object current = cor.Peek().code.Current;
                        if (current == null)
                        {
                            //optimization, do nothing on null, since this is the most common use case
                        }
                        else if (current is IEnumerator)
                        {
                            if (cor.size > 0 && cor.Peek().onLeave != null) cor.Peek().onLeave();
                            cor.Add(FiberPools.AllocState(((IEnumerator)current)));
                            continue;
                        }
                        else if (current is OnExit)
                        {
                            cor.Peek().onExit = ((OnExit)current).handler;
                            continue;
                        }
                        else if (current is OnLeave)
                        {
                            cor.Peek().onLeave = ((OnLeave)current).handler;
                            continue;
                        }
                        else if (current is OnEnter)
                        {
                            cor.Peek().onEnter = ((OnEnter)current).handler;
                            continue;
                        }
                        else if (current is OnTerminate)
                        {
                            cor.Peek().onTerminate = ((OnTerminate)current).handler;
                            continue;
                        }
                        else if (current is Goto)
                        {
                            Pop();
                            cor.Add(FiberPools.AllocState((((Goto)current).code)));
                            continue;
                        }
                        else if (current is Wait)
                        {
                            cor.Add(FiberPools.AllocState((((Wait)current).Loop())));
                            continue;
                        }
#if !UNITY_2018_1_OR_NEWER                        
                        else if (current is WWW)
                        {
                            cor.Add(FiberPools.AllocState((new WWWRunner((WWW)current).Loop())));
                        }
#else
                        else if (current is UnityEngine.Networking.UnityWebRequest)
                        {
                            cor.Add(FiberPools.AllocState((new WebRequestRunner((UnityEngine.Networking.UnityWebRequest)current).Loop())));
                        }
#endif
                        else if (current is AsyncOperation)
                        {
                            var asyncRunner = new AsyncOperationRunner((AsyncOperation)current);
                            cor.Add(FiberPools.AllocState((asyncRunner.Loop())));
                        }
                        else if (current is DetectFiber)
                        {
                            ((DetectFiber)current).handler(); //Callback immeditately
                            continue;
                        }
                    }

                    return runInst == m_instId && cor.size > 0;
                }
            }
            finally
            {
                // 异常时正常时都要清除标记
                m_isInStep = false;
            }
        }

        void Pop(bool terminating = false)
        {
			if (cor==null || cor.size == 0) return;
            State s = cor.Pop();
            if (s.onExit != null) s.onExit();
			if(terminating && s.onTerminate != null) s.onTerminate();
			if (cor.size>0 && cor.Peek().onEnter!=null) cor.Peek().onEnter();
            FiberPools.FreeState(s);
        }
		
		public static void TerminateIfAble(Fiber f){
			if( f != null && !f.IsTerminated ) f.Terminate();
		}
		
        public void Terminate()
        {
            m_instId++;
            while (cor.size > 0) Pop(true);
        }

        public bool IsTerminated { get { return cor.size <= 0; } }
    
		public void Log() {
			if (cor.size>0) {
				Debug.Log("[" + Time.frameCount + "] fiber:" + this.ToString() + "  code:" + cor.Peek().code.ToString());
			}
			else {
				Debug.Log("[" + Time.frameCount + "] fiber:" + this.ToString() + "  code:empty");	
			}
		}
    }
}

public class FiberCtrl : MonoBehaviour
{	
	public class FiberPool {
		BetterList<Fibers.Fiber> running = new BetterList<Fibers.Fiber>();
		BetterList<Fibers.Fiber> available = new BetterList<Fibers.Fiber>();

		public Fibers.Fiber AllocateFiber() {
			return AllocateFiber(Fibers.FiberBucket.Manual);
		}
		
		public Fibers.Fiber AllocateFiber(Fibers.FiberBucket bucket) {
			Fibers.Fiber f = available.size > 0 ? available.Pop() : new Fibers.Fiber();
			f.bucket = bucket;
			return f;
		}
		
		public Fibers.Fiber AllocateFiber(IEnumerator e) {
			return AllocateFiber(e, Fibers.FiberBucket.Update);
		}

		public Fibers.Fiber AllocateFiber(IEnumerator e, Fibers.FiberBucket bucket) {
			Fibers.Fiber f = AllocateFiber(bucket);
			f.Start(e);
			return f;
		}
		
		public void ReleaseFiber(ref Fibers.Fiber fiber) {
			if(fiber != null) {
				Fibers.Fiber.TerminateIfAble(fiber);
				available.Add(fiber);
				fiber = null;
			}
		}

		public void Run(IEnumerator e, bool stepOneTime = false) {
			Fibers.Fiber f = AllocateFiber(e, Fibers.FiberBucket.Manual);
			running.Add(f);
			if (stepOneTime) {
				f.Step();
			}
		}

		public void Step() {
			for(int i = 0; i < running.size; ++i) {
				if(!running[i].Step()) {
					available.Add(running[i]);
					running.RemoveAt(i);
					--i;
				}
			}
		}
	}
	
	static FiberCtrl _instance;
	static FiberCtrl instance
	{
		get
		{
			if (_instance==null) {
				_instance = new GameObject().AddComponent<FiberCtrl>();	
				_instance.name = "FiberCtrl";
				GameObject.DontDestroyOnLoad(_instance.gameObject);
			}
			return _instance;
		}
	}
	/*
	public static void AddFiber(Fibers.Fiber fiber, Fibers.FiberBucket bucket)
	{
		//Main.Instance.name = "Main";
		if (!instance.fibers.ContainsKey(bucket)) instance.fibers[bucket] = new List<Fibers.Fiber>();
		if (instance.fibers[bucket].Contains(fiber)) return; // hm, technically it could be in another bucket as well :/
		instance.fibers[bucket].Add(fiber);
	}
	*/
	
	//public static void WriteLogStatic() {
	//	instance.WriteLog();	
	//}
	
	//public void WriteLog() {
	
		/*WriteLogBucket(Fibers.FiberBucket.BeforeFixedUpdate1);
		WriteLogBucket(Fibers.FiberBucket.BeforeFixedUpdate2);
		WriteLogBucket(Fibers.FiberBucket.BeforeFixedUpdate3);
		WriteLogBucket(Fibers.FiberBucket.FixedUpdate);
		WriteLogBucket(Fibers.FiberBucket.AfterFixedUpdate1);
		WriteLogBucket(Fibers.FiberBucket.AfterFixedUpdate2);
		WriteLogBucket(Fibers.FiberBucket.AfterFixedUpdate3);
		
		WriteLogBucket(Fibers.FiberBucket.BeforeUpdate1);
		WriteLogBucket(Fibers.FiberBucket.BeforeUpdate2);
		WriteLogBucket(Fibers.FiberBucket.BeforeUpdate3);*/
		//WriteLogBucket(Fibers.FiberBucket.Update);
		/*WriteLogBucket(Fibers.FiberBucket.AfterUpdate1);
		WriteLogBucket(Fibers.FiberBucket.AfterUpdate2);
		WriteLogBucket(Fibers.FiberBucket.AfterUpdate3);
		
		
		WriteLogBucket(Fibers.FiberBucket.BeforeLateUpdate1);
		WriteLogBucket(Fibers.FiberBucket.BeforeLateUpdate2);
		WriteLogBucket(Fibers.FiberBucket.BeforeLateUpdate3);
		WriteLogBucket(Fibers.FiberBucket.LateUpdate);
		WriteLogBucket(Fibers.FiberBucket.AfterLateUpdate1);
		WriteLogBucket(Fibers.FiberBucket.AfterLateUpdate2);
		WriteLogBucket(Fibers.FiberBucket.AfterLateUpdate3);
		WriteLogBucket(Fibers.FiberBucket.AfterLateUpdate4);*/
	//}
	/*
	public void WriteLogBucket(Fibers.FiberBucket bucket) {
		if (!fibers.ContainsKey(bucket)) return;
		Debug.Log("Bucket "+bucket.ToString());
		List<Fibers.Fiber> fs = fibers[bucket];
		for (int i=0; i<fs.Count; i++)
		{
			fs[i].Log();	
		}
	}
	*/
	
	public static FiberPool Pool { get {return instance.pool;} }
	
	class FiberBucketComparer : IEqualityComparer<Fibers.FiberBucket> {
	    public static readonly FiberBucketComparer Instance = new FiberBucketComparer();
	    public bool Equals(Fibers.FiberBucket x, Fibers.FiberBucket y) { return (x == y); }
	    public int GetHashCode(Fibers.FiberBucket obj) { return (int) obj;}

	    FiberBucketComparer() {}
	}
	
	FiberPool pool = new FiberPool();
	//Dictionary<Fibers.FiberBucket, List<Fibers.Fiber>> fibers = new Dictionary<Fibers.FiberBucket, List<Fibers.Fiber>>(FiberBucketComparer.Instance);
	/*
	void StepBucket(Fibers.FiberBucket bucket)
	{
		List<Fibers.Fiber> fs;
		if(fibers.TryGetValue(bucket, out fs)) {
			for (int i=0; i<fs.Count; i++)
			{
				if (!fs[i].Step())
				{
					fs.RemoveAt(i);
					i--;
				}
			}
		}
	}
	*/
	
	/*void FixedUpdate()
	{
		StepBucket(Fibers.FiberBucket.BeforeFixedUpdate1);
		StepBucket(Fibers.FiberBucket.BeforeFixedUpdate2);
		StepBucket(Fibers.FiberBucket.BeforeFixedUpdate3);
		StepBucket(Fibers.FiberBucket.FixedUpdate);
		StepBucket(Fibers.FiberBucket.AfterFixedUpdate1);
		StepBucket(Fibers.FiberBucket.AfterFixedUpdate2);
		StepBucket(Fibers.FiberBucket.AfterFixedUpdate3);
	}*/
	
	void Update()
	{	
		//StepBucket(Fibers.FiberBucket.BeforeUpdate1);
		//StepBucket(Fibers.FiberBucket.BeforeUpdate2);
		//StepBucket(Fibers.FiberBucket.BeforeUpdate3);
		//StepBucket(Fibers.FiberBucket.Update);
		//StepBucket(Fibers.FiberBucket.AfterUpdate1);
		//StepBucket(Fibers.FiberBucket.AfterUpdate2);
		//StepBucket(Fibers.FiberBucket.AfterUpdate3);
		
		pool.Step();
	}
	
	/*void LateUpdate()
	{
		StepBucket(Fibers.FiberBucket.BeforeLateUpdate1);
		StepBucket(Fibers.FiberBucket.BeforeLateUpdate2);
		StepBucket(Fibers.FiberBucket.BeforeLateUpdate3);
		StepBucket(Fibers.FiberBucket.LateUpdate);
		StepBucket(Fibers.FiberBucket.AfterLateUpdate1);
		StepBucket(Fibers.FiberBucket.AfterLateUpdate2);
		StepBucket(Fibers.FiberBucket.AfterLateUpdate3);
		StepBucket(Fibers.FiberBucket.AfterLateUpdate4);
	}*/
	
}