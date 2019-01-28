namespace InstagramApiSharp.Classes.Models
{
    public class InstaStoryQuestionItem
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public float Rotation { get; set; }

        public int IsPinned { get; set; }

        public int IsHidden { get; set; }

        public InstaStoryQuestionStickerItem QuestionSticker { get; set; } = new InstaStoryQuestionStickerItem();
    }
}
