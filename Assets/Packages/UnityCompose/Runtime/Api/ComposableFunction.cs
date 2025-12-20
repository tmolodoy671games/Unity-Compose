// ReSharper disable once CheckNamespace
namespace UnityCompose;

[Composable]
public delegate R ComposableFunc<out R>();

[Composable]
public delegate R ComposableFunc<in T1, out R>(T1 arg1);

[Composable]
public delegate R ComposableFunc<in T1, in T2, out R>(T1 arg1, T2 arg2);

[Composable]
public delegate R ComposableFunc<in T1, in T2, in T3, out R>(T1 arg1, T2 arg2, T3 arg3);

[Composable]
public delegate R ComposableFunc<in T1, in T2, in T3, in T4, out R>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);