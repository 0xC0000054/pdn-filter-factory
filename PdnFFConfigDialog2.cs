using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using PaintDotNet;
using PaintDotNet.Effects;
using PdnFF.Properties;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Globalization;

namespace PdnFF
{
	internal class PdnFFConfigDialog : PaintDotNet.Effects.EffectConfigDialog
	{
		private Button buttonOK;
		private Label Filenametxt;
		private Label Filenamelbl;
		private Button button3;
		private Button Savebtn2;
		private Button Loadbtn2;
		private OpenFileDialog openFileDialog1;
		private SaveFileDialog saveFileDialog1;
		private Label bluelbl;
		private Label alphalbl;
		private Label greenlbl;
		private Label Redlbl;
		private Button default_fltr;
		private FolderBrowserDialog DirBrowserDialog1;
		private BackgroundWorker UpdateFilterListbw;
		private TabPage FilterDirtab;
		private CheckBox subdirSearchcb;
		private Button remdirbtn;
		private ListView DirlistView1;
		private Button adddirbtn;
		private TabPage FilterManagertab;
		private Label treefltrcopytxt;
		private Label treefltrauthtxt;
		private Label treefltrcopylbl;
		private Label treefltrauthlbl;
		private TreeView filtertreeview;
		private Panel filtermgrprogresspanel;
		private Label folderloadcountlbl;
		private Label folderldprolbl;
		private ProgressBar fltrmgrprogress;
		private Label filterlistcnttxt;
		private Label filterlistcntlbl;
		private TabPage FFLtab;
		private Label FFLfltrnumtxt;
		private Label FFLfltrnumlbl;
		private Label fflnametxt;
		private Label FFLnamelbl;
		private Button clearFFLbtn;
		private Button LoadFFLbtn;
		private TabPage editorTab;
		private GroupBox Interfacegb;
		private NumericUpDown ctl5num;
		private NumericUpDown ctl4num;
		private NumericUpDown ctl6num;
		private NumericUpDown ctl7num;
		private NumericUpDown ctl3num;
		private NumericUpDown ctl2num;
		private NumericUpDown ctl1num;
		private TextBox ctl7txt;
		private CheckBox ctl7cb;
		private Label label18;
		private TextBox ctl6txt;
		private CheckBox ctl6cb;
		private TextBox ctl3txt;
		private Label label17;
		private CheckBox ctl3cb;
		private Label label16;
		private TextBox ctl2txt;
		private CheckBox ctl2cb;
		private Label label15;
		private TextBox ctl5txt;
		private CheckBox ctl5cb;
		private Label label14;
		private TextBox ctl1txt;
		private CheckBox ctl1cb;
		private Label label13;
		private TextBox ctl4txt;
		private CheckBox ctl4cb;
		private Label label12;
		private TextBox ctl0txt;
		private CheckBox ctl0cb;
		private Label label11;
		private TextBox map2txt;
		private CheckBox map2cb;
		private Label map2lbl;
		private TextBox map1txt;
		private CheckBox map1cb;
		private Label map1lbl;
		private TextBox map3txt;
		private CheckBox map3cb;
		private Label map3lbl;
		private TextBox map0txt;
		private CheckBox map0cb;
		private Label label10;
		private NumericUpDown ctl0num;
		private Label label1;
		private Label label2;
		private Label label4;
		private Label label5;
		private GroupBox Infogb;
		private Label infofilterlbl;
		private Label infocatlbl;
		private Label infoauthlbl;
		private Label infocopylbl;
		private TextBox CategoryBox;
		private TextBox CopyrightBox;
		private TextBox AuthorBox;
		private TextBox TitleBox;
		private TextBox AlphaBox;
		private TextBox BlueBox;
		private TextBox GreenBox;
		private TextBox RedBox;
		private Button Savebtn;
		private Button button2;
		private Button button1;
		private TabPage controlsTab;
		private Label copylbl;
		private Label authlbl;
		private Label catlbl;
		private Label fltrCopyTxt;
		private Label fltrCatTxt;
		private Panel ctlpanel;
		private Button resetbtn1;
		private Button resetbtn7;
		private Button resetbtn6;
		private Button resetbtn5;
		private Button resetbtn4;
		private Button resetbtn3;
		private Button resetbtn2;
		private Label ctllbl7;
		private Label ctllbl6;
		private Button resetbtn0;
		private Label ctllbl5;
		private TrackBar ctl7;
		private Label ctllbl4;
		private TrackBar ctl6;
		private Label ctllbl3;
		private TrackBar ctl5;
		private Label ctllbl2;
		private TrackBar ctl4;
		private Label ctllbl1;
		private TrackBar ctl3;
		private Label ctllbl0;
		private NumericUpDown ctl7_UpDown;
		private Label map3_lbl;
		private NumericUpDown ctl6_UpDown;
		private Label map2_lbl;
		private NumericUpDown ctl4_UpDown;
		private Label map1_lbl;
		private NumericUpDown ctl5_UpDown;
		private Label map0_lbl;
		private NumericUpDown ctl3_UpDown;
		private NumericUpDown ctl2_UpDown;
		private NumericUpDown ctl1_UpDown;
		private NumericUpDown ctl0_UpDown;
		private TrackBar ctl2;
		private TrackBar ctl1;
		private TrackBar ctl0;
		private TabControl tabControl1;
		private TextBox filterSearchBox;
		private TreeView FFLtreeView1;
		private Label ffltreecopytxt;
		private Label ffltreeauthtxt;
		private Label ffltreecopylbl;
		private Label ffltreeauthlbl;
		private ColumnHeader pathColHeader;
		private Label folderloadnamelbl;
		private Button buildfilterbtn;
		private ImageList imageList1;
		private IContainer components;
		private Label fltrTitletxt;
		private Label fltrTitlelbl;
		private Label fltrAuthorTxt;
		private Button buttonCancel;

		public PdnFFConfigDialog()
		{
			InitializeComponent();

		}
		private static filter_data NewFilterData()
		{
			filter_data d = new filter_data();
			d.Author = d.Category = d.Title = d.Copyright = d.FileName = string.Empty;
			for (int i = 0; i < 4; i++)
			{
				d.MapLabel[i] = string.Empty;
			}
			for (int i = 0; i < 8; i++)
			{
				d.ControlLabel[i] = string.Empty;
			}
			return d;
		}
		protected override void InitialInitToken()
		{
			this.theEffectToken = new PdnFFConfigToken(NewFilterData(),string.Empty,string.Empty,string.Empty);
		}
		private string lastFileName = string.Empty;
		protected override void InitTokenFromDialog()
		{
			((PdnFFConfigToken)theEffectToken).data = data;
			((PdnFFConfigToken)theEffectToken).lastFileName = lastFileName;
			((PdnFFConfigToken)theEffectToken).lastFFL = lastffl;
			((PdnFFConfigToken)theEffectToken).fflOfs = fflofs;
		}

