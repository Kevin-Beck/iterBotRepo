using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotArrayGeneration : MonoBehaviour {
    // Editable members of the segment/joint population
    public GameObject[] SegmentTypeList;
    public GameObject[] JointTypeList;
     
        
    // The block represents a single cell of the organism, it holds information about the actual individual cells and who/where they are connected
    public class Connection
    {
        private int conType;
        private float conStr;
        private float conSpeed;

        // Constructor
        public Connection()
        {
            conType = -1;
            conStr = 0;
            conSpeed = 0;
        }
        // Setter
        public void SetConType(int type)
        {
            conType = type;
        }
        public void SetConStr(float strength)
        {
            conStr = strength;
        }
        public void SetConSpeed(float speed)
        {
            conSpeed = speed;
        }
        // Getter
        public int GetConType()
        {
            return conType;
        }
        public float GetConStr()
        {
            return conStr;
        }
        public float GetConSpeed()
        {
            return conSpeed;
        }
    }
    public class Block
    {
        private Vector3 blockPosition;
        private GameObject blockType;  // this will define the type of the game object used for the segment, -1 is nothing, 0 is a block
        private float blockWeight;
        private bool stabilized;

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
        
    }
    public class DNA
    {
        private Block[,,] arrayOfBlocks;
        private int SizeXReference;
        private int SizeYReference;
        private int SizeZReference;

        // Function to get block objects
        public Block getBlock(int x, int y, int z)
        {
            return arrayOfBlocks[x, y, z];
        }
        // Function to print a DNA object to a string.
        public string GenerateGeneticString()
        {

            string geneOpen = "{";
            string geneClose = "}";
            string blockOpen = "[";
            string blockClose = "]";
            string setOpen = "(";
            string setClose = ")";
            string sequence = ",";

            string gs = "";
            Concat(gs, geneOpen); // Start off the gene
            Concat(gs, "x" + SizeXReference + "y" + SizeYReference + "z" + SizeZReference); // save the size of the DNA Array
            
            for (int i = 0; i < SizeXReference; i++)
            {
                for(int j = 0; j < SizeYReference; j++)
                {
                    for(int k = 0; k < SizeZReference; k++)
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
        private string Concat(string s, string c)
        {
            return s += c;
        }


        // Function to Instantiate a DNA Object into the Unity World
        public void InstantiateDNAasUnityCreature(Vector3 instantiationPosition) 
        {            
            for (int i = 0; i < SizeXReference; i++)
            {
                for (int j = 0; j < SizeYReference; j++)
                {
                    for (int k = 0; k < SizeZReference; k++)
                    {                                                
                        Vector3 instantiationVector = arrayOfBlocks[i, j, k].GetBlockPosition() + instantiationPosition;
                        if(arrayOfBlocks[i,j,k].GetBlockType() != null)
                        {
                            GameObject tempSegment = arrayOfBlocks[i, j, k].InstantiateBlock(instantiationVector);
                        }
                    }
                }
            }
        }
        // Function to DEBUG a DNA 3d Array, print representation to screen or whatever

        // DNA needs to be even more robust, more functionality of each joint

        // Constructor
        public DNA(int ArrayX, int ArrayY, int ArrayZ, float densityOfBlocks, float minWeight, float maxWeight, float minStr, float maxStr,
            float minSpeed, float maxSpeed, GameObject[] segtypes, GameObject[] jointtypes)
        {
            arrayOfBlocks = new Block[ArrayX, ArrayY, ArrayZ];
            SizeXReference = ArrayX;
            SizeYReference = ArrayY;
            SizeZReference = ArrayZ;
            
            // Set up if the blocks in the dna are there or not
            for (int i = 0; i < ArrayX; i++)
            {
                for (int j = 0; j < ArrayY; j++)
                {
                    for (int k = 0; k < ArrayZ; k++)
                    {
                        arrayOfBlocks[i, j, k] = new Block();
                        if (Random.Range(0f, 1f) < densityOfBlocks)
                        {
                            arrayOfBlocks[i, j, k].SetBlockType(segtypes[Random.Range(0,segtypes.Length)]); // this is a block that exists    
                            arrayOfBlocks[i, j, k].SetBlockWeight(Random.Range(minWeight, maxWeight));
                            arrayOfBlocks[i, j, k].SetBlockPosition(new Vector3(i, j, k));
                            // TODO maybe make the skin of the block reflect the color of the thing
                        }
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
                                    arrayOfBlocks[i, j, k].GetPosXCon().SetConType(0); // TEMP
                                    /*
                                     * Todo:
                                     * Code that sets up the *****FIXED***joint in the positive x direction
                                     */
                                }
                                else if (i + 2 < ArrayX)
                                {
                                    if (arrayOfBlocks[i + 2, j, k].GetBlockType() != null)
                                    {
                                        arrayOfBlocks[i, j, k].GetPosXCon().SetConType(1);
                                        /*
                                        * Todo:
                                        * Code that sets up the *****NON FIXED***joint in the positive Y direction
                                        */
                                    }
                                }

                            }
                            
                            // This is the code for if there is a block in the ypos direction
                            // we check to make sure we dont go off the array
                            if (j + 1 < ArrayY)
                            {
                                if (arrayOfBlocks[i, j + 1, k].GetBlockType() != null)
                                {
                                    arrayOfBlocks[i, j, k].GetPosYCon().SetConType(0); // temp
                                    /*
                                     * Todo:
                                     * Code that sets up the *****FIXED***joint in the positive Y direction
                                     */
                                }
                                else if (j + 2 < ArrayY)
                                {
                                    if (arrayOfBlocks[i, j + 2, k].GetBlockType() != null)
                                    {
                                        arrayOfBlocks[i, j, k].GetPosYCon().SetConType(1); // temp
                                        //todo make the code for non fixed
                                    }
                                }
                            }
                            
                            // This is the code for if there is a block in the zpos direction
                            // we check to make sure wedont go off the array
                            if (k + 1 < ArrayZ)
                            {
                                if (arrayOfBlocks[i, j, k + 1].GetBlockType() != null)
                                {
                                    arrayOfBlocks[i, j, k].GetPosZCon().SetConType(0); // temp
                                    /*
                                     * Todo:
                                     * Code that sets up the *****FIXED***joint in the positive Z direction
                                     */
                                }
                                else if (k + 2 < ArrayZ) // check to see if we go off the array again
                                {
                                    if (arrayOfBlocks[i, j, k + 2].GetBlockType() != null)
                                    {
                                        arrayOfBlocks[i, j, k].GetPosZCon().SetConType(1); // temp
                                       // make the code that sets up the *****NON FIXED***joint in the positive Z direction
                                                                                         
                                    }
                                }
                            }                            
                        }
                    }
                }
            }
        }
    }

    void Start()
    {       
      //  DNA me = new DNA(4, 4, 4, .8f, 1, 5, 300, 500, 200, 1000, SegmentTypeList, JointTypeList);
        DNA Ryan = new DNA(2, 6, 8, .3f, 1f, 2f, 400f, 500f, 1000f, 2000f, SegmentTypeList, JointTypeList);
        Ryan.InstantiateDNAasUnityCreature(new Vector3(0, 0, 0));

    } // end of start code
}