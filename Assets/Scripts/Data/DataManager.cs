using UnityEngine;
public static class DataManager
{
    private static Data data;
    public static Data DataSO
    {
        get
        {
            if (!data)
            {
                data = Resources.Load("Data") as Data;
            }
            return data;
        }
    }
}
