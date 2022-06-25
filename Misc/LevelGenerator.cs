using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public GameObject rotationPivot;

    public int rotationInt = 0;
    public float xLength = 0;
    public float zLength = 0;


    public string [] _RoadBlocks;
    //Trees
    public string [] _Trees;
    public string [] _Signs;
    private string _StraightRoad;
    public GameObject _LongTurnRoad;
    public GameObject _TracksContainer;
    private string _StreetCross;
    // Start is called before the first frame update
    private string _BridgeRamp;
    private string _BridgeStraight;
    private string _BridgeBroken;

    private string _Environment;

    public float leftMovement = -20f;
    private float rotation = 0;
    private float correction = 12f;
    //Generation rules
    private int freeRight = 2;
    private int freeLeft = 2;
    
    private string [] track;
    private GameObject [] stage;

    private const string _StraightName = "Straight";
    private const string _BridgeStartName = "Straight";
    private const string _BridgeEndName = "Straight";
    private const string _BridgeStraightName = "Straight";
    private const string _BrokenBridgeStraightName = "BridgeBroken";
    private const string _CrossStraightName = "Cross";

    

 
    
    
    private const int _CROSS_COOLDOWN = 6;
    private const int _BRIGE_COOLDOWN = 6;

    private const int _CONSTRUCTION_COOLDOWN = 6;

    private int _crossCooldown;
    private int _bridgeCooldown;

    private int _constructionCooldown;

    private string _Firework;
    private string _FireworkHLoop;

    private string [] _Building;
    private bool [] _BuildingRotable;

    [Range(0,100)]
    public int TreesPerTrack;

    public bool CreateBridge ;
    public bool CreateCross ;
    public bool CreateConstructionRoad;
    public bool CreateBrokenBridge;
    public bool CreateBuildings;


    public float startX;
    public float startZ;
    public float endX;
    public float endZ;

    void Start()
    {
     
    }

    /*
    Codes:
        LL = Long Left
        LR = Long Right
        NS = Normal Straight
     */
    void Awake(){
      
        _Trees = new string [12];
        _Trees[0] = "Trees/Tree_A_V01";
        _Trees[1] = "Trees/Tree_B_V01_Leaves02";
        _Trees[2] = "Trees/Tree_C_V02";
        _Trees[3] = "Trees/Tree_D_V01";
        _Trees[4] = "Trees/Tree_D_V02";
        _Trees[5] = "Trees/Tree_E_V01";
        _Trees[6] = "Trees/Tree_E_V02";
        _Trees[7] = "Trees/Bush_A";
        _Trees[8] = "Trees/Bush_B_Leaves03";
        _Trees[9] = "Trees/Bush_C";
        _Trees[10] = "Trees/Bush_D";
        _Trees[11] = "Trees/Bush_E_Leaves03";

        _Signs = new string [2];
        _Signs[0] = "Props/HighwaySign_C";
        _Signs[1] = "Signs/ConstructionSign";
        
        
        _StraightRoad = "Roads/Road_Streight";
        _BridgeRamp = "Roads/Bridges/BridgeSlope_Simple";
        _BridgeStraight = "Roads/Bridges/Bridge_Simple_Straight";
        _BridgeBroken = "Roads/Bridges/Bridge_Damage";
        _StreetCross = "Roads/Road_Cross_A_A";

        _RoadBlocks = new string[1];
        _RoadBlocks[0] = "Obstacles/RoadBlock_A";

        _Environment = "Background/Environnment";

        _Firework = "Particles/StartExplosionDiagNoLoop";

        _FireworkHLoop = "Particles/StartExplosionVertNoLoop";


        _Building = new string [5];
        _Building[0] = "Buildings/urban_building_prefab";
        _Building[1] = "Buildings/AptsBuildingPrefab";
        _Building[2] = "Buildings/Building2";
        _Building[3] = "Buildings/Building3";
        _Building[4] = "Buildings/Building4";

        _BuildingRotable = new bool [5];
        _BuildingRotable[0] = true;
        _BuildingRotable[1] = false;
        _BuildingRotable[2] = true;
        _BuildingRotable[3] = false;
        _BuildingRotable[4] = true;
        //BuildLevelProcedural(150);
    //    BuildLevel(10);
    }

    // Update is called once per frame
    void Update()
    {

        //  _TracksContainer.transform.RotateAround(rotationPivot.transform.position, Vector3.up, 20 * Time.deltaTime);
    }

    public void BuildLevelProcedural(int size){

        GameObject last = BuildStraightV2(null, _StraightRoad,_StraightName);
        BuildEnd(last);

        int rightRot = 0;
        int leftRot = 0;
        for (int i = 0 ; i < size; i++){
            int randomVal = (int)Mathf.Round(Random.Range(0.0f, 22.0f));
            if (randomVal >= 0 && randomVal <= 4 && leftRot < 2){
                leftRot++;
                rightRot = rightRot > 0 ? rightRot-1 : 0;
                last = BuildLeftV2(last.name);
            }   
            else if(randomVal >= 5 && randomVal <= 8 && rightRot < 2 ){
                rightRot++;
                leftRot = leftRot > 0 ? leftRot-1 : 0;
                last = BuildRightV2(last.name);
            }
            else if (CreateBridge && randomVal >= 9 && randomVal <= 11 && _bridgeCooldown <= 0){
                last = BuildBridgeRamp(last.name,true);
                int bridgeSize = (int)Mathf.Round(Random.Range(1.0f, 4.0f));
                for(int j = 0; j < bridgeSize ; j++){

                    int noBroken = (int)Mathf.Round(Random.Range(0.0f, 10.0f));
                    if(noBroken >= 0f && noBroken <= 8f || !CreateBrokenBridge){
                       last = BuildStraightV2(last.name,_BridgeStraight,_BridgeStraightName);
                    }
                    else{
                        last = BuildStraightV2(last.name,_BridgeBroken,_BrokenBridgeStraightName);
                    } 
                }
                last = BuildBridgeRamp(last.name,false);
                _bridgeCooldown = _BRIGE_COOLDOWN;
            }
            else if (CreateCross && randomVal >= 12 && randomVal <= 14 && _crossCooldown <= 0 ){
                last = BuildStraightV2(last.name,_StreetCross,_CrossStraightName);
                _crossCooldown = _CROSS_COOLDOWN;
            }
            else if (CreateConstructionRoad && randomVal >= 15 && randomVal <= 17 && _constructionCooldown <= 0){
                int  constructionParts = (int)Mathf.Round(Random.Range(3f, 9f));
                last =  BuildConstructionPath(constructionParts ,  last);
                _constructionCooldown = _CONSTRUCTION_COOLDOWN;
            }
            else{
                last = BuildStraightV2(last.name,_StraightRoad,_StraightName);
            }
            
            _constructionCooldown--;
            _bridgeCooldown--;
            _crossCooldown--;
        }
        last = BuildStraightV2(last.name,_StraightRoad,_StraightName);
        last = BuildStraightV2(last.name,_StraightRoad,_StraightName);

        BuildStart(last);
        RotateLevelForStart();

        startX = _TracksContainer.transform.position.x;
        startZ = _TracksContainer.transform.position.z;

    }

  
    void BuildLevel(int size){

        GameObject last = BuildStraightV2(null,_StraightRoad,_StraightName);
        last = BuildConstructionPath(8, last);
       
    }

    GameObject BuildConstructionPath(int size, GameObject last){
        GameObject sign = Instantiate(Resources.Load(_Signs[1]) as GameObject);
        Transform signTransform = sign.transform;
        signTransform.rotation = Quaternion.Euler(-90f,180f,90f);
        signTransform.position += Vector3.left * 5.5f;
        GameObject straight = null;
        for (int i = 0 ; i < size ; i++){
            straight = BuildStraightV2(last.name,_StraightRoad,"StraightC", false);
            Vector3 m_Size = straight.GetComponent<Collider>().bounds.size;
            if (i > 0)
                AddObstacles(straight, m_Size);
            signTransform.position += Vector3.forward * m_Size.z; 
            signTransform.parent = straight.transform;
            straight.transform.parent = _TracksContainer.transform;
            MoveStraight(m_Size.z,0);
        }
        return straight;
    }

    void AddObstacles(GameObject straight, Vector3 m_size, int qtyPerRoad = 3){
        float [] obstaclePosition = {1f, m_size.z/2, m_size.z-0.5f} ;
        float [] obstacleXPosition = {0, 2f, -2f};
        for (int i = 0 ; i < qtyPerRoad ; i++){
            GameObject roadblock = Instantiate(Resources.Load(_RoadBlocks[0]) as GameObject);
            Rigidbody blockRB = roadblock.GetComponent<Rigidbody>();
            blockRB.isKinematic = true;
            Transform roadblockTransform = roadblock.transform;
            int xPos = (int)Random.Range(0.00f, 2.99f);
            roadblockTransform.position = new Vector3(obstacleXPosition[xPos],0.7f,obstaclePosition[i]);
            roadblockTransform.Rotate(0, 0f, 90f);
            roadblockTransform.localScale = new Vector3 (1f, 1.75f, 1f);
            roadblockTransform.parent =  straight.transform;
            blockRB.isKinematic = false;
        }
    }

    void RotateLevelForStart(){
        _TracksContainer.transform.RotateAround(rotationPivot.transform.position, Vector3.up, -180);
    }

    GameObject CreateFirework(float x, float y, float z, string fireworkPrefab = "Particles/StartExplosionDiagNoLoop"){
        GameObject firework =  Instantiate(Resources.Load(fireworkPrefab) as GameObject);
        firework.transform.position = new Vector3(x,y,z);
        return firework;
    }

   void BuildStart(GameObject last){
        last = BuildStraightV2(last.name,_StraightRoad,"Straight");
        Vector3 m_Size = last.GetComponent<Collider>().bounds.size;
        GameObject sign = AddSign(0,last);
        
        sign.transform.position += Vector3.forward * m_Size.z /2; 
        last = BuildStraightV2(last.name, _StraightRoad,_StraightName,false);
       
        last.AddComponent<Fireworks>();
        Fireworks fire = last.GetComponent<Fireworks>();

        GameObject [] startFireArray = new GameObject[2];

        GameObject firework = CreateFirework(m_Size.x/2,0,m_Size.z/2);
        firework.SetActive(false);
        firework.transform.parent = last.transform;
        startFireArray[0] = firework;

        firework = CreateFirework(-m_Size.x/2,0,m_Size.z/2);
        firework.SetActive(false);
        firework.transform.rotation = Quaternion.Euler(-45f,90f,0f);
        firework.transform.parent = last.transform;
        startFireArray[1] = firework;

        fire.StartFireworks = startFireArray;

        if (GameManager.instance != null)
            GameManager.instance.SetStartFireworks(fire);


        last.transform.parent = _TracksContainer.transform;
        MoveStraight(m_Size.z,0);
        last = BuildStraightV2(last.name, _StraightRoad,_StraightName);
        last = BuildStraightV2(last.name, _StraightRoad,_StraightName);
        MoveStraight(m_Size.z * -2, 0);
   }

   GameObject AddSign(int index, GameObject track){
        GameObject sign = Instantiate(Resources.Load(_Signs[index]) as GameObject);
        Transform signTransform = sign.transform;
        signTransform.position = new Vector3(0,0,0);
        signTransform.eulerAngles = new Vector3(-90,0,-180);
        signTransform.parent = track.transform;
        sign.name = "Sign";
        return sign;
        //sign.transform.localPosition = new Vector3(0,0f,1f);
   }

   void BuildEnd(GameObject last){
        last = BuildStraightV2(last.name,_StraightRoad,"Straight");
        last = BuildStraightV2(last.name,_StraightRoad,"Straight");
        last = BuildStraightV2(last.name,_StraightRoad,"Straight");
        last = BuildStraightV2(last.name,_StraightRoad,"Straight");
       
        Vector3 m_Size = last.GetComponent<Collider>().bounds.size;
        GameObject sign = AddSign(0,last);
        last.AddComponent<BoxCollider>();
        BoxCollider finishTrigger = last.GetComponent<BoxCollider>();
        finishTrigger.isTrigger = true;
        last.AddComponent<LevelOutroScript>();

        GameObject outro = new GameObject("LastCameraPosition");

        
        outro.transform.parent = last.transform;

        Vector3 movement = outro.transform.position + new Vector3(2.5f,0.79f,0);
        int absRotation = rotationInt%4;
        float modifier = 28f;
       
        movement += modifier*transform.forward;
           
        outro.transform.position = movement; 
        if (GameManager.instance != null)
            GameManager.instance.CameraLastPosition = outro;

        sign.transform.position += Vector3.forward * m_Size.z /2; 
        last = BuildStraightV2(last.name, _StraightRoad,_StraightName,false);
      
        GameObject [] fireworks = new GameObject[2];
        fireworks[0] = CreateFirework(m_Size.x/2,0,m_Size.z/2, _FireworkHLoop);
        fireworks[0].transform.parent = last.transform;
        fireworks[1] = CreateFirework(-m_Size.x/2,0,m_Size.z/2,_FireworkHLoop);
        fireworks[1].transform.parent = last.transform;
        last.transform.parent = _TracksContainer.transform;

        MoveStraight(m_Size.z,0);
        
        last = BuildStraightV2(last.name, _StraightRoad,_StraightName);
        last.AddComponent<BoxCollider>();
        BoxCollider endTrigger = last.GetComponent<BoxCollider>();
        last.AddComponent<FireworksTrigger>();
        FireworksTrigger fireworksInstance = last.GetComponent<FireworksTrigger>();
        endTrigger.isTrigger = true;
        fireworksInstance.Fireworks = fireworks;
        fireworksInstance.SetFireworksStatus(false);
        last = BuildStraightV2(last.name, _StraightRoad,_StraightName);

   }

    void AddVegetation(GameObject track)
    {
        for (int i = 0; i < TreesPerTrack; i++){
            int treeType = (int) Random.Range(0f, _Trees.Length);
            float xModifier = Random.Range(10.00f, 18.125f);
            float zModifier = Random.Range(0.00f, 18.125f);
            int xSign = Random.Range(0.00f, 1.00f) >= 0.5f ? 1 : -1;
            int zSign = Random.Range(0.00f, 1.00f) >= 0.5f ? 1 : -1;
            bool positioned = true;
            do {
                positioned = AddTree(treeType, track, xModifier*xSign, zModifier*zSign);
                xModifier += 1;
                zModifier += 1;
            }while (!positioned);  
        }
    }

    void AddBuilding(GameObject track, bool bothSides = true){
        int random = (int)Random.Range(0f, 4f);
        GameObject building = Instantiate(Resources.Load(_Building[random]) as GameObject); 
        Vector3 m_Size = track.GetComponent<Collider>().bounds.size;
        if (bothSides){
            Transform buildingTransform = building.transform;
            buildingTransform.position = new Vector3(-m_Size.x - 15f,0,0);
            buildingTransform.localScale = new Vector3(2f,2f,2f);
            if (_BuildingRotable[random]){
                int index = (int)Random.Range(0f, 3.99f);
                float [] rotations = {0f, 90f, 180f, 270f};
                buildingTransform.rotation =  Quaternion.Euler(-0f,rotations[index],0f);
            }
            buildingTransform.parent = track.transform;
        }
        else{
            //Pick one side random
        }
    }

    bool AddTree(int type, GameObject track, float xModifier, float zModifier)
    {
        bool added = true;
        GameObject tree = Instantiate(Resources.Load(_Trees[type]) as GameObject);
        Transform treeTransform = tree.transform;
        treeTransform.parent = track.transform;

        tree.name = "Tree";
        tree.tag = "Tree";
        float scale = Random.Range(1f, 3.25f);
        float objRotation = Random.Range(0f, 360f);

        tree.transform.position = new Vector3(0,0,0);
        tree.transform.localPosition = new Vector3(0,0f,1f);

        treeTransform.localPosition = new Vector3 (
            ((xModifier)) , 
            (zModifier) ,
            1f 
        );  

        treeTransform.localScale  = new Vector3 (scale,scale,scale);
        treeTransform.Rotate(Vector3.up * objRotation);

        RaycastHit hit;
        Ray collisionRay = new Ray(treeTransform.position , Vector3.down);
        Debug.DrawRay(treeTransform.position , Vector3.down, Color.red, 60);        
        if(Physics.Raycast(treeTransform.position , new Vector3(0,-1,0), out hit,50f ) ){
            if (hit.transform.tag == "Road"){
                Destroy(tree);
                added = false;
            }
            else{
                 treeTransform.localPosition = new Vector3 (
                    treeTransform.localPosition.x , 
                    treeTransform.localPosition.y ,
                    0f
                );  
            }  
        }
        return added;
    }

    GameObject BuildBridgeRamp(string previous, bool isStart){
        GameObject bridgeStart = Instantiate(Resources.Load(_BridgeRamp) as GameObject);
        Vector3 m_Size = bridgeStart.GetComponent<Collider>().bounds.size;  
        if(isStart) bridgeStart.transform.RotateAround(rotationPivot.transform.position, Vector3.up, -180);
        bridgeStart.transform.position = new Vector3(0f,0f,m_Size.z/2);
        string name = isStart ? "BridgeStart" : "BridgeEnd";
        bridgeStart.name = name;
        bridgeStart.tag = "Road";
        bridgeStart.layer = 9;
        float [] mov = this.GetMovementForStraight(previous);
        bridgeStart.transform.parent = _TracksContainer.transform;
        MoveStraight(m_Size.z,mov[0]);
        return bridgeStart;
    }


    GameObject BuildStreetCross(string previous){
        GameObject cross = Instantiate(Resources.Load(_StreetCross) as GameObject);
        Vector3 m_Size = cross.GetComponent<Collider>().bounds.size;  
        cross.transform.position = new Vector3(0f,0f,m_Size.z/2);
        //AddVegetation(cross);
        cross.name = "Cross";
        cross.tag = "Road";
        cross.layer = 9;
        float [] mov = this.GetMovementForStraight(previous);
        cross.transform.parent = _TracksContainer.transform;
        MoveStraight(m_Size.z,mov[0]);
        return cross;
    }

  

    GameObject BuildStraightV2(string previous, string prefab, string gameObjectName, bool addParent = true){
        GameObject straight =  Instantiate(Resources.Load(prefab) as GameObject);
        Vector3 m_Size = straight.GetComponent<Collider>().bounds.size;  
        straight.transform.position = new Vector3(0f,0f,m_Size.z/2);


        straight.AddComponent<Road>();
        Road instance = straight.GetComponent<Road>() as Road;
        instance.Rotation = rotationInt;


        AddVegetation(straight);
        if (CreateBuildings)
            AddBuilding(straight);
        straight.name = gameObjectName;
        straight.tag = "Road";
        straight.layer = 9;
        float [] mov = this.GetMovementForStraight(previous);
        if(addParent){
            straight.transform.parent = _TracksContainer.transform;
            MoveStraight(m_Size.z,mov[0]);
        }
            
        
        return straight;
    }

    void MoveStraight(float trackSize, float mov){
        int absRotation = rotationInt % 4;
        if (absRotation == 0){
            _TracksContainer.transform.position += -1*transform.forward * trackSize;
            rotationPivot.transform.localPosition += transform.forward * trackSize;
            rotationPivot.transform.localPosition += transform.right * mov;
        }
        else if (absRotation == -1 || absRotation == 3){
            _TracksContainer.transform.position += -1*transform.right * trackSize;
            rotationPivot.transform.localPosition += transform.forward*-1 * trackSize;
            rotationPivot.transform.localPosition += transform.right * mov;
        }
        else if (absRotation == -2 || absRotation == 2){
            _TracksContainer.transform.position += transform.forward * trackSize;
            rotationPivot.transform.localPosition += transform.forward * trackSize;
            rotationPivot.transform.localPosition += transform.right * mov;
        }
        else if (absRotation == -3 || absRotation == 1){
            _TracksContainer.transform.position += transform.right * trackSize;
            rotationPivot.transform.localPosition += transform.forward*-1 *trackSize;
        } 
    }

    void MoveRight(){
        int absRotation = rotationInt % 4;
        float [] pivotMovement = {52.51f,52.51f};
        if (absRotation == 0){
            rotationPivot.transform.localPosition += transform.forward  * (  pivotMovement[1]);
            rotationPivot.transform.localPosition += transform.right  * ( pivotMovement[0]);
        }
        if (absRotation == -1 || absRotation == 3){
            rotationPivot.transform.localPosition += transform.forward *-1* (pivotMovement[1]);
            rotationPivot.transform.localPosition += transform.right *-1* ( pivotMovement[0]);
        }
        if (absRotation == -2 || absRotation == 2){
            int sign = -1;
            if (absRotation == 2)
               sign = 1;
            rotationPivot.transform.localPosition += transform.forward * (pivotMovement[0]);
            rotationPivot.transform.localPosition += transform.right *sign* ( pivotMovement[1]);
        }
        if (absRotation == -3 || absRotation == 1){
            rotationPivot.transform.localPosition += transform.forward *-1* ( pivotMovement[1]);
            rotationPivot.transform.localPosition += transform.right *-1*( pivotMovement[0]);
        }
        _TracksContainer.transform.RotateAround(rotationPivot.transform.position, Vector3.up, -90);
    }

    GameObject BuildRightV2(string previous){
        GameObject right = (GameObject)GameObject.Instantiate(_LongTurnRoad);
        right.name = "LongRight";
        right.tag = "Road";
        right.layer = 9;

        AddVegetation(right);

        Vector3 m_Size = right.GetComponent<Collider>().bounds.size; 
       // float [] mov = this.GetMovementForLongRight(previous);
        right.transform.position = new Vector3(15.35f,0f,37f);
        right.transform.parent = _TracksContainer.transform;
        
        MoveRight();

        rotationInt -= 1;
        rotationInt %=4;

        float [] fix = new float[2];
        fix[0] = 52.51f;
        fix[1] = 52.51f;

        _TracksContainer.transform.position += new Vector3(-fix[0],0,-fix[1]);
     
        return right;
    }

    
    GameObject BuildLeftV2(string previous){
        GameObject right = (GameObject)GameObject.Instantiate(_LongTurnRoad);
        right.name = "LongLeft";
        right.tag = "Road";
        right.layer = 9;

        AddVegetation(right);
        Vector3 m_Size = right.GetComponent<Collider>().bounds.size; 
      //  float [] mov = this.GetMovementForLongRight(previous);
        right.transform.position = new Vector3(15.35f,0f,37f);
        right.transform.RotateAround(rotationPivot.transform.position, Vector3.up, 90);
        right.transform.position += new Vector3(-52.5f,0,52.5f);
        right.transform.parent = _TracksContainer.transform;
        int absRotation = rotationInt % 4;
        float [] pivotMovement = {-52.51f,52.51f};
        // float [] pivotMovement = {0,0};
        if (absRotation == 0){
            rotationPivot.transform.localPosition += transform.forward  * (  pivotMovement[1]);
            rotationPivot.transform.localPosition += transform.right  * ( pivotMovement[0]);
        }
        if (absRotation == -1 || absRotation == 1){
            rotationPivot.transform.localPosition += transform.forward * (pivotMovement[0]);
            rotationPivot.transform.localPosition += transform.right * ( pivotMovement[1]);
        }
        if (absRotation == -2 || absRotation == 2){
            rotationPivot.transform.localPosition += transform.forward *-1* (pivotMovement[0]);
            rotationPivot.transform.localPosition += transform.right *-1* ( pivotMovement[1]);
        }
        if (absRotation == -3 || absRotation == 3){
            rotationPivot.transform.localPosition += transform.forward *-1* ( pivotMovement[1]);
            rotationPivot.transform.localPosition += transform.right *-1*( pivotMovement[0]);
        }
        
        _TracksContainer.transform.RotateAround(rotationPivot.transform.position, Vector3.up, 90);

        rotationInt += 1;
        rotationInt %=4;

        float [] fix = new float[2];
        fix[0] = 52.51f;
        fix[1] = 52.51f;

        _TracksContainer.transform.position += new Vector3(fix[0],0,-fix[1]);
     
        return right;
    }

    float [] GetCoordinatesMovement(float x, float z){
        float zMov =0;
        float xMov =0;
        if(rotationInt == -1){
            zMov = z;
            xMov = x;
        }
        else if (rotationInt  == 0){
            zMov = z;
            xMov = x;
        }
        else{
            zMov = x;
            xMov = z;
        }
        float [] coords = {xMov,zMov};
        return coords;
    }

    float [] GetMovementForStraight(string prev){
        float [] mov = new float[2];
        if (prev == null){
            mov[0] = 0f;
            mov[1] = 10f;
        }
        return mov;
    }

}
