using UnityEngine;
using System.Collections;
using UnityEditor.Callbacks;
using UnityEditor;
using UnityEditor.Build;

#if UNITY_IOS
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;
using System.IO;

public class XCodeProjectSetting
{
    private static string GADAPPLICATIONIDENTIFIER = "ca-app-pub-6513699454148002~1367758153";

    private static string[] skAdNetworkIds = { "SU67R6K2V3.skadnetwor", "2u9pt9hc89.skadnetwork", "4468km3ulz.skadnetwork", "4fzdc2evr5.skadnetwork", "7ug5zh24hu.skadnetwork", "8s468mfl3y.skadnetwork", "9rd848q2bz.skadnetwork", "9t245vhmpl.skadnetwork", "av6w8kgt66.skadnetwork", "f38h382jlk.skadnetwork", "hs6bdukanm.skadnetwork", "kbd757ywx3.skadnetwork", "ludvb6z3bs.skadnetwork", "m8dbw4sv7c.skadnetwork", "mlmmfzh3r3.skadnetwork", "prcb7njmu6.skadnetwork", "t38b2kh725.skadnetwork", "tl55sbb4fm.skadnetwork", "wzmmz9fp6w.skadnetwork", "yclnxrl5pm.skadnetwork", "ydx93a7ass.skadnetwork", "4PFYVQ9L8R.skadnetwork", "YCLNXRL5PM.skadnetwork", "V72QYCH5UU.skadnetwork", "TL55SBB4FM.skadnetwork", "T38B2KH725.skadnetwork", "PRCB7NJMU6.skadnetwork", "PPXM28T8AP.skadnetwork", "MLMMFZH3R3.skadnetwork", "KLF5C3L5U5.skadnetwork", "HS6BDUKANM.skadnetwork", "C6K4G5QG8M.skadnetwork", "9T245VHMPL.skadnetwork", "9RD848Q2BZ.skadnetwork", "8S468MFL3Y.skadnetwork", "7UG5ZH24HU.skadnetwork", "4FZDC2EVR5.skadnetwork", "4468KM3ULZ.skadnetwork", "3RD42EKR43.skadnetwork", "2U9PT9HC89.skadnetwork", "M8DBW4SV7C.skadnetwork", "7RZ58N8NTL.skadnetwork", "EJVT5QM6AK.skadnetwork", "5LM9LJ6JB7.skadnetwork", "44JX6755AQ.skadnetwork", "MTKV5XTK9E.skadnetwork", "KBD757YWX3.skadnetwork", "cstr6suwn9.skadnetwork", "4DZT52R2T5.skadnetwork", "bvpn9ufa9b.skadnetwork", "GTA9LK7P23.skadnetwork", "v9wttpbfk9.skadnetwork", "n38lu8286q.skadnetwork", "238da6jt44.skadnetwork", "22mmun2rn5.skadnetwork"};

