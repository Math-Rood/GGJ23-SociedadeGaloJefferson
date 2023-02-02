using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour

{
    private float _length, _startPos;
    [SerializeField] private float speedParallax;
    [SerializeField] private GameObject cam;
    
    void Start()
    {

        _startPos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;

    }
    
    void Update()
    {
        ParallaxExecution();
    }

    private void ParallaxExecution()
    {
        float temp = (cam.transform.position.x * (1 - speedParallax));
        float dist = (cam.transform.position.x * speedParallax);

        transform.position = new Vector3(_startPos + dist, transform.position.y, transform.position.z);

        if (temp > _startPos + _length/2)
        {
            _startPos += _length;
        }
        else
        {
            if (temp < _startPos - _length/2)
            {
                _startPos -= _length;
            }
        }
    }
    

}