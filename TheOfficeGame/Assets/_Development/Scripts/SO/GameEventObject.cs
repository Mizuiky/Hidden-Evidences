using System.Collections.Generic;
using UnityEngine;
using System;

namespace Test.Event
{
    [CreateAssetMenu(menuName = "Data/EventObject")]
    public class GameEventObject : ScriptableObject
    {
        private List<Action<object[]>> _events = new List<Action<object[]>>();

        public void Invoke(params object[] args)
        {
            foreach (var action in _events)
            {
                action?.Invoke(args);
            }
        }

        public void Subscribe(Action<object[]> action) => _events.Add(action);

        public void Unsubscribe(Action<object[]> action) => _events.Remove(action);
    }
}