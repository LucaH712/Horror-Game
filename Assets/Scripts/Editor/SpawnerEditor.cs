using UnityEngine;
using UnityEditor;
namespace Horror
{
    [CustomEditor(typeof(Spawner))]
    public class SpawnerEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Spawn"))
            {
                (target as Spawner).Spawn();
            }
            base.OnInspectorGUI();
        }
    }
}
