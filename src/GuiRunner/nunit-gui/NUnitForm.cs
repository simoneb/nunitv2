/********************************************************************************************************************
'
' Copyright (c) 2002, James Newkirk, Michael C. Two, Alexei Vorontsov, Philip Craig
'
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and
' to permit persons to whom the Software is furnished to do so, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
' THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
' AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
'
'*******************************************************************************************************************/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using Microsoft.Win32;

namespace NUnit.Gui
{
	using NUnit.Core;
	using NUnit.Util;

	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class NUnitForm : System.Windows.Forms.Form
	{
		#region Static and instance variables

		private static RecentAssemblyUtil assemblyUtil;

		private bool runCommandEnabled = false;
		private string assemblyFileName;
		private AssemblyWatcher watcher;
		private UIActions actions;
		public System.Windows.Forms.Splitter splitter1;
		public System.Windows.Forms.Panel panel1;
		public System.Windows.Forms.GroupBox groupBox1;
		public System.Windows.Forms.Splitter splitter2;
		public System.Windows.Forms.TabPage testsNotRun;
		public System.Windows.Forms.MenuItem menuItem4;
		public System.Windows.Forms.MenuItem menuItem6;
		public System.Windows.Forms.MenuItem menuItem8;
		public System.Windows.Forms.MenuItem openMenuItem;
		public System.Windows.Forms.MenuItem exitMenuItem;
		public System.Windows.Forms.MenuItem helpMenuItem;
		public System.Windows.Forms.MenuItem aboutMenuItem;
		public System.Windows.Forms.MainMenu mainMenu;
		public System.Windows.Forms.ListBox detailList;
		public System.Windows.Forms.Splitter splitter3;
		public System.Windows.Forms.TextBox stackTrace;
		public System.Windows.Forms.Label label1;
		public System.Windows.Forms.Label suiteName;
		public System.Windows.Forms.Button runButton;
		public System.Windows.Forms.StatusBar statusBar;
		public System.Windows.Forms.StatusBarPanel status;
		public System.Windows.Forms.StatusBarPanel testCaseCount;
		public System.Windows.Forms.StatusBarPanel testsRun;
		public System.Windows.Forms.StatusBarPanel failures;
		public System.Windows.Forms.StatusBarPanel time;
		public System.Windows.Forms.OpenFileDialog openFileDialog;
		public System.Windows.Forms.ContextMenu treeViewMenu;
		public System.Windows.Forms.MenuItem runMenuItem;
		public System.Windows.Forms.ImageList treeImages;
		public NUnit.Util.TestSuiteTreeView testSuiteTreeView;
		public System.Windows.Forms.TabControl resultTabs;
		public System.Windows.Forms.TabPage errorPage;
		public System.Windows.Forms.TabPage stderr;
		public System.Windows.Forms.TabPage stdout;
		public System.Windows.Forms.TextBox stdErrTab;
		public System.Windows.Forms.TextBox stdOutTab;
		public System.Windows.Forms.MenuItem RecentAssemblies;
		public NUnit.Gui.ProgressBar progressBar;
		public System.Windows.Forms.TreeView notRunTree;
		private System.ComponentModel.IContainer components;
		public TextWriter stdOutWriter;
		public System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem closeMenuItem;
		public System.Windows.Forms.MenuItem fileMenuItem;
		public TextWriter stdErrWriter;

		#endregion
		
		#region Properties

		public Test SelectedSuite
		{
			get { return testSuiteTreeView.SelectedSuite; }
		}

		public Test ContextSuite
		{
			get { return testSuiteTreeView.ContextSuite; }
		}

		#endregion

		#region Construction and Disposal

		static NUnitForm()	
		{
			assemblyUtil = new RecentAssemblyUtil("recent-assemblies");
		}
		
		public NUnitForm(string assemblyFileName)
		{
			InitializeComponent();

			stdErrTab.Enabled = true;
			stdOutTab.Enabled = true;

			SetDefault(runButton);
			DisableRunCommand();

			stdOutWriter = new TextBoxWriter(stdOutTab);
			Console.SetOut(stdOutWriter);
			stdErrWriter = new TextBoxWriter(stdErrTab);
			Console.SetError(stdErrWriter);

			actions = new UIActions(stdOutWriter, stdErrWriter);

			actions.TestStartedEvent += new UIActions.TestStartedHandler(OnTestStarted);
			actions.TestFinishedEvent += new UIActions.TestFinishedHandler(OnTestFinished);
			actions.SuiteFinishedEvent += new UIActions.SuiteFinishedHandler(OnSuiteFinished);
			actions.RunStartingEvent += new UIActions.RunStartingHandler(OnRunStarting);
			actions.RunFinishedEvent += new UIActions.RunFinishedHandler(OnRunFinished);
			actions.SuiteLoadedEvent += new UIActions.SuiteLoadedHandler(OnSuiteLoaded);
			actions.SuiteUnloadedEvent += new UIActions.SuiteUnloadedHandler(OnSuiteUnloaded);

			if (assemblyFileName == null)
				assemblyFileName = assemblyUtil.RecentAssembly;

			if(assemblyFileName != null)
				LoadAssembly(assemblyFileName);

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion
		
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(NUnitForm));
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.status = new System.Windows.Forms.StatusBarPanel();
			this.testCaseCount = new System.Windows.Forms.StatusBarPanel();
			this.testsRun = new System.Windows.Forms.StatusBarPanel();
			this.failures = new System.Windows.Forms.StatusBarPanel();
			this.time = new System.Windows.Forms.StatusBarPanel();
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.fileMenuItem = new System.Windows.Forms.MenuItem();
			this.openMenuItem = new System.Windows.Forms.MenuItem();
			this.closeMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.RecentAssemblies = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.exitMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.helpMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.aboutMenuItem = new System.Windows.Forms.MenuItem();
			this.testSuiteTreeView = new NUnit.Util.TestSuiteTreeView();
			this.treeViewMenu = new System.Windows.Forms.ContextMenu();
			this.runMenuItem = new System.Windows.Forms.MenuItem();
			this.treeImages = new System.Windows.Forms.ImageList(this.components);
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel1 = new System.Windows.Forms.Panel();
			this.resultTabs = new System.Windows.Forms.TabControl();
			this.errorPage = new System.Windows.Forms.TabPage();
			this.stackTrace = new System.Windows.Forms.TextBox();
			this.splitter3 = new System.Windows.Forms.Splitter();
			this.detailList = new System.Windows.Forms.ListBox();
			this.testsNotRun = new System.Windows.Forms.TabPage();
			this.notRunTree = new System.Windows.Forms.TreeView();
			this.stderr = new System.Windows.Forms.TabPage();
			this.stdErrTab = new System.Windows.Forms.TextBox();
			this.stdout = new System.Windows.Forms.TabPage();
			this.stdOutTab = new System.Windows.Forms.TextBox();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.runButton = new System.Windows.Forms.Button();
			this.suiteName = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.progressBar = new NUnit.Gui.ProgressBar();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.status)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.testCaseCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.testsRun)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.failures)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.time)).BeginInit();
			this.panel1.SuspendLayout();
			this.resultTabs.SuspendLayout();
			this.errorPage.SuspendLayout();
			this.testsNotRun.SuspendLayout();
			this.stderr.SuspendLayout();
			this.stdout.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusBar
			// 
			this.statusBar.Location = new System.Drawing.Point(0, 438);
			this.statusBar.Name = "statusBar";
			this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						 this.status,
																						 this.testCaseCount,
																						 this.testsRun,
																						 this.failures,
																						 this.time});
			this.statusBar.ShowPanels = true;
			this.statusBar.Size = new System.Drawing.Size(798, 27);
			this.statusBar.TabIndex = 0;
			this.statusBar.Text = "Status";
			// 
			// status
			// 
			this.status.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.status.Text = "Status";
			this.status.Width = 382;
			// 
			// testCaseCount
			// 
			this.testCaseCount.Text = "Test Cases:";
			// 
			// testsRun
			// 
			this.testsRun.Text = "Tests Run:";
			// 
			// failures
			// 
			this.failures.Text = "Failures:";
			// 
			// time
			// 
			this.time.Text = "Time:";
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.fileMenuItem,
																					 this.menuItem6});
			// 
			// fileMenuItem
			// 
			this.fileMenuItem.Index = 0;
			this.fileMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.openMenuItem,
																						 this.closeMenuItem,
																						 this.menuItem3,
																						 this.RecentAssemblies,
																						 this.menuItem4,
																						 this.exitMenuItem});
			this.fileMenuItem.Text = "&File";
			this.fileMenuItem.Popup += new System.EventHandler(this.fileMenu_Popup);
			// 
			// openMenuItem
			// 
			this.openMenuItem.Index = 0;
			this.openMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
			this.openMenuItem.Text = "&Open...";
			this.openMenuItem.Click += new System.EventHandler(this.openMenuItem_Click);
			// 
			// closeMenuItem
			// 
			this.closeMenuItem.Index = 1;
			this.closeMenuItem.Text = "&Close";
			this.closeMenuItem.Click += new System.EventHandler(this.closeMenuItem_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "-";
			// 
			// RecentAssemblies
			// 
			this.RecentAssemblies.Index = 3;
			this.RecentAssemblies.Text = "Recent Assemblies";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 4;
			this.menuItem4.Text = "-";
			// 
			// exitMenuItem
			// 
			this.exitMenuItem.Index = 5;
			this.exitMenuItem.Text = "E&xit";
			this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 1;
			this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.helpMenuItem,
																					  this.menuItem8,
																					  this.aboutMenuItem});
			this.menuItem6.Text = "&Help";
			// 
			// helpMenuItem
			// 
			this.helpMenuItem.Enabled = false;
			this.helpMenuItem.Index = 0;
			this.helpMenuItem.Text = "Help";
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 1;
			this.menuItem8.Text = "-";
			// 
			// aboutMenuItem
			// 
			this.aboutMenuItem.Index = 2;
			this.aboutMenuItem.Text = "&About NUnit...";
			this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
			// 
			// testSuiteTreeView
			// 
			this.testSuiteTreeView.AllowDrop = true;
			this.testSuiteTreeView.ContextMenu = this.treeViewMenu;
			this.testSuiteTreeView.Dock = System.Windows.Forms.DockStyle.Left;
			this.testSuiteTreeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.testSuiteTreeView.ImageList = this.treeImages;
			this.testSuiteTreeView.Name = "testSuiteTreeView";
			this.testSuiteTreeView.SelectedNode = null;
			this.testSuiteTreeView.Size = new System.Drawing.Size(307, 438);
			this.testSuiteTreeView.TabIndex = 1;
			this.testSuiteTreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.testSuiteTreeView_DragDrop);
			this.testSuiteTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.testSuiteTreeView_AfterSelect);
			this.testSuiteTreeView.DoubleClick += new System.EventHandler(this.testSuiteTreeView_DoubleClick);
			this.testSuiteTreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.testSuiteTreeView_DragEnter);
			// 
			// treeViewMenu
			// 
			this.treeViewMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.runMenuItem});
			// 
			// runMenuItem
			// 
			this.runMenuItem.Index = 0;
			this.runMenuItem.Text = "&Run";
			this.runMenuItem.Click += new System.EventHandler(this.runMenuItem_Click);
			// 
			// treeImages
			// 
			this.treeImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.treeImages.ImageSize = new System.Drawing.Size(16, 16);
			this.treeImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeImages.ImageStream")));
			this.treeImages.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(307, 0);
			this.splitter1.MinSize = 240;
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(4, 438);
			this.splitter1.TabIndex = 2;
			this.splitter1.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.resultTabs,
																				 this.splitter2,
																				 this.groupBox1});
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(311, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(487, 438);
			this.panel1.TabIndex = 3;
			// 
			// resultTabs
			// 
			this.resultTabs.Controls.AddRange(new System.Windows.Forms.Control[] {
																					 this.errorPage,
																					 this.testsNotRun,
																					 this.stderr,
																					 this.stdout});
			this.resultTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.resultTabs.Location = new System.Drawing.Point(0, 162);
			this.resultTabs.Name = "resultTabs";
			this.resultTabs.SelectedIndex = 0;
			this.resultTabs.Size = new System.Drawing.Size(487, 276);
			this.resultTabs.TabIndex = 2;
			// 
			// errorPage
			// 
			this.errorPage.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.stackTrace,
																					this.splitter3,
																					this.detailList});
			this.errorPage.Location = new System.Drawing.Point(4, 25);
			this.errorPage.Name = "errorPage";
			this.errorPage.Size = new System.Drawing.Size(479, 247);
			this.errorPage.TabIndex = 0;
			this.errorPage.Text = "Errors and Failures";
			// 
			// stackTrace
			// 
			this.stackTrace.Dock = System.Windows.Forms.DockStyle.Fill;
			this.stackTrace.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.stackTrace.Location = new System.Drawing.Point(0, 107);
			this.stackTrace.Multiline = true;
			this.stackTrace.Name = "stackTrace";
			this.stackTrace.ReadOnly = true;
			this.stackTrace.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.stackTrace.Size = new System.Drawing.Size(479, 140);
			this.stackTrace.TabIndex = 2;
			this.stackTrace.Text = "";
			this.stackTrace.WordWrap = false;
			// 
			// splitter3
			// 
			this.splitter3.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter3.Location = new System.Drawing.Point(0, 104);
			this.splitter3.MinSize = 100;
			this.splitter3.Name = "splitter3";
			this.splitter3.Size = new System.Drawing.Size(479, 3);
			this.splitter3.TabIndex = 1;
			this.splitter3.TabStop = false;
			// 
			// detailList
			// 
			this.detailList.Dock = System.Windows.Forms.DockStyle.Top;
			this.detailList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.detailList.HorizontalExtent = 2000;
			this.detailList.HorizontalScrollbar = true;
			this.detailList.ItemHeight = 20;
			this.detailList.Name = "detailList";
			this.detailList.ScrollAlwaysVisible = true;
			this.detailList.Size = new System.Drawing.Size(479, 104);
			this.detailList.TabIndex = 0;
			this.detailList.SelectedIndexChanged += new System.EventHandler(this.detailList_SelectedIndexChanged);
			// 
			// testsNotRun
			// 
			this.testsNotRun.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.notRunTree});
			this.testsNotRun.Location = new System.Drawing.Point(4, 25);
			this.testsNotRun.Name = "testsNotRun";
			this.testsNotRun.Size = new System.Drawing.Size(479, 247);
			this.testsNotRun.TabIndex = 1;
			this.testsNotRun.Text = "Tests Not Run";
			// 
			// notRunTree
			// 
			this.notRunTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.notRunTree.ImageIndex = -1;
			this.notRunTree.Name = "notRunTree";
			this.notRunTree.SelectedImageIndex = -1;
			this.notRunTree.Size = new System.Drawing.Size(479, 247);
			this.notRunTree.TabIndex = 0;
			// 
			// stderr
			// 
			this.stderr.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.stdErrTab});
			this.stderr.Location = new System.Drawing.Point(4, 25);
			this.stderr.Name = "stderr";
			this.stderr.Size = new System.Drawing.Size(479, 247);
			this.stderr.TabIndex = 2;
			this.stderr.Text = "Standard Error";
			// 
			// stdErrTab
			// 
			this.stdErrTab.Dock = System.Windows.Forms.DockStyle.Fill;
			this.stdErrTab.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.stdErrTab.Multiline = true;
			this.stdErrTab.Name = "stdErrTab";
			this.stdErrTab.ReadOnly = true;
			this.stdErrTab.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.stdErrTab.Size = new System.Drawing.Size(479, 247);
			this.stdErrTab.TabIndex = 0;
			this.stdErrTab.Text = "";
			this.stdErrTab.WordWrap = false;
			// 
			// stdout
			// 
			this.stdout.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.stdOutTab});
			this.stdout.Location = new System.Drawing.Point(4, 25);
			this.stdout.Name = "stdout";
			this.stdout.Size = new System.Drawing.Size(479, 247);
			this.stdout.TabIndex = 3;
			this.stdout.Text = "Standard Out";
			// 
			// stdOutTab
			// 
			this.stdOutTab.Dock = System.Windows.Forms.DockStyle.Fill;
			this.stdOutTab.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.stdOutTab.Multiline = true;
			this.stdOutTab.Name = "stdOutTab";
			this.stdOutTab.ReadOnly = true;
			this.stdOutTab.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.stdOutTab.Size = new System.Drawing.Size(479, 247);
			this.stdOutTab.TabIndex = 0;
			this.stdOutTab.Text = "";
			this.stdOutTab.WordWrap = false;
			// 
			// splitter2
			// 
			this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter2.Location = new System.Drawing.Point(0, 158);
			this.splitter2.MinSize = 130;
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new System.Drawing.Size(487, 4);
			this.splitter2.TabIndex = 1;
			this.splitter2.TabStop = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.runButton,
																					this.suiteName,
																					this.label1,
																					this.progressBar});
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(487, 158);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Run";
			// 
			// runButton
			// 
			this.runButton.Location = new System.Drawing.Point(10, 69);
			this.runButton.Name = "runButton";
			this.runButton.Size = new System.Drawing.Size(96, 28);
			this.runButton.TabIndex = 3;
			this.runButton.Text = "&Run";
			this.runButton.Click += new System.EventHandler(this.runButton_Click);
			// 
			// suiteName
			// 
			this.suiteName.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.suiteName.Location = new System.Drawing.Point(92, 30);
			this.suiteName.Name = "suiteName";
			this.suiteName.Size = new System.Drawing.Size(317, 19);
			this.suiteName.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(10, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(51, 19);
			this.label1.TabIndex = 1;
			this.label1.Text = "Test:";
			// 
			// progressBar
			// 
			this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.progressBar.CausesValidation = false;
			this.progressBar.Enabled = false;
			this.progressBar.ForeColor = System.Drawing.SystemColors.Highlight;
			this.progressBar.Location = new System.Drawing.Point(10, 109);
			this.progressBar.Maximum = 100;
			this.progressBar.Minimum = 0;
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(460, 29);
			this.progressBar.Step = 1;
			this.progressBar.TabIndex = 0;
			this.progressBar.Value = 0;
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "Assemblies (*.dll)|*.dll|Executables (*.exe)|*.exe|All Files (*.*)|*.*";
			// 
			// NUnitForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
			this.ClientSize = new System.Drawing.Size(798, 465);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.panel1,
																		  this.splitter1,
																		  this.testSuiteTreeView,
																		  this.statusBar});
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu;
			this.Name = "NUnitForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "NUnit";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.NUnitForm_Closing);
			this.Load += new System.EventHandler(this.NUnitForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.status)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.testCaseCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.testsRun)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.failures)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.time)).EndInit();
			this.panel1.ResumeLayout(false);
			this.resultTabs.ResumeLayout(false);
			this.errorPage.ResumeLayout(false);
			this.testsNotRun.ResumeLayout(false);
			this.stderr.ResumeLayout(false);
			this.stdout.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Application Entry Point

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static int Main(string[] args) 
		{
			string assemblyName = null;

			try 
			{
				ArrayList allowedParameters = new ArrayList();
				allowedParameters.Add(CommandLineParser.ASSEMBLY_PARM);
				CommandLineParser parser = new CommandLineParser(allowedParameters, args);
				if(!parser.NoArgs)
					assemblyName = parser.AssemblyName;

				if(assemblyName != null)
				{
					FileInfo fileInfo = new FileInfo(assemblyName);
					if(!fileInfo.Exists)
					{
						string message = String.Format("{0} does not exist", fileInfo.FullName);
						MessageBox.Show(null,message,"assembly does not exist",
							MessageBoxButtons.OK,MessageBoxIcon.Stop);
						return 1;
					}
					else
					{
						assemblyName = fileInfo.FullName;
					}
				}

				NUnitForm form = new NUnitForm(assemblyName);
				Application.Run(form);
			}
			catch(CommandLineException cle)
			{
				string message = cle.Message;
				MessageBox.Show(null,message,"Invalid Command Line Options",
					MessageBoxButtons.OK,MessageBoxIcon.Stop);
				return 2;
			}	

			return 0;

		}

		#endregion

		#region Handlers for UI Events

		/// <summary>
		/// When File menu is about to display, enable/disable Close
		/// </summary>
		private void fileMenu_Popup(object sender, System.EventArgs e)
		{
			closeMenuItem.Enabled = IsAssemblyOpen();
		}

		/// <summary>
		/// When File+Open is selected, display FileOpenDialog and
		/// open whatever assembly the user selects.
		/// </summary>
		private void openMenuItem_Click(object sender, System.EventArgs e)
		{
			if (openFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK) 
			{
				assemblyFileName = openFileDialog.FileName;
				LoadAssembly(assemblyFileName);
			}
		
		}

		/// <summary>
		/// When File+Close is selected, unload the assembly
		/// </summary>
		private void closeMenuItem_Click(object sender, System.EventArgs e)
		{
			UnloadAssembly();
		}

		/// <summary>
		/// Open recently used assembly when it's menu item is selected.
		/// </summary>
		private void recentFile_clicked(object sender, System.EventArgs args) 
		{
			MenuItem item = (MenuItem) sender;
			string text = item.Text;

			assemblyFileName = text.Substring(2);
			LoadAssembly(assemblyFileName);
		}

		/// <summary>
		/// Exit application when menu item is selected.
		/// </summary>
		private void exitMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Display the about box when menu item is selected
		/// </summary>
		private void aboutMenuItem_Click(object sender, System.EventArgs e)
		{
			AboutBox aboutBox = new AboutBox();
			aboutBox.Show();
		}

		/// <summary>
		/// When the Run Button is clicked, run the selected test.
		/// </summary>
		private void runButton_Click(object sender, System.EventArgs e)
		{
			actions.RunTestSuite( SelectedSuite );
		}

		/// <summary>
		/// When Run context menu item is clicked, run the test that
		/// was selected when the right click was done.
		/// </summary>
		private void runMenuItem_Click(object sender, System.EventArgs e)
		{
			actions.RunTestSuite( ContextSuite );
		}

		/// <summary>
		/// When a TestCase leaf node is double-clicked, run it.
		/// Base TreeView class does expand/collapse when a non-leaf
		/// node is double-clicked.
		/// </summary>
		private void testSuiteTreeView_DoubleClick(object sender, System.EventArgs e)
		{
			if ( runCommandEnabled && testSuiteTreeView.SelectedNode.Nodes.Count == 0 )
			{
				actions.RunTestSuite( SelectedSuite );
			}
		}

		/// <summary>
		/// When a tree item is selected, display info pertaining to that test
		/// </summary>
		private void testSuiteTreeView_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			TestNode node = testSuiteTreeView.SelectedNode;

			SetSuiteName( node.Text );

			// TODO: Do we really want to do this?
			InitializeStatusBar( node.Test.CountTestCases );
		}

		/// <summary>
		/// When one of the detail failure items is selected, display
		/// the stack trace and set up the tool tip for that item.
		/// </summary>
		private void detailList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			TestResultItem resultItem = (TestResultItem)detailList.SelectedItem;
			//string stackTrace = resultItem.StackTrace;
			stackTrace.Text = resultItem.StackTrace;
			toolTip.SetToolTip(detailList,resultItem.GetMessage());
		}

		/// <summary>
		/// Exit application when space key is tapped
		/// </summary>
		protected override bool ProcessKeyPreview(ref 
			System.Windows.Forms.Message m) 
		{ 
			const int SPACE_BAR=32; 
			if (m.WParam.ToInt32() == SPACE_BAR)
			{ 
				this.Close();
				return true;
			}

			return base.ProcessKeyEventArgs( ref m ); 
		} 


		private static string WIDTH = "width";
		private static string HEIGHT = "height";
		private static string XLOCATION = "x-location";
		private static string YLOCATION = "y-location";

		/// <summary>
		///	Save position when form is about to close
		/// </summary>
		private void NUnitForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			RegistryKey key = NUnitRegistry.CurrentUser.CreateSubKey("form");

			key.SetValue(WIDTH, this.Size.Width.ToString());
			key.SetValue(HEIGHT, this.Size.Height.ToString());
			key.SetValue(XLOCATION, this.Location.X.ToString());
			key.SetValue(YLOCATION, this.Location.Y.ToString());
		}

		/// <summary>
		/// Get last position when form loads
		/// </summary>
		private void NUnitForm_Load(object sender, System.EventArgs e)
		{
			RegistryKey key = NUnitRegistry.CurrentUser.OpenSubKey("form");

			int xLocation = 10; 
			int yLocation = 10;
			int width = 632;
			int height = 432; 

			if(key != null)
			{
				width = int.Parse((string)key.GetValue(WIDTH));
				height = int.Parse((string)key.GetValue(HEIGHT));
				xLocation = int.Parse((string)key.GetValue(XLOCATION));
				yLocation = int.Parse((string)key.GetValue(YLOCATION));
			}

			SetBounds(xLocation, yLocation, width, height);	
		}

		/// <summary>
		/// When a file is dragged into the tree view, check to see if
		/// it's one we can handle and display appropriate effect.
		/// </summary>
		private void testSuiteTreeView_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{

			if ( IsValidFileDrop( e.Data ) )
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}

		/// <summary>
		/// When an assembly file is dropped on treeview, load it.
		/// </summary>
		private void testSuiteTreeView_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			if ( IsValidFileDrop( e.Data ) )
			{
				string[] fileNames = e.Data.GetData( DataFormats.FileDrop ) as string[];
					LoadAssembly( fileNames[0] );
			}
		}

		#endregion

		#region Handlers for events related to loading and running tests

		private void OnTestStarted(TestCase testCase)
		{
			status.Text = "Running : " + testCase.Name;
		}

		private void OnTestFinished(TestCaseResult result)
		{
			testSuiteTreeView.SetTestResult(result);

			progressBar.PerformStep();		

			if(!result.Executed)
			{
				if(progressBar.ForeColor == Color.Lime)
					progressBar.ForeColor = Color.Yellow;
			}
			else if(!result.IsSuccess)
				progressBar.ForeColor = Color.Red;
		}

		private void OnSuiteFinished(TestSuiteResult result)
		{
			testSuiteTreeView.SetTestResult(result);
		}

		private void OnRunStarting(Test test)
		{
			int testCount = test.CountTestCases;

			DisableRunCommand();
			ClearTestResults();
			InitializeStatusBar(testCount);

			SetSuiteName(test.Name);
			InitializeProgressBar(testCount);
		}

		private void OnRunFinished(TestResult result)
		{
			status.Text = "Completed"; 

			DisplayResults(result);

			if(detailList.Items.Count > 0)
				detailList.SelectedIndex = 0;

			testSuiteTreeView.Expand( result.Test );

			EnableRunCommand();
		}

		// Note: second argument could be omitted, but tests may not
		// always have the FullName set to the assembly name in future.
		private void OnSuiteLoaded(Test test, string assemblyFileName)
		{
			this.assemblyFileName = assemblyFileName;
			InstallWatcher( assemblyFileName );
			UpdateRecentAssemblies( assemblyFileName );

			testSuiteTreeView.Load(test);

			EnableRunCommand();
			ClearTestResults();
			InitializeProgressBar( test.CountTestCases );
		}

		private void OnSuiteUnloaded()
		{
			RemoveWatcher();
			this.assemblyFileName = null;

			ClearUI();
		}

		private void OnSuiteChanged(Test test)
		{
			// TODO: Get rid of the use of the helper
			// and consider keeping useful info in the tree
			if(!UIHelper.CompareTree(SelectedSuite,test))
				testSuiteTreeView.Load(test);

			// TODO: may want to keep this info
			EnableRunCommand();
			ClearTestResults();
			InitializeProgressBar( test.CountTestCases );
		}

		#endregion

		#region Helper methods for loading and running tests

		/// <summary>
		/// Make an assembly the new most recent assembly, and
		/// update the menu accordingly. The assembly may already
		/// be in the list of recent assemblies, or may not be.
		/// </summary>
		/// <param name="assemblyFileName">Full path of the assembly to make most recent</param>
		private void UpdateRecentAssemblies(string assemblyFileName)
		{
			assemblyUtil.RecentAssembly = assemblyFileName;
			LoadRecentAssemblyMenu();
		}

		/// <summary>
		/// Remove an assembly from the recent assembly list and
		/// update the menu accordingly.
		/// </summary>
		/// <param name="assemblyFileName">Full path of the assembly to remove</param>
		private void RemoveRecentAssembly(string assemblyFileName)
		{
			assemblyUtil.Remove( assemblyFileName );
			LoadRecentAssemblyMenu();
		}

		/// <summary>
		/// Install our watcher object so as to get notifications
		/// about changes to an assembly.
		/// </summary>
		/// <param name="assemblyFileName">Full path of the assembly to watch</param>
		private void InstallWatcher(string assemblyFileName)
		{
			if(watcher!=null)
			{
				watcher.Stop();
			}

			FileInfo info = new FileInfo(assemblyFileName);
			watcher = new AssemblyWatcher(1000, info);
			watcher.AssemblyChangedEvent += new AssemblyWatcher.AssemblyChangedHandler( ReloadAssembly );
		}

		/// <summary>
		/// Stop and remove our current watcher object.
		/// </summary>
		private void RemoveWatcher()
		{
			if ( watcher != null )
			{
				watcher.Stop();
				watcher = null;
			}
		}

		/// <summary>
		/// Display message and clean up after an assembly fails to load
		/// </summary>
		/// <param name="assemblyFileName">Full path to the assembly</param>
		/// <param name="exception">Exception that was thrown when loading the assembly</param>
		private void AssemblyLoadFailure( string assemblyFileName, Exception exception )
		{
			string message = exception.Message;
			if(exception.InnerException != null)
				message = exception.InnerException.Message;
			MessageBox.Show(this,message,"Assembly Load Failure",
				MessageBoxButtons.OK,MessageBoxIcon.Stop);

			RemoveRecentAssembly( assemblyFileName );
		}

		/// <summary>
		/// Load an assembly into the UI
		/// </summary>
		/// <param name="assemblyFileName">Full path of the assembly to load</param>
		private void LoadAssembly(string assemblyFileName)
		{
			try
			{
				actions.LoadAssembly( assemblyFileName );
			}
			catch(Exception exception)
			{
				AssemblyLoadFailure( assemblyFileName, exception );			
			}
		}

		/// <summary>
		/// Reload an assembly in response to a change event
		/// </summary>
		/// <param name="assemblyFileName"></param>
		private void ReloadAssembly( string assemblyFileName )
		{
			try
			{
				actions.ReloadAssembly( assemblyFileName );
			}
			catch(Exception exception)
			{
				AssemblyLoadFailure( assemblyFileName, exception );
				RemoveRecentAssembly( assemblyFileName );

				OnSuiteUnloaded();
			}
		}

		private void UnloadAssembly()
		{
			actions.UnloadAssembly();
		}

		private bool IsAssemblyOpen()
		{
			return assemblyFileName != null;
		}

		private bool IsAssemblyFileType( string path )
		{
			string extension = Path.GetExtension( path );
			return extension == ".dll" || extension == ".exe";
		}

		private bool IsValidFileDrop( IDataObject data )
		{
			if ( !data.GetDataPresent( DataFormats.FileDrop ) )
				return false;

			string [] fileNames = data.GetData( DataFormats.FileDrop ) as string [];
				if ( fileNames == null )
					return false;

			return IsAssemblyFileType( fileNames[0] );
		}

		#endregion

		#region Helper methods for modifying the UI display

		/// <summary>
		/// Set a button as the default for the form.
		/// </summary>
		/// <param name="myDefaultBtn">The button to be set as the default</param>
		private void SetDefault(Button myDefaultBtn)
		{
			this.AcceptButton = myDefaultBtn;
		}

		/// <summary>
		/// Disable running of tests
		/// </summary>
		private void DisableRunCommand()
		{
			runButton.Enabled = runMenuItem.Enabled = runCommandEnabled = false;
		}
			
		/// <summary>
		/// Enable running of tests
		/// </summary>
		private void EnableRunCommand()
		{
			runButton.Enabled = runMenuItem.Enabled = runCommandEnabled = true;
		}

		/// <summary>
		/// Load our menu with recently used assemblies.
		/// </summary>
		private void LoadRecentAssemblyMenu() 
		{
			IList assemblies = assemblyUtil.GetAssemblies();
			if (assemblies.Count == 0)
				RecentAssemblies.Enabled = false;
			else 
			{
				RecentAssemblies.Enabled = true;
				RecentAssemblies.MenuItems.Clear();
				int index = 1;
				foreach (string name in assemblies) 
				{
					MenuItem item = new MenuItem(String.Format("{0} {1}", index++, name));
					item.Click += new System.EventHandler(recentFile_clicked);
					this.RecentAssemblies.MenuItems.Add(item);
				}
			}
		}

		/// <summary>
		/// Clear all the display information and disable running
		/// </summary>
		private void ClearUI()
		{
			DisableRunCommand();

			ClearTree();
			ClearSuiteName();
			ClearProgressBar();
			ClearTabs();
			ClearStatusBar();
		}

		/// <summary>
		/// Clear all info from the tree
		/// </summary>
		private void ClearTree()
		{
			testSuiteTreeView.Clear();
		}

		/// <summary>
		/// Clear the current test suite name
		/// </summary>
		private void ClearSuiteName()
		{
			suiteName.Text = null;
		}

		/// <summary>
		/// Clear all test results from the UI.
		/// </summary>
		private void ClearTestResults()
		{
			testSuiteTreeView.ClearResults();

			ClearTabs();
		}

		/// <summary>
		/// Clear the progress bar by intializing it with a default
		/// maximum value - for use when no test is loaded.
		/// </summary>
		private void ClearProgressBar( )
		{
			InitializeProgressBar( 100 );
		}

		/// <summary>
		/// Clear info out of our four tabs and stack trace
		/// </summary>
		private void ClearTabs()
		{
			detailList.Items.Clear();
			stackTrace.Text = "";

			notRunTree.Nodes.Clear();

			stdErrTab.Clear();
			stdOutTab.Clear();
		}
		
		/// <summary>
		/// Clear all info in the status bar
		/// </summary>
		private void ClearStatusBar()
		{
			InitializeStatusBar( 0 );
		}

		/// <summary>
		/// Set the current test suite name
		/// </summary>
		/// <param name="name">The name of the suite</param>
		private void SetSuiteName( string name )
		{
			int val = name.LastIndexOf("\\");
			if(val != -1)
				name = name.Substring(val+1);
			suiteName.Text = name;
		}

		/// <summary>
		/// Initialize the progress bar for a loaded test suite.
		/// </summary>
		/// <param name="testCount">Number of tests in the suite</param>
		private void InitializeProgressBar(int testCount)
		{
			progressBar.ForeColor = Color.Lime;
			progressBar.Value = 0;
			progressBar.Maximum = testCount;
		}

		/// <summary>
		/// Initialize the summary fields in the status bar
		/// </summary>
		/// <param name="testCount">The number of tests in the suite</param>
		private void InitializeStatusBar( int testCount )
		{
			testCaseCount.Text = String.Format("Test cases: {0}", testCount);
			failures.Text = "Failures : 0";
			testsRun.Text = "Tests Run : 0";
			time.Text = "Time : 0";
		}
		
		/// <summary>
		/// Display test results in the UI
		/// </summary>
		/// <param name="results">Test results to be displayed</param>
		private void DisplayResults(TestResult results)
		{
			DetailResults detailResults = new DetailResults(detailList, notRunTree);
			notRunTree.BeginUpdate();
			results.Accept(detailResults);
			notRunTree.EndUpdate();

			ResultSummarizer summarizer = new ResultSummarizer(results);

			int failureCount = summarizer.Failures;

			failures.Text = "Failures : " + failureCount.ToString();
			testsRun.Text = "Tests run : " + summarizer.ResultCount.ToString();
			time.Text = "Time : " + summarizer.Time.ToString();
		}

		#endregion	
	}
}

