using System;

/*  This script demonstrates implementation of the Observer Pattern.
 *  If you're interested in learning about Observer Pattern, 
 *  you can find a dedicated course on Outscal's website.
 *  Link: https://outscal.com/lms/course/horror-escape-game-using-observer-pattern-using-unity/module/6465e1084c39da1772f30cf9?chapterId=6465e17f4c39da1772f30d09&materialId=6465e3374c39da1772f31bc3
 * */

namespace ServiceLocator.Events
{
    public class GameEventController<T>
    {
        public event Action<T> baseEvent;
        public void InvokeEvent(T type) => baseEvent?.Invoke(type);
        public void AddListener(Action<T> listener) => baseEvent += listener;
        public void RemoveListener(Action<T> listener) => baseEvent -= listener;
    }

    public class GameEventController
    {
        public event Action baseEvent;
        public void InvokeEvent() => baseEvent?.Invoke();
        public void AddListener(Action listener) => baseEvent += listener;
        public void RemoveListener(Action listener) => baseEvent -= listener;

    }
}