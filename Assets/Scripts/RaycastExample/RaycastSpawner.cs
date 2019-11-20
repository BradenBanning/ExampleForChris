using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RaycastSpawner : MonoBehaviour
{
    [Header("AI To Spawn")]
    [Tooltip("Each Element in the Array will Contain a different Unit to Spawn")]
    [SerializeField] 
    private UnitToSpawn[] _UnitToSpawn;

    [Header("Spawning Limitations")]
    [Tooltip("The Maximum Value a Raycast can bet be directed towards on the X Axis")]
    [SerializeField]
    private float _MaxX = 90f;
    [Tooltip("The Maximum Value a Raycast can bet be directed towards on the Y Axis")]
    [SerializeField]
    private float _MaxY = 90f;
    [Tooltip("The Maximum Value a Raycast can bet be directed towards on the Z Axis")]
    [SerializeField]
    private float _MaxZ = 90f;
    [Tooltip("The Minimum Value a Raycast can bet be directed towards on the X Axis")]
    [SerializeField]
    private float _MinX = -90f;
    [SerializeField]
    [Tooltip("The Minimum Value a Raycast can bet be directed towards on the Y Axis")]
    private float _MinY = -90f;
    [SerializeField]
    [Tooltip("The Minimum Value a Raycast can bet be directed towards on the Z Axis")]
    private float _MinZ = -90f;
    [Tooltip("The Max Distance a Raycast can travel")] 
    [SerializeField]
    private float _MaxDistance = 50f;
    [Tooltip("The Number of Units you want to Spawn per Key Press")]
    [SerializeField]
    private int _NumberOfUnitsToSpawn = 10;
    [Tooltip("The Layers you want the Raycast to Collide with")] 
    [SerializeField]
    private LayerMask _LayerMask;
    [Tooltip("Can the Raycast Collide with Triggers?")] 
    [SerializeField]
    private QueryTriggerInteraction _CanCollideWithTriggers;
    
    
    
    [Header("Inputs")]
    [SerializeField] 
    private KeyCode _KeyCode;

    private bool _EnableRaycast;
    private Camera _MainCamera;

    private void Awake()
    {
        _MainCamera = Camera.main;
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _EnableRaycast = true;
            
            Debug.Log("Fire1");
        }
    }

    private void FixedUpdate()
    {
        if (_EnableRaycast == true)
        {
            Ray[] rays = new Ray[_NumberOfUnitsToSpawn];

            for (int rIndex = 0; rIndex < rays.Length; rIndex++)
            {
                
            
            
                float xVector = Random.Range(_MinX, _MaxX);
                float yVector = Random.Range(_MinY, _MaxY);
                float zVector = Random.Range(_MinZ, _MaxZ);
                
                Vector3 direction = new Vector3(xVector, yVector, zVector);

                RaycastHit hit;
                
                //rays[rIndex] = _MainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(/*rays[rIndex].GetPoint(100)*/transform.position, direction,out hit, _MaxDistance, _LayerMask,
                    _CanCollideWithTriggers))
                {
                    int randomUnitIndex = Random.Range(0, _UnitToSpawn.Length);
                    Instantiate(_UnitToSpawn[randomUnitIndex], hit.point, Quaternion.identity);
                }
            }
            
            _EnableRaycast = false;
        }
    }
}


