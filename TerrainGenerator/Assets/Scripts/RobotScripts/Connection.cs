using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : MonoBehaviour {
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
