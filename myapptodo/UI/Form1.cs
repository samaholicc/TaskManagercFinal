using System;
using System.Linq;
using System.Windows.Forms;
using NLog; // Add this directive for logging
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MyAppTodo;

namespace MyAppTodo
{
    public partial class Form1 : Form
    {
<<<<<<< HEAD
        private static readonly Logger logger = LogManager.GetCurrentClassLogger(); // Create the logger
        private readonly TodoRepository _todoRepository; // Declare the task repository

        public Form1()
        {
            InitializeComponent(); // Initialize components
            new Database().InitializeDatabase(); // Initialize the database
            _todoRepository = new TodoRepository(); // Initialize the repository
            ChargerTaches(); // Load tasks from the repository

            // Configure the search bar
            InitializeSearchBar();
        }

=======
        private readonly TodoRepository _todoRepository; // D�clare le d�p�t de t�ches
        public Form1()
        {
            InitializeComponent(); // Initialise les composants de l'interface
            new Database().InitializeDatabase(); // Initialise la base de donn�es
            _todoRepository = new TodoRepository(); // Initialise le d�p�t
            ChargerTaches(); // Charge les t�ches depuis le d�p�t

            // Configuration de la barre de recherche
            InitializeSearchBar();
        }
>>>>>>> 1023f2c5db4adc178a50a92bc1a0e819b37783b9
        private void OnAjouterClick(object sender, EventArgs e)
        {
            var newTask = ShowAddDialog();
            if (newTask != null)
            {
<<<<<<< HEAD
                _todoRepository.Add(newTask); // Add the new task
                ChargerTaches(); // Reload the task list
                logger.Info($"Task added: {newTask.Name}"); // Log the addition
=======
                _todoRepository.Add(newTask); // Ajoute la nouvelle t�che
                ChargerTaches(); // Recharge la liste
>>>>>>> 1023f2c5db4adc178a50a92bc1a0e819b37783b9
            }
        }

        private void InitializeSearchBar()
        {
            searchButton.Click += SearchButton_Click;
            this.Controls.Add(searchBox);
            this.Controls.Add(searchButton);
        }

        private void SearchButton_Click(object? sender, EventArgs e)
        {
            string searchTerm = searchBox.Text.ToLower();
            foreach (ListViewItem item in TodoListView.Items)
            {
                item.Selected = item.SubItems[1].Text.ToLower().Contains(searchTerm);
<<<<<<< HEAD
                item.BackColor = item.Selected ? SystemColors.Highlight : SystemColors.Window; // Change color for selected item
=======
                item.BackColor = item.Selected ? SystemColors.Highlight : SystemColors.Window; // Change couleur pour l'�l�ment s�lectionn�
>>>>>>> 1023f2c5db4adc178a50a92bc1a0e819b37783b9
            }
        }

        private void ChargerTaches()
        {
            TodoListView.Items.Clear();
<<<<<<< HEAD
            var taches = _todoRepository.GetAll(); // Retrieve tasks
=======
            var taches = _todoRepository.GetAll(); // R�cup�re les t�ches
>>>>>>> 1023f2c5db4adc178a50a92bc1a0e819b37783b9
            foreach (var tache in taches)
            {
                var item = new ListViewItem(tache.Id.ToString())
                {
                    SubItems =
                    {
                        tache.Name,
                        tache.StartDate.ToString("g"),
                        tache.EndDate.ToString("g"),
                        tache.Status,
                        tache.Priority.ToString()
                    }
                };

                TodoListView.Items.Add(item);
            }
        }

<<<<<<< HEAD
        private int selectedTaskId; // Variable to store the selected task ID
=======
        private int selectedTaskId; // Variable pour stocker l'ID de la t�che s�lectionn�e
>>>>>>> 1023f2c5db4adc178a50a92bc1a0e819b37783b9

        private void TodoListView_MouseClick(object sender, MouseEventArgs e)
        {
            var hitTest = TodoListView.HitTest(e.Location);
            if (hitTest.Item != null)
            {
<<<<<<< HEAD
                selectedTaskId = int.Parse(hitTest.Item.Text); // Get the ID of the selected task
            }
        }

=======
                selectedTaskId = int.Parse(hitTest.Item.Text); // R�cup�rer l'ID de la t�che s�lectionn�e

            }
        }
