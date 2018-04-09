using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Run : MonoBehaviour {

    [Header("Time Factors")]
    public float timescale = 3; // default 2
    public int delay = 30; // default 14

    [Header("Terrain Factors")]
    public float scaleX = 220;
    public float scaleZ = 110;    
    // The surfacce structures that the robots traverse
    public GameObject[] SurfaceTypeList;
    public int SelectedSurfaceType = 0; // the selcted type off the list of types
    public float gravitationalForce = 1;

    [Header("Generational Data")]
    public int CurrentGenerationCount = 0; // must keep 0, first gen is 0, do not change
    public int NumberOfGenerationsToDo = 100; // number of generations ~50-100
    public int RowsOfCreatures = 8;
    public int numOfCreatures = 64; // total number of creatures to make, should be a perfect square, 64, set by squaring the rows
    [Header("Size Factors")]
    public int sizeOfCreaturesX = 3; // 3
    public int sizeOfCreaturesY = 6; // 6
    public int sizeOfCreaturesZ = 4; // 4
    public int selectedBodyType = 0;
    [Header("DNA Attribute Boundaries")]
    public float densityOfBlocks = 0.3f; // .3f
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
    public float blockMutationChance = 0.1f; // .1f is nice
    public float jointMutationMagnitude = 0.5f; // does nothing yet
    public float jointMutationChance = 0.1f; 
    public int pickedWinnersEachGen = 1;
    public float maxDeviation = 50; // maximum we want the bots to be able to vary

    [Header("Menu Items")]
    public GameObject MenuController;

    [Header("GameObject Components")]
    // Editable members of the segment/joint population
    public GameObject[] SegmentTypeList;
    public GameObject[] JointTypeList;

    public GameObject JointRenderObject;

    public DNA[] Creatures;
    public DNA[] Winners;
    public DNA OverallChampion;
    public DNA BestChampion;

    private bool candidatefound = false;

    private float curTime = 0;
    private bool pause = true;
    private IEnumerator loop; // This is the coroutine's name that runs the simulation loop

    public void ChampionSelectRound() {
        if (CurrentGenerationCount >= NumberOfGenerationsToDo)
        {
            Debug.Break();
        }
        else
        {
            curTime = 0;
            loop = ChampionSelect(delay);
            StartCoroutine(loop);
        }
    }
    public void ClearMemory() {
        Resources.UnloadUnusedAssets();
    }
    IEnumerator ChampionSelect(int numberOfSeconds) {
        ClearMemory(); // Just a precaution to stop mem leaks

        yield return new WaitForSeconds(numberOfSeconds);
        // Get a champion from the current generation,  start a new test from that generation
        
        CurrentGenerationCount++;
        GradeAllCreatures();
        if(Winners[0].GetFitness() == 0)
        {
            MenuController.GetComponent<RunMenu>().AlertText.text = "No successful candidate created in Generation " + CurrentGenerationCount + ". \nReinitializing Base Model";
            CreateRandomGeneration();
        }
        else
        {
            if (candidatefound == false)
            {
                MenuController.GetComponent<RunMenu>().AlertText.text = "Successful Candidate created in Generation " + CurrentGenerationCount + "\nTesting Mutations of Best Candidate";
                candidatefound = true;
            } else
            {
                MenuController.GetComponent<RunMenu>().AlertText.text = "Continuing Mutation and Testing Process";
            }
            GenerateArrayFromIndividual();
        }
        InstantiateCreatureArray();
        ChampionSelectRound();
    }
    void Start() {
       // RunSimulation();
    } // end of start code
    public void PauseSimulation() {
        Time.timeScale = 0;
        pause = true;
    }
    public void RestartSimulation() {
        candidatefound = false;
        try
        {
            StopSimulation();
        }catch
        {

        }
        RunSimulation();
    }
    public void StopSimulation() {
        StopCoroutine(loop);
        DestroyAllTerrain();
        DestroyAllCreatures();
    }
    public void UpdateSimulationSpeed() {
        Time.timeScale = MenuController.GetComponent<RunMenu>().GetSimulationSpeed();
    }
    public void RunSimulation() {
        curTime = 0;
        pause = false;
        OverallChampion = new DNA("default", sizeOfCreaturesX, sizeOfCreaturesY, sizeOfCreaturesZ);
        CurrentGenerationCount = 0;

        numOfCreatures = RowsOfCreatures * RowsOfCreatures; // DO NOT REMOVE THIS, it sets the number of creatures to the num of rows squared. Everything depends on this structure.
        Creatures = new DNA[numOfCreatures];
        Winners = new DNA[pickedWinnersEachGen];

        Physics.gravity = new Vector3(0f, -9.81f * gravitationalForce, 0f);
        maxDeviation = 2*(float)Math.Sqrt((sizeOfCreaturesX + sizeOfCreaturesY + sizeOfCreaturesZ));

        GenerateAllSurfaces();
        CreateRandomGeneration();
        InstantiateCreatureArray();

        loop = ChampionSelect(delay);
        StartCoroutine(loop);
    }
    public void GenerateArrayFromIndividual() {
        int count = 0;
        // PUtting this here to get the project done,
        // it just sees if the winner beat the overal best guy, if not, it makes the winner array[0] a replicate
        // of the overall champ

        if (Winners[0].GetFitness() < OverallChampion.GetFitness())
        {
            Winners[0] = OverallChampion.Replicate("ReplacementWinner");
        }
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
                maximumJointForce, minimumJointSpeed, maximumJointSpeed, SegmentTypeList, selectedBodyType, JointTypeList, JointRenderObject);
        } // Rolls new random creatures to fill array
    }
    public void InstantiateCreatureArray() {
        MenuController.GetComponent<RunMenu>().UpdateGenerationText(CurrentGenerationCount);
        int k = 0;
        for (int i = 0; i < Mathf.Sqrt(numOfCreatures); i++)
        {
            for (int j = 0; j < Mathf.Sqrt(numOfCreatures); j++)
            {
                if(k == 0)
                {
                    Creatures[k].SetRenderJointObjects(true);
                }
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
                GenerateTerrainSurface(new Vector3(scaleX * i, 0, scaleZ * j), k);
                k++;
            }
        }
    }
    public void DestroyAllCreatures() {
        foreach (DNA individual in Creatures)
        {
            individual.SelfDestruct();
        }
    }
    public void DestroyAllTerrain() {
        for (int i = 0; i < numOfCreatures; i++)
        {
            Destroy(GameObject.Find("Terrain" + i));
        }
    }
    public void GenerateTerrainSurface(Vector3 location, int terrainNumber) {
        GameObject individualTerrain = Instantiate(SurfaceTypeList[SelectedSurfaceType], new Vector3(location.x, 0, location.z), Quaternion.identity);
        individualTerrain.name = "Terrain" + terrainNumber;
    }
    private void GradeAllCreatures() {
        float winnerFitness = -1;
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
                    // CurSegment is the current block portion of the individual
                    GameObject curSegment = GameObject.Find(blockObject.GetBlockName());
                    // travelVector is the vector the block has travelled from start to finish
                    Vector3 travelVector = Vector3.Project((curSegment.transform.position - blockObject.GetInstantiatedObjectStartingLocation()), fitnessVector);
                    // wrongWayVector is all the directions except the fitness vector
                    Vector3 wrongWayVector = Vector3.ProjectOnPlane((curSegment.transform.position - blockObject.GetInstantiatedObjectStartingLocation()), fitnessVector);

                    // work is a misnomer, but it was the closest we wanted, it isthe value of mass x distance, minues the mass x distance in the wrong direction
                    Work = curSegment.GetComponent<Rigidbody>().mass * ((travelVector.magnitude) - wrongWayVector.magnitude);

                    if (Work < 0)
                    {
                        Work = 0;
                    }
                    if (travelVector.normalized != fitnessVector.normalized)
                    {
                        Work = 0;
                    }
                    if(travelVector.y < 0)
                    {
                        Work = 0;
                    }
                }
            }

            // Calculate the standardDeviation of the blocks, if its too high, we'll reset the values of the fitness to 0 as punishment for varying too much
            // First we get the total vector size
            
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
            if(numOfBlocks > 0) // quick check to make sure we have an actual individual that exists
            {
                Vector3 averageVector = totalVector / numOfBlocks;
                // now subtract this average position from each of the blocks positions to calculate the distance each 
                // piece is from the average
                float sumOfSquaresOfDistances = 0;
                foreach (Block blockObject in individual.arrayOfBlocks)
                {
                    if (blockObject.GetBlockType() != null)
                    {
                        sumOfSquaresOfDistances += Mathf.Pow(((GameObject.Find(blockObject.GetBlockName()).GetComponent<Rigidbody>().transform.position - averageVector).magnitude), 2);
                    }
                }
                float standardDeviationOfBlockVectors = sumOfSquaresOfDistances / numOfBlocks;
                if(standardDeviationOfBlockVectors > maxDeviation)
                {
                    Work = 0; // This negates the work done by this object, as it was too varied
                }
            }

            // Keep track of winning individuals
            individual.SetFitness(Work);
            if (individual.GetFitness() > lowestWinnerScore)
            {                
                // Keep track of best of gen
                if (individual.GetFitness() > winnerFitness)
                {
                    Debug.Log(individual.GetCreatureName() + " has joined the winners");
                    winnerFitness = individual.GetFitness();
                    Winners[0] = individual.Replicate("winner");
                    Winners[0].SetFitness(winnerFitness);
                }
            }
            if (individual.GetFitness() > OverallChampion.GetFitness())
            {
                // Print the winner to the screen and also set the overall champ to the new winning dude
                MenuController.GetComponent<RunMenu>().UpdateWinnerFitnessText(winnerFitness);
                OverallChampion = individual.Replicate("OverallChampion");
            }
            //Remove the individual after testing is completed
            individual.SelfDestruct();
        }

    }
    private void Update() {
        if(!pause)
        {
            curTime += Time.deltaTime;
            MenuController.GetComponent<RunMenu>().UpdateClockText(curTime);
        }

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
