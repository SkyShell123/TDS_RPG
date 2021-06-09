using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoomPlacer : MonoBehaviour
{
    private int ind = 0;
    public Transform Player;
    public Room[] RoomPrefabs;
    public Room StartRoom;

    public GameObject Door;

    private Room[,] spawnedRooms;
    HashSet<Vector2Int> vacantPlaces;

    private void Start()
    {
        spawnedRooms = new Room[11, 11];
        spawnedRooms[5, 5] = StartRoom;

        for (int i = 0; i < 50; i++)
        {
            PlaceOneRoom();
        }
    }

    //void Update()
    //{
    //    if (spawnedRooms.Length > 5)
    //    {
    //        foreach (Vector2Int item in vacantPlaces)
    //        {
    //            if (Vector2.Distance(Player.position, vacantPlaces[item]) < 12)
    //            {
    //                PlaceOneRoom();
    //            }
    //        }
    //    }
    //}

    private void PlaceOneRoom()
    {
        vacantPlaces = new HashSet<Vector2Int>();
        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                if (spawnedRooms[x, y] == null) continue;

                int maxX = spawnedRooms.GetLength(0) - 1;
                int maxY = spawnedRooms.GetLength(1) - 1;

                if (x > 0 && spawnedRooms[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));
                if (y > 0 && spawnedRooms[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));
                if (x < maxX && spawnedRooms[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));
                if (y < maxY && spawnedRooms[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));
            }
        }

        // Эту строчку можно заменить на выбор комнаты с учётом её вероятности, вроде как в ChunksPlacer.GetRandomChunk()
        Room newRoom = Instantiate(RoomPrefabs[Random.Range(0, RoomPrefabs.Length)]);
        
        

        int limit = 500;
        while (limit-- > 0)
        {
            // Эту строчку можно заменить на выбор положения комнаты с учётом того насколько он далеко/близко от центра,
            // или сколько у него соседей, чтобы генерировать более плотные, или наоборот, растянутые данжи
            Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
            newRoom.RotateRandomly();

            if (ConnectToSomething(newRoom, position))
            {
                newRoom.transform.position = new Vector3(position.x - 5, position.y - 5) * 10;
                spawnedRooms[position.x, position.y] = newRoom;

                newRoom.index = ind++;
                
                return;
            }
        }

        Destroy(newRoom.gameObject);
    }

    private bool ConnectToSomething(Room room, Vector2Int p)
    {
        int maxX = spawnedRooms.GetLength(0) - 1;
        int maxY = spawnedRooms.GetLength(1) - 1;

        List<Vector2Int> neighbours = new List<Vector2Int>();

        if (room.DoorU != null && p.y < maxY && spawnedRooms[p.x, p.y + 1]?.DoorD != null) neighbours.Add(Vector2Int.up);
        if (room.DoorD != null && p.y > 0 && spawnedRooms[p.x, p.y - 1]?.DoorU != null) neighbours.Add(Vector2Int.down);
        if (room.DoorR != null && p.x < maxX && spawnedRooms[p.x + 1, p.y]?.DoorL != null) neighbours.Add(Vector2Int.right);
        if (room.DoorL != null && p.x > 0 && spawnedRooms[p.x - 1, p.y]?.DoorR != null) neighbours.Add(Vector2Int.left);

        if (neighbours.Count == 0) return false;

        Vector2Int selectedDirection = neighbours[Random.Range(0, neighbours.Count)];
        Room selectedRoom = spawnedRooms[p.x + selectedDirection.x, p.y + selectedDirection.y];

        if (room.DoorU != null && p.y < maxY && spawnedRooms[p.x, p.y + 1]?.DoorD != null)
        {
            room.DoorU.SetActive(false);
            selectedRoom.DoorD.SetActive(false);
            spawnedRooms[p.x, p.y + 1]?.DoorD.SetActive(false);

            Instantiate(Door, spawnedRooms[p.x, p.y + 1].DoorD.transform.position, transform.rotation);
            //Instantiate(Door, selectedRoom.DoorD.transform.position, transform.rotation);
        }
        if (room.DoorD != null && p.y > 0 && spawnedRooms[p.x, p.y - 1]?.DoorU != null)
        {
            room.DoorD.SetActive(false);
            selectedRoom.DoorU.SetActive(false);
            spawnedRooms[p.x, p.y - 1]?.DoorU.SetActive(false);

            Instantiate(Door, spawnedRooms[p.x, p.y - 1].DoorU.transform.position, transform.rotation);
            //Instantiate(Door, selectedRoom.DoorU.transform.position, transform.rotation);
        }
        if (room.DoorR != null && p.x < maxX && spawnedRooms[p.x + 1, p.y]?.DoorL != null)
        {
            room.DoorR.SetActive(false);
            selectedRoom.DoorL.SetActive(false);
            spawnedRooms[p.x+1, p.y]?.DoorL.SetActive(false);

            Instantiate(Door, spawnedRooms[p.x + 1, p.y].DoorL.transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 90));
            //Instantiate(Door, selectedRoom.DoorL.transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 90));

        }
        if (room.DoorL != null && p.x > 0 && spawnedRooms[p.x - 1, p.y]?.DoorR != null)
        {
            room.DoorL.SetActive(false);
            selectedRoom.DoorR.SetActive(false);
            spawnedRooms[p.x-1, p.y]?.DoorR.SetActive(false);

            Instantiate(Door, spawnedRooms[p.x - 1, p.y].DoorR.transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 90));
            //Instantiate(Door, selectedRoom.DoorR.transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 90));
        }



        //if (selectedDirection == Vector2Int.up)
        //{
        //    room.DoorU.SetActive(false);
        //    selectedRoom.DoorD.SetActive(false);

        //    Instantiate(Door, selectedRoom.DoorD.transform.position, transform.rotation);
        //}
        //else if (selectedDirection == Vector2Int.down)
        //{
        //    room.DoorD.SetActive(false);
        //    selectedRoom.DoorU.SetActive(false);

        //    Instantiate(Door, selectedRoom.DoorU.transform.position, transform.rotation);
        //}
        //else if (selectedDirection == Vector2Int.right)
        //{
        //    room.DoorR.SetActive(false);
        //    selectedRoom.DoorL.SetActive(false);

        //    Instantiate(Door, selectedRoom.DoorL.transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 90));

        //}
        //else if (selectedDirection == Vector2Int.left)
        //{
        //    room.DoorL.SetActive(false);
        //    selectedRoom.DoorR.SetActive(false);

        //    Instantiate(Door, selectedRoom.DoorR.transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 90));
        //}

        return true;
    }
}