>>>>>>> 1023f2c5db4adc178a50a92bc1a0e819b37783b9
        private void ModifierTache(int id)
        {
            var tache = _todoRepository.GetAll().FirstOrDefault(t => t.Id == id);
            if (tache != null)
            {
                var updatedTask = ShowUpdateDialog(tache);
                if (updatedTask != null)
                {
<<<<<<< HEAD
                    _todoRepository.Update(updatedTask);
                    ChargerTaches(); // Reload the task list
                    logger.Info($"Task modified: {updatedTask.Name}"); // Log the modification
=======
                    _todoRepository.Update(updatedTask); 
                    ChargerTaches(); // Recharge la liste
>>>>>>> 1023f2c5db4adc178a50a92bc1a0e819b37783b9
                }
            }
        }

        private void SupprimerTache(int id)
        {
<<<<<<< HEAD
            var result = MessageBox.Show("Are you sure you want to delete this task?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                _todoRepository.Delete(id); // Delete the selected task
                ChargerTaches(); // Reload the task list
                logger.Info($"Task deleted: ID {id}"); // Log the deletion
=======
            var result = MessageBox.Show("�tes-vous s�r de vouloir supprimer cette t�che ?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                _todoRepository.Delete(id); // Supprime la t�che s�lectionn�e
                ChargerTaches(); // Recharge la liste
>>>>>>> 1023f2c5db4adc178a50a92bc1a0e819b37783b9
            }
        }

        private Todo ShowAddDialog()
        {
<<<<<<< HEAD
=======
            
>>>>>>> 1023f2c5db4adc178a50a92bc1a0e819b37783b9
            Label CreateTransparentLabel(string text, int left, int top)
            {
                return new Label
                {
                    Text = text,
                    Left = left,
                    Top = top,
                    AutoSize = true,
<<<<<<< HEAD
                    BackColor = System.Drawing.Color.Transparent
                };
            }

            // Create the add task dialog
=======
                    BackColor = System.Drawing.Color.Transparent 
                };
            }

            // Cr�e une bo�te de dialogue pour ajouter une nouvelle t�che
