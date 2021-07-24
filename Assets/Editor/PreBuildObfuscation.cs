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
        // 1) ����ȭ �� ����������ϴ� ���� ������Ʈ �̸� ����Ʈ �̱�
        List<Object> objToAddScript = new List<Object>();
        Object[] GameobjectList = Resources.FindObjectsOfTypeAll(typeof(GameObject));
        foreach (Object obj in GameobjectList)
        {
            Debug.Log(obj.name);
            objToAddScript.Add(obj);
        }

        // 2) ����ȭ (Ŭ����/�Լ��� ����) �� ���ϸ� ����
        string str = File.ReadAllText("Assets/Scripts/TestForRefactoring.cs");
        str = str.Replace("Test1", "EditedTest1");
        str = str.Replace("TestForRefactoring", "EditedTestForRefactoring");
        File.WriteAllText("Assets/Scripts/EditedTestForRefactoring.cs", str);

        // 3) ��ũ��Ʈ ���ϸ���� ���� - �� �� ���� ��ũ��Ʈ���� ����������

        
        // 4) ���� ������ �ָ� �ٿ��ֱ�
        foreach (GameObject obj in objToAddScript) {
            bool exists=false;
            if (File.Exists("EditedTestForRefactoring.cs")) exists = true;
#if exists
            obj.AddComponent<EditedTestForRefactoring>();
#endif
        }


        // ����Ʈ
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
