using System;
using System.Collections.Generic;
using System.IO;
using Gtk;

public partial class MainWindow : Gtk.Window
{
	private Gtk.ListStore listdata;
	
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build ();
		setupFilelist();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	
	private void setupFilelist() {
		listdata = new Gtk.ListStore(typeof(string));
		this.filelist.Model = listdata;
		this.filelist.AppendColumn("Filename", new Gtk.CellRendererText(), "text", 0);
	}
	
	private string getFilename(string fullPath) {
			string[] segments = new Uri(fullPath).Segments;
			return segments[segments.Length - 1];
	}
	
	private void loadCurrentFolder() {
		this.listdata.Clear();
		List<string> files = new List<string>(Directory.GetFiles(new Uri(this.folder.Uri).LocalPath));
		files = files.ConvertAll<string>(new Converter<string, string>(this.getFilename));
		foreach(string file in files) {
			this.listdata.AppendValues(file);
		}
	}
	
	protected virtual void loadFolder (object sender, System.EventArgs e) {
		this.loadCurrentFolder();
	}
}
	
	
