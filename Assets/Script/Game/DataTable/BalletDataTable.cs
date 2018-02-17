using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyGame/Create BalletDataTable", fileName = "BalletDataTable")]
public class BalletDataTable : ScriptableObject
{
    [SerializeField]
    private float m_Speed;
    public float Speed { get { return m_Speed; } }

    [SerializeField]
    private int m_Damege;
    public int Damege { get { return m_Damege; } }

    [SerializeField]
    private AttackType m_AttackType;
    public AttackType AttackType { get { return m_AttackType; } }

    [SerializeField]
    private CharacterStatus m_AddStatus;
    public CharacterStatus AddStatus { get { return m_AddStatus; } }
}
