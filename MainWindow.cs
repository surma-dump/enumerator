using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Gtk;

public partial class MainWindow : Gtk.Window
{
	private const int RENAME_COLUMN = 0;
	private const int FILENAME_COLUMN = 1;
	private Gtk.ListStore listdata;
	
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build ();
		SetupFilelist();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	private Gtk.CellRendererToggle CreateRenameCellRenderer() {
		Gtk.CellRendererToggle renameToggle = new Gtk.CellRendererToggle();
		renameToggle.Activatable = true;
		renameToggle.Toggled += (object o, Gtk.ToggledArgs ta) => {
			Gtk.TreeIter it;
			if(this.listdata.GetIter(out it, new TreePath(ta.Path))) {
				bool oldValue = (bool)this.listdata.GetValue(it, RENAME_COLUMN);
				this.listdata.SetValue(it, RENAME_COLUMN, !oldValue);
			}
		};
		return renameToggle;
	}

	private void SetupFilelist() {
		listdata = new Gtk.ListStore(typeof(bool), typeof(string));
		this.filelist.Model = listdata;
		this.filelist.AppendColumn("Rename?", CreateRenameCellRenderer(), "active", RENAME_COLUMN);
		this.filelist.AppendColumn("Old Filename", new Gtk.CellRendererText(), "text", FILENAME_COLUMN);
	}

	private string PathToFilename(string fullPath) {
			string[] segments = new Uri(fullPath).Segments;
			return segments[segments.Length - 1];
	}

	private string GetSelectedDirectoryPath() {
		return new Uri (this.directorychooser.Uri).LocalPath;
	}

	private void LoadCurrentFolder() {
		this.listdata.Clear();
		List<string> files = new List<string>(Directory.GetFiles(GetSelectedDirectoryPath()));
		files = files.ConvertAll<string>(new Converter<string, string>(this.PathToFilename));
		foreach(string file in files) {
			this.listdata.AppendValues(true, file);
		}
	}

	protected virtual void LoadFolderEvent (object sender, System.EventArgs e) {
		this.LoadCurrentFolder();
	}

	private void RenameFilesThreadStart(object param) {
		RenameFiles();
	}

	private void Log(String line) {
		this.logview.Buffer.Text += line+"\n";
		Gtk.ScrolledWindow view = (Gtk.ScrolledWindow)this.logview.Parent;
		view.Vadjustment.Value = view.Vadjustment.Upper;
	}

	private void RenameFiles() {
		string template = this.templatefield.Text;
		int i = 0;
		foreach(object[] rowitem in this.listdata) {
			if(!(bool)rowitem[RENAME_COLUMN]) {
				continue;
			}
			
			i++;
			string filename = (string)rowitem[FILENAME_COLUMN];
			string filepath = new Uri(this.directorychooser.Uri + "/" + filename).LocalPath;
			string extension = System.IO.Path.GetExtension(filename);
			string newfilename = String.Format(template, i) + extension;
			string newfilepath = new Uri(this.directorychooser.Uri + "/" + newfilename).LocalPath;

			Log(filename + " => " + newfilename);
			try {
				File.Move(filepath, newfilepath);
			} catch (IOException e) {
				Log("\t"+e.Message);
				continue;
			}
		}
		LoadCurrentFolder();
		ProcessDone();
	}

	private void ProcessStarted() {
		this.directorychooser.Sensitive = false;
		this.templatefield.Sensitive = false;
		this.applybutton.Sensitive = false;
		this.refreshbutton.Sensitive = false;
		this.dummybutton.Sensitive = false;
		this.cancelbutton.Sensitive = true;
	}

	private void ProcessDone() {
		this.directorychooser.Sensitive = true;
		this.templatefield.Sensitive = true;
		this.applybutton.Sensitive = true;
		this.refreshbutton.Sensitive = true;
		this.dummybutton.Sensitive = true;
		this.cancelbutton.Sensitive = false;
	}

	protected virtual void RenameFilesEvent (object sender, System.EventArgs e)
	{
		ProcessStarted();
		//Thread t = new Thread(new ParameterizedThreadStart(this.RenameFilesThreadStart));
		//t.Start();
		this.RenameFiles();
	}
	
	protected virtual void ShowHelpEvent (object sender, System.EventArgs e)
	{
		System.Diagnostics.Process.Start("http://blog.stevex.net/string-formatting-in-csharp/");
	}

	private void AddDummy()
	{
		string dir = GetSelectedDirectoryPath();
		int count = 0;
		string dummyFilePath = "";

		do {
			dummyFilePath = new Uri(String.Format(dir+"/{0:000}.dummy", count)).LocalPath;
			count++;
		} while(File.Exists(dummyFilePath));

		File.Create(dummyFilePath).Close();
		LoadCurrentFolder();
	}

	protected virtual void AddDummyEvent (object sender, System.EventArgs e)
	{
		AddDummy();
	}
}
