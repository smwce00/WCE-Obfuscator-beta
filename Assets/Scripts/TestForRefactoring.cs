using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TestForRefactoring : MonoBehaviour
{
    public void Test1() { }

    public void Test2() { }

    public void Test3() { }

    // Start is called before the first frame update
    void Start()
    {
        Test1();
        Test2();
        Test3();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}