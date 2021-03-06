﻿/*
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

using PdnFF.Interop;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PdnFF.Controls
{
    // Adapted from http://dotnetrix.co.uk/tabcontrol.htm#tip2

    /// <summary>
    /// A TabControl that supports custom background and foreground colors
    /// </summary>
    internal sealed class TabControlEx : TabControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components;
        private Color backColor;
        private Color foreColor;
        private Color borderColor;
        private Color hotTrackColor;
        private int hotTabIndex;

        private static readonly Color DefaultBorderColor = SystemColors.ControlDark;
        private static readonly Color DefaultHotTrackColor = Color.FromArgb(128, SystemColors.HotTrack);

        /// <summary>
        /// Initializes a new instance of the <see cref="TabControlEx"/> class.
        /// </summary>
        public TabControlEx()
        {
            // This call is required by the Windows Forms Designer.
            InitializeComponent();
            backColor = Color.Empty;
            foreColor = Color.Empty;
            borderColor = DefaultBorderColor;
            hotTrackColor = DefaultHotTrackColor;
            hotTabIndex = -1;
        }

        private enum TabState
        {
            Active = 0,
            MouseOver,
            Inactive
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new Container();
        }
        #endregion

        [Browsable(true), Description("The background color of the control.")]
        public override Color BackColor
        {
            get
            {
                if (backColor.IsEmpty)
                {
                    if (Parent == null)
                    {
                        return DefaultBackColor;
                    }
                    else
                    {
                        return Parent.BackColor;
                    }
                }

                return backColor;
            }
            set
            {
                if (backColor != value)
                {
                    backColor = value;
                    DetermineDrawingMode();
                    // Let the Tabpages know that the backcolor has changed.
                    OnBackColorChanged(EventArgs.Empty);
                }
            }
        }

        [Browsable(true), Description("The foreground color of the control.")]
        public override Color ForeColor
        {
            get
            {
                if (foreColor.IsEmpty)
                {
                    if (Parent == null)
                    {
                        return DefaultForeColor;
                    }
                    else
                    {
                        return Parent.ForeColor;
                    }
                }

                return foreColor;
            }
            set
            {
                if (foreColor != value)
                {
                    foreColor = value;
                    DetermineDrawingMode();

                    // Let the Tabpages know that the forecolor has changed.
                    OnForeColorChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets the border color of the tabs.
        /// </summary>
        /// <value>
        /// The border color of the tabs.
        /// </value>
        [Description("The border color of the tabs.")]
        public Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                if (borderColor != value)
                {
                    borderColor = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color displayed when the mouse is over an inactive tab.
        /// </summary>
        /// <value>
        /// The color displayed when the mouse is over an inactive tab.
        /// </value>
        [Description("The color displayed when the mouse is over an inactive tab.")]
        public Color HotTrackColor
        {
            get
            {
                return hotTrackColor;
            }
            set
            {
                if (hotTrackColor != value)
                {
                    hotTrackColor = value;
                    Invalidate();
                }
            }
        }

        private bool HotTrackingEnabled
        {
            get
            {
                if (SystemInformation.IsHotTrackingEnabled)
                {
                    return true;
                }

                return HotTrack;
            }
        }

        private bool UseOwnerDraw
        {
            get
            {
                if (SystemInformation.HighContrast)
                {
                    return false;
                }

                return (!backColor.IsEmpty && backColor != DefaultBackColor || !foreColor.IsEmpty && foreColor != DefaultForeColor);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override void ResetBackColor()
        {
            BackColor = Color.Empty;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override void ResetForeColor()
        {
            ForeColor = Color.Empty;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetBorderColor()
        {
            BorderColor = DefaultBorderColor;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetHotTrackColor()
        {
            HotTrackColor = DefaultHotTrackColor;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeBackColor()
        {
            return !backColor.IsEmpty;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeForeColor()
        {
            return !foreColor.IsEmpty;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeBorderColor()
        {
            return borderColor != DefaultBorderColor;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeHotTrackColor()
        {
            return hotTrackColor != DefaultHotTrackColor;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (!DesignMode)
            {
                if (hotTabIndex != -1)
                {
                    hotTabIndex = -1;
                    Invalidate();
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!DesignMode)
            {
                int index = GetTabIndexUnderCursor();

                if (index != hotTabIndex)
                {
                    hotTabIndex = index;
                    Invalidate();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (GetStyle(ControlStyles.UserPaint))
            {
                e.Graphics.Clear(backColor);

                if (TabCount <= 0)
                {
                    return;
                }

                SolidBrush background = null;
                Pen border = null;
                SolidBrush hotTrack = null;
                SolidBrush text = null;
                StringFormat format = null;

                try
                {
                    background = new SolidBrush(BackColor);
                    border = new Pen(borderColor);
                    hotTrack = new SolidBrush(hotTrackColor);
                    text = new SolidBrush(ForeColor);
                    format = new StringFormat()
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };

                    int activeTabIndex = SelectedIndex;

                    // Draw the inactive tabs.
                    for (int index = 0; index < TabCount; index++)
                    {
                        if (index != activeTabIndex)
                        {
                            TabState state = TabState.Inactive;
                            if (index == hotTabIndex && HotTrackingEnabled)
                            {
                                state = TabState.MouseOver;
                            }

                            DrawTab(e.Graphics, background, border, hotTrack, text, format, TabPages[index], GetTabRect(index), state);
                        }
                    }

                    // Draw the active tab.
                    DrawTab(e.Graphics, background, border, hotTrack, text, format, TabPages[activeTabIndex], GetTabRect(activeTabIndex), TabState.Active);
                }
                finally
                {
                    if (background != null)
                    {
                        background.Dispose();
                        background = null;
                    }
                    if (border != null)
                    {
                        border.Dispose();
                        border = null;
                    }
                    if (hotTrack != null)
                    {
                        hotTrack.Dispose();
                        hotTrack = null;
                    }
                    if (text != null)
                    {
                        text.Dispose();
                        text = null;
                    }
                    if (format != null)
                    {
                        format.Dispose();
                        format = null;
                    }
                }
            }
        }

        protected override void OnSystemColorsChanged(EventArgs e)
        {
            base.OnSystemColorsChanged(e);

            DetermineDrawingMode();
        }

        private void DetermineDrawingMode()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, UseOwnerDraw);
            UpdateTabPageVisualStyleBackColor();
            Invalidate();
        }

        private void DrawTab(Graphics graphics, Brush background, Pen border, Brush hotTrack, SolidBrush text, StringFormat format,
            TabPage page, Rectangle bounds, TabState state)
        {
            if (state == TabState.Active)
            {
                Rectangle activeTabRect = Rectangle.Inflate(bounds, 2, 2);

                graphics.FillRectangle(background, activeTabRect);
                DrawTabBorder(graphics, border, activeTabRect);
            }
            else if (state == TabState.MouseOver)
            {
                graphics.FillRectangle(hotTrack, bounds);
                DrawTabBorder(graphics, border, bounds);
            }
            else
            {
                graphics.FillRectangle(background, bounds);
                DrawTabBorder(graphics, border, bounds);
            }

            DrawTabText(graphics, text, format, bounds, page);
        }

        private void DrawTabBorder(Graphics graphics, Pen borderPen, Rectangle bounds)
        {
            Point[] points = new Point[6];

            switch (Alignment)
            {
                case TabAlignment.Top:
                    points[0] = new Point(bounds.Left, bounds.Top);
                    points[1] = new Point(bounds.Left, bounds.Bottom - 1);
                    points[2] = new Point(bounds.Left, bounds.Top);
                    points[3] = new Point(bounds.Right - 1, bounds.Top);
                    points[4] = new Point(bounds.Right - 1, bounds.Top);
                    points[5] = new Point(bounds.Right - 1, bounds.Bottom - 1);
                    break;
                case TabAlignment.Bottom:
                    points[0] = new Point(bounds.Left, bounds.Top);
                    points[1] = new Point(bounds.Left, bounds.Bottom - 1);
                    points[2] = new Point(bounds.Left, bounds.Bottom - 1);
                    points[3] = new Point(bounds.Right - 1, bounds.Bottom - 1);
                    points[4] = new Point(bounds.Right - 1, bounds.Bottom - 1);
                    points[5] = new Point(bounds.Right - 1, bounds.Top);
                    break;
                case TabAlignment.Left:
                    points[0] = new Point(bounds.Left, bounds.Top);
                    points[1] = new Point(bounds.Right - 1, bounds.Top);
                    points[2] = new Point(bounds.Left, bounds.Top);
                    points[3] = new Point(bounds.Left, bounds.Bottom - 1);
                    points[4] = new Point(bounds.Left, bounds.Bottom - 1);
                    points[5] = new Point(bounds.Right, bounds.Bottom - 1);
                    break;
                case TabAlignment.Right:
                    points[0] = new Point(bounds.Left, bounds.Top);
                    points[1] = new Point(bounds.Right - 1, bounds.Top);
                    points[2] = new Point(bounds.Right - 1, bounds.Top);
                    points[3] = new Point(bounds.Right - 1, bounds.Bottom - 1);
                    points[4] = new Point(bounds.Right - 1, bounds.Bottom - 1);
                    points[5] = new Point(bounds.Left, bounds.Bottom - 1);
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }

            graphics.DrawLines(borderPen, points);
        }

        private void DrawTabText(Graphics graphics, SolidBrush textBrush, StringFormat format, Rectangle bounds, TabPage page)
        {
            // Set up rotation for left and right aligned tabs.
            if (Alignment == TabAlignment.Left || Alignment == TabAlignment.Right)
            {
                float rotateAngle = 90;
                if (Alignment == TabAlignment.Left)
                {
                    rotateAngle = 270;
                }

                PointF cp = new PointF(bounds.Left + (bounds.Width / 2), bounds.Top + (bounds.Height / 2));
                graphics.TranslateTransform(cp.X, cp.Y);
                graphics.RotateTransform(rotateAngle);

                bounds = new Rectangle(-(bounds.Height / 2), -(bounds.Width / 2), bounds.Height, bounds.Width);
            }

            // Draw the Tab text.
            if (page.Enabled)
            {
                graphics.DrawString(page.Text, Font, textBrush, bounds, format);
            }
            else
            {
                ControlPaint.DrawStringDisabled(graphics, page.Text, Font, textBrush.Color, bounds, format);
            }

            graphics.ResetTransform();
        }

        private int GetTabIndexUnderCursor()
        {
            Point cursor = PointToClient(MousePosition);

            NativeStructs.TCHITTESTINFO hti = new NativeStructs.TCHITTESTINFO
            {
                pt = new NativeStructs.POINT(cursor.X, cursor.Y),
                flags = 0
            };

            int index = SafeNativeMethods.SendMessage(Handle, NativeConstants.TCM_HITTEST, IntPtr.Zero, ref hti).ToInt32();

            return index;
        }

        private void UpdateTabPageVisualStyleBackColor()
        {
            bool useVisualStyleBackColor = false;
            if (SystemInformation.HighContrast || BackColor == DefaultBackColor)
            {
                useVisualStyleBackColor = true;
            }

            // When the BackColor is changed the TabControl only updates the UseVisualStyleBackColor property for the active tab.
            // Set the property on all tabs so that the correct color is displayed when the user switches tabs.

            for (int i = 0; i < TabCount; i++)
            {
                TabPages[i].UseVisualStyleBackColor = useVisualStyleBackColor;
            }
        }
    }
}
