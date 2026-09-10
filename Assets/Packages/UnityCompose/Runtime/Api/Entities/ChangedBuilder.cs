// ReSharper disable CheckNamespace
namespace UnityCompose;

public struct ChangedBuilder
{
    private bool _changed;
    private readonly Composer _composer;

    public ChangedBuilder(Composer composer)
    {
        _composer = composer;
        _changed = false;
    }
    
    public ChangedBuilder Changed()
    {
        _changed = _composer.Changed();
        return this;
    }

    public ChangedBuilder Changed<T>(T? value)
    {
        if (!_changed)
            _changed = _composer.Changed(value);
        else
            _composer.Write(value);
        return this;
    }
    
    public ChangedBuilder ChangedAsFlag(bool changed)
    {
        if (!_changed)
            _changed = changed;
        return this;
    }
    
    public bool Get() => _changed;
}