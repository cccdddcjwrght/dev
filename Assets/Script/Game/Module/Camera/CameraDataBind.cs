using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


/**@defgroup group100017 战斗系统-战斗相机-曾长付
 *-----------------------------
 *[作者:]{zcf}\n
 *[时间:]{2015年6月17日 16:11:13}\n
 *[模块描述:]{战斗相机系统--相机操作}\n
 *[类名:]CameraDataBind\n
 *-----------------------------
 *@{
*/
/// <summary>
/// 相机操作
/// </summary>
[System.Serializable]
public class CameraDataBind
{
	static public bool G_UseLimit = true;
	/// <summary>
	/// 名
	/// </summary>
	public string name;
	/// <summary>
	/// 是否激活
	/// </summary>
	public bool isEnable;
	/// <summary>
	/// 是否限制
	/// </summary>
	public bool isUselimit;
	/// <summary>
	/// 开始值
	/// </summary>
	public float startValue;
	/// <summary>
	/// 最小值
	/// </summary>
	public float minValue;
	/// <summary>
	/// 最大值
	/// </summary>
	public float maxValue;
	/// <summary>
	/// 当前值
	/// </summary>
	public float currentValue;
	/// <summary>
	/// 输入值
	/// </summary>
	public float inputValue;
	public float wrapValue;

	[NonSerialized]
	public float limitScale = 1;

	/// <summary>
	/// 速度
	/// </summary>
	public float speed = 2f;
	public float refSpeed = 0f;
	public float time = 1f;
	public bool isAxis = false;
	public bool lerp = false;
	public Vector3 startPos;
	public Vector3 endPos;
	public bool enableControl = true;
	public bool disableBounce = false;

	public bool limit
	{
		get
		{
			return G_UseLimit && isUselimit;
		}
	}

	public bool isChange { get { return Mathf.Abs(currentValue - inputValue) > 0.001f; } }

	public bool isEnableChange { get { return isEnable && isChange; } }

	private float _temptime = 0;
	private float _tempspeed = 0;

	/// <summary>
	/// 控制轴
	/// </summary>
	public List<ControlAxis> control;

	public float GetValue(bool isLerp = false, bool getlast = false)
	{
		if (getlast || currentValue == inputValue) return currentValue = inputValue;
		var sp = speed;
		var le = isLerp || lerp;
		if (_temptime > 0 && _tempspeed > 0)
		{
			_temptime -= Time.deltaTime;
			sp = _tempspeed;
			le = false;
		}
		var dt = Time.deltaTime * sp;
		if (!le)
			currentValue = Mathf.MoveTowards(currentValue, inputValue, dt);
		else
			currentValue = Mathf.Lerp(currentValue, inputValue, dt);
		if (wrapValue != 0) return currentValue % wrapValue;
		return currentValue;
	}

	public Vector3 GetVector(bool slerp = false)
	{
		var v = currentValue - minValue;
		return slerp ?
						Vector3.Slerp(startPos, endPos, v / (maxValue - minValue))
						: Vector3.Lerp(startPos, endPos, v / (maxValue - minValue));
	}

	public float CalulateTime(float target)
	{
		return Mathf.Abs(currentValue - target) / speed;
	}

	public float CalulateSpeed(float target, float time, bool use = false)
	{
		var sp = time > 0 ? Mathf.Abs(currentValue - target) / time : speed;
		if (use && time > 0 && sp != 0)
		{
			_tempspeed = sp;
			_temptime = time;
		}
		return sp;
	}

	public CameraDataBind InvertAxis(bool state)
	{
		control?.ForEach(c => c.isInvertAxis = state);
		return this;
	}

	public CameraDataBind AxisLimit(bool limit, float min, float max, bool onlylimit = false)
	{
		control?.ForEach(c => c.Limit(limit, min, max, onlylimit));
		return this;
	}

	public float Rate(float val = 0)
	{
		val = val == 0 ? GetValue() : val;
		if (maxValue != 0 && minValue != 0)
			return (val - minValue) / (maxValue - minValue);
		return -1f;
	}
}

//@}