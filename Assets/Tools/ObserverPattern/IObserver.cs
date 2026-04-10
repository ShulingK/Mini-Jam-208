// Interface
using UnityEngine;

public interface IObserver {

    public void OnNotify();
    public void OnNotify(int notification);
    public void OnNotify(float notification);
    public void OnNotify(bool notification);

}
