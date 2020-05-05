using UnityEngine;

public class Drink : FindNeeds
{
    public Drink(float searchRadius, Transform transform, float need) : base(searchRadius, transform, need,"Drink")
    {
    }
}