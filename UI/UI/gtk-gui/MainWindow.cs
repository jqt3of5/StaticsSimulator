
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.UIManager UIManager;
	private global::Gtk.Action FileAction;
	private global::Gtk.Action HelpAction;
	private global::Gtk.Action NewAction;
	private global::Gtk.Action OpenAction;
	private global::Gtk.Action SaveAction;
	private global::Gtk.Action SaveAsAction;
	private global::Gtk.Action ExitAction;
	private global::Gtk.Action AboutAction;
	private global::Gtk.RadioAction newAction;
	private global::Gtk.RadioAction refreshAction;
	private global::Gtk.RadioAction redoAction;
	private global::Gtk.Action firstAction;
	private global::Gtk.Action secondAction;
	private global::Gtk.Action thirdAction;
	private global::Gtk.Action fourthAction;
	private global::Gtk.Action fifthAction;
	private global::Gtk.VBox vbox1;
	private global::Gtk.MenuBar menubar2;
	private global::Gtk.VBox vbox2;
	private global::Gtk.Alignment toolbarAlignment;
	private global::Gtk.Frame frame1;
	private global::Gtk.Alignment DrawingBoxAlignment;
	private global::Gtk.Label GtkLabel3;
	private global::Gtk.Statusbar _statusBar;
	
	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.UIManager = new global::Gtk.UIManager ();
		global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup ("Default");
		this.FileAction = new global::Gtk.Action ("FileAction", global::Mono.Unix.Catalog.GetString ("File"), null, null);
		this.FileAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("File");
		w1.Add (this.FileAction, null);
		this.HelpAction = new global::Gtk.Action ("HelpAction", global::Mono.Unix.Catalog.GetString ("Help"), null, null);
		this.HelpAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Help");
		w1.Add (this.HelpAction, null);
		this.NewAction = new global::Gtk.Action ("NewAction", global::Mono.Unix.Catalog.GetString ("New"), null, null);
		this.NewAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("New");
		w1.Add (this.NewAction, null);
		this.OpenAction = new global::Gtk.Action ("OpenAction", global::Mono.Unix.Catalog.GetString ("Open"), null, null);
		this.OpenAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Open");
		w1.Add (this.OpenAction, null);
		this.SaveAction = new global::Gtk.Action ("SaveAction", global::Mono.Unix.Catalog.GetString ("Save"), null, null);
		this.SaveAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Save");
		w1.Add (this.SaveAction, null);
		this.SaveAsAction = new global::Gtk.Action ("SaveAsAction", global::Mono.Unix.Catalog.GetString ("Save As"), null, null);
		this.SaveAsAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Save As");
		w1.Add (this.SaveAsAction, null);
		this.ExitAction = new global::Gtk.Action ("ExitAction", global::Mono.Unix.Catalog.GetString ("Exit"), null, null);
		this.ExitAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Exit");
		w1.Add (this.ExitAction, null);
		this.AboutAction = new global::Gtk.Action ("AboutAction", global::Mono.Unix.Catalog.GetString ("About"), null, null);
		this.AboutAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("About");
		w1.Add (this.AboutAction, null);
		this.newAction = new global::Gtk.RadioAction ("newAction", null, null, "gtk-new", 0);
		this.newAction.Group = new global::GLib.SList (global::System.IntPtr.Zero);
		w1.Add (this.newAction, null);
		this.refreshAction = new global::Gtk.RadioAction ("refreshAction", null, null, "gtk-refresh", 0);
		this.refreshAction.Group = this.newAction.Group;
		w1.Add (this.refreshAction, null);
		this.redoAction = new global::Gtk.RadioAction ("redoAction", null, null, "gtk-redo", 0);
		this.redoAction.Group = this.newAction.Group;
		w1.Add (this.redoAction, null);
		this.firstAction = new global::Gtk.Action ("firstAction", null, null, "gtk-goto-bottom");
		w1.Add (this.firstAction, null);
		this.secondAction = new global::Gtk.Action ("secondAction", null, null, "gtk-goto-first");
		w1.Add (this.secondAction, null);
		this.thirdAction = new global::Gtk.Action ("thirdAction", null, null, "gtk-goto-last");
		w1.Add (this.thirdAction, null);
		this.fourthAction = new global::Gtk.Action ("fourthAction", null, null, "gtk-go-back");
		w1.Add (this.fourthAction, null);
		this.fifthAction = new global::Gtk.Action ("fifthAction", global::Mono.Unix.Catalog.GetString ("_Down"), null, "gtk-go-down");
		this.fifthAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Down");
		w1.Add (this.fifthAction, null);
		this.UIManager.InsertActionGroup (w1, 0);
		this.AddAccelGroup (this.UIManager.AccelGroup);
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("MainWindow");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.vbox1 = new global::Gtk.VBox ();
		this.vbox1.Name = "vbox1";
		this.vbox1.Spacing = 6;
		// Container child vbox1.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString ("<ui><menubar name='menubar2'><menu name='FileAction' action='FileAction'><menuitem name='NewAction' action='NewAction'/><menuitem name='OpenAction' action='OpenAction'/><menuitem name='SaveAction' action='SaveAction'/><menuitem name='SaveAsAction' action='SaveAsAction'/><menuitem name='ExitAction' action='ExitAction'/></menu><menu name='HelpAction' action='HelpAction'><menuitem name='AboutAction' action='AboutAction'/></menu></menubar></ui>");
		this.menubar2 = ((global::Gtk.MenuBar)(this.UIManager.GetWidget ("/menubar2")));
		this.menubar2.Name = "menubar2";
		this.vbox1.Add (this.menubar2);
		global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.menubar2]));
		w2.Position = 0;
		w2.Expand = false;
		w2.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.vbox2 = new global::Gtk.VBox ();
		this.vbox2.Name = "vbox2";
		this.vbox2.Spacing = 6;
		// Container child vbox2.Gtk.Box+BoxChild
		this.toolbarAlignment = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
		this.toolbarAlignment.Name = "toolbarAlignment";
		this.vbox2.Add (this.toolbarAlignment);
		global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.toolbarAlignment]));
		w3.Position = 0;
		w3.Expand = false;
		// Container child vbox2.Gtk.Box+BoxChild
		this.frame1 = new global::Gtk.Frame ();
		this.frame1.WidthRequest = 0;
		this.frame1.HeightRequest = 0;
		this.frame1.Name = "frame1";
		this.frame1.BorderWidth = ((uint)(1));
		// Container child frame1.Gtk.Container+ContainerChild
		this.DrawingBoxAlignment = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
		this.DrawingBoxAlignment.Name = "DrawingBoxAlignment";
		this.DrawingBoxAlignment.LeftPadding = ((uint)(12));
		this.frame1.Add (this.DrawingBoxAlignment);
		this.GtkLabel3 = new global::Gtk.Label ();
		this.GtkLabel3.Name = "GtkLabel3";
		this.GtkLabel3.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Drawing Area</b>");
		this.GtkLabel3.UseMarkup = true;
		this.frame1.LabelWidget = this.GtkLabel3;
		this.vbox2.Add (this.frame1);
		global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.frame1]));
		w5.Position = 1;
		this.vbox1.Add (this.vbox2);
		global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.vbox2]));
		w6.Position = 1;
		// Container child vbox1.Gtk.Box+BoxChild
		this._statusBar = new global::Gtk.Statusbar ();
		this._statusBar.Name = "_statusBar";
		this._statusBar.Spacing = 6;
		this.vbox1.Add (this._statusBar);
		global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this._statusBar]));
		w7.Position = 2;
		w7.Expand = false;
		w7.Fill = false;
		this.Add (this.vbox1);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 480;
		this.DefaultHeight = 360;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
	}
}
