using CircleDock;
using FileOps;
using LanguageLoader;
using PerPixelAlphaForms;
using Pinvoke;
using SettingsLoader;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace BaseDockObjects
{
	public class CentreObject : DockItemPerPixelAlphaForm
	{
		private ContextMenuStrip contextMenuStrip;

		private IContainer components;

		private ToolStripMenuItem quitToolStripMenuItem;

		private ToolStripMenuItem addToolStripMenuItem;

		private ToolStripMenuItem hideToolStripMenuItem;

		private ToolStripMenuItem settingsToolStripMenuItem;

		private ToolStripMenuItem dockFolderToolStripMenuItem;

		private ToolStripMenuItem changeIconToolStripMenuItem;

		private ToolStripMenuItem iconReplacementModeToolStripMenuItem;

		private ToolStripMenuItem pauseMouseHotkeysToolStripMenuItem;

		private ToolStripMenuItem blankIconToolStripMenuItem;

		private OpenFileDialog ChangeIconDialog;

		public string SectionName;

		public int PreviousOpacity;

		private Point previousMousePosition;

		private Point StoredMouseOffset;

		private int TargetOpacity;

		private double OpacityRateOfChange;

		private double tempNewObjectOpacity;

		private void InitializeComponent()
		{
			this.components = new Container();
			this.contextMenuStrip = new ContextMenuStrip(this.components);
			this.quitToolStripMenuItem = new ToolStripMenuItem();
			this.addToolStripMenuItem = new ToolStripMenuItem();
			this.hideToolStripMenuItem = new ToolStripMenuItem();
			this.dockFolderToolStripMenuItem = new ToolStripMenuItem();
			this.blankIconToolStripMenuItem = new ToolStripMenuItem();
			this.settingsToolStripMenuItem = new ToolStripMenuItem();
			this.changeIconToolStripMenuItem = new ToolStripMenuItem();
			this.iconReplacementModeToolStripMenuItem = new ToolStripMenuItem();
			this.pauseMouseHotkeysToolStripMenuItem = new ToolStripMenuItem();
			this.ChangeIconDialog = new OpenFileDialog();
			this.contextMenuStrip.SuspendLayout();
			base.SuspendLayout();
			this.contextMenuStrip.Items.AddRange(new ToolStripItem[]
			{
				this.hideToolStripMenuItem,
				this.addToolStripMenuItem,
				this.changeIconToolStripMenuItem,
				this.iconReplacementModeToolStripMenuItem,
				this.pauseMouseHotkeysToolStripMenuItem,
				this.settingsToolStripMenuItem,
				this.quitToolStripMenuItem
			});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new Size(153, 48);
			this.contextMenuStrip.Opening += new CancelEventHandler(this.contextMenuStrip_Opening);
			this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
			this.quitToolStripMenuItem.Size = new Size(152, 22);
			this.quitToolStripMenuItem.Text = this.Language.MainContextMenu.QuitWord;
			this.quitToolStripMenuItem.Click += new EventHandler(this.quitToolStripMenuItem_Click);
			this.addToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.dockFolderToolStripMenuItem,
				this.blankIconToolStripMenuItem
			});
			this.addToolStripMenuItem.Name = "addToolStripMenuItem";
			this.addToolStripMenuItem.Size = new Size(152, 22);
			this.addToolStripMenuItem.Text = this.Language.MainContextMenu.AddWord;
			this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
			this.hideToolStripMenuItem.Size = new Size(152, 22);
			this.hideToolStripMenuItem.Text = this.Language.MainContextMenu.HideWord;
			this.hideToolStripMenuItem.Click += new EventHandler(this.hideToolStripMenuItem_Click);
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.Size = new Size(152, 22);
			this.settingsToolStripMenuItem.Text = this.Language.MainContextMenu.SettingsWord;
			this.settingsToolStripMenuItem.Click += new EventHandler(this.settingsToolStripMenuItem_Click);
			this.iconReplacementModeToolStripMenuItem.Name = "iconReplacementModeToolStripMenuItem";
			this.iconReplacementModeToolStripMenuItem.Size = new Size(152, 22);
			this.iconReplacementModeToolStripMenuItem.Text = this.Language.General.IconReplacementMode;
			this.iconReplacementModeToolStripMenuItem.CheckOnClick = true;
			this.iconReplacementModeToolStripMenuItem.Checked = this.DockSettings.General.IconReplacementMode;
			this.iconReplacementModeToolStripMenuItem.Click += new EventHandler(this.iconReplacementModeToolStripMenuItem_Click);
			this.pauseMouseHotkeysToolStripMenuItem.Name = "pauseMouseHotkeysToolStripMenuItem";
			this.pauseMouseHotkeysToolStripMenuItem.Size = new Size(152, 22);
			this.pauseMouseHotkeysToolStripMenuItem.Text = this.Language.MainContextMenu.PauseMouseToggling;
			this.pauseMouseHotkeysToolStripMenuItem.CheckOnClick = true;
			this.pauseMouseHotkeysToolStripMenuItem.Click += new EventHandler(this.pauseMouseHotkeysToolStripMenuItem_Click);
			this.ChangeIconDialog.InitialDirectory = Application.StartupPath + "\\System\\Icons";
			this.ChangeIconDialog.Filter = "PNG|*.png";
			this.ChangeIconDialog.Multiselect = false;
			this.ChangeIconDialog.Title = this.Language.MainContextMenu.ChangeIcon;
			this.ChangeIconDialog.FileOk += new CancelEventHandler(this.ChangeIconDialog_FileOk);
			this.changeIconToolStripMenuItem.Name = "changeIconToolStripMenuItem";
			this.changeIconToolStripMenuItem.Size = new Size(152, 22);
			this.changeIconToolStripMenuItem.Text = this.Language.MainContextMenu.ChangeIcon;
			this.changeIconToolStripMenuItem.Click += new EventHandler(this.changeIconToolStripMenuItem_Click);
			this.dockFolderToolStripMenuItem.Name = "dockFolderToolStripMenuItem";
			this.dockFolderToolStripMenuItem.Size = new Size(152, 22);
			this.dockFolderToolStripMenuItem.Text = this.Language.MainContextMenu.DockFolder;
			this.dockFolderToolStripMenuItem.Click += new EventHandler(this.dockFolderToolStripMenuItem_Click);
			this.blankIconToolStripMenuItem.Name = "blankIconToolStripMenuItem";
			this.blankIconToolStripMenuItem.Size = new Size(152, 22);
			this.blankIconToolStripMenuItem.Text = this.Language.MainContextMenu.blankIcon;
			this.blankIconToolStripMenuItem.Click += new EventHandler(this.blankIconToolStripMenuItem_Click);
			base.ClientSize = new Size(1, 1);
			base.MouseEnter += new EventHandler(this.CentreObject_MouseEnter);
			base.MouseDown += new MouseEventHandler(this.CentreObject_MouseDown);
			base.MouseUp += new MouseEventHandler(this.CentreObject_MouseUp);
			base.MouseMove += new MouseEventHandler(this.CentreObject_MouseMove);
			base.MouseWheel += new MouseEventHandler(this.CentreObject_MouseWheel);
			base.MouseHover += new EventHandler(this.CentreObject_MouseHover);
			base.DragEnter += new DragEventHandler(this.CentreObject_DragEnter);
			base.DragDrop += new DragEventHandler(this.CentreObject_DragDrop);
			base.KeyDown += new KeyEventHandler(this.CentreObject_KeyDown);
			this.ContextMenuStrip = this.contextMenuStrip;
			base.Deactivate += new EventHandler(this.CentreObject_Deactivate);
			base.Name = "TransparentObject";
			this.contextMenuStrip.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void CentreObject_Deactivate(object sender, EventArgs e)
		{
			this.ParentObject.itemDeactivated();
		}

		public CentreObject(MainForm TheParent, LanguageLoader.LanguageLoader LanguageData, SettingsLoader.SettingsLoader SettingsData, string Section, Size InitialSize)
		{
			this.Language = LanguageData;
			this.DockSettings = SettingsData;
			this.ParentObject = TheParent;
			this.ObjectSize = InitialSize;
			this.SectionName = Section;
			this.InitializeComponent();
			this.DoubleBuffered = true;
			this.AnimationTimer.Tick += new EventHandler(this.AnimationTimer_Tick);
			this.AnimationTimer.Interval = this.DockSettings.General.AnimationInterval;
			base.ShowInTaskbar = false;
			this.AllowDrop = true;
			this.LeftMouseButtonDown = false;
			this.ThisObjectMovedWithLeftMouse = false;
			this.StoredMouseOffset = new Point(0, 0);
			this.ObjectBitmap = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
			this.OverlayBitmap = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
			this.ObjectOpacity = this.DockSettings.CentreImage.DefaultOpacity;
			this.PreviousOpacity = this.ObjectOpacity;
			this.SetZLevel();
			this.SetBitmap();
		}

		public void SetZLevel()
		{
			if (this.DockSettings.General.zLevel == "Always On Bottom")
			{
				Pinvoke.Win32.SetWindowPos(base.Handle, (IntPtr)1, 0, 0, 0, 0, 1563u);
			}
			else if (this.DockSettings.General.zLevel == "Normal")
			{
				Pinvoke.Win32.SetWindowPos(base.Handle, (IntPtr)(-2), 0, 0, 0, 0, 1563u);
			}
			else
			{
				Pinvoke.Win32.SetWindowPos(base.Handle, (IntPtr)(-1), 0, 0, 0, 0, 1563u);
			}
		}

		public void AnimateOpacity(int NewOpacity, int Duration)
		{
			if (Duration > 0)
			{
				this.TargetOpacity = NewOpacity;
				this.OpacityRateOfChange = ((double)NewOpacity - (double)this.ObjectOpacity) / ((double)Duration / (double)this.AnimationTimer.Interval);
				this.tempNewObjectOpacity = (double)this.ObjectOpacity;
				this.AnimationTimer.Start();
			}
		}

		private void AnimationTimer_Tick(object sender, EventArgs e)
		{
			this.tempNewObjectOpacity += this.OpacityRateOfChange;
			if ((this.OpacityRateOfChange > 0.0 && this.tempNewObjectOpacity > (double)this.TargetOpacity) || (this.OpacityRateOfChange < 0.0 && this.tempNewObjectOpacity < (double)this.TargetOpacity))
			{
				this.tempNewObjectOpacity = (double)this.TargetOpacity;
			}
			base.UpdateOpacity((int)this.tempNewObjectOpacity);
			if (this.ObjectOpacity == 0 || this.ObjectOpacity == 255 || (this.OpacityRateOfChange < 0.0 && this.ObjectOpacity <= this.TargetOpacity) || (this.OpacityRateOfChange > 0.0 && this.ObjectOpacity >= this.TargetOpacity))
			{
				this.AnimationTimer.Stop();
				this.AnimationTimer.Enabled = false;
			}
		}

		public void UpdateAnimationTimerInterval(int NewInterval)
		{
			this.AnimationTimer.Interval = NewInterval;
		}

		public void SetBitmap()
		{
			Bitmap bitmap;
			try
			{
				if (this.DockSettings.CentreImage.Path.StartsWith(".\\"))
				{
					bitmap = new Bitmap(Application.StartupPath + this.DockSettings.CentreImage.Path.Substring(1, this.DockSettings.CentreImage.Path.Length - 1));
				}
				else
				{
					bitmap = new Bitmap(this.DockSettings.CentreImage.Path);
				}
			}
			catch (Exception)
			{
				bitmap = new Bitmap(ImageResources.CircleDockIconCentreImage);
			}
			base.SetBitmapManaged(ref bitmap);
		}

		protected override void OnMove(EventArgs e)
		{
			base.OnMove(e);
		}

		private void CentreObject_MouseHover(object sender, EventArgs e)
		{
			base.Activate();
		}

		private void CentreObject_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Left)
			{
				this.ParentObject.RotateDock(-1.0);
			}
			else if (e.KeyCode == Keys.Right)
			{
				this.ParentObject.RotateDock(1.0);
			}
			else if (e.KeyCode == Keys.Down)
			{
				this.ParentObject.RotateDock(-1.0);
			}
			else if (e.KeyCode == Keys.Up)
			{
				this.ParentObject.RotateDock(1.0);
			}
		}

		private void CentreObject_DragDrop(object sender, DragEventArgs e)
		{
			string[] array = e.Data.GetData(DataFormats.FileDrop) as string[];
			if (this.DockSettings.General.IconReplacementMode && array != null && array.Length == 1 && array[0].ToUpper().EndsWith(".PNG"))
			{
				this.ChangeIconDialog.FileName = array[0];
				this.ChangeIconDialog_FileOk(this, null);
			}
			else if (array != null)
			{
				this.ParentObject.CentreObject.Activate();
				for (int i = 0; i < array.Length; i++)
				{
					this.ParentObject.AddDockItem("[link]", array[i], "Link", "", "");
				}
				this.ParentObject.PositionCurrentLevel();
			}
		}

		private void CentreObject_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Link;
		}

		private void CentreObject_MouseWheel(object sender, MouseEventArgs e)
		{
			if (e.Delta < 0)
			{
				this.ParentObject.RotateDock(1.0);
			}
			else
			{
				this.ParentObject.RotateDock(-1.0);
			}
		}

		private void CentreObject_MouseMove(object sender, MouseEventArgs e)
		{
			if (!this.DockSettings.General.lockDockPosition && this.LeftMouseButtonDown)
			{
				if (this.ThisObjectMovedWithLeftMouse || Math.Abs(Cursor.Position.X - this.previousMousePosition.X) > this.DockSettings.General.ClickSensitivity || Math.Abs(Cursor.Position.Y - this.previousMousePosition.Y) > this.DockSettings.General.ClickSensitivity)
				{
					this.ThisObjectMovedWithLeftMouse = true;
					base.Location = new Point(Cursor.Position.X - this.StoredMouseOffset.X, Cursor.Position.Y - this.StoredMouseOffset.Y);
					if (this.ParentObject != null)
					{
						this.ParentObject.ChildMoved(this);
					}
				}
			}
		}

		private void CentreObject_MouseUp(object sender, MouseEventArgs e)
		{
			if (!this.ThisObjectMovedWithLeftMouse)
			{
				if (e.Button == MouseButtons.Left && this.LeftMouseButtonDown)
				{
					this.LeftMouseButtonDown = false;
					if (this.ParentObject.DockIsVisible || this.DockSettings.General.zLevel == "Always On Bottom")
					{
						base.DrawBitmapManaged(this.ObjectSize.Width, this.ObjectSize.Height, false, 0, 0, false, 0, 0, 0, 0, true, this.DockSettings.DockItems.DefaultOpacity);
						this.PreviousOpacity = this.ObjectOpacity;
						if (this.ParentObject.AtRootLevel())
						{
							if (!this.ParentObject.showingLevel)
							{
								if (this.DockSettings.CentreImage.ShowStartMenuWhenClicked)
								{
									IntPtr intPtr = (IntPtr)Pinvoke.Win32.FindWindow("DV2ControlHost", null);
									IntPtr intPtr2 = (IntPtr)Pinvoke.Win32.FindWindow("Shell_TrayWnd", null);
									IntPtr hWnd = Pinvoke.Win32.FindWindowEx(intPtr2, IntPtr.Zero, "Button", "Start");
									Pinvoke.Win32.WINDOWINFO wINDOWINFO = default(Pinvoke.Win32.WINDOWINFO);
									Pinvoke.Win32.GetWindowInfo(intPtr, ref wINDOWINFO);
									Point point = new Point(base.Location.X, base.Location.Y - (wINDOWINFO.rcWindow.bottom - wINDOWINFO.rcWindow.top));
									if (point.Y < SystemInformation.VirtualScreen.Top)
									{
										point = new Point(point.X, SystemInformation.VirtualScreen.Top);
									}
									if (point.Y + (wINDOWINFO.rcWindow.bottom - wINDOWINFO.rcWindow.top) > SystemInformation.VirtualScreen.Bottom)
									{
										point = new Point(point.X, SystemInformation.VirtualScreen.Bottom - (wINDOWINFO.rcWindow.bottom - wINDOWINFO.rcWindow.top));
									}
									if (point.X + (wINDOWINFO.rcWindow.right - wINDOWINFO.rcWindow.left) > SystemInformation.VirtualScreen.Right)
									{
										point = new Point(SystemInformation.VirtualScreen.Right - (wINDOWINFO.rcWindow.right - wINDOWINFO.rcWindow.left), point.Y);
									}
									if (point.X < SystemInformation.VirtualScreen.Left)
									{
										point = new Point(SystemInformation.VirtualScreen.Left, point.Y);
									}
									Pinvoke.Win32.SetWindowPos(intPtr2, IntPtr.Zero, 0, 0, 0, 0, 67u);
									Pinvoke.Win32.SetWindowPos(hWnd, IntPtr.Zero, 0, 0, 0, 0, 67u);
									Pinvoke.Win32.SetWindowPos(intPtr, (IntPtr)(-1), point.X, point.Y, 0, 0, 97u);
								}
								if (this.DockSettings.DockItems.HideDockAfterExecution)
								{
									this.ParentObject.HideAll();
								}
							}
						}
						else
						{
							this.ParentObject.ShowLevelUp();
						}
					}
				}
			}
			else
			{
				this.ThisObjectMovedWithLeftMouse = false;
				if (e.Button == MouseButtons.Left && this.LeftMouseButtonDown)
				{
					this.LeftMouseButtonDown = false;
					if (this.ParentObject.DockIsVisible || this.DockSettings.General.zLevel == "Always On Bottom")
					{
						base.DrawBitmapManaged(this.ObjectSize.Width, this.ObjectSize.Height, false, 0, 0, false, 0, 0, 0, 0, true, this.DockSettings.DockItems.DefaultOpacity);
						this.PreviousOpacity = this.ObjectOpacity;
					}
				}
			}
		}

		private void CentreObject_MouseDown(object sender, MouseEventArgs e)
		{
			base.Activate();
			base.BringToFront();
			if (e.Button == MouseButtons.Left)
			{
				this.LeftMouseButtonDown = true;
				this.ThisObjectMovedWithLeftMouse = false;
				this.PreviousOpacity = this.ObjectOpacity;
				base.DrawBitmapManaged(this.ObjectSize.Width, this.ObjectSize.Height, false, 0, 0, false, 0, 0, 0, 0, true, this.ObjectOpacity / 2);
				this.StoredMouseOffset = new Point(Cursor.Position.X - base.Location.X, Cursor.Position.Y - base.Location.Y);
				this.previousMousePosition = Cursor.Position;
			}
		}

		private void CentreObject_MouseEnter(object sender, EventArgs e)
		{
			base.BringToFront();
		}

		private void quitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.ParentObject.ChildRequestAction("QUIT");
		}

		private void dockFolderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.ParentObject.CentreObject.Activate();
			this.ParentObject.AddDockItem("[dockfolder]", "", this.Language.MainContextMenu.DockFolder, this.DockSettings.Folders.DockFolderImagePath, this.Language.MainContextMenu.DockFolder);
			this.ParentObject.PositionCurrentLevel();
		}

		private void blankIconToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.ParentObject.CentreObject.Activate();
			this.ParentObject.AddDockItem("[blanklink]", "", "Link", "", "");
			this.ParentObject.PositionCurrentLevel();
		}

		private void hideToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.ParentObject.HideAll();
		}

		private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.ParentObject.ShowSettingsPanel();
		}

		private void changeIconToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.ChangeIconDialog.InitialDirectory = Application.StartupPath + "\\System\\Icons";
			this.ChangeIconDialog.ShowDialog();
		}

		private void iconReplacementModeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.DockSettings.General.IconReplacementMode = this.iconReplacementModeToolStripMenuItem.Checked;
			this.DockSettings.SetEntry("General", "IconReplacementMode", this.iconReplacementModeToolStripMenuItem.Checked.ToString());
			if (this.DockSettings.General.IconReplacementMode)
			{
				FileOps.FileOps fileOps = new FileOps.FileOps(IntPtr.Zero, this.Language, this.DockSettings);
				fileOps.Open(Application.StartupPath + "\\System\\Icons", "", ProcessWindowStyle.Normal, base.Handle);
			}
		}

		private void pauseMouseHotkeysToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.ParentObject.pauseMouseHotkeysToolStripMenuItem.Checked = this.pauseMouseHotkeysToolStripMenuItem.Checked;
			this.ParentObject.SetMouseKeys();
		}

		private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			this.iconReplacementModeToolStripMenuItem.Checked = this.DockSettings.General.IconReplacementMode;
			this.pauseMouseHotkeysToolStripMenuItem.Checked = this.ParentObject.pauseMouseHotkeysToolStripMenuItem.Checked;
		}

		private void ChangeIconDialog_FileOk(object sender, CancelEventArgs e)
		{
			string text;
			if (this.ChangeIconDialog.FileName.StartsWith(Application.StartupPath + "\\"))
			{
				text = "." + this.ChangeIconDialog.FileName.Substring(Application.StartupPath.Length, this.ChangeIconDialog.FileName.Length - Application.StartupPath.Length);
			}
			else
			{
				text = this.ChangeIconDialog.FileName;
			}
			this.DockSettings.SetEntry("CentreImage", "Path", text);
			this.DockSettings.CentreImage.Path = text;
			this.SetBitmap();
			Size objectSize = this.ObjectSize;
			base.DrawBitmapManaged(objectSize.Width, objectSize.Height, false, 0, 0, false, 0, 0, 0, 0, false, 0);
		}
	}
}
