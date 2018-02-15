using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotArrayGeneration : MonoBehaviour
{
    public float timescale = 1;
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
    public Vector3 fitnessVector = new Vector3(1, 0, 0);
    public float blockMutationMagnitude = 0.3f;
    public float blockMutationChance = 0.2f;
    public float jointMutationMagnitude = 0.5f;
    public float jointMutationChance = 0.2f;

    // Editable members of the segment/joint population
    public GameObject[] SegmentTypeList;
    public GameObject[] JointTypeList;

    public int GenerationCount = 0;

    DNA[] Creatures;
    List<DNA> WinningCreatures = new List<DNA>();

    public class Connection
    {
        private GameObject conType;
        private bool isFixedJoint; // Special condition if the joint is a fixed joint
        private float conStr;
        private float conStrMax;
        private float conStrMin;
        private float conSpeed;
        private float conSpeedMax;
        private float conSpeedMin;
        private Vector3 conAxis;

        // Constructor
        public Connection() {
            conType = null;
            isFixedJoint = false;
            conStr = 0;
            conStrMin = 0;
            conStrMax = 0;
            conSpeed = 0;
            conSpeedMin = 0;
            conSpeedMax = 0;
            conAxis = Vector3.zero;
        }
        // Instantiator
        public GameObject InstantiateConnection(Vector3 location, Quaternion rotation) {
            GameObject connection = Instantiate(conType, location, rotation);
            return connection;
        }
        public void RollNewConStr() {
            conStr = (UnityEngine.Random.Range(conStrMin, conStrMax));
        }
        public void RollNewConSpeed() {
            conSpeed = (UnityEngine.Random.Range(conSpeedMin, conSpeedMax));
        }
        public void RollNewAxisOfRotation() {
            conAxis = new Vector3(UnityEngine.Random.Range(-1, 2), UnityEngine.Random.Range(-1, 2), UnityEngine.Random.Range(-1, 2));
        }
        // Setter
        public void SetConStrMin(float min) {
            conStrMin = min;
        }
        public void SetConStrMax(float max) {
            conStrMax = max;
        }
        public void SetConSpeedMin(float min) {
            conSpeedMin = min;
        }
        public void SetConSpeedMax(float max) {
            conSpeedMax = max;
        }
        public void SetConType(GameObject type) {
            conType = type;
        }
        public void SetFixedJoint(bool state) {
            isFixedJoint = state;
        }
        public void SetConStr(float strength) {
            conStr = strength;
        }
        public void SetConSpeed(float speed) {
            conSpeed = speed;
        }
        public void SetConAxis(Vector3 axis) {
            conAxis = axis;
        }
        // Getter
        public float GetConStrMin() {
            return conStrMin;
        }
        public float GetConStrMax() {
            return conStrMax;
        }
        public float GetConSpeedMin() {
            return conSpeedMin;
        }
        public float GetConSpeedMax() {
            return conSpeedMax;
        }
        public GameObject GetConType() {
            return conType;
        }
        public bool GetIsFixedJoint() {
            return isFixedJoint;
        }
        public float GetConStr() {
            return conStr;
        }
        public float GetConSpeed() {
            return conSpeed;
        }
        public Vector3 GetConAxis() {
            return conAxis;
        }
    }
    public class Block
    {
        private string blockName;
        private Vector3 blockPosition;
        private GameObject blockType;
        private float blockWeight;
        private float stabilizedChance;
        private bool stabilized;
        private Color blockColor;
        private float minWeight;
        private float maxWeight;
        private Vector3 instantiatedBlockObjectStartingLocation;

        private Connection posXCon;
        private Connection posYCon;
        private Connection posZCon;

        // Constructor
        public Block() {
            blockPosition = Vector3.zero;
            blockType = null;
            blockWeight = 0;
            minWeight = 0;
            maxWeight = 0;
            stabilizedChance = 0;
            stabilized = false;
            posXCon = new Connection();
            posYCon = new Connection();
            posZCon = new Connection();
            blockColor = new Color(1, 1, 1, 1);
            blockName = "";
            instantiatedBlockObjectStartingLocation = Vector3.zero;
        }
        // Instantiator
        public GameObject InstantiateBlock(Vector3 position) {
            GameObject block = Instantiate(blockType, position, new Quaternion(0, 0, 0, 0));
            return block;
        }
        public void CalculateBlockColor() {
            float weightColorMod = (blockWeight - minWeight) / (maxWeight - minWeight);
            blockColor = (new Color(1 - weightColorMod, 1 - weightColorMod, 1 - weightColorMod, 1));
        }
        public void RollNewBlockWeight() {
            blockWeight = (UnityEngine.Random.Range(minWeight, maxWeight));
            CalculateBlockColor();
        }
        // Setters
        public void SetBlockType(GameObject type) {
            blockType = type;
        }
        public void SetBlockPosition(Vector3 posVector) {
            blockPosition = posVector;
        }
        public void SetBlockWeight(float weight) {
            blockWeight = weight;
        }
        public void SetBlockStabilized(bool stab) {
            stabilized = stab;
        }
        public void SetBlockMinWeight(float minw) {
            minWeight = minw;
        }
        public void SetBlockMaxWeight(float maxw) {
            maxWeight = maxw;
        }
        public void SetBlockStabilizedChance(float stbl) {
            stabilizedChance = stbl;
        }
        public void SetBlockName(string name) {
            blockName = name;
        }
        public void SetInstantiatedObjectStartingLocation(Vector3 start) {
            instantiatedBlockObjectStartingLocation = start;
        }

        // Getter
        public GameObject GetBlockType() {
            return blockType;
        }
        public Vector3 GetBlockPosition() {
            return blockPosition;
        }
        public float GetBlockWeight() {
            return blockWeight;
        }
        public bool GetBlockStabilized() {
            return stabilized;
        }
        public Connection GetPosXCon() {
            return posXCon;
        }
        public Connection GetPosYCon() {
            return posYCon;
        }
        public Connection GetPosZCon() {
            return posZCon;
        }
        public Color GetBlockColor() {
            return blockColor;
        }
        public float GetBlockMinWeight() {
            return minWeight;
        }
        public float GetBlockMaxWeight() {
            return maxWeight;
        }
        public float GetBlockStabilizedChance() {
            return stabilizedChance;
        }
        public Vector3 GetInstantiatedObjectStartingLocation() {
            return instantiatedBlockObjectStartingLocation;
        }
        public string GetBlockName() {
            return blockName;
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
        public DNA Replicate() {
            DNA replicatedDNA = new DNA(CreatureName, arrayOfBlocks.GetLength(0), arrayOfBlocks.GetLength(1), arrayOfBlocks.GetLength(2));
            for (int i = 0; i < replicatedDNA.arrayOfBlocks.GetLength(0); i++)
            {
                for (int j = 0; j < replicatedDNA.arrayOfBlocks.GetLength(1); j++)
                {
                    for (int k = 0; k < replicatedDNA.arrayOfBlocks.GetLength(2); k++)
                    {
                        // Copy dna block weight, and calc repDNA color
                        replicatedDNA.arrayOfBlocks[i, j, k].SetBlockMaxWeight(arrayOfBlocks[i, j, k].GetBlockMaxWeight());
                        replicatedDNA.arrayOfBlocks[i, j, k].SetBlockMinWeight(arrayOfBlocks[i, j, k].GetBlockMinWeight());
                        replicatedDNA.arrayOfBlocks[i, j, k].SetBlockWeight(arrayOfBlocks[i, j, k].GetBlockWeight());
                        replicatedDNA.arrayOfBlocks[i, j, k].CalculateBlockColor();
                        // Position
                        replicatedDNA.arrayOfBlocks[i, j, k].SetBlockPosition(arrayOfBlocks[i, j, k].GetBlockPosition());
                        // Stabilization
                        replicatedDNA.arrayOfBlocks[i, j, k].SetBlockStabilized(arrayOfBlocks[i, j, k].GetBlockStabilized());
                        replicatedDNA.arrayOfBlocks[i, j, k].SetBlockStabilizedChance(arrayOfBlocks[i, j, k].GetBlockStabilizedChance());
                        // Type
                        replicatedDNA.arrayOfBlocks[i, j, k].SetBlockType(arrayOfBlocks[i, j, k].GetBlockType());
                        // Name
                        replicatedDNA.arrayOfBlocks[i, j, k].SetBlockName(arrayOfBlocks[i, j, k].GetBlockName());

                        // Now we copy all the data from the time joints connected to the original block, to put into the new block data
                        if (arrayOfBlocks[i, j, k].GetPosXCon() != null)
                        {
                            if (arrayOfBlocks[i, j, k].GetPosXCon().GetIsFixedJoint())
                            {
                                replicatedDNA.arrayOfBlocks[i, j, k].GetPosXCon().SetFixedJoint(true);
                            }
                            else
                            {
                                // Copy x joint axis
                                replicatedDNA.arrayOfBlocks[i, j, k].GetPosXCon().SetConAxis(arrayOfBlocks[i, j, k].GetPosXCon().GetConAxis());
                                // Copy x joint speed
                                replicatedDNA.arrayOfBlocks[i, j, k].GetPosXCon().SetConSpeed(arrayOfBlocks[i, j, k].GetPosXCon().GetConSpeed());
                                replicatedDNA.arrayOfBlocks[i, j, k].GetPosXCon().SetConSpeedMax(arrayOfBlocks[i, j, k].GetPosXCon().GetConSpeedMax());
                                replicatedDNA.arrayOfBlocks[i, j, k].GetPosXCon().SetConSpeedMin(arrayOfBlocks[i, j, k].GetPosXCon().GetConSpeedMin());
                                // copy x joint strength
                                replicatedDNA.arrayOfBlocks[i, j, k].GetPosXCon().SetConStr(arrayOfBlocks[i, j, k].GetPosXCon().GetConStr());
                                replicatedDNA.arrayOfBlocks[i, j, k].GetPosXCon().SetConStrMax(arrayOfBlocks[i, j, k].GetPosXCon().GetConStrMax());
                                replicatedDNA.arrayOfBlocks[i, j, k].GetPosXCon().SetConStrMin(arrayOfBlocks[i, j, k].GetPosXCon().GetConStrMin());
                                // copy x joint type
                                replicatedDNA.arrayOfBlocks[i, j, k].GetPosXCon().SetConType(arrayOfBlocks[i, j, k].GetPosXCon().GetConType());
                            }
                        }
                        if (arrayOfBlocks[i, j, k].GetPosYCon() != null)
                        {
                            if (arrayOfBlocks[i, j, k].GetPosYCon().GetIsFixedJoint())
                            {
                                replicatedDNA.arrayOfBlocks[i, j, k].GetPosYCon().SetFixedJoint(true);
                            }
                            else
                            {
                                // Copy x joint axis
                                replicatedDNA.arrayOfBlocks[i, j, k].GetPosYCon().SetConAxis(arrayOfBlocks[i, j, k].GetPosYCon().GetConAxis());
                                // Copy x joint speed
                                replicatedDNA.arrayOfBlocks[i, j, k].GetPosYCon().SetConSpeed(arrayOfBlocks[i, j, k].GetPosYCon().GetConSpeed());
                                replicatedDNA.arrayOfBlocks[i, j, k].GetPosYCon().SetConSpeedMax(arrayOfBlocks[i, j, k].GetPosYCon().GetConSpeedMax());
                                replicatedDNA.arrayOfBlocks[i, j, k].GetPosYCon().SetConSpeedMin(arrayOfBlocks[i, j, k].GetPosYCon().GetConSpeedMin());
                                // copy x joint strength
                                replicatedDNA.arrayOfBlocks[i, j, k].GetPosYCon().SetConStr(arrayOfBlocks[i, j, k].GetPosYCon().GetConStr());
                                replicatedDNA.arrayOfBlocks[i, j, k].GetPosYCon().SetConStrMax(arrayOfBlocks[i, j, k].GetPosYCon().GetConStrMax());
                                replicatedDNA.arrayOfBlocks[i, j, k].GetPosYCon().SetConStrMin(arrayOfBlocks[i, j, k].GetPosYCon().GetConStrMin());
                                // copy x joint type
                                replicatedDNA.arrayOfBlocks[i, j, k].GetPosYCon().SetConType(arrayOfBlocks[i, j, k].GetPosYCon().GetConType());
                            }
                        }
                        if (arrayOfBlocks[i, j, k].GetPosZCon() != null)
                        {
                            if (arrayOfBlocks[i, j, k].GetPosZCon().GetIsFixedJoint())
                            {
                                replicatedDNA.arrayOfBlocks[i, j, k].GetPosZCon().SetFixedJoint(true);
                            }
                            else
                            {
                                // Copy x joint axis
                                replicatedDNA.arrayOfBlocks[i, j, k].GetPosZCon().SetConAxis(arrayOfBlocks[i, j, k].GetPosZCon().GetConAxis());
                                // Copy x joint speed
                                replicatedDNA.arrayOfBlocks[i, j, k].GetPosZCon().SetConSpeed(arrayOfBlocks[i, j, k].GetPosZCon().GetConSpeed());
                                replicatedDNA.arrayOfBlocks[i, j, k].GetPosZCon().SetConSpeedMax(arrayOfBlocks[i, j, k].GetPosZCon().GetConSpeedMax());
                                replicatedDNA.arrayOfBlocks[i, j, k].GetPosZCon().SetConSpeedMin(arrayOfBlocks[i, j, k].GetPosZCon().GetConSpeedMin());
                                // copy x joint strength
                                replicatedDNA.arrayOfBlocks[i, j, k].GetPosZCon().SetConStr(arrayOfBlocks[i, j, k].GetPosZCon().GetConStr());
                                replicatedDNA.arrayOfBlocks[i, j, k].GetPosZCon().SetConStrMax(arrayOfBlocks[i, j, k].GetPosZCon().GetConStrMax());
                                replicatedDNA.arrayOfBlocks[i, j, k].GetPosZCon().SetConStrMin(arrayOfBlocks[i, j, k].GetPosZCon().GetConStrMin());
                                // copy x joint type
                                replicatedDNA.arrayOfBlocks[i, j, k].GetPosZCon().SetConType(arrayOfBlocks[i, j, k].GetPosZCon().GetConType());
                            }
                        }
                    }
                }
            }
            return replicatedDNA;
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
                // instantiation vector is the blockposition within the DNA added to the insantiation position of the DNA itself
                Vector3 instantiationVector = blockObject.GetBlockPosition() + instantiationPosition;
                if (blockObject.GetBlockType() != null)
                {
                    // Set the instantiated block object starting position
                    blockObject.SetInstantiatedObjectStartingLocation(instantiationVector);

                    // Actually instante the block
                    GameObject tempSegment = blockObject.InstantiateBlock(instantiationVector);
                    tempSegment.name = CreatureName + "x" + blockObject.GetBlockPosition().x + "y" + blockObject.GetBlockPosition().y + "z" + blockObject.GetBlockPosition().z;

                    // This stores the value of the unity objects name in the refered to block object inside the DNA
                    blockObject.SetBlockName(tempSegment.name);

                    // Set the weight of the object
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
        // Mutator Functions change the values of the blocks, should be used during the iterative process of changing genes
        private void MutateJoint(float JointMutationRate, float JointMutationChance) {

        }
        public void MutateDNA(float BlockMutationChance, float BlockMutationMagnitude, float JointMutationChance, float JointMutationMagnitude) {
            foreach (Block blockObject in arrayOfBlocks)
            {
                if (blockObject.GetBlockType() != null)
                {
                    //Block Weight
                    if (UnityEngine.Random.Range(0f, 1f) < BlockMutationChance)
                    {
                        blockObject.SetBlockWeight(UnityEngine.Random.Range(blockObject.GetBlockMinWeight(), blockObject.GetBlockMaxWeight()));
                        blockObject.CalculateBlockColor();
                    }
                    //Block
                    if (UnityEngine.Random.Range(0f, 1f) < BlockMutationChance)
                    {
                        if (UnityEngine.Random.Range(0f, 1f) > blockObject.GetBlockStabilizedChance())
                        {
                            blockObject.SetBlockStabilized(!blockObject.GetBlockStabilized());
                        }
                    }
                    // mutate Joint X if it exists
                    if (blockObject.GetPosXCon().GetIsFixedJoint() != true && blockObject.GetPosXCon().GetConType() != null)
                    {
                        if (UnityEngine.Random.Range(0f, 1f) < JointMutationChance)
                        {
                            blockObject.GetPosXCon().RollNewAxisOfRotation();
                        }
                        if (UnityEngine.Random.Range(0f, 1f) < JointMutationChance)
                        {
                            blockObject.GetPosXCon().RollNewConSpeed();
                        }
                        if (UnityEngine.Random.Range(0f, 1f) < JointMutationChance)
                        {
                            blockObject.GetPosXCon().RollNewConStr();
                        }
                    }
                    // mutate Joint Y if it exists
                    if (blockObject.GetPosYCon().GetIsFixedJoint() != true && blockObject.GetPosYCon().GetConType() != null)
                    {
                        if (UnityEngine.Random.Range(0f, 1f) < JointMutationChance)
                        {
                            blockObject.GetPosYCon().RollNewAxisOfRotation();
                        }
                        if (UnityEngine.Random.Range(0f, 1f) < JointMutationChance)
                        {
                            blockObject.GetPosYCon().RollNewConSpeed();
                        }
                        if (UnityEngine.Random.Range(0f, 1f) < JointMutationChance)
                        {
                            blockObject.GetPosYCon().RollNewConStr();
                        }
                    }
                    // mutate Joint Z if it exists
                    if (blockObject.GetPosZCon().GetIsFixedJoint() != true && blockObject.GetPosZCon().GetConType() != null)
                    {
                        if (UnityEngine.Random.Range(0f, 1f) < JointMutationChance)
                        {
                            blockObject.GetPosZCon().RollNewAxisOfRotation();
                        }
                        if (UnityEngine.Random.Range(0f, 1f) < JointMutationChance)
                        {
                            blockObject.GetPosZCon().RollNewConSpeed();
                        }
                        if (UnityEngine.Random.Range(0f, 1f) < JointMutationChance)
                        {
                            blockObject.GetPosZCon().RollNewConStr();
                        }
                    }
                }
            }
        }
        // Function to create the data for the individual block data, based on information based into the DNA constructor
        private void InitializeDNABlockData(int x, int y, int z, float density, float minW, float maxW, float chanceOfStabilization, GameObject[] SegmentTypes) {
            arrayOfBlocks[x, y, z] = new Block();
            arrayOfBlocks[x, y, z].SetBlockPosition(new Vector3(x, y, z));

            // Decide if block exists
            if (UnityEngine.Random.Range(0f, 1f) < density)
            {
                // Choose Segment Type
                arrayOfBlocks[x, y, z].SetBlockType(SegmentTypes[UnityEngine.Random.Range(0, SegmentTypes.Length)]);

                // Set the min and max block weights and roll for a new one
                arrayOfBlocks[x, y, z].SetBlockMinWeight(minW);
                arrayOfBlocks[x, y, z].SetBlockMaxWeight(maxW);
                arrayOfBlocks[x, y, z].RollNewBlockWeight();

                // Choose if Segment is Stabilized, meaning the block will not rotate around any axis
                arrayOfBlocks[x, y, z].SetBlockStabilizedChance(chanceOfStabilization);
                if (UnityEngine.Random.Range(0f, 1f) < chanceOfStabilization)
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
        // Constructors
        public DNA(string name, int SizeX, int SizeY, int SizeZ) { // Basic Constructor for blank objects
            arrayOfBlocks = new Block[SizeX, SizeY, SizeZ];
            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {
                    for (int k = 0; k < SizeZ; k++)
                    {
                        arrayOfBlocks[i, k, j] = new Block();
                    }
                }
            }
            CreatureName = name;
        }
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
            for (int i = 0; i < arrayOfBlocks.GetLength(0); i++)
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
                                        // Pick a type off the list, even if its just 1 list long
                                        arrayOfBlocks[i, j, k].GetPosXCon().SetConType(jointtypes[UnityEngine.Random.Range(0, jointtypes.Length)]);

                                        // Set the min and max x connection str min and max, then roll a new value for it
                                        arrayOfBlocks[i, j, k].GetPosXCon().SetConStrMin(minStr);
                                        arrayOfBlocks[i, j, k].GetPosXCon().SetConStrMax(maxStr);
                                        arrayOfBlocks[i, j, k].GetPosXCon().RollNewConStr();
                                        // Set the min and max x connection speed min and max, then roll a new value for it
                                        arrayOfBlocks[i, j, k].GetPosXCon().SetConSpeedMin(minSpeed);
                                        arrayOfBlocks[i, j, k].GetPosXCon().SetConSpeedMax(maxSpeed);
                                        arrayOfBlocks[i, j, k].GetPosXCon().RollNewConSpeed();
                                        // Roll new axis of rotation
                                        arrayOfBlocks[i, j, k].GetPosXCon().RollNewAxisOfRotation();
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
                                        arrayOfBlocks[i, j, k].GetPosYCon().SetConType(jointtypes[UnityEngine.Random.Range(0, jointtypes.Length)]);

                                        // Set the min and max Y connection str min and max, then roll a new value for it
                                        arrayOfBlocks[i, j, k].GetPosYCon().SetConStrMin(minStr);
                                        arrayOfBlocks[i, j, k].GetPosYCon().SetConStrMax(maxStr);
                                        arrayOfBlocks[i, j, k].GetPosYCon().RollNewConStr();
                                        // Set the min and max Y connection speed min and max, then roll a new value for it
                                        arrayOfBlocks[i, j, k].GetPosYCon().SetConSpeedMin(minSpeed);
                                        arrayOfBlocks[i, j, k].GetPosYCon().SetConSpeedMax(maxSpeed);
                                        arrayOfBlocks[i, j, k].GetPosYCon().RollNewConSpeed();
                                        // Roll new axis of rotation
                                        arrayOfBlocks[i, j, k].GetPosYCon().RollNewAxisOfRotation();
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
                                        arrayOfBlocks[i, j, k].GetPosZCon().SetConType(jointtypes[UnityEngine.Random.Range(0, jointtypes.Length)]);

                                        // Set the min and max x connection str min and max, then roll a new value for it
                                        arrayOfBlocks[i, j, k].GetPosZCon().SetConStrMin(minStr);
                                        arrayOfBlocks[i, j, k].GetPosZCon().SetConStrMax(maxStr);
                                        arrayOfBlocks[i, j, k].GetPosZCon().RollNewConStr();
                                        // Set the min and max x connection speed min and max, then roll a new value for it
                                        arrayOfBlocks[i, j, k].GetPosZCon().SetConSpeedMin(minSpeed);
                                        arrayOfBlocks[i, j, k].GetPosZCon().SetConSpeedMax(maxSpeed);
                                        arrayOfBlocks[i, j, k].GetPosZCon().RollNewConSpeed();
                                        // Roll new axis of rotation
                                        arrayOfBlocks[i, j, k].GetPosZCon().RollNewAxisOfRotation();
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
    
    public void Round1() {
        GenerationCount++;
        Creatures = new DNA[numOfCreatures];
        Array.Clear(Creatures, 0, Creatures.Length);
        CreateRandomGeneration();
        InstantiateCreatureArray();

        StartCoroutine(Iterate(delay));
    }
    IEnumerator Iterate(int numberOfSeconds) {
        yield return new WaitForSeconds(numberOfSeconds);
        WinningCreatures.Add(GradeAllCreatures());
        Round1();
    }
    void Start() {
        Creatures = new DNA[numOfCreatures];
        Time.timeScale = timescale;
        CreateRandomGeneration();
        InstantiateCreatureArray();

        StartCoroutine(Iterate(delay));
        //Time.fixedDeltaTime = 0.02F * Time.timeScale;
    } // end of start code
    /*
    public DNA[] InstantiateIndividualsFromChampion(DNA winner) {
        DNA[] newCreatures = new DNA[numOfCreatures];
        newCreatures[0] = winner.Replicate();
        newCreatures[0].SetCreatureName("Creature0");
        for (int i = 1; i < newCreatures.Length; i++)
        {
            // Add the champion dna to the array, and then mutate it to be different than the original, or the same! idk
            // This is copying the previous slots exact values, not mutating it and making a new object 
            newCreatures[i] = winner.Replicate();
            newCreatures[i].MutateDNA(blockMutationChance, blockMutationChance, jointMutationChance, jointMutationMagnitude);
            newCreatures[i].SetCreatureName("Creature" + i.ToString());
        }
        int k = 0;
        for (int i = 0; i < Mathf.Sqrt(numOfCreatures); i++)
        {
            for (int j = 0; j < Mathf.Sqrt(numOfCreatures); j++)
            {
                newCreatures[k].InstantiateDNAasUnityCreature(new Vector3(50 * i, 0, 50 * j));
                k++;
            }
        }
        return newCreatures;
    }
    */
    public void CreateRandomGeneration() { 
        for (int i = 0; i < Creatures.Length; i++)
        {
            Creatures[i] = new DNA("G" + GenerationCount.ToString() + "Creature" + i.ToString(), sizeOfCreaturesX, sizeOfCreaturesY, sizeOfCreaturesZ, densityOfBlocks, stabilizationChance, minimumWeight, maximumWeight, minimumJointForce,
                maximumJointForce, minimumJointSpeed, maximumJointSpeed, SegmentTypeList, JointTypeList);
        } // Rolls new random creatures to fill array
    }
    public void InstantiateCreatureArray() {
        int k = 0;
        for (int i = 0; i < Mathf.Sqrt(numOfCreatures); i++)
        {
            for (int j = 0; j < Mathf.Sqrt(numOfCreatures); j++)
            {
                Creatures[k].InstantiateDNAasUnityCreature(new Vector3(50 * i, 0, 50 * j));
                k++;
            }
        } // Instantiates the generations
    }
    private DNA GradeAllCreatures() {
        DNA winner = null;
        float WinnerWork = -99999f;
        foreach (DNA individual in Creatures)
        {
            Vector3 startTemp = individual.GetStartingPosition();
            float Work = 0;

            foreach (Block blockObject in individual.arrayOfBlocks)
            {
                if (blockObject.GetBlockType() != null)
                {

                    GameObject curSegment = GameObject.Find(blockObject.GetBlockName());
                    Vector3 travelVector = Vector3.Project((curSegment.transform.position - blockObject.GetInstantiatedObjectStartingLocation()), fitnessVector);
                    Vector3 wrongWayVector = Vector3.ProjectOnPlane((curSegment.transform.position - blockObject.GetInstantiatedObjectStartingLocation()), fitnessVector);

                    Work = curSegment.GetComponent<Rigidbody>().mass * ((travelVector.magnitude) - wrongWayVector.magnitude);

                    if (Work < 0)
                    {
                        Work = 0;
                    }
                    if (travelVector.normalized != fitnessVector.normalized)
                    {
                        Work = 0;
                    }
                }
            }

            individual.SetFitness(Work);
            if (individual.GetFitness() > WinnerWork)
            {
                winner = individual;
                WinnerWork = individual.GetFitness();
                Debug.Log(individual.GetCreatureName() + "  " + WinnerWork);
            }
            individual.SelfDestruct();
        }
        return winner;
    }
}