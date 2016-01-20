using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Labyrinthe : MonoBehaviour {

    //public Room[,] labyrinthe;
    public GameObject[,] labyrinthe;
    public GameObject prefabRoom;
	public GameObject PlayerPrefab;
	public GameObject TriggerFin;
	GameObject fin;
	GameObject player;
    int length = 7;
	int roomLength = 13;
    GameObject StartRoom;
	GameObject EndRoom;
	List<GameObject> leaf;
    int xPointA;
    int yPointA;
    int xPointB;
    int yPointB;
    int count = 0;
    List<int> directions;
	int prevDirection = -1;
	int countDirection = 0;
    GameObject LastRoom;

    // Use this for initialization
    void Start ()
    {
		/*leaf = new List<GameObject> ();
        directions = new List<int>();
        directions.Add(0);
        directions.Add(1);
        directions.Add(2);
        directions.Add(3);
        //labyrinthe = new Room[length, length];
        labyrinthe = new GameObject[length, length];
        xPointA = Random.Range(0, length-1);
        yPointA = Random.Range(0, length-1);
		StartRoom = Instantiate(prefabRoom, new Vector3(xPointA*roomLength,0,yPointA*roomLength), Quaternion.identity) as GameObject;
        Room newRoom = StartRoom.GetComponent<Room>();
		newRoom.position = new Vector3(xPointA*roomLength,0,yPointA*roomLength);
        newRoom.xRoom = xPointA;
        newRoom.yRoom = yPointA;
        newRoom.id = count;
        labyrinthe[newRoom.xRoom, newRoom.yRoom] = StartRoom;
		count++;
        RandomDir(StartRoom);

        //Debug.Log("fin de la génération:" + count);

		for(int i= 0; i< length; ++i)
		{
			for(int j= 0; j< length; ++j)
			{
				labyrinthe[i,j].GetComponent<Room>().create();
			}
		}

		Instantiate (Player, new Vector3 (StartRoom.transform.position.x, StartRoom.transform.position.y+1, StartRoom.transform.position.z), Quaternion.identity);
		int rand = Random.Range (0, leaf.Count);
		EndRoom = leaf [rand];
		Instantiate (TriggerFin, EndRoom.transform.position, Quaternion.identity);
		leaf.RemoveAt (rand);*/

		Generate ();
    }

	public void Generate()
	{
		if(labyrinthe != null)
		{
			StartRoom.GetComponent<Room> ().RemoveBonus ();
			for (int i = 0; i < leaf.Count; ++i)
			{
				leaf[i].GetComponent<Room>().RemoveBonus();
			}

			for(int i= 0; i< length; ++i)
			{
				for(int j= 0; j< length; ++j)
				{
					Destroy(labyrinthe[i,j].gameObject);
				}
			}
			Destroy(fin);
		}

		leaf = new List<GameObject> ();
		directions = new List<int>();
		directions.Add(0);
		directions.Add(1);
		directions.Add(2);
		directions.Add(3);
		//labyrinthe = new Room[length, length];
		labyrinthe = new GameObject[length, length];
		xPointA = Random.Range(0, length-1);
		yPointA = Random.Range(0, length-1);
		StartRoom = Instantiate(prefabRoom, new Vector3(xPointA*roomLength,0,yPointA*roomLength), Quaternion.identity) as GameObject;
		Room newRoom = StartRoom.GetComponent<Room>();
		newRoom.position = new Vector3(xPointA*roomLength,0,yPointA*roomLength);
		newRoom.xRoom = xPointA;
		newRoom.yRoom = yPointA;
		newRoom.id = count;
		labyrinthe[newRoom.xRoom, newRoom.yRoom] = StartRoom;
		count++;
		RandomDir(StartRoom);
		
		//Debug.Log("fin de la génération:" + count);
		
		for(int i= 0; i< length; ++i)
		{
			for(int j= 0; j< length; ++j)
			{
				labyrinthe[i,j].GetComponent<Room>().create();
			}
		}

		if (player == null)
			player = Instantiate (PlayerPrefab, new Vector3 (StartRoom.transform.position.x, StartRoom.transform.position.y + 1, StartRoom.transform.position.z), Quaternion.identity) as GameObject;
		else 
			player.transform.position = new Vector3 (StartRoom.transform.position.x, StartRoom.transform.position.y + 1, StartRoom.transform.position.z);

		int rand = Random.Range (0, leaf.Count);
		EndRoom = leaf [rand];
		fin = Instantiate (TriggerFin, EndRoom.transform.position, Quaternion.identity) as GameObject;
		fin.GetComponent<EndLevel> ().parent = gameObject;
		leaf.RemoveAt (rand);

		StartRoom.GetComponent<Room> ().PlaceBonus ();
		int increment = 0;
		if (leaf.Count % 2 != 0)
			increment = 1;
		for (int i = 0; i < (leaf.Count/2)+increment; ++i)
		{
			leaf[i].GetComponent<Room>().PlaceBonus();
        }
	}

    void RandomDir(GameObject CurrentRoom)
    {
        //Debug.Log("RANDOM DIR");
		directions = new List<int>();
		directions.Add(0);
		directions.Add(1);
		directions.Add(2);
		directions.Add(3);
        Room theCurrentRoom = CurrentRoom.GetComponent<Room>();
		theCurrentRoom.directions = Shuffle(directions, prevDirection);
		if(prevDirection == theCurrentRoom.directions[0])
		{
			countDirection ++;
			if(countDirection >= 1)
			{
				theCurrentRoom.directions = Shuffle(theCurrentRoom.directions, prevDirection);
				countDirection = 0;
				//Debug.Log("Changement :"+theCurrentRoom.id);
				prevDirection = theCurrentRoom.directions[0];
			}
		}
		else
		{
			countDirection = 0;
			prevDirection = theCurrentRoom.directions[0];
		}

		int dirTry = 0;

        foreach (int Dir in theCurrentRoom.directions)
        {
			//Debug.Log("Dir:"+Dir+" id:"+ theCurrentRoom.id);
            GameObject instanceNewRoom = null;
            if (Dir == 0 && (theCurrentRoom.yRoom - 1 >= 0) && (labyrinthe[theCurrentRoom.xRoom, theCurrentRoom.yRoom - 1] == null)) // on monte
            {
                CurrentRoom.GetComponent<Room>().top = 0;
				instanceNewRoom = Instantiate(prefabRoom, new Vector3(theCurrentRoom.xRoom*roomLength,0,(theCurrentRoom.yRoom - 1)*roomLength), Quaternion.identity) as GameObject;
                Room newRoom = instanceNewRoom.GetComponent<Room>();
				newRoom.position = new Vector3(theCurrentRoom.transform.position.x,0,theCurrentRoom.transform.position.y);
                newRoom.xRoom = theCurrentRoom.xRoom;
                newRoom.yRoom = theCurrentRoom.yRoom - 1;
                newRoom.id = count;
                newRoom.parent = CurrentRoom;
                newRoom.down = 0;
                labyrinthe[newRoom.xRoom, newRoom.yRoom] = instanceNewRoom;
            }
            else if (Dir == 1 && theCurrentRoom.yRoom + 1 < length && labyrinthe[theCurrentRoom.xRoom, theCurrentRoom.yRoom + 1] == null) // on descend
            {
                CurrentRoom.GetComponent<Room>().down = 0;
				instanceNewRoom = Instantiate(prefabRoom, new Vector3(theCurrentRoom.xRoom*roomLength,0,(theCurrentRoom.yRoom + 1)*roomLength), Quaternion.identity) as GameObject;
                Room newRoom = instanceNewRoom.GetComponent<Room>();
				newRoom.position = new Vector3(theCurrentRoom.transform.position.x,0,theCurrentRoom.transform.position.y);
                newRoom.xRoom = theCurrentRoom.xRoom;
                newRoom.yRoom = theCurrentRoom.yRoom + 1;
                newRoom.id = count;
                newRoom.parent = CurrentRoom;
                newRoom.top = 0;
                labyrinthe[newRoom.xRoom, newRoom.yRoom] = instanceNewRoom;
            }
            else if (Dir == 2 && theCurrentRoom.xRoom - 1 >= 0 && labyrinthe[theCurrentRoom.xRoom - 1, theCurrentRoom.yRoom] == null) // on va a gauche
            {
                CurrentRoom.GetComponent<Room>().left = 0;
				instanceNewRoom = Instantiate(prefabRoom, new Vector3((theCurrentRoom.xRoom - 1)*roomLength,0,theCurrentRoom.yRoom*roomLength), Quaternion.identity) as GameObject;
                Room newRoom = instanceNewRoom.GetComponent<Room>();
				newRoom.position = new Vector3(theCurrentRoom.transform.position.x,0,theCurrentRoom.transform.position.y);
                newRoom.xRoom = theCurrentRoom.xRoom - 1;
                newRoom.yRoom = theCurrentRoom.yRoom;
                newRoom.id = count;
                newRoom.parent = CurrentRoom;
                newRoom.right = 0;
                labyrinthe[newRoom.xRoom, newRoom.yRoom] = instanceNewRoom;
            }
            else if (Dir == 3 && theCurrentRoom.xRoom + 1 < length && labyrinthe[theCurrentRoom.xRoom + 1, theCurrentRoom.yRoom] == null) // on va a droite
            {
                CurrentRoom.GetComponent<Room>().right = 0;
				instanceNewRoom = Instantiate(prefabRoom, new Vector3((theCurrentRoom.xRoom + 1)*roomLength,0,theCurrentRoom.yRoom*roomLength), Quaternion.identity) as GameObject;
                Room newRoom = instanceNewRoom.GetComponent<Room>();
				newRoom.position = new Vector3(theCurrentRoom.transform.position.x,0,theCurrentRoom.transform.position.y);
                newRoom.xRoom = theCurrentRoom.xRoom + 1;
                newRoom.yRoom = theCurrentRoom.yRoom;
                newRoom.id = count;
                newRoom.parent = CurrentRoom;
                newRoom.left = 0;
                labyrinthe[newRoom.xRoom, newRoom.yRoom] = instanceNewRoom;
            }
            else
            {
//                Debug.Log("on peut rien faire");
				dirTry++;
            }

			if(dirTry == 4)
			{
				leaf.Add(CurrentRoom);
				//Debug.Log("je suis une feuille !");
			}

            if(instanceNewRoom != null)
            {
				count++;
                RandomDir(instanceNewRoom);

            }
        }
    }

    List<int> Shuffle(List<int> tab, int dirToAvoid = -1)
    {
        List<int> tabReturn = new List<int>();
        List<int> tabTemp = tab;

		if(dirToAvoid != -1)
		{
			//Debug.Log("dirToAvoid:"+dirToAvoid);
			int find = FindInTab(tabTemp, dirToAvoid);
			//Debug.Log("find:"+find);
			tabTemp.RemoveAt(find);
		}

        while (tabTemp.Count > 0)
        {
            int rand = Random.Range(0, tabTemp.Count - 1);
            tabReturn.Add(tabTemp[rand]);
            tabTemp.RemoveAt(rand);
        }

		if(dirToAvoid != -1)
		{
			tabReturn.Add(dirToAvoid);
		}

        return tabReturn;
    }

	int FindInTab(List<int> tab, int toFind)
	{
		//Debug.Log("FindInTab");
		for(int i=0; i < tab.Count; ++i)
		{
			//Debug.Log("i:"+i+" val:"+tab[i]);
			if (tab[i] == toFind)
			{
				return i;
			}
		}
		return -1;
	}
}
