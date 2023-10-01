using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEditor.U2D.Aseprite.Common;
using UnityEngine;

namespace UnityEditor.U2D.Aseprite
{
    [InitializeOnLoad]
    internal static class BackgroundImport
    {
        static readonly string k_AssetsPath = "Assets/";
        static readonly List<string> s_AssetsFullPath = new List<string>();
        static bool s_HasChange = false;
        static bool s_LastSettingsValue = false;

        static FileSystemWatcher s_Watcher = null;

        static BackgroundImport()
        {
            EditorApplication.update += OnUpdate;
            if (ImportSettings.backgroundImport)
                SetupWatcher();

            s_LastSettingsValue = ImportSettings.backgroundImport;
        }

        static void OnUpdate()
        {
            if (EditorApplication.isCompiling) 
                return;
            if (EditorApplication.isUpdating) 
                return;

            CheckForSettingsUpdate();
            CheckForChange();
        }

        static void CheckForSettingsUpdate()
        {
            if (ImportSettings.backgroundImport == s_LastSettingsValue)
                return;

            if (ImportSettings.backgroundImport)
                SetupWatcher();
            else
                StopWatcher();
            
            s_LastSettingsValue = ImportSettings.backgroundImport;
        }
        
        static void SetupWatcher()
        {
            if (Application.isBatchMode)
                return;
            
            ThreadPool.QueueUserWorkItem(MonitorDirectory, k_AssetsPath);
        }

        static void MonitorDirectory(object obj)
        {
            var path = (string)obj;

            s_Watcher = new FileSystemWatcher();
            s_Watcher.Path = path;
            s_Watcher.IncludeSubdirectories = true;
            s_Watcher.Changed += OnChangeDetected;
            s_Watcher.Created += OnChangeDetected;
            s_Watcher.Renamed += OnChangeDetected;
            s_Watcher.EnableRaisingEvents = true;
        }
        
        static void OnChangeDetected(object sender, FileSystemEventArgs e)
        {
            var extension = Path.GetExtension(e.FullPath);
            if (extension != ".aseprite" && extension != ".ase")
                return;

            s_AssetsFullPath.Add(e.FullPath);
            s_HasChange = true;
        }

        static void StopWatcher()
        {
            if (s_Watcher != null)
            {
                s_Watcher.Dispose();
                s_Watcher = null;
            }
        }

        static void CheckForChange()
        {
            if (!s_HasChange) 
                return;
            // If the editor is already focused, skip forced import.
            if (UnityEditorInternal.InternalEditorUtility.isApplicationActive)
            {
                s_HasChange = false;
                return;
            }
            if (Application.isPlaying)
                return;
            
            AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport & ImportAssetOptions.ForceUpdate);

            var relativePaths = new List<string>(s_AssetsFullPath.Count);
            for (var i = 0; i < s_AssetsFullPath.Count; ++i)
                relativePaths.Add(FileUtil.GetProjectRelativePath(s_AssetsFullPath[i]));
            
            AssetDatabase.ForceReserializeAssets(relativePaths);
            InternalEditorBridge.RefreshInspectors();

            s_AssetsFullPath.Clear();
            s_HasChange = false;
        }
    }
}