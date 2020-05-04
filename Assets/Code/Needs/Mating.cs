using UnityEngine;

public class Mating : IFindNeeds
{
    private float _searchRadius;
    private Transform _transform;

    public Mating(float SearchRadius, Transform transform)
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
            if (collider.transform.CompareTag("Player"))
            {
                var point = collider.ClosestPointOnBounds(_transform.position);
                if (Vector3.Distance(point, _transform.position) < 2f)
                {
                    if (collider.transform.CompareTag("Player"))
                    {
                        System.Random rnd = new System.Random();
                        var x = (float) rnd.NextDouble() * 100f;
                        var z = (float) rnd.NextDouble() * -100f;
                        GameObject.Instantiate(_transform.gameObject, new Vector3(x, 1, z),
                            Quaternion.identity, _transform.parent);
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