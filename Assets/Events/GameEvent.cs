using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{

    public List<GameEventListener> gameEventListeners;

    public void OnRaise()
    {
        for (int i = gameEventListeners.Count - 1; i >= 0; i--)
        {
            gameEventListeners[i].OnEventRaised();
        }

    }

    public void RegisterListener(GameEventListener listener)
    {
        gameEventListeners.Add(listener);
    }
    public void UnRegisterListener(GameEventListener listener)
    {
        gameEventListeners.Remove(listener);
    }
}
