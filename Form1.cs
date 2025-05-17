using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Management;
using System.Windows.Forms;
using System.Linq;

namespace ProcessKillerApp
{
    public partial class Form1 : Form
    {
        private readonly Dictionary<string, Dictionary<string, string>> _langResources = new Dictionary<string, Dictionary<string, string>>
        {
            {
                "zh-CN", new Dictionary<string, string>
                {
                    {"FormTitle", "Python进程杀手-C#版"},
                    {"TitleLabel", "运行中的 Python 进程"},
                    {"KillButton", "终止进程"},
                    {"RefreshButton", "刷新列表"},
                    {"StatusReady", "就绪"},
                    {"SelectProcessTip", "请先选择一个要终止的进程"},
                    {"ConfirmKill", "确定要终止选中的Python进程吗？此操作不可恢复！"},
                    {"KillSuccess", "已成功终止进程 PID: {0}"},
                    {"PIDLabel", "PID: "},
                    {"NameLabel", "名称: "},
                    {"MemoryLabel", "内存: "},
                    {"PathLabel", "路径: "},
                    {"MBLabel", "MB"},
                    {"RefreshLoading", "正在刷新进程列表..."},
                    {"RefreshCount", "找到 {0} 个Python进程"},
                    {"RefreshFailed", "刷新失败: {0}"}
                }
            },
            {
                "en-US", new Dictionary<string, string>
                {
                    {"FormTitle", "Python Process Killer - C#"},
                    {"TitleLabel", "Running Python Processes"},
                    {"KillButton", "Kill Process"},
                    {"RefreshButton", "Refresh List"},
                    {"StatusReady", "Ready"},
                    {"SelectProcessTip", "Please select a process to kill first"},
                    {"ConfirmKill", "Are you sure you want to kill the selected Python process? This operation is irreversible!"},
                    {"KillSuccess", "Successfully killed process PID: {0}"},
                    {"PIDLabel", "PID: "},
                    {"NameLabel", "Name: "},
                    {"MemoryLabel", "Memory: "},
                    {"PathLabel", "Path: "},
                    {"MBLabel", "MB"},
                    {"RefreshLoading", "Refreshing process list..."},
                    {"RefreshCount", "Found {0} Python processes"},
                    {"RefreshFailed", "Refresh failed: {0}"}
                }
            }
        };

        private string _currentLang = "zh-CN";
        private ListBox _processListBox;
        private Button _killButton;
        private Button _refreshButton;
        private Label _statusLabel;
        private Label _titleLabel;

        public Form1()
        {
            InitializeComponents();
            UpdateUILanguage();
            RefreshProcesses();
        }

