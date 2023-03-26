using System;
using UnityEngine;

[Serializable]
public class SerializableVector3
{
    public float x;
    public float y;
    public float z;

    public static readonly SerializableVector3 Zero = new SerializableVector3(Vector3.zero);

    public SerializableVector3(Vector3 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }

    public static SerializableVector3 operator +(SerializableVector3 a, SerializableVector3 b)
    {
        return new SerializableVector3(new Vector3(a.x, a.y, a.z) + new Vector3(b.x, b.y, b.z));
    }
}