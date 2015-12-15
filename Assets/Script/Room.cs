using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour {

    public int top = 1;
    public int down = 1;
    public int left = 1;
    public int right = 1;

    public GameObject prefab;

    public int xRoom;
    public int yRoom;

	public Vector3 position;

    public GameObject parent = null;
    public bool isCorridor = true;

	public GameObject[,] room;
    int length = 13;
    int corridorLarge = 7;
    public GameObject prefabCase;

    public List<int> directions;

	public GameObject ShopPrefab;
	GameObject Shop;

    public int id;

    // Use this for initialization
    void Awake ()
    {
        room = new GameObject[length, length];
        Debug.Log(length);
		prefabCase = Resources.Load ("Prefab/Cube") as GameObject;
		if (  prefabCase == null ) 
			Debug.Log("Load Object Fail"); 
    }

    public void create()
    {
		float r = Random.Range (0f, 1f);
		if (r > .33f)
			isCorridor = false;

		if(id == 0)
			isCorridor = false;

        if (!isCorridor)
        {
            for (int x = 0; x < length; ++x)
            {
                for (int y = 0; y < length; ++y)
                {
					GameObject enfant = Instantiate(prefabCase, new Vector3(transform.position.x+x-2,0,transform.position.z+y-2), Quaternion.identity) as GameObject;
					room[x, y] = enfant;
					enfant.transform.parent = transform;
					room[x, y].GetComponent<Case>().SetPosition(transform.position.x+x-2,transform.position.z+y-2);

                    if (x == Mathf.Floor(length / 2)) // le haut et le bas
                    {                        
						if (y == 0 && top == 0)
                        {
							room[x, y].GetComponent<Case>().SetCase(2);
                        }
                        else if (y == length - 1 && down == 0)
                        {
							room[x, y].GetComponent<Case>().SetCase(2);
                        }
						else if (y != length - 1 && y != 0)
						{
							room[x, y].GetComponent<Case>().SetCase(0);
						}
                        else
                        {
							room[x, y].GetComponent<Case>().SetCase(1);
                        }
					}
					else if (y == Mathf.Floor(length / 2)) // la droite et la gauche
                    {
                        if (x == 0 && left == 0)
                        {
							room[x, y].GetComponent<Case>().SetCase(2);
                        }
                        else if (x == length - 1 && right == 0)
                        {
							room[x, y].GetComponent<Case>().SetCase(2);
                        }
						else if (x != length - 1 && x != 0)
						{
							room[x, y].GetComponent<Case>().SetCase(0);
						}
                        else
                        {
							room[x, y].GetComponent<Case>().SetCase(1);
                        }

                    }
                    else if (x == 0 || y == 0 || x == length - 1 || y == length - 1) // les murs autour
					{

						room[x, y].GetComponent<Case>().SetCase(1);

					}
                    else //le milieu
					{
						room[x, y].GetComponent<Case>().SetCase(0);

					}
                }
            }
        }
        else
        {
			//Debug.Log("corridor !");
            for (int x = 0; x < length; ++x)
            {
                for (int y = 0; y < length; ++y)
                {
					GameObject enfant = Instantiate(prefabCase, new Vector3(transform.position.x+x-2,0,transform.position.z+y-2), Quaternion.identity) as GameObject;
					room[x, y] = enfant;
					enfant.transform.parent = transform;
					room[x, y].GetComponent<Case>().SetCase(1);
					room[x, y].GetComponent<Case>().SetPosition(transform.position.x+x-2,transform.position.z+y-2);
                }
            }
            EmptyCorridor();
        }

		for (int x = 0; x < length; ++x)
		{
			for (int y = 0; y < length; ++y)
			{
				room[x, y].GetComponent<Case>().Construct();
			}
		}
    }

    void EmptyCorridor()
    {
        int half = (int)Mathf.Floor(length / 2) ;
        int largHalf = (corridorLarge / 2) - 2;
        //Debug.Log ("half:"+half);

        if (top == 0)
		{
			for(int x = half - largHalf; x <= half + largHalf; ++x)
			{
				for(int y = 0; y < corridorLarge + (half - largHalf); ++y)
				{
					if(room [x,y].GetComponent<Case>().GetTypeCase() == 1)
					{
						room[x, y].GetComponent<Case>().SetTypeCase(0);
					}
				}
			}
		}

		if (down == 0)
		{
			for (int x = half - largHalf; x <= half + largHalf; ++x)
			{
				for (int y = half - largHalf; y <= length - largHalf; ++y)
				{
					if (room[x, y].GetComponent<Case>().GetTypeCase() == 1)
					{
						room[x, y].GetComponent<Case>().SetTypeCase(0);
					}
				}
			}
		}

		if (right == 0)
		{
			for (int x = half - largHalf; x <= length - largHalf; ++x)
			{
				for (int y = half - largHalf; y <= half + largHalf; ++y)
				{
					if (room[x, y].GetComponent<Case>().GetTypeCase() == 1)
					{
						room[x, y].GetComponent<Case>().SetTypeCase(0);
					}
				}
			}
		}

		if (left == 0)
		{
			for (int x = 0; x < corridorLarge + (half - largHalf); ++x)
			{
				for (int y = half - largHalf; y <= half + largHalf; ++y)
				{
					if (room[x, y].GetComponent<Case>().GetTypeCase() == 1)
					{
						room[x, y].GetComponent<Case>().SetTypeCase(0);
					}
				}
			}
		}
    }

	public void PlaceBonus()
	{
		int caseX = 0;
		int caseY = 0;
		int rand = Random.Range (0, 3);

		if(rand == 0)
		{
			caseX = 1;
			caseY = 1;
		}
		else if(rand == 1)
		{
			caseX = 3;
			caseY = 1;
		}
		else if(rand == 2)
		{
			caseX = 1;
			caseY = 3;
		}
		else if(rand == 3)
		{
			caseX = 3;
			caseY = 3;
		}

		Shop = Instantiate(ShopPrefab, new Vector3(transform.position.x+caseX-2, transform.position.y, transform.position.z+caseY-2), Quaternion.identity) as GameObject;
	}

	public void RemoveBonus()
	{
		if(Shop != null)
		{
			Destroy(Shop.gameObject);
		}
	}
}
