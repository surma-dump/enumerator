
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.VBox vbox1;

	private global::Gtk.Table table1;

	private global::Gtk.Button button45;

	private global::Gtk.FileChooserButton directorychooser;

	private global::Gtk.Label label1;

	private global::Gtk.Label label2;

	private global::Gtk.Button refreshbutton;

	private global::Gtk.Entry templatefield;

	private global::Gtk.HBox hbox1;

	private global::Gtk.ScrolledWindow GtkScrolledWindow;

	private global::Gtk.TreeView filelist;

	private global::Gtk.HButtonBox hbuttonbox1;

	private global::Gtk.Button applybutton;

	private global::Gtk.Button cancelbutton;

	private global::Gtk.Button dummybutton;

	private global::Gtk.HSeparator hseparator1;

	private global::Gtk.ScrolledWindow GtkScrolledWindow1;

	private global::Gtk.TextView logview;

	private global::Gtk.ProgressBar progressbar;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("Enumerator");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		this.BorderWidth = ((uint)(6));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.vbox1 = new global::Gtk.VBox ();
		this.vbox1.Name = "vbox1";
		this.vbox1.Spacing = 6;
		// Container child vbox1.Gtk.Box+BoxChild
		this.table1 = new global::Gtk.Table (((uint)(2)), ((uint)(3)), false);
		this.table1.Name = "table1";
		this.table1.RowSpacing = ((uint)(6));
		this.table1.ColumnSpacing = ((uint)(6));
		// Container child table1.Gtk.Table+TableChild
		this.button45 = new global::Gtk.Button ();
		this.button45.CanFocus = true;
		this.button45.Name = "button45";
		this.button45.UseStock = true;
		this.button45.UseUnderline = true;
		this.button45.Label = "gtk-help";
		this.table1.Add (this.button45);
		global::Gtk.Table.TableChild w1 = ((global::Gtk.Table.TableChild)(this.table1[this.button45]));
		w1.TopAttach = ((uint)(1));
		w1.BottomAttach = ((uint)(2));
		w1.LeftAttach = ((uint)(2));
		w1.RightAttach = ((uint)(3));
		w1.XOptions = ((global::Gtk.AttachOptions)(4));
		w1.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table1.Gtk.Table+TableChild
		this.directorychooser = new global::Gtk.FileChooserButton (global::Mono.Unix.Catalog.GetString ("Select Folder"), ((global::Gtk.FileChooserAction)(2)));
		this.directorychooser.Name = "directorychooser";
		this.table1.Add (this.directorychooser);
		global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table1[this.directorychooser]));
		w2.LeftAttach = ((uint)(1));
		w2.RightAttach = ((uint)(2));
		w2.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table1.Gtk.Table+TableChild
		this.label1 = new global::Gtk.Label ();
		this.label1.Name = "label1";
		this.label1.Xalign = 1f;
		this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("Directory:");
		this.table1.Add (this.label1);
		global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.table1[this.label1]));
		w3.XOptions = ((global::Gtk.AttachOptions)(4));
		w3.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table1.Gtk.Table+TableChild
		this.label2 = new global::Gtk.Label ();
		this.label2.Name = "label2";
		this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Template Name:");
		this.table1.Add (this.label2);
		global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.table1[this.label2]));
		w4.TopAttach = ((uint)(1));
		w4.BottomAttach = ((uint)(2));
		w4.XOptions = ((global::Gtk.AttachOptions)(4));
		w4.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table1.Gtk.Table+TableChild
		this.refreshbutton = new global::Gtk.Button ();
		this.refreshbutton.CanFocus = true;
		this.refreshbutton.Name = "refreshbutton";
		this.refreshbutton.UseStock = true;
		this.refreshbutton.UseUnderline = true;
		this.refreshbutton.Label = "gtk-refresh";
		this.table1.Add (this.refreshbutton);
		global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table1[this.refreshbutton]));
		w5.LeftAttach = ((uint)(2));
		w5.RightAttach = ((uint)(3));
		w5.XOptions = ((global::Gtk.AttachOptions)(4));
		w5.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table1.Gtk.Table+TableChild
		this.templatefield = new global::Gtk.Entry ();
		this.templatefield.CanFocus = true;
		this.templatefield.Name = "templatefield";
		this.templatefield.Text = global::Mono.Unix.Catalog.GetString ("Tv.Show.s01e{0:00}");
		this.templatefield.IsEditable = true;
		this.templatefield.InvisibleChar = '●';
		this.table1.Add (this.templatefield);
		global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1[this.templatefield]));
		w6.TopAttach = ((uint)(1));
		w6.BottomAttach = ((uint)(2));
		w6.LeftAttach = ((uint)(1));
		w6.RightAttach = ((uint)(2));
		w6.YOptions = ((global::Gtk.AttachOptions)(4));
		this.vbox1.Add (this.table1);
		global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.table1]));
		w7.Position = 0;
		w7.Expand = false;
		w7.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.hbox1 = new global::Gtk.HBox ();
		this.hbox1.Name = "hbox1";
		this.hbox1.Spacing = 6;
		// Container child hbox1.Gtk.Box+BoxChild
		this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow.Name = "GtkScrolledWindow";
		this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
		this.filelist = new global::Gtk.TreeView ();
		this.filelist.CanFocus = true;
		this.filelist.Name = "filelist";
		this.filelist.Reorderable = true;
		this.GtkScrolledWindow.Add (this.filelist);
		this.hbox1.Add (this.GtkScrolledWindow);
		global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.GtkScrolledWindow]));
		w9.Position = 0;
		this.vbox1.Add (this.hbox1);
		global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox1]));
		w10.Position = 1;
		// Container child vbox1.Gtk.Box+BoxChild
		this.hbuttonbox1 = new global::Gtk.HButtonBox ();
		this.hbuttonbox1.Name = "hbuttonbox1";
		this.hbuttonbox1.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(1));
		// Container child hbuttonbox1.Gtk.ButtonBox+ButtonBoxChild
		this.applybutton = new global::Gtk.Button ();
		this.applybutton.CanFocus = true;
		this.applybutton.Name = "applybutton";
		this.applybutton.UseStock = true;
		this.applybutton.UseUnderline = true;
		this.applybutton.Label = "gtk-apply";
		this.hbuttonbox1.Add (this.applybutton);
		global::Gtk.ButtonBox.ButtonBoxChild w11 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.hbuttonbox1[this.applybutton]));
		w11.Expand = false;
		w11.Fill = false;
		// Container child hbuttonbox1.Gtk.ButtonBox+ButtonBoxChild
		this.cancelbutton = new global::Gtk.Button ();
		this.cancelbutton.Sensitive = false;
		this.cancelbutton.CanFocus = true;
		this.cancelbutton.Name = "cancelbutton";
		this.cancelbutton.UseStock = true;
		this.cancelbutton.UseUnderline = true;
		this.cancelbutton.Label = "gtk-cancel";
		this.hbuttonbox1.Add (this.cancelbutton);
		global::Gtk.ButtonBox.ButtonBoxChild w12 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.hbuttonbox1[this.cancelbutton]));
		w12.Position = 1;
		w12.Expand = false;
		w12.Fill = false;
		// Container child hbuttonbox1.Gtk.ButtonBox+ButtonBoxChild
		this.dummybutton = new global::Gtk.Button ();
		this.dummybutton.CanFocus = true;
		this.dummybutton.Name = "dummybutton";
		this.dummybutton.UseUnderline = true;
		this.dummybutton.Label = global::Mono.Unix.Catalog.GetString ("Add Dummy");
		this.hbuttonbox1.Add (this.dummybutton);
		global::Gtk.ButtonBox.ButtonBoxChild w13 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.hbuttonbox1[this.dummybutton]));
		w13.Position = 2;
		w13.Expand = false;
		w13.Fill = false;
		this.vbox1.Add (this.hbuttonbox1);
		global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbuttonbox1]));
		w14.Position = 2;
		w14.Expand = false;
		w14.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.hseparator1 = new global::Gtk.HSeparator ();
		this.hseparator1.Name = "hseparator1";
		this.vbox1.Add (this.hseparator1);
		global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hseparator1]));
		w15.Position = 3;
		w15.Expand = false;
		w15.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
		this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
		this.logview = new global::Gtk.TextView ();
		this.logview.CanFocus = true;
		this.logview.Name = "logview";
		this.logview.Editable = false;
		this.logview.CursorVisible = false;
		this.GtkScrolledWindow1.Add (this.logview);
		this.vbox1.Add (this.GtkScrolledWindow1);
		global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.GtkScrolledWindow1]));
		w17.Position = 4;
		w17.Expand = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.progressbar = new global::Gtk.ProgressBar ();
		this.progressbar.Name = "progressbar";
		this.vbox1.Add (this.progressbar);
		global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.progressbar]));
		w18.Position = 5;
		w18.Expand = false;
		w18.Fill = false;
		this.Add (this.vbox1);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 400;
		this.DefaultHeight = 316;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.refreshbutton.Clicked += new global::System.EventHandler (this.LoadFolderEvent);
		this.directorychooser.SelectionChanged += new global::System.EventHandler (this.LoadFolderEvent);
		this.applybutton.Clicked += new global::System.EventHandler (this.RenameFilesEvent);
		this.dummybutton.Clicked += new global::System.EventHandler (this.AddDummyEvent);
	}
}
