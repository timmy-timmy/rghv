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
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RGHV {
    public partial class Viewer : Form {
        string[] img_formats = new string[] { ".JPG", ".JPEG", ".PNG", ".BMP", ".GIF", ".TIFF" };
        const string hostMain = "http://rghost.ru/main";
        const string hostRu = "http://rghost.ru";
        const string proxyConfPath = "proxy.conf";
        const string filterConfPath = "filter.conf";
        int filteredCount = 0;
        WebProxy proxy;

        public Viewer() {
            InitializeComponent();
            contentPanel.Dock = DockStyle.Fill;
            flowPanel.Dock = DockStyle.Top;
            flowPanel.Height = 2000;
            LoadProxy();
            LoadFilter();
        }
        bool connected = false;
        bool Connected {
            get { return connected; }
            set {
                if(!value) {
                    statusLabel.Text = "Disconnected";
                    filteredCount = 0;
                }
                else {
                    statusLabel.Text = "Connected";
                }
                btnRefresh.Enabled = true;
                connected = value;
            }
        }
        void UpdateConnectedState() {
            PingHost(hostRu);
        }
        void DoRefresh() {
            if(!Connected) {
                UpdateConnectedState();
                return;
            }
            List<string> urlsToLoad = new List<string>();
            try {
                statusLabel.Text = "Getting images from host...";
                btnRefresh.Enabled = false;
                filteredCount = 0;
                Application.DoEvents();
                using(var client = new WebClient()) {
                    client.Encoding = Encoding.UTF8;
                    string html = client.DownloadString(hostMain);
                    int startInd = html.IndexOf(@"class=""main-column""");
                    int endInd = html.IndexOf(@"<a href=""/files"">Далее");
                    if(endInd <= startInd) return;
                    html = html.Substring(startInd, endInd - startInd);
                    List<string> lis = html.Split(new string[] { "<li>" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    LeaveOnlyImages(lis);
                    DoFiltering(lis);
                    foreach(string li in lis) {
                        List<string> splits = li.Split(new string[] { @"""" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        if(splits.Count > 1) urlsToLoad.Add(splits[1]);
                    }
                }
            }
            catch { Connected = false; }
            finally {
                ClearPics();
                urlsToLoad = urlsToLoad.Distinct().ToList();
                ProceedURLs(urlsToLoad);
            }
        }
        void DoFiltering(List<string> lis) {
            if(!cbFilter.Checked) return;
            List<string> filters = tbFilter.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach(string filter in filters) {
                List<string> badLis = lis.Where(x => x.Contains(filter)).ToList();
                foreach(var badLi in badLis)
                    lis.Remove(badLi);
                filteredCount = badLis.Count;
            }
        }
        void LeaveOnlyImages(List<string> lis) {
            List<string> lisToRemove = new List<string>();
            foreach(string li in lis) {
                string upperli = li.ToUpper();
                string[] splits = upperli.Split(img_formats, StringSplitOptions.RemoveEmptyEntries);
                if(splits.Length <= 1) lisToRemove.Add(li);
            }
            foreach(string liToRemove in lisToRemove) {
                lis.Remove(liToRemove);
            }
        }
        void ClearPics() {
            while(flowPanel.Controls.Count > 0) {
                Control c = flowPanel.Controls[0];
                flowPanel.Controls.Remove(c);
                c.MouseDoubleClick -= new MouseEventHandler(pic_MouseDoubleClick);
                c.Dispose();
                c = null;
            }
        }
        private void ProceedURLs(List<string> urlsToLoad) {
            if(urlsToLoad.Count == 0) { btnRefresh.Enabled = true; return; }
            Thread t = new Thread(() => {
                Parallel.ForEach(urlsToLoad, url => {
                    lock(urlsToLoad) {
                        bool last = urlsToLoad.IndexOf(url) == urlsToLoad.Count - 1;
                        LoadImage(url, last);
                    }
                });
            });
            t.Start();
        }
        void LoadImage(string url, bool last) {
            try {
                using(var client = new WebClient()) {
                    using(var stream = client.OpenRead(hostRu + url + "/thumb.png")) {
                        Image img = Bitmap.FromStream(stream);
                        if(img == null) return;
                        PictureBox pic = new PictureBox() { Size = img.Size, Image = img, Tag = hostRu + url };
                        pic.MouseDoubleClick += new MouseEventHandler(pic_MouseDoubleClick);
                        flowPanel.Invoke(new Action(() => {
                            flowPanel.Controls.Add(pic);
                        }));
                    }
                }
            }
            catch { }
            finally {
                if(last) {
                    toolStrip.Invoke(new Action(() => {
                        btnRefresh.Enabled = true;
                        statusLabel.Text = "Done!";
                        statusLabel.Text += filteredCount > 0 ? " (" + filteredCount + " images were filtered)" : "";
                    }));
                }
            }
        }
        void pic_MouseDoubleClick(object sender, MouseEventArgs e) {
            Control c = sender as Control;
            string url = (string) c.Tag;
            Process.Start(url);
        }
        void PingHost(string nameOrAddress) {
            statusLabel.Text = "Ping host...";
            Thread t = new Thread(() => { RequestPing(nameOrAddress); });
            t.Start();
        }
        void RequestPing(string nameOrAddress) {
            try {
                WebRequest request = WebRequest.Create(nameOrAddress);
                using(var response = request.GetResponse()) {
                    this.Invoke(new MethodInvoker(SetConnectedTrue));
                }
            }
            catch {
                this.Invoke(new MethodInvoker(SetConnectedFalse));
            }
        }
        void SetConnectedFalse() { Connected = false; }
        void SetConnectedTrue() { Connected = true; }
        void toolStripButton1_Click(object sender, EventArgs e) {
            DoRefresh();
        }
        void checkBox1_CheckedChanged(object sender, EventArgs e) {
            this.Connected = false;
            if(cbProxy.Checked)
                WebRequest.DefaultWebProxy = proxy;
            else
                WebRequest.DefaultWebProxy = null;
        }
        void textBox1_TextChanged(object sender, EventArgs e) {
            try {
                string str = tbProxy.Text.Trim();
                proxy = new WebProxy(@"http://" + str);
                if(cbProxy.Checked) {
                    WebRequest.DefaultWebProxy = proxy;
                    this.Connected = false;
                }
                SaveProxy();
            }
            catch { }
        }
        void SaveProxy() {
            SaveCore(tbProxy, proxyConfPath);
        }
        void LoadProxy() {
            LoadCore(tbProxy, proxyConfPath);
        }
        void SaveFilter() {
            SaveCore(tbFilter, filterConfPath);
        }
        void LoadFilter() {
            LoadCore(tbFilter, filterConfPath);
        }
        void SaveCore(TextBox tb, string filepath) {
            string str = tb.Text.Trim();
            try { File.WriteAllText(filepath, str); }
            catch { }
        }
        void LoadCore(TextBox tb, string filepath) {
            try { if(File.Exists(filepath)) tb.Text = File.ReadAllText(filepath); }
            catch { }
        }
        private void tbFilter_TextChanged(object sender, EventArgs e) {
            SaveFilter();
        }

        ProxyFinder proxyFinder;
        ProxyFinder ProxyFinder {
            get {
                if(proxyFinder == null || proxyFinder.IsDisposed)
                    proxyFinder = new ProxyFinder(this);
                return proxyFinder;
            }
        }
        private void btnShowFinder_Click(object sender, EventArgs e) {
            ProxyFinder.Show();
            ProxyFinder.BringToFront();
        }
        public void SetProxy(string ip) {
            this.tbProxy.Text = ip;
        }
        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
            if(proxyFinder != null && !proxyFinder.IsDisposed) {
                proxyFinder.StopAndClose();
                proxyFinder.Dispose();
                proxyFinder = null;
            }
        }
    }
}
