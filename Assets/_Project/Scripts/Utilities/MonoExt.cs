using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;


//An extension of the mono behaviour script, this will handle auto disposable of events
public class MonoExt : SerializedMonoBehaviour
{
    protected List<IDisposable> disposables = null;
    
    //Method to override to initialize data 
    public virtual void Initialize()
    {
        disposables = new List<IDisposable>();
    }
    
    //Method that handles disposal of events
    public virtual void Dispose()
    {
        disposables?.ForEach(disposable => disposable?.Dispose());
        disposables?.Clear();
    }

    //Method to override to set subscription using UniRX
    public virtual void OnSubscriptionSet()
    {

    }

    //Method to use to handle addition of events
    protected void AddEvent<T>(Subject<T> subject, Action<T> action)
    {
        subject.Subscribe(action).AddTo(disposables);
    }

    private void OnDestroy()
    {
        Dispose();
    }
}
