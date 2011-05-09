using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Gtk;

public partial class MainWindow : Gtk.Window
{
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

	private void SetupFilelist() {
		listdata = new Gtk.ListStore(typeof(string));
		this.filelist.Model = listdata;
		this.filelist.AppendColumn("Filename", new Gtk.CellRendererText(), "text", 0);
	}

	private string PathToFilename(string fullPath) {
			string[] segments = new Uri(fullPath).Segments;
			return segments[segments.Length - 1];
	}

	private void LoadCurrentFolder() {
		this.listdata.Clear();
		List<string> files = new List<string>(Directory.GetFiles(new Uri(this.directorychooser.Uri).LocalPath));
		files = files.ConvertAll<string>(new Converter<string, string>(this.PathToFilename));
		foreach(string file in files) {
			this.listdata.AppendValues(file);
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
			i++;
			string filename = (string)rowitem[0];
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
		this.cancelbutton.Sensitive = true;
	}

	private void ProcessDone() {
		this.directorychooser.Sensitive = true;
		this.templatefield.Sensitive = true;
		this.applybutton.Sensitive = true;
		this.refreshbutton.Sensitive = true;
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
}
