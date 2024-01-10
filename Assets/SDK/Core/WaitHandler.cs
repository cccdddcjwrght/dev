using System.Collections;
using System;

namespace LibRequest
{

	public interface IResult
	{
		event Action<IResult> onCompleted;

		public bool isDone { get; }
		public string error { get; }
		public object value { get; set; }


		public void Result(bool s, string text);
		public void Clear();


	}

	public class SResult : IResult
	{
		public event Action<IResult> onCompleted;
		public bool isDone { get; private set; }
		public string error { get; private set; }
		public object value { get; set; }


		public virtual void Result(bool s, string text)
		{
			isDone = true;
			error = text;
			var c = onCompleted;
			onCompleted = null;
			c?.Invoke(this);
		}

		public virtual void Clear()
		{
			isDone = false;
			error = null;
			onCompleted = null;
		}





	}

	public class IWait : IEnumerator, IResult
	{
		public bool isDone { get; private set; }
		public string error { get; private set; }
		public virtual bool isSucceed { get { return string.IsNullOrEmpty(error); } }

		public virtual bool isRequesting { get; protected set; }


		public event Action<IResult> onCompleted;

		public object value { get; set; }

		public long timetick;

		public void Set(string error)
		{
			this.error = error;
			this.isDone = true;
			this.isRequesting = false;
			this.timetick = 0;
			var c = onCompleted;
			onCompleted = null;
			c?.Invoke(this);
		}

		public void Set(string error, object val)
		{
			value = val;
			Set(error);
		}

		public void Result(bool s, string text)
		{
			if (s)
				Set(null, text);
			else
				Set(text);
		}

		public virtual IWait Request() => this;
		public virtual void Excute() { }

		public void Close()
		{
			Clear();
		}

		public void Restore()
		{
			isDone = false;
			error = null;
			value = null;
			isRequesting = false;
		}

		public virtual void Clear()
		{
			Restore();
			timetick = 0;
			onCompleted = null;
		}

		#region IEnumerator
		public bool MoveNext()
		{
			if (timetick > 0)
			{
				if (timetick < DateTime.Now.Ticks)
					Set($" {GetType()} timeout!!!");
			}
			return !isDone;
		}

		public void Reset()
		{
			Clear();
		}

		public object Current => value;
		#endregion
	}

	public abstract class IRequestWait : IWait
	{
		public object proxy { get; private set; }
		public string id { get; private set; }
		public virtual int state { get; protected set; }

		/// <summary>
		/// 状态， 结果， tips
		/// </summary>
		public event Action<bool, string, string> call;
		public object ext;
		public string extData;

		public IRequestWait Bind<T>(T uhicall)
		{
			var old = proxy;
			proxy = uhicall;
			if (proxy != null)
				AddEvent();
			else
			{
				proxy = old;
				RemoveEvent();
				proxy = null;
			}
			return this;
		}

		/// <summary>
		/// 请求
		/// </summary>
		/// <param name="id"></param>
		/// <param name="call"> 状态， 结果， tips</param>
		public IRequestWait Request(string id, Action<bool, string, string> call = null)
		{
			if (string.IsNullOrEmpty(id) || proxy == null)
				Set("id or bind is null");
			else
			{
				extData = id;

				this.id = id = id.Split('|')[0];
				this.call -= call;
				this.call += call;
				Restore();
#if UNITY_EDITOR
				Set("Unsupported platform!!!");
				return this;
#endif
				isRequesting = true;
				DoRequest(id);
			}
			return this;
		}

		public virtual IEnumerator WaitState(int state, float timeout = 0)
		{
			yield return null;
		}

		public override void Clear()
		{
			base.Clear();
			RemoveEvent();
			this.id = null;
			this.call = null;
			this.proxy = null;
			this.state = -1;
		}

		protected abstract void DoRequest(string id);

		protected abstract void AddEvent();

		protected abstract void RemoveEvent();

	}

}