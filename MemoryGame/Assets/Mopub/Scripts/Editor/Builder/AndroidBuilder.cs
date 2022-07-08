using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

#if UNITY_ANDROID
using System;
using System.IO;
using System.Text;

public class AndroidBuilder {
    private static bool closeExtralBuild = false;
    private static bool useUnityFirebaseSDK = false;

    [PostProcessBuildAttribute (1)]
    public static void OnPostprocessBuild (BuildTarget target, string pathToBuiltProject) {
        if (target != BuildTarget.Android)
            return;

        if (closeExtralBuild) {
            return;
        }
        Debug.Log ("Extral Build Start：" + pathToBuiltProject);
        try {
            string fileDir = System.Environment.CurrentDirectory;
            Debug.Log ("当前的工作目录为: " + fileDir);
            StringBuilder sb = new StringBuilder ();
            
#if UNITY_2019_3_OR_NEWER
            Debug.Log ("UNITY_2019_3_OR_NEWER");

            string activityName = "com.mopub.v2.UnityPlayerActivity";

            if (useUnityFirebaseSDK)
            {
                activityName = "com.mopub.v2.MessagingUnityPlayerActivity";
            }

            // using 语句也能关闭 StreamReader
            //项目级gradle
            using (StreamReader sr = new StreamReader (Path.Combine (pathToBuiltProject, "build.gradle"))) {
                string line;
                // 从文件读取并显示行，直到文件的末尾 
                while ((line = sr.ReadLine ()) != null) {
                    sb.Append ("\n" + line);
                    if (line.Contains ("jcenter()")) {
                        sb.Append ("\n			 maven { url 'https://sdk.tapjoy.com'}");

                        sb.Append ("\n			 maven { url 'https://jitpack.io' }");

                        sb.Append ("\n			 maven { url 'https://developer.huawei.com/repo/' }");

                        sb.Append ("\n			mavenCentral()");

                        sb.Append ("\n			 maven { url 'http://mve.130qq.com/repository/CasualSdk/' }\n");

                        sb.Append ("\n			 maven { url 'https://android-sdk.is.com' }");

                        sb.Append ("\n			 maven { url  'https://dl-maven-android.mintegral.com/repository/mbridge_android_sdk_oversea' }");

                        sb.Append ("\n			 maven { url 'https://dl-maven-android.mintegral.com/repository/mbridge_android_sdk_china' }");

                        sb.Append ("\n			 maven {url 'https://artifact.bytedance.com/repository/pangle'}");

                        sb.Append ("\n			 maven {url 'https://dl-maven-android.mintegral.com/repository/mbridge_android_sdk_support/'}");

                    }
                }
            }

            using (StreamWriter sw = new StreamWriter (Path.Combine (pathToBuiltProject, "build.gradle"))) {
                sw.Write (sb.ToString ());
            }

            sb.Replace (sb.ToString (), "");
            //launcher build.gradle
            using (StreamReader sr = new StreamReader (Path.Combine (pathToBuiltProject, "launcher/build.gradle"))) {
                string line;
                while ((line = sr.ReadLine ()) != null) {
                    sb.Append ("\n" + line);
                    if (line.Contains ("versionName")) {
                        sb.Append ("\n		multiDexEnabled true");
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter (Path.Combine (pathToBuiltProject, "launcher/build.gradle"))) {
                sw.Write (sb.ToString ());
            }

            sb.Replace (sb.ToString (), "");

            //unityLibrary build.gradle
            using (StreamReader sr = new StreamReader (Path.Combine (fileDir, "Assets/Mopub/Scripts/Editor/Builder/AndroidMainActivityGradle"))) {
                string line;
                while ((line = sr.ReadLine ()) != null) {
                    if (line.Contains ("HUB_ACTIVITY")) {
                        line = line.Replace ("HUB_ACTIVITY", "com.mopub.v2.UnityPlayerActivity");
                    }
                    sb.Append ("\n" + line);
                }
            }

            using (StreamReader sr = new StreamReader (Path.Combine (pathToBuiltProject, "unityLibrary/build.gradle"))) {
                string line;
                while ((line = sr.ReadLine ()) != null) {
                    sb.Append ("\n" + line);
                }
            }

            using (StreamWriter sw = new StreamWriter (Path.Combine (pathToBuiltProject, "unityLibrary/build.gradle"))) {
                sw.Write (sb.ToString ());
            }

            sb.Replace (sb.ToString (), "");
            //gradle.properties
            using (StreamWriter sw = new StreamWriter (Path.Combine (pathToBuiltProject, "gradle.properties"), true)) {
                sb.Append ("\nandroid.useAndroidX=true");
                sb.Append ("\nandroid.enableJetifier=true");
                sw.Write (sb.ToString ());
            }
            sb.Replace (sb.ToString (), "");
            
            //改为用导出工程路径形式

//  #else
//              Debug.Log ("UNITY_2019_2_OR_OLDER");
//             //mainTemplate.gradle
//             using (StreamReader sr = new StreamReader (Path.Combine (pathToBuiltProject, "unity-sample/build.gradle"))) {
//                 using (StreamReader sr1 = new StreamReader (Path.Combine (fileDir, "Assets/Mopub/Scripts/Editor/Builder/AndroidMainActivityGradle"))) {
//                     string line1;
//                     while ((line1 = sr1.ReadLine ()) != null) {
//                         if (line1.Contains ("HUB_ACTIVITY")) {
//                             line1 = line1.Replace ("HUB_ACTIVITY", "com.mopub.UnityPlayerActivity");
//                         }
//                         sb.Append ("\n" + line1);
//                     }
//                 }
//                 string line;
//                 // 从文件读取并显示行，直到文件的末尾 
//                 while ((line = sr.ReadLine ()) != null) {
//                     if(line.Contains("apply plugin: 'com.android.application'")){
//                         sb.Append("\n([rootProject] + (rootProject.subprojects as List)).each {");
//                         sb.Append("\n    ext {");
//                         sb.Append("\n        it.setProperty(\"android.useAndroidX\", true)");
//                         sb.Append("\n        it.setProperty(\"android.enableJetifier\", true)");
//                         sb.Append("\n    }");
//                         sb.Append("\n}");
//                     }
//                     sb.Append ("\n" + line);
//                     Debug.Log(line);
//                     if (line.Contains ("jcenter()")) {
//                         sb.Append ("\n			maven { url 'https://jitpack.io' }");
//                         sb.Append ("\n			maven { url \"http://mve.130qq.com/repository/CasualSdk/\" }");
//                         sb.Append ("\n			maven { url 'https://dl.bintray.com/ironsource-mobile/android-sdk'}");
//                         sb.Append ("\n			mavenCentral()");
//                         sb.Append ("\n			maven { url \"https://fyber.bintray.com/marketplace\" }");
//                         sb.Append ("\n			maven { url 'https://developer.huawei.com/repo/' }\n");
//                     }
//                     if (line.Contains ("versionName")) {
//                         sb.Append ("\n        multiDexEnabled true");
//                     }
//                 }
//             }

//             using (StreamWriter sw = new StreamWriter (Path.Combine (pathToBuiltProject, "unity-sample/build.gradle"))) {
//                 sw.Write (sb.ToString ());
//             }
            


#endif

        } catch (Exception e) {
            Debug.Log ("The file could not be read:" + e.Message);
        }
    }

     [PostProcessSceneAttribute (1)]
    public static void OnPostprocessScene(){   
        if (closeExtralBuild) {
            return;
        }
        Debug.Log("OnPostprocessScene");
#if !UNITY_2019_3_OR_NEWER
        try{
            StringBuilder sb = new StringBuilder ();
            string fileDir = System.Environment.CurrentDirectory;
            Debug.Log ("UNITY_2019_2_OR_OLDER");
            string activityName = "com.mopub.UnityPlayerActivity";

            if (useUnityFirebaseSDK)
            {
                activityName = "com.mopub.MessagingUnityPlayerActivity";
            }
            sb.Append("// MODIFIED BY AndroidBuilder.cs.");
            
            //mainTemplate.gradle
            using (StreamReader sr = new StreamReader (Path.Combine (fileDir, "Assets/Plugins/Android/mainTemplate.gradle"))) {
                using (StreamReader sr1 = new StreamReader (Path.Combine (fileDir, "Assets/Mopub/Scripts/Editor/Builder/AndroidMainActivityGradle"))) {
                    string line1;
                    while ((line1 = sr1.ReadLine ()) != null) {
                        if (line1.Contains ("HUB_ACTIVITY")) {
                            line1 = line1.Replace ("HUB_ACTIVITY", "com.mopub.UnityPlayerActivity");
                        }
                        sb.Append ("\n" + line1);
                    }
                }
                string line;
                // 从文件读取并显示行，直到文件的末尾 
                while ((line = sr.ReadLine ()) != null) {
                    if(line.Contains("// MODIFIED BY AndroidBuilder.cs.")){
                        Debug.Log("Return by modified");
                        return;
                    }
                    if(line.Contains("apply plugin: 'com.android.application'")){
                        sb.Append("\n([rootProject] + (rootProject.subprojects as List)).each {");
                        sb.Append("\n    ext {");
                        sb.Append("\n        it.setProperty(\"android.useAndroidX\", true)");
                        sb.Append("\n        it.setProperty(\"android.enableJetifier\", true)");
                        sb.Append("\n    }");
                        sb.Append("\n}");
                    }
                    sb.Append ("\n" + line);
                    Debug.Log(line);
                    if (line.Contains ("jcenter()")) {
                        sb.Append ("\n			 maven { url 'https://sdk.tapjoy.com'}");

                        sb.Append ("\n			 maven { url 'https://jitpack.io' }");

                        sb.Append ("\n			 maven { url 'https://developer.huawei.com/repo/' }");

                        sb.Append ("\n			mavenCentral()");

                        sb.Append ("\n			 maven { url 'http://mve.130qq.com/repository/CasualSdk/' }\n");

                        sb.Append ("\n			 maven { url 'https://android-sdk.is.com' }");

                        sb.Append ("\n			 maven { url  'https://dl-maven-android.mintegral.com/repository/mbridge_android_sdk_oversea' }");

                        sb.Append ("\n			 maven { url 'https://dl-maven-android.mintegral.com/repository/mbridge_android_sdk_china' }");

                        sb.Append ("\n			 maven {url 'https://artifact.bytedance.com/repository/pangle'}");

                        sb.Append ("\n			 maven {url 'https://dl-maven-android.mintegral.com/repository/mbridge_android_sdk_support/'}");
                    }
                    if (line.Contains ("versionName")) {
                        sb.Append ("\n        multiDexEnabled true");
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter (Path.Combine (fileDir, "Assets/Plugins/Android/mainTemplate.gradle"))) {
                sw.Write (sb.ToString ());
            }
        } catch (Exception e) {
            Debug.Log ("The file could not be read:" + e.Message);
        }
#endif
    }
}
#endif