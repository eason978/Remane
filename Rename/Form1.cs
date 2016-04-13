using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net;

namespace Rename
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            label_file_name.Text = UpdateNewName(textBox_file_exp.Text);
            label_folder_name.Text = UpdateNewName(textBox_folder_exp.Text);
            stWebMname = new string[] { "", "", "", "", "" };
            stWebEname = new string[] { "", "", "", "", "" };
            stWebYear = new string[] { "", "", "", "", "" };

        }

        private string stCurrentRootFolder = "";
        private string stCurrentSelectedFolder = "";
        private string stWebResult = "";
        private int intAllNumRlt = 5;
        private string[] stWebMname;
        private string[] stWebEname;
        private string[] stWebYear;

        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories())
                directoryNode.Nodes.Add(CreateDirectoryNode(directory));
            foreach (var file in directoryInfo.GetFiles())
            {
                if (file.Extension == ".db") continue;
                directoryNode.Nodes.Add(new TreeNode(file.Name));
            }
            return directoryNode;
        }

        private void CreateFolderTreeViewNode(string strFolder, TreeView tv)
        {
            try
            {
                DirectoryInfo directory = new DirectoryInfo(strFolder);
                DirectoryInfo[] directories = directory.GetDirectories();
                tv.Nodes.Clear();
                foreach (DirectoryInfo folder in directories)
                    tv.Nodes.Add(CreateDirectoryNode(folder));
            }
            catch { }
        }

        private string UpdateNewName(string User_exp)
        {
            string stNewName;
            stNewName = Regex.Replace(User_exp, "(%M)", textBox_Mname.Text);
            stNewName = Regex.Replace(stNewName, "(%E)", textBox_Ename.Text);
            stNewName = Regex.Replace(stNewName, "(%Y)", textBox_Year.Text);
            return stNewName;
        }

        private async Task GetInfoFromWeb(string strKeyWord)
        {
            var client = new HttpClient();
            // Create the HttpContent for the form to be posted.
            var requestContent = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("qmv", strKeyWord),
                });

            // Get the response.
            HttpResponseMessage response = await client.PostAsync(
                "http://tw.movies.yahoo.com/moviesearch_result.html?",
                requestContent);