>>>>>>> 1023f2c5db4adc178a50a92bc1a0e819b37783b9
            Form prompt = new Form
            {
                Width = 400,
                Height = 400,
<<<<<<< HEAD
                Text = "Add Task",
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            const int spacing = 5;

            Label nameLabel = CreateTransparentLabel("Task Name", 50, 20);
            TextBox nameBox = new TextBox { Left = 50, Top = nameLabel.Bottom + spacing, Width = 300 };

            Label startDateLabel = CreateTransparentLabel("Start Date", 50, nameBox.Bottom + spacing);
            DateTimePicker startDatePicker = new DateTimePicker { Left = 50, Top = startDateLabel.Bottom + spacing, Width = 300, Value = DateTime.Now };

            Label endDateLabel = CreateTransparentLabel("End Date", 50, startDatePicker.Bottom + spacing);
            DateTimePicker endDatePicker = new DateTimePicker { Left = 50, Top = endDateLabel.Bottom + spacing, Width = 300, Value = DateTime.Now.AddDays(1) };

            Label statusLabel = CreateTransparentLabel("Status", 50, endDatePicker.Bottom + spacing);
            ComboBox statusBox = new ComboBox { Left = 50, Top = statusLabel.Bottom + spacing, Width = 300 };
            statusBox.Items.AddRange(new[] { "In Progress", "Completed", "Suspended" });
            statusBox.SelectedIndex = 0; // Default to "In Progress"

            Label priorityLabel = CreateTransparentLabel("Priority", 50, statusBox.Bottom + spacing);
            NumericUpDown priorityBox = new NumericUpDown { Left = 50, Top = priorityLabel.Bottom + spacing, Width = 300, Minimum = 1, Maximum = 5 };

            Button confirmation = new Button { Text = "Add", Width = 100, Top = priorityBox.Bottom + spacing + 10, Enabled = false };
=======
                Text = "Ajouter T�che",
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog, 
                MaximizeBox = false, 
                MinimizeBox = false 
            };

            // D�finir un espacement entre les contr�les
            const int spacing = 5; 

            // Cr�er les contr�les avec des positions ajust�es
            Label nameLabel = CreateTransparentLabel("Nom de la t�che", 50, 20);
            TextBox nameBox = new TextBox { Left = 50, Top = nameLabel.Bottom + spacing, Width = 300 };

            Label startDateLabel = CreateTransparentLabel("Date de d�but", 50, nameBox.Bottom + spacing);
            DateTimePicker startDatePicker = new DateTimePicker { Left = 50, Top = startDateLabel.Bottom + spacing, Width = 300, Value = DateTime.Now };

            Label endDateLabel = CreateTransparentLabel("Date de fin", 50, startDatePicker.Bottom + spacing);
            DateTimePicker endDatePicker = new DateTimePicker { Left = 50, Top = endDateLabel.Bottom + spacing, Width = 300, Value = DateTime.Now.AddDays(1) };

            Label statusLabel = CreateTransparentLabel("Statut", 50, endDatePicker.Bottom + spacing);
            ComboBox statusBox = new ComboBox { Left = 50, Top = statusLabel.Bottom + spacing, Width = 300 };
            statusBox.Items.AddRange(new[] { "En cours", "Termin�e", "Suspendue" });
            statusBox.SelectedIndex = 0; // S�lectionne "En cours" par d�faut

            Label priorityLabel = CreateTransparentLabel("Priorit�", 50, statusBox.Bottom + spacing);
            NumericUpDown priorityBox = new NumericUpDown { Left = 50, Top = priorityLabel.Bottom + spacing, Width = 300, Minimum = 1, Maximum = 5 };

            Button confirmation = new Button { Text = "Ajouter", Width = 100, Top = priorityBox.Bottom + spacing + 10, Enabled = false };
>>>>>>> 1023f2c5db4adc178a50a92bc1a0e819b37783b9
            confirmation.Left = (prompt.ClientSize.Width - confirmation.Width) / 2;

            confirmation.Click += (sender, e) =>
            {
                prompt.DialogResult = DialogResult.OK;
                prompt.Close();
            };

<<<<<<< HEAD
=======
            // V�rifie si tous les champs requis sont remplis
>>>>>>> 1023f2c5db4adc178a50a92bc1a0e819b37783b9
            void UpdateButtonState()
            {
                confirmation.Enabled = !string.IsNullOrWhiteSpace(nameBox.Text) && statusBox.SelectedItem != null;
            }

            nameBox.TextChanged += (sender, e) => UpdateButtonState();
            statusBox.SelectedIndexChanged += (sender, e) => UpdateButtonState();

<<<<<<< HEAD
=======
            // Ajouter les contr�les au formulaire
>>>>>>> 1023f2c5db4adc178a50a92bc1a0e819b37783b9
            prompt.Controls.Add(nameLabel);
            prompt.Controls.Add(nameBox);
            prompt.Controls.Add(startDateLabel);
            prompt.Controls.Add(startDatePicker);
            prompt.Controls.Add(endDateLabel);
            prompt.Controls.Add(endDatePicker);
            prompt.Controls.Add(statusLabel);
            prompt.Controls.Add(statusBox);
            prompt.Controls.Add(priorityLabel);
            prompt.Controls.Add(priorityBox);
            prompt.Controls.Add(confirmation);

<<<<<<< HEAD
=======
            // Affiche le formulaire
>>>>>>> 1023f2c5db4adc178a50a92bc1a0e819b37783b9
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                return new Todo
                {
                    Name = nameBox.Text,
                    StartDate = startDatePicker.Value,
                    EndDate = endDatePicker.Value,
                    Status = statusBox.SelectedItem.ToString(),
                    Priority = (int)priorityBox.Value
                };
            }

            return null;
        }

