using Units;
using Units.Types;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float arrowSpeed;
    
    private Unit target;
    private Archer archer;
    private bool initialized;
    private bool hitTarget;

    // Update is called once per frame
    void Update()
    {
        if (!initialized) return;

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, arrowSpeed * Time.deltaTime);
        
        if ((transform.position - target.transform.position).magnitude < 0.2f && !hitTarget)
        {
            hitTarget = true;
            archer.NotifyHitTarget();
        }

        if (hitTarget)
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(Unit target, Archer archer)
    {
        this.target = target;
        this.archer = archer;
        initialized = true;
        
        Vector3 dir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}