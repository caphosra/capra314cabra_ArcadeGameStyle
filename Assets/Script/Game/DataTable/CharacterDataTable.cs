using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyGame/Create CharacterDataTable", fileName = "CharacterDataTable")]
public class CharacterDataTable : ScriptableObject
{
    [SerializeField]
    private int m_MaxHP;
    public int MaxHP { get { return m_MaxHP; } }

    [SerializeField]
    private float m_Speed;
    public float Speed { get { return m_Speed; } }

    [SerializeField]
    private float m_FreezeTime;
    public float FreezeTime { get { return m_FreezeTime; } }

    [SerializeField]
    private int m_Score;
    public int Score { get { return m_Score; } }
}
