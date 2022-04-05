using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public delegate void InvokeDelegate(object[] arr);

        private void Tasker(int val)
        {
            var cnt = 20;
            if (val < 20) cnt = val;
            var list = new Task[cnt];

            var results = new List<decimal>(); 
            var i = 1;
            for (; i <= cnt; i++)
            {
                int buf = i;
                results.Add(1);
                list[i-1]=Task.Factory.StartNew(() => Factor(buf,cnt,results, val));
            }

            i = 0;
            while(i<cnt)
            {
                Task.WaitAny(list);
                i = 0;
                for(int j = 0;j<cnt;j++)
                    if (list[j].IsCompleted)
                        i++;
                string r = (((i + 1) / cnt) * 100).ToString();
                var arr2 = new object[1];
                arr2[0] = r;
                progressPercent.BeginInvoke(new InvokeDelegate(InvokeMethod), arr2);
                //progressBar.Value = ((i + 1) / cnt) * 100;
            }

            decimal? res=1;
            results.ForEach(x=>res*=x);
            var arr = new object[1] ;
            arr[0] = res.ToString() as object;
            label2.BeginInvoke(new InvokeDelegate(InvokeLabel), arr);
        }
        public void InvokeMethod(object[] percent)
        {
            progressPercent.Text = percent[0] as string;
            progressBar.Value = int.Parse(percent[0] as string);
        }
        public void InvokeLabel(object[] percent)
        {
            label2.Text = percent[0] as string;
        }
        private void Factor(int i, int cnt, List<decimal> results, int val)
        {
            for (var j = i; j < val; j += cnt)
                results[i-1] *= j;
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {

            label1.Text = trackBar1.Value.ToString();
            if (trackBar1.Value != 0)
            {
                CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
                if (!first && !token.IsCancellationRequested)
                    cancelTokenSource.Cancel();
                while (mac)
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