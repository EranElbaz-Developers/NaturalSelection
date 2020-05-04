using UnityEngine;

public class Food : IFindNeeds
{
    private float _searchRadius;
    private Transform _transform;

    public Food(float SearchRadius, Transform transform)
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
            if (collider.transform.CompareTag("Food"))
            {
                var point = collider.ClosestPointOnBounds(_transform.position);
                if (Vector3.Distance(point, _transform.position) < 2f)
                {
                    if (collider.transform.CompareTag("Food"))
                    {
                        System.Random rnd = new System.Random();
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