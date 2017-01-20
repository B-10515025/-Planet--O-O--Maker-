using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace Planet
{
    public class Obj
    {
        public String name;
        public TextBox console;
        public Point Location;
        public KRSU B_Location;
        public Flat[] Flats;
        public double Azi, Ele;
        public void write(string a)
        {
            console.Text = console.Text + a + "\r\n";
        }
        static public KRSU setR(Point p)
        {
            KRSU a;
            a.k = Math.Sqrt(p.x * p.x + p.z * p.z);
            a.s = Math.Sqrt(a.k * a.k + p.y * p.y);
            a.r = Math.Atan2(p.x, p.z);
            a.u = Math.Atan2(p.y, a.k);
            return a;
        }
        static public Point setX(KRSU a)
        {
            Point p;
            p.y = a.s * Math.Sin(a.u);
            a.k = a.s * Math.Cos(a.u);
            p.x = a.k * Math.Sin(a.r);
            p.z = a.k * Math.Cos(a.r);
            return p;
        }
        static public Point p_set(Point p, double k)
        {
            Point a;
            KRSU r;
            r = setR(p);
            r.s = k;
            a = setX(r);
            return a;
        }
        public Flat[] Add_flat(Flat[] a, Flat[] b, Point o, double ry,double rx)
        {
            Flat[] c,reg;
            int i,j;
            c = new Flat[a.GetLength(0) + b.GetLength(0)];
            reg = new Flat[b.GetLength(0)];
            for (i = 0; i < a.GetLength(0); i++)
                c[i] = a[i];
            for (i = 0; i < b.GetLength(0); i++)
                reg[i] = new Flat(b[i]);
            reg = rollXYZ(reg, new Point(0,0,0), "Y", ry);
            reg = rollXYZ(reg, new Point(0, 0, 0), "X", rx);
            for (i = 0; i < b.GetLength(0); i++)
            {
                for (j = 0; j < 3; j++)
                    reg[i].p[j] = reg[i].p[j] + o;
                c[i + a.GetLength(0)] = new Flat(reg[i]);
            }
            return c;
        }
        public Flat[] rollXYZ(Flat[] a,Point o,string axis,double angle)
        {
            int i, j;
            Point p;
            double k=0,r=0;
            Flat[] all_flats = new Flat[a.GetLength(0)];
            for (i = 0; i < all_flats.GetLength(0); i++)
            {
                all_flats[i] = new Flat(a[i]);
                for (j = 0; j < 3; j++)
                {
                    p = all_flats[i].p[j] - o;
                    if (axis == "X")
                    {
                        k = Math.Sqrt(p.y * p.y + p.z * p.z);
                        r = Math.Atan2(p.y, p.z);
                    }
                    if (axis == "Y")
                    {
                        k = Math.Sqrt(p.x * p.x + p.z * p.z);
                        r = Math.Atan2(p.x, p.z);
                    }
                    if (axis == "Z")
                    {
                        k = Math.Sqrt(p.y * p.y + p.x * p.x);
                        r = Math.Atan2(p.y, p.x);
                    }
                    r = r + angle;
                    if (axis == "X")
                    {
                        p.y = k * Math.Sin(r);
                        p.z = k * Math.Cos(r);
                    }
                    if (axis == "Y")
                    {
                        p.x = k * Math.Sin(r);
                        p.z = k * Math.Cos(r);
                    }
                    if (axis == "Z")
                    {
                        p.y = k * Math.Sin(r);
                        p.x = k * Math.Cos(r);
                    }
                    all_flats[i].p[j] = p + o;
                }
            }
            return all_flats;
        }
        public Flat[] Makeball(Flat[] f, double R, int t, int k, Color clr)
        {
            Flat[] a = new Flat[0];
            Point[] p = new Point[6];
            int i, j;
            double phi = (1 + Math.Sqrt(5)) / 2;
            if (t == 0)
            {
                a = new Flat[]
                {
                    new Flat(new Point[] { new Point(0,1,phi), new Point(0,-1,phi), new Point(phi,0,1) }, clr),
                    new Flat(new Point[] { new Point(0,1,phi), new Point(0,-1,phi), new Point(-phi,0,1) }, clr),
                    new Flat(new Point[] { new Point(0,1,-phi), new Point(0,-1,-phi), new Point(phi,0,-1) }, clr),
                    new Flat(new Point[] { new Point(0,1,-phi), new Point(0,-1,-phi), new Point(-phi,0,-1) }, clr),
                    new Flat(new Point[] { new Point(1,phi,0), new Point(-1,phi,0), new Point(0,1,phi) }, clr) ,
                    new Flat(new Point[] { new Point(1,phi,0), new Point(-1,phi,0), new Point(0,1,-phi) }, clr) ,
                    new Flat(new Point[] { new Point(1,-phi,0), new Point(-1,-phi,0), new Point(0,-1,phi) }, clr) ,
                    new Flat(new Point[] { new Point(1,-phi,0), new Point(-1,-phi,0), new Point(0,-1,-phi) }, clr) ,
                    new Flat(new Point[] { new Point(phi,0,1), new Point(phi,0,-1), new Point(1,phi,0) }, clr) ,
                    new Flat(new Point[] { new Point(phi,0,1), new Point(phi,0,-1), new Point(1,-phi,0) }, clr) ,
                    new Flat(new Point[] { new Point(-phi,0,1), new Point(-phi,0,-1), new Point(-1,phi,0) }, clr) ,
                    new Flat(new Point[] { new Point(-phi,0,1), new Point(-phi,0,-1), new Point(-1,-phi,0) }, clr) ,
                    new Flat(new Point[] { new Point(0,1,phi), new Point(1,phi,0), new Point(phi,0,1) }, clr),
                    new Flat(new Point[] { new Point(0,1,phi), new Point(-1,phi,0), new Point(-phi,0,1) }, clr),
                    new Flat(new Point[] { new Point(0,-1,phi), new Point(1,-phi,0), new Point(phi,0,1) }, clr),
                    new Flat(new Point[] { new Point(0,1,-phi), new Point(1,phi,0), new Point(phi,0,-1) }, clr),
                    new Flat(new Point[] { new Point(0,-1,phi), new Point(-1,-phi,0), new Point(-phi,0,1) }, clr),
                    new Flat(new Point[] { new Point(0,1,-phi), new Point(-1,phi,0), new Point(-phi,0,-1) }, clr),
                    new Flat(new Point[] { new Point(0,-1,-phi), new Point(1,-phi,0), new Point(phi,0,-1) }, clr),
                    new Flat(new Point[] { new Point(0,-1,-phi), new Point(-1,-phi,0), new Point(-phi,0,-1) }, clr),
                };
                for (i = 0; i < 20; i++)
                    for (j = 0; j < 3; j++)
                        a[i].p[j] = p_set(a[i].p[j], R);
            }
            else
            {
                a = new Flat[f.GetLength(0) * 4];
                for (i = 0; i < f.GetLength(0); i++)
                {
                    for (j = 0; j < 3; j++)
                        p[j] = f[i].p[j];
                    for (j = 0; j < 3; j++)
                        p[j] = f[i].p[j];
                    for (j = 3; j < 6; j++)
                        p[j] = p_set(new Point(f[i].p[j - 3], f[i].p[(j - 2) % 3]), R);
                    a[i * 4] = new Flat(new Point[] { p[0], p[3], p[5] }, clr);
                    a[i * 4 + 1] = new Flat(new Point[] { p[1], p[3], p[4] }, clr);
                    a[i * 4 + 2] = new Flat(new Point[] { p[2], p[4], p[5] }, clr);
                    a[i * 4 + 3] = new Flat(new Point[] { p[3], p[4], p[5] }, clr);
                }
            }
            if (t == k)
            {
                for (i = 0; i < a.GetLength(0); i++)
                    for (j = 0; j < 3; j++)
                        a[i].p[j] = a[i].p[j];
                return a;
            }
            else
                return Makeball(a,R, t + 1, k, clr);
        }
        public Flat[] Maketripill(double h,double l,Color clr)
        {
            Flat[] a;
            Point[] p;
            p = new Point[]
            {
                new Point(0,0,l/Math.Sqrt(3)),
                new Point(l/2, 0,-l / Math.Sqrt(3)/2),
                new Point(-l/2, 0, -l / Math.Sqrt(3)/2),
                new Point(0,h,l/Math.Sqrt(3)),
                new Point(l/2, h,-l / Math.Sqrt(3)/2),
                new Point(-l/2, h, -l / Math.Sqrt(3)/2)
            };
            a = new Flat[]
            {
                new Flat(new Point[] { p[0],p[1],p[2] }, clr),
                new Flat(new Point[] { p[3],p[4],p[5] }, clr),
                new Flat(new Point[] { p[0],p[1],p[4] }, clr),
                new Flat(new Point[] { p[5],p[1],p[2] }, clr),
                new Flat(new Point[] { p[0],p[3],p[2]}, clr),
                new Flat(new Point[] { p[0],p[3],p[4]}, clr),
                new Flat(new Point[] { p[4],p[1],p[5]}, clr),
                new Flat(new Point[] { p[3],p[5],p[2]}, clr)
            };
            return a;
        }
        public Flat[] Maketricone( double h, double l, Color clr)
        {
            Flat[] a;
            Point[] p;
            p = new Point[]
            {
                new Point(0,0,l/Math.Sqrt(3)),
                new Point(l/2, 0,-l / Math.Sqrt(3)/2),
                new Point(-l/2, 0, -l / Math.Sqrt(3)/2),
                new Point(0,h,0),
            };
            a = new Flat[]
            {
                new Flat(new Point[] { p[0],p[1],p[2] }, clr),
                new Flat(new Point[] { p[0],p[1],p[3] }, clr),
                new Flat(new Point[] { p[0],p[2],p[3] }, clr),
                new Flat(new Point[] { p[1],p[2],p[3] }, clr),
            };
            return a;
        }
    }
    public class Comparer : IComparer
    {
        double range;
        public Comparer(double r)
        {
            range = r;
        }
        int IComparer.Compare(Object x, Object y)
        {
            int i;
            double a, b;
            a = 0;
            b = 0;
            for (i = 0; i < 3; i++)
            {
                a = a + ((Flat)x).p[i].z / 3;
                b = b + ((Flat)y).p[i].z / 3;
            }
            if((((Flat)x).fil == Color.FromArgb(115, 74, 18)|| ((Flat)x).fil == Color.FromArgb(255, 0, 0)|| ((Flat)x).fil == Color.Blue) ^ (((Flat)y).fil == Color.FromArgb(115, 74, 18) || ((Flat)y).fil == Color.FromArgb(255, 0, 0) || ((Flat)y).fil == Color.Blue))
            {
                if ((((Flat)x).fil == Color.FromArgb(115, 74, 18) || ((Flat)x).fil == Color.FromArgb(255, 0, 0) || ((Flat)x).fil == Color.Blue) && a < range / 10&& b < range / 5)
                    return -1;
                if ((((Flat)y).fil == Color.FromArgb(115, 74, 18) || ((Flat)y).fil == Color.FromArgb(255, 0, 0) || ((Flat)y).fil == Color.Blue) && a< range / 10&&b < range / 5)
                    return 1;
            }
            if (a < b) return 1;
            if (a > b) return -1;
            return 0;
        }
    }
    public class Planets : Obj
    {
        public double range;
        public List<CreatureType> Creature;
        public List<CreatureType> adds;
        public Planets(double R, Point p)
        {
            range = R;
            Location = p;
            Azi = 0;
            Ele = 0;
            Flats = Makeball(Flats, R, 0, 3, Color.FromArgb(115, 74, 18));
            Creature = new List<CreatureType>();
            adds = new List<CreatureType>();
        }
        public Planets(Planets other)
        {
            int i;
            Flats = new Flat[other.Flats.GetLength(0)];
            for (i = 0; i < other.Flats.GetLength(0); i++)
                Flats[i] = new Flat(other.Flats[i].p, other.Flats[i].fil);
            Location = other.Location;
            Azi = other.Azi;
            Ele = other.Ele;
            Creature = new List<CreatureType>(other.Creature);
            adds = new List<CreatureType>(other.adds);
        }
        public void search(int k)
        {
            int i;
            Point p = new Point(0, 0, 0);
            for (i = 0; i < 3; i++)
                p = p + Flats[k].p[i];
            p = p / 3;
            Azi = Math.Acos(-1) - Obj.setR(p).r;
            Ele = Obj.setR(p).u;
        }
        public void search(CreatureType k)
        {
            Point p;
            p = k.Location;
            Azi = Math.Acos(-1) - Obj.setR(p).r;
            Ele = Obj.setR(p).u;
        }
    }
    public class camera : Obj
    {
        IComparer com;
        public double dep,dep2;
        public bool mode;
        public bool data;
        public int speed;
        public camera(Point p,double a,double b,double c,TextBox t)
        {
            Azi = a;
            Ele = b;
            dep = c;
            dep2 = c;
            speed = 5;
            mode = false;
            data = true;
            Location = p;
            console = t;
        }
        public Bitmap look(Planets pla, int w,int h)
        {
            int i,j,n,r;
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            Bitmap b = new Bitmap(w, h);
            Graphics all = Graphics.FromImage(b);
            all.Clear(Color.FromArgb(50, 50, 50));
            if(rand.Next()%10==1)
            {
                n = rand.Next() % 20 + 10;
                for (i = 0; i < n; i++)
                {
                    r = rand.Next() % 3;
                    all.FillEllipse(new SolidBrush(Color.Silver), rand.Next() % w, rand.Next() % h, 3 + r, 3 + r);
                }
            }
            if(pla!=null)
            {
                Flats = Add_flat(new Flat[0], pla.Flats, new Point(0,0,0), 0,0);
                foreach (CreatureType thing in pla.Creature)
                    Flats = Add_flat(Flats, thing.Flats, thing.Location, thing.Azi, thing.Ele);
                Flats = Add_flat(new Flat[0], Flats, pla.Location, pla.Azi, pla.Ele);
                if(mode)
                {
                    Flats = rollXYZ(Flats, Location - pla.Location, "X", -Math.Acos(-1) / 2);
                    Flats = rollXYZ(Flats, Location - pla.Location, "Y", -Azi);
                    Flats = rollXYZ(Flats, Location - pla.Location, "X", -Ele);
                }
                for (i = 0; i < Flats.GetLength(0); i++)
                    for (j = 0; j < 3; j++)
                        Flats[i].p[j] = Flats[i].p[j] - Location;
                com = new Comparer(pla.range);
                Array.Sort(Flats, com);
                for (i = 0; i < Flats.GetLength(0); i++)
                    Draw(Flats[i], all, w, h);
                if (data)
                    if (mode)
                        all.DrawString("Move Speed: " + speed.ToString() + "\n水平角: " + ((int)(Azi / Math.Acos(-1) * 180) % 360).ToString() + "\n仰角: " + ((int)(Ele / Math.Acos(-1) * 180)).ToString(), new Font("新細明體", 20), new SolidBrush(Color.Purple), 10, 10);
                    else
                    {
                        all.DrawString("Move Speed: " + speed.ToString() + "\n經度: " + ((int)(pla.Azi / Math.Acos(-1) * 180) % 360).ToString() + "\n緯度: " + ((int)(pla.Ele / Math.Acos(-1) * 180)).ToString(), new Font("新細明體", 20), new SolidBrush(Color.Purple), 10, 10);
                        all.DrawLine(new Pen(new SolidBrush(Color.FromArgb(255, 0, 0))), w / 2-5, h / 2, w / 2+5, h / 2);
                        all.DrawLine(new Pen(new SolidBrush(Color.FromArgb(255, 0, 0))), w / 2, h / 2-5, w / 2, h / 2+5);
                    }
            }
            return b;
        }
        public int get_id(Planets pla,int x,int y, int w, int h)
        {
            Flat[] reg = new Flat[pla.Flats.GetLength(0)];
            Point p;
            int i,j,k;
            double X, Y;
            for (i = 0; i < reg.GetLength(0); i++)
            {
                pla.Flats[i].id = i;
                reg[i] = new Flat(pla.Flats[i]);
            }
            reg = rollXYZ(reg, pla.Location, "Y", pla.Azi);
            reg = rollXYZ(reg, pla.Location, "X", pla.Ele);
            Array.Sort(reg, com);
            k = -1;
            for (i = 0; i < reg.GetLength(0); i++)
            {
                p = new Point(0, 0, 0);
                for (j = 0; j < 3; j++)
                    p = p + reg[i].p[j]/3;
                p = p - Location;
                X = w / 2 + (p.x / p.z * dep);
                Y = h / 2 - (p.y / p.z * dep);
                if ((X - x) * (X - x) + (Y - y) * (Y - y) < 100)
                    k = reg[i].id;
            }
            return k;
        }
        private void Draw(Flat f,Graphics g,int w,int h)
        {
            int i,k,n;
            n = 0;
            k = 0;
            PointF[] p;
            Point s;
            for (i = 0; i < 3; i++)
                if (visible(f.p[i]))
                    k++;
            if (k == 3)
            {
                p = new PointF[3];
                for (i = 0; i < 3; i++)
                {
                    p[i].X = w / 2 + (float)(f.p[i].x / f.p[i].z * dep);
                    p[i].Y = h / 2 - (float)(f.p[i].y / f.p[i].z * dep);
                }
                g.FillPolygon(new SolidBrush(f.fil), p);
                if (f.set)
                    g.DrawPolygon(new Pen(Color.Red, 3), p);
                else
                    g.DrawPolygon(new Pen(Color.Black, 1), p);
            }
            else if(k==2)
            {
                p = new PointF[4];
                for (i = 0; i < 3; i++)
                    if (!visible(f.p[i]))
                        n = i;
                s = f.p[n];
                f.p[n] = f.p[2];
                f.p[2] = s;
                s = cross(f.p[0], f.p[2],0);
                f.p[2]=cross(f.p[1], f.p[2],0);
                for (i = 0; i < 3; i++)
                {
                    p[i].X = w / 2 + (float)(f.p[i].x / f.p[i].z * dep);
                    p[i].Y = h / 2 - (float)(f.p[i].y / f.p[i].z * dep);
                }
                p[3].X= w / 2 + (float)(s.x / s.z * dep);
                p[3].Y= h / 2 - (float)(s.y / s.z * dep);
                g.FillPolygon(new SolidBrush(f.fil), p);
                if (f.set)
                    g.DrawPolygon(new Pen(Color.Red, 3), p);
                else
                    g.DrawPolygon(new Pen(Color.Black, 1), p);
            }
            else if(k==1)
            {
                p = new PointF[3];
                for (i = 0; i < 3; i++)
                    if (visible(f.p[i]))
                        n = i;
                s = f.p[n];
                f.p[n] = f.p[0];
                f.p[0] = s;
                f.p[1] = cross(f.p[0], f.p[1],0);
                f.p[2] = cross(f.p[0], f.p[2],0);
                for (i = 0; i < 3; i++)
                {
                    p[i].X = w / 2 + (float)(f.p[i].x / f.p[i].z * dep);
                    p[i].Y = h / 2 - (float)(f.p[i].y / f.p[i].z * dep);
                }
                g.FillPolygon(new SolidBrush(f.fil), p);
                if (f.set)
                    g.DrawPolygon(new Pen(Color.Red, 3), p);
                else
                    g.DrawPolygon(new Pen(Color.Black, 1), p);
            }
        }
        private bool visible(Point p)
        {
            if (p.z <= 0)
                return false;
            else if (Math.Abs(p.x / p.z * dep) >= 100000 || Math.Abs(p.y / p.z * dep) >= 100000)
                return false;
            else
                return true;
        }
        private Point cross(Point a,Point b,int t)
        {
            Point p;
            p = a + b / 2;
            if (visible(p) && t > 10)
                return p;
            else if (t > 20)
                return a;
            else if (visible(p))
                return cross(p, b, t + 1);
            else
                return cross(a, p,t + 1);
        }
    }
    public abstract class CreatureType:Obj
    {
        public CreatureType(TextBox t,string n,int f,Planets p)
        {
            console = t;
            name = n;
            loc_flat = f;
            loc_planet = p;
            write(name + " created!");
        }
        public void set_loc()
        {
            Point a,b;
            Random r = new Random(Guid.NewGuid().GetHashCode());
            Location = new Point(0, 0, 0);
            int i;
            for(i=0;i<3;i++)
            {
                a = loc_planet.Flats[loc_flat].p[(0+i)%3] + (loc_planet.Flats[loc_flat].p[(1 + i) % 3] - loc_planet.Flats[loc_flat].p[(0 + i) % 3]) * (r.Next() % 100) / 100;
                b = loc_planet.Flats[loc_flat].p[(0 + i) % 3] + (loc_planet.Flats[loc_flat].p[(2 + i) % 3] - loc_planet.Flats[loc_flat].p[(0 + i) % 3]) * (r.Next() % 100) / 100;
                Location = Location + a + (b - a) * (r.Next() % 100) / 100;
            }
            Location = Location / 3;
            
        }
        public void set_flat()
        {
            KRSU r;
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            Flats = rollXYZ(Flats, new Point(0, 0, 0), "Y", rand.Next());
            r = setR((loc_planet.Flats[loc_flat].p[0]+ loc_planet.Flats[loc_flat].p[1]+ loc_planet.Flats[loc_flat].p[2])/3);            
            Flats = rollXYZ(Flats, new Point(0, 0, 0), "X",-Math.Acos(-1)/2);
            Flats = rollXYZ(Flats, new Point(0, 0, 0), "Y",r.r);
            Flats = rollXYZ(Flats, new Point(0, 0, 0), "X", r.u);
        }
        public virtual void make_flat() { }
        public virtual void myname() { }
        public virtual void move() { }
        public virtual void absorb() { }
        public virtual bool alive() { return true; }
        public virtual int deadOrAlive() { return 0; }
        public virtual int birth() { return 0; }
        public virtual void auto() { }
        public void update()
        {
            myname();
            move();
            absorb();
            _amount -= deadOrAlive();
            if (alive())
                _amount += birth();
            _amount = _amount < 0 ? 0 : _amount;
        }
        private int _amount = 0;
        public int loc_flat;
        public Planets loc_planet;
        public string type;
    }
    public class Lion : CreatureType
    {
        public Lion(TextBox t,string n,int f,Planets p):base (t,n,f,p)
        {
            type = "Lion";
            set_loc();
            make_flat();
            set_flat();
        }
        public override void make_flat()
        {
            Flats = Add_flat(new Flat[0], Maketripill(2, 1, Color.Yellow), new Point(0, 1, 1), 0, Math.Acos(-1) / 2);
            Flats = Add_flat(Flats, Maketricone(1, 0.5, Color.Yellow), new Point(0.5, 0, 1), 0, 0);
            Flats = Add_flat(Flats, Maketricone(1, 0.5, Color.Yellow), new Point(0.5, 0, -1), 0, 0);
            Flats = Add_flat(Flats, Maketricone(1, 0.5, Color.Yellow), new Point(-0.5, 0, 1), 0, 0);
            Flats = Add_flat(Flats, Maketricone(1, 0.5, Color.Yellow), new Point(-0.5, 0, -1), 0, 0);
            Flats = Add_flat(Flats, Maketricone(1.5 * Math.Sqrt(6) / 3, 1.5, Color.Yellow), new Point(0, 1, 1.1), 0, -Math.Acos(-1) / 2);

        }
        public override void myname() { write("Lion:"+name);write("area:" + loc_flat.ToString()); }
        public override void move(){write("Run Run Run");}
        public override void absorb() {write("Eat Eat Eat"); }
        public override bool alive() { return true; }
        public override int deadOrAlive() { write("Alive"); return 0; }
        public override int birth() { return 0; }
        public override void auto()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            if (rand.Next() % 50 == 1)
            {
                set_loc();
                make_flat();
                set_flat();
            }        
        }
    }
    public class Plant : CreatureType
    {
        int seed = 0;
        int max;
        public Plant(TextBox t,string n ,int f,Planets p,int max_seed):base (t,n,f,p)
        {
            type = "Plant";
            max = max_seed;
            set_loc();
            make_flat();
            set_flat();
        }
        public override void make_flat()
        {
            Flats = Maketripill(5, 3, Color.Brown);
            Flats = Add_flat(Flats, Maketricone(7 * Math.Sqrt(6) / 3, 7, Color.Green), new Point(0, 5, 0), 0, 0);

        }
        public override void myname() { write("Plant:" + name); write("area:" + loc_flat.ToString()); }
        public override void move() { write("Plant cannot move :("); }
        public override void absorb() { write("Growth"); }
        public override bool alive() { return true; }
        public override int deadOrAlive() { write("Alive"); return 0; }
        public override int birth()
        {
            if (seed <= max)
                write("Spread seeds");
            else
                write("No seeds");
            return 0;
        }
        public override void auto()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            if (rand.Next() % 300 == 1&&seed<=max)
            {
                loc_planet.adds.Add(new Plant(console, name + "." + seed.ToString(), loc_flat, loc_planet,max-1));
                seed++;
            }
        }
    }
}
