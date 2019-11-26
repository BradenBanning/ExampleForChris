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

    [Header("Raycast")]
    [Space]
    [Header("Spawning Limitations")]
    [Tooltip("The Maximum Value a Raycast can bet be directed towards on the X Axis")]
    [SerializeField]
    private float _MaxX = 90f;

    [Tooltip("The Maximum Value a Raycast can bet be directed towards on the Y Axis")] [SerializeField]
    private float _MaxY = 90f;

    [Tooltip("The Maximum Value a Raycast can bet be directed towards on the Z Axis")] [SerializeField]
    private float _MaxZ = 90f;

    [Tooltip("The Minimum Value a Raycast can bet be directed towards on the X Axis")] [SerializeField]
    private float _MinX = -90f;

    [SerializeField] [Tooltip("The Minimum Value a Raycast can bet be directed towards on the Y Axis")]
    private float _MinY = -90f;

    [SerializeField] [Tooltip("The Minimum Value a Raycast can bet be directed towards on the Z Axis")]
    private float _MinZ = -90f;

    [Tooltip("The Max Distance a Raycast can travel")] [SerializeField]
    private float _MaxDistance = 50f;

    [Tooltip("The Number of Units you want to Spawn per Key Press")] [SerializeField]
    private int _NumberOfUnitsToSpawn = 10;

    [Tooltip("The Layers you want the Raycast to Collide with, the Raycast determines where a unit can spawn")] [SerializeField]
    private LayerMask _RaycastLayerMask;
    
    [Tooltip("Can the Raycast Collide with Triggers?")] [SerializeField]
    private QueryTriggerInteraction _CanCollideWithTriggers;
    
    [Space]
    [Header("OverlapSphere")]
    [Tooltip("The Radius of the OverlapSphere")] [SerializeField]
    private float _Radius = 0.5f;

    [Tooltip("The Layers you want the OverlapSphere to Collide with, the OverlapSphere determines if a unit can spawn")] [SerializeField]
    private LayerMask _OverlapSphereLayerMask;
    
    
    
    [Space]
    [Header("Inputs")] 
    [SerializeField] private int _MouseButton;

    private bool _EnableRaycast;
    private Camera _MainCamera;

    private void Awake()
    {
        _MainCamera = Camera.main;
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(_MouseButton))
        {
            _EnableRaycast = true;
        }
    }

    private void FixedUpdate()
    {
        if (_EnableRaycast == true)
        {
            Ray[] rays = new Ray[_NumberOfUnitsToSpawn];

            for (int rIndex = 0; rIndex < rays.Length; rIndex++)
            {
                Vector3 direction = GetRandomDirection();

                RaycastHit hit;
                

                rays[rIndex] = _MainCamera.ScreenPointToRay(Input.mousePosition);


                if (Physics.Raycast(rays[rIndex].GetPoint(10),
                    direction,out hit, _MaxDistance, _RaycastLayerMask, _CanCollideWithTriggers))
                {
                    if(CanSpawn(hit.point) == true)
                    {
                        SpawnRandomUnit(hit.point);
                    }
                }
            }

            _EnableRaycast = false;
        }
    }

    private void SpawnRandomUnit(Vector3 spawnLocation )
    {
        int randomUnitIndex = Random.Range(0, _UnitToSpawn.Length);
        Instantiate(_UnitToSpawn[randomUnitIndex], spawnLocation, Quaternion.identity);
    }

    private bool CanSpawn(Vector3 spawnLocation)
    {
        Collider[] colliders = Physics.OverlapSphere(spawnLocation, _Radius,
            _OverlapSphereLayerMask);
        if (colliders.Length > 0)
        {
            return false;
        }
       

        return true;
    }

    private Vector3 GetRandomDirection()
    {
        float xVector = Random.Range(_MinX, _MaxX);
        float yVector = Random.Range(_MinY, _MaxY);
        float zVector = Random.Range(_MinZ, _MaxZ);
        Vector3 direction = new Vector3(xVector, yVector, zVector);
        return direction;
    }
}