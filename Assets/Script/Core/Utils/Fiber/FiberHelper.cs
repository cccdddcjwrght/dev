using UnityEngine;
using System.Collections;
using Fibers;

public class FiberHelper
{
	//Parallel
	public static IEnumerator RunParallel(params IEnumerator[] enums) {
		
		Fiber[] fibers = new Fiber[enums.Length];
		
		for (int i = 0; i < enums.Length; i++) {
			fibers[i] = new Fiber(enums[i], FiberBucket.Manual);	
		}
		
		yield return new Fibers.Fiber.OnExit(delegate {
			for (int i = 0; i < enums.Length; i++) {
				fibers[i].Terminate();
			}	
		});
		
		bool finished = false;
		while (!finished) {
			finished = true;
			for (int i = 0; i < enums.Length; i++) {
				if (fibers[i].Step()) finished = false;
			}
			yield return null;
		}
	}

	[System.Obsolete("Use RunParallel() instead. Phasing out confusing names.")]
	public static IEnumerator Compound(params IEnumerator[] enums) {
		return RunParallel(enums);
	}

	//Serial
	public static IEnumerator RunSerial(params IEnumerator[] enums) {

		Fiber[] fibers = new Fiber[enums.Length];
		
		for (int i = 0; i < enums.Length; i++) {
			fibers[i] = new Fiber(enums[i], FiberBucket.Manual);	
		}
		
		yield return new Fibers.Fiber.OnExit(delegate {
			for (int i = 0; i < enums.Length; i++) {
				fibers[i].Terminate();
			}	
		});
		
		for (int i = 0; i < enums.Length; i++) {
			while (fibers[i].Step()) yield return null;
		}
	}

	[System.Obsolete("Use RunSerial() instead. Phasing out confusing names.")]
	public static IEnumerator Combine(params IEnumerator[] enums) {
		return RunSerial(enums);
	}

	
	public static IEnumerator Wait(float duration) {
		while (duration>0) {
			yield return null;
			duration-=Time.deltaTime;
		}	
	}
}

