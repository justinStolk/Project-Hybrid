using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType 
    { 
    ON_PLAYER_DAMAGED, 
    ON_PUZZLE_CLEARED, 
    ON_PUZZLE_ERROR,
    ON_MOUSESENSITIVITY_CHANGED
    }

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

public static class FloatEventSystem
{
    private static Dictionary<EventType, Action<float>> events = new();
    public static void Subscribe(EventType eventType, Action<float> subscribedAction)
    {
        if (!events.ContainsKey(eventType))
        {
            events.Add(eventType, null);
        }
        events[eventType] += subscribedAction;
    }
    public static void CallEvent(EventType calledEvent, float eventValue)
    {
        events[calledEvent]?.Invoke(eventValue);
    }
    public static void Unsubscribe(EventType eventType, Action<float> unsubscribedAction)
    {
        if (events.ContainsKey(eventType) && events[eventType] != null)
        {
            events[eventType] -= unsubscribedAction;
        }
    }
}
