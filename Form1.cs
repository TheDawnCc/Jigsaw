using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication1
{
   
    struct Node
    {
        public int[] a;
        public int value;
        public int bk;
        public int step;
        public object _next;
        public Node pre
        {
            get
            {
                return (Node)_next;
            }
            set
            {
                _next = value;
            }
        }

    }
    

    public partial class Form1 : Form
    {
        Button[] bt;
        bool check;
        int[] rt;
        int Black;
        int[,] map = new int[4, 4];
        int[,] mark = new int[4, 4];
        int[,] step = new int[4, 4];
        Image[] pic = new Image[16];
        int time,Tips;
        PriorityQueue<Node> ste = new PriorityQueue<Node>(new X());
        
        
       
       
        

        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            bt = new Button[18];
            rt = new int[16];
            bt[0] = button1;
            bt[1] = button2;
            bt[2] = button3;
            bt[3] = button4;
            bt[4] = button5;
            bt[5] = button6;
            bt[6] = button7;
            bt[7] = button8;
            bt[8] = button9;
            bt[9] = button10;
            bt[10] = button11;
            bt[11] = button12;
            bt[12] = button13;
            bt[13] = button14;
            bt[14] = button15;
            bt[15] = button16;
            bt[15].Text = "";
            for (int i = 0; i <= 15; i++)
            {
                rt[i] = i;
            }
            Black = 15;
            comboBox1.Text = "图片1";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sender == bt[Black])
            {
                for (int i = 0; i < 15; i++)
                {
                    bt[i].BackgroundImage = pic[i + 1];
                    bt[i].Enabled = false;
                }
                button17.Enabled = false;
                button18.Enabled = false;
                bt[15].BackgroundImage = null;
                Tips = 0;

                timer3.Start();
            }
            else
            {
                for (int i = 0; i < 16; i++)
                {
                    if (sender == bt[i])
                    {
                        int k = i % 4;
                        if (k + 1 < 4)
                        {
                            if (bt[i + 1].BackgroundImage == null)
                            {
                                rt[i + 1] = rt[i];
                                rt[i] = 15;
                                bt[i + 1].BackgroundImage = bt[i].BackgroundImage;
                                bt[i].BackgroundImage = null;
                                Black = i;
                            }
                        }
                        if (k - 1 >= 0)
                        {
                            if (bt[i - 1].BackgroundImage == null)
                            {
                                rt[i - 1] = rt[i];
                                rt[i] = 15;
                                bt[i - 1].BackgroundImage = bt[i].BackgroundImage;
                                bt[i].BackgroundImage = null;
                                Black = i;
                            }
                        }
                        if (i - 4 >= 0)
                        {
                            if (bt[i - 4].BackgroundImage == null)
                            {
                                rt[i - 4] = rt[i];
                                rt[i] = 15;
                                bt[i - 4].BackgroundImage = bt[i].BackgroundImage;
                                bt[i].BackgroundImage = null;
                                Black = i;
                            }
                        }
                        if (i + 4 < 16)
                        {
                            if (bt[i + 4].BackgroundImage == null)
                            {
                                rt[i + 4] = rt[i];
                                rt[i] = 15;
                                bt[i + 4].BackgroundImage = bt[i].BackgroundImage;
                                bt[i].BackgroundImage = null;
                                Black = i;
                            }
                        }
                    }
                }
            }
            if (check == true)
            {
                int Sum = 0;
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (rt[i * 4 + j] == 15) continue;
                        int x, y;
                        x = rt[i * 4 + j] / 4;
                        y = rt[i * 4 + j] % 4;
                        step[i, j] = Math.Abs(x - i) + Math.Abs(y - j);
                        Sum += step[i, j];
                    }
                }
                if (Sum == 0)
                {
                    timer2.Stop();
                    timer1.Stop();
                    MessageBox.Show("Hava Fun,Your Time is " + time);
                    check = false;
                }
            }
        }

        
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Random rm = new Random();
            int n = rm.Next(20, 50 + 1);


            for (int i = 0; i < n; i++)
            {
                int m = rm.Next(8);

                for (int j = 0; j < m; j++)
                {
                    int turn = rm.Next(4);
                    if (turn == 0)
                    {
                        if (Black - 4 >= 0)
                        {
                            rt[Black] = rt[Black - 4];
                            rt[Black - 4] = 15;
                            bt[Black].BackgroundImage = bt[Black - 4].BackgroundImage;
                            bt[Black - 4].BackgroundImage = null;
                            Black -= 4;
                        }
                    }
                    else if (turn == 1)
                    {
                        if (Black + 4 < 16)
                        {
                            rt[Black] = rt[Black + 4];
                            rt[Black + 4] =15;
                            bt[Black].BackgroundImage = bt[Black + 4].BackgroundImage;
                            bt[Black + 4].BackgroundImage = null;
                            Black += 4;
                        }
                    }
                    else if (turn == 2)
                    {
                        if ((Black % 4) - 1 >= 0)
                        {
                            rt[Black] = rt[Black - 1];
                            rt[Black - 1] =15;
                            bt[Black].BackgroundImage = bt[Black - 1].BackgroundImage;
                            bt[Black - 1].BackgroundImage = null;
                            Black--;
                        }
                    }
                    else if (turn == 3)
                    {
                        if ((Black % 4) + 1 < 4)
                        {
                            rt[Black] = rt[Black + 1];
                            rt[Black + 1] =15;
                            bt[Black].BackgroundImage = bt[Black + 1].BackgroundImage;
                            bt[Black + 1].BackgroundImage = null;
                            Black++;
                        }
                    }
                    Thread.Sleep(20);
                }
            }
            button17.Enabled = true;
            button18.Enabled = true;
            this.Invoke(new MethodInvoker(() => { timer1.Start(); }));
         //   timer1.Start();
        }


        private void button17_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
            button17.Enabled = false;
            button18.Enabled = false;
            time = 0;
            check = true;
        }
       
        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            for(int i=1;i<=5;i++)
            {
                if(comboBox1.Text=="图片"+i.ToString())
                {
                    for(int j=1;j<16;j++)
                    {
                        string path = Application.StartupPath+"\\pic\\" + i + "\\" + j.ToString() + ".jpg";
                        pic[j] = new Bitmap(path);
                    }
                }
            }
            for(int i=0;i<16;i++)
            {
                if (rt[i] ==15)
                {
                    bt[i].BackgroundImage = null;
                    continue;
                }
                bt[i].BackgroundImage = pic[rt[i]+1];
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time++;
            label1.Text = "Time:"+time.ToString();
        }
        private void button18_Click(object sender, EventArgs e)
        {
            
            Solve();
        }
        string trans(int[] a)
        {
            string s="";
            for(int i=0;i<a.Length;i++)
            {
                if (i < 10)
                {
                    s += ("0" + a[i].ToString());
                }
                else
                    s += a[i].ToString();
            }
            return s;
        }
        Stack<int> turns = new Stack<int>();
        Queue<string> clos = new Queue<string>();
        void Solve()
        {
            
            int Sum = 0,bk=0;
            Array.Clear(step, 0, step.Length);
            clos.Clear();
            turns.Clear();
            while(ste.Count>0)
            {
                ste.Pop();
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (rt[i * 4 + j] ==15)
                    {
                        bk = i * 4 + j;
                        continue;
                    }
                    int x, y;
                    x = rt[i * 4 + j] / 4;
                    y = rt[i * 4 + j] % 4;
                    step[i, j] = Math.Abs(x - i) + Math.Abs(y - j);
                    Sum += step[i, j];
                }
            }
            if (Sum == 0)
                return;

            button17.Enabled = false;
            button18.Enabled = false;
            
            Node node;
            
            node.a = rt; node.value = Sum; node.bk = bk; node._next = null; node.step = 0;
            ste.Push(node);
            while(ste.Count>0)
            {
             
                node = ste.Top();
                ste.Pop();
                //for (int i = 0; i < 16;i++)
                //{
                //    Console.Write(node.a[i]);
                //    if (i % 4 == 3)
                //        Console.WriteLine();
                //}
                if (node.value == 0)
                    break;

                clos.Enqueue(trans(node.a));
                int bkk = node.bk;
                int stp = node.step+1;

                //if (stp > 20)
                //    continue;

                int[] a = new int[16]; Array.Copy(node.a, a, node.a.Length);
                int[] b = new int[16]; Array.Copy(node.a, b, node.a.Length);
                int[] c = new int[16]; Array.Copy(node.a, c, node.a.Length);
                int[] d = new int[16]; Array.Copy(node.a, d, node.a.Length);
                bk = bkk;
                if ((bk % 4) + 1 < 4)
                {

                    a[bk] = a[bk + 1];
                    a[bk + 1] = 15;
                    bk++;
                    Sum = 0;
                    Array.Clear(step, 0, step.Length);
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (a[i * 4 + j] == 15)
                            {
                                continue;
                            }
                            int x, y;
                            x = a[i * 4 + j] / 4;
                            y = a[i * 4 + j] % 4;
                            step[i, j] = Math.Abs(x - i) + Math.Abs(y - j);
                            Sum += step[i, j];
                        }
                    }
                    if (!clos.Contains(trans(a)))
                    {
                        Node aa = new Node();
                        aa.pre = node;
                        aa.a = a;
                        aa.bk = bk;
                        aa.step = stp;
                        aa.value = Sum;
                        ste.Push(aa);
                    }
                }
                bk = bkk;
                if ((bk % 4) - 1 >= 0)
                {
                    b[bk] = b[bk - 1];
                    b[bk - 1] = 15;
                    bk--;
                    Sum = 0;
                    Array.Clear(step, 0, step.Length);
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (b[i * 4 + j] == 15)
                            {
                                continue;
                            }
                            int x, y;
                            x = b[i * 4 + j] / 4;
                            y = b[i * 4 + j] % 4;
                            step[i, j] = Math.Abs(x - i) + Math.Abs(y - j);
                            Sum += step[i, j];
                        }
                    }
                    if (!clos.Contains(trans(b)))
                    {
                        Node bb = new Node();
                        bb.pre = node;
                        bb.a = b;
                        bb.bk = bk;
                        bb.step = stp;
                        bb.value = Sum;
                        ste.Push(bb);
                    }
                }
                bk = bkk;
                if (bk + 4 < 16)
                {
                    c[bk] = c[bk + 4];
                    c[bk + 4] = 15;
                    bk += 4;
                    Sum = 0;
                    Array.Clear(step, 0, step.Length);
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (c[i * 4 + j] == 15)
                            {
                                continue;
                            }
                            int x, y;
                            x = c[i * 4 + j] / 4;
                            y = c[i * 4 + j] % 4;
                            step[i, j] = Math.Abs(x - i) + Math.Abs(y - j);
                            Sum += step[i, j];
                        }
                    }
                    if (!clos.Contains(trans(c)))
                    {
                        Node cc = new Node();
                        cc.pre = node;
                        cc.a = c;
                        cc.bk = bk;
                        cc.step = stp;
                        cc.value = Sum;
                        ste.Push(cc);
                    }
                }
                bk = bkk;
                if (bk - 4 >= 0)
                {
                    d[bk] = d[bk - 4];
                    d[bk - 4] = 15;
                    bk -= 4;
                    Sum = 0;
                    Array.Clear(step, 0, step.Length);
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (d[i * 4 + j] == 15)
                            {
                                continue;
                            }
                            int x, y;
                            x = d[i * 4 + j] / 4;
                            y = d[i * 4 + j] % 4;
                            step[i, j] = Math.Abs(x - i) + Math.Abs(y - j);
                            Sum += step[i, j];
                        }
                    }
                    if (!clos.Contains(trans(d)))
                    {
                        Node dd = new Node();
                        dd.pre = node;
                        dd.a = d;
                        dd.bk = bk;
                        dd.step = stp;
                        dd.value = Sum;
                        ste.Push(dd);
                    }                    
                }

            }
           
         //   int Step=node.value;
            while(node._next!=null)
            {
                turns.Push(node.bk);
                node = node.pre;
            }
            timer2.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            
            timer1.Stop();
            if(turns.Count > 0)
            {
                bt[turns.Pop()].PerformClick();
            }
            if (turns.Count == 0)
            {
                timer2.Stop();
                button17.Enabled = true;
                button18.Enabled = true;
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if(Tips==3)
            {
                timer3.Stop();
                for(int i=0;i<16;i++)
                {
                    if (rt[i] == 15)
                    {
                        bt[i].BackgroundImage = null;
                    }
                    else
                        bt[i].BackgroundImage = pic[rt[i] + 1];
                    bt[i].Enabled = true;
                }
                button17.Enabled = true;
                button18.Enabled = true;
            }
            Tips++;
        }

        

    }
    public class PriorityQueue<T>           //优先队列
    {
        IComparer<T> comparer;
        T[] heap;

        public int Count { get; private set; }

        public PriorityQueue() : this(null) { }
        public PriorityQueue(int capacity) : this(capacity, null) { }
        public PriorityQueue(IComparer<T> comparer) : this(16, comparer) { }

        public PriorityQueue(int capacity, IComparer<T> comparer)
        {
            this.comparer = (comparer == null) ? Comparer<T>.Default : comparer;
            this.heap = new T[capacity];
        }

        public void Push(T v)
        {
            if (Count >= heap.Length) Array.Resize(ref heap, Count * 2);
            heap[Count] = v;
            SiftUp(Count++);
        }

        public T Pop()
        {
            var v = Top();
            heap[0] = heap[--Count];
            if (Count > 0) SiftDown(0);
            return v;
        }

        public T Top()
        {
            if (Count > 0) return heap[0];
            throw new InvalidOperationException("优先队列为空");
        }

        void SiftUp(int n)
        {
            var v = heap[n];
            for (var n2 = n / 2; n > 0 && comparer.Compare(v, heap[n2]) > 0; n = n2, n2 /= 2) heap[n] = heap[n2];
            heap[n] = v;
        }

        void SiftDown(int n)
        {
            var v = heap[n];
            for (var n2 = n * 2; n2 < Count; n = n2, n2 *= 2)
            {
                if (n2 + 1 < Count && comparer.Compare(heap[n2 + 1], heap[n2]) > 0) n2++;
                if (comparer.Compare(v, heap[n2]) >= 0) break;
                heap[n] = heap[n2];
            }
            heap[n] = v;
        }
    }


    class X : Comparer<Node>
    {
        public override int Compare(Node x, Node y)
        {
            return (y.step + (y.value<<2)).CompareTo((x.value<<2) + x.step);
            throw new NotImplementedException();
        }
    }
}
