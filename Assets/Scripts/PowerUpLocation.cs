using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PowerUpLocation
{
    private bool occupied;
    private float x, y, z;

    public PowerUpLocation(float x1, float y1, float z1)
    {
        occupied = false;
        x = x1;
        y = y1;
        z = z1;
    }

    public bool Occupied
    {
        get { return this.occupied; }
        set { this.occupied = value; }
    }

    public float X
    {
        get { return this.x; }
        set { this.x = value; }
    }

    public float Y
    {
        get { return this.y; }
        set { this.y = value; }
    }

    public float Z
    {
        get { return this.z; }
        set { this.z = value; }
    }
}
