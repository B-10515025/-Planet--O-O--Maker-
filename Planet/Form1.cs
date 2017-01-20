using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Planet
{
    public partial class Form1 : Form
    {
        int ctrl=0;
        List<int> set_f;
        bool add_f;
        Obj cout;
        camera c;
        Planets myplanet;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            myplanet = null;
            c = null;
            set_f = new List<int>();
            cout = new Obj();
            cout.console = textBox1;
            c = new camera(new Point(0,0,0), 0, 0, (pictureBox1.Height + pictureBox1.Width) / 4, textBox1);
            cout.write("指令--------------------------------------------");
            cout.write("cp <name> <x> <y> <z> <radius>");
            cout.write("新增名為<name> 坐標為(<x>,<y>,<z>)半徑為<radius>的星球。(建議大小500)");
            cout.write("up");
            cout.write("更新星球物件並顯示資訊。");
            cout.write("ro <name>");
            cout.write("移除名稱為<name>的物件。");
            cout.write("ac <type> <name>");
            cout.write("新增<type>種類並名為<name>的生物。(生物種類:Lion、Plant，Lion不能存在於sea中，Plant只能存在於land上)");
            cout.write("help");
            cout.write("顯示操作說明。");
            cout.write("clear");
            cout.write("清除文字版面。");
            cout.write("find <name>");
            cout.write("將視角調整到名為<name>的生物上方。");
            cout.write("set <type>");
            cout.write("將選取區域設定為名稱<type>的區域(區域名稱:land、grass、sea)");
            cout.write("滑鼠--------------------------------------------");
            cout.write("點擊區域中心可選擇/取消區域");
            cout.write("鍵盤--------------------------------------------");
            cout.write("左Ctrl");
            cout.write("切換是否可複選區域");
            cout.write("A/D");
            cout.write("經度/水平角控制");
            cout.write("W/S");
            cout.write("緯度/仰角控制");
            cout.write("Q/E");
            cout.write("鏡頭遠近/高低控制");
            cout.write("R");
            cout.write("創造/觀察者模式切換");
            cout.write("F");
            cout.write("鏡頭資訊顯示/關閉");
            cout.write("Z/C");
            cout.write("鏡頭轉動速度控制");
            cout.write("X");
            cout.write("將鏡頭切換至選取區域上方(限創造模式)");
            cout.write("END--------------------------------------------");
        }
        private void pictureBox1_Click(object sender, EventArgs e )
        {
            int k;
            if(c!=null&&myplanet!=null&&!c.mode)
            {
                k = c.get_id(myplanet, ((MouseEventArgs)e).X, ((MouseEventArgs)e).Y, pictureBox1.Width, pictureBox1.Height);
                if (k >= 0 && k < myplanet.Flats.GetLength(0))
                {
                    myplanet.Flats[k].set = !myplanet.Flats[k].set;
                    if (!add_f)
                    {
                        foreach (int key in set_f)
                            if(key!=k)
                                myplanet.Flats[key].set = false;
                        set_f.Clear();
                    }
                    set_f.Add(k);
                }
            }
            this.KeyPreview = true;
            textBox2.ReadOnly = true;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            ctrl = e.KeyValue;
            if (e.KeyCode == Keys.ControlKey)
                add_f = !add_f;
        }
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            string[] cin;
            int k;
            if (e.KeyCode==Keys.Enter&&textBox2.ReadOnly==false)
            {
                timer1.Enabled = false;
                cin = textBox2.Text.Split(' ');
                textBox2.Text = "";
                k = cin.GetLength(0);
                if (cin[0] == "help" && k == 1)
                {
                    cout.write("指令--------------------------------------------");
                    cout.write("cp <name> <x> <y> <z> <radius>");
                    cout.write("新增名為<name> 坐標為(<x>,<y>,<z>)半徑為<radius>的星球。(建議大小500)");
                    cout.write("up");
                    cout.write("更新星球物件並顯示資訊。");
                    cout.write("ro <name>");
                    cout.write("移除名稱為<name>的物件。");
                    cout.write("ac <type> <name>");
                    cout.write("新增<type>種類並名為<name>的生物。(生物種類:Lion、Plant，Lion不能存在於sea中，Plant只能存在於land上)");
                    cout.write("help");
                    cout.write("顯示操作說明。");
                    cout.write("clear");
                    cout.write("清除文字版面。");
                    cout.write("find <name>");
                    cout.write("將視角調整到名為<name>的生物上方。");
                    cout.write("set <type>");
                    cout.write("將選取區域設定為名稱<type>的區域(區域名稱:land、grass、sea)");
                    cout.write("滑鼠--------------------------------------------");
                    cout.write("點擊區域中心可選擇/取消區域");
                    cout.write("鍵盤--------------------------------------------");
                    cout.write("左Ctrl");
                    cout.write("切換是否可複選區域");
                    cout.write("A/D");
                    cout.write("經度/水平角控制");
                    cout.write("W/S");
                    cout.write("緯度/仰角控制");
                    cout.write("Q/E");
                    cout.write("鏡頭遠近/高低控制");
                    cout.write("R");
                    cout.write("創造/觀察者模式切換");
                    cout.write("F");
                    cout.write("鏡頭資訊顯示/關閉");
                    cout.write("Z/C");
                    cout.write("鏡頭轉動速度控制");
                    cout.write("X");
                    cout.write("將鏡頭切換至選取區域上方(限創造模式)");
                    cout.write("END--------------------------------------------");
                }
                else if (cin[0] == "clear"&&k==1)
                    textBox1.Text = "";
                else if (cin[0] == "find" && k == 2)
                {
                    bool b = true;
                    foreach (CreatureType thing in myplanet.Creature)
                        if (thing.name == cin[1])
                        {
                            myplanet.search(thing);
                            cout.write(cin[1] + " have found!");
                            b = false;
                        }
                    if(b)
                        cout.write(cin[1] + "not found!");
                }
                else if (cin[0] == "cp"&&k == 6)
                {
                    Point p=new Point(0,0,0);
                    double r;
                    if (double.TryParse(cin[2], out p.x) && double.TryParse(cin[3], out p.y) && double.TryParse(cin[4], out p.z) && double.TryParse(cin[5], out r))
                    {
                        if(r>=100)
                        {
                            myplanet = new Planets(r, p);
                            c = new camera(new Point(p.x, p.y, p.z-r*2), 0, 0, (pictureBox1.Height + pictureBox1.Width) / 4, textBox1);
                            myplanet.name = cin[1];
                            cout.write("Planet: " + myplanet.name + " created!");
                        }
                        else
                            cout.write("planet too small!!");
                    }
                    else
                        cout.write("you shall not pass!!");
                }
                else if (cin[0] == "up" && k == 1)
                    if (myplanet != null)
                    {
                        foreach (CreatureType thing in myplanet.Creature)
                            thing.update();
                    }
                    else
                        cout.write("Please create planet first.");
                else if (cin[0] == "ro" && k == 2)
                    if (myplanet != null)
                    {
                        CreatureType move = null;
                        if (myplanet.name == cin[1])
                        {
                            myplanet = null;
                            cout.write(cin[1] + " removed!");
                        }
                        else
                            foreach (CreatureType thing in myplanet.Creature)
                                if (thing.name == cin[1])
                                    move = thing;
                        if (move!=null)
                        {
                            myplanet.Creature.Remove(move);
                            cout.write(cin[1] + " removed!");
                        }
                        else if(myplanet != null)
                            cout.write(cin[1] + "not found!");
                    }
                    else 
                        cout.write("Please create planet first.");
                else if (cin[0] == "ac" && k == 3)
                    if (myplanet != null)
                    {
                        int n=0;
                        int[] area;
                        foreach (int key in set_f)
                            if (myplanet.Flats[key].set)
                                n++;
                        area = new int[n];
                        n = 0;
                        foreach (int key in set_f)
                            if (myplanet.Flats[key].set)
                                area[n++]=key;
                        if (cin[1]=="Lion")
                            if(n!=1)
                                cout.write("please choose an area.");
                            else
                            {
                                bool b = true;
                                foreach (CreatureType thing in myplanet.Creature)
                                    if (thing.name == cin[2])
                                        b = false;
                                if (b)
                                    if (myplanet.Flats[area[0]].fil == Color.Blue)
                                        cout.write("this area can't create");
                                    else
                                        myplanet.Creature.Add(new Lion(textBox1, cin[2], area[0], myplanet));
                                else
                                    cout.write("this name was used.");
                            }
                        else if (cin[1] == "Plant")
                            if (n != 1)
                                cout.write("please choose an area.");
                            else
                            {
                                bool b = true;
                                foreach (CreatureType thing in myplanet.Creature)
                                    if (thing.name == cin[2])
                                        b = false;
                                if (b)
                                    if (myplanet.Flats[area[0]].fil != Color.FromArgb(115, 74, 18))
                                        cout.write("this area can't create");
                                    else
                                        myplanet.Creature.Add(new Plant(textBox1, cin[2], area[0], myplanet,2));
                                else
                                    cout.write("this name was used.");
                            }
                        else
                            cout.write("you shall not pass!!");
                    }
                    else
                        cout.write("Please create planet first.");
                else if (cin[0] == "set" && k == 2)
                    if (myplanet != null)
                    {
                        if (cin[1] == "sea")
                        {
                            List<CreatureType> move = new List<CreatureType>();
                            foreach (int key in set_f)
                            {
                                if(myplanet.Flats[key].set)
                                {
                                    myplanet.Flats[key].fil = Color.Blue;
                                    foreach (CreatureType thing in myplanet.Creature)
                                        if (thing.loc_flat == key)
                                            move.Add(thing);
                                }
                            }
                            foreach (CreatureType move_thing in move)
                                myplanet.Creature.Remove(move_thing);
                            cout.write("area set OK!");
                        }
                        else if (cin[1] == "land")
                        {
                            foreach (int key in set_f)
                                if (myplanet.Flats[key].set) 
                                    myplanet.Flats[key].fil = Color.FromArgb(115, 74, 18);
                            cout.write("area set OK!");
                        }
                        else if (cin[1] == "grass")
                        {
                            List<CreatureType> move = new List<CreatureType>();
                            foreach (int key in set_f)
                            {
                                if (myplanet.Flats[key].set)
                                {
                                    myplanet.Flats[key].fil = Color.FromArgb(0, 255, 0);
                                    foreach (CreatureType thing in myplanet.Creature)
                                        if (thing.loc_flat == key && thing.type == "Plant")
                                            move.Add(thing);
                                }
                            }
                            foreach (CreatureType move_thing in move)
                                myplanet.Creature.Remove(move_thing);
                            cout.write("area set OK!");
                        }
                        else
                            cout.write("you shall not pass!!");
                        pictureBox1.Image = c.look(myplanet, pictureBox1.Width, pictureBox1.Height);
                    }
                    else
                        cout.write("Please create planet first.");
                else
                    cout.write("Doesn't have this command");
                timer1.Enabled = true;
            }
        }
        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.ReadOnly = false;
            this.KeyPreview = false;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Obj obj;
            if(myplanet!=null)
            {
                obj = myplanet;
                if (c.mode)
                    obj = c;
                if (ctrl == 70)
                    c.data = !c.data;
                if (ctrl == 67 && c.speed < 20)
                    c.speed++;
                if (ctrl == 90 && c.speed > 1)
                    c.speed--;
                if (ctrl == 88)
                {
                    int n = 0;
                    foreach (int key in set_f)
                        if (myplanet.Flats[key].set)
                            n++;
                    if (n == 1 && !c.mode)
                    {
                        Point p = new Point(0, 0, 0);
                        foreach (int key in set_f)
                            if (myplanet.Flats[key].set)
                                n = key;
                        myplanet.search(n);
                    }
                }
                if (ctrl == 82)
                {
                    c.mode = !c.mode;
                    if (c.mode)
                    {
                        c.Location = Obj.p_set(c.Location - myplanet.Location, myplanet.range +5) + myplanet.Location;
                        c.dep2 = c.dep;
                        c.dep = (pictureBox1.Height + pictureBox1.Width) / 4;
                    }
                    else
                    {
                        c.Location = Obj.p_set(c.Location - myplanet.Location, myplanet.range *2) + myplanet.Location;
                        c.dep = c.dep2;
                    }
                }
                if (ctrl == 68)
                    obj.Azi = obj.Azi + Math.Acos(-1) / 360*c.speed;
                if (ctrl == 65)
                    obj.Azi = obj.Azi - Math.Acos(-1) / 360 * c.speed;
                if (ctrl == 87)
                    if (obj.Ele + Math.Acos(-1) / 360 * c.speed < Math.Acos(-1) / 2)
                        obj.Ele = obj.Ele + Math.Acos(-1) / 360 * c.speed;
                if (ctrl == 83)
                    if (obj.Ele - Math.Acos(-1) / 360 * c.speed > -Math.Acos(-1) / 2)
                        obj.Ele = obj.Ele - Math.Acos(-1) / 360 * c.speed;
                if (ctrl == 69)
                    if (c.mode)
                    {
                        if (Obj.setR(c.Location - myplanet.Location).s > 1 + myplanet.range)
                            c.Location = Obj.p_set(c.Location - myplanet.Location, Obj.setR(c.Location - myplanet.Location).s - 1) + myplanet.Location;
                    }
                    else
                        c.dep = c.dep + 30;
                if (ctrl == 81)
                    if (c.mode)
                    {
                        if (Obj.setR(c.Location - myplanet.Location).s < 20 + myplanet.range)
                            c.Location = Obj.p_set(c.Location - myplanet.Location, Obj.setR(c.Location - myplanet.Location).s + 1) + myplanet.Location;
                    }
                    else if (c.dep - 30 > 0)
                        c.dep = c.dep - 30;
                myplanet.adds.Clear();
                foreach(CreatureType things in myplanet.Creature)
                    things.auto();
                foreach (CreatureType thing in myplanet.adds)
                    myplanet.Creature.Add(thing);   
            }
            if (c != null)
                pictureBox1.Image = c.look(myplanet, pictureBox1.Width, pictureBox1.Height);
            ctrl = 0;
        }       
    }
}
