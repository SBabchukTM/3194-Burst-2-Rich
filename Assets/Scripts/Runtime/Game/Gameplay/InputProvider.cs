using System;
using Runtime.Game.Tools;
using UnityEngine;
using Zenject;

public class InputProvider : ITickable, IInitializable
{
    private Camera _camera;
    private bool _enabled;
    
    public event Action<Vector3> OnPlayerTap;

    public void Initialize()
    {
        _camera = Camera.main;
    }

    public void Tick()
    {
        if(!_enabled)
            return;
        
        if(Input.touchCount == 0)
            return;
        
        var touch = Input.GetTouch(0);
        
        if(touch.phase != TouchPhase.Began)
            return;
        
        if(Tools.IsPointerOverUIElement())
            return;
        
        OnPlayerTap?.Invoke(GetTouchWorldPos(touch.position));
    }

    public void Enable(bool enable) => _enabled = enable;

    private Vector3 GetTouchWorldPos(Vector3 screenPos)
    {
        var pos = _camera.ScreenToWorldPoint(screenPos);
        pos.z = 0;
        return pos;
    }
}
