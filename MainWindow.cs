using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Gtk;

class Progress {
	public const byte FLAG_DONE    = 0x01;
	public const byte FLAG_MESSAGE = 0x02;
	public const byte FLAG_RUNNING = 0x04;

	public object lockObject = new object();
	public byte Flags = 0;
	public int CurrentPos = 0;
	public int Total = 0;
	public string Filename = "";
	public Queue<string> MsgQueue = new Queue<string>();

	public bool IsFlagSet(int flag) {
		return (this.Flags & flag) > 0;
	}

	public void SetFlag(byte flag) {
		lock(this.lockObject) {
			this.Flags |= flag;
		}
	}

	public void UnsetFlag(byte flag) {
		lock(this.lockObject) {
			this.Flags &= (byte)~flag;
		}
	}
}

public partial class MainWindow : Gtk.Window
{
	private const int RENAME_COLUMN = 0;
	private const int FILENAME_COLUMN = 1;
	private Gtk.ListStore listdata;
	private Progress progressdata = new Progress();

	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build ();
		SetupFilelistView();
		GLib.Timeout.Add(500, UpdateStatus);
	}

	private bool UpdateStatus() {
		if(this.progressdata.IsFlagSet(Progress.FLAG_MESSAGE)) {
			while(this.progressdata.MsgQueue.Count > 0) {
				this.logview.Buffer.Text += this.progressdata.MsgQueue.Dequeue()+"\n";
			}
			this.progressdata.UnsetFlag(Progress.FLAG_MESSAGE);
		}

		if(this.progressdata.IsFlagSet(Progress.FLAG_DONE)) {
			LoadCurrentFolder();
			ProcessDone();
			this.progressdata.UnsetFlag(Progress.FLAG_DONE);
		}

		if (this.progressdata.IsFlagSet(Progress.FLAG_RUNNING)) {
			this.progressbar.Text = String.Format("{0} ({1}/{2})", this.progressdata.Filename, this.progressdata.CurrentPos, this.progressdata.Total);
			this.progressbar.Fraction = (double)this.progressdata.CurrentPos / (double)this.progressdata.Total;
		} else {
			this.progressbar.Text = "Done";
			this.progressbar.Fraction = 1.0;
		}

		return true;
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

	private void SetupFilelistView() {
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

	protected virtual void LoadFolderEvent (object sender, EventArgs e) {
		this.LoadCurrentFolder();
	}

	private void RenameFilesThreadStart(object param) {
		RenameFiles();
	}

	private void Log(String line) {
		this.progressdata.MsgQueue.Enqueue(line);
		this.progressdata.SetFlag(Progress.FLAG_MESSAGE);
	}

	private int GetFileCount() {
		int i = 0;
		foreach(object[] rowitem in this.listdata) {
			if(!(bool)rowitem[RENAME_COLUMN]) {
				continue;
			}
			i++;
		}
		return i;
	}

	private void RenameFiles() {
		string template = this.templatefield.Text;
		this.progressdata.Total = GetFileCount();

		this.progressdata.SetFlag(Progress.FLAG_RUNNING);
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

			this.progressdata.CurrentPos = i;
			this.progressdata.Filename = filename;

			try {
				File.Move(filepath, newfilepath);
			} catch (IOException e) {
				Log("\t"+e.Message);
				continue;
			}
		}
		this.progressdata.SetFlag(Progress.FLAG_DONE);
		this.progressdata.UnsetFlag(Progress.FLAG_RUNNING);
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
		Thread t = new Thread(new ParameterizedThreadStart(this.RenameFilesThreadStart));
		t.Start();
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
