using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public BalletDataTable dataTable;
    public string[] m_Tag;
    public int m_Power;

    [HideInInspector]
    public Vector2 m_To;

    private Rigidbody2D me;

	// Use this for initialization
	void Start ()
    {
        me = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_To.x < transform.position.x) transform.localScale = new Vector3(-5, 5, 5);
        me.MovePosition(Vector2.MoveTowards(me.position, m_To, dataTable.Speed));
        if (me.position == m_To) Destroy(gameObject);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "GuardBlock")
        {
            if(m_Power < collision.gameObject.GetComponent<GuardBlockScript>().data.CanGuard)
                Destroy(gameObject);
        }

        foreach (var tag in m_Tag)
        {
            if(collision.gameObject.tag == tag)
            {
                var hit = collision.gameObject.GetComponent(typeof(IBATTLE_Character)) as IBATTLE_Character;
                hit.Damege(dataTable.Damege, dataTable.AttackType);
                hit.AddStatus(dataTable.AddStatus);
                Destroy(gameObject);
                
                break;
            }
        }
    }
}
