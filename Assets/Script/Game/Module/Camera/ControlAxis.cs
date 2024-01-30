using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/**@defgroup group100017 战斗系统-战斗相机-曾长付
 *-----------------------------
 *[作者:]{zcf}\n
 *[时间:]{2015年6月17日 16:11:13}\n
 *[模块描述:]{战斗相机系统--控制轴}\n
 *[类名:]ControlAxis\n
 *-----------------------------
 *@{
*/

public delegate float OnCalulateAxis(List<ControlAxis> axises, out bool reset, out bool state, float val);
public delegate void OnCalulateAxisInput(ControlAxis axis, ref float input, float lastvalue);


/// <summary>
/// 控制轴
/// </summary>
[System.Serializable]
public class ControlAxis
{
	public static OnCalulateAxis OnCalulate;

	static public float G_TOUCH_VALUE = 1;

	static public bool AnyKeyInput { get; private set; }

	static public event Action OnAnyKeyInput;

	public enum EventMouseButton
	{
		None = -10,
		Left = 0,
		Right = 1,
		Middle = 2
	}
	public enum EventTouth
	{
		none = 0,
		one = 1,
		two = 2,
		/*three = 3,
        four = 4,
        five = 5*/
	}
	public enum TouchState
	{
		none = 0,
		xMove,
		yMove,
		scale
	}
	/// <summary>
	/// 控制轴名
	/// </summary>
	public string axisName;
	/// <summary>
	/// 灵敏度
	/// </summary>
	public float sensitivity;
	public float touchRevise;
	/// <summary>
	/// 是否反向控制
	/// </summary>
	public bool isInvertAxis;
	/// <summary>
	/// 控制按键
	/// </summary>
	public KeyCode additionalKey;
	/// <summary>
	/// 控制鼠标
	/// </summary>
	public EventMouseButton additionalMouseButton;
	/// <summary>
	/// 控制触控
	/// </summary>
	public EventTouth additionalTouch;
	/// <summary>
	/// 触控状态
	/// </summary>
	public TouchState touchState;
	public bool isResetOnRelease;

	public bool limit;
	public float min;
	public float max;

	public Dictionary<int, TouchData> touchList = new Dictionary<int, TouchData>();

	public void Limit(bool limit, float min, float max, bool onlyenable = false)
	{
		this.limit = limit;
		if (!onlyenable)
		{
			this.min = min;
			this.max = max;
		}
	}


	#region Static

	/// <summary>
	/// 计算控件变化
	/// </summary>
	/// <param name="axiss"></param>
	/// <param name="isReset"></param>
	/// <returns></returns>
	static public float CalcuControlAxis(List<ControlAxis> axiss, out bool isReset, out bool isAxis, float val = 0)
	{
		isReset = false;
		float aValue = 0f;
		float aSign = 1f;
		isAxis = false;
		if (axiss != null && axiss.Count > 0)
		{
			foreach (var axis in axiss)
			{
				try
				{
					if (axis.isInvertAxis)
					{
						aSign = -1.0f;
					}
					if (axis.additionalKey == KeyCode.None && axis.additionalMouseButton == ControlAxis.EventMouseButton.None)
					{
						float anAxis = 0.0f;
						anAxis = Input.GetAxis(axis.axisName);
						aValue += anAxis * axis.sensitivity * aSign;
					}
					else
					{
						if (Input.GetKey(axis.additionalKey) || Input.GetMouseButton((int)axis.additionalMouseButton))
						{
							aValue += Input.GetAxis(axis.axisName) * axis.sensitivity * aSign;
							isAxis = isAxis || true;

							if (axis.limit && axis.min != 0 && axis.max != 0)
								aValue = Math.Clamp(aValue + val, axis.min, axis.max) - val;
						}
						else
						{
							if (axis.isResetOnRelease) isReset = axis.isResetOnRelease;
						}
					}
				}
				catch (Exception e)
				{
					Debug.LogError(e.Message);
					continue;
				}
			}
		}
		return aValue;
	}

