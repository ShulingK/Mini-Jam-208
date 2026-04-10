using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    private List<IObserver> _observers = new List<IObserver>();

    public void AddObserver(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }

    #region Notify
    protected void NotifyObserver()
    {
        _observers.ForEach(observer => observer.OnNotify());
    }
    
    protected void NotifyObserver(int notification)
    {
        _observers.ForEach(observer => observer.OnNotify(notification));
    }
    
    protected void NotifyObserver(float notification)
    {
        _observers.ForEach(observer => observer.OnNotify(notification));
    }
    
    protected void NotifyObserver(bool notification)
    {
        _observers.ForEach(observer => observer.OnNotify(notification));
    }
    #endregion
}
