using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyGame/Create GuardPowerDataTable", fileName = "GuardPowerDataTable")]
public class GuardPowerData : ScriptableObject
{
    [SerializeField]
    private int m_CanGuard;
    public int CanGuard { get { return m_CanGuard; } }
}
