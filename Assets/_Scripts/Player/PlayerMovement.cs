using UnityEngine;

public class PlayerMovement
{
    private readonly GridManager _gridManager;
    private readonly Transform _playerTransform;

    public bool IsMoving { get; private set; }
    public Vector2Int CurrentGridPos { get; private set; }
    private Vector3 _targetWorldPos;
    private readonly float _moveSpeed;

    public PlayerMovement(GridManager gridManager, Transform playerTransform, Vector2Int startPos, float moveSpeed)
    {
        _gridManager = gridManager;
        _playerTransform = playerTransform;
        _moveSpeed = moveSpeed;
        CurrentGridPos = startPos;

        _targetWorldPos = GridToWorld(CurrentGridPos);
        _playerTransform.position = _targetWorldPos;
    }

    public bool TryMove(Vector2Int direction)
    {
        if (IsMoving) return false;

        Vector2Int newPos = CurrentGridPos + direction;
        if (!IsInsideGrid(newPos)) return false;

        CurrentGridPos = newPos;
        _targetWorldPos = GridToWorld(CurrentGridPos);
        IsMoving = true;

        // Quay mặt hướng di chuyển
        Vector3 dir = _targetWorldPos - _playerTransform.position;
        dir.y = 0;
        if (dir != Vector3.zero)
            _playerTransform.forward = dir.normalized;

        return true;
    }

    public void UpdateMovement(float deltaTime)
    {
        if (!IsMoving) return;

        _playerTransform.position = Vector3.MoveTowards(
            _playerTransform.position, _targetWorldPos, _moveSpeed * deltaTime);

        if (Vector3.Distance(_playerTransform.position, _targetWorldPos) < 0.001f)
        {
            _playerTransform.position = _targetWorldPos;
            IsMoving = false;
        }
    }

    private bool IsInsideGrid(Vector2Int gridPos)
    {
        return gridPos.x >= 0 && gridPos.x < _gridManager.GetGridWidth()
            && gridPos.y >= 0 && gridPos.y < _gridManager.GetGridHeight();
    }

    private Vector3 GridToWorld(Vector2Int gridPos)
    {
        float total = _gridManager.GetCellSize() + _gridManager.GetSpacing();
        Vector3 origin = _gridManager.GetGridOrigin();

        return new Vector3(
            gridPos.x * total + origin.x,
            0f,
            gridPos.y * total + origin.z);
    }
}
