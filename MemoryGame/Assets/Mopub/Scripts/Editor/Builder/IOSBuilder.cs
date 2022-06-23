using UnityEngine;
using System.Collections;
using UnityEditor.Callbacks;
using UnityEditor;
using UnityEditor.Build;

#if UNITY_IOS
using UnityEditor.iOS.Xcode;
using System.IO;

public class IOSBuilder 
{

    [PostProcessBuildAttribute(0)]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string pathToBuiltProject)
    {
        if (buildTarget != BuildTarget.iOS)
            return;

        // init
        string projPath = PBXProject.GetPBXProjectPath(pathToBuiltProject);
        PBXProject proj = new PBXProject();
        proj.ReadFromString(File.ReadAllText(projPath));
        
        
        Debug.Log("============ xcode build begin ==============");
        Debug.Log(projPath);

        EditInfoPlist(pathToBuiltProject);

        string mainTargetGuid;
        string unityFrameworkTargetGuid;
        var unityMainTargetGuidMethod = proj.GetType().GetMethod("GetUnityMainTargetGuid");
        var unityFrameworkTargetGuidMethod = proj.GetType().GetMethod("GetUnityFrameworkTargetGuid");
        if (unityMainTargetGuidMethod != null && unityFrameworkTargetGuidMethod != null) {
            mainTargetGuid = (string)unityMainTargetGuidMethod.Invoke(proj, null);
            unityFrameworkTargetGuid = (string)unityFrameworkTargetGuidMethod.Invoke(proj, null);

        }
        else
        {
            mainTargetGuid = proj. TargetGuidByName("Unity-iPhone");
            unityFrameworkTargetGuid = mainTargetGuid;
        }

        string target = unityFrameworkTargetGuid;

        ///config buildSettings
        proj.SetBuildProperty(mainTargetGuid, "ENABLE_BITCODE", "false");
        proj.SetBuildProperty(target, "ENABLE_BITCODE", "false");
        proj.SetBuildProperty(mainTargetGuid, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");
        proj.AddBuildProperty(target, "FRAMEWORK_SEARCH_PATHS", "$(SRCROOT)");
        proj.AddBuildProperty(target, "FRAMEWORK_SEARCH_PATHS", "$(inherited)");
        proj.AddBuildProperty(mainTargetGuid, "FRAMEWORK_SEARCH_PATHS", "$(SRCROOT)");
        proj.AddBuildProperty(mainTargetGuid, "FRAMEWORK_SEARCH_PATHS", "$(inherited)");

        ///save
        proj.WriteToFile(projPath);
        Debug.Log("============ xcode build end ==============");
    }

    static void EditInfoPlist(string filePath)
    {
        string path = filePath + "/Info.plist";

        PlistDocument plistDocument = new PlistDocument();
        plistDocument.ReadFromFile(path);

        PlistElementDict root = plistDocument.root.AsDict();

        PlistElementDict dicSecurity = root.CreateDict("NSAppTransportSecurity");
        dicSecurity.SetBoolean("NSAllowsArbitraryLoads", true);

        plistDocument.WriteToFile(path);
    }
}
#endif
