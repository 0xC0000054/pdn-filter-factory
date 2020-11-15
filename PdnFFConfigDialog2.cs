/*
*  This file is part of pdn-filter-factory, a Paint.NET Effect that
*  interprets Filter Factory-based Adobe Photoshop filters.
*
*  Copyright (C) 2010, 2011, 2012, 2015, 2018, 2020 Nicholas Hayes
*
*  This program is free software: you can redistribute it and/or modify
*  it under the terms of the GNU General Public License as published by
*  the Free Software Foundation, either version 3 of the License, or
*  (at your option) any later version.
*
*  This program is distributed in the hope that it will be useful,
*  but WITHOUT ANY WARRANTY; without even the implied warranty of
*  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*  GNU General Public License for more details.
*
*  You should have received a copy of the GNU General Public License
*  along with this program.  If not, see <http://www.gnu.org/licenses/>.
*
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using PaintDotNet;
using PaintDotNet.Effects;
using PdnFF.Controls;
using PdnFF.Dialogs;
using PdnFF.Properties;

namespace PdnFF
{
	internal class PdnFFConfigDialog : EffectConfigDialog
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
		private PlatformFolderBrowserDialog DirBrowserDialog1;
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
		private TabControlEx tabControl1;
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

		private List<string> searchDirectories;
		private ListViewItem[] searchDirListViewCache;
		private int cacheStartIndex;
		private PdnFFSettings settings;
		private FilterData resetData;

		public PdnFFConfigDialog()
		{
			InitializeComponent();
			searchDirectories = new List<string>();
			resetData = null;
		}

		private static FilterData NewFilterData()
		{
			FilterData d = new FilterData();
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
			theEffectToken = new PdnFFConfigToken(null, null);
		}

		protected override void InitTokenFromDialog()
		{
			((PdnFFConfigToken)theEffectToken).Data = data;
			((PdnFFConfigToken)theEffectToken).ResetData = resetData;
		}

		protected override void InitDialogFromToken(EffectConfigToken effectTokenCopy)
		{
			PdnFFConfigToken token = (PdnFFConfigToken)effectTokenCopy;
			data = token.Data;
			resetData = token.ResetData;
		}

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PdnFFConfigDialog));
			buttonCancel = new System.Windows.Forms.Button();
			buttonOK = new System.Windows.Forms.Button();
			bluelbl = new System.Windows.Forms.Label();
			alphalbl = new System.Windows.Forms.Label();
			greenlbl = new System.Windows.Forms.Label();
			Redlbl = new System.Windows.Forms.Label();
			Savebtn2 = new System.Windows.Forms.Button();
			Loadbtn2 = new System.Windows.Forms.Button();
			Filenamelbl = new System.Windows.Forms.Label();
			Filenametxt = new System.Windows.Forms.Label();
			button3 = new System.Windows.Forms.Button();
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			default_fltr = new System.Windows.Forms.Button();
			DirBrowserDialog1 = new PdnFF.Dialogs.PlatformFolderBrowserDialog();
			UpdateFilterListbw = new System.ComponentModel.BackgroundWorker();
			FilterDirtab = new System.Windows.Forms.TabPage();
			subdirSearchcb = new System.Windows.Forms.CheckBox();
			remdirbtn = new System.Windows.Forms.Button();
			DirlistView1 = new System.Windows.Forms.ListView();
			pathColHeader = new System.Windows.Forms.ColumnHeader();
			adddirbtn = new System.Windows.Forms.Button();
			FilterManagertab = new System.Windows.Forms.TabPage();
			filterSearchBox = new System.Windows.Forms.TextBox();
			treefltrcopytxt = new System.Windows.Forms.Label();
			treefltrauthtxt = new System.Windows.Forms.Label();
			treefltrcopylbl = new System.Windows.Forms.Label();
			treefltrauthlbl = new System.Windows.Forms.Label();
			filtertreeview = new System.Windows.Forms.TreeView();
			filtermgrprogresspanel = new System.Windows.Forms.Panel();
			folderloadnamelbl = new System.Windows.Forms.Label();
			folderloadcountlbl = new System.Windows.Forms.Label();
			folderldprolbl = new System.Windows.Forms.Label();
			fltrmgrprogress = new System.Windows.Forms.ProgressBar();
			filterlistcnttxt = new System.Windows.Forms.Label();
			filterlistcntlbl = new System.Windows.Forms.Label();
			FFLtab = new System.Windows.Forms.TabPage();
			ffltreecopytxt = new System.Windows.Forms.Label();
			ffltreeauthtxt = new System.Windows.Forms.Label();
			ffltreecopylbl = new System.Windows.Forms.Label();
			ffltreeauthlbl = new System.Windows.Forms.Label();
			FFLtreeView1 = new System.Windows.Forms.TreeView();
			FFLfltrnumtxt = new System.Windows.Forms.Label();
			FFLfltrnumlbl = new System.Windows.Forms.Label();
			fflnametxt = new System.Windows.Forms.Label();
			FFLnamelbl = new System.Windows.Forms.Label();
			clearFFLbtn = new System.Windows.Forms.Button();
			LoadFFLbtn = new System.Windows.Forms.Button();
			editorTab = new System.Windows.Forms.TabPage();
			Interfacegb = new System.Windows.Forms.GroupBox();
			ctl5num = new System.Windows.Forms.NumericUpDown();
			ctl4num = new System.Windows.Forms.NumericUpDown();
			ctl6num = new System.Windows.Forms.NumericUpDown();
			ctl7num = new System.Windows.Forms.NumericUpDown();
			ctl3num = new System.Windows.Forms.NumericUpDown();
			ctl2num = new System.Windows.Forms.NumericUpDown();
			ctl1num = new System.Windows.Forms.NumericUpDown();
			ctl7txt = new System.Windows.Forms.TextBox();
			ctl7cb = new System.Windows.Forms.CheckBox();
			label18 = new System.Windows.Forms.Label();
			ctl6txt = new System.Windows.Forms.TextBox();
			ctl6cb = new System.Windows.Forms.CheckBox();
			ctl3txt = new System.Windows.Forms.TextBox();
			label17 = new System.Windows.Forms.Label();
			ctl3cb = new System.Windows.Forms.CheckBox();
			label16 = new System.Windows.Forms.Label();
			ctl2txt = new System.Windows.Forms.TextBox();
			ctl2cb = new System.Windows.Forms.CheckBox();
			label15 = new System.Windows.Forms.Label();
			ctl5txt = new System.Windows.Forms.TextBox();
			ctl5cb = new System.Windows.Forms.CheckBox();
			label14 = new System.Windows.Forms.Label();
			ctl1txt = new System.Windows.Forms.TextBox();
			ctl1cb = new System.Windows.Forms.CheckBox();
			label13 = new System.Windows.Forms.Label();
			ctl4txt = new System.Windows.Forms.TextBox();
			ctl4cb = new System.Windows.Forms.CheckBox();
			label12 = new System.Windows.Forms.Label();
			ctl0txt = new System.Windows.Forms.TextBox();
			ctl0cb = new System.Windows.Forms.CheckBox();
			label11 = new System.Windows.Forms.Label();
			map2txt = new System.Windows.Forms.TextBox();
			map2cb = new System.Windows.Forms.CheckBox();
			map2lbl = new System.Windows.Forms.Label();
			map1txt = new System.Windows.Forms.TextBox();
			map1cb = new System.Windows.Forms.CheckBox();
			map1lbl = new System.Windows.Forms.Label();
			map3txt = new System.Windows.Forms.TextBox();
			map3cb = new System.Windows.Forms.CheckBox();
			map3lbl = new System.Windows.Forms.Label();
			map0txt = new System.Windows.Forms.TextBox();
			map0cb = new System.Windows.Forms.CheckBox();
			label10 = new System.Windows.Forms.Label();
			ctl0num = new System.Windows.Forms.NumericUpDown();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			Infogb = new System.Windows.Forms.GroupBox();
			infofilterlbl = new System.Windows.Forms.Label();
			infocatlbl = new System.Windows.Forms.Label();
			infoauthlbl = new System.Windows.Forms.Label();
			infocopylbl = new System.Windows.Forms.Label();
			CategoryBox = new System.Windows.Forms.TextBox();
			CopyrightBox = new System.Windows.Forms.TextBox();
			AuthorBox = new System.Windows.Forms.TextBox();
			TitleBox = new System.Windows.Forms.TextBox();
			AlphaBox = new System.Windows.Forms.TextBox();
			BlueBox = new System.Windows.Forms.TextBox();
			GreenBox = new System.Windows.Forms.TextBox();
			RedBox = new System.Windows.Forms.TextBox();
			Savebtn = new System.Windows.Forms.Button();
			button2 = new System.Windows.Forms.Button();
			button1 = new System.Windows.Forms.Button();
			controlsTab = new System.Windows.Forms.TabPage();
			fltrAuthorTxt = new System.Windows.Forms.Label();
			fltrTitletxt = new System.Windows.Forms.Label();
			fltrTitlelbl = new System.Windows.Forms.Label();
			copylbl = new System.Windows.Forms.Label();
			authlbl = new System.Windows.Forms.Label();
			catlbl = new System.Windows.Forms.Label();
			fltrCopyTxt = new System.Windows.Forms.Label();
			fltrCatTxt = new System.Windows.Forms.Label();
			ctlpanel = new System.Windows.Forms.Panel();
			ctllbl2 = new System.Windows.Forms.Label();
			resetbtn1 = new System.Windows.Forms.Button();
			resetbtn7 = new System.Windows.Forms.Button();
			resetbtn6 = new System.Windows.Forms.Button();
			resetbtn5 = new System.Windows.Forms.Button();
			resetbtn4 = new System.Windows.Forms.Button();
			resetbtn3 = new System.Windows.Forms.Button();
			resetbtn2 = new System.Windows.Forms.Button();
			ctllbl7 = new System.Windows.Forms.Label();
			ctllbl6 = new System.Windows.Forms.Label();
			resetbtn0 = new System.Windows.Forms.Button();
			ctllbl5 = new System.Windows.Forms.Label();
			ctl7 = new System.Windows.Forms.TrackBar();
			ctllbl4 = new System.Windows.Forms.Label();
			ctl6 = new System.Windows.Forms.TrackBar();
			ctllbl3 = new System.Windows.Forms.Label();
			ctl5 = new System.Windows.Forms.TrackBar();
			ctl4 = new System.Windows.Forms.TrackBar();
			ctllbl1 = new System.Windows.Forms.Label();
			ctl3 = new System.Windows.Forms.TrackBar();
			ctllbl0 = new System.Windows.Forms.Label();
			ctl7_UpDown = new System.Windows.Forms.NumericUpDown();
			map3_lbl = new System.Windows.Forms.Label();
			ctl6_UpDown = new System.Windows.Forms.NumericUpDown();
			map2_lbl = new System.Windows.Forms.Label();
			ctl4_UpDown = new System.Windows.Forms.NumericUpDown();
			ctl5_UpDown = new System.Windows.Forms.NumericUpDown();
			map0_lbl = new System.Windows.Forms.Label();
			ctl3_UpDown = new System.Windows.Forms.NumericUpDown();
			ctl2_UpDown = new System.Windows.Forms.NumericUpDown();
			ctl1_UpDown = new System.Windows.Forms.NumericUpDown();
			ctl0_UpDown = new System.Windows.Forms.NumericUpDown();
			ctl2 = new System.Windows.Forms.TrackBar();
			ctl1 = new System.Windows.Forms.TrackBar();
			ctl0 = new System.Windows.Forms.TrackBar();
			map1_lbl = new System.Windows.Forms.Label();
			tabControl1 = new PdnFF.Controls.TabControlEx();
			buildfilterbtn = new System.Windows.Forms.Button();
			imageList1 = new System.Windows.Forms.ImageList(components);
			FilterDirtab.SuspendLayout();
			FilterManagertab.SuspendLayout();
			filtermgrprogresspanel.SuspendLayout();
			FFLtab.SuspendLayout();
			editorTab.SuspendLayout();
			Interfacegb.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(ctl5num)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(ctl4num)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(ctl6num)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(ctl7num)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(ctl3num)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(ctl2num)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(ctl1num)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(ctl0num)).BeginInit();
			Infogb.SuspendLayout();
			controlsTab.SuspendLayout();
			ctlpanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(ctl7)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(ctl6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(ctl5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(ctl4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(ctl3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(ctl7_UpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(ctl6_UpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(ctl4_UpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(ctl5_UpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(ctl3_UpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(ctl2_UpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(ctl1_UpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(ctl0_UpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(ctl2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(ctl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(ctl0)).BeginInit();
			tabControl1.SuspendLayout();
			SuspendLayout();
			//
			// buttonCancel
			//
			resources.ApplyResources(buttonCancel, "buttonCancel");
			buttonCancel.Name = "buttonCancel";
			buttonCancel.UseVisualStyleBackColor = true;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			//
			// buttonOK
			//
			resources.ApplyResources(buttonOK, "buttonOK");
			buttonOK.Name = "buttonOK";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			//
			// bluelbl
			//
			resources.ApplyResources(bluelbl, "bluelbl");
			bluelbl.Name = "bluelbl";
			//
			// alphalbl
			//
			resources.ApplyResources(alphalbl, "alphalbl");
			alphalbl.Name = "alphalbl";
			//
			// greenlbl
			//
			resources.ApplyResources(greenlbl, "greenlbl");
			greenlbl.Name = "greenlbl";
			//
			// Redlbl
			//
			resources.ApplyResources(Redlbl, "Redlbl");
			Redlbl.Name = "Redlbl";
			//
			// Savebtn2
			//
			resources.ApplyResources(Savebtn2, "Savebtn2");
			Savebtn2.Name = "Savebtn2";
			Savebtn2.UseVisualStyleBackColor = true;
			Savebtn2.Click += new System.EventHandler(Savebtn_Click);
			//
			// Loadbtn2
			//
			resources.ApplyResources(Loadbtn2, "Loadbtn2");
			Loadbtn2.Name = "Loadbtn2";
			Loadbtn2.UseVisualStyleBackColor = true;
			Loadbtn2.Click += new System.EventHandler(Loadbtn_Click);
			//
			// Filenamelbl
			//
			resources.ApplyResources(Filenamelbl, "Filenamelbl");
			Filenamelbl.Name = "Filenamelbl";
			//
			// Filenametxt
			//
			resources.ApplyResources(Filenametxt, "Filenametxt");
			Filenametxt.Name = "Filenametxt";
			//
			// button3
			//
			resources.ApplyResources(button3, "button3");
			button3.Name = "button3";
			button3.UseVisualStyleBackColor = true;
			//
			// openFileDialog1
			//
			resources.ApplyResources(openFileDialog1, "openFileDialog1");
			//
			// saveFileDialog1
			//
			saveFileDialog1.DefaultExt = "txt";
			resources.ApplyResources(saveFileDialog1, "saveFileDialog1");
			//
			// default_fltr
			//
			resources.ApplyResources(default_fltr, "default_fltr");
			default_fltr.Name = "default_fltr";
			default_fltr.UseVisualStyleBackColor = true;
			default_fltr.Click += new System.EventHandler(default_fltr_Click);
			//
			// UpdateFilterListbw
			//
			UpdateFilterListbw.WorkerReportsProgress = true;
			UpdateFilterListbw.WorkerSupportsCancellation = true;
			UpdateFilterListbw.DoWork += new System.ComponentModel.DoWorkEventHandler(UpdateFilterListbw_DoWork);
			UpdateFilterListbw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(UpdateFilterListbw_ProgressChanged);
			UpdateFilterListbw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(UpdateFilterListbw_RunWorkerCompleted);
			//
			// FilterDirtab
			//
			FilterDirtab.BackColor = System.Drawing.SystemColors.Control;
			FilterDirtab.Controls.Add(subdirSearchcb);
			FilterDirtab.Controls.Add(remdirbtn);
			FilterDirtab.Controls.Add(DirlistView1);
			FilterDirtab.Controls.Add(adddirbtn);
			resources.ApplyResources(FilterDirtab, "FilterDirtab");
			FilterDirtab.Name = "FilterDirtab";
			//
			// subdirSearchcb
			//
			resources.ApplyResources(subdirSearchcb, "subdirSearchcb");
			subdirSearchcb.Name = "subdirSearchcb";
			subdirSearchcb.UseVisualStyleBackColor = true;
			subdirSearchcb.Click += new System.EventHandler(subdirSearchcb_CheckedChanged);
			//
			// remdirbtn
			//
			resources.ApplyResources(remdirbtn, "remdirbtn");
			remdirbtn.Name = "remdirbtn";
			remdirbtn.UseVisualStyleBackColor = true;
			remdirbtn.Click += new System.EventHandler(remdirbtn_Click);
			//
			// DirlistView1
			//
			DirlistView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			pathColHeader});
			DirlistView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			DirlistView1.HideSelection = false;
			resources.ApplyResources(DirlistView1, "DirlistView1");
			DirlistView1.MultiSelect = false;
			DirlistView1.Name = "DirlistView1";
			DirlistView1.UseCompatibleStateImageBehavior = false;
			DirlistView1.View = System.Windows.Forms.View.Details;
			DirlistView1.VirtualMode = true;
			DirlistView1.CacheVirtualItems += new CacheVirtualItemsEventHandler(DirlistView1_CacheVirtualItems);
			DirlistView1.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(DirlistView1_RetrieveVirtualItem);
			//
			// pathColHeader
			//
			resources.ApplyResources(pathColHeader, "pathColHeader");
			//
			// adddirbtn
			//
			resources.ApplyResources(adddirbtn, "adddirbtn");
			adddirbtn.Name = "adddirbtn";
			adddirbtn.UseVisualStyleBackColor = true;
			adddirbtn.Click += new System.EventHandler(adddirbtn_Click);
			//
			// FilterManagertab
			//
			FilterManagertab.BackColor = System.Drawing.SystemColors.Control;
			FilterManagertab.Controls.Add(filterSearchBox);
			FilterManagertab.Controls.Add(treefltrcopytxt);
			FilterManagertab.Controls.Add(treefltrauthtxt);
			FilterManagertab.Controls.Add(treefltrcopylbl);
			FilterManagertab.Controls.Add(treefltrauthlbl);
			FilterManagertab.Controls.Add(filtertreeview);
			FilterManagertab.Controls.Add(filtermgrprogresspanel);
			FilterManagertab.Controls.Add(filterlistcnttxt);
			FilterManagertab.Controls.Add(filterlistcntlbl);
			resources.ApplyResources(FilterManagertab, "FilterManagertab");
			FilterManagertab.Name = "FilterManagertab";
			//
			// filterSearchBox
			//
			resources.ApplyResources(filterSearchBox, "filterSearchBox");
			filterSearchBox.ForeColor = System.Drawing.SystemColors.GrayText;
			filterSearchBox.Name = "filterSearchBox";
			filterSearchBox.TextChanged += new System.EventHandler(filterSearchBox_TextChanged);
			filterSearchBox.Enter += new System.EventHandler(filterSearchBox_Enter);
			filterSearchBox.Leave += new System.EventHandler(filterSearchBox_Leave);
			//
			// treefltrcopytxt
			//
			resources.ApplyResources(treefltrcopytxt, "treefltrcopytxt");
			treefltrcopytxt.Name = "treefltrcopytxt";
			//
			// treefltrauthtxt
			//
			resources.ApplyResources(treefltrauthtxt, "treefltrauthtxt");
			treefltrauthtxt.Name = "treefltrauthtxt";
			//
			// treefltrcopylbl
			//
			resources.ApplyResources(treefltrcopylbl, "treefltrcopylbl");
			treefltrcopylbl.Name = "treefltrcopylbl";
			//
			// treefltrauthlbl
			//
			resources.ApplyResources(treefltrauthlbl, "treefltrauthlbl");
			treefltrauthlbl.Name = "treefltrauthlbl";
			//
			// filtertreeview
			//
			filtertreeview.HideSelection = false;
			resources.ApplyResources(filtertreeview, "filtertreeview");
			filtertreeview.Name = "filtertreeview";
			filtertreeview.ShowLines = false;
			filtertreeview.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(filtertreeview_AfterSelect);
			//
			// filtermgrprogresspanel
			//
			filtermgrprogresspanel.Controls.Add(folderloadnamelbl);
			filtermgrprogresspanel.Controls.Add(folderloadcountlbl);
			filtermgrprogresspanel.Controls.Add(folderldprolbl);
			filtermgrprogresspanel.Controls.Add(fltrmgrprogress);
			resources.ApplyResources(filtermgrprogresspanel, "filtermgrprogresspanel");
			filtermgrprogresspanel.Name = "filtermgrprogresspanel";
			//
			// folderloadnamelbl
			//
			resources.ApplyResources(folderloadnamelbl, "folderloadnamelbl");
			folderloadnamelbl.Name = "folderloadnamelbl";
			//
			// folderloadcountlbl
			//
			resources.ApplyResources(folderloadcountlbl, "folderloadcountlbl");
			folderloadcountlbl.Name = "folderloadcountlbl";
			//
			// folderldprolbl
			//
			resources.ApplyResources(folderldprolbl, "folderldprolbl");
			folderldprolbl.Name = "folderldprolbl";
			//
			// fltrmgrprogress
			//
			resources.ApplyResources(fltrmgrprogress, "fltrmgrprogress");
			fltrmgrprogress.Name = "fltrmgrprogress";
			fltrmgrprogress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			//
			// filterlistcnttxt
			//
			resources.ApplyResources(filterlistcnttxt, "filterlistcnttxt");
			filterlistcnttxt.Name = "filterlistcnttxt";
			//
			// filterlistcntlbl
			//
			resources.ApplyResources(filterlistcntlbl, "filterlistcntlbl");
			filterlistcntlbl.Name = "filterlistcntlbl";
			//
			// FFLtab
			//
			FFLtab.BackColor = System.Drawing.SystemColors.Control;
			FFLtab.Controls.Add(ffltreecopytxt);
			FFLtab.Controls.Add(ffltreeauthtxt);
			FFLtab.Controls.Add(ffltreecopylbl);
			FFLtab.Controls.Add(ffltreeauthlbl);
			FFLtab.Controls.Add(FFLtreeView1);
			FFLtab.Controls.Add(FFLfltrnumtxt);
			FFLtab.Controls.Add(FFLfltrnumlbl);
			FFLtab.Controls.Add(fflnametxt);
			FFLtab.Controls.Add(FFLnamelbl);
			FFLtab.Controls.Add(clearFFLbtn);
			FFLtab.Controls.Add(LoadFFLbtn);
			resources.ApplyResources(FFLtab, "FFLtab");
			FFLtab.Name = "FFLtab";
			//
			// ffltreecopytxt
			//
			resources.ApplyResources(ffltreecopytxt, "ffltreecopytxt");
			ffltreecopytxt.Name = "ffltreecopytxt";
			//
			// ffltreeauthtxt
			//
			resources.ApplyResources(ffltreeauthtxt, "ffltreeauthtxt");
			ffltreeauthtxt.Name = "ffltreeauthtxt";
			//
			// ffltreecopylbl
			//
			resources.ApplyResources(ffltreecopylbl, "ffltreecopylbl");
			ffltreecopylbl.Name = "ffltreecopylbl";
			//
			// ffltreeauthlbl
			//
			resources.ApplyResources(ffltreeauthlbl, "ffltreeauthlbl");
			ffltreeauthlbl.Name = "ffltreeauthlbl";
			//
			// FFLtreeView1
			//
			FFLtreeView1.HideSelection = false;
			resources.ApplyResources(FFLtreeView1, "FFLtreeView1");
			FFLtreeView1.Name = "FFLtreeView1";
			FFLtreeView1.ShowLines = false;
			FFLtreeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(FFLtreeView1_AfterSelect);
			//
			// FFLfltrnumtxt
			//
			resources.ApplyResources(FFLfltrnumtxt, "FFLfltrnumtxt");
			FFLfltrnumtxt.Name = "FFLfltrnumtxt";
			//
			// FFLfltrnumlbl
			//
			resources.ApplyResources(FFLfltrnumlbl, "FFLfltrnumlbl");
			FFLfltrnumlbl.Name = "FFLfltrnumlbl";
			//
			// fflnametxt
			//
			resources.ApplyResources(fflnametxt, "fflnametxt");
			fflnametxt.Name = "fflnametxt";
			//
			// FFLnamelbl
			//
			resources.ApplyResources(FFLnamelbl, "FFLnamelbl");
			FFLnamelbl.Name = "FFLnamelbl";
			//
			// clearFFLbtn
			//
			resources.ApplyResources(clearFFLbtn, "clearFFLbtn");
			clearFFLbtn.Name = "clearFFLbtn";
			clearFFLbtn.UseVisualStyleBackColor = true;
			clearFFLbtn.Click += new System.EventHandler(clearFFLbtn_Click);
			//
			// LoadFFLbtn
			//
			resources.ApplyResources(LoadFFLbtn, "LoadFFLbtn");
			LoadFFLbtn.Name = "LoadFFLbtn";
			LoadFFLbtn.UseVisualStyleBackColor = true;
			LoadFFLbtn.Click += new System.EventHandler(LoadFFLbtn_Click);
			//
			// editorTab
			//
			editorTab.BackColor = System.Drawing.SystemColors.Control;
			editorTab.Controls.Add(Interfacegb);
			editorTab.Controls.Add(label1);
			editorTab.Controls.Add(label2);
			editorTab.Controls.Add(label4);
			editorTab.Controls.Add(label5);
			editorTab.Controls.Add(Infogb);
			editorTab.Controls.Add(AlphaBox);
			editorTab.Controls.Add(BlueBox);
			editorTab.Controls.Add(GreenBox);
			editorTab.Controls.Add(RedBox);
			editorTab.Controls.Add(Savebtn);
			editorTab.Controls.Add(button2);
			editorTab.Controls.Add(button1);
			resources.ApplyResources(editorTab, "editorTab");
			editorTab.Name = "editorTab";
			//
			// Interfacegb
			//
			Interfacegb.Controls.Add(ctl5num);
			Interfacegb.Controls.Add(ctl4num);
			Interfacegb.Controls.Add(ctl6num);
			Interfacegb.Controls.Add(ctl7num);
			Interfacegb.Controls.Add(ctl3num);
			Interfacegb.Controls.Add(ctl2num);
			Interfacegb.Controls.Add(ctl1num);
			Interfacegb.Controls.Add(ctl7txt);
			Interfacegb.Controls.Add(ctl7cb);
			Interfacegb.Controls.Add(label18);
			Interfacegb.Controls.Add(ctl6txt);
			Interfacegb.Controls.Add(ctl6cb);
			Interfacegb.Controls.Add(ctl3txt);
			Interfacegb.Controls.Add(label17);
			Interfacegb.Controls.Add(ctl3cb);
			Interfacegb.Controls.Add(label16);
			Interfacegb.Controls.Add(ctl2txt);
			Interfacegb.Controls.Add(ctl2cb);
			Interfacegb.Controls.Add(label15);
			Interfacegb.Controls.Add(ctl5txt);
			Interfacegb.Controls.Add(ctl5cb);
			Interfacegb.Controls.Add(label14);
			Interfacegb.Controls.Add(ctl1txt);
			Interfacegb.Controls.Add(ctl1cb);
			Interfacegb.Controls.Add(label13);
			Interfacegb.Controls.Add(ctl4txt);
			Interfacegb.Controls.Add(ctl4cb);
			Interfacegb.Controls.Add(label12);
			Interfacegb.Controls.Add(ctl0txt);
			Interfacegb.Controls.Add(ctl0cb);
			Interfacegb.Controls.Add(label11);
			Interfacegb.Controls.Add(map2txt);
			Interfacegb.Controls.Add(map2cb);
			Interfacegb.Controls.Add(map2lbl);
			Interfacegb.Controls.Add(map1txt);
			Interfacegb.Controls.Add(map1cb);
			Interfacegb.Controls.Add(map1lbl);
			Interfacegb.Controls.Add(map3txt);
			Interfacegb.Controls.Add(map3cb);
			Interfacegb.Controls.Add(map3lbl);
			Interfacegb.Controls.Add(map0txt);
			Interfacegb.Controls.Add(map0cb);
			Interfacegb.Controls.Add(label10);
			Interfacegb.Controls.Add(ctl0num);
			resources.ApplyResources(Interfacegb, "Interfacegb");
			Interfacegb.Name = "Interfacegb";
			Interfacegb.TabStop = false;
			//
			// ctl5num
			//
			resources.ApplyResources(ctl5num, "ctl5num");
			ctl5num.Maximum = new decimal(new int[] {
			255,
			0,
			0,
			0});
			ctl5num.Name = "ctl5num";
			ctl5num.ValueChanged += new System.EventHandler(ctlnum_ValueChanged);
			//
			// ctl4num
			//
			resources.ApplyResources(ctl4num, "ctl4num");
			ctl4num.Maximum = new decimal(new int[] {
			255,
			0,
			0,
			0});
			ctl4num.Name = "ctl4num";
			ctl4num.ValueChanged += new System.EventHandler(ctlnum_ValueChanged);
			//
			// ctl6num
			//
			resources.ApplyResources(ctl6num, "ctl6num");
			ctl6num.Maximum = new decimal(new int[] {
			255,
			0,
			0,
			0});
			ctl6num.Name = "ctl6num";
			ctl6num.ValueChanged += new System.EventHandler(ctlnum_ValueChanged);
			//
			// ctl7num
			//
			resources.ApplyResources(ctl7num, "ctl7num");
			ctl7num.Maximum = new decimal(new int[] {
			255,
			0,
			0,
			0});
			ctl7num.Name = "ctl7num";
			ctl7num.ValueChanged += new System.EventHandler(ctlnum_ValueChanged);
			//
			// ctl3num
			//
			resources.ApplyResources(ctl3num, "ctl3num");
			ctl3num.Maximum = new decimal(new int[] {
			255,
			0,
			0,
			0});
			ctl3num.Name = "ctl3num";
			ctl3num.ValueChanged += new System.EventHandler(ctlnum_ValueChanged);
			//
			// ctl2num
			//
			resources.ApplyResources(ctl2num, "ctl2num");
			ctl2num.Maximum = new decimal(new int[] {
			255,
			0,
			0,
			0});
			ctl2num.Name = "ctl2num";
			ctl2num.ValueChanged += new System.EventHandler(ctlnum_ValueChanged);
			//
			// ctl1num
			//
			resources.ApplyResources(ctl1num, "ctl1num");
			ctl1num.Maximum = new decimal(new int[] {
			255,
			0,
			0,
			0});
			ctl1num.Name = "ctl1num";
			ctl1num.ValueChanged += new System.EventHandler(ctlnum_ValueChanged);
			//
			// ctl7txt
			//
			resources.ApplyResources(ctl7txt, "ctl7txt");
			ctl7txt.Name = "ctl7txt";
			ctl7txt.TextChanged += new System.EventHandler(edittxt_TextChanged);
			//
			// ctl7cb
			//
			resources.ApplyResources(ctl7cb, "ctl7cb");
			ctl7cb.Name = "ctl7cb";
			ctl7cb.UseVisualStyleBackColor = true;
			ctl7cb.CheckedChanged += new System.EventHandler(editcb_CheckedChanged);
			//
			// label18
			//
			resources.ApplyResources(label18, "label18");
			label18.Name = "label18";
			//
			// ctl6txt
			//
			resources.ApplyResources(ctl6txt, "ctl6txt");
			ctl6txt.Name = "ctl6txt";
			ctl6txt.TextChanged += new System.EventHandler(edittxt_TextChanged);
			//
			// ctl6cb
			//
			resources.ApplyResources(ctl6cb, "ctl6cb");
			ctl6cb.Name = "ctl6cb";
			ctl6cb.UseVisualStyleBackColor = true;
			ctl6cb.CheckedChanged += new System.EventHandler(editcb_CheckedChanged);
			//
			// ctl3txt
			//
			resources.ApplyResources(ctl3txt, "ctl3txt");
			ctl3txt.Name = "ctl3txt";
			ctl3txt.TextChanged += new System.EventHandler(edittxt_TextChanged);
			//
			// label17
			//
			resources.ApplyResources(label17, "label17");
			label17.Name = "label17";
			//
			// ctl3cb
			//
			resources.ApplyResources(ctl3cb, "ctl3cb");
			ctl3cb.Name = "ctl3cb";
			ctl3cb.UseVisualStyleBackColor = true;
			ctl3cb.CheckedChanged += new System.EventHandler(editcb_CheckedChanged);
			//
			// label16
			//
			resources.ApplyResources(label16, "label16");
			label16.Name = "label16";
			//
			// ctl2txt
			//
			resources.ApplyResources(ctl2txt, "ctl2txt");
			ctl2txt.Name = "ctl2txt";
			ctl2txt.TextChanged += new System.EventHandler(edittxt_TextChanged);
			//
			// ctl2cb
			//
			resources.ApplyResources(ctl2cb, "ctl2cb");
			ctl2cb.Name = "ctl2cb";
			ctl2cb.UseVisualStyleBackColor = true;
			ctl2cb.CheckedChanged += new System.EventHandler(editcb_CheckedChanged);
			//
			// label15
			//
			resources.ApplyResources(label15, "label15");
			label15.Name = "label15";
			//
			// ctl5txt
			//
			resources.ApplyResources(ctl5txt, "ctl5txt");
			ctl5txt.Name = "ctl5txt";
			ctl5txt.TextChanged += new System.EventHandler(edittxt_TextChanged);
			//
			// ctl5cb
			//
			resources.ApplyResources(ctl5cb, "ctl5cb");
			ctl5cb.Name = "ctl5cb";
			ctl5cb.UseVisualStyleBackColor = true;
			ctl5cb.CheckedChanged += new System.EventHandler(editcb_CheckedChanged);
			//
			// label14
			//
			resources.ApplyResources(label14, "label14");
			label14.Name = "label14";
			//
			// ctl1txt
			//
			resources.ApplyResources(ctl1txt, "ctl1txt");
			ctl1txt.Name = "ctl1txt";
			ctl1txt.TextChanged += new System.EventHandler(edittxt_TextChanged);
			//
			// ctl1cb
			//
			resources.ApplyResources(ctl1cb, "ctl1cb");
			ctl1cb.Name = "ctl1cb";
			ctl1cb.UseVisualStyleBackColor = true;
			ctl1cb.CheckedChanged += new System.EventHandler(editcb_CheckedChanged);
			//
			// label13
			//
			resources.ApplyResources(label13, "label13");
			label13.Name = "label13";
			//
			// ctl4txt
			//
			resources.ApplyResources(ctl4txt, "ctl4txt");
			ctl4txt.Name = "ctl4txt";
			ctl4txt.TextChanged += new System.EventHandler(edittxt_TextChanged);
			//
			// ctl4cb
			//
			resources.ApplyResources(ctl4cb, "ctl4cb");
			ctl4cb.Name = "ctl4cb";
			ctl4cb.UseVisualStyleBackColor = true;
			ctl4cb.CheckedChanged += new System.EventHandler(editcb_CheckedChanged);
			//
			// label12
			//
			resources.ApplyResources(label12, "label12");
			label12.Name = "label12";
			//
			// ctl0txt
			//
			resources.ApplyResources(ctl0txt, "ctl0txt");
			ctl0txt.Name = "ctl0txt";
			ctl0txt.TextChanged += new System.EventHandler(edittxt_TextChanged);
			//
			// ctl0cb
			//
			resources.ApplyResources(ctl0cb, "ctl0cb");
			ctl0cb.Name = "ctl0cb";
			ctl0cb.UseVisualStyleBackColor = true;
			ctl0cb.CheckedChanged += new System.EventHandler(editcb_CheckedChanged);
			//
			// label11
			//
			resources.ApplyResources(label11, "label11");
			label11.Name = "label11";
			//
			// map2txt
			//
			resources.ApplyResources(map2txt, "map2txt");
			map2txt.Name = "map2txt";
			map2txt.TextChanged += new System.EventHandler(edittxt_TextChanged);
			//
			// map2cb
			//
			resources.ApplyResources(map2cb, "map2cb");
			map2cb.Name = "map2cb";
			map2cb.UseVisualStyleBackColor = true;
			map2cb.CheckedChanged += new System.EventHandler(editcb_CheckedChanged);
			//
			// map2lbl
			//
			resources.ApplyResources(map2lbl, "map2lbl");
			map2lbl.Name = "map2lbl";
			//
			// map1txt
			//
			resources.ApplyResources(map1txt, "map1txt");
			map1txt.Name = "map1txt";
			map1txt.TextChanged += new System.EventHandler(edittxt_TextChanged);
			//
			// map1cb
			//
			resources.ApplyResources(map1cb, "map1cb");
			map1cb.Name = "map1cb";
			map1cb.UseVisualStyleBackColor = true;
			map1cb.CheckedChanged += new System.EventHandler(editcb_CheckedChanged);
			//
			// map1lbl
			//
			resources.ApplyResources(map1lbl, "map1lbl");
			map1lbl.Name = "map1lbl";
			//
			// map3txt
			//
			resources.ApplyResources(map3txt, "map3txt");
			map3txt.Name = "map3txt";
			map3txt.TextChanged += new System.EventHandler(edittxt_TextChanged);
			//
			// map3cb
			//
			resources.ApplyResources(map3cb, "map3cb");
			map3cb.Name = "map3cb";
			map3cb.UseVisualStyleBackColor = true;
			map3cb.CheckedChanged += new System.EventHandler(editcb_CheckedChanged);
			//
			// map3lbl
			//
			resources.ApplyResources(map3lbl, "map3lbl");
			map3lbl.Name = "map3lbl";
			//
			// map0txt
			//
			resources.ApplyResources(map0txt, "map0txt");
			map0txt.Name = "map0txt";
			map0txt.TextChanged += new System.EventHandler(edittxt_TextChanged);
			//
			// map0cb
			//
			resources.ApplyResources(map0cb, "map0cb");
			map0cb.Name = "map0cb";
			map0cb.UseVisualStyleBackColor = true;
			map0cb.CheckedChanged += new System.EventHandler(editcb_CheckedChanged);
			//
			// label10
			//
			resources.ApplyResources(label10, "label10");
			label10.Name = "label10";
			//
			// ctl0num
			//
			resources.ApplyResources(ctl0num, "ctl0num");
			ctl0num.Maximum = new decimal(new int[] {
			255,
			0,
			0,
			0});
			ctl0num.Name = "ctl0num";
			ctl0num.ValueChanged += new System.EventHandler(ctlnum_ValueChanged);
			//
			// label1
			//
			resources.ApplyResources(label1, "label1");
			label1.Name = "label1";
			//
			// label2
			//
			resources.ApplyResources(label2, "label2");
			label2.Name = "label2";
			//
			// label4
			//
			resources.ApplyResources(label4, "label4");
			label4.Name = "label4";
			//
			// label5
			//
			resources.ApplyResources(label5, "label5");
			label5.Name = "label5";
			//
			// Infogb
			//
			Infogb.BackColor = System.Drawing.Color.Transparent;
			Infogb.Controls.Add(infofilterlbl);
			Infogb.Controls.Add(infocatlbl);
			Infogb.Controls.Add(infoauthlbl);
			Infogb.Controls.Add(infocopylbl);
			Infogb.Controls.Add(CategoryBox);
			Infogb.Controls.Add(CopyrightBox);
			Infogb.Controls.Add(AuthorBox);
			Infogb.Controls.Add(TitleBox);
			resources.ApplyResources(Infogb, "Infogb");
			Infogb.Name = "Infogb";
			Infogb.TabStop = false;
			//
			// infofilterlbl
			//
			resources.ApplyResources(infofilterlbl, "infofilterlbl");
			infofilterlbl.Name = "infofilterlbl";
			//
			// infocatlbl
			//
			resources.ApplyResources(infocatlbl, "infocatlbl");
			infocatlbl.Name = "infocatlbl";
			//
			// infoauthlbl
			//
			resources.ApplyResources(infoauthlbl, "infoauthlbl");
			infoauthlbl.Name = "infoauthlbl";
			//
			// infocopylbl
			//
			resources.ApplyResources(infocopylbl, "infocopylbl");
			infocopylbl.Name = "infocopylbl";
			//
			// CategoryBox
			//
			resources.ApplyResources(CategoryBox, "CategoryBox");
			CategoryBox.Name = "CategoryBox";
			CategoryBox.TextChanged += new System.EventHandler(editInfoTxt_TextChanged);
			//
			// CopyrightBox
			//
			resources.ApplyResources(CopyrightBox, "CopyrightBox");
			CopyrightBox.Name = "CopyrightBox";
			CopyrightBox.TextChanged += new System.EventHandler(editInfoTxt_TextChanged);
			//
			// AuthorBox
			//
			resources.ApplyResources(AuthorBox, "AuthorBox");
			AuthorBox.Name = "AuthorBox";
			AuthorBox.TextChanged += new System.EventHandler(editInfoTxt_TextChanged);
			//
			// TitleBox
			//
			resources.ApplyResources(TitleBox, "TitleBox");
			TitleBox.Name = "TitleBox";
			TitleBox.TextChanged += new System.EventHandler(editInfoTxt_TextChanged);
			//
			// AlphaBox
			//
			resources.ApplyResources(AlphaBox, "AlphaBox");
			AlphaBox.Name = "AlphaBox";
			AlphaBox.TextChanged += new System.EventHandler(SrcTextBoxes_TextChanged);
			AlphaBox.KeyDown += new System.Windows.Forms.KeyEventHandler(AlphaBox_KeyDown);
			//
			// BlueBox
			//
			resources.ApplyResources(BlueBox, "BlueBox");
			BlueBox.Name = "BlueBox";
			BlueBox.TextChanged += new System.EventHandler(SrcTextBoxes_TextChanged);
			BlueBox.KeyDown += new System.Windows.Forms.KeyEventHandler(BlueBox_KeyDown);
			//
			// GreenBox
			//
			resources.ApplyResources(GreenBox, "GreenBox");
			GreenBox.Name = "GreenBox";
			GreenBox.TextChanged += new System.EventHandler(SrcTextBoxes_TextChanged);
			GreenBox.KeyDown += new System.Windows.Forms.KeyEventHandler(GreenBox_KeyDown);
			//
			// RedBox
			//
			resources.ApplyResources(RedBox, "RedBox");
			RedBox.Name = "RedBox";
			RedBox.TextChanged += new System.EventHandler(SrcTextBoxes_TextChanged);
			RedBox.KeyDown += new System.Windows.Forms.KeyEventHandler(RedBox_KeyDown);
			//
			// Savebtn
			//
			resources.ApplyResources(Savebtn, "Savebtn");
			Savebtn.Name = "Savebtn";
			Savebtn.UseVisualStyleBackColor = true;
			//
			// button2
			//
			resources.ApplyResources(button2, "button2");
			button2.Name = "button2";
			button2.UseVisualStyleBackColor = true;
			button2.Click += new System.EventHandler(buttonCancel_Click);
			//
			// button1
			//
			resources.ApplyResources(button1, "button1");
			button1.Name = "button1";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(buttonOK_Click);
			//
			// controlsTab
			//
			controlsTab.BackColor = System.Drawing.SystemColors.Control;
			controlsTab.Controls.Add(fltrAuthorTxt);
			controlsTab.Controls.Add(fltrTitletxt);
			controlsTab.Controls.Add(fltrTitlelbl);
			controlsTab.Controls.Add(copylbl);
			controlsTab.Controls.Add(authlbl);
			controlsTab.Controls.Add(catlbl);
			controlsTab.Controls.Add(fltrCopyTxt);
			controlsTab.Controls.Add(fltrCatTxt);
			controlsTab.Controls.Add(ctlpanel);
			resources.ApplyResources(controlsTab, "controlsTab");
			controlsTab.Name = "controlsTab";
			//
			// fltrAuthorTxt
			//
			resources.ApplyResources(fltrAuthorTxt, "fltrAuthorTxt");
			fltrAuthorTxt.Name = "fltrAuthorTxt";
			//
			// fltrTitletxt
			//
			resources.ApplyResources(fltrTitletxt, "fltrTitletxt");
			fltrTitletxt.Name = "fltrTitletxt";
			//
			// fltrTitlelbl
			//
			resources.ApplyResources(fltrTitlelbl, "fltrTitlelbl");
			fltrTitlelbl.Name = "fltrTitlelbl";
			//
			// copylbl
			//
			resources.ApplyResources(copylbl, "copylbl");
			copylbl.Name = "copylbl";
			//
			// authlbl
			//
			resources.ApplyResources(authlbl, "authlbl");
			authlbl.Name = "authlbl";
			//
			// catlbl
			//
			resources.ApplyResources(catlbl, "catlbl");
			catlbl.Name = "catlbl";
			//
			// fltrCopyTxt
			//
			resources.ApplyResources(fltrCopyTxt, "fltrCopyTxt");
			fltrCopyTxt.Name = "fltrCopyTxt";
			//
			// fltrCatTxt
			//
			resources.ApplyResources(fltrCatTxt, "fltrCatTxt");
			fltrCatTxt.Name = "fltrCatTxt";
			//
			// ctlpanel
			//
			ctlpanel.BackColor = System.Drawing.SystemColors.Control;
			ctlpanel.Controls.Add(ctllbl2);
			ctlpanel.Controls.Add(resetbtn1);
			ctlpanel.Controls.Add(resetbtn7);
			ctlpanel.Controls.Add(resetbtn6);
			ctlpanel.Controls.Add(resetbtn5);
			ctlpanel.Controls.Add(resetbtn4);
			ctlpanel.Controls.Add(resetbtn3);
			ctlpanel.Controls.Add(resetbtn2);
			ctlpanel.Controls.Add(ctllbl7);
			ctlpanel.Controls.Add(ctllbl6);
			ctlpanel.Controls.Add(resetbtn0);
			ctlpanel.Controls.Add(ctllbl5);
			ctlpanel.Controls.Add(ctl7);
			ctlpanel.Controls.Add(ctllbl4);
			ctlpanel.Controls.Add(ctl6);
			ctlpanel.Controls.Add(ctllbl3);
			ctlpanel.Controls.Add(ctl5);
			ctlpanel.Controls.Add(ctl4);
			ctlpanel.Controls.Add(ctllbl1);
			ctlpanel.Controls.Add(ctl3);
			ctlpanel.Controls.Add(ctllbl0);
			ctlpanel.Controls.Add(ctl7_UpDown);
			ctlpanel.Controls.Add(map3_lbl);
			ctlpanel.Controls.Add(ctl6_UpDown);
			ctlpanel.Controls.Add(map2_lbl);
			ctlpanel.Controls.Add(ctl4_UpDown);
			ctlpanel.Controls.Add(ctl5_UpDown);
			ctlpanel.Controls.Add(map0_lbl);
			ctlpanel.Controls.Add(ctl3_UpDown);
			ctlpanel.Controls.Add(ctl2_UpDown);
			ctlpanel.Controls.Add(ctl1_UpDown);
			ctlpanel.Controls.Add(ctl0_UpDown);
			ctlpanel.Controls.Add(ctl2);
			ctlpanel.Controls.Add(ctl1);
			ctlpanel.Controls.Add(ctl0);
			ctlpanel.Controls.Add(map1_lbl);
			resources.ApplyResources(ctlpanel, "ctlpanel");
			ctlpanel.Name = "ctlpanel";
			//
			// ctllbl2
			//
			resources.ApplyResources(ctllbl2, "ctllbl2");
			ctllbl2.Name = "ctllbl2";
			//
			// resetbtn1
			//
			resources.ApplyResources(resetbtn1, "resetbtn1");
			resetbtn1.Name = "resetbtn1";
			resetbtn1.UseVisualStyleBackColor = true;
			resetbtn1.Click += new System.EventHandler(resetbtn_Click);
			//
			// resetbtn7
			//
			resources.ApplyResources(resetbtn7, "resetbtn7");
			resetbtn7.Name = "resetbtn7";
			resetbtn7.UseVisualStyleBackColor = true;
			resetbtn7.Click += new System.EventHandler(resetbtn_Click);
			//
			// resetbtn6
			//
			resources.ApplyResources(resetbtn6, "resetbtn6");
			resetbtn6.Name = "resetbtn6";
			resetbtn6.UseVisualStyleBackColor = true;
			resetbtn6.Click += new System.EventHandler(resetbtn_Click);
			//
			// resetbtn5
			//
			resources.ApplyResources(resetbtn5, "resetbtn5");
			resetbtn5.Name = "resetbtn5";
			resetbtn5.UseVisualStyleBackColor = true;
			resetbtn5.Click += new System.EventHandler(resetbtn_Click);
			//
			// resetbtn4
			//
			resources.ApplyResources(resetbtn4, "resetbtn4");
			resetbtn4.Name = "resetbtn4";
			resetbtn4.UseVisualStyleBackColor = true;
			resetbtn4.Click += new System.EventHandler(resetbtn_Click);
			//
			// resetbtn3
			//
			resources.ApplyResources(resetbtn3, "resetbtn3");
			resetbtn3.Name = "resetbtn3";
			resetbtn3.UseVisualStyleBackColor = true;
			resetbtn3.Click += new System.EventHandler(resetbtn_Click);
			//
			// resetbtn2
			//
			resources.ApplyResources(resetbtn2, "resetbtn2");
			resetbtn2.Name = "resetbtn2";
			resetbtn2.UseVisualStyleBackColor = true;
			resetbtn2.Click += new System.EventHandler(resetbtn_Click);
			//
			// ctllbl7
			//
			resources.ApplyResources(ctllbl7, "ctllbl7");
			ctllbl7.Name = "ctllbl7";
			//
			// ctllbl6
			//
			resources.ApplyResources(ctllbl6, "ctllbl6");
			ctllbl6.Name = "ctllbl6";
			//
			// resetbtn0
			//
			resources.ApplyResources(resetbtn0, "resetbtn0");
			resetbtn0.Name = "resetbtn0";
			resetbtn0.UseVisualStyleBackColor = true;
			resetbtn0.Click += new System.EventHandler(resetbtn_Click);
			//
			// ctllbl5
			//
			resources.ApplyResources(ctllbl5, "ctllbl5");
			ctllbl5.Name = "ctllbl5";
			//
			// ctl7
			//
			resources.ApplyResources(ctl7, "ctl7");
			ctl7.Maximum = 255;
			ctl7.Name = "ctl7";
			ctl7.TickStyle = System.Windows.Forms.TickStyle.None;
			ctl7.Scroll += new System.EventHandler(ctl7_ValueChanged);
			ctl7.ValueChanged += new System.EventHandler(ctl7_ValueChanged);
			//
			// ctllbl4
			//
			resources.ApplyResources(ctllbl4, "ctllbl4");
			ctllbl4.Name = "ctllbl4";
			//
			// ctl6
			//
			resources.ApplyResources(ctl6, "ctl6");
			ctl6.Maximum = 255;
			ctl6.Name = "ctl6";
			ctl6.TickStyle = System.Windows.Forms.TickStyle.None;
			ctl6.Scroll += new System.EventHandler(ctl6_ValueChanged);
			ctl6.ValueChanged += new System.EventHandler(ctl6_ValueChanged);
			//
			// ctllbl3
			//
			resources.ApplyResources(ctllbl3, "ctllbl3");
			ctllbl3.Name = "ctllbl3";
			//
			// ctl5
			//
			resources.ApplyResources(ctl5, "ctl5");
			ctl5.Maximum = 255;
			ctl5.Name = "ctl5";
			ctl5.TickStyle = System.Windows.Forms.TickStyle.None;
			ctl5.Scroll += new System.EventHandler(ctl5_ValueChanged);
			ctl5.ValueChanged += new System.EventHandler(ctl5_ValueChanged);
			//
			// ctl4
			//
			resources.ApplyResources(ctl4, "ctl4");
			ctl4.Maximum = 255;
			ctl4.Name = "ctl4";
			ctl4.TickStyle = System.Windows.Forms.TickStyle.None;
			ctl4.Scroll += new System.EventHandler(ctl4_ValueChanged);
			ctl4.ValueChanged += new System.EventHandler(ctl4_ValueChanged);
			//
			// ctllbl1
			//
			resources.ApplyResources(ctllbl1, "ctllbl1");
			ctllbl1.Name = "ctllbl1";
			//
			// ctl3
			//
			resources.ApplyResources(ctl3, "ctl3");
			ctl3.Maximum = 255;
			ctl3.Name = "ctl3";
			ctl3.TickStyle = System.Windows.Forms.TickStyle.None;
			ctl3.Scroll += new System.EventHandler(ctl3_ValueChanged);
			ctl3.ValueChanged += new System.EventHandler(ctl3_ValueChanged);
			//
			// ctllbl0
			//
			resources.ApplyResources(ctllbl0, "ctllbl0");
			ctllbl0.Name = "ctllbl0";
			//
			// ctl7_UpDown
			//
			resources.ApplyResources(ctl7_UpDown, "ctl7_UpDown");
			ctl7_UpDown.Maximum = new decimal(new int[] {
			255,
			0,
			0,
			0});
			ctl7_UpDown.Name = "ctl7_UpDown";
			ctl7_UpDown.ValueChanged += new System.EventHandler(ctl7_UpDown_ValueChanged);
			//
			// map3_lbl
			//
			resources.ApplyResources(map3_lbl, "map3_lbl");
			map3_lbl.Name = "map3_lbl";
			//
			// ctl6_UpDown
			//
			resources.ApplyResources(ctl6_UpDown, "ctl6_UpDown");
			ctl6_UpDown.Maximum = new decimal(new int[] {
			255,
			0,
			0,
			0});
			ctl6_UpDown.Name = "ctl6_UpDown";
			ctl6_UpDown.ValueChanged += new System.EventHandler(ctl6_UpDown_ValueChanged);
			//
			// map2_lbl
			//
			resources.ApplyResources(map2_lbl, "map2_lbl");
			map2_lbl.Name = "map2_lbl";
			//
			// ctl4_UpDown
			//
			resources.ApplyResources(ctl4_UpDown, "ctl4_UpDown");
			ctl4_UpDown.Maximum = new decimal(new int[] {
			255,
			0,
			0,
			0});
			ctl4_UpDown.Name = "ctl4_UpDown";
			ctl4_UpDown.ValueChanged += new System.EventHandler(ctl4_UpDown_ValueChanged);
			//
			// ctl5_UpDown
			//
			resources.ApplyResources(ctl5_UpDown, "ctl5_UpDown");
			ctl5_UpDown.Maximum = new decimal(new int[] {
			255,
			0,
			0,
			0});
			ctl5_UpDown.Name = "ctl5_UpDown";
			ctl5_UpDown.ValueChanged += new System.EventHandler(ctl5_UpDown_ValueChanged);
			//
			// map0_lbl
			//
			resources.ApplyResources(map0_lbl, "map0_lbl");
			map0_lbl.Name = "map0_lbl";
			//
			// ctl3_UpDown
			//
			resources.ApplyResources(ctl3_UpDown, "ctl3_UpDown");
			ctl3_UpDown.Maximum = new decimal(new int[] {
			255,
			0,
			0,
			0});
			ctl3_UpDown.Name = "ctl3_UpDown";
			ctl3_UpDown.ValueChanged += new System.EventHandler(ctl3_UpDown_ValueChanged);
			//
			// ctl2_UpDown
			//
			resources.ApplyResources(ctl2_UpDown, "ctl2_UpDown");
			ctl2_UpDown.Maximum = new decimal(new int[] {
			255,
			0,
			0,
			0});
			ctl2_UpDown.Name = "ctl2_UpDown";
			ctl2_UpDown.ValueChanged += new System.EventHandler(ctl2_UpDown_ValueChanged);
			//
			// ctl1_UpDown
			//
			resources.ApplyResources(ctl1_UpDown, "ctl1_UpDown");
			ctl1_UpDown.Maximum = new decimal(new int[] {
			255,
			0,
			0,
			0});
			ctl1_UpDown.Name = "ctl1_UpDown";
			ctl1_UpDown.ValueChanged += new System.EventHandler(ctl1_UpDown_ValueChanged);
			//
			// ctl0_UpDown
			//
			resources.ApplyResources(ctl0_UpDown, "ctl0_UpDown");
			ctl0_UpDown.Maximum = new decimal(new int[] {
			255,
			0,
			0,
			0});
			ctl0_UpDown.Name = "ctl0_UpDown";
			ctl0_UpDown.ValueChanged += new System.EventHandler(ctl0_UpDown_ValueChanged);
			//
			// ctl2
			//
			resources.ApplyResources(ctl2, "ctl2");
			ctl2.Maximum = 255;
			ctl2.Name = "ctl2";
			ctl2.TickStyle = System.Windows.Forms.TickStyle.None;
			ctl2.Scroll += new System.EventHandler(ctl2_ValueChanged);
			ctl2.ValueChanged += new System.EventHandler(ctl2_ValueChanged);
			//
			// ctl1
			//
			resources.ApplyResources(ctl1, "ctl1");
			ctl1.Maximum = 255;
			ctl1.Name = "ctl1";
			ctl1.TickStyle = System.Windows.Forms.TickStyle.None;
			ctl1.Scroll += new System.EventHandler(ctl1_ValueChanged);
			ctl1.ValueChanged += new System.EventHandler(ctl1_ValueChanged);
			//
			// ctl0
			//
			resources.ApplyResources(ctl0, "ctl0");
			ctl0.Maximum = 255;
			ctl0.Name = "ctl0";
			ctl0.TickStyle = System.Windows.Forms.TickStyle.None;
			ctl0.Scroll += new System.EventHandler(ctl0_ValueChanged);
			ctl0.ValueChanged += new System.EventHandler(ctl0_ValueChanged);
			//
			// map1_lbl
			//
			resources.ApplyResources(map1_lbl, "map1_lbl");
			map1_lbl.Name = "map1_lbl";
			//
			// tabControl1
			//
			tabControl1.Controls.Add(controlsTab);
			tabControl1.Controls.Add(editorTab);
			tabControl1.Controls.Add(FFLtab);
			tabControl1.Controls.Add(FilterManagertab);
			tabControl1.Controls.Add(FilterDirtab);
			resources.ApplyResources(tabControl1, "tabControl1");
			tabControl1.Name = "tabControl1";
			tabControl1.SelectedIndex = 0;
			//
			// buildfilterbtn
			//
			resources.ApplyResources(buildfilterbtn, "buildfilterbtn");
			buildfilterbtn.Name = "buildfilterbtn";
			buildfilterbtn.UseVisualStyleBackColor = true;
			buildfilterbtn.Click += new System.EventHandler(buildfilterbtn_Click);
			//
			// imageList1
			//
			imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			imageList1.Images.SetKeyName(0, "VSFolder_open.bmp");
			imageList1.Images.SetKeyName(1, "VSFolder_closed.bmp");
			//
			// PdnFFConfigDialog
			//
			resources.ApplyResources(this, "$this");
			Controls.Add(buildfilterbtn);
			Controls.Add(default_fltr);
			Controls.Add(buttonCancel);
			Controls.Add(Savebtn2);
			Controls.Add(Filenamelbl);
			Controls.Add(buttonOK);
			Controls.Add(tabControl1);
			Controls.Add(Loadbtn2);
			Controls.Add(Filenametxt);
			Name = "PdnFFConfigDialog";
			Controls.SetChildIndex(Filenametxt, 0);
			Controls.SetChildIndex(Loadbtn2, 0);
			Controls.SetChildIndex(tabControl1, 0);
			Controls.SetChildIndex(buttonOK, 0);
			Controls.SetChildIndex(Filenamelbl, 0);
			Controls.SetChildIndex(Savebtn2, 0);
			Controls.SetChildIndex(buttonCancel, 0);
			Controls.SetChildIndex(default_fltr, 0);
			Controls.SetChildIndex(buildfilterbtn, 0);
			FilterDirtab.ResumeLayout(false);
			FilterDirtab.PerformLayout();
			FilterManagertab.ResumeLayout(false);
			FilterManagertab.PerformLayout();
			filtermgrprogresspanel.ResumeLayout(false);
			filtermgrprogresspanel.PerformLayout();
			FFLtab.ResumeLayout(false);
			FFLtab.PerformLayout();
			editorTab.ResumeLayout(false);
			editorTab.PerformLayout();
			Interfacegb.ResumeLayout(false);
			Interfacegb.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(ctl5num)).EndInit();
			((System.ComponentModel.ISupportInitialize)(ctl4num)).EndInit();
			((System.ComponentModel.ISupportInitialize)(ctl6num)).EndInit();
			((System.ComponentModel.ISupportInitialize)(ctl7num)).EndInit();
			((System.ComponentModel.ISupportInitialize)(ctl3num)).EndInit();
			((System.ComponentModel.ISupportInitialize)(ctl2num)).EndInit();
			((System.ComponentModel.ISupportInitialize)(ctl1num)).EndInit();
			((System.ComponentModel.ISupportInitialize)(ctl0num)).EndInit();
			Infogb.ResumeLayout(false);
			Infogb.PerformLayout();
			controlsTab.ResumeLayout(false);
			controlsTab.PerformLayout();
			ctlpanel.ResumeLayout(false);
			ctlpanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(ctl7)).EndInit();
			((System.ComponentModel.ISupportInitialize)(ctl6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(ctl5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(ctl4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(ctl3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(ctl7_UpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(ctl6_UpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(ctl4_UpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(ctl5_UpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(ctl3_UpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(ctl2_UpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(ctl1_UpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(ctl0_UpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(ctl2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(ctl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(ctl0)).EndInit();
			tabControl1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			FinishTokenUpdate();
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private FilterData data = null;

		private void Loadbtn_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				LoadFilter(openFileDialog1.FileName);
			}
		}

		private void SetFilterInfoLabels(FilterData data)
		{
			fltrCatTxt.Text = data.Category;
			fltrTitletxt.Text = data.Title;
			fltrAuthorTxt.Text = data.Author;
			fltrCopyTxt.Text = data.Copyright;
		}

		private void ShowErrorMessage(string message)
		{
			if (InvokeRequired)
			{
				Invoke(new Action<string>(delegate (string text)
				{
					MessageBox.Show(this, text, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}), message);
			}
			else
			{
				MessageBox.Show(this, message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void LoadFilter(string FileName)
		{
			try
			{
				data = NewFilterData();
				SetControls(data); // set the edit controls to the empty data to force all the checkboxes to be unchecked
				ClearEditControls();
				if (FFLoadSave.LoadFile(FileName, out data))
				{
					Filenametxt.Text = Path.GetFileName(FileName);
					resetData = data.Clone();
					ResetPopDialog(data);
					SetControls(data);
					SetInfo(data);
					SetFilterInfoLabels(data);

					FinishTokenUpdate();
				}
			}
			catch (Exception ex)
			{
#if DEBUG
				ShowErrorMessage(ex.Message + Environment.NewLine + ex.StackTrace);
#else
				ShowErrorMessage(ex.Message);
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
				if (ffparse.ValidateSrc(src))
				{
					valid = true;
				}
			}
			return valid;
		}

		private void Savebtn_Click(object sender, EventArgs e)
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
				ShowErrorMessage(ex.Message);
			}
		}

		private void ctl0_UpDown_ValueChanged(object sender, EventArgs e)
		{
			if (ctl0_UpDown.Value < ctl0_UpDown.Minimum)
			{
				ctl0_UpDown.Value = (int)ctl0_UpDown.Minimum;
			}

			if (ctl0_UpDown.Value > ctl0_UpDown.Maximum)
			{
				ctl0_UpDown.Value = (int)ctl0_UpDown.Maximum;
			}

			ctl0.Value = (int)ctl0_UpDown.Value;
		}

		private void ctl0_ValueChanged(object sender, EventArgs e)
		{
			if (ctl0.Value < ctl0.Minimum)
			{
				ctl0.Value = ctl0.Minimum;
			}

			if (ctl0.Value > ctl0.Maximum)
			{
				ctl0.Value = ctl0.Maximum;
			}

			if ((int)ctl0_UpDown.Value != ctl0.Value)
			{
				ctl0_UpDown.Value = ctl0.Value;
			}

			if (data.ControlValue[0] != ctl0.Value)
			{
				data.ControlValue[0] = ctl0.Value;
				FinishTokenUpdate();
			}
		}

		private void ctl1_UpDown_ValueChanged(object sender, EventArgs e)
		{
			if (ctl1_UpDown.Value < ctl1_UpDown.Minimum)
			{
				ctl1_UpDown.Value = (int)ctl1_UpDown.Minimum;
			}

			if (ctl1_UpDown.Value > ctl1_UpDown.Maximum)
			{
				ctl1_UpDown.Value = (int)ctl1_UpDown.Maximum;
			}

			ctl1.Value = (int)ctl1_UpDown.Value;
		}

		private void ctl1_ValueChanged(object sender, EventArgs e)
		{
			if (ctl1.Value < ctl1.Minimum)
			{
				ctl1.Value = ctl1.Minimum;
			}

			if (ctl1.Value > ctl1.Maximum)
			{
				ctl1.Value = ctl1.Maximum;
			}

			if ((int)ctl1_UpDown.Value != ctl1.Value)
			{
				ctl1_UpDown.Value = ctl1.Value;
			}

			if (data.ControlValue[1] != ctl1.Value)
			{
				data.ControlValue[1] = ctl1.Value;
				FinishTokenUpdate();
			}
		}

		private void ctl2_UpDown_ValueChanged(object sender, EventArgs e)
		{
			if (ctl2_UpDown.Value < ctl2_UpDown.Minimum)
			{
				ctl2_UpDown.Value = (int)ctl2_UpDown.Minimum;
			}

			if (ctl2_UpDown.Value > ctl2_UpDown.Maximum)
			{
				ctl2_UpDown.Value = (int)ctl2_UpDown.Maximum;
			}

			ctl2.Value = (int)ctl2_UpDown.Value;
		}

		private void ctl2_ValueChanged(object sender, EventArgs e)
		{
			if (ctl2.Value < ctl2.Minimum)
			{
				ctl2.Value = ctl2.Minimum;
			}

			if (ctl2.Value > ctl2.Maximum)
			{
				ctl2.Value = ctl2.Maximum;
			}

			if ((int)ctl2_UpDown.Value != ctl2.Value)
			{
				ctl2_UpDown.Value = ctl2.Value;
			}

			if (data.ControlValue[2] != ctl2.Value)
			{
				data.ControlValue[2] = ctl2.Value;
				FinishTokenUpdate();
			}
		}

		private void ctl3_UpDown_ValueChanged(object sender, EventArgs e)
		{
			if (ctl3_UpDown.Value < ctl3_UpDown.Minimum)
			{
				ctl3_UpDown.Value = (int)ctl3_UpDown.Minimum;
			}

			if (ctl3_UpDown.Value > ctl3_UpDown.Maximum)
			{
				ctl3_UpDown.Value = (int)ctl3_UpDown.Maximum;
			}

			ctl3.Value = (int)ctl3_UpDown.Value;
		}

		private void ctl3_ValueChanged(object sender, EventArgs e)
		{
			if (ctl3.Value < ctl3.Minimum)
			{
				ctl3.Value = ctl3.Minimum;
			}

			if (ctl3.Value > ctl3.Maximum)
			{
				ctl3.Value = ctl3.Maximum;
			}

			if ((int)ctl3_UpDown.Value != ctl3.Value)
			{
				ctl3_UpDown.Value = ctl3.Value;
			}

			if (data.ControlValue[3] != ctl3.Value)
			{
				data.ControlValue[3] = ctl3.Value;
				FinishTokenUpdate();
			}
		}

		private void ctl4_UpDown_ValueChanged(object sender, EventArgs e)
		{
			if (ctl4_UpDown.Value < ctl4_UpDown.Minimum)
			{
				ctl4_UpDown.Value = (int)ctl4_UpDown.Minimum;
			}

			if (ctl4_UpDown.Value > ctl4_UpDown.Maximum)
			{
				ctl4_UpDown.Value = (int)ctl4_UpDown.Maximum;
			}

			ctl4.Value = (int)ctl4_UpDown.Value;
		}

		private void ctl4_ValueChanged(object sender, EventArgs e)
		{
			if (ctl4.Value < ctl4.Minimum)
			{
				ctl4.Value = ctl4.Minimum;
			}

			if (ctl4.Value > ctl4.Maximum)
			{
				ctl4.Value = ctl4.Maximum;
			}

			if ((int)ctl4_UpDown.Value != ctl4.Value)
			{
				ctl4_UpDown.Value = ctl4.Value;
			}

			if (data.ControlValue[4] != ctl4.Value)
			{
				data.ControlValue[4] = ctl4.Value;
				FinishTokenUpdate();
			}
		}

		private void ctl5_UpDown_ValueChanged(object sender, EventArgs e)
		{
			if (ctl5_UpDown.Value < ctl5_UpDown.Minimum)
			{
				ctl5_UpDown.Value = (int)ctl5_UpDown.Minimum;
			}

			if (ctl5_UpDown.Value > ctl5_UpDown.Maximum)
			{
				ctl5_UpDown.Value = (int)ctl5_UpDown.Maximum;
			}

			ctl5.Value = (int)ctl5_UpDown.Value;
		}

		private void ctl5_ValueChanged(object sender, EventArgs e)
		{
			if (ctl5.Value < ctl5.Minimum)
			{
				ctl5.Value = ctl5.Minimum;
			}

			if (ctl5.Value > ctl5.Maximum)
			{
				ctl5.Value = ctl5.Maximum;
			}

			if ((int)ctl5_UpDown.Value != ctl5.Value)
			{
				ctl5_UpDown.Value = ctl5.Value;
			}

			if (data.ControlValue[5] != ctl5.Value)
			{
				data.ControlValue[5] = ctl5.Value;
				FinishTokenUpdate();
			}
		}

		private void ctl6_UpDown_ValueChanged(object sender, EventArgs e)
		{
			if (ctl6_UpDown.Value < ctl6_UpDown.Minimum)
			{
				ctl6_UpDown.Value = (int)ctl6_UpDown.Minimum;
			}

			if (ctl6_UpDown.Value > ctl6_UpDown.Maximum)
			{
				ctl6_UpDown.Value = (int)ctl6_UpDown.Maximum;
			}

			ctl6.Value = (int)ctl6_UpDown.Value;
		}

		private void ctl6_ValueChanged(object sender, EventArgs e)
		{
			if (ctl6.Value < ctl6.Minimum)
			{
				ctl6.Value = ctl6.Minimum;
			}

			if (ctl6.Value > ctl6.Maximum)
			{
				ctl6.Value = ctl6.Maximum;
			}

			if ((int)ctl6_UpDown.Value != ctl6.Value)
			{
				ctl6_UpDown.Value = ctl6.Value;
			}

			if (data.ControlValue[6] != ctl6.Value)
			{
				data.ControlValue[6] = ctl6.Value;
				FinishTokenUpdate();
			}
		}

		private void ctl7_UpDown_ValueChanged(object sender, EventArgs e)
		{
			if (ctl7_UpDown.Value < ctl7_UpDown.Minimum)
			{
				ctl7_UpDown.Value = (int)ctl7_UpDown.Minimum;
			}

			if (ctl7_UpDown.Value > ctl7_UpDown.Maximum)
			{
				ctl7_UpDown.Value = (int)ctl7_UpDown.Maximum;
			}

			ctl7.Value = (int)ctl7_UpDown.Value;
		}

		private void ctl7_ValueChanged(object sender, EventArgs e)
		{
			if (ctl7.Value < ctl7.Minimum)
			{
				ctl7.Value = ctl7.Minimum;
			}

			if (ctl7.Value > ctl7.Maximum)
			{
				ctl7.Value = ctl7.Maximum;
			}

			if ((int)ctl7_UpDown.Value != ctl7.Value)
			{
				ctl7_UpDown.Value = ctl7.Value;
			}

			if (data.ControlValue[7] != ctl7.Value)
			{
				data.ControlValue[7] = ctl7.Value;
				FinishTokenUpdate();
			}
		}

		/// <summary>
		/// Resets the PopDialog function if the filter uses the maps or controls
		/// </summary>
		/// <param name="data">The filter_data to reset</param>
		private static void ResetPopDialog(FilterData data)
		{
			bool ctlused = false;
			bool mapused = false;

			for (int i = 0; i < 4; i++)
			{
				mapused |= data.MapEnable[i];
			}

			for (int i = 0; i < 8; i++)
			{
				ctlused |= data.ControlEnable[i];
			}

			if (ctlused || mapused)
			{
				data.PopDialog = true;
			}
			else
			{
				data.PopDialog = false;
			}
		}

		private void SetInfo(FilterData data)
		{
			RedBox.Text = data.Source[0];
			GreenBox.Text = data.Source[1];
			BlueBox.Text = data.Source[2];
			AlphaBox.Text = data.Source[3];
			AuthorBox.Text = data.Author;
			CategoryBox.Text = data.Category;
			CopyrightBox.Text = data.Copyright;
			TitleBox.Text = data.Title;
			if (!string.IsNullOrEmpty(data.FileName))
			{
				Filenametxt.Text = data.FileName;
			}
		}

		/// <summary>
		/// Set the control values
		/// </summary>
		/// <param name="data">The filter_data to set</param>
		private void SetControls(FilterData data)
		{
			SetControlsEnabled(data);
			SetControlValues(data);
		}

		private void SetControlsEnabled(FilterData data)
		{
			if (data.MapEnable[0] && !data.ControlEnable[0] && !data.ControlEnable[1])
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

				if (data.ControlEnable[0])
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

				if (data.ControlEnable[1])
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
				ctllbl0.Text = ctl0txt.Text = data.ControlLabel[0];
				ctllbl1.Text = ctl1txt.Text = data.ControlLabel[1];
			}
			if (data.MapEnable[1] && !data.ControlEnable[2] && !data.ControlEnable[3])
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

				if (data.ControlEnable[2])
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

				if (data.ControlEnable[3])
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
				ctllbl2.Text = ctl2txt.Text = data.ControlLabel[2];
				ctllbl3.Text = ctl3txt.Text = data.ControlLabel[3];
			}
			if (data.MapEnable[2] && !data.ControlEnable[4] && !data.ControlEnable[5])
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

				if (data.ControlEnable[4])
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

				if (data.ControlEnable[5])
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
				ctllbl4.Text = ctl4txt.Text = data.ControlLabel[4];
				ctllbl5.Text = ctl5txt.Text = data.ControlLabel[5];
			}

			if (data.MapEnable[3] && !data.ControlEnable[6] && !data.ControlEnable[7])
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

				if (data.ControlEnable[6])
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

				if (data.ControlEnable[7])
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
				ctllbl6.Text = ctl6txt.Text = data.ControlLabel[6];
				ctllbl7.Text = ctl7txt.Text = data.ControlLabel[7];
			}
		}

		private void SetControlValues(FilterData data)
		{
			ctl0num.Value = ctl0.Value = data.ControlValue[0];
			ctl1num.Value = ctl1.Value = data.ControlValue[1];
			ctl2num.Value = ctl2.Value = data.ControlValue[2];
			ctl3num.Value = ctl3.Value = data.ControlValue[3];
			ctl4num.Value = ctl4.Value = data.ControlValue[4];
			ctl5num.Value = ctl5.Value = data.ControlValue[5];
			ctl6num.Value = ctl6.Value = data.ControlValue[6];
			ctl7num.Value = ctl7.Value = data.ControlValue[7];
		}

		private void resetbtn_Click(object sender, EventArgs e)
		{
			if (resetData != null)
			{
				if (sender == resetbtn0)
				{
					ctl0.Value = resetData.ControlValue[0];
				}
				else if (sender == resetbtn1)
				{
					ctl1.Value = resetData.ControlValue[1];
				}
				else if (sender == resetbtn2)
				{
					ctl2.Value = resetData.ControlValue[2];
				}
				else if (sender == resetbtn3)
				{
					ctl3.Value = resetData.ControlValue[3];
				}
				else if (sender == resetbtn4)
				{
					ctl4.Value = resetData.ControlValue[4];
				}
				else if (sender == resetbtn5)
				{
					ctl5.Value = resetData.ControlValue[5];
				}
				else if (sender == resetbtn6)
				{
					ctl6.Value = resetData.ControlValue[6];
				}
				else if (sender == resetbtn7)
				{
					ctl7.Value = resetData.ControlValue[7];
				}
			}
		}

		private void default_fltr_Click(object sender, EventArgs e)
		{
			if (data == null)
			{
				data = new FilterData();
			}
			ClearLastFilters();
			data = FFLoadSave.DefaultFilter();
			SetControls(data);
			SetInfo(data);
			SetFilterInfoLabels(data);
			FinishTokenUpdate();
		}

		/// <summary>
		/// Clears the last loaded Filters
		/// </summary>
		private void ClearLastFilters()
		{
			Filenametxt.Text = string.Empty;
			clearFFLbtn_Click(null, null);
			resetData = null;
		}

		private void LoadSettings()
		{
			if (settings == null)
			{
				string dir = Services.GetService<PaintDotNet.AppModel.IAppInfoService>().UserDataDirectory;

				try
				{
					if (!Directory.Exists(dir))
					{
						Directory.CreateDirectory(dir);
					}

					settings = new PdnFFSettings(Path.Combine(dir, @"PdnFF.xml"));
				}
				catch (ArgumentException ex)
				{
					ShowErrorMessage(ex.Message);
				}
				catch (IOException ex)
				{
					ShowErrorMessage(ex.Message);
				}
				catch (NotSupportedException ex)
				{
					ShowErrorMessage(ex.Message);
				}
				catch (UnauthorizedAccessException ex)
				{
					ShowErrorMessage(ex.Message);
				}
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			LoadSettings();
			subdirSearchcb.Checked = settings.SearchSubdirectories;
			HashSet<string> dirs = settings.SearchDirectories;
			if (dirs != null)
			{
				searchDirectories.AddRange(dirs);
				DirlistView1.VirtualListSize = searchDirectories.Count;
				UpdateFilterList();
			}

			if (data != null)
			{
				SetControls(data);
				SetInfo(data);
				SetFilterInfoLabels(data);
			}
			else
			{
				default_fltr_Click(null, null);
			}
			PluginThemingUtil.EnableEffectDialogTheme(this);
		}

		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);

			PluginThemingUtil.UpdateControlBackColor(this);
		}

		protected override void OnForeColorChanged(EventArgs e)
		{
			base.OnForeColorChanged(e);

			PluginThemingUtil.UpdateControlForeColor(this);
			filterSearchBox.ForeColor = SystemColors.GrayText;
		}

		private void SrcTextBoxes_TextChanged(object sender, EventArgs e)
		{
			TextBox tb = sender as TextBox;
			string name = tb.Name.Substring(0, 1);

			if (string.IsNullOrEmpty(tb.Text))
			{
				ep.ContainerControl = this;
				ep.SetError(tb, Resources.ConfigDialog_FormulaEmptyError_Text);
			}
			else
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
					ep.SetError(tb, string.Empty);
					data.Source[ch] = tb.Text;
					FinishTokenUpdate();
				}
				else
				{
					ep.ContainerControl = this;
					ep.SetError(tb, Resources.ConfigDialog_FormulaSyntaxError_Text);
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
				throw new ArgumentOutOfRangeException(nameof(ch), Resources.ChannelValuesOutofRangeError);
			}
		}

		/// <summary>
		/// Loads a FFL library
		/// </summary>
		/// <param name="FileName">The FileName to load</param>
		private void LoadFFL(string FileName)
		{
			if (FFLtreeView1.Nodes.Count > 0)
			{
				FFLtreeView1.Nodes.Clear();
			}

			List<FilterData> filters;
			if (FFLoadSave.LoadFFL(FileName, out filters))
			{
				Dictionary<string, TreeNode> nodes = new Dictionary<string, TreeNode>();
				for (int i = 0; i < filters.Count; i++)
				{
					FilterData filter = filters[i];

					if (nodes.ContainsKey(filter.Category))
					{
						TreeNode node = nodes[filter.Category];

						TreeNode subnode = new TreeNode(filter.Title) { Name = filter.FileName, Tag = filter };
						node.Nodes.Add(subnode);
					}
					else
					{
						TreeNode node = new TreeNode(filter.Category);
						TreeNode subnode = new TreeNode(filter.Title) { Name = filter.FileName, Tag = filter };
						node.Nodes.Add(subnode);

						nodes.Add(filter.Category, node);
					}
				}

				FFLtreeView1.BeginUpdate();
				FFLtreeView1.TreeViewNodeSorter = null;
				foreach (var item in nodes)
				{
					FFLtreeView1.Nodes.Add(item.Value);
				}
				FFLtreeView1.TreeViewNodeSorter = TreeNodeItemComparer.Instance;
				FFLtreeView1.EndUpdate();

				fflnametxt.Text = Path.GetFileName(FileName);

				FFLfltrnumtxt.Text = filters.Count.ToString(CultureInfo.CurrentCulture);

				SetFilterInfoLabels(data);
			}
		}

		private void LoadFFLbtn_Click(object sender, EventArgs e)
		{
			try
			{
				using (OpenFileDialog fflDialog = new OpenFileDialog())
				{
					fflDialog.Multiselect = false;
					fflDialog.Filter = Resources.ConfigDialog_LoadFFLDialog_Filter;
					if (fflDialog.ShowDialog() == DialogResult.OK)
					{
						LoadFFL(fflDialog.FileName);
					}
				}
			}
			catch (Exception ex)
			{
				ShowErrorMessage(ex.Message);
			}
		}

		private void FFLtreeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (FFLtreeView1.SelectedNode.Tag != null)
			{
				TreeNode lvi = FFLtreeView1.SelectedNode;
				data = ((FilterData)lvi.Tag).Clone();
				resetData = data.Clone();
				SetControls(data);
				SetInfo(data);
				SetFilterInfoLabels(data);

				ffltreeauthtxt.Text = data.Author;
				ffltreecopytxt.Text = data.Copyright;
			}
		}

		private void SetEditControls(FilterData data, int index, bool setctlnum, bool ismaptxt)
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
				data.MapEnable[0] = true;
				if (!string.IsNullOrEmpty(map0txt.Text))
				{
					map0_lbl.Text = data.MapLabel[0] = map0txt.Text;
				}
			}
			else
			{
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
							data.ControlEnable[0] = true;
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
							data.ControlEnable[0] = false;
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
							data.ControlEnable[1] = true;
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
							data.ControlEnable[1] = false;
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
				data.MapEnable[1] = true;
				if (!string.IsNullOrEmpty(map1txt.Text))
				{
					map1_lbl.Text = data.MapLabel[1] = map1txt.Text;
				}
			}
			else
			{
				map1_lbl.Visible = false;

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

							data.ControlEnable[2] = true;
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
							data.ControlEnable[2] = false;
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
							data.ControlEnable[3] = true;
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
							data.ControlEnable[3] = false;
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
				data.MapEnable[2] = true;
				if (!string.IsNullOrEmpty(map2txt.Text))
				{
					map2_lbl.Text = data.MapLabel[2] = map2txt.Text;
				}
			}
			else
			{
				map2_lbl.Visible = false;

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
							data.ControlEnable[4] = true;
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
							data.ControlEnable[4] = false;
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
							data.ControlEnable[5] = true;
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
							data.ControlEnable[5] = false;
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
				data.MapEnable[3] = true;
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

							data.ControlEnable[6] = true;
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
							data.ControlEnable[6] = false;
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
							data.ControlEnable[7] = true;
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
							data.ControlEnable[7] = false;
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
			bool ismaptxt = cb.Name.StartsWith("MAP", StringComparison.OrdinalIgnoreCase);
			SetEditControls(data, ctlnum, false, ismaptxt);
		}

		private void ctlnum_ValueChanged(object sender, EventArgs e)
		{
			Control cb = sender as Control;
			int ctlnum = int.Parse(cb.Name.Substring(3, 1), CultureInfo.InvariantCulture);
			SetEditControls(data, ctlnum, true, false);
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

		private void UpdateSearchList()
		{
			if (settings != null)
			{
				settings.SearchDirectories = new HashSet<string>(searchDirectories, StringComparer.OrdinalIgnoreCase);
			}
		}

		/// <summary>
		/// The Parameter class for the UpdateFilterList Background Worker
		/// </summary>
		private sealed class UpdateFilterListParam
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

			public UpdateFilterListParam(ICollection<string> searchDirectories, bool searchSubDirectories)
			{
				dirlist = new string[searchDirectories.Count];
				searchDirectories.CopyTo(dirlist, 0);
				itemarr = null;
				options = searchSubDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
				itemcount = 0;
			}
		}

		/// <summary>
		/// Update the Filter Manager filterlist
		/// </summary>
		private void UpdateFilterList()
		{
			if (searchDirectories.Count > 0)
			{
#if DEBUG
				System.Diagnostics.Debug.WriteLine(string.Format("UpdateFilterListbw.IsBusy = {0}", UpdateFilterListbw.IsBusy.ToString()));
#endif
				if (!UpdateFilterListbw.IsBusy)
				{
					UpdateFilterListParam uflp = new UpdateFilterListParam(searchDirectories, subdirSearchcb.Checked);
					// Debug.WriteLine(string.Format("UpdateFilterList isbackground = {0}", Thread.CurrentThread.IsBackground.ToString()));
					fltrmgrprogress.Maximum = uflp.dirlist.Length;
					fltrmgrprogress.Step = 1;
					filtermgrprogresspanel.Visible = true;
					folderloadcountlbl.Text = string.Format(CultureInfo.InvariantCulture, "({0} of {1})", "0", searchDirectories.Count.ToString(CultureInfo.InvariantCulture));
					filtertreeview.Nodes.Clear();
					filterlistcnttxt.Text = string.Empty;

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
					searchDirectories.Add(DirBrowserDialog1.SelectedPath);
					DirlistView1.VirtualListSize = searchDirectories.Count;
					InvalidateDirectoryListViewCache(searchDirectories.Count);
					UpdateSearchList();
					UpdateFilterList();
				}
			}
		}

		private void remdirbtn_Click(object sender, EventArgs e)
		{
			if (DirlistView1.SelectedIndices.Count > 0)
			{
				if (!string.IsNullOrEmpty(Filenametxt.Text))
				{
					default_fltr_Click(null, null);
				}
				int index = DirlistView1.SelectedIndices[0];
				searchDirectories.RemoveAt(index);
				DirlistView1.VirtualListSize = searchDirectories.Count;
				InvalidateDirectoryListViewCache(index);
				UpdateSearchList();
				UpdateFilterList();
			}
		}

		private void clearFFLbtn_Click(object sender, EventArgs e)
		{
			if (FFLtreeView1.Nodes.Count > 0)
			{
				Filenametxt.Text = data.FileName = string.Empty;
				fflnametxt.Text = FFLfltrnumtxt.Text = string.Empty;
				FFLtreeView1.Nodes.Clear();
			}
		}

		private void subdirSearchcb_CheckedChanged(object sender, EventArgs e)
		{
			if (settings != null)
			{
				settings.SearchSubdirectories = subdirSearchcb.Checked;
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
			BackgroundWorker worker = (BackgroundWorker)sender;
			UpdateFilterListParam uflp = (UpdateFilterListParam)e.Argument;

			Dictionary<string, TreeNode> nodes = new Dictionary<string, TreeNode>();

			int count = 0;
			for (int i = 0; i < uflp.dirlist.Length; i++)
			{
				string path = uflp.dirlist[i];
				worker.ReportProgress(i, Path.GetFileName(path));

				using (FileEnumerator enumerator = new FileEnumerator(path, ".8bf", uflp.options, false))
				{
					while (enumerator.MoveNext())
					{
						if (worker.CancellationPending)
						{
							e.Cancel = true;
							return;
						}
						FilterData fd;
						if (FFLoadSave.LoadFrom8bf(enumerator.Current, out fd))
						{
							if (nodes.ContainsKey(fd.Category))
							{
								TreeNode node = nodes[fd.Category];

								TreeNode subnode = new TreeNode(fd.Title) { Name = enumerator.Current, Tag = fd }; // Title
								node.Nodes.Add(subnode);
							}
							else
							{
								TreeNode node = new TreeNode(fd.Category);
								TreeNode subnode = new TreeNode(fd.Title) { Name = enumerator.Current, Tag = fd }; // Title
								node.Nodes.Add(subnode);

								nodes.Add(fd.Category, node);
							}

							count++;
							//Debug.WriteLine(string.Format("Item name = {0}, Count = {1}", fi.Name, items.Count.ToString()));
						}
					}
				}
			}

#if DEBUG
			System.Diagnostics.Debug.WriteLine(string.Format("node count = {0}", nodes.Values.Count.ToString()));
#endif
			uflp.itemarr = new TreeNode[nodes.Values.Count];
			nodes.Values.CopyTo(uflp.itemarr, 0);
			uflp.itemcount = count;

			e.Result = uflp;
		}

		private Dictionary<TreeNode, string> FiltertreeviewItems = null; // used for the filter search list

		private void UpdateFilterListbw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				ShowErrorMessage(e.Error.Message);
			}
			else
			{
				if (!e.Cancelled) // has the worker been canceled
				{
					UpdateFilterListParam parm = (UpdateFilterListParam)e.Result;

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
					filtertreeview.TreeViewNodeSorter = null;
					filtertreeview.Nodes.AddRange(parm.itemarr);
					filtertreeview.TreeViewNodeSorter = TreeNodeItemComparer.Instance;
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
					System.Diagnostics.Debug.WriteLine("canceled");
				}
#endif

			}

			if (formClosePending)
			{
				Close(); // close the form
			}
		}

		private void UpdateFilterListbw_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			//Debug.WriteLine(string.Format("progresschanged isbackground = {0}", Thread.CurrentThread.IsBackground.ToString()));

			folderloadcountlbl.Text = string.Format(CultureInfo.InvariantCulture, "({0} of {1})", (e.ProgressPercentage + 1).ToString(CultureInfo.InvariantCulture), searchDirectories.Count.ToString(CultureInfo.InvariantCulture));
			folderloadnamelbl.Text = string.Format(CultureInfo.InvariantCulture, "({0})", e.UserState.ToString());
			fltrmgrprogress.PerformStep();
		}

		private bool formClosePending;

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if (UpdateFilterListbw.IsBusy && searchDirectories.Count > 0) // don't hold the form open if there are no search Dirs
			{
				UpdateFilterListbw.CancelAsync();
				e.Cancel = true;
				formClosePending = true;
			}

			if (!e.Cancel)
			{
				settings?.Flush();
			}

			base.OnFormClosing(e);
		}

		/// <summary>
		/// Filters the filtertreeview Items by the specified text
		/// </summary>
		/// <param name="filtertext">The keyword text to filter by</param>
		private void FilterTreeView(string filtertext)
		{
			if (FiltertreeviewItems.Count > 0)
			{
				Dictionary<string, TreeNode> nodes = new Dictionary<string, TreeNode>();
				foreach (KeyValuePair<TreeNode, string> item in FiltertreeviewItems)
				{
					TreeNode child = item.Key;
					string Title = child.Text;
					if ((string.IsNullOrEmpty(filtertext)) || Title.Contains(filtertext, StringComparison.OrdinalIgnoreCase))
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
				filtertreeview.TreeViewNodeSorter = TreeNodeItemComparer.Instance;
				filtertreeview.EndUpdate();
			}
		}

		private void filtertreeview_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (filtertreeview.SelectedNode.Tag != null)
			{
				FilterData filter = (FilterData)filtertreeview.SelectedNode.Tag;
				treefltrauthtxt.Text = filter.Author;
				treefltrcopytxt.Text = filter.Copyright;

				data = filter.Clone();
				resetData = data.Clone();
				SetControls(data);
				SetInfo(data);
				SetFilterInfoLabels(data);
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
				filterSearchBox.ForeColor = ForeColor != DefaultForeColor ? ForeColor : SystemColors.WindowText;
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

		private void buildfilterbtn_Click(object sender, EventArgs e)
		{
			if (data != null && !string.IsNullOrEmpty(data.Author))
			{
				try
				{
					string FileName = string.Empty;
					if (!string.IsNullOrEmpty(Filenametxt.Text))
					{
						FileName = Path.GetFileNameWithoutExtension(Filenametxt.Text);
					}
					else
					{
						FileName = Path.GetFileName(TitleBox.Text);
					}

					FileName += ".dll";

					Version pdnVersion = Services.GetService<PaintDotNet.AppModel.IAppInfoService>().AppVersion;

					using (FilterBuilder builder = new FilterBuilder(data, pdnVersion))
					{
						string fullPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), FileName);

						string error = builder.Build(fullPath);

						if (!string.IsNullOrEmpty(error))
						{
							ShowErrorMessage(error);
						}
						else
						{
							MessageBox.Show(this, string.Format("Filter built successfully.\n\nYou will need copy {0} from your Desktop into the Paint.NET Effects folder and to restart Paint.NET to see it in the Effects menu.", Path.GetFileName(FileName)), Text);
						}
					}
				}
				catch (Exception ex)
				{
#if DEBUG
					ShowErrorMessage(ex.Message + Environment.NewLine + ex.StackTrace);
#else
					ShowErrorMessage(ex.Message);
#endif

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

		private void DirlistView1_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
		{
			// Check if the cache needs to be refreshed.
			if (searchDirListViewCache != null && e.StartIndex >= cacheStartIndex && e.EndIndex <= cacheStartIndex + searchDirListViewCache.Length)
			{
				// If the newly requested cache is a subset of the old cache,
				// no need to rebuild everything, so do nothing.
				return;
			}

			cacheStartIndex = e.StartIndex;
			// The indexes are inclusive.
			int length = e.EndIndex - e.StartIndex + 1;
			searchDirListViewCache = new ListViewItem[length];

			// Fill the cache with the appropriate ListViewItems.
			for (int i = 0; i < length; i++)
			{
				searchDirListViewCache[i] = new ListViewItem(searchDirectories[i + cacheStartIndex]);
			}
		}

		private void DirlistView1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
		{
			if (searchDirListViewCache != null && e.ItemIndex >= cacheStartIndex && e.ItemIndex < cacheStartIndex + searchDirListViewCache.Length)
			{
				e.Item = searchDirListViewCache[e.ItemIndex - cacheStartIndex];
			}
			else
			{
				e.Item = new ListViewItem(searchDirectories[e.ItemIndex]);
			}
		}

		private void InvalidateDirectoryListViewCache(int changedIndex)
		{
			if (searchDirListViewCache != null)
			{
				int endIndex = cacheStartIndex + searchDirListViewCache.Length;
				if (changedIndex >= cacheStartIndex && changedIndex <= endIndex)
				{
					searchDirListViewCache = null;

					if (DirlistView1.VirtualListSize > 0)
					{
						if (endIndex > DirlistView1.VirtualListSize)
						{
							endIndex = DirlistView1.VirtualListSize;
						}
						// The indexes in the CacheVirtualItems event are inclusive,
						// so we need to subtract 1 from the end index.
						DirlistView1_CacheVirtualItems(this, new CacheVirtualItemsEventArgs(cacheStartIndex, endIndex - 1));
					}
				}

				DirlistView1.Invalidate();
			}
		}
	}
}
