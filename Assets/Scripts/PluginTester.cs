using UnityEngine;

public class PluginTester : MonoBehaviour
{
    
    void Start()
    {
        MyPlugin.ID = 10;
        Debug.Log(MyPlugin.ID);
    }

}
