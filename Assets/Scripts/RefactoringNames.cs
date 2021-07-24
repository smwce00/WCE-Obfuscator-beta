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

        // 1-1) 난독화될 스크립트가 붙은 게임 오브젝트들을 모두 찾아 (리스트로 저장)
        // 1-2) 난독화될 스크립트의 함수에 대해 액션리스너가 붙은 게임 오브젝트들을 모두 찾아 (리스트로 저장)
        // 2) 모든 스크립트들 난독화
        // 3-1) 새로 난독화된 스크립트들을 다시 게임 오브젝트들에 붙여줘 (리스트 참고)
        // 3-2) 새로 난독화된 스크립트들의 함수들을 다시 액션 리스너들에 붙여줘 (리스트 참고)


        
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
