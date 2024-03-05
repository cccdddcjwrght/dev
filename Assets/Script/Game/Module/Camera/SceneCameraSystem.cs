using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

namespace SGame
{
	public interface ITouchOrHited
	{
		void OnClick();
	}

	public interface ITrigger
	{
		public bool isTrigger { get; set; }
	}

	public class SceneCameraSystem : MonoSingleton<SceneCameraSystem>
	{
		private static string c_cpath;
		private static string c_current;
		private static GameObject __inst;

		const int C_DEF_FOV = 50;

		const string C_CTR_TAG = "__cameratarget";
		const string C_RES_PATH = "Assets/BuildAsset/Prefabs/CameraCtr.prefab";

		public const string Layer_Exhibit = "Exhibit";
		public const string Layer_Default = "Default";

		private float[] _orginValue;

		private GameObject _ctrObj;
		private GameObject _farCameraPoint;
		private Camera _camera;
		private CinemachineBrain _brain;
		private CinemachineVirtualCamera _vcamera;
		private CinemachineTransposer _body;

		private GameObject _orginPoint;

		private float presstime;
		private bool isUpdating;
		private bool isInited;
		private int cameraLayer;

		private Vector2 lastPos;

		public float rayHitMaxDistance = 50;

		/// <summary>
		/// 左右
		/// </summary>
		public CameraDataBind leftRight = new CameraDataBind();

		/// <summary>
		/// 视野
		/// </summary>
		public CameraDataBind fieldOfView = new CameraDataBind();

		public CameraDataBind xMove = new CameraDataBind();
		public CameraDataBind yMove = new CameraDataBind();
		public CameraDataBind zMove = new CameraDataBind();

		public bool disbaleControl = false;
		public bool disbaleTouch = false;

		private CameraDataBind sceneXMove;
		private CameraDataBind sceneZMove;
		private CameraDataBind sceneFOV;
		private bool focus = false;
		private bool aroundflag = false;
		private int handlerflag = 0;

		private RaycastHit[] hits = new RaycastHit[8];

		private void Start()
		{
			__inst = gameObject;
		}

		private void Update()
		{
			if (!isInited) return;
			UseBind(focus);
			focus = false;
		}


		private void LateUpdate()
		{
			if (!isInited) return;
			if (FairyGUI.Stage.isTouchOnUI || disbaleControl) return;
			if (TouchTrigger()) return;

			ControlAxis.Control(leftRight);
			ControlAxis.Control(fieldOfView);
			ControlAxis.Control(yMove);
			ControlAxis.Control(xMove);
			ControlAxis.Control(zMove);
		}

		public static void Reload(string path)
		{
			path = path ?? c_cpath;
			if (string.IsNullOrEmpty(path) || path == c_current) return;
			c_current = path;
			libx.Assets
				.LoadAsset(path, typeof(GameObject))
				.Wait((r) =>
				{
					if (string.IsNullOrEmpty(r.error))
					{
						if (__inst) Game.Destroy(__inst);
						var g = GameObject.Instantiate(r.asset) as GameObject;
						g.GetComponent<SceneCameraSystem>().Init();
						g.GetComponent<SceneCameraSystem>().Return();
					}
				});
		}


		public void LiveVCamera(ICinemachineCamera camera)
		{
			_brain?.IsLive(camera);
			if (camera is MonoBehaviour m)
			{
				m.enabled = false;
				m.enabled = true;
			}
		}

		public void LiveDefaultVCamera()
		{
			LiveVCamera(_vcamera);
		}

		public int Flag(int flag = 0)
		{
			if (flag != 0)
				handlerflag = flag;
			return handlerflag;
		}

		public void Focus(GameObject target, bool useY = true, int fov = -1, float time = 0)
		{
			if (target)
			{
				Focus(target.transform.position, target.transform.rotation.eulerAngles.y, useY, fov, time);
			}
		}

