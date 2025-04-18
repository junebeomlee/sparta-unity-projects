using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Idle,
    Detected,
    Attacking
}

public abstract class StateMachine: MonoBehaviour
{
    public abstract List<State> states { get; }

    public IBehaviour currentBehaviour;
    
    //
    // public void GetMoveableTile()
    // {
    //     while (moveableTransforms.Count > 0)
    //     {
    //         Debug.Log(1);
    //
    //         var (remainMovePoint, currentPos) = moveableTransforms.Dequeue();
    //
    //         _arrowDirections.ForEach(arrow =>
    //         {
    //
    //             var nextPos = new Vector2Int(currentPos.x + arrow.x, currentPos.y + arrow.y);
    //
    //             // fix: new Vector 라서 존재하는 값으로 인식하지 않음
    //             // 중복 체크 : 이 타일이 이미 추가되어 있어도 그 다음 타일 조사가 필요한 상황이 발생함
    //             if (visited.Contains(nextPos)) return;
    //
    //             // if (remainMovePoint != 1)
    //             // {
    //             //     var tile = _mapGeneratorSystem.GetTileByCoord(selectedPosition.x, selectedPosition.y);
    //             //     if (!tile) return;
    //             //
    //             //     cost = remainMovePoint - tile.GetComponent<TileBlock>().movingCost;
    //             //     GetMoveableTile(cost, selectedPosition);
    //             //
    //             // }
    //             // return;
    //
    //             var curTile = _mapGeneratorSystem.GetTileByCoord(nextPos.x, nextPos.y);
    //             if (!curTile) return; // 타일이 없으면 스킵
    //
    //             int cost = curTile.GetComponent<TileBlock>().movingCost;
    //             int nextMovePoint = remainMovePoint - cost;
    //
    //             if (nextMovePoint >= 0)
    //             {
    //                 moveableTransforms.Enqueue((nextMovePoint, nextPos));
    //                 visited.Add(nextPos);
    //                 curTile.GetComponent<Renderer>().material.color = Color.green;
    //
    //
    //             }
    //         });
    //     }
    // }
}