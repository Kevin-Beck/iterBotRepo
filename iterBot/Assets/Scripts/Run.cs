using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Run : MonoBehaviour {

    [Header("Time Factors")]
    public float timescale = 1; // default 2
    public int delay = 14; // default 14
    public float scaleX = 50;
    public float scaleZ = 50;

    [Header("Generational Data")]
    public int CurrentGenerationCount = 0; // must keep 0, first gen is 0, do not change
    public int NumberOfGenerationsToDo = 16; // number of gnenerations wanted currently 16
    public int RowsOfCreatures = 8;
    public int numOfCreatures; // total number of creatures to make, should be a perfect square, 64, set by squaring the rows
    [Header("Size Factors")]
    public int sizeOfCreaturesX = 5; // 5
    public int sizeOfCreaturesY = 5; // 5
    public int sizeOfCreaturesZ = 5; // 5
    [Header("DNA Attribute Boundaries")]
    public float densityOfBlocks = 0.2f; // .2f
    public float stabilizationChance = 0.1f; // .1f
    public float minimumWeight = 2f; // 2f
    public float maximumWeight = 5f; // 5f
    public float minimumJointForce = 500f; // 500f
    public float maximumJointForce = 800f; // 800f
    public float minimumJointSpeed = 200f; // 200f
    public float maximumJointSpeed = 800f; // 800f
    [Header("Fitness and Mutation Factors")]
    public Vector3 fitnessVector = new Vector3(1, 0, 0); // any vector you want to test for
    public float blockMutationMagnitude = 0.3f; // does nothing currently
    public float blockMutationChance = 0.2f; // .2f is nice
    public float jointMutationMagnitude = 0.5f; // .5f // does nothing yet
    public float jointMutationChance = 0.2f; // .2f default
    public int pickedWinnersEachGen = 5;

    [Header("Surface Type")]
    // The surfacce structures that the robots traverse
    public GameObject[] SurfaceTypeList;
    public int SelectedSurfaceType = 0; // the selcted type off the list of types

    [Header("GameObject Components")]
    // Editable members of the segment/joint population
    public GameObject[] SegmentTypeList;
    public GameObject[] JointTypeList;

    private DNA[] Creatures;
    private DNA[] Winners;

    public void ChampionSelectRound() {
        if (CurrentGenerationCount >= NumberOfGenerationsToDo)
        {
            Debug.Break();
        }
        else
        {
            StartCoroutine(ChampionSelect(delay));
        }
    }

    IEnumerator ChampionSelect(int numberOfSeconds) {
        yield return new WaitForSeconds(numberOfSeconds);
        // Get a champion from the current generation,  start a new test from that generation

        CurrentGenerationCount++;
        GradeAllCreatures();
        GenerateArrayFromIndividual();
        InstantiateCreatureArray();
        ChampionSelectRound();
    }
    void Start() {
        numOfCreatures = RowsOfCreatures * RowsOfCreatures; // DO NOT REMOVE THIS, it sets the number of creatures to the num of rows squared. Everything depends on this structure.
        Creatures = new DNA[numOfCreatures];
        Winners = new DNA[pickedWinnersEachGen];
        RunSimulation();
    } // end of start code
    public void ReloadLevel() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void RunSimulation() {
        Time.timeScale = timescale;
        // Should only really change the stuff after this, above this point is stuff you dont wanna get into basically
        GenerateAllSurfaces();
        CreateRandomGeneration();
        InstantiateCreatureArray();

        StartCoroutine(ChampionSelect(delay));
    }
    public void GenerateArrayFromIndividual() {
        Time.timeScale = timescale;

        int count = 0;
        for (int i = 0; i < Mathf.Sqrt(numOfCreatures); i++)
        {
            for (int j = 0; j < Math.Sqrt(numOfCreatures); j++)
            {
                if (i == 0 && j < pickedWinnersEachGen) // if we're in the first row, and we want to recreate our winners from last round
                {
                    Creatures[count] = Winners[count % pickedWinnersEachGen].Replicate("Gen" + CurrentGenerationCount + "Creature" + count);
                    Creatures[count].SetCreatureName("Gen" + CurrentGenerationCount + "Creature" + count);
                }
                else // we want to make the creature after we mutated it
                {
                    Creatures[count] = Winners[count % pickedWinnersEachGen].Replicate("Gen" + CurrentGenerationCount + "Creature" + count);
                    Creatures[count].MutateDNA(blockMutationChance, blockMutationChance, jointMutationChance, jointMutationMagnitude);
                    Creatures[count].SetCreatureName("Gen" + CurrentGenerationCount + "Creature" + count);
                }
                count++;
            }
        }
    }
    public void CreateRandomGeneration() {
        for (int i = 0; i < Creatures.Length; i++)
        {
            Creatures[i] = new DNA("G" + CurrentGenerationCount.ToString() + "Creature" + i.ToString(), sizeOfCreaturesX, sizeOfCreaturesY, sizeOfCreaturesZ, densityOfBlocks, stabilizationChance, minimumWeight, maximumWeight, minimumJointForce,
                maximumJointForce, minimumJointSpeed, maximumJointSpeed, SegmentTypeList, JointTypeList);
        } // Rolls new random creatures to fill array
    }
    public void InstantiateCreatureArray() {
        int k = 0;
        for (int i = 0; i < Mathf.Sqrt(numOfCreatures); i++)
        {
            for (int j = 0; j < Mathf.Sqrt(numOfCreatures); j++)
            {
                Creatures[k].InstantiateDNAasUnityCreature(new Vector3(scaleX * i, 1, scaleZ * j));                
                k++;
            }
        } // Instantiates the generations
    }
    public void GenerateAllSurfaces() {
        int k = 0;
        for (int i = 0; i < Mathf.Sqrt(numOfCreatures); i++)
        {
            for (int j = 0; j < Mathf.Sqrt(numOfCreatures); j++)
            {
                GenerateTerrainSurface(new Vector3(scaleX * i, 0, scaleZ * j));
                k++;
            }
        }
    }
    public void GenerateTerrainSurface(Vector3 location) {
        Instantiate(SurfaceTypeList[SelectedSurfaceType], new Vector3(location.x, 0, location.z), Quaternion.identity);
    }
    private void GradeAllCreatures() {
        int numberOfWinners = 0;
        float winnerFitness = 0;
        float lowestWinnerScore = -1;

        foreach (DNA individual in Creatures)
        {
            //Vector3 startTemp = individual.GetStartingPosition();
            float Work = 0;
            // Calculate the work done by each block
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

            // Calculate the standardDeviation of the blocks, if its too high, we'll reset the values of the fitness to 0 as punishment for varying too much
            // First we get the total vector size
            /*
            Vector3 totalVector = Vector3.zero;
            int numOfBlocks = 0;
            foreach(Block blockObject in individual.arrayOfBlocks)
            {
                if(blockObject.GetBlockType() != null)
                {
                    numOfBlocks++;
                    GameObject curSegment = GameObject.Find(blockObject.GetBlockName());
                    totalVector += curSegment.GetComponent<Rigidbody>().transform.position;
                }
            }
            
            if(numOfBlocks > 0)
            {
                Vector3 averageVector = totalVector / numOfBlocks;

                // now subtract this average position from each of the blocks positions to calculate the distance each piece is from the average
                float sumOfSquaresOfDistances = 0;
                foreach (Block blockObject in individual.arrayOfBlocks)
                {
                    if (blockObject.GetBlockType() != null)
                    {
                        sumOfSquaresOfDistances += Mathf.Pow(((GameObject.Find(blockObject.GetBlockName()).GetComponent<Rigidbody>().transform.position - averageVector).magnitude), 2);
                    }
                }
                float standardDeviationOfBlockVectors = sumOfSquaresOfDistances / numOfBlocks;
                if(standardDeviationOfBlockVectors > 50)
                {
                    Work = 0; // This negates the work done by this object, as it was too varied
                }
            }
            */

            // TODO THIS NEEDS WORK STILL SELECTION PROCESS RUINS ITSELF WITH MULTIPLE WINNERS
            // THIS MUST BE FIXED

            // Determine if the fitness is good enough to get into the winners bracket
            individual.SetFitness(Work);
            if (individual.GetFitness() > lowestWinnerScore)
            {
                // Keep track of best of gen
                if (individual.GetFitness() > winnerFitness)
                {
                    Debug.Log(individual.GetCreatureName() + " has joined the winners");
                    winnerFitness = individual.GetFitness();
                }
                // Select winners and if we have enough, then we need to check
                if (numberOfWinners >= pickedWinnersEachGen)
                {
                    //float tempLow = float.MaxValue;
                    //int weakestBotIndex = -1;
                    //for(int j = 0; j < Winners.Length; j++)
                    //{
                    if (Winners[0].GetFitness() < individual.GetFitness())
                    {
                        Winners[0] = individual.Replicate("demo");
                        //weakestBotIndex = j;
                        //tempLow = Winners[j].GetFitness();
                    }
                    //}
                    //Winners[weakestBotIndex] = individual.Replicate("replacer");

                }
                else
                {
                    Winners[numberOfWinners] = individual.Replicate("tempWinner");
                    numberOfWinners++;
                }
            }
            //Remove the individual after testing is completed
            individual.SelfDestruct();
        }
        // Print out the best score so far
        Debug.Log("Gen: " + CurrentGenerationCount + "  Winner: " + winnerFitness);
    }
    private void PresentWinningCreatures() {
        int counter = 0;
        foreach (DNA indiv in Winners)
        {
            indiv.SetCreatureName("Winner" + counter);
            counter++;
        }
        int k = 0;
        for (int i = 0; i < Mathf.Sqrt(Winners.Length); i++)
        {
            for (int j = 0; j < Mathf.Sqrt(Winners.Length); j++)
            {
                Winners[k].InstantiateDNAasUnityCreature(new Vector3(50 * i, 0, 50 * j));
                k++;
            }
        }
    }
}