		public void Focus(Vector3 pos, float rot = 0, bool useY = true, int fov = -1, float time = 0)
		{
			if (!useY) pos.y = 0;
			handlerflag = 0;
			aroundflag = false;
			if (time < 0)
			{
				xMove.inputValue = xMove.currentValue = pos.x;
				yMove.inputValue = yMove.currentValue = pos.y;
				zMove.inputValue = zMove.currentValue = pos.z;
				if (rot != 0) leftRight.inputValue = leftRight.currentValue = rot % 360;
				fieldOfView.inputValue = fieldOfView.currentValue = fov >= 0 ? fov : fieldOfView.inputValue;
				UseBind(true);
				return;
			}

			xMove.inputValue = pos.x;
			yMove.inputValue = pos.y;
			zMove.inputValue = pos.z;
			if (rot != 0)
			{
				rot = rot % 360;
				var lrot = rot > 0 ? -360 + rot : 360 + rot;
				rot = Mathf.Abs(rot - leftRight.currentValue) > Mathf.Abs(lrot - leftRight.currentValue) ? lrot : rot;
				leftRight.inputValue = rot;
				leftRight.currentValue %= 360;
			}
			fieldOfView.inputValue = fov >= 0 ? fov : fieldOfView.inputValue;

			if (time > 0)
			{
				xMove.CalulateSpeed(xMove.inputValue, time, true);
				yMove.CalulateSpeed(yMove.inputValue, time, true);
				zMove.CalulateSpeed(zMove.inputValue, time, true);
				leftRight.CalulateSpeed(leftRight.inputValue, time, true);
				fieldOfView.CalulateSpeed(fieldOfView.inputValue, time, true);
			}
		}

		public void Return() => Return(0.5f);

		public void Return(float time)
		{
			disbaleControl = false;
			ToggleAxisLimit(true, 0);
			ToggleAxisLimit(true, 1);
			SetLayer();
			if (_orginPoint != null)
				Focus(_orginPoint, fov: C_DEF_FOV, time: time);
			else
			{
				Focus(
					_orginValue.GetVector3(),
					_orginValue.GetVal(3, 180),
					fov: (int)_orginValue.GetVal(4, C_DEF_FOV),
					time: time
				);
			}
		}

		public void Around(int circle = 1, int d = 4, int interval = 1, Action call = default)
		{
			var angle = 360 * circle;
			YieldInstruction wait = interval > 0 ? new WaitForSeconds(interval * 0.001f) : new WaitForEndOfFrame();
			aroundflag = true;
			disbaleControl = true;
			IEnumerator Rotation()
			{
				while (angle > 0 && aroundflag)
				{
					d = angle > d ? d : angle;
					angle -= d;
					leftRight.inputValue -= d;
					//leftRight.currentValue = leftRight.inputValue;
					yield return wait;
				}
				call?.Invoke();
			}
			StartCoroutine(Rotation());
		}

		public void Cache(out Vector3 pos, out float rot, out int fov)
		{
			pos = new Vector3(xMove.inputValue, yMove.inputValue, zMove.inputValue);
			rot = leftRight.inputValue;
			fov = (int)fieldOfView.inputValue;
		}

		public void PosX(float val, float time = 0)
		{
			xMove.inputValue = val;
			if (time > 0)
				xMove.CalulateSpeed(val, time, true);
		}

		public void PosY(float val, float time = 0)
		{
			yMove.inputValue = val;
			if (time > 0)
				yMove.CalulateSpeed(val, time, true);
		}

		public void PosZ(float val, float time = 0)
		{
			zMove.inputValue = val;
			if (time > 0)
				zMove.CalulateSpeed(val, time, true);
		}

		public void Fov(float val, float time = 0)
		{
			fieldOfView.inputValue = val;
			if (time > 0)
				fieldOfView.CalulateSpeed(val, time, true);
		}

		public void SwitchCameraMoveBind(bool isScene = true)
		{
			focus = true;
			if (isScene)
			{
				xMove = sceneXMove;
				zMove = sceneZMove;
				fieldOfView = sceneFOV;
			}
			else
				SwitchMoveAxis();
		}

		public void LimitX(bool limit, float min, float max, bool relative = false)
		{
			xMove.isUselimit = limit;
			xMove.minValue = relative ? xMove.currentValue + min : min;
			xMove.maxValue = relative ? xMove.currentValue + max : max;
		}

		public void LimitZ(bool limit, float min, float max, bool relative = false)
		{
			zMove.isUselimit = limit;
			zMove.minValue = relative ? zMove.currentValue + min : min;
			zMove.maxValue = relative ? zMove.currentValue + max : max;
		}

