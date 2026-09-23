#if UNITY_EDITOR
using System;
using System.IO;
using System.Linq;
using Mono.Cecil;
using Packages.UnityCompose.Editor.Utils;
using SharpExtensions;
using StableCollections;
using UnityEditor;
using UnityEngine;

namespace Packages.UnityCompose.Editor.Rewriters;

// [InitializeOnLoad]
// public static class ComposeAssetsPostProcessor
// {
//     static ComposeAssetsPostProcessor()
//     {
//         EditorApplication.delayCall += Patch;
//     }
//     
//     private static void Patch()
//     { 
//         var time = TimeUtils.Measure(() =>
//         {
//             var asmdefs = AssetDatabase.FindAssets("", new[] { "Assets", "Packages" })
//                 .Select(AssetDatabase.GUIDToAssetPath)
//                 .Select(it => new FileInfo(it))
//                 .Where(it => it.Extension == ".asmdef")
//                 .Select(it => it.Name.Replace(".asmdef", ""))
//                 .ToImmutableStableSet();
//             var allAssets = AssetDatabase.FindAssets("", new[] { "Assets", "Packages", "Library" })
//                 .Select(AssetDatabase.GUIDToAssetPath)
//                 .Select(it => new FileInfo(it))
//                 .Where(it => it.Extension == ".dll")
//                 .Where(it => asmdefs.Contains(it.Name.Replace(".dll", "")))
//                 .ToImmutableStableList();
//             Debug.Log(allAssets);
//             foreach (var fileInfo in allAssets)
//             {
//                 var assembly = AssemblyDefinitionFactory.Create(fileInfo);
//                 if (assembly == null)
//                     continue;
//                 var messages = ComposableMethodRewriter.Patch(assembly);
//                 foreach (var message in messages)
//                 {
//                     Debug.LogWarning(message.MessageData);
//                 }
//             }
//         });
//         Debug.Log($"Patched in {time.TotalSeconds}");
//     }
// }
#endif