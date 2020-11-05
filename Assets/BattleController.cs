using System.Collections.Generic;
using Units;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public static BattleController instance;
    
    
    [SerializeField] private List<Unit> aliveUnits = new List<Unit>();
    
    
    public Unit GetNearestAlly(Unit unit)
    {
        List<Unit> allies = aliveUnits.FindAll(u => u.faction == unit.faction && u.gameObject.GetInstanceID() != unit.gameObject.GetInstanceID());
        return GetClosestUnit(unit, allies);
    }

    public Unit GetNearestEnemy(Unit unit)
    {
        List<Unit> enemies = aliveUnits.FindAll(u => u.faction != unit.faction && u.gameObject.GetInstanceID() != unit.gameObject.GetInstanceID());
        return GetClosestUnit(unit, enemies);
    }

    public void NotifyDeath(GameObject go)
    {
        aliveUnits.Remove(go.GetComponent<Unit>());
    }

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        ScanUnits();
    }

    private void ScanUnits()
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        foreach (var goUnit in units)
        {
            aliveUnits.Add(goUnit.GetComponent<Unit>());
        }
    }

    private void Initialize()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private Unit GetClosestUnit(Unit unit, List<Unit> units)
    {
        if (units.Count == 0) return null;
        Unit closestOtherUnit = units[0];
        float closestDistance = GetDistanceToUnit(unit, units[0]);

        foreach (var otherUnit in units)
        {
            if (GetDistanceToUnit(unit, otherUnit) < closestDistance)
            {
                closestOtherUnit = otherUnit;
                closestDistance = GetDistanceToUnit(unit, otherUnit);
            }
        }

        return closestOtherUnit;
    }

    private float GetDistanceToUnit(Unit a, Unit b)
    {
        return (a.transform.position - b.transform.position).magnitude;
    }
}