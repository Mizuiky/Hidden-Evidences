using UnityEngine;
using System;
using System.Collections.Generic;

namespace Test.Event
{
    [CreateAssetMenu(menuName = "Data/Event")]
    public class GameEvent : ScriptableObject
    {
        private List<Action> _events = new List<Action>();

        public void InvokeEvent()
        {
            foreach (var action in _events)
            {
                action?.Invoke();
            }            
        }

        public void Subscribe(Action action) => _events.Add(action);
        public void Unsubscribe(Action action) => _events.Remove(action);
    }
}