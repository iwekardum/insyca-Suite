using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace inSyca.foundation.framework.application.windowsforms
{
	public class FrameworkManager
	{
		private readonly NotifyIcon notifyIcon;
		public bool IsDecorated { get; private set; }

		public FrameworkManager(NotifyIcon notifyIcon)
		{
			this.notifyIcon = notifyIcon;
		}

		public void BuildContextMenu(ContextMenuStrip contextMenuStrip)
		{
			contextMenuStrip.Items.Clear();
			//contextMenuStrip.Items.AddRange(
			//    projectDict.Keys.OrderBy(project => project).Select(project => BuildSubMenu(project)).ToArray());
			contextMenuStrip.Items.AddRange(
				new ToolStripItem[] {
				   new ToolStripSeparator(),
					//ToolStripMenuItemWithHandler("&Open HOSTS file", openHostsFileItem_Click),
					//ToolStripMenuItemWithHandler("Open HOSTS &folder", openHostsFolderItem_Click)
					ToolStripMenuItemWithHandler("&Open HOSTS file", null),
					ToolStripMenuItemWithHandler("Open HOSTS &folder", null)
				});
		}

		# region support methods

		private ToolStripMenuItem ToolStripMenuItemWithHandler(
			string displayText, int enabledCount, int disabledCount, EventHandler eventHandler)
		{
			var item = new ToolStripMenuItem(displayText);
			if (eventHandler != null) { item.Click += eventHandler; }

			//item.Image = (enabledCount > 0 && disabledCount > 0) ? Properties.Resources.signal_yellow
			//             : (enabledCount > 0) ? Properties.Resources.signal_green
			//             : (disabledCount > 0) ? Properties.Resources.signal_red
			//             : null;
			item.ToolTipText = (enabledCount > 0 && disabledCount > 0) ?
												 string.Format("{0} enabled, {1} disabled", enabledCount, disabledCount)
						 : (enabledCount > 0) ? string.Format("{0} enabled", enabledCount)
						 : (disabledCount > 0) ? string.Format("{0} disabled", disabledCount)
						 : "";
			return item;
		}

		public ToolStripMenuItem ToolStripMenuItemWithHandler(string displayText, EventHandler eventHandler)
		{
			return ToolStripMenuItemWithHandler(displayText, 0, 0, eventHandler);
		}

		# endregion support methods

	}
}