//    private void SpawnChunk(float corner)
//    {
//        Room newRoom = Instantiate(RoomPrefabs[Random.Range(0, RoomPrefabs.Length)]);
//        newRoom.transform.rotation = Quaternion.Euler(0, 0, 0 + corner);
//        newRoom.transform.position = SpawnedRooms[SpawnedRooms.Count - 1].Enter_Exit_Hor[1].position - newRoom.Enter_Exit_Hor[0].localPosition;

//        SpawnedRooms.Add(newRoom);

//        if (SpawnedRooms.Count >= 100)
//        {
//            Destroy(SpawnedRooms[0].gameObject);
//            SpawnedRooms.RemoveAt(0);
//        }
//    }

//    private void SpawnChunk1(float corner)
//    {
//        Room newRoom = Instantiate(RoomPrefabs[Random.Range(0, RoomPrefabs.Length)]);
//        newRoom.transform.rotation = Quaternion.Euler(0, 0, 0 + corner);
//        newRoom.transform.position = SpawnedRooms[SpawnedRooms.Count - 1].Enter_Exit_Ver[1].position - newRoom.Enter_Exit_Ver[0].localPosition;

//        SpawnedRooms.Add(newRoom);

//        if (SpawnedRooms.Count >= 100)
//        {
//            Destroy(SpawnedRooms[0].gameObject);
//            SpawnedRooms.RemoveAt(0);
//        }
//    }
//}