<<<<<<< HEAD
        private Todo ShowUpdateDialog(Todo tache)
        {
=======

        private Todo ShowUpdateDialog(Todo tache)
        {
            
>>>>>>> 1023f2c5db4adc178a50a92bc1a0e819b37783b9
            Label CreateTransparentLabel(string text, int left, int top)
            {
                return new Label
                {
                    Text = text,
                    Left = left,
                    Top = top,
                    AutoSize = true,
<<<<<<< HEAD
                    BackColor = System.Drawing.Color.Transparent
=======
                    BackColor = System.Drawing.Color.Transparent 
>>>>>>> 1023f2c5db4adc178a50a92bc1a0e819b37783b9
                };
            }

            Form prompt = new Form
            {
                Width = 400,
                Height = 400,
<<<<<<< HEAD
                Text = "Modify Task",
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            const int spacing = 5;

            Label nameLabel = CreateTransparentLabel("Task Name", 50, 20);
            TextBox nameBox = new TextBox { Left = 50, Top = nameLabel.Bottom + spacing, Width = 300, Text = tache.Name };

            Label startDateLabel = CreateTransparentLabel("Start Date", 50, nameBox.Bottom + spacing);
            DateTimePicker startDatePicker = new DateTimePicker { Left = 50, Top = startDateLabel.Bottom + spacing, Width = 300, Value = tache.StartDate };

            Label endDateLabel = CreateTransparentLabel("End Date", 50, startDatePicker.Bottom + spacing);
            DateTimePicker endDatePicker = new DateTimePicker { Left = 50, Top = endDateLabel.Bottom + spacing, Width = 300, Value = tache.EndDate };

            Label statusLabel = CreateTransparentLabel("Status", 50, endDatePicker.Bottom + spacing);
            ComboBox statusBox = new ComboBox { Left = 50, Top = statusLabel.Bottom + spacing, Width = 300 };
            statusBox.Items.AddRange(new[] { "In Progress", "Completed", "Suspended" });
            statusBox.SelectedItem = tache.Status;

            Label priorityLabel = CreateTransparentLabel("Priority", 50, statusBox.Bottom + spacing);
            NumericUpDown priorityBox = new NumericUpDown { Left = 50, Top = priorityLabel.Bottom + spacing, Width = 300, Value = tache.Priority, Minimum = 1, Maximum = 5 };

            Button confirmation = new Button { Text = "Modify", Width = 100, Top = priorityBox.Bottom + spacing + 10, Enabled = false };
=======
                Text = "Modifier T�che",
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog, 
                MaximizeBox = false,
                MinimizeBox = false 
            };

            
            const int spacing = 5; 

            
            Label nameLabel = CreateTransparentLabel("Nom de la t�che", 50, 20);
            TextBox nameBox = new TextBox { Left = 50, Top = nameLabel.Bottom + spacing, Width = 300, Text = tache.Name };

            Label startDateLabel = CreateTransparentLabel("Date de d�but", 50, nameBox.Bottom + spacing);
            DateTimePicker startDatePicker = new DateTimePicker { Left = 50, Top = startDateLabel.Bottom + spacing, Width = 300, Value = tache.StartDate };

            Label endDateLabel = CreateTransparentLabel("Date de fin", 50, startDatePicker.Bottom + spacing);
            DateTimePicker endDatePicker = new DateTimePicker { Left = 50, Top = endDateLabel.Bottom + spacing, Width = 300, Value = tache.EndDate };

            Label statusLabel = CreateTransparentLabel("Statut", 50, endDatePicker.Bottom + spacing);
            ComboBox statusBox = new ComboBox { Left = 50, Top = statusLabel.Bottom + spacing, Width = 300 };
            statusBox.Items.AddRange(new[] { "En cours", "Termin�e", "Suspendue" });
            statusBox.SelectedItem = tache.Status;

            Label priorityLabel = CreateTransparentLabel("Priorit�", 50, statusBox.Bottom + spacing);
            NumericUpDown priorityBox = new NumericUpDown { Left = 50, Top = priorityLabel.Bottom + spacing, Width = 300, Value = tache.Priority, Minimum = 1, Maximum = 5 };

            Button confirmation = new Button { Text = "Modifier", Width = 100, Top = priorityBox.Bottom + spacing + 10, Enabled = false };
>>>>>>> 1023f2c5db4adc178a50a92bc1a0e819b37783b9
            confirmation.Left = (prompt.ClientSize.Width - confirmation.Width) / 2;

            confirmation.Click += (sender, e) =>
            {
                prompt.DialogResult = DialogResult.OK;
                prompt.Close();
            };

            void UpdateButtonState()
            {
                confirmation.Enabled = !string.IsNullOrWhiteSpace(nameBox.Text) && statusBox.SelectedItem != null;
            }

<<<<<<< HEAD
            UpdateButtonState();

            nameBox.TextChanged += (sender, e) => UpdateButtonState();
            statusBox.SelectedIndexChanged += (sender, e) => UpdateButtonState();

=======
            nameBox.TextChanged += (sender, e) => UpdateButtonState();
            statusBox.SelectedIndexChanged += (sender, e) => UpdateButtonState();

            
>>>>>>> 1023f2c5db4adc178a50a92bc1a0e819b37783b9
            prompt.Controls.Add(nameLabel);
            prompt.Controls.Add(nameBox);
            prompt.Controls.Add(startDateLabel);
            prompt.Controls.Add(startDatePicker);
            prompt.Controls.Add(endDateLabel);
            prompt.Controls.Add(endDatePicker);
            prompt.Controls.Add(statusLabel);
            prompt.Controls.Add(statusBox);
            prompt.Controls.Add(priorityLabel);
            prompt.Controls.Add(priorityBox);
            prompt.Controls.Add(confirmation);

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                return new Todo
                {
                    Id = tache.Id,
                    Name = nameBox.Text,
                    StartDate = startDatePicker.Value,
                    EndDate = endDatePicker.Value,
                    Status = statusBox.SelectedItem.ToString(),
                    Priority = (int)priorityBox.Value
                };
            }

            return null;
        }
