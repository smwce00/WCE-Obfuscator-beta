using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Reflection;
using System.IO;

public class RefactoringNames : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {/*
        List<GameObject> rootObjects = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(rootObjects);

        // iterate root objects and do something
        for (int i = 0; i < rootObjects.Count; ++i)
        {
            GameObject gameObject = rootObjects[i];
            //Debug.Log(gameObject.name);
            //gameObject.DoSomething();
        }

        // 1-1) ����ȭ�� ��ũ��Ʈ�� ���� ���� ������Ʈ���� ��� ã�� (����Ʈ�� ����)
        // 1-2) ����ȭ�� ��ũ��Ʈ�� �Լ��� ���� �׼Ǹ����ʰ� ���� ���� ������Ʈ���� ��� ã�� (����Ʈ�� ����)
        // 2) ��� ��ũ��Ʈ�� ����ȭ
        // 3-1) ���� ����ȭ�� ��ũ��Ʈ���� �ٽ� ���� ������Ʈ�鿡 �ٿ��� (����Ʈ ����)
        // 3-2) ���� ����ȭ�� ��ũ��Ʈ���� �Լ����� �ٽ� �׼� �����ʵ鿡 �ٿ��� (����Ʈ ����)


        
        // 1-1)
        List<string> objToAddScript = new List<string>();
        Object[] GameobjectList = Resources.FindObjectsOfTypeAll(typeof(RefactoringNames));
        foreach (Object obj in GameobjectList) {
            Debug.Log(obj.name);
            objToAddScript.Add(obj.name);
        }
        */

        /*
        // 1-2)
        RefactoringNames[] yourObjects = FindObjectsOfType<RefactoringNames>();
        foreach (RefactoringNames obj in yourObjects)
        {
            
            //Debug.Log(obj.name);
        }
        */


        /*
        foreach (MethodInfo info in typeof(TestForRefactoring).GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly))
        {
            if (!info.IsSpecialName) {
                if (info.Name != "Start" && info.Name != "Update")
                {
                    string temp = "temp";
                    info.Name.Replace((string)info.Name, temp);
                }
            }
            Debug.Log(info.Name);
        }
        */


    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
