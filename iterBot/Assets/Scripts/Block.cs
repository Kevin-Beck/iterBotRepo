using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Block{
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
    //Print
    public string Print() {
        string[] cols = new string[15];
        // Block
        cols[0] = blockPosition.ToString();
      //  cols[1] = blockType.ToString();
        cols[1] = blockWeight.ToString();
        cols[2] = stabilized.ToString();
        // XCON
        cols[3] = posXCon.GetConAxis().ToString();
        cols[4] = posXCon.GetConSpeed().ToString();
        cols[5] = posXCon.GetConStr().ToString();
        cols[6] = posXCon.GetIsFixedJoint().ToString();
        // YCON
        cols[7] = posYCon.GetConAxis().ToString();
        cols[8] = posYCon.GetConSpeed().ToString();
        cols[9] = posYCon.GetConStr().ToString();
        cols[10] = posYCon.GetIsFixedJoint().ToString();
        // ZCON
        cols[11] = posZCon.GetConAxis().ToString();
        cols[12] = posZCon.GetConSpeed().ToString();
        cols[13] = posZCon.GetConStr().ToString();
        cols[14] = posZCon.GetIsFixedJoint().ToString();

        // Build a string that will represent the columns of the data
        string delimiter = ",";
        StringBuilder row = new StringBuilder();
        row.Append(string.Join(delimiter, cols));
        return row.ToString();
    }
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
        GameObject block = MonoBehaviour.Instantiate(blockType, position, new Quaternion(0, 0, 0, 0));
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
    public void CalculateBlockName(string creatureNameOfParentDNA) {
        blockName = creatureNameOfParentDNA + 'x' + blockPosition.x + 'y' + blockPosition.y + 'z' + blockPosition.z;
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
