using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System.IO;
using System.Reflection;

public class PreBuildObfuscation : IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }
    public void OnPreprocessBuild(BuildReport report)
    {
        // 1) 난독화 후 수정해줘야하는 게임 오브젝트 이름 리스트 뽑기
        List<Object> objToAddScript = new List<Object>();
        Object[] GameobjectList = Resources.FindObjectsOfTypeAll(typeof(GameObject));
        foreach (Object obj in GameobjectList)
        {
            Debug.Log(obj.name);
            objToAddScript.Add(obj);
        }

        // 2) 난독화 (클래스/함수명 변경) 및 파일명 변경
        string str = File.ReadAllText("Assets/Scripts/TestForRefactoring.cs");
        str = str.Replace("Test1", "EditedTest1");
        str = str.Replace("TestForRefactoring", "EditedTestForRefactoring");
        File.WriteAllText("Assets/Scripts/EditedTestForRefactoring.cs", str);

        // 3) 스크립트 파일명까지 변경 - 이 때 기존 스크립트들이 떨어져나감

        
        // 4) 새로 생성된 애를 붙여주기
        foreach (GameObject obj in objToAddScript) {
            bool exists=false;
            if (File.Exists("EditedTestForRefactoring.cs")) exists = true;
#if exists
            obj.AddComponent<EditedTestForRefactoring>();
#endif
        }


        // 리포트
        Debug.Log("MyCustomBuildProcessor.OnPreprocessBuild for target " + report.summary.platform + " at path " + report.summary.outputPath);

        /*
public void EditorialResponse(string fileName, string word, string replacement, string saveFileName)
{
    FileStream fcreate = File.Open("Assets/Scripts/TestForRefactoring.cs", FileMode.Create);


    StreamReader reader = new StreamReader("Assets/Scripts/" + fileName);
    string input = reader.ReadToEnd();

    using (StreamWriter writer = new StreamWriter("Assets/Scripts/" + saveFileName, false))
    {
        {
            string output = input.Replace(word, replacement);
            reader.Dispose();
            File.Delete("Assets/Scripts/" + fileName);
            writer.Write(output);
        }
        writer.Close();
    }
}
*/
    }
}
