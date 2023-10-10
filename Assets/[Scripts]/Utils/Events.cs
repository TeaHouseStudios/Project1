using System;
using UnityEngine;

public static class Events
{
    //public static readonly Evt<GameObject> onRoomHover = new Evt<GameObject>(); 
    //public static readonly Evt onNoRoomHover = new Evt();
    //public static readonly Evt<Table, TableState> onTableStateChanged = new Evt<Table, TableState>();
    public static readonly Evt<int> onReceiverActivated = new Evt<int>();
    public static readonly Evt<int> onReceiverDeactivated = new Evt<int>();

    public static readonly Evt<int> onPcActivated = new Evt<int>();
    public static readonly Evt<int> onPcDeactivated = new Evt<int>();

    public static readonly Evt<int> onSwitch = new Evt<int>();

    public static readonly Evt<float> onLevelEnd = new Evt<float>();

    public static readonly Evt<GameObject, int> onVentEnter = new Evt<GameObject, int>();

    public static readonly Evt<GameObject> onTeleportThrown = new Evt<GameObject>();
    public static readonly Evt onCharacterTP = new Evt();
}

public class Evt
{
    private event Action action = delegate { };

    public void Invoke()
    {
        action.Invoke();
    }

    public void AddListener(Action listener)
    {
        action += listener;
    }

    public void RemoveListener(Action listener)
    {
        action -= listener;
    }
}

public class Evt<T>
{
    private event Action<T> action = delegate { };

    public void Invoke(T param)
    {
        action.Invoke(param);
    }

    public void AddListener(Action<T> listener)
    {
        action += listener;
    }

    public void RemoveListener(Action<T> listener)
    {
        action -= listener;
    }
}

public class Evt<T, T2>
{
    private event Action<T, T2> action = delegate { };

    public void Invoke(T param, T2 param2)
    {
        action.Invoke(param, param2);
    }

    public void AddListener(Action<T, T2> listener)
    {
        action += listener;
    }

    public void RemoveListener(Action<T, T2> listener)
    {
        action -= listener;
    }
}

