using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterPlayer player;
    private Rigidbody2D rigidBody2D;
    private Animator animator;

	// Use this for initialization
	void Start ()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GetComponent<CharacterPlayer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.Attack();
        }
        else
        {
            var horiontal = calc(Input.GetAxis("Horizontal"));
            var vertical = calc(Input.GetAxis("Vertical"));
            if (horiontal != 0) transform.localScale = new Vector2(horiontal * 5, 5);
            var move = new Vector2(horiontal, vertical) * player.Speed;
            rigidBody2D.MovePosition((Vector2)transform.position + move);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            player.ChangeCharacterType(PlayerType.Fire);
        }

        if(Input.GetKeyDown(KeyCode.B))
        {
            player.ChangeCharacterType(PlayerType.Water);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            player.ChangeCharacterType(PlayerType.Wind);
        }
    }

    private int calc(float f)
    {
        if(f > 0)
        {
            return 1;
        }
        else
        {
            if(f < 0)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}

public enum PlayerType
{
    Fire,
    Water,
    Wind,
    Thunder
}
