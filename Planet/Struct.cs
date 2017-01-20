using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Planet
{
    public struct Point
    {
        public double x, y, z;
        public Point(double a, double b, double c)
        {
            x = a;
            y = b;
            z = c;
        }
        public Point(Point a)
        {
            x = a.x;
            y = a.y;
            z = a.z;
        }
        public Point(Point a,Point b)
        {
            x = (a.x + b.x) / 2;
            y = (a.y + b.y) / 2;
            z = (a.z + b.z) / 2;
        }
        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.x+p2.x,p1.y+p2.y,p1.z+p2.z);
        }
        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.x - p2.x, p1.y - p2.y, p1.z - p2.z);
        }
        public static Point operator *(Point p1, int a)
        {
            return new Point(p1.x*a, p1.y*a, p1.z*a);
        }
        public static Point operator /(Point p1,int a)
        {
            return new Point(p1.x /a, p1.y/a, p1.z /a);
        }
    }
    public struct KRSU
    {
        public double k,r,s,u;
        public KRSU(double a, double b, double c,double d)
        {
            k = a;
            r = b;
            s = c;
            u = d;
        }
    }
    public struct Flat
    {
        int i;
        public int id;
        public Point[] p;
        public Color fil;
        public bool set;
        public Flat(Point[] a,Color b)
        {
            p = new Point[3];
            for(i=0;i< 3;i++)
            {
                p[i] = new Point(a[i].x, a[i].y, a[i].z);
            }
            fil = b;
            set = false;
            id = 0;
        }
        public Flat(Flat a)
        {
            p = new Point[3];
            for (i = 0; i < 3; i++)
                p[i] = new Point(a.p[i]);
            fil = a.fil;
            set = a.set;
            id = a.id;
        }
    }
}
