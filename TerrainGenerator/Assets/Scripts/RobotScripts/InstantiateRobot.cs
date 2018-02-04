using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateRobot : MonoBehaviour {

    public GameObject basecomponent;
    public GameObject jointcomponent;
    public int x;
    public int y;
    public int z;
    public int maxnumx;
    public int maxnumy;
    public int maxnumz;

    public int targetspeedmax;
    public int targetspeedmin;
    public int maxstr;
    public int minstr;
    public int minmass;
    public int maxmass;
    public float ChanceToSpawnLegs;
    public float ChancetoFreezeXYZ;


    private void Start()
    {
        
        GameObject starter = Instantiate(basecomponent, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        x = Random.Range(0, maxnumx); // SETS THE RANDOM RANGE FOR THE THING
        StartCoroutine(Generate(starter, x));
        
    }

    // Use this for initialization
    IEnumerator Generate(GameObject S1, int repeat) {
        repeat--;
        S1.GetComponent<Rigidbody>().mass = Random.Range(minmass, maxmass);
        if (Random.Range(0, 100) > ChancetoFreezeXYZ)
        {
            Rigidbody s1rb = S1.GetComponent<Rigidbody>();
            s1rb.constraints = RigidbodyConstraints.FreezeRotationX| RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        }

        int dir = Random.Range(1, 4);
        HingeJoint j1a = S1.AddComponent<HingeJoint>();

        Vector3 directionVector = Vector3.zero;
        switch (dir)
        {
            case 1:
                directionVector = new Vector3(1, 0, 0);
                break;
            case 2:
                directionVector = new Vector3(0, 1, 0);
                break;
            case 3:
                directionVector = new Vector3(0, 0, 1);
                break;
            default:
                break;
        }
        j1a.axis = directionVector;
        j1a.autoConfigureConnectedAnchor = true;
        JointMotor j1ajm = j1a.motor;

        j1ajm.force = Random.Range(minstr, maxstr);
        j1ajm.targetVelocity = Random.Range(targetspeedmin, targetspeedmax);
        j1a.motor = j1ajm;
        j1a.useMotor = true;

        GameObject J1 = Instantiate(jointcomponent, new Vector3(S1.transform.position.x + 1, 0, 0), new Quaternion(0, 0, 0, 0));
        Rigidbody J1rb = J1.GetComponent<Rigidbody>();

        j1a.connectedBody = J1rb;
        HingeJoint j1b = J1.AddComponent<HingeJoint>();
        j1b.axis = Vector3.zero;
        j1b.autoConfigureConnectedAnchor = true;

        GameObject S2 = Instantiate(basecomponent, new Vector3(S1.transform.position.x + 2, 0, 0), new Quaternion(0, 0, 0, 0));
        Rigidbody S2rb = S2.GetComponent<Rigidbody>();        

        
        // Break to go towards the Y direction
        if(Random.Range(0,100) < ChanceToSpawnLegs)
        {
            y = Random.Range(0, maxnumy); // SETS THE RANDOM RANGE FOR THE THING
            StartCoroutine(GenerateY(S2, y));
        }
        // end of the go to Y direction

        //Break to go towards the Z direction
        if (Random.Range(0,100) < ChanceToSpawnLegs)
        {
            z = Random.Range(0, maxnumz); // SETS THE RANDOM RANGE FOR THE THING
            StartCoroutine(GenerateZ(S2, z));
        }
        // end of the go to Z direction


        j1b.connectedBody = S2rb;
        if (repeat > 0)
        {
            yield return new WaitForSeconds(0.034f);
            StartCoroutine(Generate(S2, repeat));
        }
        else
        {
            yield return new WaitForSeconds(0.034f);
        }
    }
    IEnumerator GenerateY(GameObject Y1, int repeat)
    {
        repeat--;
        int dir = Random.Range(1, 4);
        HingeJoint j1a = Y1.AddComponent<HingeJoint>();

        Vector3 directionVector = Vector3.zero;
        switch (dir)
        {
            case 1:
                directionVector = new Vector3(1, 0, 0);
                break;
            case 2:
                directionVector = new Vector3(0, 1, 0);
                break;
            case 3:
                directionVector = new Vector3(0, 0, 1);
                break;
            default:
                break;
        }
        j1a.axis = directionVector;
        j1a.autoConfigureConnectedAnchor = true;
        JointMotor j1ajm = j1a.motor;


        j1ajm.force = Random.Range(minstr, maxstr);
        j1ajm.targetVelocity = Random.Range(targetspeedmin, targetspeedmax);
        j1a.motor = j1ajm;
        j1a.useMotor = true;

        GameObject J1 = Instantiate(jointcomponent, new Vector3(Y1.transform.position.x, Y1.transform.position.y + 1, 0), new Quaternion(0, 0, 0, 0));
        Rigidbody J1rb = J1.GetComponent<Rigidbody>();

        j1a.connectedBody = J1rb;
        HingeJoint j1b = J1.AddComponent<HingeJoint>();
        j1b.axis = Vector3.zero;
        j1b.autoConfigureConnectedAnchor = true;


        GameObject S2 = Instantiate(basecomponent, new Vector3(Y1.transform.position.x, Y1.transform.position.y + 2, 0), new Quaternion(0, 0, 0, 0));
        Rigidbody S2rb = S2.GetComponent<Rigidbody>();


        j1b.connectedBody = S2rb;
        if (repeat > 0)
        {
            yield return new WaitForSeconds(0.034f);
            StartCoroutine(GenerateY(S2, repeat));
        }
        else
        {
            yield return new WaitForSeconds(0.034f);
        }
    }
    IEnumerator GenerateZ(GameObject Y1, int repeat)
    {
        repeat--;

        int dir = Random.Range(1, 4);
        HingeJoint j1a = Y1.AddComponent<HingeJoint>();
        Vector3 directionVector = new Vector3(1, 0, 0);
   /*
     switch (dir)
        {
            case 1:
                directionVector = new Vector3(1, 0, 0);
                break;
            case 2:
                directionVector = new Vector3(0, 1, 0);
                break;
            case 3:
                directionVector = new Vector3(0, 0, 1);
                break;
            default:
                break;
        }
        */
        j1a.axis = directionVector;
        j1a.autoConfigureConnectedAnchor = true;
        JointMotor j1ajm = j1a.motor;

        j1ajm.force = Random.Range(minstr, maxstr);
        j1ajm.targetVelocity = Random.Range(targetspeedmin, targetspeedmax);
        j1a.motor = j1ajm;
        j1a.useMotor = true;

        GameObject J1 = Instantiate(jointcomponent, new Vector3(Y1.transform.position.x, Y1.transform.position.y, Y1.transform.position.z + 1), new Quaternion(0, 0, 0, 0));
        Rigidbody J1rb = J1.GetComponent<Rigidbody>();

        j1a.connectedBody = J1rb;
        HingeJoint j1b = J1.AddComponent<HingeJoint>();
        j1b.axis = Vector3.zero;
        j1b.autoConfigureConnectedAnchor = true;


        GameObject S2 = Instantiate(basecomponent, new Vector3(Y1.transform.position.x, Y1.transform.position.y, Y1.transform.position.z + 2), new Quaternion(0, 0, 0, 0));
        Rigidbody S2rb = S2.GetComponent<Rigidbody>();


        j1b.connectedBody = S2rb;
        if (repeat > 0)
        {
            yield return new WaitForSeconds(0.034f);
            StartCoroutine(GenerateZ(S2, repeat));
        }
        else
        {
            yield return new WaitForSeconds(0.034f);
        }
    }

}
