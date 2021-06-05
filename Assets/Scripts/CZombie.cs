using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CZombie : MonoBehaviour
{
    public Animator m_animator;
    public Transform m_transform;
    public  bool isWalk;
    public bool isRun;
    public Vector3 vForward;
    private CMove move;
    private CTimer zwait;
    public float walkSpeed;
    public float runSpeed;
    public int steps = 8;
    public int curStep = 0;
    public int a = 360;

    ////////////////////////////////////////////////
    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_transform = GetComponent<Transform>();
        walkSpeed = 0.5f;
        runSpeed = 2.0f;
        isWalk = false;
        isRun = false;
        move = new CMove();
        zwait = new CTimer();
        move.SetActionSpeed(walkSpeed);
        vForward = m_transform.forward;
        zwait.SetActionTime(3);
    }

    private void StartWalk()
    {
        isWalk = true;
        move.SetPositions(m_transform.position, m_transform.position + vForward);
        move.StartAction();
        m_animator.SetBool("walk", isWalk);
   }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isWalk)
        {
            move.SetActionSpeed(walkSpeed);
            StartWalk();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (isWalk) isRun = true;
            // m_animator.SetBool("run", isRun);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (isRun) isRun = false;
            m_animator.SetBool("run", isRun);
        }
    }

    //////////////////////////////////
    // Update is called once per frame
    void Update()
    {
        CheckInput();
        if (move.IsActive())
        {
            if (!move.UpdatePosition())
            {
                isWalk = false;
            }
            m_transform.position = move.GetCurrentPosition();
            if (!isWalk)
            {
                if (++curStep < steps)
                {
                    if (isRun)
                    {
                        move.SetActionSpeed(runSpeed);
                        m_animator.SetBool("run", isRun);
                    }
                    else move.SetActionSpeed(walkSpeed);
                    StartWalk();
                }
                else
                {
                    curStep = 0;
                    m_animator.SetBool("walk", isWalk);
                    zwait.StartAction();
                    isRun = false;
                    m_animator.SetBool("run", isRun);
                    m_animator.SetBool("attack", true);
                }
            }
        }
        if (zwait.IsActive())
        {
            if (zwait.UpdateState())
            {
             float angle;

                angle = a - 90.0f * zwait.GetState();
                m_transform.rotation = Quaternion.Euler(0, angle, 0);
            }
            else
            {
                a -= 90;
                m_transform.rotation = Quaternion.Euler(0, a, 0);
                if (a == 0) a = 360;
                vForward = m_transform.forward;
                m_animator.SetBool("attack", false);
                move.SetActionSpeed(walkSpeed);
                StartWalk();
            }
        }

    }
}
