using UnityEngine;

public class Block : MonoBehaviour {
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