		public void ToggleAxisLimit(bool state, int axis = 0)
		{

			switch (axis)
			{
				case 0:
					xMove.AxisLimit(state, 0, 0, true);
					break;
				case 1:
					zMove.AxisLimit(state, 0, 0, true);
					break;
			}

		}

		public void CameraLayerMask(string layer, bool add = true)
		{
			if (_camera)
			{
				var layerInt = LayerMask.NameToLayer(layer);
				if (add)
					_camera.cullingMask |= 1 << layerInt;
				else
					_camera.cullingMask &= ~(1 << layerInt);
			}
		}

		public void ToggleExhibit(bool state)
		{
			CameraLayerMask(Layer_Default, !state);
			CameraLayerMask(Layer_Exhibit, state);
		}

		public void ToggleCamera(bool state)
		{
			if (_camera)
			{
				if (!state)
				{
					cameraLayer = _camera.cullingMask;
					_camera.cullingMask = 0;
				}
				else
				{
					if (cameraLayer == 0) return;
					_camera.cullingMask = cameraLayer;
					cameraLayer = 0;
				}
			}
		}

		public void SetDefaultBlendInfo(int time = 0)
		{
			SetBlendInfo(((int)CinemachineBlendDefinition.Style.EaseInOut), time);
		}

		public void SetBlendInfo(int style, int time)
		{
			if (_brain != null)
			{
				var blend = _brain.m_DefaultBlend;
				blend.m_Style = (CinemachineBlendDefinition.Style)style;
				blend.m_Time = time * 0.001f;
				_brain.m_DefaultBlend = blend;
			}
		}

		public void SetOrginVal(params float[] vals)
		{
			_orginValue = vals;
			_orginPoint = GameObject.Find("__center");
		}

		public Vector3 GetPointByDistance(float distance)
		{
			var op = _orginValue.GetVector3();
			var rot = Quaternion.Euler(0, _orginValue.GetVal(3, -30), 0);
			return op += rot * new Vector3(0, 0, distance);
		}

		public Vector3 GetOrginPos() => _orginValue.GetVector3();
		public float GetOrginRot() => _orginValue.GetVal(3, -30);
		public int GetOrginFOV() => (int)_orginValue.GetVal(4, C_DEF_FOV);


		void UseBind(bool force = false)
		{
			if (leftRight.isEnableChange || force)
				_ctrObj.transform.localRotation = Quaternion.Euler(0, leftRight.GetValue(), 0);

			if (fieldOfView.isEnableChange || force)
				UpdateFieldOfView();

			if (xMove.isEnableChange || yMove.isEnableChange || zMove.isEnableChange || force)
			{
				var old = _ctrObj.transform.position;
				old.x = xMove.isEnableChange ? xMove.GetValue() : old.x;
				old.y = yMove.isEnableChange ? yMove.GetValue() : old.y;
				old.z = zMove.isEnableChange ? zMove.GetValue() : old.z;
				_ctrObj.transform.position = old;
			}
		}

		bool TouchTrigger()
		{
			if (_camera == null || disbaleTouch) return false;
			var pos = Vector2.zero;
			var flag = false;
			var ret = false;
			if (Input.touchCount == 1)
			{
				var touch = Input.GetTouch(0);
				if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
					flag = true;

			}
			if (flag || Input.GetMouseButtonUp(0))
			{
				lastPos = pos = Input.touchCount == 1 ? Input.GetTouch(0).position : Input.mousePosition;
				var ray = _camera.ViewportPointToRay(_camera.ScreenToViewportPoint(pos));
				var count = 0;
				if ((count = Physics.RaycastNonAlloc(ray, hits, rayHitMaxDistance)) > 0)
				{
					count--;
					while (count >= 0)
					{
						var c = hits.Length <= count ? default : hits[count].collider;
						if (c != null)
						{
							var t = c.gameObject.GetComponents<ITouchOrHited>();
							if (t != null && t.Length > 0)
							{
								t.Foreach(v => v.OnClick());
								ret = ret || true;
								if (t[0] is ITrigger v && !v.isTrigger) break;
							}
						}
						count--;
					}
				}
			}
			ControlAxis.CheckAnyKeyInput();
			return ret;
		}

		void UpdateFieldOfView()
		{
			var v = fieldOfView.GetValue();
			_vcamera.m_Lens.FieldOfView = v;

			var off = fieldOfView.GetVector(true);
			_body.m_FollowOffset = off;
		}

