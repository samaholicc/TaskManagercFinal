using System;
using System.Linq;
using System.Windows.Forms;
using NLog; // Add this directive for logging
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using MyAppTodo;

namespace MyAppTodo
{
    public partial class Form1 : Form
    {
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

        private void OnAjouterClick(object sender, EventArgs e)
        {
            var newTask = ShowAddDialog();
            if (newTask != null)
            {
                _todoRepository.Add(newTask); // Add the new task
                ChargerTaches(); // Reload the task list
                logger.Info($"Task added: {newTask.Name}"); // Log the addition
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

        private int selectedTaskId; // Variable to store the selected task ID

        private void TodoListView_MouseClick(object sender, MouseEventArgs e)
        {
            var hitTest = TodoListView.HitTest(e.Location);
            if (hitTest.Item != null)
            {
                selectedTaskId = int.Parse(hitTest.Item.Text); // Get the ID of the selected task
            }
        }

        private void ModifierTache(int id)
        {
            var tache = _todoRepository.GetAll().FirstOrDefault(t => t.Id == id);
            if (tache != null)
            {
                var updatedTask = ShowUpdateDialog(tache);
                if (updatedTask != null)
                {
                    _todoRepository.Update(updatedTask);
                    ChargerTaches(); // Reload the task list
                    logger.Info($"Task modified: {updatedTask.Name}"); // Log the modification
                }
            }
        }

        private void SupprimerTache(int id)
        {
            var result = MessageBox.Show("Are you sure you want to delete this task?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                _todoRepository.Delete(id); // Delete the selected task
                ChargerTaches(); // Reload the task list
                logger.Info($"Task deleted: ID {id}"); // Log the deletion
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

            // Create the add task dialog
            Form prompt = new Form
            {
                Width = 400,
                Height = 400,
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

            // Add controls to the form
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

            // Show the dialog
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
                tache.Name = nameBox.Text;
                tache.StartDate = startDatePicker.Value;
                tache.EndDate = endDatePicker.Value;
                tache.Status = statusBox.SelectedItem.ToString();
                tache.Priority = (int)priorityBox.Value;
                return tache;
            }

            return null;
        }
    }
}
