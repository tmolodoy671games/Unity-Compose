using Compose.Net;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters;

internal class ComposeLoggerImpl : IComposeLogger
{
    public void Debug(object? message)
    {
        UnityEngine.Debug.Log(message);
    }
}