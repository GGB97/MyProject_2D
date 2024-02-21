using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerGroundData
{
    [field: SerializeField][field: Range(0, 25f)] public float BaseSpeed { get; private set; } = 2f;

    [field : Header("Move Data")]
    [field: SerializeField][field: Range(0, 5f)] public float MoveSpeedModifier { get; private set; } = 1f;
}
