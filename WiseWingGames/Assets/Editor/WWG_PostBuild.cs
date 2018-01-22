using UnityEditor.Callbacks;
using UnityEngine;
using UnityEditor.iOS.Xcode;
using System.IO;
using UnityEditor;

public class WWG_PostBuild : MonoBehaviour {

    [PostProcessBuild(700)]
    public static void OnPostProcessBuild(BuildTarget target, string pathToBuiltProject)
    {
        if (target != BuildTarget.iOS)
        {
            Debug.Log("WiseWingGames: Returning because build is not for iOS.");
            return;
        }
        Debug.Log("WiseWingGames: Starting post build operation for iOS ");
        string projPath = pathToBuiltProject + "/Unity-iPhone.xcodeproj/project.pbxproj";
        PBXProject proj = new PBXProject();
        proj.ReadFromString(File.ReadAllText(projPath));
        string targetGUID = proj.TargetGuidByName("Unity-iPhone");
        Debug.Log("WiseWingGames: Adding linker flag.");
        proj.AddBuildProperty(targetGUID, "OTHER_LDFLAGS", "-ObjC");
        File.WriteAllText(projPath, proj.WriteToString());
    }
}
