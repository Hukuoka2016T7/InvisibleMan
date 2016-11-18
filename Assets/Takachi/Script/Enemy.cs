using UnityEngine;
using System.Collections;

enum State
{
    // 待機中
    Idol,
    // 元の位置にもっているか
    OriginPoint,
    // 対話している
    Dialogue,
    // 歩いてる
    Walking,
    // 追いかける
    Chasing,
    // 隠れている(物影等に)
    Hide,
}
public class Enemy : MonoBehaviour
{
    [SerializeField,Header("住民の視野の距離(プレイヤーの高さ × 距離の長さ)")]
    public float m_ViewRange;
    [SerializeField, Header("目的地の前で止まるための距離")]
    public float m_ArrivedRange;
    [SerializeField, Header("視野の範囲(角度)")]
    public float m_ViewAngle;

    private NavMeshAgent m_Agent;
    private Transform m_Player;
    private Vector3 m_Location;
    private State m_State = State.Idol;

	// Use this for initialization
	void Start ()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
        m_Agent = GetComponent<NavMeshAgent>();

        m_Location = transform.position;
        m_State = State.Idol;
	}
	
	// Update is called once per frame
	void Update (/*float deltaTime*/)
    {
        if (m_State == State.Chasing)
        {
            if (EnemyView())
            {
                Apper();
            }
            else
            {
                m_State = State.OriginPoint;
            }
        }
        else if (m_State == State.OriginPoint || m_State == State.Idol)
        {
            if (EnemyView())
            {
                Apper();

                m_State = State.Chasing;
            }
            else if (ArrivedDestination())
            {
                Invisible();
                m_State = State.Idol;
            }
        }
        HideAndSeek();
        //deltaTime = 0;
    }

    /// <summary>
    /// プレイヤーが見えている（実体化？）した時
    /// </summary>
    public void Apper()
    {
        // プレイヤーが透明ではなかった場合
        if (m_Player.GetComponent<PlayerStatus>()._invisibleGage >= 100.0f)
        {
            // 探索経路からプレイヤーまでの最短ルートを通りプレイヤーに近づく
            m_Agent.SetDestination(m_Player.transform.position);
        }
    }

    /// <summary>
    /// プレイヤーが見えない状態（透明）な時
    /// </summary>
    public void Invisible()
    {
        // プレイヤーが透明な場合
        if (m_Player.GetComponent<PlayerStatus>()._invisibleTr == true)
        {
            // 現在の位置から元の所定の位置に戻る
            m_Agent.SetDestination(m_Location);
        }
    }

    /// <summary>
    /// プレイヤーが視野内に入って来たか？
    /// </summary>
    /// <returns></returns>
    public bool EnemyView()
    {
        if (!InViewRange())
        {
            return false;
        }

        if (!IsPlayerInFieldOfView())
        {
            return false;
        }

        // プレイヤー間との判定
        RaycastHit hitInfo;
        bool hit = Physics.Raycast(transform.position, m_Player.transform.position - transform.position, out hitInfo, m_Player.transform.localScale.y * m_ViewRange);

        // デッバグ用
        Debug.DrawLine(transform.position, m_Player.transform.position, Color.blue);

        if (hit && hitInfo.collider.tag == "Player")
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// プレイヤーが視野角の範囲内にいるかを返す
    /// 壁の向こうは考慮しない
    /// </summary>
    /// <returns></returns>
    public bool IsPlayerInFieldOfView()
    {
        if (m_State == State.Chasing)
        {
            return true;
        }

        Vector3 find = m_Player.transform.position - transform.position;

        return (Vector3.Angle(find, transform.forward) < m_ViewAngle);

        ///Debug.DrawLine();
    }

    /// <summary>
    /// プレイヤーが視野（距離）の範囲内にいるか？
    /// </summary>
    /// <returns></returns>
    private bool InViewRange()
    {
        // プレイヤーとの距離
        float distance = (m_Player.transform.position - transform.position).magnitude;

        return (distance <= m_ViewRange);
    }

    /// <summary>
    /// NavMeshAgentの目的地（プレイヤー）に到達したか？
    /// </summary>
    /// <returns></returns>
    private bool ArrivedDestination()
    {
        // 目的地の距離に達したらその距離で止まる
        return (m_Agent.remainingDistance < m_ArrivedRange);
    }

    /// <summary>
    /// プレイヤーが物陰に隠れているのなら追わない
    /// </summary>
    /// <returns></returns>
    private void HideAndSeek()
    {
        if (m_Player.GetComponent<PlayerStatus>()._hide == 0)
        {
            Apper();
        }
        else
        {
            Invisible();
            m_State = State.Hide;
        }
    }
}
