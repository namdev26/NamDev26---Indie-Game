using UnityEngine;

public class PlayerInputHandler
{
    public Vector2Int ReadMoveInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) return Vector2Int.right;
        if (Input.GetKeyDown(KeyCode.DownArrow)) return Vector2Int.left;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) return Vector2Int.up;
        if (Input.GetKeyDown(KeyCode.RightArrow)) return Vector2Int.down;
        return Vector2Int.zero;
    }

    //public bool ReadJumpInput()
    //{
    //    return Input.GetKeyDown(KeyCode.Space);
    //}
}
