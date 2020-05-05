using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    private System.Random _rnd;
    public float MovementSensitivity = 0.05f;
    private float _age;
    private TextMesh _textHolder;
    public float NeedsSensitivity;
    public float SearchRadius = 15f;
    private Dictionary<string, FindNeeds> _needsFinder;

    void Start()
    {
        _age = 100f;
        _rnd = new System.Random();
        _textHolder = GetComponentInChildren<TextMesh>();
        _needsFinder = new Dictionary<string, FindNeeds>()
        {
            {"Drink", new Drink(SearchRadius, transform, 75f)},
            {"Mating", new Mating(SearchRadius, transform, 120f)},
            {"Food", new Food(SearchRadius, transform, 100f)}
        };
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, SearchRadius);
    }

    void FixedUpdate()
    {
        UpdateNeeds();
        UpdateText();
        if (FindNeeds(out var towards))
        {
            Move(true, towards);
        }
        else
        {
            Move();
        }

        Kill();
    }

    private bool FindNeeds(out Vector3 towards)
    {
        if (_needsFinder["Food"].Need < _needsFinder["Mating"].Need &&
            _needsFinder["Food"].Need < _needsFinder["Drink"].Need)
        {
            Debug.Log("Search Food");
            return _needsFinder["Food"].Find(out towards);
        }

        if (_needsFinder["Mating"].Need < _needsFinder["Food"].Need &&
            _needsFinder["Mating"].Need < _needsFinder["Drink"].Need)
        {
            Debug.Log("Search Mate");
            return _needsFinder["Mating"].Find(out towards);
        }

        if (_needsFinder["Drink"].Need < _needsFinder["Mating"].Need &&
            _needsFinder["Drink"].Need < _needsFinder["Food"].Need)
        {
            Debug.Log("Search Drink");
            return _needsFinder["Drink"].Find(out towards);
        }

        return _needsFinder["Drink"].Find(out towards);
    }

    private void UpdateNeeds()
    {
        //_age -= NeedsSensitivity;
        foreach (var need in _needsFinder.Values)
        {
            need.Need -= NeedsSensitivity;
        }
    }

    private void UpdateText()
    {
        string text = "";
        foreach (var need in _needsFinder.Values)
        {
            text += need.ToString() + '\n';
        }

        _textHolder.text = text;
    }

    private void Kill()
    {
        if (_age < 0.1f)
        {
            Destroy(gameObject);
        }

        foreach (var need in _needsFinder.Values)
        {
            if (need.Need < 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Move(bool needs = false, Vector3 toward = default(Vector3))
    {
        if (needs)
        {
            toward.y = transform.position.y;
        }

        if (Physics.Raycast(transform.position, transform.forward - new Vector3(0, 2, 0), out var hit))
        {
            if (hit.transform.CompareTag("Ground"))
            {
                if (!needs)
                {
                    var rand = Mathf.RoundToInt((float) (_rnd.NextDouble() * 15));
                    transform.Rotate(Vector3.up, rand - 7.5f);
                }
                else
                {
                    transform.LookAt(toward);
                }

                transform.position += transform.forward * MovementSensitivity;
            }
            else
            {
                transform.Rotate(Vector3.up, 180f);
            }
        }
        else
        {
            transform.Rotate(Vector3.up, 180f);
        }
    }
}