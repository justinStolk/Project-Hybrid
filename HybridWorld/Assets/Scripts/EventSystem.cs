using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType { ON_PLAYER_DAMAGED }

public static class EventSystem
{

    private static Dictionary<EventType, Action> events = new();
    public static void Subscribe(EventType eventType, Action subscribedAction)
    {
        if (!events.ContainsKey(eventType))
        {
            events.Add(eventType, null);
        }
        events[eventType] += subscribedAction;
    }
    public static void CallEvent(EventType calledEvent)
    {
        events[calledEvent]?.Invoke();
    }
    public static void Unsubscribe(EventType eventType, Action unsubscribedAction)
    {
        if (events.ContainsKey(eventType) && events[eventType] != null)
        {
            events[eventType] -= unsubscribedAction;
        }

    }
}