		void Init()
		{
#if !DISABLE_CAMERA
			sceneXMove = xMove;
			sceneZMove = zMove;
			sceneFOV = fieldOfView;
			SetOrginVal(xMove.startValue, yMove.startValue, zMove.startValue, leftRight.startValue, fieldOfView.startValue);
			InitCameraBrain();
			CreateTarget();
			CreateVCamera();
			LiveVCamera(_vcamera);
			Return();
			isInited = true;
#endif

		}

		void InitCameraBrain()
		{
			_camera = Camera.main ?? GameObject.FindObjectOfType<Camera>();
			if (_camera != null)
			{
				_brain = _camera.gameObject.GetComponent<Cinemachine.CinemachineBrain>()
					?? _camera.gameObject.AddComponent<Cinemachine.CinemachineBrain>();
				SetDefaultBlendInfo();
			}
		}

		void CreateTarget()
		{
			_ctrObj = new GameObject(C_CTR_TAG);
			_ctrObj.transform.parent = this.transform;
			_ctrObj.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

			//_farCameraPoint = new GameObject("_far");
			//_farCameraPoint.transform.parent = this.transform;
			//_farCameraPoint.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
		}

		void CreateVCamera()
		{
			var c = transform.Find("__vcamrea")?.gameObject;
			if (c == null)
			{
				c = new GameObject("__vcamrea");
				c.transform.parent = this.transform;
				c.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
				_vcamera = c.AddComponent<Cinemachine.CinemachineVirtualCamera>();
				_vcamera.Follow = _ctrObj.transform;
				_vcamera.LookAt = _ctrObj.transform;
				_vcamera.m_Lens.FieldOfView = C_DEF_FOV;
				_vcamera.m_Lens.FarClipPlane = 100;


				var trans = _body = _vcamera.GetCinemachineComponent<CinemachineTransposer>() ?? _vcamera.AddCinemachineComponent<CinemachineTransposer>();
				trans.m_BindingMode = CinemachineTransposer.BindingMode.LockToTargetNoRoll;
				trans.m_FollowOffset = new Vector3(0, 110, -120);
				trans.m_XDamping = 0;
				trans.m_YDamping = 0;
				trans.m_ZDamping = 0;

				var aim = _vcamera.GetCinemachineComponent<CinemachineComposer>() ?? _vcamera.AddCinemachineComponent<CinemachineComposer>();
				aim.m_HorizontalDamping = 0;
				aim.m_VerticalDamping = 0;
			}
			else
				_vcamera = c.GetOrAddComponent<Cinemachine.CinemachineVirtualCamera>();
			_vcamera.Follow = _ctrObj.transform;
			_vcamera.LookAt = _ctrObj.transform;
			_vcamera.m_Lens.OrthographicSize = 0.56f / (1f * Screen.width / Screen.height) * _vcamera.m_Lens.OrthographicSize;

		}

		void SwitchMoveAxis()
		{
			xMove = JsonUtility.FromJson<CameraDataBind>(JsonUtility.ToJson(xMove));
			zMove = JsonUtility.FromJson<CameraDataBind>(JsonUtility.ToJson(zMove));
			fieldOfView = JsonUtility.FromJson<CameraDataBind>(JsonUtility.ToJson(fieldOfView));
			xMove.control?.ForEach(c => c.limit = false);
			zMove.control?.ForEach(c => c.limit = false);
		}

		public void SetLayer()
		{
			CameraLayerMask("redpoint", true);
		}

#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			Gizmos.DrawLine(Camera.main.transform.position, Camera.main.transform.position + Quaternion.Euler(0, 5, 0) * Camera.main.transform.forward * rayHitMaxDistance);
			Gizmos.DrawLine(Camera.main.transform.position, Camera.main.transform.position + Quaternion.Euler(0, -5, 0) * Camera.main.transform.forward * rayHitMaxDistance);
			Gizmos.DrawLine(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward * rayHitMaxDistance);

			/*if (lastPos != Vector2.zero)
			{
				var ray = Camera.main.ScreenPointToRay(lastPos);
				Gizmos.DrawLine(ray.origin, ray.GetPoint(50));
			}*/
		}
#endif

	}
}
