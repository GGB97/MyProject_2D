using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletoneBase<GameManager>
{
    public int healthID;

    public Player player;

    private void Awake()
    {
        healthID = 0;
    }

}