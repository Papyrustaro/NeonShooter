using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

/// <summary>
/// 特定の軌道を等速直線運動する
/// </summary>
public class GoAndBackPathMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    /// <summary>
    /// パスひとつひとつの移動量(今のパスから次のパスまでの移動量)
    /// </summary>
    [SerializeField] private Vector2[] amountOfMovement;
    private Sequence _sequence;

    private void Start()
    {
        _sequence = DOTween.Sequence()
            .SetRelative()
            .SetLink(this.gameObject)
            .SetLoops(-1);
        foreach(Vector2 p in this.amountOfMovement.Concat(this.amountOfMovement.Reverse().Select(v => v * -1f)))
        {
            _sequence.Append(this.transform.DOMove(p, p.magnitude / this.moveSpeed));
        }
        _sequence.Play();
    }
}
