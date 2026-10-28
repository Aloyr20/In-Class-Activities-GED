#include "Wrapper.h"
#include "GameObject.h"

GameObject gameObject;

PLUGIN_API int GetID()
{
    return gameObject.GetID();
}

PLUGIN_API void SetID(int id)
{
    gameObject.SetID(id);
}

PLUGIN_API Vector2D GetPosition()
{
    return gameObject.GetPosition();
}

PLUGIN_API void SetPosition(float x, float y)
{
    gameObject.SetPosition(x, y);
}

