﻿/*
 * Copyright (C) 2010, Henon <meinrad.recheis@gmail.com>
 *
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or
 * without modification, are permitted provided that the following
 * conditions are met:
 *
 * - Redistributions of source code must retain the above copyright
 *   notice, this list of conditions and the following disclaimer.
 *
 * - Redistributions in binary form must reproduce the above
 *   copyright notice, this list of conditions and the following
 *   disclaimer in the documentation and/or other materials provided
 *   with the distribution.
 *
 * - Neither the name of the project nor the
 *   names of its contributors may be used to endorse or promote
 *   products derived from this software without specific prior
 *   written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND
 * CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES,
 * INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
 * OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
 * CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
 * STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
 * ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GitSharp.Demo
{
	public partial class TextDiffView : UserControl
	{
        string fileAPath;
        string fileBPath;
        Change change;
        string aBlob;
        string bBlob;

        public TextDiffView()
		{
			InitializeComponent();
			DataContext = this;
		}

        public void Init(byte[] a, byte[] b)
		{
            Diff diff = new Diff(a, b);
            
            Diff = diff;
			ListA.ItemsSource = diff.Sections;
			ListB.ItemsSource = diff.Sections;
            aBlob = Encoding.UTF8.GetString(a);
            bBlob = Encoding.UTF8.GetString(b);

		}

		public Diff Diff
		{
			get;
			private set;
		}

		internal void Show(Change change)
		{
            this.change = change;
            if (change == null)
			{
				Clear();
				return;
			}

            Blob aB = change.ReferenceObject != null ? change.ReferenceObject as Blob : null;
            Blob bB = change.ComparedObject != null ? change.ComparedObject as Blob : null;

            //SaveTempFiles(change, aB, bB);
            

            //aBlob = aB.Data;
            //bBlob = bB.Data;
            var a = aB != null ? aB.RawData : new byte[0];
            var b = bB != null ? bB.RawData : new byte[0];

			a = (Diff.IsBinary(a)==true ? Encoding.ASCII.GetBytes("Binary content\nFile size: "+a.Length) : a);
			b = (Diff.IsBinary(b) == true ? Encoding.ASCII.GetBytes("Binary content\nFile size: " + b.Length) : b);
			Init(a, b);
            
		}

        private void SaveTempFiles()
        {
            if ( aBlob == null || bBlob == null)
            {
                return;
            }
            try
            {
                if (aBlob != null)
                {
                    fileAPath = string.Format(@"D:\Temp\{0}{1}", "revA", change == null ? "" : change.Name);
                    //File.CreateText(fileAPath);
                    File.WriteAllText(fileAPath, aBlob);
                }

                if (bBlob != null)
                {
                    fileBPath = string.Format(@"D:\Temp\{0}{1}", "revB", change == null ? "" : change.Name);
                    //File.CreateText(fileBPath);
                    File.WriteAllText(fileBPath, bBlob);
                }
            }
            catch (Exception)
            {

            }
        }

		private void OnScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			if (sender == m_scrollview_A)
			{
				m_scrollview_B.ScrollToVerticalOffset(m_scrollview_A.VerticalOffset);
				m_scrollview_B.ScrollToHorizontalOffset(m_scrollview_A.HorizontalOffset);
				return;
			}
			if (sender == m_scrollview_B)
			{
				m_scrollview_A.ScrollToVerticalOffset(m_scrollview_B.VerticalOffset);
				m_scrollview_A.ScrollToHorizontalOffset(m_scrollview_B.HorizontalOffset);
				return;
			}
		}

		/// <summary>
		/// Work around a limitation in the framework that does not allow to stretch ListBoxItemTemplates
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StretchDataTemplate(object sender, RoutedEventArgs e)
		{
			// found this method at: http://silverlight.net/forums/p/18918/70469.aspx#70469
			var t = sender as FrameworkElement;
			if (t == null)
				return;
			var p = VisualTreeHelper.GetParent(t) as ContentPresenter;
			if (p == null)
				return;
			p.HorizontalAlignment = HorizontalAlignment.Stretch;
		}


		internal void Clear()
		{
			ListA.ItemsSource = null;
			ListB.ItemsSource = null;
		}

        private void Button_Diff_Click(object sender, RoutedEventArgs e)
        {
            SaveTempFiles();
            OpenDiff(fileAPath, fileBPath, true);
        }

        public static void OpenDiff(string revA, string revB, bool isSvnDiff)
        {
            Process process = new Process();
            if (!isSvnDiff)
            {
                //process.StartInfo.FileName = Application.StartupPath + @"\DiffDotNet.exe";
                string baseFile = string.Format("{0} {1} /f:gta", revA, revB);
                process.StartInfo.Arguments = baseFile;
            }
            else
            {
                process.StartInfo.FileName = @"TortoiseMerge.exe";
                string baseFile = string.Format("/base:\"{0}\" /readonly", revA);
                string theirsFile = string.Format("/theirs:\"{0}\" /readonly", revA);
                string myFile = string.Format("/mine:\"{0}\"", revB);
                //string description = string.Format("/basename:\"{0}\" /minename:\"{1}\"", "Base", fileName);
                //string description = string.Format("/theirsname:\"{0}\" /minename:\"{1}\"", "Base", fileName);
                string description = string.Format("/theirsname:\"{0}\" /minename:\"{1}\"", "Base", revA);
                //process.StartInfo.Arguments = baseFile + " " + theirsFile + " " + myFile + " " + description;
                process.StartInfo.Arguments = baseFile + " " + myFile + " " + description;
                
                //process.StartInfo.FileName = @"TortoiseMerge.exe";
                //string baseFile = string.Format("/base:\"{0}\" /readonly", revB);
                //string theirsFile = string.Format("/theirs:\"{0}\" /readonly", revA);
                //string myFile = string.Format("/mine:\"{0}\"", revB);
                ////string description = string.Format("/basename:\"{0}\" /minename:\"{1}\"", "Base", fileName);
                //string description = string.Format("/theirsname:\"{0}\" /minename:\"{1}\"", "Base", revA);
                //process.StartInfo.Arguments = baseFile + " " + theirsFile + " " + myFile + " " + description;
            }
            try
            {
                process.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

	/// <summary>
	/// Adjust text block sizes to correspond to each other by adding lines
	/// </summary>
	public class BlockTextConverterA : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var section = value as Diff.Section;
			if (section == null)
				return 0;
			var a_lines = section.EndA - section.BeginA;
			var b_lines = section.EndB - section.BeginB;
			var line_difference = Math.Max(a_lines, b_lines) - a_lines;
			var s = new StringBuilder(Regex.Replace(section.TextA, "\r?\n$", ""));
			if (a_lines == 0)
				line_difference -= 1;
			for (var i = 0; i < line_difference; i++)
				s.AppendLine();
			return s.ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>
	/// Adjust text block sizes to correspond to each other by adding lines
	/// </summary>
	public class BlockTextConverterB : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var section = value as Diff.Section;
			if (section == null)
				return 0;
			var a_lines = section.EndA - section.BeginA;
			var b_lines = section.EndB - section.BeginB;
			var line_difference = Math.Max(a_lines, b_lines) - b_lines;
			var s = new StringBuilder(Regex.Replace(section.TextB, "\r?\n$", ""));
			if (b_lines == 0)
				line_difference -= 1;
			for (var i = 0; i < line_difference; i++)
				s.AppendLine();
			return s.ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>
	/// Calculate block background color
	/// </summary>
	public class BlockColorConverterA : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var section = value as Diff.Section;
			if (section == null)
				return Brushes.Pink; // <-- this shouldn't happen anyway
			switch (section.EditWithRespectToA)
			{
				case Diff.EditType.Deleted:
					return Brushes.LightSkyBlue;
				case Diff.EditType.Replaced:
					return Brushes.LightSalmon;
				case Diff.EditType.Inserted:
					return Brushes.DarkGray;
				case Diff.EditType.Unchanged:
				default:
					return Brushes.White;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>
	/// Calculate block background color
	/// </summary>
	public class BlockColorConverterB : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var section = value as Diff.Section;
			if (section == null)
				return Brushes.Pink; // <-- this shouldn't happen anyway
			switch (section.EditWithRespectToA)
			{
				case Diff.EditType.Deleted:
					return Brushes.DarkGray;
				case Diff.EditType.Replaced:
					return Brushes.LightSalmon;
				case Diff.EditType.Inserted:
					return Brushes.LightGreen;
				case Diff.EditType.Unchanged:
				default:
					return Brushes.White;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}


