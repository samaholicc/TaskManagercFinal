using System;
using System.Linq;
using System.Windows.Forms;
using NLog;

namespace MyAppTodo
{
    public partial class Form1 : Form
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly TodoRepository _todoRepository;
        private int selectedTaskId;

        public Form1()
        {
            InitializeComponent(); // Calls designer-generated InitializeComponent
            new MyAppTodo.Database.Database().InitializeDatabase();
            _todoRepository = new TodoRepository();
            ChargerTaches();
            Logger.Info("Application démarrée");
        }

        private void ChargerTaches()
        {
            TodoListView.Items.Clear(); // Relies on designer-declared TodoListView
            var taches = _todoRepository.GetAll().OrderByDescending(t => t.Priority).ToList();
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

        private void OnAjouterClick(object sender, EventArgs e)
        {
            var newTask = ShowAddDialog();
            if (newTask != null)
            {
                _todoRepository.Add(newTask);
                ChargerTaches();
                Logger.Info($"Tâche ajoutée: {newTask.Name}");
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
                        Logger.Info($"Tâche modifiée: {updatedTask.Name}");
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
            var result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette tâche ?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                _todoRepository.Delete(selectedTaskId);
                ChargerTaches();
                Logger.Info($"Tâche supprimée: ID {selectedTaskId}");
            }
        }

        private Todo ShowAddDialog()
        {
            Form prompt = new Form { Width = 400, Height = 400, Text = "Ajouter Tâche", StartPosition = FormStartPosition.CenterScreen };
            var nameBox = new TextBox { Left = 50, Top = 40, Width = 300 };
            var startDatePicker = new DateTimePicker { Left = 50, Top = 80, Width = 300, Value = DateTime.Now };
            var endDatePicker = new DateTimePicker { Left = 50, Top = 120, Width = 300, Value = DateTime.Now.AddDays(1) };
            var statusBox = new ComboBox { Left = 50, Top = 160, Width = 300 };
            statusBox.Items.AddRange(new[] { "En cours", "Terminée", "Suspendue" });
            statusBox.SelectedIndex = 0;
            var priorityBox = new NumericUpDown { Left = 50, Top = 200, Width = 300, Minimum = 1, Maximum = 5 };
            var confirmation = new Button { Text = "Ajouter", Left = 150, Top = 240, Width = 100 };
            confirmation.Click += (s, e) => { prompt.DialogResult = DialogResult.OK; prompt.Close(); };
            prompt.Controls.AddRange(new Control[] { nameBox, startDatePicker, endDatePicker, statusBox, priorityBox, confirmation });

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
            Form prompt = new Form { Width = 400, Height = 400, Text = "Modifier Tâche", StartPosition = FormStartPosition.CenterScreen };
            var nameBox = new TextBox { Left = 50, Top = 40, Width = 300, Text = tache.Name };
            var startDatePicker = new DateTimePicker { Left = 50, Top = 80, Width = 300, Value = tache.StartDate };
            var endDatePicker = new DateTimePicker { Left = 50, Top = 120, Width = 300, Value = tache.EndDate };
            var statusBox = new ComboBox { Left = 50, Top = 160, Width = 300 };
            statusBox.Items.AddRange(new[] { "En cours", "Terminée", "Suspendue" });
            statusBox.SelectedItem = tache.Status;
            var priorityBox = new NumericUpDown { Left = 50, Top = 200, Width = 300, Value = tache.Priority, Minimum = 1, Maximum = 5 };
            var confirmation = new Button { Text = "Modifier", Left = 150, Top = 240, Width = 100 };
            confirmation.Click += (s, e) => { prompt.DialogResult = DialogResult.OK; prompt.Close(); };
            prompt.Controls.AddRange(new Control[] { nameBox, startDatePicker, endDatePicker, statusBox, priorityBox, confirmation });

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

        private void TodoListView_MouseClick(object sender, MouseEventArgs e)
        {
            var hitTest = TodoListView.HitTest(e.Location);
            if (hitTest.Item != null)
            {
                selectedTaskId = int.Parse(hitTest.Item.Text);
                Modifier_btn.Enabled = true;
                Supprimer.Enabled = true;
            }
        }
    }
}