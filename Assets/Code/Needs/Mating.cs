using UnityEngine;

public class Mating : FindNeeds
{
    public Mating(float searchRadius, Transform transform, float need) : base(searchRadius, transform, need, "Player")
    {
    }

    protected override void FoundNeed()
    {
        base.FoundNeed();
        Debug.Log("Mating");
    }
}