using UnityEngine;

public class Drink : IFindNeeds
{
    private float _searchRadius;
    private Transform _transform;

    public Drink(float SearchRadius, Transform transform)
    {
        _searchRadius = SearchRadius;
        _transform = transform;
    }

    public bool FindNeeds(out Vector3 towards)
    {
        towards = default(Vector3);
        var sphereCastHits =
            Physics.OverlapSphere(_transform.position, _searchRadius);
        foreach (var collider in sphereCastHits)
        {
            if (collider.transform.CompareTag("Water"))
            {
                var point = collider.ClosestPointOnBounds(_transform.position);
                if (Vector3.Distance(point, _transform.position) < 2f)
                {
                    if (collider.transform.CompareTag("Water"))
                    {
                        
                    }

                    return false;
                }

                towards = point;
                return true;
            }
        }

        return false;
    }
}