<<<<<<< HEAD

        private void Modifier_btn_Click(object sender, EventArgs e)
        {
            if (selectedTaskId > 0) // Check if a task is selected
=======
        private void Modifier_btn_Click(object sender, EventArgs e)
        {
            if (selectedTaskId > 0) // V�rifie si une t�che est s�lectionn�e
>>>>>>> 1023f2c5db4adc178a50a92bc1a0e819b37783b9
            {
                var tache = _todoRepository.GetAll().FirstOrDefault(t => t.Id == selectedTaskId);
                if (tache != null)
                {
                    var updatedTask = ShowUpdateDialog(tache);
<<<<<<< HEAD
                    if (updatedTask != null) // If the task has been modified
                    {
                        _todoRepository.Update(updatedTask);
                        ChargerTaches(); // Reload the task list
                        logger.Info($"Task modified: {updatedTask.Name}"); // Log the modification
=======
                    if (updatedTask != null) // Si la t�che a �t� modifi�e
                    {
                        _todoRepository.Update(updatedTask); // Met � jour la t�che
                        ChargerTaches(); // Recharge la liste
>>>>>>> 1023f2c5db4adc178a50a92bc1a0e819b37783b9
                    }
                }
            }
            else
            {
<<<<<<< HEAD
                MessageBox.Show("Please select a task to modify.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

=======
                MessageBox.Show("Veuillez s�lectionner une t�che � modifier.", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



>>>>>>> 1023f2c5db4adc178a50a92bc1a0e819b37783b9
        private void Supprimer_Click(object sender, EventArgs e)
        {
            SupprimerTache(selectedTaskId);
        }

<<<<<<< HEAD
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
=======

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

>>>>>>> 1023f2c5db4adc178a50a92bc1a0e819b37783b9
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
<<<<<<< HEAD
=======

>>>>>>> 1023f2c5db4adc178a50a92bc1a0e819b37783b9
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
<<<<<<< HEAD
        }
    }
}
=======

        }
    }
}
>>>>>>> 1023f2c5db4adc178a50a92bc1a0e819b37783b9