    [PostProcessBuildAttribute(999)]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {
        if(target == BuildTarget.iOS)
        {
            string pbxprojPath = PBXProject.GetPBXProjectPath(pathToBuiltProject);
            Debug.Log("pbxprojPath: " + pbxprojPath);
            PBXProject pbxProject = new PBXProject();
            pbxProject.ReadFromFile(pbxprojPath);

            string fileDir = System.Environment.CurrentDirectory;
            Debug.Log("当前的工作目录为: " + fileDir);

            string frameworksDirUnity = Path.Combine(fileDir, "Assets/Mopub/Plugins/iOS/Frameworks");  // unity脚本中的Frameworks目录
            string resourcesDirUnity = Path.Combine(fileDir, "Assets/Mopub/Plugins/iOS/Resources");    // unity脚本中的Resources目录
            string frameworksDirXCode = Path.Combine(pathToBuiltProject, "Frameworks");
            Debug.Log("Frameworks dir: " + frameworksDirUnity);
            Debug.Log("Resources dir: " + resourcesDirUnity);

            string unityIphoneGuid = null;
            string unityFrameworkGuid = null;

            // 适配 2019.3.9.f1 后的版本
            // 多了 UnityFramework
#if UNITY_2019_3_OR_NEWER
            unityIphoneGuid = pbxProject.GetUnityMainTargetGuid();
            unityFrameworkGuid = pbxProject.GetUnityFrameworkTargetGuid();
            Debug.Log("##########2019###########");
#else
            Debug.Log("##########2018###########");
            unityIphoneGuid = pbxProject.TargetGuidByName("Unity-iPhone");
#endif

            // 添加 framework
            if (System.IO.Directory.Exists(frameworksDirUnity))
            {
                string unityIphoneFrameworkBuildPhaseGuid = pbxProject.GetFrameworksBuildPhaseByTarget(unityIphoneGuid);
                string unityFrameworkBuildPhaseGuid = null;
                if (unityFrameworkGuid != null)
                {
                    unityFrameworkBuildPhaseGuid = pbxProject.GetFrameworksBuildPhaseByTarget(unityFrameworkGuid);
                }
                DirectoryInfo frameworksInfo = new DirectoryInfo(frameworksDirUnity);
                foreach (DirectoryInfo nextFolder in frameworksInfo.GetDirectories())
                {
                    if (Path.GetExtension(nextFolder.Name) == ".framework")
                    {
                        string frameworkPathXCode = Path.Combine(pathToBuiltProject, nextFolder.Name);
                        if (System.IO.Directory.Exists(frameworkPathXCode))
                        {
                            string frameworkGuidOld = pbxProject.FindFileGuidByRealPath(frameworkPathXCode);
                            if (frameworkGuidOld != null)
                            {
                                pbxProject.RemoveFile(frameworkGuidOld);
                            }
                        }

                        CopyEntireDir(nextFolder.FullName, frameworkPathXCode);

                        string frameworkGuidNew = pbxProject.AddFile(nextFolder.Name, nextFolder.Name, PBXSourceTree.Source);
                        // pbxProject.AddFileToBuild(unityIphoneGuid, frameworkGuidNew);
                        pbxProject.AddFileToBuildSection(unityIphoneGuid, unityIphoneFrameworkBuildPhaseGuid, frameworkGuidNew);
                        PBXProjectExtensions.AddFileToEmbedFrameworks(pbxProject, unityIphoneGuid, frameworkGuidNew);

                        if (unityFrameworkGuid != null)
                        {
                            // pbxProject.AddFileToBuild(unityFrameworkGuid, frameworkGuidNew);
                            pbxProject.AddFileToBuildSection(unityFrameworkGuid, unityFrameworkBuildPhaseGuid, frameworkGuidNew);

                            // framework search path
                        }
                    }
                }
            }

            // 添加 bundle
            if (System.IO.Directory.Exists(resourcesDirUnity))
            {
                DirectoryInfo resourcesInfo = new DirectoryInfo(resourcesDirUnity);
                foreach (DirectoryInfo nextFolder in resourcesInfo.GetDirectories())
                {
                    if (Path.GetExtension(nextFolder.Name) == ".bundle")
                    {
                        string resourcePathXCode = Path.Combine(pathToBuiltProject, nextFolder.Name);
                        if (System.IO.Directory.Exists(resourcePathXCode))
                        {
                            string resourceGuidOld = pbxProject.FindFileGuidByRealPath(resourcePathXCode);
                            if (resourceGuidOld != null)
                            {
                                pbxProject.RemoveFile(resourceGuidOld);
                            }
                        }

                        CopyEntireDir(nextFolder.FullName, resourcePathXCode);

                        string resourceGuidNew = pbxProject.AddFile(nextFolder.Name, nextFolder.Name);
                        string unityIphoneResourceBuildPhaseGuid = pbxProject.GetResourcesBuildPhaseByTarget(unityIphoneGuid);
                        pbxProject.AddFileToBuildSection(unityIphoneGuid, unityIphoneResourceBuildPhaseGuid, resourceGuidNew);
                    }
                }
            }

            pbxProject.WriteToFile(pbxprojPath);

            // 修改 Info.plist
            var plistPath = Path.Combine(pathToBuiltProject, "Info.plist");
            PlistDocument plist = new PlistDocument();
            plist.ReadFromFile(plistPath);
            plist.root.SetString("GADApplicationIdentifier", GADAPPLICATIONIDENTIFIER);
            plist.root.SetString("NSCalendarsUsageDescription", "This permission comes from the advertising request of the advertising service provider to provide users with more accurate advertising.");
                
            PlistElementDict appTransportSecurity = plist.root.CreateDict("NSAppTransportSecurity");
            appTransportSecurity.SetBoolean("NSAllowsArbitraryLoads", true);

            PlistElementArray skAdNetworkItems = plist.root.CreateArray("SKAdNetworkItems");
            foreach (string skAdNetworkId in skAdNetworkIds)
            {
                PlistElementDict dict = skAdNetworkItems.AddDict();
                dict.SetString("SKAdNetworkIdentifier", skAdNetworkId);
            }
            plist.WriteToFile(plistPath);
        }   
    }

    private static void CopyEntireDir(string sourcePath, string destPath)
    {
        // Create root Dir
        Directory.CreateDirectory(destPath);

        // Now Create all of the directories
        foreach (string dirPath in Directory.GetDirectories(sourcePath, "*",
           SearchOption.AllDirectories))
            Directory.CreateDirectory(dirPath.Replace(sourcePath, destPath));

        // Copy all the files & Replaces any files with the same name
        foreach (string newPath in Directory.GetFiles(sourcePath, "*",
           SearchOption.AllDirectories))
            File.Copy(newPath, newPath.Replace(sourcePath, destPath), true);
    }
}

#endif
