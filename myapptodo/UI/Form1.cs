using System;
using System.Collections.Generic;
using System.Drawing;
using MyAppTodo;
using NLog;

namespace MyAppTodo
{
    public partial class Form1 : Form
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger(); // Logger initialization
        private readonly TodoRepository _todoRepository; // Declare the task repository
        private int selectedTaskId;

        public Form1()
        {
            InitializeComponent();
            new Database().InitializeDatabase();
            _todoRepository = new TodoRepository();
            ChargerTaches();
            InitializeSearchBar();
            Logger.Info("Application démarrée");
        }

        private void OnAjouterClick(object sender, EventArgs e)
        {
            var newTask = ShowAddDialog();
            if (newTask != null)
            {
                _todoRepository.Add(newTask);
                ChargerTaches();
<<<<<<< HEAD
                Logger.Info($"Tâche ajoutée: {newTask.Nom}");
=======
                Logger.Info($"Tâche ajoutée: {newTask.Name}");
>>>>>>> 5d093ac407a3468ab5104bafc6b6f94107a88c1e
            }
        }

        private void InitializeSearchBar()
        {
            searchButton.Click += SearchButton_Click;
            this.Controls.Add(searchBox);
            this.Controls.Add(searchButton);
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            string searchTerm = searchBox.Text.ToLower();
            foreach (ListViewItem item in TodoListView.Items)
            {
                item.Selected = item.SubItems[1].Text.ToLower().Contains(searchTerm);
                item.BackColor = item.Selected ? SystemColors.Highlight : SystemColors.Window; // Change color for selected item
            }
        }

        private void ChargerTaches()
        {
            TodoListView.Items.Clear();
            var taches = _todoRepository.GetAll(); // Retrieve tasks
            foreach (var tache in taches)
            {
                var item = new ListViewItem(tache.Id.ToString())
                {
                    SubItems =
                    {
                        tache.Nom,
                        tache.StartDate.ToString("g"),
                        tache.EndDate.ToString("g"),
                        tache.Status,
                        tache.Priority.ToString()
                    }
                };

                TodoListView.Items.Add(item);
            }
        }

        private void TodoListView_MouseClick(object sender, MouseEventArgs e)
        {
            var hitTest = TodoListView.HitTest(e.Location);
            if (hitTest.Item != null)
            {
                selectedTaskId = int.Parse(hitTest.Item.Text); // Get the ID of the selected task
                Modifier_btn.Enabled = true;
                Supprimer.Enabled = true;
            }
        }

