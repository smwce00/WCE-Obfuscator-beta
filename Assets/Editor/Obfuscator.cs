using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using Mono.Cecil;

public class Obfuscator
{
#if !UNITY_EDITOR
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        // Deobfuscate the assembly to run the program
        //DeobfuscateAssembly();
        Debug.Log("Before first Scene loaded");
    }
#endif

    private static string path;
    private static string originalAssemblyPath;
    private static string obfuscatedAssemblyPath;
    private static string deobfuscatedAssemblyPath;

    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget build, string pathToBuiltProject) {
        path = @pathToBuiltProject;
        Debug.Log(path);
        Debug.Log("Build Complete.");
        Debug.Log("Obfuscation in Progress...");
        //ObfuscateAssembly();
        Debug.Log("Obfuscation Complete");
    }

    private static void ObfuscateAssembly() {
        originalAssemblyPath = Path.GetDirectoryName(path) + "/" + Application.productName + "_Data/Managed/Assembly-CSharp.dll";
        obfuscatedAssemblyPath = Path.GetDirectoryName(path) + "/" + Application.productName + "_Data/Managed/Assembly-CSharp_Obfuscated.dll";
        
        AssemblyDefinition obfuscatedAsm;
        using (AssemblyDefinition asm = AssemblyDefinition.ReadAssembly(originalAssemblyPath)) { 
            foreach (TypeDefinition t in asm.MainModule.Types)
            {
                foreach (FieldDefinition f in t.Fields)
                {
                    f.Name = "F" + f.Name;
                }
            }
            obfuscatedAsm = asm;
            obfuscatedAsm.Write(obfuscatedAssemblyPath);
        }
        
        Debug.Log("NEARLY DONE!");
        FileInfo obfusFileInfo = new FileInfo(obfuscatedAssemblyPath);
        obfusFileInfo.Replace(originalAssemblyPath, null, false);
        Debug.Log("WCE - OBFUSCATION COMPLETE");
    }

    private static void DeobfuscateAssembly() {
        originalAssemblyPath = Path.GetDirectoryName(path) + "/" + Application.productName + "_Data/Managed/Assembly-CSharp.dll";
        deobfuscatedAssemblyPath = Path.GetDirectoryName(path) + "/" + Application.productName + "_Data/Managed/Assembly-CSharp_Deobfuscated.dll";

        AssemblyDefinition deobfuscatedAsm;
        using (AssemblyDefinition asm = AssemblyDefinition.ReadAssembly(originalAssemblyPath))
        {
            foreach (TypeDefinition t in asm.MainModule.Types)
            {
                foreach (FieldDefinition f in t.Fields)
                {
                    f.Name = f.Name.Substring(1);
                }
            }
            deobfuscatedAsm = asm;
            deobfuscatedAsm.Write(deobfuscatedAssemblyPath);
        }

        FileInfo deobfusFileInfo = new FileInfo(deobfuscatedAssemblyPath);
        deobfusFileInfo.Replace(originalAssemblyPath, null, false);
        Debug.Log("WCE - DEOBFUSCATION COMPLETE");
    }
}