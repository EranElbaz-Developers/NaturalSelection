using UnityEngine;


public abstract class FindNeeds
{
    protected float _searchRadius;
    protected Transform _transform;
    protected float _need;
    protected float _initialNeed;
    protected string _tag;

    public float Need
    {
        get => _need;
        set => _need = value;
    }

    public FindNeeds(float searchRadius, Transform transform, float need, string tag)
    {
        _searchRadius = searchRadius;
        _transform = transform;
        _need = need;
        _initialNeed = need;
        _tag = tag;
    }

    public bool Find(out Vector3 towards)
    {
        towards = default(Vector3);
        var sphereCastHits =
            Physics.OverlapSphere(_transform.position, _searchRadius);
        foreach (var collider in sphereCastHits)
        {
            if (collider.transform.CompareTag(_tag))
            {
                var point = collider.ClosestPointOnBounds(_transform.position);
                if (Vector3.Distance(point, _transform.position) < 2f)
                {
                    if (collider.transform.CompareTag(_tag))
                    {
                        FoundNeed();
                    }

                    return false;
                }

                towards = point;
                return true;
            }
        }

        return false;
    }

    protected virtual void FoundNeed()
    {
        _need = _initialNeed;
    }

    public override string ToString()
    {
        return _tag + ": " + _need;
    }
}