	/// <summary>
	/// 计算控件变化
	/// </summary>
	/// <param name="axiss"></param>
	/// <param name="isReset"></param>
	/// <returns></returns>
	static public float CalcuTouchControlAxis(List<ControlAxis> axiss, out bool isReset, float val = 0)
	{
		isReset = false;
		float aValue = 0f;
		float temp = 0f;
		float aSign = 1f;
		ControlAxis axis;
		int currentTouchID = 0;
		TouchData currentTouch = null;

		if (axiss != null && axiss.Count > 0)
		{
			axis = axiss.Find(pair => pair.additionalTouch != EventTouth.none);
			if (axis != null)
			{
				if (axis.isInvertAxis)
				{
					aSign = -1.0f;
				}
				if (Input.touchCount == (int)axis.additionalTouch)
				{
					for (int i = 0; i < Input.touchCount; ++i)
					{
						Touch touch = Input.GetTouch(i);
						currentTouchID = touch.fingerId;
						currentTouch = GetTouch(axis, currentTouchID, touch);
						bool pressed = (touch.phase == TouchPhase.Began) || currentTouch.touchBegan;
						bool unpressed = (touch.phase == TouchPhase.Canceled) || (touch.phase == TouchPhase.Ended);
						currentTouch.touchBegan = false;
						currentTouch.delta = pressed ? Vector2.zero : touch.position - currentTouch.pos;
						currentTouch.totalDelta += currentTouch.delta;
						currentTouch.touchTime += Time.deltaTime;
						currentTouch.lastPos = currentTouch.pos;
						currentTouch.pos = touch.position;
						ProcessTouch(pressed, unpressed);
						if (unpressed) RemoveTouch(axis, currentTouchID);
						currentTouch = null;

					}

					Bounds oldb = new Bounds(Vector3.zero, Vector3.zero);
					Bounds newb = new Bounds(Vector3.zero, Vector3.zero);

					foreach (var v in axis.touchList)
					{
						if ((int)axis.additionalTouch >= v.Key + 1 && v.Value.touchTime > 0.1f)
						{
							oldb.Encapsulate(v.Value.lastPos);
							newb.Encapsulate(v.Value.pos);
						}
					}

					if (axis.touchState == TouchState.scale && axis.additionalTouch != EventTouth.one)
					{
						Vector3 v = newb.size - oldb.size;
						aSign *= Vector3.SqrMagnitude(newb.size) - Vector3.SqrMagnitude(oldb.size);
						aSign = aSign / Mathf.Abs(aSign);
						temp = v.sqrMagnitude * aSign;
						temp = temp > 0 ? 0.5f : temp < 0 ? -0.5f : 0f;
						aValue = temp * axis.sensitivity * 0.2f * (axis.touchRevise == 0 ? 1 : axis.touchRevise);
					}
					else
					{
						aValue = axis.touchState == TouchState.yMove ? (newb.center - oldb.center).y : (newb.center - oldb.center).x;
						aValue *= axis.sensitivity * aSign * 0.2f * (axis.touchRevise == 0 ? ControlAxis.G_TOUCH_VALUE : axis.touchRevise);
					}

					if (axis.limit && axis.min != 0 && axis.max != 0)
						aValue = Math.Clamp(aValue + val, axis.min, axis.max) - val;

				}
				else
				{
					axis.touchList?.Clear();
				}
			}
		}

		return aValue;
	}

	static private void ProcessTouch(bool fristTouch, bool anTouch)
	{

	}

