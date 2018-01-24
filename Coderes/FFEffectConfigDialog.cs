/*
*  This file is part of pdn-filter-factory, a Paint.NET Effect that
*  interprets Filter Factory-based Adobe Photoshop filters.
*
*  Copyright (C) 2010, 2011, 2012, 2015, 2018 Nicholas Hayes
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
using System.Text.RegularExpressions;
using System.Windows.Forms;
using PaintDotNet.Effects;
using System.Diagnostics;
using System.IO;
using PaintDotNet.AppModel;
using System.Globalization;

namespace FFEffect
{
    public class FFEffectConfigDialog : PaintDotNet.Effects.EffectConfigDialog
    {
        private Button buttonOK;
        private Panel ctlpanel;
        private Label ctllbl2;
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
        private TrackBar ctl4;
        private Label ctllbl1;
        private TrackBar ctl3;
        private Label ctllbl0;
        private NumericUpDown ctl7_UpDown;
        private Label map3_lbl;
        private NumericUpDown ctl6_UpDown;
        private Label map2_lbl;
        private NumericUpDown ctl4_UpDown;
        private NumericUpDown ctl5_UpDown;
        private Label map0_lbl;
        private NumericUpDown ctl3_UpDown;
        private NumericUpDown ctl2_UpDown;
        private NumericUpDown ctl1_UpDown;
        private NumericUpDown ctl0_UpDown;
        private TrackBar ctl2;
        private TrackBar ctl1;
        private TrackBar ctl0;
        private Label map1_lbl;
        private Label authlbl;
        private Label copylbl;
        private Label authtxt;
        private Label copytxt;
        private Button buttonCancel;


        private static FilterData data = new FilterData();
        private bool firstTokenInit = true;
        public FFEffectConfigDialog(FilterData staticdata)
        {
            InitializeComponent();
            data = staticdata;

            Array.Copy(staticdata.ControlValue, resetdata, 8);

            this.Text = data.Title;
            this.authtxt.Text = data.Author;
            this.copytxt.Text = data.Copyright;

            SetControls(data);

            base.PerformLayout();
            this.ResizetoVisibleControls();
        }

        protected override void InitialInitToken()
        {
            theEffectToken = new FFEffectConfigToken(data.ControlValue);
        }

        protected override void InitTokenFromDialog()
        {
            ((FFEffectConfigToken)EffectToken).ctlvalues = data.ControlValue;
        }

        protected override void InitDialogFromToken(EffectConfigToken effectToken)
        {
            if (firstTokenInit)
            {
                firstTokenInit = false;
                return; // Bail since the values in data.ControlValue have not been copied to the token.
            }

            if (effectToken is FFEffectConfigToken)
            {
                FFEffectConfigToken token = (FFEffectConfigToken)effectToken;
                data.ControlValue = token.ctlvalues;
            }
        }



        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FFEffectConfigDialog));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.ctlpanel = new System.Windows.Forms.Panel();
            this.ctl4_UpDown = new System.Windows.Forms.NumericUpDown();
            this.ctl3_UpDown = new System.Windows.Forms.NumericUpDown();
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
            this.ctl5_UpDown = new System.Windows.Forms.NumericUpDown();
            this.map0_lbl = new System.Windows.Forms.Label();
            this.ctl2_UpDown = new System.Windows.Forms.NumericUpDown();
            this.ctl1_UpDown = new System.Windows.Forms.NumericUpDown();
            this.ctl0_UpDown = new System.Windows.Forms.NumericUpDown();
            this.ctl2 = new System.Windows.Forms.TrackBar();
            this.ctl1 = new System.Windows.Forms.TrackBar();
            this.ctl0 = new System.Windows.Forms.TrackBar();
            this.map1_lbl = new System.Windows.Forms.Label();
            this.authlbl = new System.Windows.Forms.Label();
            this.copylbl = new System.Windows.Forms.Label();
            this.authtxt = new System.Windows.Forms.Label();
            this.copytxt = new System.Windows.Forms.Label();
            this.ctlpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctl4_UpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl3_UpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl7_UpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl6_UpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl5_UpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl2_UpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl1_UpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl0_UpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl0)).BeginInit();
            this.SuspendLayout();
            //
            // buttonCancel
            //
            this.buttonCancel.Location = new System.Drawing.Point(339, 271);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            //
            // buttonOK
            //
            this.buttonOK.Location = new System.Drawing.Point(258, 271);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            //
            // ctlpanel
            //
            this.ctlpanel.BackColor = System.Drawing.SystemColors.Control;
            this.ctlpanel.Controls.Add(this.ctl7);
            this.ctlpanel.Controls.Add(this.ctl6);
            this.ctlpanel.Controls.Add(this.ctl5);
            this.ctlpanel.Controls.Add(this.ctl4);
            this.ctlpanel.Controls.Add(this.ctl3);
            this.ctlpanel.Controls.Add(this.ctl2);
            this.ctlpanel.Controls.Add(this.ctl1);
            this.ctlpanel.Controls.Add(this.ctl4_UpDown);
            this.ctlpanel.Controls.Add(this.ctl3_UpDown);
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
            this.ctlpanel.Controls.Add(this.ctllbl4);
            this.ctlpanel.Controls.Add(this.ctllbl3);
            this.ctlpanel.Controls.Add(this.ctllbl1);
            this.ctlpanel.Controls.Add(this.ctllbl0);
            this.ctlpanel.Controls.Add(this.ctl7_UpDown);
            this.ctlpanel.Controls.Add(this.map3_lbl);
            this.ctlpanel.Controls.Add(this.ctl6_UpDown);
            this.ctlpanel.Controls.Add(this.map2_lbl);
            this.ctlpanel.Controls.Add(this.ctl5_UpDown);
            this.ctlpanel.Controls.Add(this.map0_lbl);
            this.ctlpanel.Controls.Add(this.ctl2_UpDown);
            this.ctlpanel.Controls.Add(this.ctl1_UpDown);
            this.ctlpanel.Controls.Add(this.ctl0_UpDown);
            this.ctlpanel.Controls.Add(this.ctl0);
            this.ctlpanel.Controls.Add(this.map1_lbl);
            this.ctlpanel.Location = new System.Drawing.Point(12, 12);
            this.ctlpanel.Name = "ctlpanel";
            this.ctlpanel.Size = new System.Drawing.Size(467, 234);
            this.ctlpanel.TabIndex = 17;
            //
            // ctl4_UpDown
            //
            this.ctl4_UpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctl4_UpDown.Location = new System.Drawing.Point(294, 98);
            this.ctl4_UpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ctl4_UpDown.Name = "ctl4_UpDown";
            this.ctl4_UpDown.Size = new System.Drawing.Size(81, 20);
            this.ctl4_UpDown.TabIndex = 19;
            this.ctl4_UpDown.ValueChanged += new System.EventHandler(this.ctl4_UpDown_ValueChanged);
            //
            // ctl3_UpDown
            //
            this.ctl3_UpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctl3_UpDown.Location = new System.Drawing.Point(294, 76);
            this.ctl3_UpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ctl3_UpDown.Name = "ctl3_UpDown";
            this.ctl3_UpDown.Size = new System.Drawing.Size(81, 20);
            this.ctl3_UpDown.TabIndex = 14;
            this.ctl3_UpDown.ValueChanged += new System.EventHandler(this.ctl3_UpDown_ValueChanged);
            //
            // ctllbl2
            //
            this.ctllbl2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctllbl2.Location = new System.Drawing.Point(4, 54);
            this.ctllbl2.Name = "ctllbl2";
            this.ctllbl2.Size = new System.Drawing.Size(115, 20);
            this.ctllbl2.TabIndex = 18;
            this.ctllbl2.Text = "Control 2";
            //
            // resetbtn1
            //
            this.resetbtn1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resetbtn1.BackgroundImage")));
            this.resetbtn1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.resetbtn1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.resetbtn1.Location = new System.Drawing.Point(383, 31);
            this.resetbtn1.Name = "resetbtn1";
            this.resetbtn1.Size = new System.Drawing.Size(20, 20);
            this.resetbtn1.TabIndex = 34;
            this.resetbtn1.UseVisualStyleBackColor = true;
            this.resetbtn1.Click += new System.EventHandler(this.resetbtn_Click);
            //
            // resetbtn7
            //
            this.resetbtn7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resetbtn7.BackgroundImage")));
            this.resetbtn7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.resetbtn7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.resetbtn7.Location = new System.Drawing.Point(383, 171);
            this.resetbtn7.Name = "resetbtn7";
            this.resetbtn7.Size = new System.Drawing.Size(20, 20);
            this.resetbtn7.TabIndex = 33;
            this.resetbtn7.UseVisualStyleBackColor = true;
            this.resetbtn7.Click += new System.EventHandler(this.resetbtn_Click);
            //
            // resetbtn6
            //
            this.resetbtn6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resetbtn6.BackgroundImage")));
            this.resetbtn6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.resetbtn6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.resetbtn6.Location = new System.Drawing.Point(383, 145);
            this.resetbtn6.Name = "resetbtn6";
            this.resetbtn6.Size = new System.Drawing.Size(20, 20);
            this.resetbtn6.TabIndex = 32;
            this.resetbtn6.UseVisualStyleBackColor = true;
            this.resetbtn6.Click += new System.EventHandler(this.resetbtn_Click);
            //
            // resetbtn5
            //
            this.resetbtn5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resetbtn5.BackgroundImage")));
            this.resetbtn5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.resetbtn5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.resetbtn5.Location = new System.Drawing.Point(383, 124);
            this.resetbtn5.Name = "resetbtn5";
            this.resetbtn5.Size = new System.Drawing.Size(20, 20);
            this.resetbtn5.TabIndex = 31;
            this.resetbtn5.UseVisualStyleBackColor = true;
            this.resetbtn5.Click += new System.EventHandler(this.resetbtn_Click);
            //
            // resetbtn4
            //
            this.resetbtn4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resetbtn4.BackgroundImage")));
            this.resetbtn4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.resetbtn4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.resetbtn4.Location = new System.Drawing.Point(383, 100);
            this.resetbtn4.Name = "resetbtn4";
            this.resetbtn4.Size = new System.Drawing.Size(20, 20);
            this.resetbtn4.TabIndex = 30;
            this.resetbtn4.UseVisualStyleBackColor = true;
            this.resetbtn4.Click += new System.EventHandler(this.resetbtn_Click);
            //
            // resetbtn3
            //
            this.resetbtn3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resetbtn3.BackgroundImage")));
            this.resetbtn3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.resetbtn3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.resetbtn3.Location = new System.Drawing.Point(383, 78);
            this.resetbtn3.Name = "resetbtn3";
            this.resetbtn3.Size = new System.Drawing.Size(20, 20);
            this.resetbtn3.TabIndex = 29;
            this.resetbtn3.UseVisualStyleBackColor = true;
            this.resetbtn3.Click += new System.EventHandler(this.resetbtn_Click);
            //
            // resetbtn2
            //
            this.resetbtn2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resetbtn2.BackgroundImage")));
            this.resetbtn2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.resetbtn2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.resetbtn2.Location = new System.Drawing.Point(383, 54);
            this.resetbtn2.Name = "resetbtn2";
            this.resetbtn2.Size = new System.Drawing.Size(20, 20);
            this.resetbtn2.TabIndex = 28;
            this.resetbtn2.UseVisualStyleBackColor = true;
            this.resetbtn2.Click += new System.EventHandler(this.resetbtn_Click);
            //
            // ctllbl7
            //
            this.ctllbl7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctllbl7.Location = new System.Drawing.Point(4, 173);
            this.ctllbl7.Name = "ctllbl7";
            this.ctllbl7.Size = new System.Drawing.Size(115, 20);
            this.ctllbl7.TabIndex = 23;
            this.ctllbl7.Text = "Control 7";
            //
            // ctllbl6
            //
            this.ctllbl6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctllbl6.Location = new System.Drawing.Point(4, 147);
            this.ctllbl6.Name = "ctllbl6";
            this.ctllbl6.Size = new System.Drawing.Size(115, 20);
            this.ctllbl6.TabIndex = 22;
            this.ctllbl6.Text = "Control 6";
            //
            // resetbtn0
            //
            this.resetbtn0.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resetbtn0.BackgroundImage")));
            this.resetbtn0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.resetbtn0.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.resetbtn0.Location = new System.Drawing.Point(383, 6);
            this.resetbtn0.Name = "resetbtn0";
            this.resetbtn0.Size = new System.Drawing.Size(20, 20);
            this.resetbtn0.TabIndex = 24;
            this.resetbtn0.UseVisualStyleBackColor = true;
            this.resetbtn0.Click += new System.EventHandler(this.resetbtn_Click);
            //
            // ctllbl5
            //
            this.ctllbl5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctllbl5.Location = new System.Drawing.Point(3, 124);
            this.ctllbl5.Name = "ctllbl5";
            this.ctllbl5.Size = new System.Drawing.Size(115, 20);
            this.ctllbl5.TabIndex = 21;
            this.ctllbl5.Text = "Control 5";
            //
            // ctl7
            //
            this.ctl7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctl7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctl7.Location = new System.Drawing.Point(121, 168);
            this.ctl7.Maximum = 255;
            this.ctl7.Name = "ctl7";
            this.ctl7.Size = new System.Drawing.Size(171, 45);
            this.ctl7.TabIndex = 7;
            this.ctl7.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ctl7.ValueChanged += new System.EventHandler(this.ctl7_ValueChanged);
            //
            // ctllbl4
            //
            this.ctllbl4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctllbl4.Location = new System.Drawing.Point(3, 100);
            this.ctllbl4.Name = "ctllbl4";
            this.ctllbl4.Size = new System.Drawing.Size(115, 20);
            this.ctllbl4.TabIndex = 20;
            this.ctllbl4.Text = "Control 4";
            //
            // ctl6
            //
            this.ctl6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctl6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctl6.Location = new System.Drawing.Point(120, 145);
            this.ctl6.Maximum = 255;
            this.ctl6.Name = "ctl6";
            this.ctl6.Size = new System.Drawing.Size(171, 45);
            this.ctl6.TabIndex = 10;
            this.ctl6.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ctl6.ValueChanged += new System.EventHandler(this.ctl6_ValueChanged);
            //
            // ctllbl3
            //
            this.ctllbl3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctllbl3.Location = new System.Drawing.Point(4, 78);
            this.ctllbl3.Name = "ctllbl3";
            this.ctllbl3.Size = new System.Drawing.Size(115, 20);
            this.ctllbl3.TabIndex = 19;
            this.ctllbl3.Text = "Control 3";
            //
            // ctl5
            //
            this.ctl5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctl5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctl5.Location = new System.Drawing.Point(120, 122);
            this.ctl5.Maximum = 255;
            this.ctl5.Name = "ctl5";
            this.ctl5.Size = new System.Drawing.Size(171, 45);
            this.ctl5.TabIndex = 6;
            this.ctl5.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ctl5.ValueChanged += new System.EventHandler(this.ctl5_ValueChanged);
            //
            // ctl4
            //
            this.ctl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctl4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctl4.Location = new System.Drawing.Point(120, 98);
            this.ctl4.Maximum = 255;
            this.ctl4.Name = "ctl4";
            this.ctl4.Size = new System.Drawing.Size(171, 45);
            this.ctl4.TabIndex = 19;
            this.ctl4.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ctl4.ValueChanged += new System.EventHandler(this.ctl4_ValueChanged);
            //
            // ctllbl1
            //
            this.ctllbl1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctllbl1.Location = new System.Drawing.Point(4, 31);
            this.ctllbl1.Name = "ctllbl1";
            this.ctllbl1.Size = new System.Drawing.Size(115, 20);
            this.ctllbl1.TabIndex = 17;
            this.ctllbl1.Text = "Control 1";
            //
            // ctl3
            //
            this.ctl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctl3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctl3.Location = new System.Drawing.Point(120, 75);
            this.ctl3.Maximum = 255;
            this.ctl3.Name = "ctl3";
            this.ctl3.Size = new System.Drawing.Size(171, 45);
            this.ctl3.TabIndex = 8;
            this.ctl3.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ctl3.ValueChanged += new System.EventHandler(this.ctl3_ValueChanged);
            //
            // ctllbl0
            //
            this.ctllbl0.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctllbl0.Location = new System.Drawing.Point(4, 6);
            this.ctllbl0.Name = "ctllbl0";
            this.ctllbl0.Size = new System.Drawing.Size(115, 20);
            this.ctllbl0.TabIndex = 16;
            this.ctllbl0.Text = "Control 0";
            //
            // ctl7_UpDown
            //
            this.ctl7_UpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctl7_UpDown.Location = new System.Drawing.Point(294, 171);
            this.ctl7_UpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ctl7_UpDown.Name = "ctl7_UpDown";
            this.ctl7_UpDown.Size = new System.Drawing.Size(81, 20);
            this.ctl7_UpDown.TabIndex = 18;
            this.ctl7_UpDown.ValueChanged += new System.EventHandler(this.ctl7_UpDown_ValueChanged);
            //
            // map3_lbl
            //
            this.map3_lbl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.map3_lbl.Location = new System.Drawing.Point(4, 162);
            this.map3_lbl.Name = "map3_lbl";
            this.map3_lbl.Size = new System.Drawing.Size(114, 20);
            this.map3_lbl.TabIndex = 27;
            this.map3_lbl.Text = "map3";
            this.map3_lbl.Visible = false;
            //
            // ctl6_UpDown
            //
            this.ctl6_UpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctl6_UpDown.Location = new System.Drawing.Point(294, 145);
            this.ctl6_UpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ctl6_UpDown.Name = "ctl6_UpDown";
            this.ctl6_UpDown.Size = new System.Drawing.Size(81, 20);
            this.ctl6_UpDown.TabIndex = 17;
            this.ctl6_UpDown.ValueChanged += new System.EventHandler(this.ctl6_UpDown_ValueChanged);
            //
            // map2_lbl
            //
            this.map2_lbl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.map2_lbl.Location = new System.Drawing.Point(4, 113);
            this.map2_lbl.Name = "map2_lbl";
            this.map2_lbl.Size = new System.Drawing.Size(114, 20);
            this.map2_lbl.TabIndex = 26;
            this.map2_lbl.Text = "Map 2";
            this.map2_lbl.Visible = false;
            //
            // ctl5_UpDown
            //
            this.ctl5_UpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctl5_UpDown.Location = new System.Drawing.Point(294, 122);
            this.ctl5_UpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ctl5_UpDown.Name = "ctl5_UpDown";
            this.ctl5_UpDown.Size = new System.Drawing.Size(81, 20);
            this.ctl5_UpDown.TabIndex = 16;
            this.ctl5_UpDown.ValueChanged += new System.EventHandler(this.ctl5_UpDown_ValueChanged);
            //
            // map0_lbl
            //
            this.map0_lbl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.map0_lbl.Location = new System.Drawing.Point(4, 24);
            this.map0_lbl.Name = "map0_lbl";
            this.map0_lbl.Size = new System.Drawing.Size(115, 20);
            this.map0_lbl.TabIndex = 24;
            this.map0_lbl.Text = "Map 0";
            this.map0_lbl.Visible = false;
            //
            // ctl2_UpDown
            //
            this.ctl2_UpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctl2_UpDown.Location = new System.Drawing.Point(294, 51);
            this.ctl2_UpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ctl2_UpDown.Name = "ctl2_UpDown";
            this.ctl2_UpDown.Size = new System.Drawing.Size(81, 20);
            this.ctl2_UpDown.TabIndex = 13;
            this.ctl2_UpDown.ValueChanged += new System.EventHandler(this.ctl2_UpDown_ValueChanged);
            //
            // ctl1_UpDown
            //
            this.ctl1_UpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctl1_UpDown.Location = new System.Drawing.Point(294, 29);
            this.ctl1_UpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ctl1_UpDown.Name = "ctl1_UpDown";
            this.ctl1_UpDown.Size = new System.Drawing.Size(81, 20);
            this.ctl1_UpDown.TabIndex = 12;
            this.ctl1_UpDown.ValueChanged += new System.EventHandler(this.ctl1_UpDown_ValueChanged);
            //
            // ctl0_UpDown
            //
            this.ctl0_UpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctl0_UpDown.Location = new System.Drawing.Point(294, 4);
            this.ctl0_UpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ctl0_UpDown.Name = "ctl0_UpDown";
            this.ctl0_UpDown.Size = new System.Drawing.Size(81, 20);
            this.ctl0_UpDown.TabIndex = 11;
            this.ctl0_UpDown.ValueChanged += new System.EventHandler(this.ctl0_UpDown_ValueChanged);
            //
            // ctl2
            //
            this.ctl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctl2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctl2.Location = new System.Drawing.Point(120, 49);
            this.ctl2.Maximum = 255;
            this.ctl2.Name = "ctl2";
            this.ctl2.Size = new System.Drawing.Size(171, 45);
            this.ctl2.TabIndex = 4;
            this.ctl2.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ctl2.ValueChanged += new System.EventHandler(this.ctl2_ValueChanged);
            //
            // ctl1
            //
            this.ctl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctl1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctl1.Location = new System.Drawing.Point(120, 24);
            this.ctl1.Maximum = 255;
            this.ctl1.Name = "ctl1";
            this.ctl1.Size = new System.Drawing.Size(171, 45);
            this.ctl1.TabIndex = 9;
            this.ctl1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ctl1.ValueChanged += new System.EventHandler(this.ctl1_ValueChanged);
            //
            // ctl0
            //
            this.ctl0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctl0.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctl0.Location = new System.Drawing.Point(120, 0);
            this.ctl0.Maximum = 255;
            this.ctl0.Name = "ctl0";
            this.ctl0.Size = new System.Drawing.Size(171, 45);
            this.ctl0.TabIndex = 3;
            this.ctl0.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ctl0.ValueChanged += new System.EventHandler(this.ctl0_ValueChanged);
            //
            // map1_lbl
            //
            this.map1_lbl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.map1_lbl.Location = new System.Drawing.Point(4, 69);
            this.map1_lbl.Name = "map1_lbl";
            this.map1_lbl.Size = new System.Drawing.Size(115, 18);
            this.map1_lbl.TabIndex = 25;
            this.map1_lbl.Text = "Map 1";
            this.map1_lbl.Visible = false;
            //
            // authlbl
            //
            this.authlbl.AutoSize = true;
            this.authlbl.Location = new System.Drawing.Point(12, 232);
            this.authlbl.Name = "authlbl";
            this.authlbl.Size = new System.Drawing.Size(41, 13);
            this.authlbl.TabIndex = 13;
            this.authlbl.Text = "Author:";
            //
            // copylbl
            //
            this.copylbl.AutoSize = true;
            this.copylbl.Location = new System.Drawing.Point(12, 249);
            this.copylbl.Name = "copylbl";
            this.copylbl.Size = new System.Drawing.Size(54, 13);
            this.copylbl.TabIndex = 14;
            this.copylbl.Text = "Copyright:";
            //
            // authtxt
            //
            this.authtxt.AutoSize = true;
            this.authtxt.Location = new System.Drawing.Point(60, 232);
            this.authtxt.Name = "authtxt";
            this.authtxt.Size = new System.Drawing.Size(28, 13);
            this.authtxt.TabIndex = 15;
            this.authtxt.Text = "auth";
            //
            // copytxt
            //
            this.copytxt.AutoSize = true;
            this.copytxt.Location = new System.Drawing.Point(73, 249);
            this.copytxt.Name = "copytxt";
            this.copytxt.Size = new System.Drawing.Size(30, 13);
            this.copytxt.TabIndex = 16;
            this.copytxt.Text = "copy";
            //
            // FFEffectConfigDialog
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.ClientSize = new System.Drawing.Size(426, 306);
            this.Controls.Add(this.copytxt);
            this.Controls.Add(this.authtxt);
            this.Controls.Add(this.copylbl);
            this.Controls.Add(this.authlbl);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.ctlpanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "FFEffectConfigDialog";
            this.Controls.SetChildIndex(this.ctlpanel, 0);
            this.Controls.SetChildIndex(this.buttonOK, 0);
            this.Controls.SetChildIndex(this.buttonCancel, 0);
            this.Controls.SetChildIndex(this.authlbl, 0);
            this.Controls.SetChildIndex(this.copylbl, 0);
            this.Controls.SetChildIndex(this.authtxt, 0);
            this.Controls.SetChildIndex(this.copytxt, 0);
            this.ctlpanel.ResumeLayout(false);
            this.ctlpanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctl4_UpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl3_UpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl7_UpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl6_UpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl5_UpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl2_UpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl1_UpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl0_UpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctl0)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal bool formok = false;
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.formok = true;
            FinishTokenUpdate();
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
            if ((int)this.ctl2_UpDown.Value != this.ctl2.Value)
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

            if ((int)this.ctl7_UpDown.Value != this.ctl7.Value)
            {
                this.ctl7_UpDown.Value = this.ctl7.Value;
            }

            if (data.ControlValue[7] != this.ctl7.Value)
            {
                data.ControlValue[7] = this.ctl7.Value;
                FinishTokenUpdate();
            }
        }

        /// Set the control values
        /// </summary>
        /// <param name="data">The FilterData to set</param>
        private void SetControls(FilterData data)
        {

            #region ctl / map enables

            if (data.MapEnable[0] && !data.ControlEnable[0] && !data.ControlEnable[1]/* && UsesMap(data.source,0)*/)
            {
                map0_lbl.Visible = true;
                map0_lbl.Text = data.MapLabel[0];
                ctl0.Visible = true;
                ctl0_UpDown.Visible = true;
                ctl1.Visible = true;
                ctl1_UpDown.Visible = true;
                ctllbl0.Visible = false;
                resetbtn0.Visible = true;
                resetbtn1.Visible = true;
            }
            else
            {
                if (data.ControlEnable[0])
                {
                    ctl0.Visible = true;
                    ctl0_UpDown.Visible = true;
                    ctllbl0.Visible = true;
                    resetbtn0.Visible = true;
                }
                else
                {
                    ctl0.Visible = false;
                    ctl0_UpDown.Visible = false;
                    ctllbl0.Visible = false;
                    resetbtn0.Visible = false;
                }

                if (data.ControlEnable[1])
                {
                    ctl1.Visible = true;
                    ctl1_UpDown.Visible = true;
                    ctllbl1.Visible = true;
                    resetbtn1.Visible = true;
                }
                else
                {
                    ctl1.Visible = false;
                    ctl1_UpDown.Visible = false;
                    ctllbl1.Visible = false;
                    resetbtn1.Visible = false;
                }
                this.ctllbl0.Text = data.ControlLabel[0];
                this.ctllbl1.Text = data.ControlLabel[1];
            }
            if (data.MapEnable[1] && !data.ControlEnable[2] && !data.ControlEnable[3]/* && UsesMap(data.source,1)*/)
            {
                map1_lbl.Visible = true;
                map1_lbl.Text = data.MapLabel[1];
                ctl2.Visible = true;
                ctl2_UpDown.Visible = true;
                ctl3.Visible = true;
                ctl3_UpDown.Visible = true;
                ctllbl2.Visible = false;
                ctllbl3.Visible = false;
                resetbtn2.Visible = true;
                resetbtn3.Visible = true;
            }
            else
            {
                if (data.ControlEnable[2])
                {
                    ctl2.Visible = true;
                    ctl2_UpDown.Visible = true;
                    ctllbl2.Visible = true;
                    resetbtn2.Visible = true;
                }
                else
                {
                    ctl2.Visible = false;
                    ctl2_UpDown.Visible = false;
                    ctllbl2.Visible = false;
                    resetbtn2.Visible = false;
                }

                if (data.ControlEnable[3])
                {
                    ctl3.Visible = true;
                    ctl3_UpDown.Visible = true;
                    ctllbl3.Visible = true;
                    resetbtn3.Visible = true;
                }
                else
                {
                    ctl3.Visible = false;
                    ctl3_UpDown.Visible = false;
                    ctllbl3.Visible = false;
                    resetbtn3.Visible = false;
                }
                this.ctllbl2.Text = data.ControlLabel[2];
                this.ctllbl3.Text = data.ControlLabel[3];
            }
            if (data.MapEnable[2] && !data.ControlEnable[4] && !data.ControlEnable[5] /*&& UsesMap(data.source, 2)*/)
            {
                map2_lbl.Visible = true;
                map2_lbl.Text = data.MapLabel[2];
                ctl4.Visible = true;
                ctl4_UpDown.Visible = true;
                ctl5.Visible = true;
                ctl5_UpDown.Visible = true;
                ctllbl4.Visible = false;
                ctllbl5.Visible = false;
                resetbtn4.Visible = true;
                resetbtn5.Visible = true;
            }
            else
            {
                if (data.ControlEnable[4])
                {
                    ctl4.Visible = true;
                    ctl4_UpDown.Visible = true;
                    ctllbl4.Visible = true;
                    resetbtn4.Visible = true;
                }
                else
                {
                    ctl4.Visible = false;
                    ctl4_UpDown.Visible = false;
                    ctllbl4.Visible = false;
                    resetbtn4.Visible = false;
                }

                if (data.ControlEnable[5])
                {
                    ctl5.Visible = true;
                    ctl5_UpDown.Visible = true;
                    ctllbl5.Visible = true;
                    resetbtn5.Visible = true;
                }
                else
                {
                    ctl5.Visible = false;
                    ctl5_UpDown.Visible = false;
                    ctllbl5.Visible = false;
                    resetbtn5.Visible = false;
                }
                this.ctllbl4.Text = data.ControlLabel[4];
                this.ctllbl5.Text = data.ControlLabel[5];
            }

            if (data.MapEnable[3] && !data.ControlEnable[6] && !data.ControlEnable[7] /*&& UsesMap(data.source, 3)*/)
            {
                map3_lbl.Visible = true;
                map3_lbl.Text = data.MapLabel[3];
                ctl6.Visible = true;
                ctl6_UpDown.Visible = true;
                ctl7.Visible = true;
                ctl7_UpDown.Visible = true;
                ctllbl6.Visible = false;
                ctllbl7.Visible = false;
                resetbtn6.Visible = true;
                resetbtn7.Visible = true;
            }
            else
            {
                if (data.ControlEnable[6])
                {
                    ctl6.Visible = true;
                    ctl6_UpDown.Visible = true;
                    ctllbl6.Visible = true;
                    resetbtn6.Visible = true;
                }
                else
                {
                    ctl6.Visible = false;
                    ctl6_UpDown.Visible = false;
                    ctllbl6.Visible = false;
                    resetbtn6.Visible = false;
                }

                if (data.ControlEnable[7])
                {
                    ctl7.Visible = true;
                    ctl7_UpDown.Visible = true;
                    ctllbl7.Visible = true;
                    resetbtn7.Visible = true;
                }
                else
                {
                    ctl7.Visible = false;
                    ctl7_UpDown.Visible = false;
                    ctllbl7.Visible = false;
                    resetbtn7.Visible = false;
                }
                this.ctllbl6.Text = data.ControlLabel[6];
                this.ctllbl7.Text = data.ControlLabel[7];
            }

            #endregion

            #region ctl values
            // set the default editor control values
            this.ctl0.Value = data.ControlValue[0];
            this.ctl1.Value = data.ControlValue[1];
            this.ctl2.Value = data.ControlValue[2];
            this.ctl3.Value = data.ControlValue[3];
            this.ctl4.Value = data.ControlValue[4];
            this.ctl5.Value = data.ControlValue[5];
            this.ctl6.Value = data.ControlValue[6];
            this.ctl7.Value = data.ControlValue[7];
            #endregion
        }
        int[] resetdata = new int[8];
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

        private void BringSlidersToFront()
        {
            this.ctl0.BringToFront();
            this.ctl1.BringToFront();
            this.ctl2.BringToFront();
            this.ctl3.BringToFront();
            this.ctl4.BringToFront();
            this.ctl5.BringToFront();
            this.ctl6.BringToFront();
            this.ctl7.BringToFront();
        }

        private void ResizetoVisibleControls()
        {
            SuspendLayout();

            this.Height = 0;
            this.ctlpanel.Height = 0;
            for (int i = 0; i < 8; i++)
            {

                if (data.ControlEnable[i])
                {
                    Debug.WriteLine(i);
                    switch (i)
                    {
                        case 0:
                            ctllbl0.Location = new Point(ctllbl0.Location.X, ctlpanel.ClientSize.Height);
                            ctl0.Location = new Point(ctl0.Location.X, ctlpanel.ClientSize.Height);
                            ctl0_UpDown.Location = new Point(ctl0_UpDown.Location.X, ctlpanel.ClientSize.Height);
                            resetbtn0.Location = new Point(resetbtn0.Location.X, ctlpanel.ClientSize.Height);
                            break;
                        case 1:
                            ctllbl1.Location = new Point(ctllbl1.Location.X, ctlpanel.ClientSize.Height);
                            ctl1.Location = new Point(ctl1.Location.X, ctlpanel.ClientSize.Height);
                            ctl1_UpDown.Location = new Point(ctl1_UpDown.Location.X, ctlpanel.ClientSize.Height);
                            resetbtn1.Location = new Point(resetbtn1.Location.X, ctlpanel.ClientSize.Height);
                            break;
                        case 2:
                            ctllbl2.Location = new Point(ctllbl2.Location.X, ctlpanel.ClientSize.Height);
                            ctl2.Location = new Point(ctl2.Location.X, ctlpanel.ClientSize.Height);
                            ctl2_UpDown.Location = new Point(ctl2_UpDown.Location.X, ctlpanel.ClientSize.Height);
                            resetbtn2.Location = new Point(resetbtn2.Location.X, ctlpanel.ClientSize.Height);
                            break;
                        case 3:
                            ctllbl3.Location = new Point(ctllbl3.Location.X, ctlpanel.ClientSize.Height);
                            ctl3.Location = new Point(ctl3.Location.X, ctlpanel.ClientSize.Height);
                            ctl3_UpDown.Location = new Point(ctl3_UpDown.Location.X, ctlpanel.ClientSize.Height);
                            resetbtn3.Location = new Point(resetbtn3.Location.X, ctlpanel.ClientSize.Height);
                            break;
                        case 4:
                            ctllbl4.Location = new Point(ctllbl4.Location.X, ctlpanel.ClientSize.Height);
                            ctl4.Location = new Point(ctl4.Location.X, ctlpanel.ClientSize.Height);
                            ctl4_UpDown.Location = new Point(ctl4_UpDown.Location.X, ctlpanel.ClientSize.Height);
                            resetbtn4.Location = new Point(resetbtn4.Location.X, ctlpanel.ClientSize.Height);
                            break;
                        case 5:
                            ctllbl5.Location = new Point(ctllbl5.Location.X, ctlpanel.ClientSize.Height);
                            ctl5.Location = new Point(ctl5.Location.X, ctlpanel.ClientSize.Height);
                            ctl5_UpDown.Location = new Point(ctl5_UpDown.Location.X, ctlpanel.ClientSize.Height);
                            resetbtn5.Location = new Point(resetbtn5.Location.X, ctlpanel.ClientSize.Height);
                            break;
                        case 6:
                            ctllbl6.Location = new Point(ctllbl6.Location.X, ctlpanel.ClientSize.Height);
                            ctl6.Location = new Point(ctl6.Location.X, ctlpanel.ClientSize.Height);
                            ctl6_UpDown.Location = new Point(ctl6_UpDown.Location.X, ctlpanel.ClientSize.Height);
                            resetbtn6.Location = new Point(resetbtn6.Location.X, ctlpanel.ClientSize.Height);
                            break;
                        case 7:
                            ctllbl7.Location = new Point(ctllbl7.Location.X, ctlpanel.ClientSize.Height);
                            ctl7.Location = new Point(ctl7.Location.X, ctlpanel.ClientSize.Height);
                            ctl7_UpDown.Location = new Point(ctl7_UpDown.Location.X, ctlpanel.ClientSize.Height);
                            resetbtn7.Location = new Point(resetbtn7.Location.X, ctlpanel.ClientSize.Height);
                            break;
                    }
                    this.Height += 25;
                    this.ctlpanel.Height += 25;
                }
            }
            BringSlidersToFront();

            this.Height += 12;
            // Debug.WriteLine("ctlpanel height = " + this.ctlpanel.Height.ToString());

            this.authtxt.Location = new Point(this.authtxt.Location.X, this.ClientSize.Height);
            this.authtxt.Visible = true;
            this.authlbl.Location = new Point(this.authlbl.Location.X, this.ClientSize.Height);
            this.authlbl.Visible = true;
            this.Height += authlbl.Height + 5;

            this.copytxt.Location = new Point(this.copytxt.Location.X, this.ClientSize.Height);
            this.copytxt.Visible = true;
            this.copylbl.Location = new Point(this.copylbl.Location.X, this.ClientSize.Height);
            this.copylbl.Visible = true;
            this.Height += copylbl.Height;

            this.buttonOK.Location = new Point(this.buttonOK.Location.X, this.ClientSize.Height);
            this.buttonOK.Visible = true;
            this.buttonCancel.Location = new Point(this.buttonCancel.Location.X, this.ClientSize.Height);
            this.buttonCancel.Visible = true;
            this.Height += this.buttonOK.Height + 3;

            PerformLayout();
        }
    }
}