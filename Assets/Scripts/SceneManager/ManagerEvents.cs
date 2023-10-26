using System;

public static class ManagerEvents 
{
    public static Action OnGameOver;

    public static Action OnRemoveTorus;

    public static void SetGameOver()
    {
        OnGameOver?.Invoke();
    }

    public static void SetRemoveTorus()
    {
        OnRemoveTorus?.Invoke();
    }
}
