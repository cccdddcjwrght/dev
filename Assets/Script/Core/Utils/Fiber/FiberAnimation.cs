using UnityEngine;
using System.Collections;

public static class FiberAnimation
{
	private static float Interpolate(float value, AnimationCurve curve) {
		return (curve == null || curve.length==0) ? value : curve.Evaluate(value);	
	}
	
	public static Vector3 LerpNoClamp(Vector3 s, Vector3 d, float t) {
		return new Vector3(
			s.x + (d.x - s.x) * t,
			s.y + (d.y - s.y) * t,
			s.z + (d.z - s.z) * t
			);
	}

	/// <summary>
	/// Make animation driven by fiber. Any positive duration overwrites the duration of the curve. 
	/// A duration of 0 lets the curve dictate actual duration.
	/// </summary>
	/// <param name="duration">Duration.</param>
	/// <param name="curve">Curve.</param>
	/// <param name="lerpFunc">Callback getting the progression of the animation. Typically does lerps. </param>
	public static IEnumerator Animate(float duration,AnimationCurve curve, System.Action<float> lerpFunc) {
		float timer = 0;
		float curveDuration = 1;
		if( curve != null && curve.length>0) {
			curveDuration = curve.keys[curve.length-1].time;
			if( duration <= 0 ){
				duration = curveDuration;
 			}
		}
		else {
			if(duration<=0) duration = 0.25f;
		}

		while (true) {
			float whereOnCurve = Mathf.Clamp01(timer/duration) * curveDuration;
			float progress = Interpolate(whereOnCurve,curve);
			lerpFunc(progress);
			if (timer>duration) break;
			timer += Time.deltaTime;
			yield return null;
        }
	}

	public static IEnumerator Animate(float duration, System.Action<float> lerpFunc) {
		return Animate (duration,null,lerpFunc);
	}

	public static IEnumerator ScaleTransform(Transform transform, Vector3 sourceScale, Vector3 destScale, AnimationCurve curve, float duration) {
		yield return Animate (duration,curve,(lerpFactor)=>{
			transform.localScale = LerpNoClamp(sourceScale, destScale, lerpFactor);
		});
	}

	public static IEnumerator MoveTransform(Transform transform, Vector3 sourcePosition, Vector3 destPosition, AnimationCurve curve, float duration) {
		yield return Animate(duration,curve,(lerpFactor) => {
			transform.position = LerpNoClamp(sourcePosition, destPosition, lerpFactor);
		});
	}	

	public static IEnumerator MoveLocalTransform(Transform transform, Vector3 sourcePosition, Vector3 destPosition, AnimationCurve curve, float duration) {
		yield return Animate(duration,curve,(lerpFactor) => {
			transform.localPosition = LerpNoClamp(sourcePosition, destPosition, lerpFactor);
		});
	}	

	public static IEnumerator RotateTransform(Transform transform, Vector3 sourceAngles, Vector3 destAngles, AnimationCurve curve, float duration) {
		yield return Animate(duration,curve,(lerpFactor) => {
			transform.localRotation = Quaternion.Euler( LerpNoClamp(sourceAngles, destAngles, Interpolate(lerpFactor, curve)));
		});
	}	
	
	public static IEnumerator RotateTransform(Transform transform, Quaternion source, Quaternion dest, AnimationCurve curve, float duration) {
		yield return Animate(duration,curve,(lerpFactor) => {
			transform.localRotation = Quaternion.Slerp(source, dest, Interpolate(lerpFactor, curve));
		});
	}

}

