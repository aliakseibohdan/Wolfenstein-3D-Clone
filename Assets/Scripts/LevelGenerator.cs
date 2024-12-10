using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	public int width = 10;
	public int height = 10;

	public GameObject wall;
	public GameObject floor;

	void Start () {
		GenerateLevel();
	}
	
	void GenerateLevel()
	{
		// Цикл по сетке
		for (int x = 0; x <= width; x+=2)
		{
			for (int y = 0; y <= height; y+=2)
			{
				bool isEdgeCell = (x == 0 || x == width) || (y == 0 || y == height);
                // Поставить стену?
                if (Random.value > .7f || isEdgeCell)
				{
					// Ставим стену
					Vector3 pos = new Vector3(x - width / 2f, 0.5f, y - height / 2f);
					Instantiate(wall, pos, Quaternion.identity, transform);
				}

                Vector3 floorPos = new Vector3(x - width / 2f, -1.5f, y - height / 2f);
                Instantiate(floor, floorPos, Quaternion.identity, transform);
            }
        }
	}
}
