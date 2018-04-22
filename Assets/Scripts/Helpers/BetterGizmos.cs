using UnityEngine;
using System.Collections;

public class BetterGizmos {

    public static void DrawCircle(Vector3 center, float radius) {
        Vector3 offset = new Vector3(Mathf.Cos(0) * radius, 0, Mathf.Sin(0) * radius);
        for (float r = 0; r < 2 * Mathf.PI; r += 0.1f) {
            Vector3 next = new Vector3(Mathf.Cos(r + 0.1f) * radius, 0, Mathf.Sin(r + 0.1f) * radius);
            Gizmos.DrawLine(center + offset, center + next);
            offset = next;
        }
    }
    
    public static Color color {
        get {
            return Gizmos.color;
        }
        set {
            Gizmos.color = value;
        }
    }

    public static Matrix4x4 matrix {
        get {
            return Gizmos.matrix;
        }
        set {
            Gizmos.matrix = value;
        }
    }
    
    public static void DrawCube (Vector3 center, Vector3 size) {
        Gizmos.DrawCube(center, size);
    }

    public static void DrawFrustum (Vector3 center, float fov, float maxRange, float minRange, float aspect) {
        Gizmos.DrawFrustum(center, fov, maxRange, minRange, aspect);
    }

    public static void DrawGUITexture (Rect screenRect, Texture texture){
        Gizmos.DrawGUITexture(screenRect, texture);
    }

    public static void DrawGUITexture (Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder){
        Gizmos.DrawGUITexture(screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder);
    }

    public static void DrawGUITexture (Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Material mat){
        Gizmos.DrawGUITexture(screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder, mat);
    }

    public static void DrawGUITexture (Rect screenRect, Texture texture, Material mat){
        Gizmos.DrawGUITexture(screenRect, texture, mat);
    }

    public static void DrawIcon (Vector3 center, string name, bool allowScaling){
        Gizmos.DrawIcon(center, name, allowScaling);
    }

    public static void DrawIcon (Vector3 center, string name){
        Gizmos.DrawIcon(center, name);
    }

    public static void DrawLine (Vector3 from, Vector3 to){
        Gizmos.DrawLine(from, to);
    }

    public static void DrawMesh (Mesh mesh, int submeshIndex, Vector3 position){
        Gizmos.DrawMesh(mesh, submeshIndex, position);
    }

    public static void DrawMesh (Mesh mesh, int submeshIndex){
        Gizmos.DrawMesh(mesh, submeshIndex);
    }

    public static void DrawMesh (Mesh mesh, int submeshIndex, Vector3 position, Quaternion rotation){
        Gizmos.DrawMesh(mesh, submeshIndex, position, rotation);
    }

    public static void DrawMesh (Mesh mesh, Vector3 position, Quaternion rotation){
        Gizmos.DrawMesh(mesh, position, rotation);
    }

    public static void DrawMesh (Mesh mesh, Vector3 position){
        Gizmos.DrawMesh(mesh, position);
    }

    public static void DrawMesh (Mesh mesh){
        Gizmos.DrawMesh(mesh);
    }

    public static void DrawMesh (Mesh mesh, Vector3 position, Quaternion rotation, Vector3 scale){
        Gizmos.DrawMesh(mesh, position, rotation, scale);
    }

    public static void DrawMesh (Mesh mesh, int submeshIndex, Vector3 position, Quaternion rotation, Vector3 scale){
        Gizmos.DrawMesh(mesh, submeshIndex, position, rotation, scale);
    }

    public static void DrawRay (Ray r){
        Gizmos.DrawRay(r);
    }

    public static void DrawRay (Vector3 from, Vector3 direction){
        Gizmos.DrawRay(from, direction);
    }

    public static void DrawSphere (Vector3 center, float radius){
        Gizmos.DrawSphere(center, radius);
    }

    public static void DrawWireCube (Vector3 center, Vector3 size){
        Gizmos.DrawWireCube(center, size);
    }

    public static void DrawWireMesh (Mesh mesh, int submeshIndex){
        Gizmos.DrawWireMesh(mesh, submeshIndex);
    }

    public static void DrawWireMesh (Mesh mesh, Vector3 position){
        Gizmos.DrawWireMesh(mesh, position);
    }

    public static void DrawWireMesh (Mesh mesh){
        Gizmos.DrawWireMesh(mesh);
    }

    public static void DrawWireMesh (Mesh mesh, Vector3 position, Quaternion rotation, Vector3 scale){
        Gizmos.DrawWireMesh(mesh, position, rotation, scale);
    }

    public static void DrawWireMesh (Mesh mesh, int submeshIndex, Vector3 position, Quaternion rotation, Vector3 scale){
        Gizmos.DrawWireMesh(mesh, submeshIndex, position, rotation, scale);
    }

    public static void DrawWireMesh (Mesh mesh, int submeshIndex, Vector3 position, Quaternion rotation){
        Gizmos.DrawWireMesh(mesh, submeshIndex, position, rotation);
    }

    public static void DrawWireMesh (Mesh mesh, int submeshIndex, Vector3 position){
        Gizmos.DrawWireMesh(mesh, submeshIndex, position);
    }

    public static void DrawWireMesh (Mesh mesh, Vector3 position, Quaternion rotation){
        Gizmos.DrawWireMesh(mesh, position, rotation);
    }

    public static void DrawWireSphere (Vector3 center, float radius){
        Gizmos.DrawWireSphere(center, radius);
    }
}
