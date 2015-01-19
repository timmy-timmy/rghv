using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RGHV {
    public partial class ProxyFinder : Form {
        public ProxyFinder(Viewer viewer) {
            InitializeComponent();
            this.viewer = viewer;
        }
        Viewer viewer;
        const string host = "http://fineproxy.org";


        private void btnRefresh_Click(object sender, EventArgs e) {
            DoRefresh();
        }

        void DoRefresh() {
            listBox1.Items.Clear();
            btnRefresh.Enabled = false;
            try {
                toolStripStatusLabel1.Text = "Getting and testing some proxies from fineproxy.org, please wait...";
                using(var client = new WebClient()) {
                    client.Encoding = Encoding.UTF8;
                    client.Proxy = null;
                    string html = client.DownloadString(host);
                    html = html.Replace(" ", string.Empty);
                    int startInd = html.IndexOf(@"<strong>Быстрыепрокси:");
                    int endInd = html.IndexOf(@"<br/><ahref=""http");
                    if(endInd <= startInd) throw new Exception();
                    html = html.Substring(startInd, endInd - startInd);
                    List<string> ips = html.Split(new string[] { "<br/>" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    ips.RemoveAt(0);
                    ips.RemoveAt(ips.Count - 1);
                    ips = ips.Distinct().ToList();
                    Thread t = new Thread(() => {
                        Parallel.ForEach(ips, ip => {
                            lock(ips) {
                                bool last = ips.IndexOf(ip) == ips.Count - 1;
                                TestIp(ip, last, new Stopwatch());
                            }
                        });
                    });
                    t.Start();
                    if(ips.Count == 0) DoDone();
                }
            }
            catch { 
                toolStripStatusLabel1.Text = "..ooops, something went wrong..";
                btnRefresh.Enabled = true;
            }
        }

        void TestIp(string ip, bool last, Stopwatch sw) {
            ip = ip.Trim();
            try {
                using(var client = new WebClient()) {
                    client.Proxy = new WebProxy(ip);
                    sw.Restart();
                    using(var stream = client.OpenRead("http://rghost.ru")) {
                        if(stream == null) return;
                        long ms = sw.ElapsedMilliseconds;
                        this.Invoke(new Action(() => {
                            this.AddGoodIp(ip, ms);
                        }));
                    }
                }
            }
            catch { }
            if(last) {
                this.Invoke(new Action(() => {
                    this.DoDone();
                }));
            }
            sw = null;
        }
        void AddGoodIp(string ip, long ms) {
            listBox1.Items.Add(new ProxyIpItem(ip, ms));
        }
        void DoDone() {
            btnRefresh.Enabled = true;
            toolStripStatusLabel1.Text = "Done!";
        }
        protected override void OnClosing(CancelEventArgs e) {
            e.Cancel = true;
            Hide();
        }

        void listBox1_MouseDoubleClick(object sender, MouseEventArgs e) {
            var selected = listBox1.SelectedItem;
            if(selected == null || viewer == null) return;
            viewer.SetProxy((listBox1.SelectedItem as ProxyIpItem).Ip);
        }
    }

    class ProxyIpItem {
        public ProxyIpItem(string ip, long ms) {
            Ip = ip;
            Ms = ms;
        }
        public string Ip;
        public long Ms;

        public override string ToString() {
            return Ip + "  ping: " + Ms.ToString()+" ms.";
        }
    }
}
