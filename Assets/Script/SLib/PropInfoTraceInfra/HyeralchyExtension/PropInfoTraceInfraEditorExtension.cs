// ä«óùé“ êõè¿
using UnityEditor;
using UnityEngine;
public class PropInfoTraceInfraEditorExtension
{
    [MenuItem("GameObject/PropInfoTraceInfra(RSEngine)/Observer", false, 10)]
    static void CreateObserverGameObject(MenuCommand menuCommand)
    {
        // Create a custom game object
        GameObject go = new GameObject("Observer");
        // Ensure it gets reparented if this was a context click (otherwise does nothing)
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
        go.AddComponent<PropertyInfoObserver>();
    }
    [MenuItem("GameObject/PropInfoTraceInfra(RSEngine)/UserLinker", false, 10)]
    static void CreateLinkerGameObject(MenuCommand menuCommand)
    {
        // Create a custom game object
        GameObject go = new GameObject("UserLinker");
        // Ensure it gets reparented if this was a context click (otherwise does nothing)
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
        go.AddComponent<PropertyInfoHandlerLinker>();
    }
}