        private void Modifier_btn_Click(object sender, EventArgs e)
        {
            if (selectedTaskId > 0)
            {
                var tache = _todoRepository.GetAll().FirstOrDefault(t => t.Id == selectedTaskId);
                if (tache != null)
                {
                    var updatedTask = ShowUpdateDialog(tache);
                    if (updatedTask != null)
                    {
                        _todoRepository.Update(updatedTask);
                        ChargerTaches();
<<<<<<< HEAD
                        Logger.Info($"Tâche modifiée: {updatedTask.Nom}");
=======
                        Logger.Info($"Tâche modifiée: {updatedTask.Name}");
>>>>>>> 5d093ac407a3468ab5104bafc6b6f94107a88c1e
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une tâche à modifier.", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Supprimer_Click(object sender, EventArgs e)
        {
            SupprimerTache(selectedTaskId);
        }

        private void SupprimerTache(int id)
        {
            var result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette tâche ?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                _todoRepository.Delete(id);
                ChargerTaches();
                Logger.Info($"Tâche supprimée: ID {id}");
            }
        }

        private Todo ShowAddDialog()
        {
            Label CreateTransparentLabel(string text, int left, int top)
            {
                return new Label
                {
                    Text = text,
                    Left = left,
                    Top = top,
                    AutoSize = true,
                    BackColor = System.Drawing.Color.Transparent
                };
            }

            Form prompt = new Form
            {
                Width = 400,
                Height = 400,
                Text = "Ajouter Tâche",
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            const int spacing = 5;

            Label nameLabel = CreateTransparentLabel("Nom de la tâche", 50, 20);
            TextBox nameBox = new TextBox { Left = 50, Top = nameLabel.Bottom + spacing, Width = 300 };

            Label startDateLabel = CreateTransparentLabel("Date de début", 50, nameBox.Bottom + spacing);
            DateTimePicker startDatePicker = new DateTimePicker { Left = 50, Top = startDateLabel.Bottom + spacing, Width = 300, Value = DateTime.Now };

            Label endDateLabel = CreateTransparentLabel("Date de fin", 50, startDatePicker.Bottom + spacing);
            DateTimePicker endDatePicker = new DateTimePicker { Left = 50, Top = endDateLabel.Bottom + spacing, Width = 300, Value = DateTime.Now.AddDays(1) };

            Label statusLabel = CreateTransparentLabel("Statut", 50, endDatePicker.Bottom + spacing);
            ComboBox statusBox = new ComboBox { Left = 50, Top = statusLabel.Bottom + spacing, Width = 300 };
            statusBox.Items.AddRange(new[] { "En cours", "Terminée", "Suspendue" });
            statusBox.SelectedIndex = 0; // Default "En cours"

            Label priorityLabel = CreateTransparentLabel("Priorité", 50, statusBox.Bottom + spacing);
            NumericUpDown priorityBox = new NumericUpDown { Left = 50, Top = priorityLabel.Bottom + spacing, Width = 300, Minimum = 1, Maximum = 5 };

            Button confirmation = new Button { Text = "Ajouter", Width = 100, Top = priorityBox.Bottom + spacing + 10, Enabled = false };
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

            nameBox.TextChanged += (sender, e) => UpdateButtonState();

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
                    Nom = nameBox.Text,
                    StartDate = startDatePicker.Value,
                    EndDate = endDatePicker.Value,
                    Status = statusBox.SelectedItem.ToString(),
                    Priority = (int)priorityBox.Value
                };
            }

            return null;
        }

        private Todo ShowUpdateDialog(Todo tache)
        {
            Label CreateTransparentLabel(string text, int left, int top)
            {
                return new Label
                {
                    Text = text,
                    Left = left,
                    Top = top,
                    AutoSize = true,
                    BackColor = System.Drawing.Color.Transparent
                };
            }

            Form prompt = new Form
            {
                Width = 400,
                Height = 400,
                Text = "Modifier Tâche",
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            const int spacing = 5;

            Label nameLabel = CreateTransparentLabel("Nom de la tâche", 50, 20);
<<<<<<< HEAD
            TextBox nameBox = new TextBox { Left = 50, Top = nameLabel.Bottom + spacing, Width = 300, Text = tache.Nom };
=======
            TextBox nameBox = new TextBox { Left = 50, Top = nameLabel.Bottom + spacing, Width = 300, Text = tache.Name };
>>>>>>> 5d093ac407a3468ab5104bafc6b6f94107a88c1e

            Label startDateLabel = CreateTransparentLabel("Date de début", 50, nameBox.Bottom + spacing);
            DateTimePicker startDatePicker = new DateTimePicker { Left = 50, Top = startDateLabel.Bottom + spacing, Width = 300, Value = tache.StartDate };

            Label endDateLabel = CreateTransparentLabel("Date de fin", 50, startDatePicker.Bottom + spacing);
            DateTimePicker endDatePicker = new DateTimePicker { Left = 50, Top = endDateLabel.Bottom + spacing, Width = 300, Value = tache.EndDate };

            Label statusLabel = CreateTransparentLabel("Statut", 50, endDatePicker.Bottom + spacing);
            ComboBox statusBox = new ComboBox { Left = 50, Top = statusLabel.Bottom + spacing, Width = 300 };
            statusBox.Items.AddRange(new[] { "En cours", "Terminée", "Suspendue" });
            statusBox.SelectedItem = tache.Status;

            Label priorityLabel = CreateTransparentLabel("Priorité", 50, statusBox.Bottom + spacing);
            NumericUpDown priorityBox = new NumericUpDown { Left = 50, Top = priorityLabel.Bottom + spacing, Width = 300, Value = tache.Priority, Minimum = 1, Maximum = 5 };

            Button confirmation = new Button { Text = "Modifier", Width = 100, Top = priorityBox.Bottom + spacing + 10, Enabled = false };
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

            nameBox.TextChanged += (sender, e) => UpdateButtonState();
            statusBox.SelectedIndexChanged += (sender, e) => UpdateButtonState();

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
                tache.Nom = nameBox.Text;
                tache.StartDate = startDatePicker.Value;
                tache.EndDate = endDatePicker.Value;
                tache.Status = statusBox.SelectedItem.ToString();
                tache.Priority = (int)priorityBox.Value;
                return tache;
            }

            return null;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}