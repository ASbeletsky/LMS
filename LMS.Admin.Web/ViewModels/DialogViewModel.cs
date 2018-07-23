namespace LMS.Admin.Web.ViewModels
{
    public class DialogViewModel
    {
        public DialogViewModel(string id)
        {
            Id = id;
        }

        public string Id { get; }

        public string Title { get; set; } = "Confirmation";

        public string Content { get; set; }

        public string SubmitCaption { get; set; } = "Accept";

        public string SubmitAction { get; set; }
        public string SubmitController { get; set; }

        public string CancelCaption { get; set; } = "Cancel";
    }
}
