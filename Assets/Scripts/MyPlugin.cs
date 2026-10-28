using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct Vector2D
{
    public float x;
    public float y;
}
public static class MyPlugin
{
    [DllImport("DLL_GDWMARIOGAME")]
    private static extern int GetID();

    [DllImport("DLL_GDWMARIOGAME")]
    private static extern void SetID(int id);

    [DllImport("DLL_GDWMARIOGAME")]
    private static extern Vector2D GetPosition();

    [DllImport("DLL_GDWMARIOGAME")]
    private static extern Vector2D SetPosition(float x, float y);


    public static int ID
    {
        get { return GetID(); }
        set { SetID(value); }
    }

    public static Vector2D Position
    {
        get { return GetPosition(); }
    }
}