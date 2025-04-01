using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DungeonGenerator : MonoBehaviour
{
    public Vector2Int size;
    public int startPoint=0;
    public bool RandomStartPoint;
    public Vector2 offset;
    public int maxNumberRooms;
    public int minNumberRooms;
    [SerializeField] private GameObject[] rooms;
    [SerializeField] private GameObject lastRoom;
    [SerializeField] private GameObject bossRoom;
    [SerializeField] private GameObject startingRoom;
    [SerializeField] private GameObject exRoom;
    [SerializeField] private int maxNumberExRoom;
    [SerializeField] [Range(0f,1f)]  private float  rateSpawnExRoom=0.6f;
    [SerializeField] private Transform originRooms;
    [SerializeField] private Transform holdRooms;

    public List<GameObject> listRooms;
    private int randomNumberRooms;
    private int currentRoom=0;
    private int curentExRoom=0;

    //main road
    List<Cell> gridBroad;
    //can create

    

    private void Start()
    {
        StartCoroutine(GenerateMap());
    }
    public IEnumerator GenerateMap(){
        if (RandomStartPoint)
            SetRandomStartPoint();
        foreach (var item in rooms)
        {
            GameObject roomCreate = Instantiate(item, transform.position, Quaternion.identity, originRooms);
            listRooms.Add(roomCreate);
            roomCreate.SetActive(false);

        }

        randomNumberRooms = Random.Range(minNumberRooms, maxNumberRooms + 1);
        yield return new WaitForEndOfFrame();
        GeneraterMaze();
        yield return new WaitForEndOfFrame();
        if (GameManager.instance != null && GameManager.instance.Surface2D != null)
        {
            GameManager.instance.Surface2D.BuildNavMeshAsync();
        }
        else
        {
            Debug.LogWarning("GameManager.instance or Surface2D is null. Cannot build NavMesh.");
        }
        yield return new WaitForEndOfFrame();
        int dem = 0;
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                int positionCurrentGridBroad = i + j * size.x;
                Cell newCurrentCell = gridBroad[positionCurrentGridBroad];
                if (newCurrentCell.visited || newCurrentCell.canCreateExRoom)
                {
                    dem++;

                    for (int q = 0; q < newCurrentCell.status.Length; q++)
                    {
                        if (newCurrentCell.status[q] == true)
                        {
                            
                            int tempNumber;
                            if (q == 0)
                            {
                                tempNumber = positionCurrentGridBroad - size.x;
                            }
                            else if (q == 1)
                            {
                                tempNumber = positionCurrentGridBroad + size.x;
                            }
                            else if (q == 2)
                            {
                                tempNumber = positionCurrentGridBroad + 1;
                            }
                            else
                            {
                                tempNumber = positionCurrentGridBroad - 1;
                            }
                            CheckDoorRoom(q, newCurrentCell.position, tempNumber);
                        }
                    }
                }

            }
        }
        yield return new WaitForEndOfFrame();
        DisabledRooms();
        yield return null;

    }

    private void DisabledRooms()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                int positionCurrentGridBroad = i + j * size.x;
                if (gridBroad[positionCurrentGridBroad].visited)
                {
                    if (gridBroad[positionCurrentGridBroad].position.gameObject.GetComponent<StartingRoom>() != null){
                        gridBroad[positionCurrentGridBroad].position.gameObject.GetComponent<Room>().ActiveMiniMapIcon();
                        continue;
                    }
                    Room room = gridBroad[positionCurrentGridBroad].position.gameObject.GetComponent<Room>();
                    room.gameObject.SetActive(false);
                }
            }
        }
    }
    public void SetUpStartingRoom(){
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                int positionCurrentGridBroad = i + j * size.x;
                if (gridBroad[positionCurrentGridBroad].visited)
                {
                    if (gridBroad[positionCurrentGridBroad].position.gameObject.GetComponent<StartingRoom>() != null){
                        gridBroad[positionCurrentGridBroad].position.gameObject.GetComponent<Room>().ActiveMiniMapIcon();
                        gridBroad[positionCurrentGridBroad].position.gameObject.GetComponent<Room>().ActivateConnectedRooms();
                        return;
                    }
                }
                
            }
        }
    }

    private void SetRandomStartPoint()
    {
        int randomPosition = Random.Range(0, 4);
        
        switch(randomPosition)
        {
            case 0:
                startPoint = size.x/2;
                break;
            case 1:
                startPoint =(size.y - 1)*size.x + size.x/2;
                break;
            case 2:
                startPoint = size.x * (size.y / 2);
                break;
            case 3:
                startPoint = (size.y/2)*size.x + size.x - 1;
                break;
        }

        Debug.Log($"Starting room position: {randomPosition}, Grid position: {startPoint}");
    }

    private void GenerateDungeon(){

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Cell currentCell=gridBroad[i+j*size.x];
                if(currentCell.visited==true){
                    Vector2 roomPosition = new Vector2(i * offset.x + offset.x / 2,-j * offset.y - offset.y / 2 );
                    if(currentCell.startingRoom){
                        GameObject newRoom=Instantiate(startingRoom,roomPosition,Quaternion.identity,holdRooms);
                        gridBroad[i+j*size.x].position=newRoom.transform;
                        newRoom.GetComponent<Room>().UpdateRoom(currentCell.status);

                        newRoom.name+=$"Starting room {i} - {j}";
                    }
                    else if(currentCell.lastRoom){

                        // create boss room
                        GameObject roomType;
                        if(DungeonManager.instance.GetRoundNumberDungeon()<=0){
                            roomType=bossRoom;
                        }
                        else{
                            roomType=lastRoom;
                        }
                        GameObject newRoom=Instantiate(roomType,roomPosition,Quaternion.identity,holdRooms);
                        gridBroad[i+j*size.x].position=newRoom.transform;
                        newRoom.GetComponent<Room>().UpdateRoom(currentCell.status);
                        newRoom.GetComponent<Room>().SpawnBoss();
                        newRoom.name+=$"Last room {i} - {j}";
                    }
                    else if(currentCell.canCreateExRoom){
                        GameObject newRoom=Instantiate(exRoom,roomPosition,Quaternion.identity,holdRooms);
                        gridBroad[i+j*size.x].position=newRoom.transform;

                        newRoom.GetComponent<Room>().UpdateRoom(currentCell.status);
                        newRoom.name+=$"Ex room {i} - {j}";
                    }else if(currentCell.visited){
                        GameObject roomCreate=listRooms[Random.Range(0,listRooms.Count)];
                        GameObject newRoom=Instantiate(roomCreate,roomPosition,Quaternion.identity,holdRooms);
                        newRoom.SetActive(true);

                        gridBroad[i+j*size.x].position=newRoom.transform;

                        newRoom.GetComponent<Room>().UpdateRoom(currentCell.status);
                        newRoom.GetComponent<Room>().StartSpawningEnemies();
                        
                        int indeRroomHasCreate= newRoom.GetComponentInChildren<TypeRoom>().ChooseRandomType();
                        roomCreate.GetComponentInChildren<TypeRoom>().RemoveType(indeRroomHasCreate);

                        if(newRoom.GetComponent<Room>().deleteWhenHasCreate){
                            listRooms.Remove(roomCreate);
                        }
                        newRoom.name+=$"Main room {i} - {j}";
                    }
                    
                }
            }
        }
    }
    private void GeneraterMaze(){
        List<int> canCreateexRoom=new List<int>();
        gridBroad=new List<Cell>();
        for(int i=0;i<size.x;i++){
            for (int j = 0; j < size.y; j++)
            {
                gridBroad.Add(new Cell());
            }
        }
        int currentCell=startPoint;
        gridBroad[currentCell].startingRoom=true;
        Stack<int> path=new Stack<int>();
        int k=0;
        List<int> previousCellneighbors=new List<int>();
        while(k<1000){
            k++;
            gridBroad[currentCell].visited=true;
            if(currentRoom>=randomNumberRooms-1){
                gridBroad[currentCell].lastRoom=true;
                break;
            }
            List<int> neighbors=CheckNullNeighbors(currentCell);
            
            if(neighbors.Count==0){
                if(path.Count==0){
                    break;
                }
                else{
                    currentCell=path.Pop();
                }
            }
            else
            {
                currentRoom+=1;
                path.Push(currentCell);
                if(currentCell!=startPoint)
                    canCreateexRoom.Add(currentCell);
                int newCell = neighbors[Random.Range(0, neighbors.Count)];
                if (newCell > currentCell){
                    //right down
                    if (newCell - 1 == currentCell)
                    {
                        gridBroad[currentCell].status[2] = true;
                        currentCell = newCell;
                        gridBroad[currentCell].status[3] = true;

                    }
                    else
                    {
                        gridBroad[currentCell].status[1] = true;
                        currentCell = newCell;
                        gridBroad[currentCell].status[0] = true;

                    }
                }
                else
                {
                    //left up
                    if (newCell + 1 == currentCell)
                    {
                        gridBroad[currentCell].status[3] = true;
                        currentCell = newCell;
                        gridBroad[currentCell].status[2] = true;

                    }
                    else
                    {
                        gridBroad[currentCell].status[0] = true;
                        currentCell = newCell;
                        gridBroad[currentCell].status[1] = true;

                    }
                }
                
            }

        }


        foreach (var item in canCreateexRoom)
        {
            if(curentExRoom==maxNumberExRoom) break;
            if(maxNumberExRoom<=0) break;
            previousCellneighbors=CheckNullNeighbors(item);
            foreach (var item2 in previousCellneighbors)
            {
                if(curentExRoom==maxNumberExRoom) break;
                if(previousCellneighbors.Count==0) break;
                
                if(TryGenerateExtraRoom(item2,item)){
                    gridBroad[item2].canCreateExRoom=true;
                    gridBroad[item2].visited=true;
                    break;
                
                }
            }
                
        }
        GenerateDungeon();
    }

    private bool TryGenerateExtraRoom(int _originRoom, int _roomCreate){
        if(Random.value>=rateSpawnExRoom) return false;
        if (_roomCreate > _originRoom){
                //right down
            if (_roomCreate - 1 == _originRoom)
            {
                gridBroad[_originRoom].status[2] = true;
                _originRoom = _roomCreate;
                gridBroad[_originRoom].status[3] = true;

            }
            else
            {
                gridBroad[_originRoom].status[1] = true;
                _originRoom = _roomCreate;
                gridBroad[_originRoom].status[0] = true;

            }
        }
        else
        {
            //left up
            if (_roomCreate + 1 == _originRoom)
            {
                gridBroad[_originRoom].status[3] = true;
                _originRoom = _roomCreate;
                gridBroad[_originRoom].status[2] = true;

            }
            else
            {
                gridBroad[_originRoom].status[0] = true;
                _originRoom = _roomCreate;
                gridBroad[_originRoom].status[1] = true;

            }
        }
            curentExRoom++;
            return true;
    }


    private List<int> CheckHasNeighbors(int _cell){
        List<int> neighbors=new List<int>();


        //check up
        if((_cell-size.x>=0 && gridBroad[(_cell-size.x)].visited)||(_cell-size.x>=0 && gridBroad[(_cell-size.x)].canCreateExRoom)){
            neighbors.Add((_cell-size.x));
        }
        //check down
        if((_cell+size.x < gridBroad.Count && gridBroad[(_cell+size.x)].visited)||(_cell+size.x < gridBroad.Count && gridBroad[(_cell+size.x)].canCreateExRoom)){
            neighbors.Add((_cell+size.x));
        }
        //check right
        if((_cell+1)%size.x!=0 && (gridBroad[(_cell+1)].visited)||((_cell+1)%size.x!=0 && (gridBroad[(_cell+1)].canCreateExRoom))){
            neighbors.Add((_cell+1));
        }
        //check left
        if((_cell%size.x!=0 && gridBroad[(_cell-1)].visited)||(_cell%size.x!=0 && gridBroad[(_cell-1)].canCreateExRoom)){
            neighbors.Add((_cell-1));
        }
        return neighbors;
    }
    private List<int> CheckNullNeighbors(int _cell){
        List<int> neighbors=new List<int>();

         //check up
        if((_cell-size.x>=0 && !gridBroad[(_cell-size.x)].visited)){
            neighbors.Add((_cell-size.x));
        }
        //check down
        if((_cell+size.x < gridBroad.Count && !gridBroad[(_cell+size.x)].visited)){
            neighbors.Add((_cell+size.x));
        }
        //check right
        if((_cell+1)%size.x!=0 && (!gridBroad[(_cell+1)].visited)){
            neighbors.Add((_cell+1));
        }
        //check left
        if((_cell%size.x!=0 && !gridBroad[(_cell-1)].visited)){
            neighbors.Add((_cell-1));
        }



        return neighbors;
    }



    public void CheckDoorRoom(int _positioDoor, Transform _room,int _positionRoomCheck){
        _room.gameObject.GetComponent<Room>().doors[_positioDoor].gameObject.GetComponent<Door>().
        GetDoor(gridBroad[_positionRoomCheck].position.gameObject.GetComponent<Room>().positionSpawns[_positioDoor]);
        _room.gameObject.GetComponent<Room>().doors[_positioDoor].gameObject.GetComponent<Door>().
        GetRoom(GetRoom(_positionRoomCheck));
        return;
        
            

    }
    public Room GetRoom(int _position){
        return gridBroad[_position].position.gameObject.GetComponent<Room>();
    }
}