		protected override void InitDialogFromToken(EffectConfigToken effectToken)
		{
			 PdnFFConfigToken token = (PdnFFConfigToken)effectToken;
			 this.data = token.data;
			 this.lastFileName = token.lastFileName;
			 this.lastffl = token.lastFFL;
			 this.fflofs = token.fflOfs;
		}

		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PdnFFConfigDialog));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.bluelbl = new System.Windows.Forms.Label();
            this.alphalbl = new System.Windows.Forms.Label();
            this.greenlbl = new System.Windows.Forms.Label();
            this.Redlbl = new System.Windows.Forms.Label();
            this.Savebtn2 = new System.Windows.Forms.Button();
            this.Loadbtn2 = new System.Windows.Forms.Button();
            this.Filenamelbl = new System.Windows.Forms.Label();
            this.Filenametxt = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.default_fltr = new System.Windows.Forms.Button();
            this.DirBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.UpdateFilterListbw = new System.ComponentModel.BackgroundWorker();
            this.FilterDirtab = new System.Windows.Forms.TabPage();
            this.subdirSearchcb = new System.Windows.Forms.CheckBox();
            this.remdirbtn = new System.Windows.Forms.Button();
            this.DirlistView1 = new System.Windows.Forms.ListView();
            this.pathColHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.adddirbtn = new System.Windows.Forms.Button();
            this.FilterManagertab = new System.Windows.Forms.TabPage();
            this.filterSearchBox = new System.Windows.Forms.TextBox();
            this.treefltrcopytxt = new System.Windows.Forms.Label();
            this.treefltrauthtxt = new System.Windows.Forms.Label();
            this.treefltrcopylbl = new System.Windows.Forms.Label();
            this.treefltrauthlbl = new System.Windows.Forms.Label();
            this.filtertreeview = new System.Windows.Forms.TreeView();
            this.filtermgrprogresspanel = new System.Windows.Forms.Panel();
            this.folderloadnamelbl = new System.Windows.Forms.Label();
            this.folderloadcountlbl = new System.Windows.Forms.Label();
            this.folderldprolbl = new System.Windows.Forms.Label();
            this.fltrmgrprogress = new System.Windows.Forms.ProgressBar();
            this.filterlistcnttxt = new System.Windows.Forms.Label();
            this.filterlistcntlbl = new System.Windows.Forms.Label();
            this.FFLtab = new System.Windows.Forms.TabPage();
            this.ffltreecopytxt = new System.Windows.Forms.Label();
            this.ffltreeauthtxt = new System.Windows.Forms.Label();
            this.ffltreecopylbl = new System.Windows.Forms.Label();
            this.ffltreeauthlbl = new System.Windows.Forms.Label();
            this.FFLtreeView1 = new System.Windows.Forms.TreeView();
            this.FFLfltrnumtxt = new System.Windows.Forms.Label();
            this.FFLfltrnumlbl = new System.Windows.Forms.Label();
            this.fflnametxt = new System.Windows.Forms.Label();
            this.FFLnamelbl = new System.Windows.Forms.Label();
            this.clearFFLbtn = new System.Windows.Forms.Button();
            this.LoadFFLbtn = new System.Windows.Forms.Button();
            this.editorTab = new System.Windows.Forms.TabPage();
            this.Interfacegb = new System.Windows.Forms.GroupBox();
            this.ctl5num = new System.Windows.Forms.NumericUpDown();
            this.ctl4num = new System.Windows.Forms.NumericUpDown();
            this.ctl6num = new System.Windows.Forms.NumericUpDown();
            this.ctl7num = new System.Windows.Forms.NumericUpDown();
            this.ctl3num = new System.Windows.Forms.NumericUpDown();
            this.ctl2num = new System.Windows.Forms.NumericUpDown();
            this.ctl1num = new System.Windows.Forms.NumericUpDown();
            this.ctl7txt = new System.Windows.Forms.TextBox();
            this.ctl7cb = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.ctl6txt = new System.Windows.Forms.TextBox();
            this.ctl6cb = new System.Windows.Forms.CheckBox();
            this.ctl3txt = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.ctl3cb = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.ctl2txt = new System.Windows.Forms.TextBox();
            this.ctl2cb = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.ctl5txt = new System.Windows.Forms.TextBox();
            this.ctl5cb = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.ctl1txt = new System.Windows.Forms.TextBox();
            this.ctl1cb = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.ctl4txt = new System.Windows.Forms.TextBox();
            this.ctl4cb = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.ctl0txt = new System.Windows.Forms.TextBox();
            this.ctl0cb = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.map2txt = new System.Windows.Forms.TextBox();
            this.map2cb = new System.Windows.Forms.CheckBox();
            this.map2lbl = new System.Windows.Forms.Label();
            this.map1txt = new System.Windows.Forms.TextBox();
            this.map1cb = new System.Windows.Forms.CheckBox();
            this.map1lbl = new System.Windows.Forms.Label();
            this.map3txt = new System.Windows.Forms.TextBox();
            this.map3cb = new System.Windows.Forms.CheckBox();
            this.map3lbl = new System.Windows.Forms.Label();
            this.map0txt = new System.Windows.Forms.TextBox();
            this.map0cb = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ctl0num = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Infogb = new System.Windows.Forms.GroupBox();
            this.infofilterlbl = new System.Windows.Forms.Label();
            this.infocatlbl = new System.Windows.Forms.Label();
            this.infoauthlbl = new System.Windows.Forms.Label();
            this.infocopylbl = new System.Windows.Forms.Label();
            this.CategoryBox = new System.Windows.Forms.TextBox();
            this.CopyrightBox = new System.Windows.Forms.TextBox();
            this.AuthorBox = new System.Windows.Forms.TextBox();
            this.TitleBox = new System.Windows.Forms.TextBox();
            this.AlphaBox = new System.Windows.Forms.TextBox();
            this.BlueBox = new System.Windows.Forms.TextBox();
            this.GreenBox = new System.Windows.Forms.TextBox();
            this.RedBox = new System.Windows.Forms.TextBox();
            this.Savebtn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.controlsTab = new System.Windows.Forms.TabPage();
            this.fltrAuthorTxt = new System.Windows.Forms.Label();
            this.fltrTitletxt = new System.Windows.Forms.Label();
            this.fltrTitlelbl = new System.Windows.Forms.Label();
            this.copylbl = new System.Windows.Forms.Label();
            this.authlbl = new System.Windows.Forms.Label();
            this.catlbl = new System.Windows.Forms.Label();
            this.fltrCopyTxt = new System.Windows.Forms.Label();
            this.fltrCatTxt = new System.Windows.Forms.Label();
            this.ctlpanel = new System.Windows.Forms.Panel();
            this.ctllbl2 = new System.Windows.Forms.Label();
            this.resetbtn1 = new System.Windows.Forms.Button();
            this.resetbtn7 = new System.Windows.Forms.Button();
            this.resetbtn6 = new System.Windows.Forms.Button();
            this.resetbtn5 = new System.Windows.Forms.Button();
            this.resetbtn4 = new System.Windows.Forms.Button();
            this.resetbtn3 = new System.Windows.Forms.Button();
            this.resetbtn2 = new System.Windows.Forms.Button();
            this.ctllbl7 = new System.Windows.Forms.Label();
            this.ctllbl6 = new System.Windows.Forms.Label();
            this.resetbtn0 = new System.Windows.Forms.Button();
            this.ctllbl5 = new System.Windows.Forms.Label();
            this.ctl7 = new System.Windows.Forms.TrackBar();
            this.ctllbl4 = new System.Windows.Forms.Label();
            this.ctl6 = new System.Windows.Forms.TrackBar();
            this.ctllbl3 = new System.Windows.Forms.Label();
            this.ctl5 = new System.Windows.Forms.TrackBar();
            this.ctl4 = new System.Windows.Forms.TrackBar();
            this.ctllbl1 = new System.Windows.Forms.Label();
            this.ctl3 = new System.Windows.Forms.TrackBar();
            this.ctllbl0 = new System.Windows.Forms.Label();
            this.ctl7_UpDown = new System.Windows.Forms.NumericUpDown();
            this.map3_lbl = new System.Windows.Forms.Label();
            this.ctl6_UpDown = new System.Windows.Forms.NumericUpDown();
            this.map2_lbl = new System.Windows.Forms.Label();
            this.ctl4_UpDown = new System.Windows.Forms.NumericUpDown();
            this.ctl5_UpDown = new System.Windows.Forms.NumericUpDown();
            this.map0_lbl = new System.Windows.Forms.Label();
            this.ctl3_UpDown = new System.Windows.Forms.NumericUpDown();
            this.ctl2_UpDown = new System.Windows.Forms.NumericUpDown();
            this.ctl1_UpDown = new System.Windows.Forms.NumericUpDown();
            this.ctl0_UpDown = new System.Windows.Forms.NumericUpDown();
            this.ctl2 = new System.Windows.Forms.TrackBar();
            this.ctl1 = new System.Windows.Forms.TrackBar();
            this.ctl0 = new System.Windows.Forms.TrackBar();
            this.map1_lbl = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.buildfilterbtn = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.FilterDirtab.SuspendLayout();
            this.FilterManagertab.SuspendLayout();
            this.filtermgrprogresspanel.SuspendLayout();
            this.FFLtab.SuspendLayout();
            this.editorTab.SuspendLayout();
            this.Interfacegb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctl5num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl4num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl6num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl7num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl3num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl2num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl1num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl0num)).BeginInit();
            this.Infogb.SuspendLayout();
            this.controlsTab.SuspendLayout();
            this.ctlpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctl7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl7_UpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl6_UpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl4_UpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl5_UpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl3_UpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl2_UpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl1_UpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl0_UpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl0)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // bluelbl
            // 
            resources.ApplyResources(this.bluelbl, "bluelbl");
            this.bluelbl.Name = "bluelbl";
            // 
            // alphalbl
            // 
            resources.ApplyResources(this.alphalbl, "alphalbl");
            this.alphalbl.Name = "alphalbl";
            // 
            // greenlbl
            // 
            resources.ApplyResources(this.greenlbl, "greenlbl");
            this.greenlbl.Name = "greenlbl";
            // 
            // Redlbl
            // 
            resources.ApplyResources(this.Redlbl, "Redlbl");
            this.Redlbl.Name = "Redlbl";
            // 
            // Savebtn2
            // 
            resources.ApplyResources(this.Savebtn2, "Savebtn2");
            this.Savebtn2.Name = "Savebtn2";
            this.Savebtn2.UseVisualStyleBackColor = true;
            this.Savebtn2.Click += new System.EventHandler(this.Savebtn_Click);
            // 
            // Loadbtn2
            // 
            resources.ApplyResources(this.Loadbtn2, "Loadbtn2");
            this.Loadbtn2.Name = "Loadbtn2";
            this.Loadbtn2.UseVisualStyleBackColor = true;
            this.Loadbtn2.Click += new System.EventHandler(this.Loadbtn_Click);
            // 
            // Filenamelbl
            // 
            resources.ApplyResources(this.Filenamelbl, "Filenamelbl");
            this.Filenamelbl.Name = "Filenamelbl";
            // 
            // Filenametxt
            // 
            resources.ApplyResources(this.Filenametxt, "Filenametxt");
            this.Filenametxt.Name = "Filenametxt";
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "txt";
            resources.ApplyResources(this.saveFileDialog1, "saveFileDialog1");
            // 
            // default_fltr
            // 
            resources.ApplyResources(this.default_fltr, "default_fltr");
            this.default_fltr.Name = "default_fltr";
            this.default_fltr.UseVisualStyleBackColor = true;
            this.default_fltr.Click += new System.EventHandler(this.default_fltr_Click);
            // 
            // UpdateFilterListbw
            // 
            this.UpdateFilterListbw.WorkerReportsProgress = true;
            this.UpdateFilterListbw.WorkerSupportsCancellation = true;
            this.UpdateFilterListbw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.UpdateFilterListbw_DoWork);
            this.UpdateFilterListbw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.UpdateFilterListbw_ProgressChanged);
            this.UpdateFilterListbw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.UpdateFilterListbw_RunWorkerCompleted);
            // 
            // FilterDirtab
            // 
            this.FilterDirtab.BackColor = System.Drawing.SystemColors.Control;
            this.FilterDirtab.Controls.Add(this.subdirSearchcb);
            this.FilterDirtab.Controls.Add(this.remdirbtn);
            this.FilterDirtab.Controls.Add(this.DirlistView1);
            this.FilterDirtab.Controls.Add(this.adddirbtn);
            resources.ApplyResources(this.FilterDirtab, "FilterDirtab");
            this.FilterDirtab.Name = "FilterDirtab";
            // 
            // subdirSearchcb
            // 
            resources.ApplyResources(this.subdirSearchcb, "subdirSearchcb");
            this.subdirSearchcb.Name = "subdirSearchcb";
            this.subdirSearchcb.UseVisualStyleBackColor = true;
            this.subdirSearchcb.Click += new System.EventHandler(this.subdirSearchcb_CheckedChanged);
            // 
            // remdirbtn
            // 
            resources.ApplyResources(this.remdirbtn, "remdirbtn");
            this.remdirbtn.Name = "remdirbtn";
            this.remdirbtn.UseVisualStyleBackColor = true;
            this.remdirbtn.Click += new System.EventHandler(this.remdirbtn_Click);
            // 
            // DirlistView1
            // 
            this.DirlistView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.pathColHeader});
            this.DirlistView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.DirlistView1.HideSelection = false;
            resources.ApplyResources(this.DirlistView1, "DirlistView1");
            this.DirlistView1.MultiSelect = false;
            this.DirlistView1.Name = "DirlistView1";
            this.DirlistView1.UseCompatibleStateImageBehavior = false;
            this.DirlistView1.View = System.Windows.Forms.View.Details;
            // 
            // pathColHeader
            // 
            resources.ApplyResources(this.pathColHeader, "pathColHeader");
            // 
            // adddirbtn
            // 
            resources.ApplyResources(this.adddirbtn, "adddirbtn");
            this.adddirbtn.Name = "adddirbtn";
            this.adddirbtn.UseVisualStyleBackColor = true;
            this.adddirbtn.Click += new System.EventHandler(this.adddirbtn_Click);
            // 
            // FilterManagertab
            // 
            this.FilterManagertab.BackColor = System.Drawing.SystemColors.Control;
            this.FilterManagertab.Controls.Add(this.filterSearchBox);
            this.FilterManagertab.Controls.Add(this.treefltrcopytxt);
            this.FilterManagertab.Controls.Add(this.treefltrauthtxt);
            this.FilterManagertab.Controls.Add(this.treefltrcopylbl);
            this.FilterManagertab.Controls.Add(this.treefltrauthlbl);
            this.FilterManagertab.Controls.Add(this.filtertreeview);
            this.FilterManagertab.Controls.Add(this.filtermgrprogresspanel);
            this.FilterManagertab.Controls.Add(this.filterlistcnttxt);
            this.FilterManagertab.Controls.Add(this.filterlistcntlbl);
            resources.ApplyResources(this.FilterManagertab, "FilterManagertab");
            this.FilterManagertab.Name = "FilterManagertab";
            // 
            // filterSearchBox
            // 
            resources.ApplyResources(this.filterSearchBox, "filterSearchBox");
            this.filterSearchBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.filterSearchBox.Name = "filterSearchBox";
            this.filterSearchBox.TextChanged += new System.EventHandler(this.filterSearchBox_TextChanged);
            this.filterSearchBox.Enter += new System.EventHandler(this.filterSearchBox_Enter);
            this.filterSearchBox.Leave += new System.EventHandler(this.filterSearchBox_Leave);
            // 
            // treefltrcopytxt
            // 
            resources.ApplyResources(this.treefltrcopytxt, "treefltrcopytxt");
            this.treefltrcopytxt.Name = "treefltrcopytxt";
            // 
            // treefltrauthtxt
            // 
            resources.ApplyResources(this.treefltrauthtxt, "treefltrauthtxt");
            this.treefltrauthtxt.Name = "treefltrauthtxt";
            // 
            // treefltrcopylbl
            // 
            resources.ApplyResources(this.treefltrcopylbl, "treefltrcopylbl");
            this.treefltrcopylbl.Name = "treefltrcopylbl";
            // 
            // treefltrauthlbl
            // 
            resources.ApplyResources(this.treefltrauthlbl, "treefltrauthlbl");
            this.treefltrauthlbl.Name = "treefltrauthlbl";
            // 
            // filtertreeview
            // 
            this.filtertreeview.HideSelection = false;
            resources.ApplyResources(this.filtertreeview, "filtertreeview");
            this.filtertreeview.Name = "filtertreeview";
            this.filtertreeview.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.filtertreeview_AfterSelect);
            // 
            // filtermgrprogresspanel
            // 
            this.filtermgrprogresspanel.Controls.Add(this.folderloadnamelbl);
            this.filtermgrprogresspanel.Controls.Add(this.folderloadcountlbl);
            this.filtermgrprogresspanel.Controls.Add(this.folderldprolbl);
            this.filtermgrprogresspanel.Controls.Add(this.fltrmgrprogress);
            resources.ApplyResources(this.filtermgrprogresspanel, "filtermgrprogresspanel");
            this.filtermgrprogresspanel.Name = "filtermgrprogresspanel";
            // 
            // folderloadnamelbl
            // 
            resources.ApplyResources(this.folderloadnamelbl, "folderloadnamelbl");
            this.folderloadnamelbl.Name = "folderloadnamelbl";
            // 
            // folderloadcountlbl
            // 
            resources.ApplyResources(this.folderloadcountlbl, "folderloadcountlbl");
            this.folderloadcountlbl.Name = "folderloadcountlbl";
            // 
            // folderldprolbl
            // 
            resources.ApplyResources(this.folderldprolbl, "folderldprolbl");
            this.folderldprolbl.Name = "folderldprolbl";
            // 
            // fltrmgrprogress
            // 
            resources.ApplyResources(this.fltrmgrprogress, "fltrmgrprogress");
            this.fltrmgrprogress.Name = "fltrmgrprogress";
            this.fltrmgrprogress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // filterlistcnttxt
            // 
            resources.ApplyResources(this.filterlistcnttxt, "filterlistcnttxt");
            this.filterlistcnttxt.Name = "filterlistcnttxt";
            // 
            // filterlistcntlbl
            // 
            resources.ApplyResources(this.filterlistcntlbl, "filterlistcntlbl");
            this.filterlistcntlbl.Name = "filterlistcntlbl";
            // 
            // FFLtab
            // 
            this.FFLtab.BackColor = System.Drawing.SystemColors.Control;
            this.FFLtab.Controls.Add(this.ffltreecopytxt);
            this.FFLtab.Controls.Add(this.ffltreeauthtxt);
            this.FFLtab.Controls.Add(this.ffltreecopylbl);
            this.FFLtab.Controls.Add(this.ffltreeauthlbl);
            this.FFLtab.Controls.Add(this.FFLtreeView1);
            this.FFLtab.Controls.Add(this.FFLfltrnumtxt);
            this.FFLtab.Controls.Add(this.FFLfltrnumlbl);
            this.FFLtab.Controls.Add(this.fflnametxt);
            this.FFLtab.Controls.Add(this.FFLnamelbl);
            this.FFLtab.Controls.Add(this.clearFFLbtn);
            this.FFLtab.Controls.Add(this.LoadFFLbtn);
            resources.ApplyResources(this.FFLtab, "FFLtab");
            this.FFLtab.Name = "FFLtab";
            // 
            // ffltreecopytxt
            // 
            resources.ApplyResources(this.ffltreecopytxt, "ffltreecopytxt");
            this.ffltreecopytxt.Name = "ffltreecopytxt";
            // 
            // ffltreeauthtxt
            // 
            resources.ApplyResources(this.ffltreeauthtxt, "ffltreeauthtxt");
            this.ffltreeauthtxt.Name = "ffltreeauthtxt";
            // 
            // ffltreecopylbl
            // 
            resources.ApplyResources(this.ffltreecopylbl, "ffltreecopylbl");
            this.ffltreecopylbl.Name = "ffltreecopylbl";
            // 
            // ffltreeauthlbl
            // 
            resources.ApplyResources(this.ffltreeauthlbl, "ffltreeauthlbl");
            this.ffltreeauthlbl.Name = "ffltreeauthlbl";
            // 
            // FFLtreeView1
            // 
            this.FFLtreeView1.HideSelection = false;
            resources.ApplyResources(this.FFLtreeView1, "FFLtreeView1");
            this.FFLtreeView1.Name = "FFLtreeView1";
            this.FFLtreeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.FFLtreeView1_AfterSelect);
            // 
            // FFLfltrnumtxt
            // 
            resources.ApplyResources(this.FFLfltrnumtxt, "FFLfltrnumtxt");
            this.FFLfltrnumtxt.Name = "FFLfltrnumtxt";
            // 
            // FFLfltrnumlbl
            // 
            resources.ApplyResources(this.FFLfltrnumlbl, "FFLfltrnumlbl");
            this.FFLfltrnumlbl.Name = "FFLfltrnumlbl";
            // 
            // fflnametxt
            // 
            resources.ApplyResources(this.fflnametxt, "fflnametxt");
            this.fflnametxt.Name = "fflnametxt";
            // 
            // FFLnamelbl
            // 
            resources.ApplyResources(this.FFLnamelbl, "FFLnamelbl");
            this.FFLnamelbl.Name = "FFLnamelbl";
            // 
            // clearFFLbtn
            // 
            resources.ApplyResources(this.clearFFLbtn, "clearFFLbtn");
            this.clearFFLbtn.Name = "clearFFLbtn";
            this.clearFFLbtn.UseVisualStyleBackColor = true;
            this.clearFFLbtn.Click += new System.EventHandler(this.clearFFLbtn_Click);
            // 
            // LoadFFLbtn
            // 
            resources.ApplyResources(this.LoadFFLbtn, "LoadFFLbtn");
            this.LoadFFLbtn.Name = "LoadFFLbtn";
            this.LoadFFLbtn.UseVisualStyleBackColor = true;
            this.LoadFFLbtn.Click += new System.EventHandler(this.LoadFFLbtn_Click);
            // 
            // editorTab
            // 
            this.editorTab.BackColor = System.Drawing.SystemColors.Control;
            this.editorTab.Controls.Add(this.Interfacegb);
            this.editorTab.Controls.Add(this.label1);
            this.editorTab.Controls.Add(this.label2);
            this.editorTab.Controls.Add(this.label4);
            this.editorTab.Controls.Add(this.label5);
            this.editorTab.Controls.Add(this.Infogb);
            this.editorTab.Controls.Add(this.AlphaBox);
            this.editorTab.Controls.Add(this.BlueBox);
            this.editorTab.Controls.Add(this.GreenBox);
            this.editorTab.Controls.Add(this.RedBox);
            this.editorTab.Controls.Add(this.Savebtn);
            this.editorTab.Controls.Add(this.button2);
            this.editorTab.Controls.Add(this.button1);
            resources.ApplyResources(this.editorTab, "editorTab");
            this.editorTab.Name = "editorTab";
            // 
            // Interfacegb
            // 
            this.Interfacegb.Controls.Add(this.ctl5num);
            this.Interfacegb.Controls.Add(this.ctl4num);
            this.Interfacegb.Controls.Add(this.ctl6num);
            this.Interfacegb.Controls.Add(this.ctl7num);
            this.Interfacegb.Controls.Add(this.ctl3num);
            this.Interfacegb.Controls.Add(this.ctl2num);
            this.Interfacegb.Controls.Add(this.ctl1num);
            this.Interfacegb.Controls.Add(this.ctl7txt);
            this.Interfacegb.Controls.Add(this.ctl7cb);
            this.Interfacegb.Controls.Add(this.label18);
            this.Interfacegb.Controls.Add(this.ctl6txt);
            this.Interfacegb.Controls.Add(this.ctl6cb);
            this.Interfacegb.Controls.Add(this.ctl3txt);
            this.Interfacegb.Controls.Add(this.label17);
            this.Interfacegb.Controls.Add(this.ctl3cb);
            this.Interfacegb.Controls.Add(this.label16);
            this.Interfacegb.Controls.Add(this.ctl2txt);
            this.Interfacegb.Controls.Add(this.ctl2cb);
            this.Interfacegb.Controls.Add(this.label15);
            this.Interfacegb.Controls.Add(this.ctl5txt);
            this.Interfacegb.Controls.Add(this.ctl5cb);
            this.Interfacegb.Controls.Add(this.label14);
            this.Interfacegb.Controls.Add(this.ctl1txt);
            this.Interfacegb.Controls.Add(this.ctl1cb);
            this.Interfacegb.Controls.Add(this.label13);
            this.Interfacegb.Controls.Add(this.ctl4txt);
            this.Interfacegb.Controls.Add(this.ctl4cb);
            this.Interfacegb.Controls.Add(this.label12);
            this.Interfacegb.Controls.Add(this.ctl0txt);
            this.Interfacegb.Controls.Add(this.ctl0cb);
            this.Interfacegb.Controls.Add(this.label11);
            this.Interfacegb.Controls.Add(this.map2txt);
            this.Interfacegb.Controls.Add(this.map2cb);
            this.Interfacegb.Controls.Add(this.map2lbl);
            this.Interfacegb.Controls.Add(this.map1txt);
            this.Interfacegb.Controls.Add(this.map1cb);
            this.Interfacegb.Controls.Add(this.map1lbl);
            this.Interfacegb.Controls.Add(this.map3txt);
            this.Interfacegb.Controls.Add(this.map3cb);
            this.Interfacegb.Controls.Add(this.map3lbl);
            this.Interfacegb.Controls.Add(this.map0txt);
            this.Interfacegb.Controls.Add(this.map0cb);
            this.Interfacegb.Controls.Add(this.label10);
            this.Interfacegb.Controls.Add(this.ctl0num);
            resources.ApplyResources(this.Interfacegb, "Interfacegb");
            this.Interfacegb.Name = "Interfacegb";
            this.Interfacegb.TabStop = false;
            // 
            // ctl5num
            // 
            resources.ApplyResources(this.ctl5num, "ctl5num");
            this.ctl5num.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ctl5num.Name = "ctl5num";
            this.ctl5num.ValueChanged += new System.EventHandler(this.ctlnum_ValueChanged);
            // 
            // ctl4num
            // 
            resources.ApplyResources(this.ctl4num, "ctl4num");
            this.ctl4num.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ctl4num.Name = "ctl4num";
            this.ctl4num.ValueChanged += new System.EventHandler(this.ctlnum_ValueChanged);
            // 
            // ctl6num
            // 
            resources.ApplyResources(this.ctl6num, "ctl6num");
            this.ctl6num.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ctl6num.Name = "ctl6num";
            this.ctl6num.ValueChanged += new System.EventHandler(this.ctlnum_ValueChanged);
            // 
            // ctl7num
            // 
            resources.ApplyResources(this.ctl7num, "ctl7num");
            this.ctl7num.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ctl7num.Name = "ctl7num";
            this.ctl7num.ValueChanged += new System.EventHandler(this.ctlnum_ValueChanged);
            // 
            // ctl3num
            // 
            resources.ApplyResources(this.ctl3num, "ctl3num");
            this.ctl3num.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ctl3num.Name = "ctl3num";
            this.ctl3num.ValueChanged += new System.EventHandler(this.ctlnum_ValueChanged);
            // 
            // ctl2num
            // 
            resources.ApplyResources(this.ctl2num, "ctl2num");
            this.ctl2num.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ctl2num.Name = "ctl2num";
            this.ctl2num.ValueChanged += new System.EventHandler(this.ctlnum_ValueChanged);
            // 
            // ctl1num
            // 
            resources.ApplyResources(this.ctl1num, "ctl1num");
            this.ctl1num.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ctl1num.Name = "ctl1num";
            this.ctl1num.ValueChanged += new System.EventHandler(this.ctlnum_ValueChanged);
            // 
            // ctl7txt
            // 
            resources.ApplyResources(this.ctl7txt, "ctl7txt");
            this.ctl7txt.Name = "ctl7txt";
            this.ctl7txt.TextChanged += new System.EventHandler(this.edittxt_TextChanged);
            // 
            // ctl7cb
            // 
            resources.ApplyResources(this.ctl7cb, "ctl7cb");
            this.ctl7cb.Name = "ctl7cb";
            this.ctl7cb.UseVisualStyleBackColor = true;
            this.ctl7cb.CheckedChanged += new System.EventHandler(this.editcb_CheckedChanged);
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // ctl6txt
            // 
            resources.ApplyResources(this.ctl6txt, "ctl6txt");
            this.ctl6txt.Name = "ctl6txt";
            this.ctl6txt.TextChanged += new System.EventHandler(this.edittxt_TextChanged);
            // 
            // ctl6cb
            // 
            resources.ApplyResources(this.ctl6cb, "ctl6cb");
            this.ctl6cb.Name = "ctl6cb";
            this.ctl6cb.UseVisualStyleBackColor = true;
            this.ctl6cb.CheckedChanged += new System.EventHandler(this.editcb_CheckedChanged);
            // 
            // ctl3txt
            // 
            resources.ApplyResources(this.ctl3txt, "ctl3txt");
            this.ctl3txt.Name = "ctl3txt";
            this.ctl3txt.TextChanged += new System.EventHandler(this.edittxt_TextChanged);
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // ctl3cb
            // 
            resources.ApplyResources(this.ctl3cb, "ctl3cb");
            this.ctl3cb.Name = "ctl3cb";
            this.ctl3cb.UseVisualStyleBackColor = true;
            this.ctl3cb.CheckedChanged += new System.EventHandler(this.editcb_CheckedChanged);
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // ctl2txt
            // 
            resources.ApplyResources(this.ctl2txt, "ctl2txt");
            this.ctl2txt.Name = "ctl2txt";
            this.ctl2txt.TextChanged += new System.EventHandler(this.edittxt_TextChanged);
            // 
            // ctl2cb
            // 
            resources.ApplyResources(this.ctl2cb, "ctl2cb");
            this.ctl2cb.Name = "ctl2cb";
            this.ctl2cb.UseVisualStyleBackColor = true;
            this.ctl2cb.CheckedChanged += new System.EventHandler(this.editcb_CheckedChanged);
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // ctl5txt
            // 
            resources.ApplyResources(this.ctl5txt, "ctl5txt");
            this.ctl5txt.Name = "ctl5txt";
            this.ctl5txt.TextChanged += new System.EventHandler(this.edittxt_TextChanged);
            // 
            // ctl5cb
            // 
            resources.ApplyResources(this.ctl5cb, "ctl5cb");
            this.ctl5cb.Name = "ctl5cb";
            this.ctl5cb.UseVisualStyleBackColor = true;
            this.ctl5cb.CheckedChanged += new System.EventHandler(this.editcb_CheckedChanged);
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // ctl1txt
            // 
            resources.ApplyResources(this.ctl1txt, "ctl1txt");
            this.ctl1txt.Name = "ctl1txt";
            this.ctl1txt.TextChanged += new System.EventHandler(this.edittxt_TextChanged);
            // 
            // ctl1cb
            // 
            resources.ApplyResources(this.ctl1cb, "ctl1cb");
            this.ctl1cb.Name = "ctl1cb";
            this.ctl1cb.UseVisualStyleBackColor = true;
            this.ctl1cb.CheckedChanged += new System.EventHandler(this.editcb_CheckedChanged);
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // ctl4txt
            // 
            resources.ApplyResources(this.ctl4txt, "ctl4txt");
            this.ctl4txt.Name = "ctl4txt";
            this.ctl4txt.TextChanged += new System.EventHandler(this.edittxt_TextChanged);
            // 
            // ctl4cb
            // 
            resources.ApplyResources(this.ctl4cb, "ctl4cb");
            this.ctl4cb.Name = "ctl4cb";
            this.ctl4cb.UseVisualStyleBackColor = true;
            this.ctl4cb.CheckedChanged += new System.EventHandler(this.editcb_CheckedChanged);
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // ctl0txt
            // 
            resources.ApplyResources(this.ctl0txt, "ctl0txt");
            this.ctl0txt.Name = "ctl0txt";
            this.ctl0txt.TextChanged += new System.EventHandler(this.edittxt_TextChanged);
            // 
            // ctl0cb
            // 
            resources.ApplyResources(this.ctl0cb, "ctl0cb");
            this.ctl0cb.Name = "ctl0cb";
            this.ctl0cb.UseVisualStyleBackColor = true;
            this.ctl0cb.CheckedChanged += new System.EventHandler(this.editcb_CheckedChanged);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // map2txt
            // 
            resources.ApplyResources(this.map2txt, "map2txt");
            this.map2txt.Name = "map2txt";
            this.map2txt.TextChanged += new System.EventHandler(this.edittxt_TextChanged);
            // 
            // map2cb
            // 
            resources.ApplyResources(this.map2cb, "map2cb");
            this.map2cb.Name = "map2cb";
            this.map2cb.UseVisualStyleBackColor = true;
            this.map2cb.CheckedChanged += new System.EventHandler(this.editcb_CheckedChanged);
            // 
            // map2lbl
            // 
            resources.ApplyResources(this.map2lbl, "map2lbl");
            this.map2lbl.Name = "map2lbl";
            // 
            // map1txt
            // 
            resources.ApplyResources(this.map1txt, "map1txt");
            this.map1txt.Name = "map1txt";
            this.map1txt.TextChanged += new System.EventHandler(this.edittxt_TextChanged);
            // 
            // map1cb
            // 
            resources.ApplyResources(this.map1cb, "map1cb");
            this.map1cb.Name = "map1cb";
            this.map1cb.UseVisualStyleBackColor = true;
            this.map1cb.CheckedChanged += new System.EventHandler(this.editcb_CheckedChanged);
            // 
            // map1lbl
            // 
            resources.ApplyResources(this.map1lbl, "map1lbl");
            this.map1lbl.Name = "map1lbl";
            // 
            // map3txt
            // 
            resources.ApplyResources(this.map3txt, "map3txt");
            this.map3txt.Name = "map3txt";
            this.map3txt.TextChanged += new System.EventHandler(this.edittxt_TextChanged);
            // 
            // map3cb
            // 
            resources.ApplyResources(this.map3cb, "map3cb");
            this.map3cb.Name = "map3cb";
            this.map3cb.UseVisualStyleBackColor = true;
            this.map3cb.CheckedChanged += new System.EventHandler(this.editcb_CheckedChanged);
            // 
            // map3lbl
            // 
            resources.ApplyResources(this.map3lbl, "map3lbl");
            this.map3lbl.Name = "map3lbl";
            // 
            // map0txt
            // 
            resources.ApplyResources(this.map0txt, "map0txt");
            this.map0txt.Name = "map0txt";
            this.map0txt.TextChanged += new System.EventHandler(this.edittxt_TextChanged);
            // 
            // map0cb
            // 
            resources.ApplyResources(this.map0cb, "map0cb");
            this.map0cb.Name = "map0cb";
            this.map0cb.UseVisualStyleBackColor = true;
            this.map0cb.CheckedChanged += new System.EventHandler(this.editcb_CheckedChanged);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // ctl0num
            // 
            resources.ApplyResources(this.ctl0num, "ctl0num");
            this.ctl0num.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ctl0num.Name = "ctl0num";
            this.ctl0num.ValueChanged += new System.EventHandler(this.ctlnum_ValueChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // Infogb
            // 
            this.Infogb.BackColor = System.Drawing.Color.Transparent;
            this.Infogb.Controls.Add(this.infofilterlbl);
            this.Infogb.Controls.Add(this.infocatlbl);
            this.Infogb.Controls.Add(this.infoauthlbl);
            this.Infogb.Controls.Add(this.infocopylbl);
            this.Infogb.Controls.Add(this.CategoryBox);
            this.Infogb.Controls.Add(this.CopyrightBox);
            this.Infogb.Controls.Add(this.AuthorBox);
            this.Infogb.Controls.Add(this.TitleBox);
            resources.ApplyResources(this.Infogb, "Infogb");
            this.Infogb.Name = "Infogb";
            this.Infogb.TabStop = false;
            // 
            // infofilterlbl
            // 
            resources.ApplyResources(this.infofilterlbl, "infofilterlbl");
            this.infofilterlbl.Name = "infofilterlbl";
            // 
            // infocatlbl
            // 
            resources.ApplyResources(this.infocatlbl, "infocatlbl");
            this.infocatlbl.Name = "infocatlbl";
            // 
            // infoauthlbl
            // 
            resources.ApplyResources(this.infoauthlbl, "infoauthlbl");
            this.infoauthlbl.Name = "infoauthlbl";
            // 
            // infocopylbl
            // 
            resources.ApplyResources(this.infocopylbl, "infocopylbl");
            this.infocopylbl.Name = "infocopylbl";
            // 
            // CategoryBox
            // 
            resources.ApplyResources(this.CategoryBox, "CategoryBox");
            this.CategoryBox.Name = "CategoryBox";
            this.CategoryBox.TextChanged += new System.EventHandler(this.editInfoTxt_TextChanged);
            // 
            // CopyrightBox
            // 
            resources.ApplyResources(this.CopyrightBox, "CopyrightBox");
            this.CopyrightBox.Name = "CopyrightBox";
            this.CopyrightBox.TextChanged += new System.EventHandler(this.editInfoTxt_TextChanged);
            // 
            // AuthorBox
            // 
            resources.ApplyResources(this.AuthorBox, "AuthorBox");
            this.AuthorBox.Name = "AuthorBox";
            this.AuthorBox.TextChanged += new System.EventHandler(this.editInfoTxt_TextChanged);
            // 
            // TitleBox
            // 
            resources.ApplyResources(this.TitleBox, "TitleBox");
            this.TitleBox.Name = "TitleBox";
            this.TitleBox.TextChanged += new System.EventHandler(this.editInfoTxt_TextChanged);
            // 
            // AlphaBox
            // 
            resources.ApplyResources(this.AlphaBox, "AlphaBox");
            this.AlphaBox.Name = "AlphaBox";
            this.AlphaBox.TextChanged += new System.EventHandler(this.SrcTextBoxes_TextChanged);
            this.AlphaBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AlphaBox_KeyDown);
            // 
            // BlueBox
            // 
            resources.ApplyResources(this.BlueBox, "BlueBox");
            this.BlueBox.Name = "BlueBox";
            this.BlueBox.TextChanged += new System.EventHandler(this.SrcTextBoxes_TextChanged);
            this.BlueBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BlueBox_KeyDown);
            // 
            // GreenBox
            // 
            resources.ApplyResources(this.GreenBox, "GreenBox");
            this.GreenBox.Name = "GreenBox";
            this.GreenBox.TextChanged += new System.EventHandler(this.SrcTextBoxes_TextChanged);
            this.GreenBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GreenBox_KeyDown);
            // 
            // RedBox
            // 
            resources.ApplyResources(this.RedBox, "RedBox");
            this.RedBox.Name = "RedBox";
            this.RedBox.TextChanged += new System.EventHandler(this.SrcTextBoxes_TextChanged);
            this.RedBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RedBox_KeyDown);
            // 
            // Savebtn
            // 
            resources.ApplyResources(this.Savebtn, "Savebtn");
            this.Savebtn.Name = "Savebtn";
            this.Savebtn.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // controlsTab
            // 
            this.controlsTab.BackColor = System.Drawing.SystemColors.Control;
            this.controlsTab.Controls.Add(this.fltrAuthorTxt);
            this.controlsTab.Controls.Add(this.fltrTitletxt);
            this.controlsTab.Controls.Add(this.fltrTitlelbl);
            this.controlsTab.Controls.Add(this.copylbl);
            this.controlsTab.Controls.Add(this.authlbl);
            this.controlsTab.Controls.Add(this.catlbl);
            this.controlsTab.Controls.Add(this.fltrCopyTxt);
            this.controlsTab.Controls.Add(this.fltrCatTxt);
            this.controlsTab.Controls.Add(this.ctlpanel);
            resources.ApplyResources(this.controlsTab, "controlsTab");
            this.controlsTab.Name = "controlsTab";
            // 
            // fltrAuthorTxt
            // 
            resources.ApplyResources(this.fltrAuthorTxt, "fltrAuthorTxt");
            this.fltrAuthorTxt.Name = "fltrAuthorTxt";
            // 
            // fltrTitletxt
            // 
            resources.ApplyResources(this.fltrTitletxt, "fltrTitletxt");
            this.fltrTitletxt.Name = "fltrTitletxt";
            // 
            // fltrTitlelbl
            // 
            resources.ApplyResources(this.fltrTitlelbl, "fltrTitlelbl");
            this.fltrTitlelbl.Name = "fltrTitlelbl";
            // 
            // copylbl
            // 
            resources.ApplyResources(this.copylbl, "copylbl");
            this.copylbl.Name = "copylbl";
            // 
            // authlbl
            // 
            resources.ApplyResources(this.authlbl, "authlbl");
            this.authlbl.Name = "authlbl";
            // 
            // catlbl
            // 
            resources.ApplyResources(this.catlbl, "catlbl");
            this.catlbl.Name = "catlbl";
            // 
            // fltrCopyTxt
            // 
            resources.ApplyResources(this.fltrCopyTxt, "fltrCopyTxt");
            this.fltrCopyTxt.Name = "fltrCopyTxt";
            // 
            // fltrCatTxt
            // 
            resources.ApplyResources(this.fltrCatTxt, "fltrCatTxt");
            this.fltrCatTxt.Name = "fltrCatTxt";
            // 
            // ctlpanel
            // 
            this.ctlpanel.BackColor = System.Drawing.SystemColors.Control;
            this.ctlpanel.Controls.Add(this.ctllbl2);
            this.ctlpanel.Controls.Add(this.resetbtn1);
            this.ctlpanel.Controls.Add(this.resetbtn7);
            this.ctlpanel.Controls.Add(this.resetbtn6);
            this.ctlpanel.Controls.Add(this.resetbtn5);
            this.ctlpanel.Controls.Add(this.resetbtn4);
            this.ctlpanel.Controls.Add(this.resetbtn3);
            this.ctlpanel.Controls.Add(this.resetbtn2);
            this.ctlpanel.Controls.Add(this.ctllbl7);
            this.ctlpanel.Controls.Add(this.ctllbl6);
            this.ctlpanel.Controls.Add(this.resetbtn0);
            this.ctlpanel.Controls.Add(this.ctllbl5);
            this.ctlpanel.Controls.Add(this.ctl7);
            this.ctlpanel.Controls.Add(this.ctllbl4);
            this.ctlpanel.Controls.Add(this.ctl6);
            this.ctlpanel.Controls.Add(this.ctllbl3);
            this.ctlpanel.Controls.Add(this.ctl5);
            this.ctlpanel.Controls.Add(this.ctl4);
            this.ctlpanel.Controls.Add(this.ctllbl1);
            this.ctlpanel.Controls.Add(this.ctl3);
            this.ctlpanel.Controls.Add(this.ctllbl0);
            this.ctlpanel.Controls.Add(this.ctl7_UpDown);
            this.ctlpanel.Controls.Add(this.map3_lbl);
            this.ctlpanel.Controls.Add(this.ctl6_UpDown);
            this.ctlpanel.Controls.Add(this.map2_lbl);
            this.ctlpanel.Controls.Add(this.ctl4_UpDown);
            this.ctlpanel.Controls.Add(this.ctl5_UpDown);
            this.ctlpanel.Controls.Add(this.map0_lbl);
            this.ctlpanel.Controls.Add(this.ctl3_UpDown);
            this.ctlpanel.Controls.Add(this.ctl2_UpDown);
            this.ctlpanel.Controls.Add(this.ctl1_UpDown);
            this.ctlpanel.Controls.Add(this.ctl0_UpDown);
            this.ctlpanel.Controls.Add(this.ctl2);
            this.ctlpanel.Controls.Add(this.ctl1);
            this.ctlpanel.Controls.Add(this.ctl0);
            this.ctlpanel.Controls.Add(this.map1_lbl);
            resources.ApplyResources(this.ctlpanel, "ctlpanel");
            this.ctlpanel.Name = "ctlpanel";
            // 
            // ctllbl2
            // 
            resources.ApplyResources(this.ctllbl2, "ctllbl2");
            this.ctllbl2.Name = "ctllbl2";
            // 
            // resetbtn1
            // 
            resources.ApplyResources(this.resetbtn1, "resetbtn1");
            this.resetbtn1.Name = "resetbtn1";
            this.resetbtn1.UseVisualStyleBackColor = true;
            this.resetbtn1.Click += new System.EventHandler(this.resetbtn_Click);
            // 
            // resetbtn7
            // 
            resources.ApplyResources(this.resetbtn7, "resetbtn7");
            this.resetbtn7.Name = "resetbtn7";
            this.resetbtn7.UseVisualStyleBackColor = true;
            this.resetbtn7.Click += new System.EventHandler(this.resetbtn_Click);
            // 
            // resetbtn6
            // 
            resources.ApplyResources(this.resetbtn6, "resetbtn6");
            this.resetbtn6.Name = "resetbtn6";
            this.resetbtn6.UseVisualStyleBackColor = true;
            this.resetbtn6.Click += new System.EventHandler(this.resetbtn_Click);
            // 
            // resetbtn5
            // 
            resources.ApplyResources(this.resetbtn5, "resetbtn5");
            this.resetbtn5.Name = "resetbtn5";
            this.resetbtn5.UseVisualStyleBackColor = true;
            this.resetbtn5.Click += new System.EventHandler(this.resetbtn_Click);
            // 
            // resetbtn4
            // 
            resources.ApplyResources(this.resetbtn4, "resetbtn4");
            this.resetbtn4.Name = "resetbtn4";
            this.resetbtn4.UseVisualStyleBackColor = true;
            this.resetbtn4.Click += new System.EventHandler(this.resetbtn_Click);
            // 
            // resetbtn3
            // 
            resources.ApplyResources(this.resetbtn3, "resetbtn3");
            this.resetbtn3.Name = "resetbtn3";
            this.resetbtn3.UseVisualStyleBackColor = true;
            this.resetbtn3.Click += new System.EventHandler(this.resetbtn_Click);
            // 
            // resetbtn2
            // 
            resources.ApplyResources(this.resetbtn2, "resetbtn2");
            this.resetbtn2.Name = "resetbtn2";
            this.resetbtn2.UseVisualStyleBackColor = true;
            this.resetbtn2.Click += new System.EventHandler(this.resetbtn_Click);
            // 
            // ctllbl7
            // 
            resources.ApplyResources(this.ctllbl7, "ctllbl7");
            this.ctllbl7.Name = "ctllbl7";
            // 
            // ctllbl6
            // 
            resources.ApplyResources(this.ctllbl6, "ctllbl6");
            this.ctllbl6.Name = "ctllbl6";
            // 
            // resetbtn0
            // 
            resources.ApplyResources(this.resetbtn0, "resetbtn0");
            this.resetbtn0.Name = "resetbtn0";
            this.resetbtn0.UseVisualStyleBackColor = true;
            this.resetbtn0.Click += new System.EventHandler(this.resetbtn_Click);
            // 
            // ctllbl5
            // 
            resources.ApplyResources(this.ctllbl5, "ctllbl5");
            this.ctllbl5.Name = "ctllbl5";
            // 
            // ctl7
            // 
            resources.ApplyResources(this.ctl7, "ctl7");
            this.ctl7.Maximum = 255;
            this.ctl7.Name = "ctl7";
            this.ctl7.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ctl7.Scroll += new System.EventHandler(this.ctl7_ValueChanged);
            this.ctl7.ValueChanged += new System.EventHandler(this.ctl7_ValueChanged);
            // 
            // ctllbl4
            // 
            resources.ApplyResources(this.ctllbl4, "ctllbl4");
            this.ctllbl4.Name = "ctllbl4";
            // 
            // ctl6
            // 
            resources.ApplyResources(this.ctl6, "ctl6");
            this.ctl6.Maximum = 255;
            this.ctl6.Name = "ctl6";
            this.ctl6.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ctl6.Scroll += new System.EventHandler(this.ctl6_ValueChanged);
            this.ctl6.ValueChanged += new System.EventHandler(this.ctl6_ValueChanged);
            // 
            // ctllbl3
            // 
            resources.ApplyResources(this.ctllbl3, "ctllbl3");
            this.ctllbl3.Name = "ctllbl3";
            // 
            // ctl5
            // 
            resources.ApplyResources(this.ctl5, "ctl5");
            this.ctl5.Maximum = 255;
            this.ctl5.Name = "ctl5";
            this.ctl5.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ctl5.Scroll += new System.EventHandler(this.ctl5_ValueChanged);
            this.ctl5.ValueChanged += new System.EventHandler(this.ctl5_ValueChanged);
            // 
            // ctl4
            // 
            resources.ApplyResources(this.ctl4, "ctl4");
            this.ctl4.Maximum = 255;
            this.ctl4.Name = "ctl4";
            this.ctl4.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ctl4.Scroll += new System.EventHandler(this.ctl4_ValueChanged);
            this.ctl4.ValueChanged += new System.EventHandler(this.ctl4_ValueChanged);
            // 
            // ctllbl1
            // 
            resources.ApplyResources(this.ctllbl1, "ctllbl1");
            this.ctllbl1.Name = "ctllbl1";
            // 
            // ctl3
            // 
            resources.ApplyResources(this.ctl3, "ctl3");
            this.ctl3.Maximum = 255;
            this.ctl3.Name = "ctl3";
            this.ctl3.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ctl3.Scroll += new System.EventHandler(this.ctl3_ValueChanged);
            this.ctl3.ValueChanged += new System.EventHandler(this.ctl3_ValueChanged);
            // 
            // ctllbl0
            // 
            resources.ApplyResources(this.ctllbl0, "ctllbl0");
            this.ctllbl0.Name = "ctllbl0";
            // 
            // ctl7_UpDown
            // 
            resources.ApplyResources(this.ctl7_UpDown, "ctl7_UpDown");
            this.ctl7_UpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ctl7_UpDown.Name = "ctl7_UpDown";
            this.ctl7_UpDown.ValueChanged += new System.EventHandler(this.ctl7_UpDown_ValueChanged);
            // 
            // map3_lbl
            // 
            resources.ApplyResources(this.map3_lbl, "map3_lbl");
            this.map3_lbl.Name = "map3_lbl";
            // 
            // ctl6_UpDown
            // 
            resources.ApplyResources(this.ctl6_UpDown, "ctl6_UpDown");
            this.ctl6_UpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ctl6_UpDown.Name = "ctl6_UpDown";
            this.ctl6_UpDown.ValueChanged += new System.EventHandler(this.ctl6_UpDown_ValueChanged);
            // 
            // map2_lbl
            // 
            resources.ApplyResources(this.map2_lbl, "map2_lbl");
            this.map2_lbl.Name = "map2_lbl";
            // 
            // ctl4_UpDown
            // 
            resources.ApplyResources(this.ctl4_UpDown, "ctl4_UpDown");
            this.ctl4_UpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ctl4_UpDown.Name = "ctl4_UpDown";
            this.ctl4_UpDown.ValueChanged += new System.EventHandler(this.ctl4_UpDown_ValueChanged);
            // 
            // ctl5_UpDown
            // 
            resources.ApplyResources(this.ctl5_UpDown, "ctl5_UpDown");
            this.ctl5_UpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ctl5_UpDown.Name = "ctl5_UpDown";
            this.ctl5_UpDown.ValueChanged += new System.EventHandler(this.ctl5_UpDown_ValueChanged);
            // 
            // map0_lbl
            // 
            resources.ApplyResources(this.map0_lbl, "map0_lbl");
            this.map0_lbl.Name = "map0_lbl";
            // 
            // ctl3_UpDown
            // 
            resources.ApplyResources(this.ctl3_UpDown, "ctl3_UpDown");
            this.ctl3_UpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ctl3_UpDown.Name = "ctl3_UpDown";
            this.ctl3_UpDown.ValueChanged += new System.EventHandler(this.ctl3_UpDown_ValueChanged);
            // 
            // ctl2_UpDown
            // 
            resources.ApplyResources(this.ctl2_UpDown, "ctl2_UpDown");
            this.ctl2_UpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ctl2_UpDown.Name = "ctl2_UpDown";
            this.ctl2_UpDown.ValueChanged += new System.EventHandler(this.ctl2_UpDown_ValueChanged);
            // 
            // ctl1_UpDown
            // 
            resources.ApplyResources(this.ctl1_UpDown, "ctl1_UpDown");
            this.ctl1_UpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ctl1_UpDown.Name = "ctl1_UpDown";
            this.ctl1_UpDown.ValueChanged += new System.EventHandler(this.ctl1_UpDown_ValueChanged);
            // 
            // ctl0_UpDown
            // 
            resources.ApplyResources(this.ctl0_UpDown, "ctl0_UpDown");
            this.ctl0_UpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ctl0_UpDown.Name = "ctl0_UpDown";
            this.ctl0_UpDown.ValueChanged += new System.EventHandler(this.ctl0_UpDown_ValueChanged);
            // 
            // ctl2
            // 
            resources.ApplyResources(this.ctl2, "ctl2");
            this.ctl2.Maximum = 255;
            this.ctl2.Name = "ctl2";
            this.ctl2.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ctl2.Scroll += new System.EventHandler(this.ctl2_ValueChanged);
            this.ctl2.ValueChanged += new System.EventHandler(this.ctl2_ValueChanged);
            // 
            // ctl1
            // 
            resources.ApplyResources(this.ctl1, "ctl1");
            this.ctl1.Maximum = 255;
            this.ctl1.Name = "ctl1";
            this.ctl1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ctl1.Scroll += new System.EventHandler(this.ctl1_ValueChanged);
            this.ctl1.ValueChanged += new System.EventHandler(this.ctl1_ValueChanged);
            // 
            // ctl0
            // 
            resources.ApplyResources(this.ctl0, "ctl0");
            this.ctl0.Maximum = 255;
            this.ctl0.Name = "ctl0";
            this.ctl0.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ctl0.Scroll += new System.EventHandler(this.ctl0_ValueChanged);
            this.ctl0.ValueChanged += new System.EventHandler(this.ctl0_ValueChanged);
            // 
            // map1_lbl
            // 
            resources.ApplyResources(this.map1_lbl, "map1_lbl");
            this.map1_lbl.Name = "map1_lbl";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.controlsTab);
            this.tabControl1.Controls.Add(this.editorTab);
            this.tabControl1.Controls.Add(this.FFLtab);
            this.tabControl1.Controls.Add(this.FilterManagertab);
            this.tabControl1.Controls.Add(this.FilterDirtab);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // buildfilterbtn
            // 
            resources.ApplyResources(this.buildfilterbtn, "buildfilterbtn");
            this.buildfilterbtn.Name = "buildfilterbtn";
            this.buildfilterbtn.UseVisualStyleBackColor = true;
            this.buildfilterbtn.Click += new System.EventHandler(this.buildfilterbtn_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "VSFolder_open.bmp");
            this.imageList1.Images.SetKeyName(1, "VSFolder_closed.bmp");
            // 
            // PdnFFConfigDialog
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.buildfilterbtn);
            this.Controls.Add(this.default_fltr);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.Savebtn2);
            this.Controls.Add(this.Filenamelbl);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.Loadbtn2);
            this.Controls.Add(this.Filenametxt);
            this.Name = "PdnFFConfigDialog";
            this.Opacity = 0.9D;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PdnFFConfigDialog_FormClosing);
            this.Load += new System.EventHandler(this.PdnFFConfigDialog_Load);
            this.Controls.SetChildIndex(this.Filenametxt, 0);
            this.Controls.SetChildIndex(this.Loadbtn2, 0);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.Controls.SetChildIndex(this.buttonOK, 0);
            this.Controls.SetChildIndex(this.Filenamelbl, 0);
            this.Controls.SetChildIndex(this.Savebtn2, 0);
            this.Controls.SetChildIndex(this.buttonCancel, 0);
            this.Controls.SetChildIndex(this.default_fltr, 0);
            this.Controls.SetChildIndex(this.buildfilterbtn, 0);
            this.FilterDirtab.ResumeLayout(false);
            this.FilterDirtab.PerformLayout();
            this.FilterManagertab.ResumeLayout(false);
            this.FilterManagertab.PerformLayout();
            this.filtermgrprogresspanel.ResumeLayout(false);
            this.filtermgrprogresspanel.PerformLayout();
            this.FFLtab.ResumeLayout(false);
            this.FFLtab.PerformLayout();
            this.editorTab.ResumeLayout(false);
            this.editorTab.PerformLayout();
            this.Interfacegb.ResumeLayout(false);
            this.Interfacegb.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctl5num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl4num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl6num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl7num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl3num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl2num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl1num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl0num)).EndInit();
            this.Infogb.ResumeLayout(false);
            this.Infogb.PerformLayout();
            this.controlsTab.ResumeLayout(false);
            this.controlsTab.PerformLayout();
            this.ctlpanel.ResumeLayout(false);
            this.ctlpanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctl7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl7_UpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl6_UpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl4_UpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl5_UpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl3_UpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl2_UpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl1_UpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl0_UpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl0)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			FinishTokenUpdate();
			DialogResult = DialogResult.OK;
			this.Close();
		}
		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			this.Close();
		}
		int[] resetdata = new int[8];
		private filter_data data = null;
		private void Loadbtn_Click(object sender, System.EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				LoadFilter(openFileDialog1.FileName,false);
			}
		}
		/// <summary>
		/// Reset the Control and the filter Info for the token filterdata
		/// </summary>
		/// <param name="tmpdata">The temp filter data with the filter defaults</param>
		private void ResetTokenDataInfo(filter_data tmpdata)
		{
			for (int i = 0; i < 8; i++)
			{
				// set the reset values but keep the last control values
				resetdata[i] = tmpdata.ControlValue[i]; 
				data.ControlLabel[i] = tmpdata.ControlLabel[i];
				data.ControlEnable[i] = tmpdata.ControlEnable[i];
			}
			for (int i = 0; i < 4; i++)
			{
				data.MapLabel[i] = tmpdata.MapLabel[i];
				data.MapEnable[i] = tmpdata.MapEnable[i];
				data.Source[i] = tmpdata.Source[i];
			}
			data.Author = tmpdata.Author;
			data.Category = tmpdata.Category;
			data.Copyright = tmpdata.Copyright;
			data.Title = tmpdata.Title;

		}
		private void SetFltrInfoLabels(filter_data data)
		{
			fltrCatTxt.Text = data.Category;
			fltrTitletxt.Text = data.Title;
			fltrAuthorTxt.Text = data.Author;
			fltrCopyTxt.Text = data.Copyright;
		}
		
		private void LoadFilter(string FileName, bool uselastvalues)
		{
			try
			{
				if (uselastvalues)
				{                        
					ClearEditControls();
					filter_data tmpdata = new filter_data();
					if (FFLoadSave.LoadFile(FileName, tmpdata))
					{
						this.Filenametxt.Text = Path.GetFileName(FileName);
						ResetTokenDataInfo(tmpdata);
						ResetPopDialog(data);
						SetControls(data); // set the controls from the token data
						SetInfo(data);
						SetFltrInfoLabels(data);

						FinishTokenUpdate();
					}
				}
				else
				{                          
					 
					data = NewFilterData();
					SetControls(data); // set the edit controls to the empty data to force all the checkboxes to be unchecked
					ClearEditControls();     
					if (FFLoadSave.LoadFile(FileName, data))
					{
						this.Filenametxt.Text = Path.GetFileName(FileName);
						this.lastFileName = FileName;
						this.fflofs = string.Empty;
						for (int i = 0; i < 8; i++)
						{
							resetdata[i] = data.ControlValue[i];
						}
						ResetPopDialog(data);
						SetControls(data);
						SetInfo(data);
						SetFltrInfoLabels(data);

						FinishTokenUpdate();
					}
				}

			}
			catch (Exception ex)
			{
#if DEBUG
				MessageBox.Show(this, ex.Message + Environment.NewLine + ex.StackTrace, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
#else
				MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
				
				default_fltr_Click(null, null);
			}
		}
		private ErrorProvider ep = new ErrorProvider();
		/// <summary>
		/// Validates the syntax of the Source code 
		/// </summary>
		/// <param name="src">The Source code to check</param>
		/// <returns>True if valid otherwise false</returns>
		private static bool ValidateApplybtn(string src)
		{
			bool valid = false;
		   
			if (src.Length > 0)
			{
				if (ffparse.ValidateSrc(src) == 1)
				{
					valid = true;
				}
			}
			return valid;
		}

		private void Savebtn_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
				{
					FFLoadSave.SaveFile(saveFileDialog1.FileName, data);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void ctl0_UpDown_ValueChanged(object sender, System.EventArgs e)
		{
			if (this.ctl0_UpDown.Value < this.ctl0_UpDown.Minimum) this.ctl0_UpDown.Value = (int)this.ctl0_UpDown.Minimum;
			if (this.ctl3_UpDown.Value > this.ctl0_UpDown.Maximum) this.ctl0_UpDown.Value = (int)this.ctl0_UpDown.Maximum;
			this.ctl0.Value = (int)this.ctl0_UpDown.Value;
		}

		private void ctl0_ValueChanged(object sender, System.EventArgs e)
		{
			if (this.ctl0.Value < this.ctl0.Minimum) this.ctl0.Value = (int)this.ctl0.Minimum;
			if (this.ctl0.Value > this.ctl0.Maximum) this.ctl0.Value = (int)this.ctl0.Maximum;

			if ((int)this.ctl0_UpDown.Value != this.ctl0.Value)
			{
				this.ctl0_UpDown.Value = this.ctl0.Value;
			}

			if (data.ControlValue[0] != this.ctl0.Value)
			{
				data.ControlValue[0] = this.ctl0.Value;
				FinishTokenUpdate();
			}
		}

		private void ctl1_UpDown_ValueChanged(object sender, System.EventArgs e)
		{
			if (this.ctl1_UpDown.Value < this.ctl1_UpDown.Minimum) this.ctl1_UpDown.Value = (int)this.ctl1_UpDown.Minimum;
			if (this.ctl1_UpDown.Value > this.ctl1_UpDown.Maximum) this.ctl1_UpDown.Value = (int)this.ctl1_UpDown.Maximum;
			this.ctl1.Value = (int)this.ctl1_UpDown.Value;
		}

		private void ctl1_ValueChanged(object sender, System.EventArgs e)
		{
			if (this.ctl1.Value < this.ctl1.Minimum) this.ctl1.Value = (int)this.ctl1.Minimum;
			if (this.ctl1.Value > this.ctl1.Maximum) this.ctl1.Value = (int)this.ctl1.Maximum;
			if ((int)this.ctl1_UpDown.Value != this.ctl1.Value)
			{
				this.ctl1_UpDown.Value = this.ctl1.Value;
			}

			if (data.ControlValue[1] != this.ctl1.Value)
			{
				data.ControlValue[1] = this.ctl1.Value;
				FinishTokenUpdate();
			}
		}
		private void ctl2_UpDown_ValueChanged(object sender, System.EventArgs e)
		{
			if (this.ctl2_UpDown.Value < this.ctl2_UpDown.Minimum) this.ctl2_UpDown.Value = (int)this.ctl2_UpDown.Minimum;
			if (this.ctl2_UpDown.Value > this.ctl2_UpDown.Maximum) this.ctl2_UpDown.Value = (int)this.ctl2_UpDown.Maximum;
			this.ctl2.Value = (int)this.ctl2_UpDown.Value;
		}

		private void ctl2_ValueChanged(object sender, System.EventArgs e)
		{
			if (this.ctl2.Value < this.ctl2.Minimum) this.ctl2.Value = (int)this.ctl2.Minimum;
			if (this.ctl2.Value > this.ctl2.Maximum) this.ctl2.Value = (int)this.ctl2.Maximum;
			if ((int)this.ctl2_UpDown.Value != this.ctl2.Value )
			{
				this.ctl2_UpDown.Value = this.ctl2.Value;
			}

			if (data.ControlValue[2] != this.ctl2.Value)
			{
				data.ControlValue[2] = this.ctl2.Value;
				FinishTokenUpdate();
			}
		}
		private void ctl3_UpDown_ValueChanged(object sender, System.EventArgs e)
		{
			if (this.ctl3_UpDown.Value < this.ctl3_UpDown.Minimum) this.ctl3_UpDown.Value = (int)this.ctl3_UpDown.Minimum;
			if (this.ctl3_UpDown.Value > this.ctl3_UpDown.Maximum) this.ctl3_UpDown.Value = (int)this.ctl3_UpDown.Maximum;
			this.ctl3.Value = (int)this.ctl3_UpDown.Value;
		}

		private void ctl3_ValueChanged(object sender, System.EventArgs e)
		{
			if (this.ctl3.Value < this.ctl3.Minimum) this.ctl3.Value = (int)this.ctl3.Minimum;
			if (this.ctl3.Value > this.ctl3.Maximum) this.ctl3.Value = (int)this.ctl3.Maximum;

			if ((int)this.ctl3_UpDown.Value != this.ctl3.Value)
			{
				this.ctl3_UpDown.Value = this.ctl3.Value;
			}

			if (data.ControlValue[3] != this.ctl3.Value)
			{
				data.ControlValue[3] = this.ctl3.Value;
				FinishTokenUpdate();
			}
		}
		private void ctl4_UpDown_ValueChanged(object sender, System.EventArgs e)
		{
			if (this.ctl4_UpDown.Value < this.ctl4_UpDown.Minimum) this.ctl4_UpDown.Value = (int)this.ctl4_UpDown.Minimum;
			if (this.ctl4_UpDown.Value > this.ctl4_UpDown.Maximum) this.ctl4_UpDown.Value = (int)this.ctl4_UpDown.Maximum;
			this.ctl4.Value = (int)this.ctl4_UpDown.Value;
		}
		
		private void ctl4_ValueChanged(object sender, EventArgs e)
		{
			if (this.ctl4.Value < this.ctl4.Minimum) this.ctl4.Value = (int)this.ctl4.Minimum;
			if (this.ctl4.Value > this.ctl4.Maximum) this.ctl4.Value = (int)this.ctl4.Maximum;
			
			if ((int)this.ctl4_UpDown.Value != this.ctl4.Value)
			{
				this.ctl4_UpDown.Value = this.ctl4.Value;
			}

			if (data.ControlValue[4] != this.ctl4.Value)
			{
				data.ControlValue[4] = this.ctl4.Value;
				FinishTokenUpdate();
			}
		}

		private void ctl5_UpDown_ValueChanged(object sender, System.EventArgs e)
		{
			if (this.ctl5_UpDown.Value < this.ctl5_UpDown.Minimum) this.ctl5_UpDown.Value = (int)this.ctl5_UpDown.Minimum;
			if (this.ctl5_UpDown.Value > this.ctl5_UpDown.Maximum) this.ctl5_UpDown.Value = (int)this.ctl5_UpDown.Maximum;
			this.ctl5.Value = (int)this.ctl5_UpDown.Value;
	   }

		private void ctl5_ValueChanged(object sender, System.EventArgs e)
		{
			if (this.ctl5.Value < this.ctl5.Minimum) this.ctl5.Value = (int)this.ctl5.Minimum;
			if (this.ctl5.Value > this.ctl5.Maximum) this.ctl5.Value = (int)this.ctl5.Maximum;

			if ((int)this.ctl5_UpDown.Value != this.ctl5.Value)
			{
				this.ctl5_UpDown.Value = this.ctl5.Value;
			}

			if (data.ControlValue[5] != this.ctl5.Value)
			{
				data.ControlValue[5] = this.ctl5.Value;
				FinishTokenUpdate();
			}

		}
		private void ctl6_UpDown_ValueChanged(object sender, System.EventArgs e)
		{
			if (this.ctl6_UpDown.Value < this.ctl6_UpDown.Minimum) this.ctl6_UpDown.Value = (int)this.ctl6_UpDown.Minimum;
			if (this.ctl6_UpDown.Value > this.ctl6_UpDown.Maximum) this.ctl6_UpDown.Value = (int)this.ctl6_UpDown.Maximum;
			this.ctl6.Value = (int)this.ctl6_UpDown.Value;
		}

		private void ctl6_ValueChanged(object sender, System.EventArgs e)
		{
			if (this.ctl6.Value < this.ctl6.Minimum) this.ctl6.Value = (int)this.ctl6.Minimum;
			if (this.ctl6.Value > this.ctl6.Maximum) this.ctl6.Value = (int)this.ctl6.Maximum;

			if ((int)this.ctl6_UpDown.Value != this.ctl6.Value)
			{
				this.ctl6_UpDown.Value = this.ctl6.Value;
			}

			if (data.ControlValue[6] != this.ctl6.Value)
			{
				data.ControlValue[6] = this.ctl6.Value;
				FinishTokenUpdate();
			}
		}
		private void ctl7_UpDown_ValueChanged(object sender, System.EventArgs e)
		{
			if (this.ctl7_UpDown.Value < this.ctl7_UpDown.Minimum) this.ctl7_UpDown.Value = (int)this.ctl7_UpDown.Minimum;
			if (this.ctl7_UpDown.Value > this.ctl7_UpDown.Maximum) this.ctl7_UpDown.Value = (int)this.ctl7_UpDown.Maximum;
			this.ctl7.Value = (int)this.ctl7_UpDown.Value;

		}

		private void ctl7_ValueChanged(object sender, System.EventArgs e)
		{
			if (this.ctl7.Value < this.ctl7.Minimum) this.ctl7.Value = (int)this.ctl7.Minimum;
			if (this.ctl7.Value > this.ctl7.Maximum) this.ctl7.Value = (int)this.ctl7.Maximum;

			if ((int)this.ctl7_UpDown.Value != this.ctl7.Value )
			{
				this.ctl7_UpDown.Value = this.ctl7.Value;
			}

			if (data.ControlValue[7] != this.ctl7.Value)
			{
				data.ControlValue[7] = this.ctl7.Value;
				FinishTokenUpdate();
			}
		}

	   
		/// <summary>
		/// Set the visability of the controls tab based on the filter_data PopDialog
		/// </summary>
		/// <param name="data">The filter_data to set</param>
		private void SetControlsTabVisability(filter_data data)
		{
			if (data.PopDialog == 1)
			{
				if (!tabControl1.TabPages.Contains(controlsTab))
				{
					tabControl1.TabPages.Insert(0, controlsTab);
				}
			}
			else
			{
				if (tabControl1.TabPages.Contains(controlsTab))
				{
					tabControl1.TabPages.Remove(controlsTab);
				}
			}
		}
		/// <summary>
		/// Resets the PopDialog function if the filter uses the maps or controls
		/// </summary>
		/// <param name="data">The filter_data to reset</param>
		private static void ResetPopDialog(filter_data data)
		{
			List<bool> ctlused = new List<bool>(8);
			List<bool> mapused = new List<bool>(4);

			for (int i = 0; i < 4; i++)
			{
				bool val = (data.MapEnable[i] == 1);
				mapused.Add(val);
			}

			for (int i = 0; i < 8; i++)
			{
				bool val = (data.ControlEnable[i] == 1);
				ctlused.Add(val);
			}

			if (ctlused.Contains(true) || mapused.Contains(true))
			{
				data.PopDialog = 1;
			}
			else
			{
				data.PopDialog = 0;
			}

		}
		private void SetInfo(filter_data data)
		{
			#region src text
			RedBox.Text = data.Source[0];
			GreenBox.Text = data.Source[1];
			BlueBox.Text = data.Source[2];
			AlphaBox.Text = data.Source[3]; 
			#endregion
			#region filter info
			AuthorBox.Text = data.Author;
			CategoryBox.Text = data.Category;
			CopyrightBox.Text = data.Copyright;
			TitleBox.Text = data.Title;
			if (!string.IsNullOrEmpty(data.FileName))
			{
				Filenametxt.Text = data.FileName;
			}
			#endregion

		}
		/// <summary>
		/// Set the control values
		/// </summary>
		/// <param name="data">The filter_data to set</param>
		/// <param name="reset">Reset the control values to the filter defaults</param>
		private void SetControls(filter_data data)
		{

			#region ctl / map enables

			if (data.MapEnable[0] > 0 && data.ControlEnable[0] == 0 && data.ControlEnable[1] == 0/* && UsesMap(data.Source,0)*/)
			{
				map0_lbl.Visible = true;
				map0_lbl.Text = map0txt.Text = data.MapLabel[0];
				ctl0.Visible = true;
				ctl0_UpDown.Visible = true;
				ctl1.Visible = true;
				ctl1_UpDown.Visible = true;
				ctllbl0.Visible = false;
				map0cb.Checked = true;
				resetbtn0.Visible = true;
				resetbtn1.Visible = true;
			}
			else
			{
				if (map0cb.Checked)
				{
					map0cb.Checked = false;
				}
			   
				if (data.ControlEnable[0] > 0)
				{
					ctl0cb.Checked = true;
					ctl0.Visible = true;
					ctl0_UpDown.Visible = true;
					ctllbl0.Visible = true;
					resetbtn0.Visible = true;
				}
				else
				{
					ctl0cb.Checked = false;
					ctl0.Visible = false;
					ctl0_UpDown.Visible = false;
					ctllbl0.Visible = false;
					resetbtn0.Visible = false;
				}

				if (data.ControlEnable[1] > 0)
				{
					ctl1cb.Checked = true;
					ctl1.Visible = true;
					ctl1_UpDown.Visible = true;
					ctllbl1.Visible = true;
					resetbtn1.Visible = true;
				}
				else
				{
					ctl1cb.Checked = false;
					ctl1.Visible = false;
					ctl1_UpDown.Visible = false;
					ctllbl1.Visible = false;
					resetbtn1.Visible = false;
				}
				this.ctllbl0.Text = this.ctl0txt.Text = data.ControlLabel[0];
				this.ctllbl1.Text = this.ctl1txt.Text = data.ControlLabel[1];
			}
			if (data.MapEnable[1] > 0 && data.ControlEnable[2] == 0 && data.ControlEnable[3] == 0/* && UsesMap(data.Source,1)*/)
			{
				map1_lbl.Visible = true;
				map1_lbl.Text = data.MapLabel[1];
				ctl2.Visible = true;
				ctl2_UpDown.Visible = true;
				ctl3.Visible = true;
				ctl3_UpDown.Visible = true;
				ctllbl2.Visible = false;
				ctllbl3.Visible = false;
				map1txt.Text = data.MapLabel[1];
				map1cb.Checked = true;
				resetbtn2.Visible = true;
				resetbtn3.Visible = true;
			}
			else
			{
				if (map1cb.Checked)
				{
					map1cb.Checked = false;
				}

				if (data.ControlEnable[2] > 0)
				{
					ctl2cb.Checked = true;
					ctl2.Visible = true;
					ctl2_UpDown.Visible = true;
					ctllbl2.Visible = true;
					resetbtn2.Visible = true;
				}
				else
				{
					ctl2cb.Checked = false;
					ctl2.Visible = false;
					ctl2_UpDown.Visible = false;
					ctllbl2.Visible = false;
					resetbtn2.Visible = false;
				}

				if (data.ControlEnable[3] > 0)
				{
					ctl3cb.Checked = true;
					ctl3.Visible = true;
					ctl3_UpDown.Visible = true;
					ctllbl3.Visible = true;
					resetbtn3.Visible = true;
				}
				else
				{
					ctl3cb.Checked = false;
					ctl3.Visible = false;
					ctl3_UpDown.Visible = false;
					ctllbl3.Visible = false;
					resetbtn3.Visible = false;
				}
				this.ctllbl2.Text = this.ctl2txt.Text = data.ControlLabel[2];
				this.ctllbl3.Text = this.ctl3txt.Text = data.ControlLabel[3];
			}
			if (data.MapEnable[2] > 0 && data.ControlEnable[4] == 0 && data.ControlEnable[5] == 0 /*&& UsesMap(data.Source, 2)*/)
			{
				map2_lbl.Visible = true;
				map2_lbl.Text = data.MapLabel[2];
				ctl4.Visible = true;
				ctl4_UpDown.Visible = true;
				ctl5.Visible = true;
				ctl5_UpDown.Visible = true;
				ctllbl4.Visible = false;
				ctllbl5.Visible = false;
				map2txt.Text = data.MapLabel[2];
				map2cb.Checked = true;
				resetbtn4.Visible = true;
				resetbtn5.Visible = true;
			}
			else
			{
				if (map2cb.Checked)
				{
					map2cb.Checked = false;
				}
			   
				if (data.ControlEnable[4] > 0)
				{
					ctl4cb.Checked = true;
					ctl4.Visible = true;
					ctl4_UpDown.Visible = true;
					ctllbl4.Visible = true;
					resetbtn4.Visible = true;
				}
				else
				{
					ctl4cb.Checked = false;
					ctl4.Visible = false;
					ctl4_UpDown.Visible = false;
					ctllbl4.Visible = false;
					resetbtn4.Visible = false;
				}

				if (data.ControlEnable[5] > 0)
				{
					ctl5cb.Checked = true;
					ctl5.Visible = true;
					ctl5_UpDown.Visible = true;
					ctllbl5.Visible = true;
					resetbtn5.Visible = true;
				}
				else
				{
					ctl5cb.Checked = false;
					ctl5.Visible = false;
					ctl5_UpDown.Visible = false;
					ctllbl5.Visible = false;
					resetbtn5.Visible = false;
				} 
				this.ctllbl4.Text = this.ctl4txt.Text = data.ControlLabel[4];
				this.ctllbl5.Text = this.ctl5txt.Text = data.ControlLabel[5];
			}

			if (data.MapEnable[3] > 0 && data.ControlEnable[6] == 0 && data.ControlEnable[7] == 0 /*&& UsesMap(data.Source, 3)*/)
			{
				map3_lbl.Visible = true;
				map3_lbl.Text = data.MapLabel[3];
				ctl6.Visible = true;
				ctl6_UpDown.Visible = true;
				ctl7.Visible = true;
				ctl7_UpDown.Visible = true;
				ctllbl6.Visible = false;
				ctllbl7.Visible = false;
				map3txt.Text = data.MapLabel[3];
				map3cb.Checked = true;
				resetbtn6.Visible = true;
				resetbtn7.Visible = true;
			}
			else
			{
				if (map3cb.Checked)
				{
					map3cb.Checked = false;
				}
			   
				if (data.ControlEnable[6] > 0)
				{
					ctl6cb.Checked = true;
					ctl6.Visible = true;
					ctl6_UpDown.Visible = true;
					ctllbl6.Visible = true;
					resetbtn6.Visible = true;
				}
				else
				{
					ctl6cb.Checked = false;
					ctl6.Visible = false;
					ctl6_UpDown.Visible = false;
					ctllbl6.Visible = false;
					resetbtn6.Visible = false;
				}

				if (data.ControlEnable[7] > 0)
				{
					ctl7cb.Checked = true;
					ctl7.Visible = true;
					ctl7_UpDown.Visible = true;
					ctllbl7.Visible = true;
					resetbtn7.Visible = true;
				}
				else
				{
					ctl7cb.Checked = false;
					ctl7.Visible = false;
					ctl7_UpDown.Visible = false;
					ctllbl7.Visible = false;
					resetbtn7.Visible = false;
				}
				this.ctllbl6.Text = this.ctl6txt.Text = data.ControlLabel[6];
				this.ctllbl7.Text = this.ctl7txt.Text = data.ControlLabel[7];
			}
		
			#endregion 
			
			#region ctl values
			// set the default editor control values 
			this.ctl0num.Value = this.ctl0.Value = data.ControlValue[0];
			this.ctl1num.Value = this.ctl1.Value = data.ControlValue[1];
			this.ctl2num.Value = this.ctl2.Value = data.ControlValue[2];
			this.ctl3num.Value = this.ctl3.Value = data.ControlValue[3];
			this.ctl4num.Value = this.ctl4.Value = data.ControlValue[4];
			this.ctl5num.Value = this.ctl5.Value = data.ControlValue[5];
			this.ctl6num.Value = this.ctl6.Value = data.ControlValue[6];
			this.ctl7num.Value = this.ctl7.Value = data.ControlValue[7];
			#endregion
		}
		private void resetbtn_Click(object sender, EventArgs e)
		{
			if (data != null)
			{
				Button b = sender as Button;
				int num = int.Parse(b.Name.Substring(8, 1), CultureInfo.InvariantCulture); // get the number from the control name

				switch (num)
				{
					case 0:
						ctl0.Value = resetdata[0];
						break;
					case 1:
						ctl1.Value = resetdata[1];
						break;
					case 2:
						ctl2.Value = resetdata[2];
						break;
					case 3:
						ctl3.Value = resetdata[3];
						break;
					case 4:
						ctl4.Value = resetdata[4];
						break;
					case 5:
						ctl5.Value = resetdata[5];
						break;
					case 6:
						ctl6.Value = resetdata[6];
						break;
					case 7:
						ctl7.Value = resetdata[7];
						break;
				}
			   

			   
			}
		}
		private void SetTokenData()
		{
			if (((PdnFFConfigToken)theEffectToken).data != null && this.data == null)
			{
			   this.data = ((PdnFFConfigToken)theEffectToken).data;
			}
		}
		private void default_fltr_Click(object sender, EventArgs e)
		{
			if (data == null)
			{
				data = new filter_data();
			}
			ClearLastFilters();
			FFLoadSave.DefaultFilter(data);
			SetControls(data);
			SetInfo(data);
			SetFltrInfoLabels(data);
			FinishTokenUpdate();
		}
		/// <summary>
		/// Clears the last loaded Filters
		/// </summary>
		private void ClearLastFilters()
		{
			Filenametxt.Text = this.lastFileName = string.Empty;
			clearFFLbtn_Click(null, null);
		}
	   
		private void LoadSettings()
		{
			if (settings == null)
			{   
				string dir = this.Services.GetService<PaintDotNet.AppModel.IAppInfoService>().UserDataDirectory;

				try
				{
					if (!Directory.Exists(dir))
					{
						Directory.CreateDirectory(dir);
					}
				}
				catch (Exception)
				{
					// Ignore it 
				}

				string path = Path.Combine(dir, @"PdnFF.xml");
				if (File.Exists(path))
				{
					settings = new Settings(path);
				}
				else
				{
					using (Stream res = Assembly.GetAssembly(typeof(PdnFF_Effect)).GetManifestResourceStream(@"PdnFF.PdnFF.xml"))
					{
						byte[] bytes = new byte[res.Length];
						int numBytesToRead = (int)res.Length;
						int numBytesRead = 0;
						while (numBytesToRead > 0)
						{
							// Read may return anything from 0 to numBytesToRead.
							int n = res.Read(bytes, numBytesRead, numBytesToRead);
							// The end of the file is reached.
							if (n == 0)
								break;
							numBytesRead += n;
							numBytesToRead -= n;
						}
						File.WriteAllBytes(path, bytes);
					}

					settings = new Settings(path);

				}
			}
		}
	   
		private void PdnFFConfigDialog_Load(object sender, EventArgs e)
		{
			SetTokenData();
			LoadSettings();
			subdirSearchcb.Checked = bool.Parse(settings.GetSetting("SearchSubDir", bool.TrueString).Trim());
			string dirs = settings.GetSetting("SearchDirs", string.Empty).Trim();
			if (!string.IsNullOrEmpty(dirs))
			{
				string[] dirlist = dirs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
				foreach (string dir in dirlist)
				{
					if (Directory.Exists(dir))
					{
						DirlistView1.Items.Add(dir);
					}
				}
				UpdateFilterList();
			}

			if (!string.IsNullOrEmpty(this.lastffl) && !string.IsNullOrEmpty(this.fflofs))
			{
				LoadFFL(this.lastffl, this.fflofs);
				FinishTokenUpdate();
			}
			else if (!string.IsNullOrEmpty(this.lastFileName))
			{
				LoadFilter(this.lastFileName, true);
			}
			else
			{
				default_fltr_Click(null, null);
			}
		}
		
		private void SrcTextBoxes_TextChanged(object sender, EventArgs e)
		{
			
			TextBox tb = sender as TextBox;
			string name = tb.Name.Substring(0, 1);

			if (!string.IsNullOrEmpty(tb.Text))
			{
				int ch = 0;
				switch (name)
				{
					case "R":
						ch = 0;
						break;
					case "G":
						ch = 1;
						break;
					case "B":
						ch = 2;
						break;
					case "A":
						ch = 3;
						break;
				}
				if (ValidateApplybtn(tb.Text))
				{
					ep.ContainerControl = this;
					ep.SetError((TextBox)sender, string.Empty);
					data.Source[ch] = tb.Text;
					FinishTokenUpdate();
				}
				else
				{
					ep.ContainerControl = this;
					ep.SetError((TextBox)sender, Resources.ConfigDialog_FormulaSyntaxError_Text);
				}
			}
		}
		private TextBox GetErrorTextbox(int ch)
		{
			if (ch == 0)
			{
				return RedBox;
			}
			else if (ch == 1)
			{
				return GreenBox;
			}
			else if (ch == 2)
			{
				return BlueBox;
			}
			else if (ch == 3)
			{
				return AlphaBox;
			}
			else
			{
				throw new ArgumentOutOfRangeException("ch", Resources.ChannelValuesOutofRangeError);
			}

		}
		private string lastffl = string.Empty;
		private string fflofs = string.Empty;
		/// <summary>
		/// Loads a FFL library
		/// </summary>
		/// <param name="FileName">The FileName to load</param>
		/// <param name="index">The item name of the filter to select</param>
		private void LoadFFL(string FileName, string itemname)
		{ 
			if (FFLtreeView1.Nodes.Count > 0)
			{
				FFLtreeView1.Nodes.Clear();
			}

			List<TreeNode> nodes = new List<TreeNode>();
			if (FFLoadSave.LoadFFL(FileName, nodes))
			{
				int count = 0;

				FFLtreeView1.BeginUpdate();
				FFLtreeView1.TreeViewNodeSorter = null;
				foreach (var item in nodes)
				{
					count += item.Nodes.Count;
					FFLtreeView1.Nodes.Add(item);
				}
				FFLtreeView1.TreeViewNodeSorter = new TreeNodeItemComparer();
				FFLtreeView1.EndUpdate();

				lastffl = FileName;
				fflnametxt.Text = Path.GetFileName(this.lastffl);


				FFLfltrnumtxt.Text = count.ToString(CultureInfo.CurrentCulture);
				if (!string.IsNullOrEmpty(itemname) && FFLtreeView1.Nodes.ContainsKey(itemname))
				{
					int index = FFLtreeView1.Nodes.IndexOfKey(itemname);
					long ofs = (long)FFLtreeView1.Nodes[index].Tag;
					GetFilterfromFFLOffset(ofs, true);
				}
				
				SetFltrInfoLabels(data);
			}

		}
		private void LoadFFLbtn_Click(object sender, EventArgs e)
		{
			
			try 
			{
				OpenFileDialog ffldlg = new OpenFileDialog { Multiselect = false, Filter = Resources.ConfigDialog_LoadFFLDialog_Filter };
				if (ffldlg.ShowDialog() == DialogResult.OK)
				{
					LoadFFL(ffldlg.FileName, string.Empty);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}
		private void GetFilterfromFFLOffset(long offset, bool uselastvalues)
		{ 
			using (BinaryReader br = new BinaryReader(new FileStream(lastffl, FileMode.Open, FileAccess.Read),Encoding.Default))
			{
				if (uselastvalues)
				{
					filter_data tmpdata = new filter_data();
					if (FFLoadSave.GetFilterfromFFL(br, offset, tmpdata))
					{
						for (int i = 0; i < 8; i++)
						{
							resetdata[i] = tmpdata.ControlValue[i];
						}
						ResetTokenDataInfo(tmpdata);
						SetControls(data); // set the controls from the token data
						SetInfo(data);
					}
				}
				else
				{
					data = NewFilterData();
					if (FFLoadSave.GetFilterfromFFL(br, offset, data))
					{
						for (int i = 0; i < 8; i++)
						{
							resetdata[i] = data.ControlValue[i];
						}
						SetControls(data);
						SetInfo(data);
						SetFltrInfoLabels(data);

					}
				}
			}
		}
		private void FFLtreeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (FFLtreeView1.SelectedNode.Tag != null)
			{
				TreeNode lvi = FFLtreeView1.SelectedNode;
				fflofs = lvi.Name; // the item's FileName in the FFL
				long offset = (long)lvi.Tag;
				GetFilterfromFFLOffset(offset, false);
				
				ffltreeauthtxt.Text = data.Author;
				ffltreecopytxt.Text = data.Copyright;
			}
		}
		private void SetEditControls(filter_data data, int index, bool setctlnum, bool ismaptxt)
		{
			#region map0 / ctl 0 / ctl 1
			if (map0cb.Checked && !ctl0cb.Checked && !ctl1cb.Checked)
			{
				ctl0txt.Enabled = false;
				ctl0cb.Enabled = false;
				ctl1txt.Enabled = false;
				ctl1num.Enabled = false;
				ctl0num.Enabled = false;
				ctl1cb.Enabled = false;

				ctl0.Visible = true;
				ctl0_UpDown.Visible = true;
				ctl1.Visible = true;
				ctl1_UpDown.Visible = true;
				ctllbl0.Visible = false;
				resetbtn0.Visible = true;
				resetbtn1.Visible = true;

				map0_lbl.Visible = true;
				data.MapEnable[0] = 1;
				if (!string.IsNullOrEmpty(map0txt.Text))
				{
				   map0_lbl.Text = data.MapLabel[0] = map0txt.Text;
				}

			}
			else
			{
				/*data.MapEnable[0] = 0;*/
				map0_lbl.Visible = false;
				if (string.IsNullOrEmpty(map0txt.Text))
				{
				   map0txt.Text = data.MapLabel[0];
				}
				if (!ismaptxt)
				{
					ctl0txt.Enabled = true;
					ctl0cb.Enabled = true;
					ctl0num.Enabled = true;
					ctl1txt.Enabled = true;
					ctl1cb.Enabled = true;
					ctl1num.Enabled = true;

					if (!ctl0cb.Checked && !ctl1cb.Checked)
					{
						map0cb.Enabled = true;
					}
					else
					{
						map0cb.Enabled = false;
					}

					if (index == 0)
					{
						if (ctl0cb.Checked)
						{
							ctl0.Visible = true;
							ctl0_UpDown.Visible = true;
							ctllbl0.Visible = true;
							resetbtn0.Visible = true;
							data.ControlEnable[0] = 1;
							if (setctlnum)
							{
								ctl0_UpDown.Value = data.ControlValue[0] = (int)ctl0num.Value;
							}

							if (!string.IsNullOrEmpty(ctl0txt.Text))
							{
								ctllbl0.Text = data.ControlLabel[0] = ctl0txt.Text;
							}


						}
						else
						{
							ctl0.Visible = false;
							ctl0_UpDown.Visible = false;
							ctllbl0.Visible = false;
							resetbtn0.Visible = false;
							if (string.IsNullOrEmpty(data.ControlLabel[0]))
							{
								data.ControlLabel[0] = "Control 0:";
							}
							data.ControlEnable[0] = 0;
							data.ControlValue[0] = 0;
						}
					}
					if (index == 1)
					{
						if (ctl1cb.Checked)
						{
							ctl1.Visible = true;
							ctl1_UpDown.Visible = true;
							ctllbl1.Visible = true;
							resetbtn1.Visible = true;
							data.ControlEnable[1] = 1;
							if (setctlnum)
							{
								ctl1_UpDown.Value = data.ControlValue[1] = (int)ctl1num.Value;
							}
							if (!string.IsNullOrEmpty(ctl1txt.Text))
							{
								ctllbl1.Text = data.ControlLabel[1] = ctl1txt.Text;
							}

						}
						else
						{
							ctl1.Visible = false;
							ctl1_UpDown.Visible = false;
							ctllbl1.Visible = false;
							resetbtn1.Visible = false;
							if (string.IsNullOrEmpty(data.ControlLabel[1]))
							{
								data.ControlLabel[1] = "Control 1:";
							}
							data.ControlEnable[1] = 0;
							data.ControlValue[1] = 0;
						}
					}
				}
			}
			#endregion
			#region map1 /ctl 2 /ctl 3
			if (map1cb.Checked && !ctl2cb.Checked && !ctl3cb.Checked)
			{
				ctl2txt.Enabled = false;
				ctl3txt.Enabled = false;
				ctl2cb.Enabled = false;
				ctl3num.Enabled = false;
				ctl2num.Enabled = false;
				ctl3cb.Enabled = false;

				ctl2.Visible = true;
				ctl2_UpDown.Visible = true;
				ctl3.Visible = true;
				ctl3_UpDown.Visible = true;
				ctllbl2.Visible = false;
				ctllbl3.Visible = false;
				map1cb.Checked = true;
				resetbtn2.Visible = true;
				resetbtn3.Visible = true;

				map1_lbl.Visible = true;
				data.MapEnable[1] = 1;
				if (!string.IsNullOrEmpty(map1txt.Text))
				{
				   map1_lbl.Text = data.MapLabel[1] = map1txt.Text;
				}

			}
			else
			{
				map1_lbl.Visible = false;
				/*data.MapEnable[1] = 0;*/
		   
				if (string.IsNullOrEmpty(map1txt.Text))
				{
					map1txt.Text = data.MapLabel[1];
				}

				if (!ismaptxt)
				{
					if (!ctl2cb.Checked && !ctl3cb.Checked)
					{
						map1cb.Enabled = true;
					}
					else
					{
						map1cb.Enabled = false;
					}
					ctl2txt.Enabled = true;
					ctl2cb.Enabled = true;
					ctl2num.Enabled = true;
					ctl3txt.Enabled = true;
					ctl3num.Enabled = true;
					ctl3cb.Enabled = true;

					if (index == 2)
					{
						if (ctl2cb.Checked)
						{
							ctl2.Visible = true;
							ctl2_UpDown.Visible = true;
							ctllbl2.Visible = true;
							resetbtn2.Visible = true;

							data.ControlEnable[2] = 1;
							if (setctlnum)
							{
								ctl2_UpDown.Value = data.ControlValue[2] = (int)ctl2num.Value;
							}
							if (!string.IsNullOrEmpty(ctl2txt.Text))
							{
								ctllbl2.Text = data.ControlLabel[2] = ctl2txt.Text;
							}

						}
						else
						{
							ctl2.Visible = false;
							ctl2_UpDown.Visible = false;
							ctllbl2.Visible = false;
							resetbtn2.Visible = false;
							if (string.IsNullOrEmpty(data.ControlLabel[2]))
							{
								data.ControlLabel[2] = "Control 2:";
							}
							data.ControlEnable[2] = 0;
							data.ControlValue[2] = 0;
						}
					}
					if (index == 3)
					{
						if (ctl3cb.Checked)
						{
							ctl3.Visible = true;
							ctl3_UpDown.Visible = true;
							ctllbl3.Visible = true;
							resetbtn3.Visible = true;
							data.ControlEnable[3] = 1;
							if (setctlnum)
							{
								ctl3_UpDown.Value = data.ControlValue[3] = (int)ctl3num.Value;
							}
							if (!string.IsNullOrEmpty(ctl3txt.Text))
							{
								ctllbl3.Text = data.ControlLabel[3] = ctl3txt.Text;
							}

						}
						else
						{
							ctl3.Visible = false;
							ctl3_UpDown.Visible = false;
							ctllbl3.Visible = false;
							resetbtn3.Visible = false;
							if (string.IsNullOrEmpty(data.ControlLabel[3]))
							{
								data.ControlLabel[3] = "Control 3:";
							}
							data.ControlEnable[3] = 0;
							data.ControlValue[3] = 0;
						}
					} 
				}
			}
			#endregion
			#region map 2 /ctl 4 /ctl 5
			if (map2cb.Checked && !ctl4cb.Checked && !ctl5cb.Checked)
			{
				ctl4txt.Enabled = false;
				ctl4cb.Enabled = false;
				ctl5num.Enabled = false;
				ctl4num.Enabled = false;
				ctl5cb.Enabled = false;

				ctl4.Visible = true;
				ctl4_UpDown.Visible = true;
				ctl5.Visible = true;
				ctl5_UpDown.Visible = true;
				ctllbl4.Visible = false;
				ctllbl5.Visible = false;
				map2cb.Checked = true;
				resetbtn4.Visible = true;
				resetbtn5.Visible = true;

				map2_lbl.Visible = true;
				data.MapEnable[2] = 1;
				if (!string.IsNullOrEmpty(map2txt.Text))
				{
				   map2_lbl.Text = data.MapLabel[2] = map2txt.Text;
				}
			}
			else
			{
				map2_lbl.Visible = false;
				/* data.MapEnable[2] = 0;*/
			   
				if (string.IsNullOrEmpty(map2txt.Text))
				{
					map2txt.Text = data.MapLabel[2];
				}
				if (!ismaptxt)
				{
					if (!ctl4cb.Checked && !ctl5cb.Checked)
					{
						map2cb.Enabled = true;
					}
					else
					{
						map2cb.Enabled = false;
					}
					ctl4txt.Enabled = true;
					ctl4cb.Enabled = true;
					ctl4num.Enabled = true;
					ctl5num.Enabled = true;
					ctl5cb.Enabled = true;
					ctl5txt.Enabled = true;
					if (index == 4)
					{
						if (ctl4cb.Checked)
						{
							ctl4.Visible = true;
							ctl4_UpDown.Visible = true;
							ctllbl4.Visible = true;
							resetbtn4.Visible = true;
							data.ControlEnable[4] = 1;
							if (setctlnum)
							{
								ctl4_UpDown.Value = data.ControlValue[4] = (int)ctl4num.Value;
							}
							if (!string.IsNullOrEmpty(ctl4txt.Text))
							{
								ctllbl4.Text = data.ControlLabel[4] = ctl4txt.Text;
							}

						}
						else
						{
							ctl4.Visible = false;
							ctl4_UpDown.Visible = false;
							ctllbl4.Visible = false;
							resetbtn4.Visible = false;
							if (string.IsNullOrEmpty(data.ControlLabel[4]))
							{
								data.ControlLabel[4] = "Control 4:";
							}
							data.ControlEnable[4] = 0;
							data.ControlValue[4] = 0;
						}
					}

					if (index == 5)
					{
						if (ctl5cb.Checked)
						{
							ctl5.Visible = true;
							ctl5_UpDown.Visible = true;
							ctllbl5.Visible = true;
							resetbtn5.Visible = true;
							data.ControlEnable[5] = 1;
							if (setctlnum)
							{
								ctl5_UpDown.Value = data.ControlValue[5] = (int)ctl5num.Value;
							}
							if (!string.IsNullOrEmpty(ctl5txt.Text))
							{
								ctllbl5.Text = data.ControlLabel[5] = ctl5txt.Text;
							}

						}
						else
						{
							ctl5.Visible = false;
							ctl5_UpDown.Visible = false;
							ctllbl5.Visible = false;
							resetbtn5.Visible = false;
							if (string.IsNullOrEmpty(data.ControlLabel[5]))
							{
								data.ControlLabel[5] = "Control 5:";
							}
							data.ControlEnable[5] = 0;
							data.ControlValue[5] = 0;
						}
					} 
				}

			}
			#endregion
			#region map 3 /ctl 6 / ctl 7
			if (map3cb.Checked && !ctl6cb.Checked && !ctl7cb.Checked)
			{
				ctl6txt.Enabled = false;
				ctl6cb.Enabled = false;
				ctl7num.Enabled = false;
				ctl6num.Enabled = false;
				ctl7cb.Enabled = false;

				ctl6.Visible = true;
				ctl6_UpDown.Visible = true;
				ctl7.Visible = true;
				ctl7_UpDown.Visible = true;
				ctllbl6.Visible = false;
				ctllbl7.Visible = false;
				map3cb.Checked = true;
				resetbtn6.Visible = true;
				resetbtn7.Visible = true;

				map3_lbl.Visible = true;
				data.MapEnable[3] = 1;
				if (!string.IsNullOrEmpty(map3txt.Text))
				{
				   map3_lbl.Text = data.MapLabel[3] = map3txt.Text;
				}

			}
			else
			{
				map3_lbl.Visible = false;
				
				// data.MapEnable[3] = 0;
				if (string.IsNullOrEmpty(map3txt.Text))
				{
					map3txt.Text = data.MapLabel[3];
				}

				if (!ismaptxt)
				{
					if (!ctl6cb.Checked && !ctl7cb.Checked)
					{
						map3cb.Enabled = true;
					}
					else
					{
						map3cb.Enabled = false;
					}
					ctl6txt.Enabled = true;
					ctl6cb.Enabled = true;
					ctl6num.Enabled = true;
					ctl7num.Enabled = true;
					ctl7cb.Enabled = true;
					if (index == 6)
					{
						if (ctl6cb.Checked)
						{
							ctl6.Visible = true;
							ctl6_UpDown.Visible = true;
							ctllbl6.Visible = true;
							resetbtn6.Visible = true;

							data.ControlEnable[6] = 1;
							if (setctlnum)
							{
								ctl6_UpDown.Value = data.ControlValue[6] = (int)ctl6num.Value;
							}
							if (!string.IsNullOrEmpty(ctl6txt.Text))
							{
								ctllbl6.Text = data.ControlLabel[6] = ctl6txt.Text;
							}

						}
						else
						{
							ctl6.Visible = false;
							ctl6_UpDown.Visible = false;
							ctllbl6.Visible = false;
							resetbtn6.Visible = false;
							if (string.IsNullOrEmpty(data.ControlLabel[6]))
							{
								data.ControlLabel[6] = "Control 6:";
							}
							data.ControlEnable[6] = 0;
							data.ControlValue[6] = 0;
						}
					}
					if (index == 7)
					{
						if (ctl7cb.Checked)
						{
							ctl7.Visible = true;
							ctl7_UpDown.Visible = true;
							ctllbl7.Visible = true;
							resetbtn7.Visible = true;
							data.ControlEnable[7] = 1;
							if (setctlnum)
							{
								ctl7_UpDown.Value = data.ControlValue[7] = (int)ctl7num.Value;
							}
							if (!string.IsNullOrEmpty(ctl7txt.Text))
							{
								ctllbl7.Text = data.ControlLabel[7] = ctl7txt.Text;
							}

						}
						else
						{
							ctl7.Visible = false;
							ctl7_UpDown.Visible = false;
							ctllbl7.Visible = false;
							resetbtn7.Visible = false;
							if (string.IsNullOrEmpty(data.ControlLabel[7]))
							{
								data.ControlLabel[7] = "Control 7:";
							}
							data.ControlEnable[7] = 0;
							data.ControlValue[7] = 0;
						}
					}
					
				}
			}
			#endregion
		}

		private void editcb_CheckedChanged(object sender, EventArgs e)
		{
			Control cb = sender as Control;
			int ctlnum = int.Parse(cb.Name.Substring(3, 1), CultureInfo.InvariantCulture);
			SetEditControls(data, ctlnum, false, false);
		}

		private void edittxt_TextChanged(object sender, EventArgs e)
		{
			Control cb = sender as Control;
			int ctlnum = int.Parse(cb.Name.Substring(3, 1), CultureInfo.InvariantCulture);
			bool ismaptxt = cb.Name.ToUpperInvariant().Substring(0,3).Equals("MAP");
			SetEditControls(data,ctlnum,false, ismaptxt);
		}

		private void ctlnum_ValueChanged(object sender, EventArgs e)
		{
			Control cb = sender as Control;
			int ctlnum = int.Parse(cb.Name.Substring(3, 1), CultureInfo.InvariantCulture);
			SetEditControls(data,ctlnum,true, false);
		}

		private void editInfoTxt_TextChanged(object sender, EventArgs e)
		{
			TextBox tb = sender as TextBox;

			if (!string.IsNullOrEmpty(tb.Text))
			{
				switch (tb.Name)
				{
					case "AuthorBox":

							if (tb.Text != data.Author)
							{
								data.Author = tb.Text;
							}

						break;
					case "CategoryBox":
					   
							if (tb.Text != data.Category)
							{
								data.Category = tb.Text;
							}

						break;
					case "CopyrightBox":

							if (tb.Text != data.Copyright)
							{
								data.Copyright = tb.Text;
							}

						break;
					case "TitleBox":

							if (tb.Text != data.Title)
							{
								data.Title = tb.Text;
							}

						break;
				}
			}
		   
		}

		private Settings settings;
		private void UpdateSearchList()
		{
			if (settings != null)
			{
				string dirs = string.Empty;
				for (int i = 0; i < DirlistView1.Items.Count; i++)
				{
					string val = DirlistView1.Items[i].Text;

					if (i != DirlistView1.Items.Count - 1)
					{
						val += ",";
					}
					dirs += val;
				}
				settings.PutSetting("SearchDirs", dirs);
			}
		}
		/// <summary>
		/// The Parameter struct for the UpdateFilterList Background Worker
		/// </summary>
		private struct UpdateFilterListParm
		{
			/// <summary>
			/// The list of Search Directories
			/// </summary>
			public string[] dirlist;
			/// <summary>
			/// The output array of TreeNodes
			/// </summary>
			public TreeNode[] itemarr;
			/// <summary>
			/// The SearchOption to use on the directories
			/// </summary>
			public SearchOption options;
			/// <summary>
			/// The number of items in the list
			/// </summary>
			public int itemcount;
		}

		/// <summary>
		/// Gets the filter items list using the UpdateFilterListbw Background Worker.
		/// </summary>
		/// <param name="parm">The output UpdateFilterListParm data.</param>
		/// <param name="worker">The  UpdateFilterListbw Background Worker.</param>
		/// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
		private static void GetFilterItemsList(UpdateFilterListParm parm, BackgroundWorker worker, DoWorkEventArgs e)
		{
			Dictionary<string, TreeNode> nodes = new Dictionary<string, TreeNode>();

			int count = 0;
			//Debug.WriteLine("GetFilterItemsList");
			for (int i = 0; i < parm.dirlist.Length; i++)
			{
				string path = parm.dirlist[i];
				if (Directory.Exists(path))
				{                    

					DirectoryInfo dir = new DirectoryInfo(path);
					FileInfo[] files = dir.GetFiles("*.8bf", parm.options);
				   // Debug.WriteLine(string.Format("File Count = {0}", files.Length.ToString()));
					
					worker.ReportProgress(i, dir.Name);

					foreach (FileInfo fi in files)
					{
						if (worker.CancellationPending)
						{
							e.Cancel = true;
							return;
						}
						if (fi.Exists)
						{
							filter_data fd = new filter_data();
							if (FFLoadSave.LoadFile(fi.FullName, fd))
							{
								string[] subtext = new string[2] { fd.Author, fd.Copyright };

								if (nodes.ContainsKey(fd.Category))
								{
									TreeNode node = nodes[fd.Category];

									TreeNode subnode = new TreeNode(fd.Title) { Name = fi.FullName, Tag = subtext }; // Title
									node.Nodes.Add(subnode);
								}
								else
								{
									TreeNode node = new TreeNode(fd.Category);
									TreeNode subnode = new TreeNode(fd.Title) { Name = fi.FullName, Tag = subtext }; // Title
									node.Nodes.Add(subnode);

									nodes.Add(fd.Category, node);
								}

								count++;
								//Debug.WriteLine(string.Format("Item name = {0}, Count = {1}", fi.Name, items.Count.ToString()));
							}

						}
					}
				}

			}

#if DEBUG
			Debug.WriteLine(string.Format("node count = {0}", nodes.Values.Count.ToString())); 
#endif
			parm.itemarr = new TreeNode[nodes.Values.Count];
			nodes.Values.CopyTo(parm.itemarr, 0); 
			parm.itemcount = count;
			Array.Sort(parm.itemarr, new TreeNodeItemComparer()); // sort the treenode items 

			e.Result = parm;
			
		}

		/// <summary>
		/// Initilizes the UpdateFilterListbw Background Worker
		/// </summary>
		private void InitilizeUpdateFilterListbw()
		{
			if (UpdateFilterListbw == null)
			{
				UpdateFilterListbw = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
				UpdateFilterListbw.DoWork += new DoWorkEventHandler(UpdateFilterListbw_DoWork);
				UpdateFilterListbw.ProgressChanged += new ProgressChangedEventHandler(UpdateFilterListbw_ProgressChanged);
				UpdateFilterListbw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(UpdateFilterListbw_RunWorkerCompleted);
			}
		}

		/// <summary>
		/// Update the Filter Manager filterlist
		/// </summary>
		private void UpdateFilterList()
		{
			if (DirlistView1.Items.Count > 0)
			{
				InitilizeUpdateFilterListbw();
	#if DEBUG
				Debug.WriteLine(string.Format("UpdateFilterListbw.IsBusy = {0}", UpdateFilterListbw.IsBusy.ToString()));
	#endif           
				if (!UpdateFilterListbw.IsBusy)
				{
					UpdateFilterListParm uflp = new UpdateFilterListParm();
					uflp.options = subdirSearchcb.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
					uflp.dirlist = new string[DirlistView1.Items.Count];
					for (int i = 0; i < DirlistView1.Items.Count; i++)
					{
						uflp.dirlist[i] = DirlistView1.Items[i].Text;
					}
					// Debug.WriteLine(string.Format("UpdateFilterList isbackground = {0}", Thread.CurrentThread.IsBackground.ToString()));
					fltrmgrprogress.Maximum = uflp.dirlist.Length;
					fltrmgrprogress.Step = 1;
					filtermgrprogresspanel.Visible = true;
					folderloadcountlbl.Text = string.Format("({0} of {1})", "0", DirlistView1.Items.Count.ToString());
					filtertreeview.Nodes.Clear();
					filterlistcnttxt.Text = string.Empty;
					UpdateFilterListbw_Done = false;

					UpdateFilterListbw.RunWorkerAsync(uflp);
				}
			}
		  
		}

		private void adddirbtn_Click(object sender, EventArgs e)
		{
			if (DirBrowserDialog1.ShowDialog() == DialogResult.OK)
			{
				if (Directory.Exists(DirBrowserDialog1.SelectedPath))
				{
					if (!string.IsNullOrEmpty(Filenametxt.Text))
					{
						default_fltr_Click(null, null); // if a filter is loaded reset it to the default
					}
					DirlistView1.Items.Add(DirBrowserDialog1.SelectedPath);
					UpdateSearchList();
					UpdateFilterList();
				}
			}
		}

		private void remdirbtn_Click(object sender, EventArgs e)
		{
			if (DirlistView1.SelectedItems.Count > 0)
			{
				if (!string.IsNullOrEmpty(Filenametxt.Text))
				{
					default_fltr_Click(null, null); 
				}
				DirlistView1.Items.Remove(DirlistView1.SelectedItems[0]);
				UpdateSearchList();
				UpdateFilterList();
			}
		}
	   
		private void clearFFLbtn_Click(object sender, EventArgs e)
		{
			if (FFLtreeView1.Nodes.Count > 0)
			{
				lastffl = string.Empty;
				fflofs = string.Empty;
				Filenametxt.Text = data.FileName = string.Empty;
				fflnametxt.Text = FFLfltrnumtxt.Text = string.Empty;
				FFLtreeView1.Nodes.Clear();
			}
		}

		private void subdirSearchcb_CheckedChanged(object sender, EventArgs e)
		{
			if (settings != null)
			{
				settings.PutSetting("SearchSubDirs", subdirSearchcb.Checked.ToString());
				UpdateFilterList();
			}
		}
		/// <summary>
		/// Clear all the Edit label textboxes
		/// </summary>
		private void ClearEditControls()
		{ 
			ctl0txt.Text = ctl1txt.Text = ctl2txt.Text = ctl3txt.Text = ctl4txt.Text = 
			ctl5txt.Text = ctl6txt.Text = ctl7txt.Text = string.Empty;
		}

		private void UpdateFilterListbw_DoWork(object sender, DoWorkEventArgs e)
		{
			UpdateFilterListParm uflp = (UpdateFilterListParm)e.Argument;
			//Debug.WriteLine(uflp.resetselection.ToString());
			GetFilterItemsList(uflp, UpdateFilterListbw, e);
		}
		private Dictionary<TreeNode, string> FiltertreeviewItems = null; // used for the filter search list
		private void UpdateFilterListbw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				string message = e.Error.Message;
				MessageBox.Show(message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				if (!e.Cancelled) // has the worker been canceled
				{
					UpdateFilterListParm parm = (UpdateFilterListParm)e.Result;

					FiltertreeviewItems = new Dictionary<TreeNode, string>();
					for (int i = 0; i < parm.itemarr.Length; i++)
					{
						TreeNode basenode = parm.itemarr[i];
						foreach (TreeNode item in basenode.Nodes)
						{
							FiltertreeviewItems.Add(item, basenode.Text);
						}
					}

					filtertreeview.BeginUpdate();
					filtertreeview.Nodes.AddRange(parm.itemarr);
					filtertreeview.EndUpdate();

					filterlistcnttxt.Text = parm.itemcount.ToString(CultureInfo.CurrentCulture);

					if (fltrmgrprogress.Value == fltrmgrprogress.Maximum)
					{
						fltrmgrprogress.Value = 0;
						//Debug.WriteLine(string.Format("Thread isbackground = {0}", Thread.CurrentThread.IsBackground.ToString()));
						filtermgrprogresspanel.Visible = false;
					} 
				}
#if DEBUG
				else
				{

					Debug.WriteLine("canceled");

				}
#endif

			}
			this.UpdateFilterListbw.Dispose();
			this.UpdateFilterListbw = null;

			this.UpdateFilterListbw_Done = true;

			if (FormClosePending)
			{
				this.Close(); // close the form
			}
		}

		private void UpdateFilterListbw_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			//Debug.WriteLine(string.Format("progresschanged isbackground = {0}", Thread.CurrentThread.IsBackground.ToString())); 

			folderloadcountlbl.Text = string.Format(CultureInfo.InvariantCulture, "({0} of {1})", (e.ProgressPercentage + 1).ToString(CultureInfo.InvariantCulture), DirlistView1.Items.Count.ToString(CultureInfo.InvariantCulture));
			folderloadnamelbl.Text = string.Format("({0})", e.UserState.ToString());
			fltrmgrprogress.PerformStep();
		}

		private bool UpdateFilterListbw_Done;
		private bool FormClosePending;
		private void PdnFFConfigDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!UpdateFilterListbw_Done && DirlistView1.Items.Count > 0) // don't hold the form open if there are no search Dirs
			{
				UpdateFilterListbw.CancelAsync();
				e.Cancel = true;
				FormClosePending = true;
			}
		}
		/// <summary>
		/// Filters the filtertreeview Items by the specified text
		/// </summary>
		/// <param name="filtertext">The keyword text to filter by</param>
		private void FilterTreeView(string filtertext)
		{ 
			if (FiltertreeviewItems.Count  > 0)
			{
				Dictionary<string, TreeNode> nodes = new Dictionary<string, TreeNode>();
				foreach (KeyValuePair<TreeNode,string> item in FiltertreeviewItems)             
				{
					TreeNode child = item.Key;
					string Title = child.Text;
					if ((string.IsNullOrEmpty(filtertext)) || Title.ToLowerInvariant().Contains(filtertext.ToLowerInvariant()))
					{
						if (nodes.ContainsKey(item.Value))
						{
							TreeNode node = nodes[item.Value];
							TreeNode subnode = new TreeNode(Title) { Name = child.Name, Tag = child.Tag }; // Title
							node.Nodes.Add(subnode);
						}
						else
						{                   
							TreeNode node = new TreeNode(item.Value);
							TreeNode subnode = new TreeNode(Title) { Name = child.Name, Tag = child.Tag }; // Title
							node.Nodes.Add(subnode);

							nodes.Add(item.Value, node);
						}

					}
				}
				
				filtertreeview.BeginUpdate();    
				filtertreeview.Nodes.Clear();
				filtertreeview.TreeViewNodeSorter = null;
				foreach (var item in nodes)
				{
					int index = filtertreeview.Nodes.Add(item.Value);
					
					if (!string.IsNullOrEmpty(filtertext))
					{ 
						filtertreeview.Nodes[index].Expand();
					}
				}
				filtertreeview.TreeViewNodeSorter = new TreeNodeItemComparer();
				filtertreeview.EndUpdate();
				
			}
		}

		private void filtertreeview_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (filtertreeview.SelectedNode.Tag != null)
			{
				string[] subtext = (string[])filtertreeview.SelectedNode.Tag;
				treefltrauthtxt.Text = subtext[0];
				treefltrcopytxt.Text = subtext[1];

				LoadFilter(filtertreeview.SelectedNode.Name, false);
			}
			else
			{
				treefltrauthtxt.Text = string.Empty;
				treefltrcopytxt.Text = string.Empty;
			}
		}

		private void filterSearchBox_TextChanged(object sender, EventArgs e)
		{
			string filtertext = filterSearchBox.Focused ? filterSearchBox.Text : string.Empty; 
			FilterTreeView(filtertext); // pass an empty string if the textbox is not focused 
		}
		private void filterSearchBox_Enter(object sender, EventArgs e)
		{
			if (filterSearchBox.Text == Resources.ConfigDialog_FilterSearchBox_BackText)
			{
				filterSearchBox.Text = string.Empty;
				filterSearchBox.Font = new Font(filterSearchBox.Font, FontStyle.Regular);
				filterSearchBox.ForeColor = SystemColors.WindowText;
			}
		}

		private void filterSearchBox_Leave(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(filterSearchBox.Text))
			{
				filterSearchBox.Text = Resources.ConfigDialog_FilterSearchBox_BackText;
				filterSearchBox.Font = new Font(filterSearchBox.Font, FontStyle.Italic);
				filterSearchBox.ForeColor = SystemColors.GrayText;
			}
		}

		private static string BuildFilterData(filter_data data)
		{
			string ret = string.Empty;

			using (StringWriter sw = new StringWriter(CultureInfo.InvariantCulture))
			{
				sw.WriteLine("data.Author = \"{0}\";", data.Author);
				sw.WriteLine("data.Category =  \"{0}\";", data.Category);
				sw.WriteLine("data.Title =  \"{0}\";", data.Title);
				sw.WriteLine("data.Copyright = \"{0}\";", data.Copyright);
				sw.WriteLine("data.MapEnable = new int[4] {0},{1},{2},{3}", new object[] { "{ " + data.MapEnable[0].ToString(), data.MapEnable[1].ToString(), data.MapEnable[2].ToString(), data.MapEnable[3].ToString() + "};" });
				sw.WriteLine("data.MapLabel = new string[4] {0}\",\"{1}\",\"{2}\",\"{3}", "{ \"" + data.MapLabel[0], data.MapLabel[1], data.MapLabel[2], data.MapLabel[3] + "\"" + "};");

				sw.WriteLine("data.ControlEnable = new int[8] {0},{1},{2},{3},{4},{5},{6},{7} ", new object[] { "{ " + data.ControlEnable[0].ToString(), data.ControlEnable[1].ToString(), data.ControlEnable[2].ToString(), data.ControlEnable[3].ToString(), data.ControlEnable[4].ToString(), data.ControlEnable[5].ToString(), data.ControlEnable[6].ToString(), data.ControlEnable[7].ToString() + "};" });
				sw.WriteLine("data.ControlLabel = new string[8] {0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}", new object[] { "{ \"" + data.ControlLabel[0], data.ControlLabel[1], data.ControlLabel[2], data.ControlLabel[3], data.ControlLabel[4], data.ControlLabel[5], data.ControlLabel[6], data.ControlLabel[7] + "\"" + "};" });
				sw.WriteLine("data.ControlValue = new int[8] {0},{1},{2},{3},{4},{5},{6},{7} ", new object[] { "{ " + data.ControlValue[0].ToString(), data.ControlValue[1].ToString(), data.ControlValue[2].ToString(), data.ControlValue[3].ToString(), data.ControlValue[4].ToString(), data.ControlValue[5].ToString(), data.ControlValue[6].ToString(), data.ControlValue[7].ToString() + "};" });
				sw.WriteLine("data.Source = new string[4]  {0}\",\"{1}\",\"{2}\",\"{3}", "{ \"" + data.Source[0], data.Source[1], data.Source[2], data.Source[3] + "\"" + "}" + ";");
				sw.WriteLine("data.PopDialog = {0};", data.PopDialog.ToString());
				sw.WriteLine("filterDataset = true;");

				ret = sw.ToString();
			}
			return ret;
		}

		private static string GetSubmenuCategory(filter_data data)
		{
			string cat = string.Empty;

			switch (data.Category.ToLowerInvariant())
			{
				case "artistic":
					cat = "SubmenuNames.Artistic";
					break;
				case "blurs":
					cat = "SubmenuNames.Blurs";
					break;
				case "distort":
					cat = "SubmenuNames.Distort";
					break;
				case "noise":
					cat = "SubmenuNames.Noise";
					break;
				case "photo":
					cat = "SubmenuNames.Photo";
					break;
				case "render":
					cat = "SubmenuNames.Render";
					break;
				case "stylize":
					cat = "SubmenuNames.Stylize";
					break;

				default:
					cat = "\"" + data.Category + "\"";
					break;
			}

			return cat;
		}

		/// <summary>
		/// Builds the effect class.
		/// </summary>
		/// <param name="classname">The class name of the Effect.</param>
		/// <param name="FileName">The FileName of the output file.</param>
		/// <param name="data">The filter_data of the filter to build.</param>
		/// <returns>The generated Effect class Source code</returns>
		private static string BuildEffectClass(string classname, string FileName,  filter_data data)
		{
			string ret = string.Empty;
			using (StringWriter sw = new StringWriter(CultureInfo.InvariantCulture))
			{
				// usings
				sw.WriteLine("using System;");
				sw.WriteLine("using System.Drawing;");
				sw.WriteLine("using System.Runtime.InteropServices;");
				sw.WriteLine("using System.Reflection;");
				sw.WriteLine("using PaintDotNet;");
				sw.WriteLine("using PaintDotNet.Effects;");
				sw.WriteLine("using FFEffect;\n");
				// AssemblyInfo
				sw.WriteLine("[assembly: AssemblyTitle(\"" + FileName + " Plugin (Compiled by PdnFF)\")]");
				sw.WriteLine("[assembly: AssemblyCompany(\""+ data.Author +"\")]");
				sw.WriteLine("[assembly: AssemblyProduct(\"" + FileName + " Plugin (Compiled by PdnFF)\")]");
				sw.WriteLine("[assembly: AssemblyCopyright(\"" + data.Copyright + "\")]");
				// namespace and class 
				sw.WriteLine("namespace FFEffect_" + classname+ " \n{\n");
				sw.WriteLine("public class " + classname + " : PaintDotNet.Effects.Effect \n{\n");

				sw.WriteLine("filter_data data = new filter_data();");
				// SetFilterData
				sw.WriteLine("private bool filterDataset;");
				sw.WriteLine("private void SetFilterData() \n { \n");
				sw.WriteLine(BuildFilterData(data) + "\n }");
				sw.WriteLine("Common com = new Common();");
				// Constructor
				string Category = GetSubmenuCategory(data);
				sw.WriteLine(string.Format(CultureInfo.InvariantCulture, "public {0}() : base(\"{1}\", null, {2}, EffectFlags.{3} | EffectFlags.SingleThreaded)", classname, data.Title, Category, data.PopDialog == 1 ? "Configurable" : "None"));
				sw.WriteLine("{}");
				//OnDispose
				sw.WriteLine("protected override void OnDispose(bool disposing)\n{");
				sw.WriteLine("if (disposing) \n {");
				sw.WriteLine("com.Dispose();\n } \n base.OnDispose(disposing); \n }");
				// CreateConfigDialog
				if (data.PopDialog == 1)
				{
					sw.WriteLine(" public override EffectConfigDialog CreateConfigDialog() \n{\n");
					sw.WriteLine("if (!filterDataset) \n { \n SetFilterData(); \n } \n");
					sw.WriteLine("com.SetupFilterEnviromentData(base.EnvironmentParameters.SourceSurface);");
					sw.WriteLine("return new FFEffectConfigDialog(data); \n }\n");
				}
				// OnSetRenderInfo
				sw.WriteLine("protected override void OnSetRenderInfo(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs)\n { \n");
				sw.WriteLine("if (!filterDataset) \n { \n SetFilterData(); \n } \n");

				if (data.PopDialog == 1) // is the Effect configurable
				{
					sw.WriteLine("FFEffectConfigToken token = (FFEffectConfigToken)parameters;");
					sw.WriteLine("\n com.SetupFilterData(token.ctlvalues, data.Source);\n ");
				}
				else
				{
					sw.WriteLine("com.SetupFilterEnviromentData(base.EnvironmentParameters.SourceSurface); \n com.SetupFilterData(data.ControlValue, data.Source);");
				}
				sw.WriteLine("base.OnSetRenderInfo(parameters, dstArgs, srcArgs); \n } \n");

                sw.WriteLine("private bool Cancel() \n{ \n return base.IsCancelRequested; \n }");

				// Render
				sw.WriteLine("public override void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, Rectangle[] rois, int startIndex, int length) \n { \n");
				sw.WriteLine("for (int i = startIndex; i < startIndex + length; ++i) \n { \n");
				sw.WriteLine("Rectangle rect = rois[i];");
                sw.WriteLine("com.Render(dstArgs.Surface, rect, new Func<bool>(Cancel)); \n } \n }");
				sw.WriteLine("}\n }"); // end the class and the namespace

				ret = sw.ToString();
			}

			return ret;
		}
		
		private static void SetupCompilerParameters(string FileName, CompilerParameters cparm)
		{
			String installDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

			cparm.GenerateInMemory = true;
			cparm.GenerateExecutable = false;
			String resourceName = Path.Combine(dir,"FFEffect.FFEffectConfigDialog.resources");

#if DEBUG
			cparm.IncludeDebugInformation = true;
			cparm.CompilerOptions = string.Format("/debug:full /unsafe /optimize /target:library /resource:\"{0}\"", resourceName);
#else
			cparm.CompilerOptions = string.Format("/debug- /unsafe /optimize /target:library /resource:\"{0}\"", resourceName);
#endif
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            string effectsDir = Path.Combine(installDir, "Effects");
            string fileTypesDir = Path.Combine(installDir, "FileTypes");

            foreach (var asm in assemblies)
            {
                if (!asm.Location.StartsWith(effectsDir) && !asm.Location.StartsWith(fileTypesDir))
                {
                    cparm.ReferencedAssemblies.Add(asm.Location);
                }
            }

			cparm.OutputAssembly = Path.Combine(effectsDir, FileName);
		}
		static string dir = string.Empty;

		/// <summary>
		/// Sets up the temp Source code files.
		/// </summary>
		/// <param name="effectFileName">The effect FileName.</param>
		/// <param name="effectClassName">The effect ClasssName</param>
		/// <returns>The list of temp files</returns>
		public string[] SetupSourceCodeFiles(string effectFileName, string effectClassName)
		{
			List<string> files = new List<string>(8);
			dir = Path.Combine(Path.GetTempPath(), "Pdnfftemp");
			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);

			string[] embeddedfiles = new string[7] {"PdnFF.Coderes.FFEffect.FFEffectConfigDialog-comp.bin","PdnFF.Coderes.Common-comp.bin",
			"PdnFF.Coderes.FFEffectConfigDialog-comp.bin","PdnFF.Coderes.FFEffectConfigToken-comp.bin","PdnFF.Coderes.ffparse-comp.bin","PdnFF.Coderes.filter_data-comp.bin"
			,"PdnFF.Coderes.FilterEnviromentData-comp.bin"};

			for (int i = 0; i < embeddedfiles.Length; i++)
			{
				using (Stream res = Assembly.GetAssembly(typeof(PdnFF_Effect)).GetManifestResourceStream(embeddedfiles[i]))
				{
					

					string fn = embeddedfiles[i];

					fn = fn.Substring(14, (fn.Length - 14));

					fn = fn.Substring(0, fn.IndexOf("-"));

					if (i == 0)
					{
						fn += ".resources";
					}
					else
					{
						fn += ".cs";
					}
					
					string outfile = Path.Combine(dir, fn);

					using (FileStream fs = new FileStream(outfile,FileMode.Create, FileAccess.Write))
					{
						System.IO.Compression.GZipStream gz = new System.IO.Compression.GZipStream(res, System.IO.Compression.CompressionMode.Decompress, true);
						
						res.Position = (res.Length - 4L);
						byte[] writeBuf = new byte[4];
						res.Read(writeBuf, 0, 4);
						int len = BitConverter.ToInt32(writeBuf, 0);

						res.Position = 0L;


						byte[] bytes = new byte[len];
						int numBytesToRead = len;
						int numBytesRead = 0;
						while (numBytesToRead > 0)
						{
							// Read may return anything from 0 to numBytesToRead.
							int n = gz.Read(bytes, numBytesRead, numBytesToRead);
							// The end of the file is reached.
							if (n == 0)
								break;
							numBytesRead += n;
							numBytesToRead -= n;
						}
						gz.Close();

						fs.Write(bytes, 0, bytes.Length);
					}
					if (i > 0)
						files.Add(outfile);
				}

			}
			string outpath = Path.Combine(dir,"ffeffect.cs");
			string code = BuildEffectClass(effectClassName, effectFileName, this.data);
			File.WriteAllText(outpath, code);
			files.Add(outpath);

			return files.ToArray();
		}

		private void buildfilterbtn_Click(object sender, EventArgs e)
		{
			if (data != null && !string.IsNullOrEmpty(data.Author))
			{
				try
				{
					string FileName = string.Empty;
					if (!string.IsNullOrEmpty(Filenametxt.Text))
						FileName = Path.GetFileNameWithoutExtension(Filenametxt.Text);
					else
						FileName = Path.GetFileName(TitleBox.Text);
					FileName += ".dll";
					string filepath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar + "Effects", FileName);


					if (!File.Exists(filepath))
					{
						CompilerParameters cparm = new CompilerParameters();
						
						string classname = Path.GetFileNameWithoutExtension(FileName);

						for (int i = 0; i < classname.Length; i++)
						{
							char c = classname[i];
							if (!char.IsLetterOrDigit(c))
							{
								classname = classname.Remove(i, 1);
								i--;
							}
						}

						if (char.IsDigit(classname[0]))
						{
							classname = string.Concat("FF_", classname); 
						}

						// Setup the source code files after setting up the effect classname  
						string[] files = this.SetupSourceCodeFiles(FileName, classname);

						SetupCompilerParameters(FileName, cparm); 


						

						using (CSharpCodeProvider cscp = new CSharpCodeProvider())
						{
							CompilerResults cr = cscp.CompileAssemblyFromFile(cparm, files);

							if (cr.Errors.HasErrors)
							{
								MessageBox.Show(this, string.Format("Unable to build filter.\n\nError Text: \n {0}", cr.Errors[0].ErrorText), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
							else
							{
								MessageBox.Show(this, string.Format("Filter {0} Built successfully.\n\nYou will need to restart Paint.NET  to see it in the Effects menu.", Path.GetFileName(FileName)), this.Text);
							}
						}
					}
				   
				}
				catch (Exception ex)
				{
#if DEBUG
					MessageBox.Show(this, ex.Message + Environment.NewLine + ex.StackTrace, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
#else
					MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
					
				}
				finally
				{
					if (Directory.Exists(dir))
					{
						DirectoryInfo di = new DirectoryInfo(dir); 
						di.Delete(true); // delete the temp files 
					}
				} 
			}

		}

        private void RedBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:         
                    e.Handled = true;

                    AlphaBox.Focus();
                    AlphaBox.SelectionStart = AlphaBox.Text.Length;
                    break;
                case Keys.Down:
                    e.Handled = true;

                    GreenBox.Focus();
                    GreenBox.SelectionStart = GreenBox.Text.Length;
                    break;
            }
        }

        private void GreenBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    e.Handled = true;
                    RedBox.Focus();
                    RedBox.SelectionStart = RedBox.Text.Length;
                    break;
                case Keys.Down:
                    e.Handled = true;
                    BlueBox.Focus();
                    BlueBox.SelectionStart = BlueBox.Text.Length;
                    break;
            }
        }

        private void BlueBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    e.Handled = true;
                    GreenBox.Focus();
                    GreenBox.SelectionStart = GreenBox.Text.Length;
                    break;
                case Keys.Down:
                    e.Handled = true;
                    AlphaBox.Focus();
                    AlphaBox.SelectionStart = AlphaBox.Text.Length;
                    break;
            }
        }

        private void AlphaBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    e.Handled = true;
                    BlueBox.Focus();
                    BlueBox.SelectionStart = BlueBox.Text.Length;
                    break;
                case Keys.Down:
                    e.Handled = true;
                    RedBox.Focus();
                    RedBox.SelectionStart = RedBox.Text.Length;
                    break;
            }

        }


	}
}