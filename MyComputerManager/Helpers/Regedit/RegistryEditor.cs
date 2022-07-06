using System.Diagnostics;
using Microsoft.Win32;

namespace DevTools.RegistryJump
{
	public static class RegistryEditor
	{
		private const string SAVE_LAST_KEY = @"Software\Microsoft\Windows\CurrentVersion\Applets\Regedit\";
		public static string CurrentKey { get; private set; }

		public static void OpenRegistryEditor(string key)
		{
			// Quit running instance of regedit.
			Process[] process = Process.GetProcessesByName("regedit");
			if (process.Length == 1)
				process[0].Kill();

			// Convert the string into its KeyInfo equivalent.
			KeyInfo keyInfo = KeyInfo.Parse(key);

			// Concatenate the key name to the key root value.
			CurrentKey = $@"{keyInfo.Hkey}\{keyInfo.Name}";

			// Save the current key. 
			using (RegistryKey registrykey = Registry.CurrentUser.OpenSubKey(SAVE_LAST_KEY, true))
				registrykey?.SetValue("Lastkey", keyInfo.GetFullname());

			// Launch the registry editor.
			StartRegistryEditor();
		}

		public static void StartRegistryEditor()
		{
			Process.Start("regedit.exe");
		}
	}
}
