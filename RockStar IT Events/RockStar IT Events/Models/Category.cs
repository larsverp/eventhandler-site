namespace RockStar_IT_Events.Models
{
    public class Category
    {
        public string CategoryName { get; private set; }

        public string CategoryDescription { get; private set; }

        public string CategoryThumbnail { get; private set; }

        public Category(string categoryname, string categorydescription, string categorythumbnail)
        {
            CategoryName = categoryname;
            CategoryDescription = categorydescription;
            CategoryThumbnail = categorythumbnail;
        }

        public void SetName(string setCategoryname)
        {
            CategoryName = setCategoryname;
        }

        public void SetDescription(string setCategorydescription)
        {
            CategoryDescription = setCategorydescription;
        }

        public void SetThumbnail(string setCategorythumbnail)
        {
            CategoryThumbnail = setCategorythumbnail;
        }

    }
}
