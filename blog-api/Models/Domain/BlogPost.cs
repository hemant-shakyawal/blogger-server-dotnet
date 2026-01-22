namespace blog_api.Models.Domain
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandel { get; set; }
        public  DateTime PublishDate { get; set; }
        public string Author { get; set; }
        public string IsVisible { get; set; }







    }
}
