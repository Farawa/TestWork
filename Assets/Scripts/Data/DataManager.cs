using UnityEngine;
public static class DataManager
{
    private static Data data;
    public static Data Data
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
