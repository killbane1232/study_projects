using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace OSLaba31
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool first = true;
        private CancellationToken token = new CancellationToken();
        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        public delegate void InvokeDelegate(string arr);
        const int threadCounter = 20;

        private void Tasker(int val)
        {
            var cnt = threadCounter;
            if (val < threadCounter) cnt = val;
            var list = new Task[cnt];
            
            var results = new List<BigInteger>(); 
            var i = 1;
            for (; i <= cnt; i++)
            {
                int buf = i;
                results.Add(1);
                list[i-1]=Task.Factory.StartNew(() => Factor(buf,cnt,results, val));
            }

            i = 0;
            var decima = decimal.MaxValue;
            while(i<cnt)
            {
                Task.WaitAny(list);
                i = 0;
                for(int j = 0;j<cnt;j++)
                    if (list[j].IsCompleted)
                        i++;
                string r = (((i + 1) / (cnt+1)) * 100).ToString();
                var arr2 = new object[1];
                arr2[0] = r;
                progressPercent.BeginInvoke(new InvokeDelegate(InvokeMethod), arr2);
                //progressBar.Value = ((i + 1) / cnt) * 100;
            }

            BigInteger res=1;
            results.ForEach(x=>res*=x);
            var arr = new object[1] ;
            arr[0] = res.ToString();
            label2.BeginInvoke(new InvokeDelegate(InvokeLabel), arr);
        }
        public class shell
        {
            public int a;
            public string b;
        }
        public void InvokeMethod(string percent)
        {
            progressPercent.Text = percent;
            progressBar.Value = int.Parse(percent);
        }
        public void InvokeLabel(string percent)
        {
            label2.Text = percent;
        }
        private void Factor(int i, int cnt, List<BigInteger> results, int val)
        {
            var a = i;
            for (var j = a; j <= val; j += cnt)
                results[a-1] *= j;
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {

            label1.Text = trackBar1.Value.ToString();
            if (trackBar1.Value != 0)
            {
                CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
                if (!first && !token.IsCancellationRequested)
                    cancelTokenSource.Cancel();
                while (false)
                    Thread.Sleep(100);
                cancelTokenSource = new CancellationTokenSource();
                token = cancelTokenSource.Token;
                var val = trackBar1.Value;
                Task.Factory.StartNew(() => Tasker(val), token);
            }
            else
            {
                progressPercent.Text = "100";
                progressBar.Value = 100;
                label2.Text = "1";
            }
        }
    }
}