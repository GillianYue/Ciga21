using UnityEngine;
using System.Collections;
using UnityEditor.Callbacks;
using UnityEditor;
using UnityEditor.Build;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

#if UNITY_IOS
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;
using System.IO;

public class XCodeProjectSetting
{
    [PostProcessBuildAttribute(999)]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {
        
        if (target != BuildTarget.iOS)
        {
            return;
        }

        string pbxprojPath = PBXProject.GetPBXProjectPath(pathToBuiltProject);
        Debug.Log("pbxprojPath: " + pbxprojPath);
        PBXProject pbxProject = new PBXProject();
        pbxProject.ReadFromFile(pbxprojPath);

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

        string entitlementPath = Path.Combine(pathToBuiltProject, "Unity-iPhone/Unity-iPhone.entitlements");
        PlistDocument entitlement = new PlistDocument();

        var plistPath = Path.Combine(pathToBuiltProject, "Info.plist");
        PlistDocument plist = new PlistDocument();
        plist.ReadFromFile(plistPath);

        string configFilePath = Path.Combine(System.Environment.CurrentDirectory, "Assets/Resources/MopubSDKConfig-ios.json");
        ConfigXCodeByFile(pbxProject, unityIphoneGuid, entitlement, plist, pathToBuiltProject, configFilePath);

        InfoPlistAddBaseConfig(plist);
        plist.WriteToFile(plistPath);

        if (entitlement.root.values.Count != 0)
        {
            entitlement.WriteToFile(entitlementPath);
            ModifyEntitlementHeader(entitlementPath);

            //string entitlementGuid = pbxProject.AddFile(entitlementPath, "Unity-iPhone.entitlements");
            string entitlementGuid = pbxProject.AddFile("Unity-iPhone/Unity-iPhone.entitlements", "Unity-iPhone.entitlements");
            string unityIphoneResourceBuildPhaseGuid = pbxProject.GetResourcesBuildPhaseByTarget(unityIphoneGuid);
            pbxProject.AddFileToBuildSection(unityIphoneGuid, unityIphoneResourceBuildPhaseGuid, entitlementGuid);
            pbxProject.AddBuildProperty(unityIphoneGuid, "CODE_SIGN_ENTITLEMENTS", "Unity-iPhone/Unity-iPhone.entitlements");
        }

        PbxProjectAddBaseConfig(pbxProject, unityIphoneGuid, unityFrameworkGuid);
        pbxProject.WriteToFile(pbxprojPath);
    }

    private static void ConfigXCodeByFile(PBXProject project, string mainTarget, PlistDocument entitlement, PlistDocument plist, string projectPath, string configFilePath)
    {
        // 根据配置文件，配置xcode工程
        
        Debug.Log("search config in path: " + configFilePath);
        if (!System.IO.File.Exists(configFilePath))
        {
            return;
        }

        Debug.Log("find the config file");
        Dictionary<string, string> config = new Dictionary<string, string>();

        string text = File.ReadAllText(configFilePath);
        config = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);

