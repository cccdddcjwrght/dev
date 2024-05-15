using System.Diagnostics;
using UnityEngine.Profiling;

public class GameProfiler 
{
    [Conditional("GAME_PROFILER")]
    public static void BeginSample(string name)
    {
        Profiler.BeginSample(name);
    }
    
    [Conditional("GAME_PROFILER")]
    public static void EndSample()
    {
        Profiler.EndSample();
    }
}