/*
            // Create the HttpContent for the form to be posted.
            var requestContent = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("action", "home"),
                    new KeyValuePair<string, string>("fr", "atmovies-homepage"),
                    new KeyValuePair<string, string>("enc", "UTF-8"),
                    new KeyValuePair<string, string>("type", "all"),
                    new KeyValuePair<string, string>("search_term", strKeyWord),
                });

            // Get the response.
            HttpResponseMessage response = await client.PostAsync(
                "http://search.atmovies.com.tw/search/search.cfm",
                requestContent);
*/
            // Get the response content.
            HttpContent responseContent = response.Content;

            // Get the stream of the content.
            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                // Write the output.
                //Console.WriteLine(await reader.ReadToEndAsync());
                stWebResult += await reader.ReadToEndAsync() + Environment.NewLine;
            }
        }

        public HtmlDocument GetHtmlDocument(string html)
        {
            WebBrowser browser = new WebBrowser();
            browser.ScriptErrorsSuppressed = true; //not necessesory you can remove it
            browser.DocumentText = html;
            browser.Document.OpenNew(true);
            browser.Document.Write(html);
            browser.Refresh();
            return browser.Document;
        }

        private string getNewFileName(FileInfo file, string newname)
        {
            string strSubtitleEx = ".srt .SRT .ass .ASS .sub .SUB .stl .STL .ssa .SSA";
            string strSubtitleLan = ".cht .CHT .Cht .chs .CHS .Chs .eng .ENG .Eng";
            if (strSubtitleEx.Contains(file.Extension))
            {
                Regex regex = new Regex(@"(\.\w+\.\w+)$");
                Match match = regex.Match(file.FullName);
                if (match.Groups.Count > 0)
                {
                    Regex regex_lan = new Regex(@"(\.\w+)\.\w+$");
                    Match match_lan = regex_lan.Match(file.FullName);
                    if (strSubtitleLan.Contains(match_lan.Groups[1].Value.ToString()))
                    {
                        return (newname + match.Groups[1].Value.ToString());
                    }
                }
            }
            return (newname + file.Extension);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(true == Directory.Exists(textBox_selected_folder.Text))
            {
                fbd.SelectedPath = textBox_selected_folder.Text;
            }
            
            DialogResult result = fbd.ShowDialog();

            if ("" == fbd.SelectedPath) return;

            //System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
            textBox_selected_folder.Text = fbd.SelectedPath;

            //string[] files = Directory.GetFiles(fbd.SelectedPath);
            //foreach (string files_str in files)
            //    listBox1.Items.Add(files_str);

            //DirectoryInfo directory = new DirectoryInfo(fbd.SelectedPath);
            //DirectoryInfo[] directories = directory.GetDirectories();
            //foreach (DirectoryInfo folder in directories)
            //    listBox1.Items.Add(folder.Name);

            stCurrentRootFolder = fbd.SelectedPath;
            CreateFolderTreeViewNode(stCurrentRootFolder, treeView_selected_list);
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            DirectoryInfo directory = new DirectoryInfo(stCurrentRootFolder + @"\" + e.Node.FullPath);

            try
            {
                directory.GetFiles();
            }
            catch (Exception)
            {
                //throw;
                return;
            }

            stCurrentSelectedFolder = stCurrentRootFolder + @"\" + e.Node.FullPath;
            textBox_selected_folder.Text = stCurrentSelectedFolder;

            listBox1.Items.Clear();
            //listBox2.Items.Clear();
            foreach (var file in directory.GetFiles())
            {
                if (file.Extension == ".db") continue;
                //var a = getNewFileName(file, label_file_name.Text);
                //listBox1.Items.Add(directory.Name + @"\" + file.Name +
                //    @"    ->    " + label_folder_name.Text + @"\" + label_file_name.Text + file.Extension);
                listBox1.Items.Add(directory.Name + @"\" + file.Name +
                    @"    ->    " + label_folder_name.Text + @"\" + getNewFileName(file, label_file_name.Text));
            }

            textBox_keyword.Text = e.Node.FullPath;
        }

        private void textBox_MatchPattern_TextChanged(object sender, EventArgs e)
        {
            label_file_name.Text = UpdateNewName(textBox_file_exp.Text);
            label_folder_name.Text = UpdateNewName(textBox_folder_exp.Text);

            if (stCurrentSelectedFolder == "") return;

            DirectoryInfo directory = new DirectoryInfo(stCurrentSelectedFolder);

            try
            {
                directory.GetFiles();
            }
            catch (Exception)
            {

                return;
            }

            listBox1.Items.Clear();
            foreach (var file in directory.GetFiles())
            {
                if (file.Extension == ".db") continue;
                listBox1.Items.Add(directory.Name + @"\" + file.Name +
                    @"    ->    " + label_folder_name.Text + @"\" + getNewFileName(file, label_file_name.Text));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            DirectoryInfo directory = new DirectoryInfo(stCurrentSelectedFolder);
            foreach (var file in directory.GetFiles())
            {
                if (file.Extension == ".db") continue;
                //Backup original name
                if (file.Length > 10 * 1024 * 1024)
                {
                    string bkfilename = file.Directory + @"\" + Path.GetFileNameWithoutExtension(file.FullName);
                    bool bExist = File.Exists(bkfilename);
                    if (false == File.Exists(bkfilename))
                    {
                        using (FileStream fs = File.Create(bkfilename))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes(file.Name);
                            // Add some information to the file.
                            fs.Write(info, 0, info.Length);
                            fs.Close();
                        }
                    }
                }
                file.MoveTo(file.Directory + @"\" + getNewFileName(file, label_file_name.Text));
            }

            directory.MoveTo(directory.Parent.FullName + @"\" +label_folder_name.Text);

            CreateFolderTreeViewNode(stCurrentRootFolder, treeView_selected_list);
            listBox1.Items.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Connection: keep-alive
            //Content-Length: 76
            //Cache-Control: max-age=0
            //Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8
            //Origin: http://www.atmovies.com.tw
            //Upgrade-Insecure-Requests: 1
            //User-Agent: Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.87 Safari/537.36
            //Content-Type: application/x-www-form-urlencoded
            //Referer: http://www.atmovies.com.tw/home/
            //Accept-Encoding: gzip, deflate
            //Accept-Language: zh-TW,zh;q=0.8,en-US;q=0.6,en;q=0.4
            //<input name="type" type="radio" value="F"/>影片
            //<input name="type" type="radio" value="S" />人物
            //<input name="type" type="radio" value="D" />DVD
            //<input name="type" type="radio" value="C" />原聲帶

            //string param = "action=home&fr=atmovies-homepage&enc=utf-8&type=F&search_term=" + textBox_keyword.Text;
            //string param = "action=home&fr=fr=atmovies-cfm&enc=utf-8&type=F&search_term=" + textBox_keyword.Text;
            //string param = textBox_keyword.Text;
            //string param = "action=home&fr=search&type=F&search=提交&search_term=" + textBox_keyword.Text;
            button3.Enabled = false;
            byte[] key = Encoding.UTF8.GetBytes(textBox_keyword.Text);
            string a = "";
            foreach (byte c in key)
            {
                a += ("%"+c.ToString("X"));
            }
            string param = "type=F&search=%E6%8F%90%E4%BA%A4&action=home&fr=search&search_term=" + a;
            byte[] bs = Encoding.ASCII.GetBytes(param);
            listBox2.Items.Clear();
            HttpWebRequest req = (HttpWebRequest) HttpWebRequest.Create( "http://search.atmovies.com.tw/search/search.cfm" );
            //Get the headers associated with the request.
            WebHeaderCollection reqHeaderCollection = req.Headers;
            req.Method = "POST";
            //req.Connection = "keep-alive";
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8;charset=UTF-8";
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.87 Safari/537.36";
            req.ContentType = "application/x-www-form-urlencoded";
            req.Referer = "http://www.atmovies.com.tw/home/";
            req.ContentLength = bs.Length;
            reqHeaderCollection.Add("Accept-Encoding: gzip, deflate");
            reqHeaderCollection.Add("Accept-Language: zh-TW,zh;q=0.8,en-US;q=0.6,en;q=0.4");
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
            }
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            using (WebResponse wr = req.GetResponse())
            {
                //在這裡對接收到的頁面內容進行處理
                using (StreamReader reader = new StreamReader(wr.GetResponseStream(), Encoding.UTF8))
                {
                    doc.Load(reader.BaseStream, Encoding.UTF8);
                }
            }
            // 裝載第一層查詢結果 
            HtmlAgilityPack.HtmlDocument docMovieContext = new HtmlAgilityPack.HtmlDocument();
            docMovieContext.LoadHtml(doc.DocumentNode.SelectSingleNode(@"//*[@id='main']").InnerHtml);
            string strResult = docMovieContext.DocumentNode.SelectSingleNode("./table[1]/tr[1]/td[2]").InnerText.Trim();

            var regex = new Regex(@"[^0-9]\s+(\d+)\s+[^0-9]");
            var match = regex.Match(strResult);
            int intNumRlt = 0;
            if(match.Groups.Count > 0)
            {
                Int32.TryParse(match.Groups[1].Value.ToString(), out intNumRlt);
            }

            if (intNumRlt > 0)
            {
                ////*[@id="main"]/blockquote[1]/ol/li[1]/font[2]
                HtmlAgilityPack.HtmlNodeCollection name_nc = docMovieContext.DocumentNode.SelectNodes(".//a[@class='at11']");
                HtmlAgilityPack.HtmlNodeCollection year_nc = docMovieContext.DocumentNode.SelectNodes(".//font[@color='#606060']");

                Array.Clear(stWebMname, 0, intAllNumRlt);
                Array.Clear(stWebEname, 0, intAllNumRlt);
                Array.Clear(stWebYear, 0, intAllNumRlt);
                int i = 0;
                foreach (var node in name_nc)
                {
                    stWebMname[i] = name_nc[i].InnerText.Trim();
                    stWebYear[i] = (year_nc.Count > i) ? year_nc[i].InnerText.Trim() : "";
                    listBox2.Items.Add(stWebMname[i].ToString().Trim() + " " + stWebYear[i].ToString().Trim());
                    i++;
                    if (i >= intAllNumRlt) break; //stWebMname = 5
                }
            }
            else
            {
                MessageBox.Show("Can not find with " + textBox_keyword.Text);
            }
            button3.Enabled = true;
        }

        private void button3_Click_Yahoo(object sender, EventArgs e)
        {
            //await GetInfoFromWeb(textBox_keyword.Text);

            /*
             *          /html[1]/body[1]/div[1]/div[2]/div[1]/div[1]/div[1]/@id[1]
             * ymvsrc : 有結果
             * ymvschn : 沒有
             * 
             * XPATH : /html[1]/body[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/strong[1]
             * Content : 很抱歉，無法找到符合條件的結果
             * 
             * XPATH : /html[1]/body[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]
             * Content : <div class="statistic">您的搜尋結果：共 <em>3</em> 筆資訊，符合 <em>pi</em></div> 
             * 
             * XPATH : /html[1]/body[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/em[1]
             * Content : 3
             * 
             * XPATH : /html[1]/body[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[1]/div[2]/div[1]/div[3]/div[1]
             * Content :  <a href="https://tw.rd.yahoo.com/referurl/movie/search/movieinfo/*https://tw.movies.yahoo.com/movieinfo_main.html/id=4376"><img src="https://s.yimg.com/vu/movies/fp/mpost4/43/76/4376.jpg" title="少年 Pi 的奇幻漂流"></a> 
             *         /html[1]/body[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[1]/div[3]/div[1]/div[3]/div[1]
             *          <a href="https://tw.rd.yahoo.com/referurl/movie/search/movieinfo/*https://tw.movies.yahoo.com/movieinfo_main.html/id=4551"><img src="https://s.yimg.com/vu/movies/fp/mpost4/45/51/4551.jpg" title="阿嬤的夢中情人"></a> 
             *          
             * XPATH : /html[1]/body[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[1]/div[2]/div[1]/div[3]/div[1]/a[1]/img[1]/@title[1]
             * Content : 少年 Pi 的奇幻漂流
             *         /html[1]/body[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[1]/div[3]/div[1]/div[3]/div[1]/a[1]/img[1]/@title[1]
             *         阿嬤的夢中情人
             * 
             * XPATH : /html[1]/body[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[1]/div[2]/div[1]/div[3]/div[2]/h4[1]/a[1]
             * Content : 少年<hi>Pi</hi>的奇幻漂流
             *         /html[1]/body[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[1]/div[3]/div[1]/div[3]/div[2]/h4[1]/a[1]
             *         /html[1]/body[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[1]/div[3]/div[1]/div[3]/div[2]/h4[1]/a[1]
             *         /html[1]/body[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[1]/div[3]/div[1]/div[3]/div[2]/h4[1]
             *         阿嬤的夢中情人
             * 
             * XPATH : /html[1]/body[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[1]/div[2]/div[1]/div[3]/div[2]/h5[1]/a[1]
             * Content : Life of <hi>Pi</hi>
             *         /html[1]/body[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[1]/div[3]/div[1]/div[3]/div[2]/h5[1]/a[1]
             *         Forever Love
             * 
             * XPATH : /html[1]/body[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[1]/div[2]/div[1]/div[3]/div[2]/span[1]
             * Content : 上映日期：<span>2012-11-21</span>
             * 
             * XPATH : /html[1]/body[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[1]/div[2]/div[1]/div[3]/div[2]/span[1]/span[1]
             * Content : 2012-11-21
             *         /html[1]/body[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[1]/div[3]/div[1]/div[3]/div[2]/span[1]/span[1]
             *          2013-02-27
            */
            string strhtml = "http://tw.movies.yahoo.com/moviesearch_result.html?qmv=";
            // 下載 Yahoo 奇摩電影
            WebClient client = new WebClient();
            MemoryStream ms = new MemoryStream(client.DownloadData(strhtml + textBox_keyword.Text));

            // 使用預設編碼讀入 HTML
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            //doc.Load(ms, Encoding.Default);
            doc.Load(ms, Encoding.UTF8);

            // 裝載第一層查詢結果 
            HtmlAgilityPack.HtmlDocument docMovieContext = new HtmlAgilityPack.HtmlDocument();
            docMovieContext.LoadHtml(doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[2]/div[1]/div[1]").InnerHtml);
            if (null == docMovieContext.DocumentNode.SelectNodes(@"//div[@id='ymvsrc']"))
            {
                MessageBox.Show("Can not find with " + textBox_keyword.Text);
            }
            else {
                Array.Clear(stWebMname, 0, intAllNumRlt);
                Array.Clear(stWebEname, 0, intAllNumRlt);
                Array.Clear(stWebYear, 0, intAllNumRlt);
                string[] XPATH_Mname = {"./div[1]/div[2]/div[1]/div[2]/div[1]/div[3]/div[2]/h4[1]/a[1]",
                                        "./div[1]/div[2]/div[1]/div[3]/div[1]/div[3]/div[2]/h4[1]/a[1]"};
                string[] XPATH_Ename = {"./div[1]/div[2]/div[1]/div[2]/div[1]/div[3]/div[2]/h5[1]/a[1]",
                                        "./div[1]/div[2]/div[1]/div[3]/div[1]/div[3]/div[2]/h5[1]/a[1]"};
                string[] XPATH_Year = { "./div[1]/div[2]/div[1]/div[2]/div[1]/div[3]/div[2]/span[1]/span[1]",
                                        "./div[1]/div[2]/div[1]/div[3]/div[1]/div[3]/div[2]/span[1]/span[1]"};
                int intNumOfResult = 0;

                listBox2.Items.Clear();
                Int32.TryParse(docMovieContext.DocumentNode.SelectSingleNode("/div[1]/div[1]/div[1]/em[1]").InnerText.Trim(), out intNumOfResult);
                stWebMname[0] = docMovieContext.DocumentNode.SelectSingleNode(XPATH_Mname[0]).InnerText.Trim();
                stWebEname[0] = docMovieContext.DocumentNode.SelectSingleNode(XPATH_Ename[0]).InnerText.Trim();
                stWebYear[0] = docMovieContext.DocumentNode.SelectSingleNode(XPATH_Year[0]).InnerText.Trim();
                listBox2.Items.Add(stWebMname[0] + "  " + stWebEname[0] + "  " + stWebYear[0]);
                if (intNumOfResult > 1)
                {
                    stWebMname[1] = docMovieContext.DocumentNode.SelectSingleNode(XPATH_Mname[1]).InnerText.Trim();
                    stWebEname[1] = docMovieContext.DocumentNode.SelectSingleNode(XPATH_Ename[1]).InnerText.Trim();
                    stWebYear[1] = docMovieContext.DocumentNode.SelectSingleNode(XPATH_Year[1]).InnerText.Trim();
                    listBox2.Items.Add(stWebMname[1] + "  " + stWebEname[1] + "  " + stWebYear[1]);
                }
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = ((ListBox)sender).SelectedIndex;
            textBox_Mname.Text = stWebMname[idx];
            //textBox_Ename.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(stWebEname[idx]); 
            textBox_Year.Text = stWebYear[idx];
        }
    }
}
