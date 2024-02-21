using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAirData 
{
    [field: Header("Jump Data")]
    [field: SerializeField][field: Range(0f, 50f)] public float JumpForce { get; private set; } = 25f;
}
