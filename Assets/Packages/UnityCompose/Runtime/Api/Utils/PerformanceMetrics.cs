// ReSharper disable CheckNamespace

using System;
using SharpExtensions;

namespace UnityCompose;

public static class PerformanceMetrics
{
    private static float Invalidate = 0f;
    private static float Try = 0f;
    private static float BeginComposeGroup = 0f;
    private static float ReusableComposeView = 0f;
    private static float EndComposeGroup = 0f;
    private static float Remember = 0f;

    private static float Body => Try - BeginComposeGroup - EndComposeGroup;
    private static float Other => Invalidate - Try;
    
    public static void MeasureInvalidate(Action body)
    {
        Invalidate += TimeUtils.Measure(body).TotalSeconds.ToFloat();
    }

    public static void MeasureTry(Action body)
    {
        Try += TimeUtils.Measure(body).TotalSeconds.ToFloat();
    }

    public static bool MeasureBeginComposeGroup(Func<bool> body)
    {
        var result = TimeUtils.Measure(body);
        BeginComposeGroup += result.Duration.TotalSeconds.ToFloat();
        return result.Result;
    }
    
    public static void MeasureReusableComposeView(Action body)
    {
        ReusableComposeView += TimeUtils.Measure(body).TotalSeconds.ToFloat();
    }
    
    public static void MeasureEndComposeGroup(Action body)
    {
        EndComposeGroup += TimeUtils.Measure(body).TotalSeconds.ToFloat();
    }
    
    public static T MeasureRemember<T>(Func<T> body)
    {
        var result = TimeUtils.Measure(body);
        Remember += result.Duration.TotalSeconds.ToFloat();
        return result.Result;
    }

    public static string Format()
    {
        return $"Invalidate={Invalidate}\n" +
               $"\tTry={Try}\n" +
               $"\t\tBeginComposeGroup={BeginComposeGroup}\n" + 
               $"\t\tBody={Body}\n" +
               $"\t\t\tReusableComposeView={ReusableComposeView}\n" +
               $"\t\tEndComposeGroup={EndComposeGroup}\n" +
               $"\tOther={Other}\n" +
               $"Remember={Remember}";
    }
}