        private void InitializeComponents()
        {
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            _titleLabel = new Label
            {
                Location = new Point(30, 20),
                Size = new Size(300, 30),
                Font = new Font("Segoe UI Semibold", 14)
            };
            this.Controls.Add(_titleLabel);

            _processListBox = new ListBox
            {
                Location = new Point(30, 60),
                Size = new Size(720, 400),
                DrawMode = DrawMode.OwnerDrawFixed,
                ItemHeight = 50
            };
            _processListBox.DrawItem += ProcessListBox_DrawItem;
            this.Controls.Add(_processListBox);

            _killButton = new Button
            {
                Location = new Point(30, 470),
                Size = new Size(120, 40),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            _killButton.Click += KillButton_Click;
            this.Controls.Add(_killButton);

            _refreshButton = new Button
            {
                Location = new Point(160, 470),
                Size = new Size(120, 40),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            _refreshButton.Click += RefreshButton_Click;
            this.Controls.Add(_refreshButton);

            var langButton = new Button
            {
                Location = new Point(630, 470),
                Size = new Size(120, 40),
                BackColor = Color.FromArgb(30, 144, 255),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Text = "切换语言"
            };
            langButton.Click += LangButton_Click;
            this.Controls.Add(langButton);

            _statusLabel = new Label
            {
                Location = new Point(30, 520),
                Size = new Size(720, 30),
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(_statusLabel);
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            RefreshProcesses();
        }

        private void LangButton_Click(object sender, EventArgs e)
        {
            _currentLang = _currentLang == "zh-CN" ? "en-US" : "zh-CN";
            UpdateUILanguage();
        }

        private void UpdateUILanguage()
        {
            this.Text = _langResources[_currentLang]["FormTitle"];
            _titleLabel.Text = _langResources[_currentLang]["TitleLabel"];
            _killButton.Text = _langResources[_currentLang]["KillButton"];
            _refreshButton.Text = _langResources[_currentLang]["RefreshButton"];
            _statusLabel.Text = _langResources[_currentLang]["StatusReady"];
            RefreshProcesses();
        }

        private void RefreshProcesses()
        {
            _statusLabel.Text = _langResources[_currentLang]["RefreshLoading"];
            _processListBox.Items.Clear();

            try
            {
                var processes = GetPythonProcesses();
                _processListBox.Items.AddRange(processes.ToArray());
                _statusLabel.Text = string.Format(_langResources[_currentLang]["RefreshCount"], processes.Count);
            }
            catch (Exception ex)
            {
                _statusLabel.Text = string.Format(_langResources[_currentLang]["RefreshFailed"], ex.Message);
            }
        }

        private List<string> GetPythonProcesses()
        {
            var result = new List<string>();
            foreach (var proc in Process.GetProcesses())
            {
                if (!proc.ProcessName.ToLower().Contains("python")) continue;

                string path = "未知路径";
                try
                {
                    var query = new ManagementObjectSearcher(string.Format("SELECT CommandLine FROM Win32_Process WHERE ProcessId = {0}", proc.Id));
                    var mo = query.Get().Cast<ManagementObject>().FirstOrDefault();
                    if (mo != null)
                    {
                        // C#5 兼容：显式检查null
                        object cmdLineObj = mo["CommandLine"];
                        string commandLine = cmdLineObj != null ? cmdLineObj.ToString() : null;
                        
                        if (!string.IsNullOrEmpty(commandLine))
                        {
                            var cmdArgs = ParseCommandLine(commandLine);
                            if (cmdArgs.Count >= 2)
                            {
                                path = cmdArgs[1]; // 脚本路径（第二个参数）
                            }
                            else
                            {
                                if (proc.MainModule != null)
                                {
                                    path = proc.MainModule.FileName;
                                }
                            }
                        }
                    }
                }
                catch { /* 忽略WMI访问异常 */ }

                double memory = proc.WorkingSet64 / 1024.0 / 1024.0;
                string memoryStr = memory.ToString("F2");

                result.Add(string.Format(
                    "{0}{1}|{2}{3}|{4}{5} {6}|{7}{8}",
                    _langResources[_currentLang]["PIDLabel"], proc.Id,
                    _langResources[_currentLang]["NameLabel"], proc.ProcessName,
                    _langResources[_currentLang]["MemoryLabel"], memoryStr,
                    _langResources[_currentLang]["MBLabel"],
                    _langResources[_currentLang]["PathLabel"], path
                ));
            }
            return result;
        }

        // 新增：C#5兼容的命令行参数解析方法
        private List<string> ParseCommandLine(string commandLine)
        {
            var args = new List<string>();
            bool inQuote = false;
            int startIndex = 0;
            for (int i = 0; i < commandLine.Length; i++)
            {
                if (commandLine[i] == '"')
                {
                    inQuote = !inQuote;
                }
                else if (!inQuote && commandLine[i] == ' ')
                {
                    if (startIndex < i)
                    {
                        args.Add(commandLine.Substring(startIndex, i - startIndex));
                    }
                    startIndex = i + 1;
                }
            }
            if (startIndex < commandLine.Length)
            {
                args.Add(commandLine.Substring(startIndex));
            }
            return args;
        }

        private void ProcessListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            e.DrawBackground();

            var info = _processListBox.Items[e.Index].ToString().Split('|');
            var accentColor = Color.FromArgb(30, 144, 255);

            e.Graphics.DrawString(info[0], 
                new Font("Segoe UI Semibold", 10), 
                new SolidBrush(accentColor), 
                new Rectangle(20, e.Bounds.Top + 5, 150, 20));

            e.Graphics.DrawString(info[1], 
                new Font("Segoe UI", 10), 
                Brushes.Black, 
                new Rectangle(180, e.Bounds.Top + 5, 200, 20));

            e.Graphics.DrawString(info[2], 
                new Font("Segoe UI", 10), 
                Brushes.DimGray, 
                new Rectangle(380, e.Bounds.Top + 5, 150, 20));

            e.Graphics.DrawString(info[3], 
                new Font("Segoe UI", 9), 
                Brushes.DarkSlateGray, 
                new Rectangle(20, e.Bounds.Top + 25, 680, 20));

            e.DrawFocusRectangle();
        }

        private void KillButton_Click(object sender, EventArgs e)
        {
            if (_processListBox.SelectedIndex < 0)
            {
                MessageBox.Show(this, 
                    _langResources[_currentLang]["SelectProcessTip"], 
                    "提示", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
                return;
            }

            var selectedItem = _processListBox.Items[_processListBox.SelectedIndex].ToString();
            var pidStr = selectedItem.Split('|')[0].Split(':')[1].Trim();
            
            int pid;
            if (!int.TryParse(pidStr, out pid)) return;

            var confirmResult = MessageBox.Show(this,
                _langResources[_currentLang]["ConfirmKill"],
                "确认终止",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    var proc = Process.GetProcessById(pid);
                    proc.Kill();
                    _statusLabel.Text = string.Format(_langResources[_currentLang]["KillSuccess"], pid);
                    RefreshProcesses();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, string.Format("终止失败: {0}", ex.Message), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
