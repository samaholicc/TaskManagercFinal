namespace MyAppTodo
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        // Exemple de code dans Form1.Designer.cs

        /// <summary>
        /// Nettoyage des ressources qui sont utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de Windows Forms

        /// <summary>
        /// Méthode requise pour le concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        /// 
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            TodoListView = new ListView();
            columnHeaderId = new ColumnHeader();
            columnHeaderName = new ColumnHeader();
            columnHeaderStartDate = new ColumnHeader();
            columnHeaderEndDate = new ColumnHeader();
            columnHeaderStatus = new ColumnHeader();
            columnHeaderPriority = new ColumnHeader();
            buttonAjouter = new Button();
            searchButton = new Button();
            searchBox = new TextBox();
            Modifier_btn = new Button();
            Supprimer = new Button();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // TodoListView
            // 
            resources.ApplyResources(TodoListView, "TodoListView");
            TodoListView.BackColor = Color.Gainsboro;
            TodoListView.Columns.AddRange(new ColumnHeader[] { columnHeaderId, columnHeaderName, columnHeaderStartDate, columnHeaderEndDate, columnHeaderStatus, columnHeaderPriority });
            TodoListView.FullRowSelect = true;
            TodoListView.GridLines = true;
            TodoListView.Name = "TodoListView";
            TodoListView.UseCompatibleStateImageBehavior = false;
            TodoListView.View = View.Details;
            TodoListView.MouseClick += TodoListView_MouseClick;
            // 
            // columnHeaderId
            // 
            resources.ApplyResources(columnHeaderId, "columnHeaderId");
            // 
            // columnHeaderName
            // 
            resources.ApplyResources(columnHeaderName, "columnHeaderName");
            // 
            // columnHeaderStartDate
            // 
            resources.ApplyResources(columnHeaderStartDate, "columnHeaderStartDate");
            // 
            // columnHeaderEndDate
            // 
            resources.ApplyResources(columnHeaderEndDate, "columnHeaderEndDate");
            // 
            // columnHeaderStatus
            // 
            resources.ApplyResources(columnHeaderStatus, "columnHeaderStatus");
            // 
            // columnHeaderPriority
            // 
            resources.ApplyResources(columnHeaderPriority, "columnHeaderPriority");
            // 
            // buttonAjouter
            // 
            resources.ApplyResources(buttonAjouter, "buttonAjouter");
            buttonAjouter.BackColor = Color.Green;
            buttonAjouter.ForeColor = SystemColors.ButtonHighlight;
            buttonAjouter.Name = "buttonAjouter";
            buttonAjouter.UseVisualStyleBackColor = false;
            buttonAjouter.Click += OnAjouterClick;
            // 
            // searchButton
            // 
            resources.ApplyResources(searchButton, "searchButton");
            searchButton.Name = "searchButton";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += SearchButton_Click;
            // 
            // searchBox
            // 
            resources.ApplyResources(searchBox, "searchBox");
            searchBox.BackColor = Color.WhiteSmoke;
            searchBox.ForeColor = Color.Black;
            searchBox.Name = "searchBox";
            searchBox.TextChanged += searchBox_TextChanged;
            // 
            // Modifier_btn
            // 
            resources.ApplyResources(Modifier_btn, "Modifier_btn");
            Modifier_btn.BackColor = Color.MediumTurquoise;
            Modifier_btn.ForeColor = SystemColors.ButtonHighlight;
            Modifier_btn.Name = "Modifier_btn";
            Modifier_btn.UseVisualStyleBackColor = false;
            Modifier_btn.Click += Modifier_btn_Click;
            // 
            // Supprimer
            // 
            resources.ApplyResources(Supprimer, "Supprimer");
            Supprimer.BackColor = Color.Red;
            Supprimer.ForeColor = SystemColors.ButtonHighlight;
            Supprimer.Name = "Supprimer";
            Supprimer.UseVisualStyleBackColor = false;
            Supprimer.Click += Supprimer_Click;
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            panel1.BackColor = SystemColors.ActiveCaptionText;
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(label1);
            panel1.ForeColor = SystemColors.ControlText;
            panel1.Name = "panel1";
            panel1.Paint += panel1_Paint;
            // 
            // pictureBox1
            // 
            resources.ApplyResources(pictureBox1, "pictureBox1");
            pictureBox1.Name = "pictureBox1";
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.BackColor = Color.Transparent;
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.ForeColor = Color.White;
            label1.Name = "label1";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            Controls.Add(panel1);
            Controls.Add(Supprimer);
            Controls.Add(Modifier_btn);
            Controls.Add(searchBox);
            Controls.Add(searchButton);
            Controls.Add(buttonAjouter);
            Controls.Add(TodoListView);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            Load += Form1_Load;
            Click += SearchButton_Click;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion

        private System.Windows.Forms.ListView TodoListView;
        private System.Windows.Forms.ColumnHeader columnHeaderId;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderStartDate;
        private System.Windows.Forms.ColumnHeader columnHeaderEndDate;
        private System.Windows.Forms.ColumnHeader columnHeaderStatus;
        private System.Windows.Forms.ColumnHeader columnHeaderPriority;
        private System.Windows.Forms.Button buttonAjouter;
        private Button searchButton;
        private TextBox searchBox;
        private Button Modifier_btn;
        private Button Supprimer;
        private Panel panel1;
        private Label label1;
        private PictureBox pictureBox1;
    }
}