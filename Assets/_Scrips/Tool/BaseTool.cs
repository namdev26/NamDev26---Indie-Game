using UnityEngine;

public abstract class BaseTool
{
    public virtual string ToolName => "Base Tool";

    public virtual void OnToolSelected() { }
    public virtual void OnToolDeselected() { }

    public virtual void OnPointerDown(Vector3 worldPos) { }
    public virtual void OnPointerHold(Vector3 worldPos) { }
    public virtual void OnPointerUp(Vector3 worldPos) { }

    public virtual bool CanUseOn(TileData tile) => true;
}
