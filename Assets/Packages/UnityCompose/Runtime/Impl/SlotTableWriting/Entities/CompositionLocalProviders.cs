namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;

internal interface ICompositionLocalProviders
{
    void Apply(CompositionLocalMap map);
}

internal class CompositionLocalProviders<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : ICompositionLocalProviders
{
    private CompositionLocalProvides<T1> _provides1;
    private CompositionLocalProvides<T2> _provides2;
    private CompositionLocalProvides<T3> _provides3;
    private CompositionLocalProvides<T4> _provides4;
    private CompositionLocalProvides<T5> _provides5;
    private CompositionLocalProvides<T6> _provides6;
    private CompositionLocalProvides<T7> _provides7;
    private CompositionLocalProvides<T8> _provides8;
    private CompositionLocalProvides<T9> _provides9;
    private CompositionLocalProvides<T10> _provides10;

    public void Update(
        CompositionLocalProvides<T1> provides1,
        CompositionLocalProvides<T2> provides2 = default,
        CompositionLocalProvides<T3> provides3 = default,
        CompositionLocalProvides<T4> provides4 = default,
        CompositionLocalProvides<T5> provides5 = default,
        CompositionLocalProvides<T6> provides6 = default,
        CompositionLocalProvides<T7> provides7 = default,
        CompositionLocalProvides<T8> provides8 = default,
        CompositionLocalProvides<T9> provides9 = default,
        CompositionLocalProvides<T10> provides10 = default
    )
    {
        _provides1 = provides1;
        _provides2 = provides2;
        _provides3 = provides3;
        _provides4 = provides4;
        _provides5 = provides5;
        _provides6 = provides6;
        _provides7 = provides7;
        _provides8 = provides8;
        _provides9 = provides9;
        _provides10 = provides10;
    }
    
    public void Apply(CompositionLocalMap map)
    {
        if (_provides1.CompositionLocal == null)
            return;
        map.Set(_provides1.CompositionLocal, _provides1.Value);
        
        if (_provides2.CompositionLocal == null)
            return;
        map.Set(_provides2.CompositionLocal, _provides2.Value);
        
        if (_provides3.CompositionLocal == null)
            return;
        map.Set(_provides3.CompositionLocal, _provides3.Value);
        
        if (_provides4.CompositionLocal == null)
            return;
        map.Set(_provides4.CompositionLocal, _provides4.Value);
        
        if (_provides5.CompositionLocal == null)
            return;
        map.Set(_provides5.CompositionLocal, _provides5.Value);
        
        if (_provides6.CompositionLocal == null)
            return;
        map.Set(_provides6.CompositionLocal, _provides6.Value);
        
        if (_provides7.CompositionLocal == null)
            return;
        map.Set(_provides7.CompositionLocal, _provides7.Value);
        
        if (_provides8.CompositionLocal == null)
            return;
        map.Set(_provides8.CompositionLocal, _provides8.Value);
        
        if (_provides9.CompositionLocal == null)
            return;
        map.Set(_provides9.CompositionLocal, _provides9.Value);
        
        if (_provides10.CompositionLocal == null)
            return;
        map.Set(_provides10.CompositionLocal, _provides10.Value);
    }
}