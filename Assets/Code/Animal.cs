using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Animal : MonoBehaviour
{
    private System.Random _rnd;
    public float MovementSensitivity = 0.05f;
    private float _age;
    private float _thirst;
    private float _hunger;
    private float _mating;
    private TextMesh _textHolder;
    public float NeedsSensitivity;
    public float SearchRadius = 15f;
    private Dictionary<string, IFindNeeds> _needsFinder;

    void Start()
    {
        _age = 100f;
        _thirst = 100f;
        _hunger = 100f;
        _mating = 150f;
        _rnd = new System.Random();
        _textHolder = GetComponentInChildren<TextMesh>();
        _needsFinder = new Dictionary<string, IFindNeeds>()
        {
            {"Drink", new Drink(SearchRadius, transform)},
            {"Mating", new Mating(SearchRadius, transform)},
            {"Food", new Food(SearchRadius, transform)}
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
        if (_hunger < _mating && _hunger < _thirst)
        {
            return _needsFinder["Food"].FindNeeds(out towards);
        }

        if (_mating < _hunger && _mating < _thirst)
        {
            return _needsFinder["Mating"].FindNeeds(out towards);
        }

        if (_thirst < _mating && _thirst < _hunger)
        {
            return _needsFinder["Drink"].FindNeeds(out towards);
        }

        return _needsFinder["Drink"].FindNeeds(out towards);
    }

    private void UpdateNeeds()
    {
        //_age -= NeedsSensitivity;
        _thirst -= NeedsSensitivity;
        _hunger -= NeedsSensitivity;
        _mating -= NeedsSensitivity;
    }

    private void UpdateText()
    {
        _textHolder.text = "Age: " + _age.ToString() + "\nThirst: " + _thirst + "\nHunder: " + _hunger + "\nMating: " +
                           _mating;
    }

    private void Kill()
    {
        if (_age < 0.1f || _thirst < 0.1f || _hunger < 0.1f)
        {
            Destroy(gameObject);
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