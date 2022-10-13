using System.Collections.Generic;

public static class MessagesManager
{
    private static List<IListener> listeners = new List<IListener>();

    public static void SendMessage(Message message)
    {
        foreach(var lis in listeners)
        {
            lis.GetMessage(message);
        }
    }

    public static void Follow(IListener listener)
    {
        listeners.Add(listener);
    }

    public static void UnFollow(IListener listener)
    {
        listeners.Remove(listener);
    }
}
