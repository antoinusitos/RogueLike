using UnityEngine;
using System.Collections;

public class Case : MonoBehaviour {

    public Vector3 position;

    public enum Type
    {
        wall,
        door,
        empty,

    }

    public Type currentType;

	public void SetPosition(float x, float y)
	{
		position = new Vector3 (x, 0, y);
	}

    public void SetCase(int theType)
    {
        if(theType == 0)
        {
            currentType = Type.empty;
        }
        else if (theType == 1)
        {
            currentType = Type.wall;
        }
        else if (theType == 2)
        {
            currentType = Type.door;
        }
    }

	public void Construct()
	{
		//transform.position = position;
		if(currentType == Type.empty || currentType == Type.door)
		{
			transform.GetChild(0).gameObject.SetActive(false);
		}
		else
		{
			transform.GetChild(1).gameObject.SetActive(false);
		}
	}

    public int GetTypeCase()
    {
        if (currentType == Type.empty)
        {
            return 0;
        }
        else if (currentType == Type.wall)
        {
            return 1;
        }
        else
            return 2;
    }

    public void SetTypeCase(int theType)
    {
        if (theType == 0)
        {
            currentType = Type.empty;
        }
        else if (theType == 1)
        {
            currentType = Type.wall;
        }
        else if (theType == 2)
        {
            currentType = Type.door;
        }
    }
}
