using UnityEngine;

public class Food : FindNeeds
{
    
    public Food(float searchRadius, Transform transform, float need) : base(searchRadius, transform, need,"Food")
    {
        
    }
}