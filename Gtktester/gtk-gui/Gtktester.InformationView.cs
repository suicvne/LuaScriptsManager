
// This file has been generated by the GUI designer. Do not modify.
namespace Gtktester
{
	public partial class InformationView
	{
		private global::Gtk.VBox vbox1;
		
		private global::Gtk.Label scriptTitleLabel;
		
		private global::Gtk.Label descriptionLabel;
		
		private global::Gtk.ScrolledWindow GtkScrolledWindow;
		
		private global::Gtk.TextView usagePreview;
		
		private global::Gtk.HBox hbox1;
		
		private global::Gtk.Button button1;
		
		private global::Gtk.Image image2;
		
		private global::Gtk.Button button2;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Gtktester.InformationView
			global::Stetic.BinContainer.Attach (this);
			this.Name = "Gtktester.InformationView";
			// Container child Gtktester.InformationView.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.scriptTitleLabel = new global::Gtk.Label ();
			this.scriptTitleLabel.Name = "scriptTitleLabel";
			this.scriptTitleLabel.LabelProp = global::Mono.Unix.Catalog.GetString ("Script Title");
			this.vbox1.Add (this.scriptTitleLabel);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.scriptTitleLabel]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.descriptionLabel = new global::Gtk.Label ();
			this.descriptionLabel.Name = "descriptionLabel";
			this.descriptionLabel.LabelProp = global::Mono.Unix.Catalog.GetString ("Description");
			this.vbox1.Add (this.descriptionLabel);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.descriptionLabel]));
			w2.Position = 1;
			w2.Expand = false;
			w2.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.usagePreview = new global::Gtk.TextView ();
			this.usagePreview.Buffer.Text = "Test!";
			this.usagePreview.CanFocus = true;
			this.usagePreview.Name = "usagePreview";
			this.usagePreview.Editable = false;
			this.usagePreview.WrapMode = ((global::Gtk.WrapMode)(2));
			this.GtkScrolledWindow.Add (this.usagePreview);
			this.vbox1.Add (this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.GtkScrolledWindow]));
			w4.Position = 2;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 134;
			// Container child hbox1.Gtk.Box+BoxChild
			this.button1 = new global::Gtk.Button ();
			this.button1.CanFocus = true;
			this.button1.Name = "button1";
			this.button1.UseUnderline = true;
			this.button1.Label = global::Mono.Unix.Catalog.GetString ("GtkButton");
			this.hbox1.Add (this.button1);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.button1]));
			w5.Position = 0;
			w5.Expand = false;
			w5.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.image2 = new global::Gtk.Image ();
			this.image2.Name = "image2";
			this.hbox1.Add (this.image2);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.image2]));
			w6.Position = 1;
			w6.Expand = false;
			w6.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.button2 = new global::Gtk.Button ();
			this.button2.CanFocus = true;
			this.button2.Name = "button2";
			this.button2.UseUnderline = true;
			this.button2.Label = global::Mono.Unix.Catalog.GetString ("GtkButton");
			this.hbox1.Add (this.button2);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.button2]));
			w7.Position = 2;
			w7.Expand = false;
			w7.Fill = false;
			this.vbox1.Add (this.hbox1);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hbox1]));
			w8.Position = 3;
			w8.Expand = false;
			w8.Fill = false;
			this.Add (this.vbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
		}
	}
}