        foreach (string key in config.Keys)
        {
            Debug.Log("Find config " + key + ": " + config[key]);
            if (key == "GoogleClientIDReverse")
            {
                InfoPlistAddURLType(plist, config[key]);
            }
            else if (key == "SKAdNetworkIDs")
            {
                if (config[key].Contains(","))
                {
                    string[] ids = config[key].Split(',');
                    foreach (string id in ids)
                    {
                        InfoPlistAddSKAdNetworkID(plist, id);
                    }
                }
                else
                {
                    InfoPlistAddSKAdNetworkID(plist, config[key]);
                }
                
            }
            else if (key.StartsWith("InfoPlist-"))
            {
                string infoPlistKey = key.Split('-')[1];
                plist.root.SetString(infoPlistKey, config[key]);

                if (key == "InfoPlist-FacebookAppID")
                {
                    ConfigFacebook(config[key], plist);
                }
            }
            else if (key == "apns" && config[key] == "1")
            {
                 ConfigApns(project, mainTarget, projectPath, entitlement);
            }
            else if (key == "signInWithApple" && config[key] == "1")
            {
                ConfigSignInWithApple(project, mainTarget, entitlement);
            }
            else if (key == "associatedDomains")
            {
                string[] domains = null;
                if (config[key].Contains(","))
                {
                    domains = config[key].Split(',');
                }
                else
                {
                    domains = new string[1];
                    domains[0] = config[key];
                }

                ConfigAssociatedDomains(project, mainTarget, entitlement, domains);
            }
            else if (key == "cocoapods")
            {
                string dependencies = config["cocoapods"];
                GeneratePodFile(projectPath, dependencies);
            }
            else if (key == "WXAppID")
            {
                ConfigWechat(config["WXAppID"], plist);
            }
            else if (key == "url-types")
            {
                string[] urlTypes = null;
                if (config[key].Contains(","))
                {
                    urlTypes = config[key].Split(',');
                }
                else
                {
                    urlTypes = new string[1];
                    urlTypes[0] = config[key];
                }

                foreach(string urlType in urlTypes)
                {
                    InfoPlistAddURLType(plist, urlType);
                }
            }
            else if (key == "schemes")
            {
                string[] schemes = null;
                if (config[key].Contains(","))
                {
                    schemes = config[key].Split(',');
                }
                else
                {
                    schemes = new string[1];
                    schemes[0] = config[key];
                }

                foreach (string scheme in schemes)
                {
                    InfoPlistAddQueriesScheme(plist, scheme);
                }
            }
            else if (key == "UIRequiredDeviceCapabilities")
            {
                string[] capabilities = null;
                if (config[key].Contains(","))
                {
                    capabilities = config[key].Split(',');
                }
                else
                {
                    capabilities = new string[1];
                    capabilities[0] = config[key];
                }

                ConfigUIRequiredDeviceCapabilities(plist, capabilities);
            }
        }
    }

    private static void ConfigFacebook(string appId, PlistDocument plist)
    {
        Debug.Log("Config facebook");
        InfoPlistAddURLType(plist, "fb" + appId);
        InfoPlistAddQueriesScheme(plist, "fbapi");
        InfoPlistAddQueriesScheme(plist, "fbapi20130214");
        InfoPlistAddQueriesScheme(plist, "fbapi20130410");
        InfoPlistAddQueriesScheme(plist, "fbapi20130702");
        InfoPlistAddQueriesScheme(plist, "fbapi20131010");
        InfoPlistAddQueriesScheme(plist, "fbapi20131219");
        InfoPlistAddQueriesScheme(plist, "fbapi20140410");
        InfoPlistAddQueriesScheme(plist, "fbapi20140116");
        InfoPlistAddQueriesScheme(plist, "fbapi20150313");
        InfoPlistAddQueriesScheme(plist, "fbapi20150629");
        InfoPlistAddQueriesScheme(plist, "fbapi20160328");
        InfoPlistAddQueriesScheme(plist, "fbauth");
        InfoPlistAddQueriesScheme(plist, "fb-messenger-share-api");
        InfoPlistAddQueriesScheme(plist, "fbauth2");
        InfoPlistAddQueriesScheme(plist, "fbshareextension");
    }

    private static void ConfigWechat(string appId, PlistDocument plist)
    {
        Debug.Log("Config wechat");
        InfoPlistAddURLType(plist, appId);
        InfoPlistAddQueriesScheme(plist, "weixinURLParamsAPI");
        InfoPlistAddQueriesScheme(plist, "weixin");
        InfoPlistAddQueriesScheme(plist, "weixinULAPI");
    }

    private static void InfoPlistAddURLType(PlistDocument plist, string scheme)
    {
        Debug.Log("Add url type in Info.plist: " + scheme);

        PlistElementArray urlTypes = null;
        if (plist.root.values.ContainsKey("CFBundleURLTypes"))
        {
            urlTypes = plist.root.values["CFBundleURLTypes"].AsArray();
        }
        else
        {
            urlTypes = plist.root.CreateArray("CFBundleURLTypes");
        }

        PlistElementDict type = urlTypes.AddDict();
        PlistElementArray schemes = type.CreateArray("CFBundleURLSchemes");
        schemes.AddString(scheme);
    }

    private static void InfoPlistAddQueriesScheme(PlistDocument plist, string scheme)
    {
        Debug.Log("Add query scheme: " + scheme);

        PlistElementArray schemes = null;
        if (plist.root.values.ContainsKey("LSApplicationQueriesSchemes"))
        {
            schemes = plist.root.values["LSApplicationQueriesSchemes"].AsArray();
        }
        else
        {
            schemes = plist.root.CreateArray("LSApplicationQueriesSchemes");
        }

        schemes.AddString(scheme);
    }

    private static void InfoPlistAddSKAdNetworkID(PlistDocument plist, string id)
    {
        Debug.Log("Add SKAdNetworkId: " + id);

        PlistElementArray items = null;
        if (plist.root.values.ContainsKey("SKAdNetworkItems"))
        {
            items = plist.root.values["SKAdNetworkItems"].AsArray();
        }
        else
        {
            items = plist.root.CreateArray("SKAdNetworkItems");
        }

        PlistElementDict item = items.AddDict();
        item.SetString("SKAdNetworkIdentifier", id);
    }

    private static void InfoPlistAddBaseConfig(PlistDocument plist)
    {
        PlistElementDict appTransportSecurity = plist.root.CreateDict("NSAppTransportSecurity");
        appTransportSecurity.SetBoolean("NSAllowsArbitraryLoads", true);
    }

    private static void ConfigApns(PBXProject project, string mainTarget, string projectPath, PlistDocument entitlement)
    {
        project.AddCapability(mainTarget, PBXCapabilityType.PushNotifications);
        entitlement.root.SetString("aps-environment", "development");

        string preprocessorPath = projectPath + "/Classes/Preprocessor.h";
        string text = File.ReadAllText(preprocessorPath);
        text = text.Replace("UNITY_USES_REMOTE_NOTIFICATIONS 0", "UNITY_USES_REMOTE_NOTIFICATIONS 1");
        File.WriteAllText(preprocessorPath, text);
    }

    private static void ConfigSignInWithApple(PBXProject project, string mainTarget, PlistDocument entitlement)
    {
        project.AddCapability(mainTarget, PBXCapabilityType.SignInWithApple);
        PlistElementArray array = entitlement.root.CreateArray("com.apple.developer.applesignin");
        array.AddString("Default");
    }

    private static void ConfigAssociatedDomains(PBXProject project, string mainTarget, PlistDocument entitlement, string[] domains)
    {
        project.AddCapability(mainTarget, PBXCapabilityType.AssociatedDomains);
        PlistElementArray domainsElement = entitlement.root.CreateArray("com.apple.developer.associated-domains");
        foreach(string domain in domains)
        {
            domainsElement.AddString(domain);
        }
    }

    private static void ConfigUIRequiredDeviceCapabilities(PlistDocument plist, string[] capabilities)
    {

        PlistElementArray items = null;

        items = plist.root.CreateArray("UIRequiredDeviceCapabilities");

        foreach (string capability in capabilities)
        {
            Debug.Log("Add UIRequiredDeviceCapabilities: " + capability);
            items.AddString(capability);
        }
    }

    private static void ModifyEntitlementHeader(string path)
    {
        if (!File.Exists(path))
        {
            return;
        }

        try
        {
            StreamReader reader = new StreamReader(path);
            var content = reader.ReadToEnd().Trim();
            reader.Close();

            if (content.Contains("< !DOCTYPE plist PUBLIC \"-//Apple//DTD PLIST 1.0//EN\" \"http://www.apple.com/DTDs/PropertyList-1.0.dtd\" >"))
            {  
                return;
            }
            var needFindString = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?>";
            var changeString = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?>\n< !DOCTYPE plist PUBLIC \"-//Apple//DTD PLIST 1.0//EN\" \"http://www.apple.com/DTDs/PropertyList-1.0.dtd\" >";
            content = content.Replace(needFindString, changeString);
            StreamWriter writer = new StreamWriter(new FileStream(path,FileMode.Create));
            writer.WriteLine(content);
            writer.Flush();
            writer.Close();
        }
        catch (Exception e)
        {
            Debug.Log("ModifyEntitlementFile failed: " + e.Message);
        }
    }

    private static void PbxProjectAddBaseConfig(PBXProject project, string mainTarget, string frameworkTarget)
    {
        project.AddFrameworkToProject(mainTarget, "libz.tbd", false);
        project.AddFrameworkToProject(mainTarget, "AppTrackingTransparency.framework", true);
        project.AddFrameworkToProject(mainTarget, "AdServices.framework", true);
        project.AddFrameworkToProject(mainTarget, "UserNotifications.framework", true);
        project.AddBuildProperty(mainTarget, "OTHER_LDFLAGS", "-ObjC");

        project.AddFrameworkToProject(frameworkTarget, "libz.tbd", false);
        project.AddFrameworkToProject(frameworkTarget, "AppTrackingTransparency.framework", true);
        project.AddFrameworkToProject(frameworkTarget, "AdServices.framework", true);
        project.AddFrameworkToProject(frameworkTarget, "UserNotifications.framework", true);

        project.SetBuildProperty(mainTarget, "ENABLE_BITCODE", "false");
        project.SetBuildProperty(frameworkTarget, "ENABLE_BITCODE", "false");
        project.SetBuildProperty(mainTarget, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");
        project.AddBuildProperty(frameworkTarget, "FRAMEWORK_SEARCH_PATHS", "$(SRCROOT)");
        project.AddBuildProperty(frameworkTarget, "FRAMEWORK_SEARCH_PATHS", "$(inherited)");
        project.AddBuildProperty(mainTarget, "FRAMEWORK_SEARCH_PATHS", "$(SRCROOT)");
        project.AddBuildProperty(mainTarget, "FRAMEWORK_SEARCH_PATHS", "$(inherited)");

        project.SetBuildProperty(mainTarget, "ARCHS", "arm64");
        project.SetBuildProperty(frameworkTarget, "ARCHS", "arm64");
    }

    private static void GeneratePodFile(string projectPath, string dependencies)
    {
        string content = "use_frameworks!\n\nsource 'http://gitlab.getapk.cn/liangjiahao/JodoSpecs.git'\n\nplatform :ios, '12.0'\n\ntarget 'Unity-iPhone' do\n\n#dependencies_start\n" + dependencies + "\n#dependencies_end\n\nend\n\ntarget 'UnityFramework' do\n\n#dependencies_start\n" + dependencies + "\n#dependencies_end\n\nend";
        string path = Path.Combine(projectPath, "Podfile");

        StreamWriter writer;
        FileInfo info = new FileInfo(path);
        writer = info.CreateText();

        writer.WriteLine(content);
        writer.Close();
        writer.Dispose();
    }
}

#endif
