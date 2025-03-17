using System.Windows.Forms; // Importation nécessaire pour utiliser les composants d'interface utilisateur Windows Forms

/// <summary>
/// Classe statique pour afficher une boîte de dialogue personnalisée pour la saisie de texte.
/// </summary>
public static class Prompt
{
    /// <summary>
    /// Affiche une boîte de dialogue qui demande une saisie à l'utilisateur.
    /// </summary>
    /// <param name="text">Le texte à afficher dans la boîte de dialogue.</param>
    /// <param name="caption">Le titre de la boîte de dialogue.</param>
    /// <returns>Le texte saisi par l'utilisateur.</returns>
    public static string ShowDialog(string text, string caption)
    {
        // Création d'une nouvelle instance de Form (fenêtre de dialogue)
        Form prompt = new Form()
        {
            Width = 400, // Largeur de la fenêtre
            Height = 150, // Hauteur de la fenêtre
            Text = caption // Titre de la fenêtre défini par le paramètre 'caption'
        };

        // Création d'un Label pour afficher le texte passé en paramètre
        Label textLabel = new Label()
        {
            Left = 20, // Position horizontale du label
            Top = 20, // Position verticale du label
            Text = text // Texte à afficher dans le label
        };

        // Création d'un TextBox pour permettre à l'utilisateur d'entrer du texte
        TextBox textBox = new TextBox()
        {
            Left = 20, // Position horizontale du TextBox
            Top = 50, // Position verticale du TextBox
            Width = 340 // Largeur du TextBox
        };

        // Création d'un bouton pour valider la saisie de l'utilisateur
        Button confirmation = new Button()
        {
            Text = "Ok", // Texte du bouton
            Left = 280, // Position horizontale du bouton
            Width = 80, // Largeur du bouton
            Top = 70 // Position verticale du bouton
        };

        // Ajout d'un gestionnaire d'événements pour le clic sur le bouton
        // Lorsque le bouton est cliqué, la fenêtre de dialogue se ferme
        confirmation.Click += (sender, e) => { prompt.Close(); };

        // Ajout des contrôles (label, textbox et bouton) à la fenêtre de dialogue
        prompt.Controls.Add(textLabel); // Ajout du Label au Form
        prompt.Controls.Add(textBox);    // Ajout du TextBox au Form
        prompt.Controls.Add(confirmation); // Ajout du bouton au Form

        // Affichage de la boîte de dialogue et attente de l'interaction de l'utilisateur
        prompt.ShowDialog();

        // Retourne le texte saisi par l'utilisateur dans le TextBox
        return textBox.Text;
    }
}
