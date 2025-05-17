# English:
# PythonProcessKiller-C# - Python Process Management Tool (C# Version)  

![GitHub release (latest by date)](https://img.shields.io/github/v/release/add-qwq/PythonProcessKiller-Csharp?style=flat-square)  
![GitHub stars](https://img.shields.io/github/stars/add-qwq/PythonProcessKiller-Csharp?style=flat-square)  
![C# 版本](https://img.shields.io/badge/C%23-.NET%20Framework%204.8%2B-blue?style=flat-square)  
![License](https://img.shields.io/github/license/add-qwq/PythonProcessKiller-Csharp?style=flat-square)  

This is the **C# version** of the Python process management tool, with a corresponding **[Python version](https://github.com/add-qwq/PythonProcessKiller-Py)** available (focused on rapid development, flexible modification, and direct invocation in development environments). The C# version prioritizes **long-term maintenance and high-speed operation**, making it suitable for scenarios requiring stable performance. Note: The UI aesthetics are slightly simpler than the Python version, and due to technical limitations, automatic refresh is not implemented—manual refresh via the button is required.  


## 🌟 Key Features  
- **Manual Refresh**: Click the "Refresh List" button to manually update the list of running Python processes (`python.exe`/`pythonw.exe`), displaying PID, process name, memory usage, and executable path.  
- **Multi-Language Support**: Switch between **Simplified Chinese/English** with one click (via the "Switch Language" button), no additional language packs required.  
- **Safe Termination**: Select a process and click "Terminate Process" to stop it. Handles exceptions like permission issues or already exited processes with clear prompts.  
- **Visualized Information**: Custom-rendered process list with color-coded details (PID in accent color, path in subtle text) for better readability.  


## 🚀 Quick Start  

### Option 1: Download Prebuilt EXE (Recommended for Quick Use)  
No .NET SDK required (but needs .NET Framework 4.8+):  
1. Go to the [Releases page](https://github.com/add-qwq/PythonProcessKiller-Csharp/releases).  
2. Download `PythonProcessKiller-C#-EXE.zip` and extract it.  
3. Run `PythonProcessKiller-C#.exe` directly.  


### Option 2: Run from Source Code (For Development/Modification)  
Requires .NET Framework 4.8 SDK :  

#### Prerequisites  
- .NET Framework 4.8 SDK (download [here](https://dotnet.microsoft.com/download/dotnet-framework/net48))  
- Command Prompt (Windows)  

#### Steps  
1. Download source code:  
   - Go to the [Releases page](https://github.com/add-qwq/PythonProcessKiller-Csharp/releases), click "Source code (ZIP)" to download.  
   - Extract the ZIP to your preferred location.  

2. Compile via command line:  
   ```bash  
   # Navigate to the extracted folder (where *.cs files are located)  
   cd PythonProcessKiller-Csharp
   
   # Compile using csc (C# compiler)  
   csc /target:winexe /out:bin/ProcessKillerApp.exe *.cs /r:System.dll /r:System.Windows.Forms.dll /r:System.Drawing.dll  
   ```  
   - After successful compilation, run `bin/ProcessKillerApp.exe`.  


## 🖥 Interface Overview  
![English Interface](https://github.com/add-qwq/PythonProcessKiller-Csharp/raw/main/PythonProcessKiller-Py-EN.png?raw=true)  
*Switch languages via the "Switch Language" button. Core functions are identical across languages.*  


## 📘 Usage Examples  

### Example 1: Manually Refresh and Terminate a Python Process  
**Goal**: Terminate a Python process named `python.exe` with high memory usage.  

**Steps**:  
1. Launch the app. The initial list shows no data (manual refresh required).  
2. Click "Refresh List" to load current Python processes.  
3. Select the target process in the list (highlighted on click).  
4. Click "Terminate Process" and confirm in the pop-up.  
5. On success, the status bar shows "Successfully killed process PID: XXXX", and the list refreshes automatically.  


### Example 2: Switch Interface Language  
**Goal**: Change the interface to English.  

**Steps**:  
1. Click the "Switch Language" button (top-right, labeled "切换语言" in Chinese).  
2. The interface updates to English, including buttons, labels, and status messages.  
3. Click "Refresh List" to reload process data in the new language.  


### Example 3: Handle Permission Issues  
**Goal**: Terminate a protected Python process.  

**Steps**:  
1. Attempt to terminate the process. If a "permission denied" error occurs, close the app.  
2. Right-click the EXE/Compiled program and select "Run as Administrator".  
3. Retry termination—the elevated permissions should resolve the issue.  


## 📜 License  
This project is licensed under the [Apache License 2.0](https://github.com/add-qwq/PythonProcessKiller-Csharp/blob/main/LICENSE).  


## 🙋 Contributing & Feedback  
- **Bug Reports/Feature Requests**: Submit an [Issue](https://github.com/add-qwq/PythonProcessKiller-Csharp/issues).  
- **Code Contributions**: Fork the repo, make changes, and submit a PR (welcome to optimize UI rendering or add auto-refresh functionality).  
- **Localization**: To add new languages, update the `_langResources` dictionary in `Form1.cs` (reference `zh-CN` and `en-US` configurations).  


---  


# PythonProcessKiller-C# - Python 进程管理工具（C# 版）  

![GitHub release (latest by date)](https://img.shields.io/github/v/release/add-qwq/PythonProcessKiller-Csharp?style=flat-square)  
![GitHub stars](https://img.shields.io/github/stars/add-qwq/PythonProcessKiller-Csharp?style=flat-square)  
![C# 版本](https://img.shields.io/badge/C%23-.NET%20Framework%204.8%2B-blue?style=flat-square)  
![许可证](https://img.shields.io/github/license/add-qwq/PythonProcessKiller-Csharp?style=flat-square)  

本项目为 **Python 进程管理工具** 的 C# 版本实现，配套存在 **[Python 版本](https://github.com/add-qwq/PythonProcessKiller-Py)**（侧重快速开发与灵活修改，支持直接在开发环境中调用运行）。C# 版聚焦 **长期维护与高速运行**，适合对性能稳定性要求较高的场景。注：界面美观度略逊于 Python 版，且因技术限制未实现自动刷新功能，需通过按钮手动刷新。  


## 🌟 核心功能  
- **手动刷新**：点击「刷新列表」按钮手动获取当前运行的 Python 进程（含 `python.exe`/`pythonw.exe`），显示 PID、进程名称、内存占用及可执行路径。  
- **多语言支持**：通过「切换语言」按钮一键切换 **简体中文/英文** 界面，无额外语言包依赖。  
- **安全终止**：选中目标进程后点击「终止进程」按钮尝试终止，自动处理无权限、进程已退出等异常场景（含明确提示）。  
- **信息可视化**：自定义绘制进程列表（PID 高亮显示、路径文本淡化处理），提升信息可读性。  


## 🚀 快速开始  

### 方式 1：下载预编译 EXE（推荐快速使用）  
无需安装 .NET SDK（需 .NET Framework 4.8+ 运行时）：  
1. 前往 [Releases 页面](https://github.com/add-qwq/PythonProcessKiller-Csharp/releases)。  
2. 下载 `PythonProcessKiller-C#-EXE.zip` 并解压。  
3. 直接运行 `PythonProcessKiller-C#.exe`。  


### 方式 2：从源代码运行（适合开发/修改）  
需 .NET Framework 4.8 SDK：  

#### 环境要求  
- .NET Framework 4.8 SDK（下载地址：[点此获取](https://dotnet.microsoft.com/download/dotnet-framework/net48)）  
- Windows 命令提示符  

#### 步骤  
1. 下载源代码：  
   - 前往 [Releases 页面](https://github.com/add-qwq/PythonProcessKiller-Csharp/releases)，点击 "Source code (ZIP)" 下载压缩包。  
   - 解压至目标位置。  

2. 通过命令行编译：  
   ```bash  
   # 进入解压后的文件夹（包含 *.cs 文件的目录）  
   cd PythonProcessKiller-Csharp
   
   # 使用 csc 编译器编译（Windows 命令提示符）  
   csc /target:winexe /out:bin/ProcessKillerApp.exe *.cs /r:System.dll /r:System.Windows.Forms.dll /r:System.Drawing.dll  
   ```  
   - 编译成功后，运行 `bin/ProcessKillerApp.exe`。  


## 🖥 界面概览  
![中文界面](https://github.com/add-qwq/PythonProcessKiller-Csharp/raw/main/PythonProcessKiller-Py-CN.png?raw=true)  
*(通过「切换语言」按钮切换中英文，核心功能完全一致。)*  


## 📘 使用示例  

### 示例 1：手动刷新并终止 Python 进程  
**目标**：终止一个路径为 `D:\test\pyt.py` 且内存占用较高的进程。  

**步骤**：  
1. 启动程序，初始列表无数据（需手动刷新）。  
2. 点击「刷新列表」加载当前 Python 进程。  
3. 在列表中选中目标进程（点击高亮）。  
4. 点击「终止进程」并在弹出框中确认。  
5. 成功后状态栏显示「已成功终止进程 PID: XXXX」，列表自动刷新。  


### 示例 2：切换界面语言  
**目标**：将界面切换为英文。  

**步骤**：  
1. 点击右上角「切换语言」按钮（中文界面显示为「切换语言」）。  
2. 界面自动切换为英文，按钮、标签及状态信息同步更新。  
3. 点击「Refresh List」重新加载进程数据（英文界面按钮）。  


### 示例 3：处理无权限终止进程的场景  
**目标**：终止受保护的 Python 进程。  

**步骤**：  
1. 尝试终止进程，若提示「无权限」错误，关闭程序。  
2. 右键点击 EXE 文件或编译后的程序，选择「以管理员身份运行」。  
3. 重新尝试终止，提升后的权限可解决此问题。  


## 📜 许可证  
本项目采用 [Apache 2.0 许可证](https://github.com/add-qwq/PythonProcessKiller-Csharp/blob/main/LICENSE)。  


## 🙋 贡献与反馈  
- **问题反馈/功能建议**：提交 [Issue](https://github.com/add-qwq/PythonProcessKiller-Csharp/issues)。  
- **代码贡献**：Fork 仓库，修改后提交 PR（欢迎优化界面渲染或添加自动刷新功能）。  
- **多语言支持**：如需添加其他语言，可在 `Form1.cs` 的 `_langResources` 字典中补充翻译（参考 `zh-CN` 和 `en-US` 配置）。  
