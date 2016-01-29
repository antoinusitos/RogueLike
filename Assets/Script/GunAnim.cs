using UnityEngine;
using System.Collections;

public class GunAnim : MonoBehaviour {

     bool up;
     float limit;
     float offset;
     float speed;
     float speedBack;

    bool isShooting;

    float shotLength;
    float backLength;

    Vector3 startPos;

    public enum state
    {
        idle,
        walk,
        shoot,
        sprint,

    }

    public state currenState;
    public state prevState;

    void Start()
    {
        speedBack = 3.0f;
        up = true;
        limit = 0.01f;
        offset = 0;
        speed = .025f;
        currenState = state.idle;
        prevState = currenState;
        startPos = transform.localPosition;
    }

	void Update()
    {
        if (currenState != state.shoot)
        {
            if (up)
            {
                float displacement = Time.deltaTime * speed;
                transform.localPosition += new Vector3(0, displacement, 0);
                offset += displacement;
                if (offset >= limit)
                    up = false;
            }
            else
            {
                float displacement = Time.deltaTime * speed;
                transform.localPosition -= new Vector3(0, displacement, 0);
                offset -= displacement;
                if (offset <= -limit)
                    up = true;
            }
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, Time.deltaTime * speedBack);
            if(Vector3.Distance(transform.localPosition, startPos) <= .005f)
            {
                isShooting = false;
            }
        }
    }

    public void Shoot()
    {
        transform.localPosition = startPos;
        transform.localPosition -= new Vector3(0, 0, .15f);
        currenState = state.shoot;
        isShooting = true;
    }

    public void ChangeState(state theState)
    {
        if (!isShooting)
        {
            prevState = currenState;
            currenState = theState;
            if (currenState == state.idle && prevState != state.idle)
            {
                transform.localPosition = startPos;
                limit = 0.01f;
                speed = .025f;
                offset = 0;
            }
            else if (currenState == state.walk && prevState != state.walk)
            {
                transform.localPosition = startPos;
                limit = 0.03f;
                speed = .1f;
                offset = 0;
            }
            else if (currenState == state.sprint && prevState != state.sprint)
            {
                transform.localPosition = startPos;
                limit = 0.03f;
                speed = .3f;
                offset = 0;
            }
        }
    }
}
