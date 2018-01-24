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

using PaintDotNet;
using PaintDotNet.Effects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace PdnFF
{
    internal static class PluginThemingUtil
    {
        // Paint.NET added theming support for plug-ins in 4.20.
        private static readonly Version PluginThemingMinVersion = new Version("4.20");

        private static MethodInfo useAppThemeSetter;
        private static bool initAppThemeSetter = false;

        public static void EnableEffectDialogTheme(EffectConfigDialog dialog)
        {
            try
            {
                Version pdnVersion = dialog.Services.GetService<PaintDotNet.AppModel.IAppInfoService>().AppVersion;

                if (pdnVersion >= PluginThemingMinVersion)
                {
                    if (!initAppThemeSetter)
                    {
                        initAppThemeSetter = true;

                        PropertyInfo propertyInfo = typeof(EffectConfigDialog).GetProperty("UseAppThemeColors");
                        if (propertyInfo != null)
                        {
                            useAppThemeSetter = propertyInfo.GetSetMethod();
                        }
                    }

                    if (useAppThemeSetter != null)
                    {
                        useAppThemeSetter.Invoke(dialog, new object[] { true });
                    }
                }
            }
            catch
            {
                // Ignore any exceptions that are thrown when trying to enable the dialog theming.
                // The dialog should be shown to the user even if theming could not be enabled.
            }
        }

        public static void UpdateControlBackColor(Control root)
        {
            Color backColor = root.BackColor;

            Stack<Control> stack = new Stack<Control>();
            stack.Push(root);

            while (stack.Count > 0)
            {
                Control parent = stack.Pop();

                var controls = parent.Controls;

                for (int i = 0; i < controls.Count; i++)
                {
                    Control control = controls[i];

                    if (control is Button button)
                    {
                        // Reset the BackColor of all Button controls.
                        button.UseVisualStyleBackColor = true;
                    }
                    else
                    {
                        // Update the BackColor for all child controls as some controls
                        // do not change the BackColor when the parent control does.

                        control.BackColor = backColor;

                        if (control.HasChildren)
                        {
                            stack.Push(control);
                        }
                    }
                }
            }
        }

        public static void UpdateControlForeColor(Control root)
        {
            Color foreColor = root.ForeColor;

            Stack<Control> stack = new Stack<Control>();
            stack.Push(root);

            while (stack.Count > 0)
            {
                Control parent = stack.Pop();

                var controls = parent.Controls;

                for (int i = 0; i < controls.Count; i++)
                {
                    Control control = controls[i];

                    if (control is Button button)
                    {
                        // Reset the ForeColor of all Button controls.
                        button.ForeColor = SystemColors.ControlText;
                    }
                    else if (control is LinkLabel link)
                    {
                        if (foreColor != Control.DefaultForeColor)
                        {
                            link.LinkColor = foreColor;
                        }
                        else
                        {
                            // If the control is using the default foreground color set the link color
                            // to Color.Empty so the LinkLabel will use its default colors.
                            link.LinkColor = Color.Empty;
                        }
                    }
                    else
                    {
                        // Update the ForeColor for all child controls as some controls
                        // do not change the ForeColor when the parent control does.

                        control.ForeColor = foreColor;

                        if (control.HasChildren)
                        {
                            stack.Push(control);
                        }
                    }
                }
            }
        }
    }
}
