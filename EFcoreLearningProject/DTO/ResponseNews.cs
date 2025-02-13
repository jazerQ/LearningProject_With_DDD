namespace EFcoreLearningProject.DTO
{
    public class ResponseNews
    {
        public string Title { get; set; } = string.Empty;
        public string TextData { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public int Views { get; set; }
        public string PathOfTitleImage { get; set; } = string.Empty;
    }
}