	/// <summary>
	/// 控件输入值校验
	/// </summary>
	/// <param name="axiss"></param>
	/// <param name="isReset"></param>
	/// <returns></returns>
	static public void Control(CameraDataBind bind, bool isAdd = true)
	{
		float offset = 1f;
		float inputValue = 0f;
		bool isReset = false;

		if (bind.isEnable && bind.enableControl)
		{

			if (OnCalulate != null)
				OnCalulate(bind.control, out isReset, out bind.isAxis, bind.inputValue);
			else
			{

#if (!UNITY_IPHONE && !UNITY_ANDROID && !UNITY_BLACKBERRY && !UNITY_WP8) || UNITY_EDITOR
				inputValue = ControlAxis.CalcuControlAxis(bind.control, out isReset, out bind.isAxis, bind.inputValue);
#else
				inputValue = ControlAxis.CalcuTouchControlAxis(bind.control, out isReset, bind.inputValue);
#endif
			}
			if (bind.limit)
			{
				if (bind.inputValue > bind.maxValue && inputValue > 0f && inputValue > offset)
				{
					inputValue = offset;
				}
				else if (bind.inputValue < bind.minValue && inputValue < 0f && inputValue < -offset)
				{
					inputValue = -offset;
				}
			}
			if (isAdd) bind.inputValue += inputValue;
			else bind.inputValue = inputValue;
			if (isReset)
			{
				bind.inputValue = bind.startValue;
			}
		}
		ControlLimit(bind);
	}

	/// <summary>
	/// 控件输入值限制
	/// </summary>
	/// <param name="bind"></param>
	static public void ControlLimit(CameraDataBind bind)
	{

		if (bind.limit)
		{
			if (!bind.disableBounce)
			{
				if (bind.inputValue > bind.maxValue)
				{
					float aDifference = Math.Abs(bind.inputValue - bind.maxValue);
					bind.inputValue = Mathf.SmoothDamp(bind.inputValue, bind.maxValue, ref bind.refSpeed, 1.0f / (1f * aDifference));
				}
				else if (bind.inputValue < bind.minValue)
				{
					float aDifference = Math.Abs(bind.inputValue - bind.minValue);
					bind.inputValue = Mathf.SmoothDamp(bind.inputValue, bind.minValue, ref bind.refSpeed, 1.0f / (1f * aDifference));
				}
			}else
				bind.inputValue = Mathf.Clamp(bind.inputValue,bind.minValue,bind.maxValue);
		}
	}

	static public void CheckAnyKeyInput()
	{
		AnyKeyInput = Input.anyKey;
		if (AnyKeyInput)
			OnAnyKeyInput?.Invoke();
	}

	static public void ListenEvent(Action call, bool remove = false)
	{
		if (call != null)
		{
			OnAnyKeyInput -= call;
			if (!remove)
				OnAnyKeyInput += call;
		}
	}

	/// <summary>
	/// 获取触控点
	/// </summary>
	/// <param name="axis"></param>
	/// <param name="id"></param>
	/// <param name="touch"></param>
	/// <returns></returns>
	static private TouchData GetTouch(ControlAxis axis, int id, Touch touch)
	{
		TouchData touchData = null;
		if (axis != null && axis.touchList != null && axis.touchList.Count > 0 && axis.touchList.ContainsKey(id))
		{
			touchData = axis.touchList[id];
		}
		else
		{
			if (axis != null)
			{
				if (axis.touchList == null) axis.touchList = new Dictionary<int, TouchData>();
				touchData = new TouchData();
				touchData.id = id;
				touchData.touch = touch;
				touchData.touchBegan = true;
				touchData.lastPos = touch.position;
				touchData.pos = touch.position;
				axis.touchList.Add(id, touchData);
			}
		}
		return touchData;
	}

	/// <summary>
	/// 移除触控点
	/// </summary>
	/// <param name="axis"></param>
	/// <param name="id"></param>
	static private void RemoveTouch(ControlAxis axis, int id)
	{
		if (axis != null && axis.touchList != null && axis.touchList.Count > 0 && axis.touchList.ContainsKey(id))
			axis.touchList.Remove(id);
	}



	#endregion

	#region Class

	public class TouchData
	{
		public int id;
		public Touch touch;
		public Vector2 pos;
		public Vector2 lastPos;
		public Vector2 delta;
		public Vector2 totalDelta;
		public float touchTime = 0f;
		public bool touchBegan = true;
	}

	#endregion
}
//@}
