using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotArrayGeneration : MonoBehaviour {
    public int delay;
    public int NumberOfGenerationsToDo = 0;
    public int numOfCreatures = 25;
    public int sizeOfCreaturesX = 5;
    public int sizeOfCreaturesY = 5;
    public int sizeOfCreaturesZ = 5;
    public float densityOfBlocks = 0.2f;
    public float stabilizationChance = 0.1f;
    public float minimumWeight = 2f;
    public float maximumWeight = 5f;
    public float minimumJointForce = 500f;
    public float maximumJointForce = 800f;
    public float minimumJointSpeed = 200f;
    public float maximumJointSpeed = 800f;
    
    // Editable members of the segment/joint population
    public GameObject[] SegmentTypeList;
    public GameObject[] JointTypeList;

    int GenerationCount = 0;
    
    DNA[] Creatures;
    List<DNA> WinningCreatures = new List<DNA>();

    public class Connection
    {
        private GameObject conType;
        private bool isFixedJoint; // Special condition if the joint is a fixed joint
        private float conStr;
        private float conSpeed;
        private Vector3 conAxis;

        // Constructor
        public Connection()
        {
            conType = null;
            isFixedJoint = false;
            conStr = 0;
            conSpeed = 0;
            conAxis = Vector3.zero;
        }
        // Instantiator
        public GameObject InstantiateConnection(Vector3 location, Quaternion rotation) 
        {
            GameObject connection = Instantiate(conType, location, rotation);
            return connection;
        }
        // Setter

        public void SetConType(GameObject type)
        {
            conType = type;
        }
        public void SetFixedJoint(bool state) 
        {
            isFixedJoint = state;
        }
        public void SetConStr(float strength)
        {
            conStr = strength;
        }
        public void SetConSpeed(float speed)
        {
            conSpeed = speed;
        }
        public void SetConAxis(Vector3 axis) {
            conAxis = axis;
        }
        // Getter
        public GameObject GetConType()
        {
            return conType;
        }
        public bool GetIsFixedJoint() 
        {
            return isFixedJoint;
        }
        public float GetConStr()
        {
            return conStr;
        }
        public float GetConSpeed()
        {
            return conSpeed;
        }
        public Vector3 GetConAxis() {
            return conAxis;
        }
    }
    public class Block
    {
        private Vector3 blockPosition;
        private GameObject blockType; 
        private float blockWeight;
        private bool stabilized;
        private Color blockColor;

        private Connection posXCon;
        private Connection posYCon;
        private Connection posZCon;

        // Constructor
        public Block()
        {
            blockPosition = Vector3.zero;
            blockType = null;
            blockWeight = 0;
            stabilized = false;
            posXCon = new Connection();
            posYCon = new Connection();
            posZCon = new Connection();
            blockColor = new Color(1, 1, 1, 1);
        }
        // Instantiator
        public GameObject InstantiateBlock(Vector3 position) {
            GameObject block = Instantiate(blockType, position, new Quaternion(0, 0, 0, 0));
            return block;
        }
        // Setters
        public void SetBlockType(GameObject type)
        {
            blockType = type;
        }
        public void SetBlockPosition(Vector3 posVector)
        {
            blockPosition = posVector;
        }
        public void SetBlockWeight(float weight)
        {
            blockWeight = weight;
        }
        public void SetBlockStabilized(bool stab)
        {
            stabilized = stab;
        }
        public void SetBlockColor(Color a) {
            blockColor = a;
        }

        // Getter
        public GameObject GetBlockType()
        {
            return blockType;
        }
        public Vector3 GetBlockPosition()
        {
            return blockPosition;
        }
        public float GetBlockWeight()
        {
            return blockWeight;
        }
        public bool GetBlockStabilized()
        {
            return stabilized;
        }
        public Connection GetPosXCon()
        {
            return posXCon;
        }
        public Connection GetPosYCon()
        {
            return posYCon;
        }
        public Connection GetPosZCon()
        {
            return posZCon;
        } 
        public Color GetBlockColor() {
            return blockColor;
        }
    }
    public class DNA
    {
        public Block[,,] arrayOfBlocks;
        string CreatureName;
        float fitness;
        private Vector3 startingPosition;

        // Function to get block objects
        public Block getBlock(int x, int y, int z) {
            return arrayOfBlocks[x, y, z];
        }
        // Function to print a DNA object to a string.
        public string GenerateGeneticString() {

            string geneOpen = "{";
            string geneClose = "}";
            string blockOpen = "[";
            string blockClose = "]";
            string setOpen = "(";
            string setClose = ")";
            string sequence = ",";

            string gs = "";
            Concat(gs, geneOpen); // Start off the gene
            Concat(gs, "x" + arrayOfBlocks.GetLength(0) + "y" + arrayOfBlocks.GetLength(1) + "z" + arrayOfBlocks.GetLength(2)); // save the size of the DNA Array

            for (int i = 0; i < arrayOfBlocks.GetLength(0); i++)
            {
                for (int j = 0; j < arrayOfBlocks.GetLength(1); j++)
                {
                    for (int k = 0; k < arrayOfBlocks.GetLength(2); k++)
                    {
                        // Code for the block
                        Concat(gs, blockOpen);
                        Concat(gs, setOpen);
                        Concat(gs, i.ToString()); // save the position vector
                        Concat(gs, sequence);
                        Concat(gs, j.ToString());
                        Concat(gs, sequence);
                        Concat(gs, k.ToString());
                        Concat(gs, setClose);
                        Concat(gs, setOpen);
                        Concat(gs, arrayOfBlocks[i, j, k].GetBlockType().ToString());
                        Concat(gs, sequence);
                        Concat(gs, arrayOfBlocks[i, j, k].GetBlockStabilized().ToString());
                        Concat(gs, setClose);
                        Concat(gs, blockClose);
                        // todo, None of the above works at all!
                        // todo, loop through all the blocks and build a big string with all data
                    }
                }
            }
            Concat(gs, geneClose);
            return gs;
        }
        private string Concat(string s, string c) {
            return s += c;
        }
        // Parents a child object by sending both objects
        private void ParentChild(GameObject par, GameObject child) {
            child.transform.parent = par.transform;
        }
        // gets the name of the object
        public string GetCreatureName() {
            return CreatureName;
        }
        public void SetCreatureName(string name) {
            CreatureName = name;
        }
        public Vector3 GetStartingPosition() {
            return startingPosition;
        }
        // Function to Instantiate a DNA Object into the Unity World
        public void InstantiateDNAasUnityCreature(Vector3 instantiationPosition) {
            GameObject CreatureParentObject = new GameObject(CreatureName);
            CreatureParentObject.GetComponent<Transform>().position = instantiationPosition;
            startingPosition = instantiationPosition; // This is used to score the robot

            // Creating the blocks and instantiating them
            foreach (Block blockObject in arrayOfBlocks)
            {
                Vector3 instantiationVector = blockObject.GetBlockPosition() + instantiationPosition;
                if (blockObject.GetBlockType() != null)
                {
                    GameObject tempSegment = blockObject.InstantiateBlock(instantiationVector);
                    tempSegment.name = CreatureName + "x" + blockObject.GetBlockPosition().x + "y" + blockObject.GetBlockPosition().y + "z" + blockObject.GetBlockPosition().z;
                    tempSegment.GetComponent<Rigidbody>().mass = blockObject.GetBlockWeight();

                    // Change color                            
                    tempSegment.GetComponent<Renderer>().material.SetColor("_Color", blockObject.GetBlockColor());

                    // Parent it
                    ParentChild(CreatureParentObject, tempSegment);

                    if (blockObject.GetBlockStabilized() == true)
                    {
                        Rigidbody stablebody = tempSegment.GetComponent<Rigidbody>();
                        stablebody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                    }
                }
            }

            // Jointing the objects in the world
            for (int i = 0; i < arrayOfBlocks.GetLength(0); i++)
            {
                for (int j = 0; j < arrayOfBlocks.GetLength(1); j++)
                {
                    for (int k = 0; k < arrayOfBlocks.GetLength(2); k++)
                    {
                        //Fixed X Joint
                        if (arrayOfBlocks[i, j, k].GetPosXCon().GetIsFixedJoint() == true)
                        {
                            int temp = i + 1;
                            string name = CreatureName + "x" + i + "y" + j + "z" + k;
                            string name2 = CreatureName + "x" + temp + "y" + j + "z" + k;
                            GameObject.Find(name).AddComponent<FixedJoint>().connectedBody = GameObject.Find(name2).GetComponent<Rigidbody>();

                        }
                        else if (arrayOfBlocks[i, j, k].GetPosXCon().GetConType() != null)
                        {
                            // Non-Fixed X Joint
                            string curBlockName = CreatureName + "x" + i + "y" + j + "z" + k;
                            int temp = i + 2;
                            string blockToBeConnectedName = CreatureName + "x" + temp + "y" + j + "z" + k;
                            // instantiate the joint object at the vector below, this is the location of where the next block would be if it existed.
                            Vector3 instantiationVector = arrayOfBlocks[i + 1, j, k].GetBlockPosition() + instantiationPosition;


                            // The connectionobject is the actual joint object, which has a hinge joint on it
                            GameObject xconnectionobject = arrayOfBlocks[i, j, k].GetPosXCon().InstantiateConnection(instantiationVector, Quaternion.Euler(new Vector3(0, 0, 90)));
                            // name the connection object the current blocks name, and the description of the location
                            xconnectionobject.name = curBlockName + "posXCon";
                            // The hingejoint on the connection object is then retreived and its target is set to the block next to it
                            xconnectionobject.GetComponent<HingeJoint>().connectedBody = GameObject.Find(blockToBeConnectedName).GetComponent<Rigidbody>();
                            // ANd the current block is given a hinge joint that connects to the connection object, this is where the str/speed is applied

                            HingeJoint xPosHJ = GameObject.Find(curBlockName).AddComponent<HingeJoint>();
                            xPosHJ.axis = arrayOfBlocks[i, j, k].GetPosXCon().GetConAxis();
                            xPosHJ.connectedBody = xconnectionobject.GetComponent<Rigidbody>();


                            JointMotor xPosMotor = xPosHJ.motor;
                            xPosMotor.force = arrayOfBlocks[i, j, k].GetPosXCon().GetConStr();
                            xPosMotor.targetVelocity = arrayOfBlocks[i, j, k].GetPosXCon().GetConSpeed();

                            xPosHJ.motor = xPosMotor;
                            xPosHJ.useMotor = true;

                            ParentChild(CreatureParentObject, xconnectionobject);
                        }
                        // Fixed Y Joint
                        if (arrayOfBlocks[i, j, k].GetPosYCon().GetIsFixedJoint() == true)
                        {

                            int temp = j + 1;
                            string name = CreatureName + "x" + i + "y" + j + "z" + k;
                            string name2 = CreatureName + "x" + i + "y" + temp + "z" + k;
                            GameObject.Find(name).AddComponent<FixedJoint>().connectedBody = GameObject.Find(name2).GetComponent<Rigidbody>();

                        }
                        else if (arrayOfBlocks[i, j, k].GetPosYCon().GetConType() != null)
                        {
                            // Non-Fixed Y Joint
                            //   string name = "x" + i + "y" + j + "z" + k;
                            //   Vector3 instantiationVector = arrayOfBlocks[i+1, j, k].GetBlockPosition() + instantiationPosition;
                            // Non-Fixed X Joint
                            string curBlockName = CreatureName + "x" + i + "y" + j + "z" + k;
                            int temp = j + 2;
                            string blockToBeConnectedName = CreatureName + "x" + i + "y" + temp + "z" + k;
                            // instantiate the joint object at the vector below, this is the location of where the next block would be if it existed.
                            Vector3 instantiationVector = arrayOfBlocks[i, j + 1, k].GetBlockPosition() + instantiationPosition;


                            // The connectionobject is the actual joint object, which has a hinge joint on it
                            GameObject yconnectionobject = arrayOfBlocks[i, j, k].GetPosYCon().InstantiateConnection(instantiationVector, Quaternion.Euler(new Vector3(0, 0, 0)));
                            // name the connection object the current blocks name, and the description of the location
                            yconnectionobject.name = curBlockName + "posYCon";
                            // The hingejoint on the connection object is then retreived and its target is set to the block next to it
                            yconnectionobject.GetComponent<HingeJoint>().connectedBody = GameObject.Find(blockToBeConnectedName).GetComponent<Rigidbody>();
                            // ANd the current block is given a hinge joint that connects to the connection object, this is where the str/speed is applied

                            HingeJoint yPosHJ = GameObject.Find(curBlockName).AddComponent<HingeJoint>();
                            yPosHJ.axis = arrayOfBlocks[i, j, k].GetPosYCon().GetConAxis();
                            yPosHJ.connectedBody = yconnectionobject.GetComponent<Rigidbody>();


                            JointMotor yPosMotor = yPosHJ.motor;
                            yPosMotor.force = arrayOfBlocks[i, j, k].GetPosYCon().GetConStr();
                            yPosMotor.targetVelocity = arrayOfBlocks[i, j, k].GetPosYCon().GetConSpeed();

                            yPosHJ.motor = yPosMotor;
                            yPosHJ.useMotor = true;

                            ParentChild(CreatureParentObject, yconnectionobject);
                        }

                        //Fixed Z Joint
                        if (arrayOfBlocks[i, j, k].GetPosZCon().GetIsFixedJoint() == true)
                        {

                            int temp = k + 1;
                            string name = CreatureName + "x" + i + "y" + j + "z" + k;
                            string name2 = CreatureName + "x" + i + "y" + j + "z" + temp;
                            GameObject.Find(name).AddComponent<FixedJoint>().connectedBody = GameObject.Find(name2).GetComponent<Rigidbody>();

                        }
                        else if (arrayOfBlocks[i, j, k].GetPosZCon().GetConType() != null)
                        {
                            // Non-Fixed Z Joint
                            //  string name = "x" + i + "y" + j + "z" + k;
                            // Non-Fixed X Joint
                            string curBlockName = CreatureName + "x" + i + "y" + j + "z" + k;
                            int temp = k + 2;
                            string blockToBeConnectedName = CreatureName + "x" + i + "y" + j + "z" + temp;
                            // instantiate the joint object at the vector below, this is the location of where the next block would be if it existed.
                            Vector3 instantiationVector = arrayOfBlocks[i, j, k + 1].GetBlockPosition() + instantiationPosition;


                            // The connectionobject is the actual joint object, which has a hinge joint on it
                            GameObject zconnectionobject = arrayOfBlocks[i, j, k].GetPosZCon().InstantiateConnection(instantiationVector, Quaternion.Euler(new Vector3(90, 0, 0)));
                            // name the connection object the current blocks name, and the description of the location
                            zconnectionobject.name = curBlockName + "posZCon";
                            // The hingejoint on the connection object is then retreived and its target is set to the block next to it
                            zconnectionobject.GetComponent<HingeJoint>().connectedBody = GameObject.Find(blockToBeConnectedName).GetComponent<Rigidbody>();
                            // ANd the current block is given a hinge joint that connects to the connection object, this is where the str/speed is applied

                            HingeJoint zPosHJ = GameObject.Find(curBlockName).AddComponent<HingeJoint>();
                            zPosHJ.axis = arrayOfBlocks[i, j, k].GetPosZCon().GetConAxis();
                            zPosHJ.connectedBody = zconnectionobject.GetComponent<Rigidbody>();


                            JointMotor zPosMotor = zPosHJ.motor;
                            zPosMotor.force = arrayOfBlocks[i, j, k].GetPosZCon().GetConStr();
                            zPosMotor.targetVelocity = arrayOfBlocks[i, j, k].GetPosZCon().GetConSpeed();

                            zPosHJ.motor = zPosMotor;
                            zPosHJ.useMotor = true;

                            ParentChild(CreatureParentObject, zconnectionobject);
                        }
                    }
                }
            }
        }
        // Removes the object from the unity world by finding the parenting object, and deleting it
        public void SelfDestruct() {
            Destroy(GameObject.Find(CreatureName));
        }
        // Function to create the data for the individual block data, based on information based into the DNA constructor
        private void InitializeDNABlockData(int x, int y, int z, float density, float minW, float maxW, float chanceOfStabilization, GameObject[] SegmentTypes) {
            arrayOfBlocks[x, y, z] = new Block();
            arrayOfBlocks[x, y, z].SetBlockPosition(new Vector3(x, y, z));

            // Decide if block exists
            if (Random.Range(0f, 1f) < density)
            {
                // Choose Segment Type
                arrayOfBlocks[x, y, z].SetBlockType(SegmentTypes[Random.Range(0, SegmentTypes.Length)]);

                // Choose Segment Weight
                arrayOfBlocks[x, y, z].SetBlockWeight(Random.Range(minW, maxW));

                // Determine the segment's Color
                float weightColorMod = (arrayOfBlocks[x, y, z].GetBlockWeight() - minW) / (maxW - minW);
                arrayOfBlocks[x, y, z].SetBlockColor(new Color(1 - weightColorMod, 1 - weightColorMod, 1 - weightColorMod, 1));

                // Choose if Segment is Stabalized, meaning the block will not rotate around any axis
                if (Random.Range(0f, 1f) < chanceOfStabilization)
                {
                    arrayOfBlocks[x, y, z].SetBlockStabilized(true);
                }
            }
        }
        public float GetFitness() {
            return fitness;
        }
        public void SetFitness(float fitnessScore) {
            fitness = fitnessScore;
        }
        // Constructor
        public DNA(string name, int ArrayX, int ArrayY, int ArrayZ, float densityOfBlocks, float stabilizedChance, float minWeight, float maxWeight, float minStr, float maxStr,
            float minSpeed, float maxSpeed, GameObject[] segtypes, GameObject[] jointtypes) {
            arrayOfBlocks = new Block[ArrayX, ArrayY, ArrayZ];
            CreatureName = name;

            for (int i = 0; i < ArrayX; i++)
            {
                for (int j = 0; j < ArrayY; j++)
                {
                    for (int k = 0; k < ArrayZ; k++)
                    {
                        InitializeDNABlockData(i, j, k, densityOfBlocks, minWeight, maxWeight, stabilizedChance, segtypes);
                    }
                }
            }
            // Check for connections of the blocks in the PosXCon direction;
            for (int i = 0; i < ArrayX; i++)
            {
                for (int j = 0; j < ArrayY; j++)
                {
                    for (int k = 0; k < ArrayZ; k++)
                    {
                        if (arrayOfBlocks[i, j, k].GetBlockType() != null) // if we have a block, is there another block 1 space, or 2 spaces away
                        {
                            // This is code for if there is a block in the xpos direction
                            // we check to make sure we dont go off the array;
                            if (i + 1 < ArrayX)
                            {
                                if (arrayOfBlocks[i + 1, j, k].GetBlockType() != null)
                                {
                                    arrayOfBlocks[i, j, k].GetPosXCon().SetFixedJoint(true);
                                }
                                else if (i + 2 < ArrayX) // If the block is two spaces away we add a mechanical joint to the block in the posx direction
                                {
                                    if (arrayOfBlocks[i + 2, j, k].GetBlockType() != null)
                                    {
                                        arrayOfBlocks[i, j, k].GetPosXCon().SetConType(jointtypes[Random.Range(0, jointtypes.Length)]);
                                        arrayOfBlocks[i, j, k].GetPosXCon().SetConStr(Random.Range(minStr, maxStr));
                                        arrayOfBlocks[i, j, k].GetPosXCon().SetConSpeed(Random.Range(minSpeed, maxSpeed));
                                        arrayOfBlocks[i, j, k].GetPosXCon().SetConAxis(new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), Random.Range(-1, 2)));
                                    }
                                }
                            }
                            // This is the code for if there is a block in the ypos direction
                            // we check to make sure we dont go off the array
                            if (j + 1 < ArrayY)
                            {
                                if (arrayOfBlocks[i, j + 1, k].GetBlockType() != null)
                                {
                                    arrayOfBlocks[i, j, k].GetPosYCon().SetFixedJoint(true);
                                }
                                else if (j + 2 < ArrayY)
                                {
                                    if (arrayOfBlocks[i, j + 2, k].GetBlockType() != null)
                                    {
                                        arrayOfBlocks[i, j, k].GetPosYCon().SetConType(jointtypes[Random.Range(0, jointtypes.Length)]);
                                        arrayOfBlocks[i, j, k].GetPosYCon().SetConStr(Random.Range(minStr, maxStr));
                                        arrayOfBlocks[i, j, k].GetPosYCon().SetConSpeed(Random.Range(minSpeed, maxSpeed));
                                        arrayOfBlocks[i, j, k].GetPosYCon().SetConAxis(new Vector3(Random.Range(0, 2), Random.Range(0, 2), Random.Range(0, 2)));
                                    }
                                }
                            }
                            // This is the code for if there is a block in the zpos direction
                            // we check to make sure wedont go off the array
                            if (k + 1 < ArrayZ)
                            {
                                if (arrayOfBlocks[i, j, k + 1].GetBlockType() != null)
                                {
                                    arrayOfBlocks[i, j, k].GetPosZCon().SetFixedJoint(true);
                                }
                                else if (k + 2 < ArrayZ) // check to see if we go off the array again
                                {
                                    if (arrayOfBlocks[i, j, k + 2].GetBlockType() != null)
                                    {
                                        arrayOfBlocks[i, j, k].GetPosZCon().SetConType(jointtypes[Random.Range(0, jointtypes.Length)]);
                                        arrayOfBlocks[i, j, k].GetPosZCon().SetConStr(Random.Range(minStr, maxStr));
                                        arrayOfBlocks[i, j, k].GetPosZCon().SetConSpeed(Random.Range(minSpeed, maxSpeed));
                                        arrayOfBlocks[i, j, k].GetPosZCon().SetConAxis(new Vector3(Random.Range(0, 2), Random.Range(0, 2), Random.Range(0, 2)));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            // Purge the system of solo blocks
            foreach (Block blockObject in arrayOfBlocks)
            {
                int i = (int)blockObject.GetBlockPosition().x;
                int j = (int)blockObject.GetBlockPosition().y;
                int k = (int)blockObject.GetBlockPosition().z;

                if (arrayOfBlocks[i, j, k].GetBlockType() != null)
                {
                    bool die = true;
                    // xdirection checks
                    if (i - 2 >= 0)
                    {
                        if (arrayOfBlocks[i - 2, j, k].GetBlockType() != null)
                        {
                            die = false;
                        }
                    }
                    if (i - 1 >= 0)
                    {
                        if (arrayOfBlocks[i - 1, j, k].GetBlockType() != null)
                        {
                            die = false;
                        }
                    }
                    if (arrayOfBlocks[i, j, k].GetPosXCon().GetConType() != null)
                    {
                        die = false;
                    }
                    // ydirection
                    if (j - 2 >= 0)
                    {
                        if (arrayOfBlocks[i, j - 2, k].GetBlockType() != null)
                        {
                            die = false;
                        }
                    }
                    if (j - 1 >= 0)
                    {
                        if (arrayOfBlocks[i, j - 1, k].GetBlockType() != null)
                        {
                            die = false;
                        }
                    }
                    if (arrayOfBlocks[i, j, k].GetPosYCon().GetConType() != null)
                    {
                        die = false;
                    }
                    // zposistion
                    if (k - 2 >= 0)
                    {
                        if (arrayOfBlocks[i, j, k - 2].GetBlockType() != null)
                        {
                            die = false;
                        }
                    }
                    if (k - 1 >= 0)
                    {
                        if (arrayOfBlocks[i, j, k - 1].GetBlockType() != null)
                        {
                            die = false;
                        }
                    }
                    if (arrayOfBlocks[i, j, k].GetPosZCon().GetConType() != null)
                    {
                        die = false;
                    }
                    if (die == true)
                    {
                        arrayOfBlocks[i, j, k] = new Block();
                    }
                }
            }
        }
    }
    void Start()
    {
        Time.timeScale = 3;
        Creatures = CreateRandomGeneration();
        int i = 0;
        StartCoroutine(Example(delay));


        //Time.fixedDeltaTime = 0.02F * Time.timeScale;

        // TODO mutation - eliminate stagnant pieces as part of the mutation process
    } // end of start code
    public void Round() {
        Creatures = CreateRandomGeneration();
        StartCoroutine(Example(delay));
    }
    public DNA[] CreateRandomGeneration()
    {

        DNA[] Creatures = new DNA[numOfCreatures];
        for (int i = 0; i < Creatures.GetLength(0); i++)
        {
            Creatures[i] = new DNA("Creature" + i.ToString(), sizeOfCreaturesX, sizeOfCreaturesY, sizeOfCreaturesZ, densityOfBlocks, stabilizationChance, minimumWeight, maximumWeight, minimumJointForce,
                maximumJointForce, minimumJointSpeed, maximumJointSpeed, SegmentTypeList, JointTypeList);
        }
        int k = 0;
        for (int i = 0; i < Mathf.Sqrt(numOfCreatures); i++)
        {
            for (int j = 0; j < Mathf.Sqrt(numOfCreatures); j++)
            {
                Creatures[k].InstantiateDNAasUnityCreature(new Vector3(50 * i, 0, 50 * j));
                k++;
            }
        }
        return Creatures;
    }
    private DNA GradeAllCreatures() {
        DNA winner = null;
        float WinnerWork = 0;
        foreach (DNA individual in Creatures)
        {
            Vector3 startTemp = individual.GetStartingPosition();
            float maxWork = 0;
            foreach (Transform child in GameObject.Find(individual.GetCreatureName()).transform)
            {
                maxWork = child.GetComponent<Rigidbody>().mass * (child.position.z - startTemp.z);                 // Judgement Vector for grading the fitness
            }

            individual.SetFitness(maxWork);
            if (individual.GetFitness() > WinnerWork)
            {
                winner = individual;
                WinnerWork = individual.GetFitness();
            }
            individual.SelfDestruct();
        }

        return winner;
    }
    private void PresentWinningCreatures() {
        int counter = 0;
        foreach( DNA indiv in WinningCreatures)
        {
            indiv.SetCreatureName("Winner" + counter);
            counter++;
        }
        int k = 0;
        DNA[] array = WinningCreatures.ToArray();
        for (int i = 0; i < Mathf.Sqrt(array.Length); i++)
        {
            for (int j = 0; j < Mathf.Sqrt(array.Length); j++)
            {
                array[k].InstantiateDNAasUnityCreature(new Vector3(50 * i, 0, 50 * j));
                k++;
            }
        }
    }
    IEnumerator Example(int numberOfSeconds) {
        yield return new WaitForSeconds(numberOfSeconds);
        WinningCreatures.Add(GradeAllCreatures());
        GenerationCount++;
        if(GenerationCount < NumberOfGenerationsToDo)
        {
            Round();
        }else
        {
            PresentWinningCreatures();
        }
    }
}